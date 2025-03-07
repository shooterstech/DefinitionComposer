using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;
using Amazon;
using Amazon.DynamoDBv2;
using Amazon.Runtime;
using DefinitionComposer.Classes;
using NLog;
using Scopos.BabelFish.APIClients;
using Scopos.BabelFish.DataModel.Clubs;
using Scopos.BabelFish.DataModel.Definitions;
using Scopos.BabelFish.Helpers;
using Scopos.BabelFish.Requests.DefinitionAPI;
using Scopos.BabelFish.Responses.DefinitionAPI;
using Scopos.BabelFish.Runtime;

namespace DefinitionComposer {
    public partial class MainForm : Form {

		ClubDetail ClubDetail = null;
		TextInfo TextInfo = CultureInfo.CurrentCulture.TextInfo;
		private Logger _logger = NLog.LogManager.GetCurrentClassLogger();
		private AmazonDynamoDBClient _dynamoDbClient;
		private DefinitionTable _definitionTable;
		private DefinitionAPIClient _definitionAPIClient;
		private ClubsAPIClient _clubsAPIClient;

        public MainForm() {
			InitializeComponent();

            DefinitionAPIClient.OnLocalStorageDirectoryChange += OnDefinitionClientLocalStoreDirectoryChange;

            //Todo read the x-api-key and local store directory from an application configuration file
            //x-api-key for the Definition Composer. Set to have a Application - Basic usage plan.
            //This will also set the LocalStoreDirectory for the DefinitionClient
            Initializer.Initialize( "tHBW68fgL0ajeCLvNP91l83q9xl26OiA6zh5LivC", @"c:\temp", false );
			DefinitionCache.AutoDownloadNewDefinitionVersions = true;

            _clubsAPIClient = new ClubsAPIClient();
            _definitionAPIClient = new DefinitionAPIClient();

            //Relying on the user's credentials in their aws credentials file

            RegionEndpoint endpoint = RegionEndpoint.USEast1;
            _dynamoDbClient = new AmazonDynamoDBClient( new AmazonDynamoDBConfig() {
                RegionEndpoint = endpoint,
                Timeout = TimeSpan.FromSeconds( 10 ),
                ReadWriteTimeout = TimeSpan.FromSeconds( 10 ),
                RetryMode = RequestRetryMode.Adaptive,
                MaxErrorRetry = 3
            } );

			_definitionTable = new DefinitionTable( _dynamoDbClient, _definitionAPIClient );

            disciplineComboBox.DataSource = Enum.GetValues( typeof( DisciplineType ) );
		}

		private void MainForm_FormClosing( object sender, FormClosingEventArgs e ) {
			saveSettings();
		}

		private void saveSettings() {
			ComposerSettings settings = new ComposerSettings();

			settings.OwnerId = ownerIdTextBox.Text;

			settings.Save();
		}

		private async void saveButton_Click( object sender, EventArgs e ) {
		}

		private void properNameTextBox_TextChanged( object sender, EventArgs e ) {
			properNameTextBox.Text = TextInfo.ToTitleCase( properNameTextBox.Text );

        }

		private Definition _definitionUnderTest;
		private string _definitionUnderTestFilePath;
        private Definition DefinitionUnderTest {
			get {
				return _definitionUnderTest;
			}
			set {
				_definitionUnderTest = value;

				typeOfDefinitionTextBox.Text = _definitionUnderTest.Type.Description();
				ownerIdTextBox.Text = _definitionUnderTest.Owner;
				//TODO Look up owner and populate owner information text box
				namespaceTextBox.Text = _definitionUnderTest.GetHierarchicalName().Namespace;
				properNameTextBox.Text = _definitionUnderTest.GetHierarchicalName().ProperName;
				subDisciplineTextBox.Text = _definitionUnderTest.Subdiscipline;
				tagsTextBox.Text = string.Join( "\r\n", _definitionUnderTest.Tags );
				disciplineComboBox.SelectedItem = _definitionUnderTest.Discipline;
				majorVersionUpDown.Value = _definitionUnderTest.GetDefinitionVersion().MajorVersion;
            }
		}

        private async void openToolStripMenuItem_Click( object sender, EventArgs e ) {

			try {
				openFileDialog1.InitialDirectory = DefinitionAPIClient.LocalStoreDirectory.FullName;
				openFileDialog1.DefaultExt = ".json";
                openFileDialog1.Filter = "JSON files (*.json)|*.json";
                openFileDialog1.Multiselect = false;

				if (openFileDialog1.ShowDialog() == DialogResult.OK) {

					_definitionUnderTestFilePath = openFileDialog1.FileName;
					string json = File.ReadAllText( _definitionUnderTestFilePath );
					
					LoadJsonIntoDefinitionUnderTestAsync( json );
                }

            } catch (Exception ex) {
				_logger.Error( ex );
				specificationTextBox.Text = ex.Message;
			}
		}

