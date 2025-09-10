using System;
using System.Threading.Tasks;
using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DocumentModel;
using NLog;
using Scopos.BabelFish.APIClients;
using Scopos.BabelFish.DataModel.Definitions;
using Scopos.BabelFish.Requests.DefinitionAPI;
using Scopos.BabelFish.Runtime;

namespace DefinitionComposer.Classes {
    public class DefinitionTable {

        private Table _definitionTable;
        private const string DEFINITION_TABLE_NAME = "Definition";
        private Logger _logger = NLog.LogManager.GetCurrentClassLogger();
        private IAmazonDynamoDB _dynamoDbClient;
        private DefinitionAPIClient _definitionAPIClient;

        public DefinitionTable( IAmazonDynamoDB dynamoDbClient, DefinitionAPIClient _definitionAPIClient ) {

            this._dynamoDbClient = dynamoDbClient;
            this._definitionAPIClient = _definitionAPIClient;

            if (!Table.TryLoadTable( _dynamoDbClient, DEFINITION_TABLE_NAME, out _definitionTable )) {
                throw new ScoposException( $"Can not load table {DEFINITION_TABLE_NAME}" );
            }
        }

        public async Task<UploadToDynamoResponse> SaveAsync( Definition definition, int majorVersion ) {

            UploadToDynamoResponse response = new UploadToDynamoResponse();
            try {
                var origSetName = definition.SetName;
                var origVersion = definition.Version;
                var origModifiedAt = definition.ModifiedAt;

                var hn = definition.GetHierarchicalName();
                var version = await GetNextMajorVersionNumber( definition, majorVersion );

                //Save the version with a specific version number.
                definition.SetName = $"v{version}:{hn}";
                definition.Version = version;
                definition.ModifiedAt = DateTime.UtcNow;
                var json = definition.SerializeToJson();
                var item = Document.FromJson( json );
                _definitionTable.PutItem( item );

                //Save as most recent overall version, if applicable
                if (await ShouldSaveAsMostRecentVersion( definition, majorVersion )) {
                    definition.SetName = $"v0.0:{hn}";
                    json = definition.SerializeToJson();
                    item = Document.FromJson( json );
                    _definitionTable.PutItem( item );
                }

                //Save as most recent major version
                definition.SetName = $"v{majorVersion}.0:{hn}";
                json = definition.SerializeToJson();
                item = Document.FromJson( json );
                _definitionTable.PutItem( item );

                var msg = $"Successfully uploaded {definition.Type} {definition.SetName} as version {version}.";
                _logger.Info( msg );

                response.Message = msg;
                response.Success = true;
                response.SetName = SetName.Parse( definition.SetName );

            } catch (Exception ex) {
                var msg = $"Could not upload {definition.SetName} with error {ex.Message}";
                _logger.Error( msg, ex );

                response.Message = msg;
                response.Success = false;
                response.SetName = null;
            }

            return response;

        }

        public async Task<string> GetNextMajorVersionNumber( Definition definition, int majorVersion ) {
            
            var hn = definition.GetHierarchicalName();
            var mostRecentVersionSetName = SetName.Parse( $"v{majorVersion}.0:{hn}" );

            var mostRecentVersionRequest = new GetDefinitionVersionPublicRequest( mostRecentVersionSetName, definition.Type ) { 
                IgnoreInMemoryCache = true,
                IgnoreFileSystemCache = true
            };
            var mostRecentVersionResponse = await _definitionAPIClient.GetDefinitionVersionPublicAsync( mostRecentVersionRequest );

            if (mostRecentVersionResponse.HasOkStatusCode) {
                //The happy path
                var apiVersion = mostRecentVersionResponse.Value.GetDefinitionVersion();
                return $"{apiVersion.MajorVersion}.{apiVersion.MinorVersion + 1}";
            } else if (mostRecentVersionResponse.RestApiStatusCode == System.Net.HttpStatusCode.NotFound) {
                //Likely means that this is a new Definition, that's not been uploaded before
                return $"{majorVersion}.{1}";
            } else {
                //Throw an error as something unexpected happen.
                throw new ScoposAPIException( $"Unable to complete GetDefinitionVersionPublicAsync request with status code {mostRecentVersionResponse.OverallStatusCode}." );
            }
        }

        public async Task<bool> ShouldSaveAsMostRecentVersion( Definition definition, int majorVersion ) {

            var hn = definition.GetHierarchicalName();
            var mostRecentVersionSetName = SetName.Parse( $"v0.0:{hn}" );

            var mostRecentVersionRequest = new GetDefinitionVersionPublicRequest( mostRecentVersionSetName, definition.Type );
            var mostRecentVersionResponse = await _definitionAPIClient.GetDefinitionVersionPublicAsync( mostRecentVersionRequest );

            if (mostRecentVersionResponse.HasOkStatusCode) {
                //The happy path
                var apiVersion = mostRecentVersionResponse.Value.GetDefinitionVersion();
                return apiVersion.MajorVersion <= majorVersion;
            } else if (mostRecentVersionResponse.RestApiStatusCode == System.Net.HttpStatusCode.NotFound) {
                //Likely means that this is a new Definition, that's not been uploaded before
                return true;
            } else {
                //Throw an error as something unexpected happen.
                throw new ScoposAPIException( $"Unable to complete GetDefinitionVersionPublicAsync request with status code {mostRecentVersionResponse.OverallStatusCode}." );
            }
        }
    }

    public class UploadToDynamoResponse {
        public bool Success { get; set; }
        public string Message { get; set; }
        public SetName SetName { get; set; }
    }
}