		private async void validateButton_Click( object sender, EventArgs e ) {

			try {
				//Reload the file, as we expect the user to have made changes.
				string json = File.ReadAllText( _definitionUnderTestFilePath );

				var loadJsonTask = LoadJsonIntoDefinitionUnderTestAsync( json );

                // Cast sender to Button
                Button clickedButton = sender as Button;

                // Ensure the sender is a valid Button and has a Tag value
                if (clickedButton != null && clickedButton.Tag != null) {
					string tagValue = clickedButton.Tag.ToString();

					if ( tagValue == "upload" && await loadJsonTask) {
						UploadDefinitionUnderTest( sender, e );
                    }
				}


            } catch (Exception ex) {
				_logger.Error( ex );
				specificationTextBox.Text = ex.Message;
			}
        }

        private async void UploadDefinitionUnderTest( object sender, EventArgs e ) {
			var saveResponse = await _definitionTable.SaveAsync( DefinitionUnderTest, (int) majorVersionUpDown.Value );
			specificationTextBox.Text = saveResponse.Message;

			if (saveResponse.Success) {
				//DefinitionCache.DownloadNewMinorVersionIfAvaliableAsync( DefinitionUnderTest );
				DefinitionCache.GetAttributeDefinitionAsync( saveResponse.SetName );
			}
        }

		/// <summary>
		/// 
		/// </summary>
		/// <param name="json"></param>
		/// <returns>Boolean indicating if the loaded json is valid, meets specification, and may be uploaded.</returns>
		private async Task<bool> LoadJsonIntoDefinitionUnderTestAsync( string json ) {

            StringBuilder errorMessage = new StringBuilder();
            try {

				//Load it
				DefinitionUnderTest = JsonSerializer.Deserialize<Definition>( json, SerializerOptions.SystemTextJsonDeserializer );

                //Start the new version check task
                var newVersionTask = _definitionUnderTest.IsVersionUpdateAvaliableAsync();

                //Now check the specifications
                if (await DefinitionUnderTest.GetMeetsSpecificationAsync()) {
                    errorMessage.AppendLine( "Meets Specifications" );
                    uploadButton.Enabled = true;
                } else {
					errorMessage.AppendLine( "Specification Errors: " );
					foreach( var sm in DefinitionUnderTest.SpecificationMessages ) {
						errorMessage.AppendLine( sm );
					}
                    uploadButton.Enabled = false;
                }

				//Check for new version
                var newVersionAvaliable = await newVersionTask;
                if (newVersionAvaliable) {
					updateAvailLabel.Visible = true;
                    uploadButton.Enabled = false;
                } else {
                    updateAvailLabel.Visible = false;
                    uploadButton.Enabled = true;
                }

            } catch (JsonException je) {
				_logger.Error( je );
				errorMessage.AppendLine( "JSON Parsing Error" );
				errorMessage.AppendLine( je.Message );
			} catch (Exception ex) {
				_logger.Error( ex );
                errorMessage.AppendLine( "General Exception" );
                errorMessage.AppendLine( ex.Message );
            }

            specificationTextBox.Text = errorMessage.ToString();
			return uploadButton.Enabled;
        }

		/// <summary>
		/// Gets called when the LocalStoreDirectory on the Definition API Client gets updated.
		/// NOTE: Will get called at start up.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="eventArgs"></param>
		private void OnDefinitionClientLocalStoreDirectoryChange( object sender, EventArgs<DirectoryInfo> eventArgs ) {
			definitionFolderTextBox.Text = eventArgs.Value.FullName;
        }

        private async void ownerIdTextBox_TextChanged( object sender, EventArgs e ) {

            var getClubDetailResponse = await _clubsAPIClient.GetClubDetailPublicAsync( ownerIdTextBox.Text );
            if (getClubDetailResponse.StatusCode == System.Net.HttpStatusCode.OK) {
                var clubDetail = getClubDetailResponse.ClubDetail;

                StringBuilder clubInfo = new StringBuilder();
                clubInfo.AppendLine( $"Club Name: {clubDetail.Name}" );
                clubInfo.AppendLine( $"Hometown: {clubDetail.Hometown}" );
                clubInfo.Append( "Namespace: " );
                clubInfo.AppendLine( string.Join( ", ", clubDetail.NamespaceList.Select(n => n.Namespace) ) );

                ownerInformationTextBox.Text = clubInfo.ToString();

            } else {
                ownerInformationTextBox.Text = $"Unable to look up. {getClubDetailResponse.StatusCode}";
            }

        }

        private void openToEditButton_Click( object sender, EventArgs e ) {

			Process.Start( new ProcessStartInfo {
				FileName = _definitionUnderTestFilePath,
				UseShellExecute = true
			} );
        }
    }
}
