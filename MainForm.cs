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
using DefinitionComposer.Forms;
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
		private RecentFiles _recentFiles = new RecentFiles();

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
			_recentFiles.OnRecentFileAdded += onRecentFileAdded;

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
				openFileDialog1.InitialDirectory = DefinitionAPIClient.LocalStoreDirectory.FullName + "\\DEFINITIONS";
				openFileDialog1.DefaultExt = ".json";
                openFileDialog1.Filter = "JSON files (*.json)|*.json";
                openFileDialog1.Multiselect = false;

				if (openFileDialog1.ShowDialog() == DialogResult.OK) {

					_definitionUnderTestFilePath = openFileDialog1.FileName;
					string json = File.ReadAllText( _definitionUnderTestFilePath );
					
					LoadJsonIntoDefinitionUnderTestAsync( json );

					_recentFiles.Add( new RecentFile( _definitionUnderTestFilePath, DefinitionUnderTest.Type, DefinitionUnderTest.SetName ) );
					openToEditButton_Click( null, null );
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

			DefinitionUnderTest.SaveToFile( new FileInfo( _definitionUnderTestFilePath ) );
        }

		/// <summary>
		/// 
		/// </summary>
		/// <param name="json"></param>
		/// <returns>Boolean indicating if the loaded json is valid, meets specification, and may be uploaded.</returns>
		private async Task<bool> LoadJsonIntoDefinitionUnderTestAsync( string json ) {

			specificationTextBox.Text = "Evaluating ...";
            StringBuilder errorMessage = new StringBuilder();
            try {

				//Load it
				DefinitionUnderTest = JsonSerializer.Deserialize<Definition>( json, SerializerOptions.SystemTextJsonDeserializer );
				DefinitionUnderTest.ConvertValues(); //Normall ConvertValue is called on deserialization from rest api. but since we're loading it from file manually, have to call ConvertValue manually as well.

                //Start the new version check task
                var newVersionTask = DefinitionUnderTest.IsVersionUpdateAvaliableAsync();

				//var eventTree = EventComposite.GrowEventTree( (CourseOfFire) DefinitionUnderTest );
				//var tl = eventTree.GetTopLevelStageStyleEvents();

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
            if (getClubDetailResponse.HasOkStatusCode) {
                var clubDetail = getClubDetailResponse.ClubDetail;

                StringBuilder clubInfo = new StringBuilder();
                clubInfo.AppendLine( $"Club Name: {clubDetail.Name}" );
                clubInfo.AppendLine( $"Hometown: {clubDetail.Hometown}" );
                clubInfo.Append( "Namespace: " );
                clubInfo.AppendLine( string.Join( ", ", clubDetail.NamespaceList.Select(n => n.Namespace) ) );

                ownerInformationTextBox.Text = clubInfo.ToString();

            } else {
                ownerInformationTextBox.Text = $"Unable to look up. {getClubDetailResponse.OverallStatusCode}";
            }

        }

        private void openToEditButton_Click( object sender, EventArgs e ) {

			DefinitionUnderTest.SaveToFile( new FileInfo( _definitionUnderTestFilePath ) );
			Process.Start( new ProcessStartInfo {
				FileName = _definitionUnderTestFilePath,
				UseShellExecute = true
			} );
        }

        private void downloadToolStripMenuItem_Click( object sender, EventArgs e ) {

			var form = new DownloadDefinitionFile( this._definitionAPIClient );

			if (form.ShowDialog() == DialogResult.OK) {

				var definition = form.Definition;
                _definitionUnderTestFilePath = definition.SaveToFile( DefinitionAPIClient.LocalStoreDirectory );
				LoadJsonIntoDefinitionUnderTestAsync( definition.SerializeToJson() );

				_recentFiles.Add( new RecentFile( _definitionUnderTestFilePath, DefinitionUnderTest.Type, DefinitionUnderTest.SetName ) );
                openToEditButton_Click( null, null );
            }
        }

        private void newToolStripMenuItem_Click( object sender, EventArgs e ) {

			NewDefinitionFileForm form = new NewDefinitionFileForm( _clubsAPIClient, _definitionAPIClient );

			if ( form.ShowDialog() == DialogResult.OK ) {

                var definition = form.Definition;
                _definitionUnderTestFilePath = definition.SaveToFile( DefinitionAPIClient.LocalStoreDirectory );
                LoadJsonIntoDefinitionUnderTestAsync( definition.SerializeToJson() );

                openToEditButton_Click( null, null );
                _recentFiles.Add( new RecentFile( _definitionUnderTestFilePath, DefinitionUnderTest.Type, DefinitionUnderTest.SetName ) );
			}
        }

        private void copyToolStripMenuItem_Click( object sender, EventArgs e ) {

			CopyDefinitionFile form = new CopyDefinitionFile( _definitionAPIClient, DefinitionUnderTest );

            if (form.ShowDialog() == DialogResult.OK) {

                var definition = form.Definition;
				definition.ModifiedAt = DateTime.MinValue;
                _definitionUnderTestFilePath = definition.SaveToFile( DefinitionAPIClient.LocalStoreDirectory );
                LoadJsonIntoDefinitionUnderTestAsync( definition.SerializeToJson() );

				openToEditButton_Click( null, null );
				_recentFiles.Add( new RecentFile( _definitionUnderTestFilePath, DefinitionUnderTest.Type, DefinitionUnderTest.SetName ) );
            }

        }

		private void onRecentFileAdded( object sender, EventArgs<RecentFile> e ) {

			recentToolStripMenuItem.DropDownItems.Clear();

			foreach( var recentFile in _recentFiles.Files ) {
				ToolStripMenuItem menuItem = new ToolStripMenuItem( recentFile.ToString() );
				menuItem.Click += onRecentFileSelected;
				menuItem.Tag = recentFile;
				recentToolStripMenuItem.DropDownItems.Add( menuItem );
			}
		}

		private void onRecentFileSelected(  object sender, EventArgs e ) {
			
			var menuItem = (ToolStripMenuItem)sender;
			var fileToOpen = (RecentFile)menuItem.Tag;

			string json = File.ReadAllText( fileToOpen.FilePath );

			_definitionUnderTestFilePath = fileToOpen.FilePath;
			LoadJsonIntoDefinitionUnderTestAsync( json );
            openToEditButton_Click( null, null );

            _recentFiles.Files.Add( fileToOpen );

		}

		/// <summary>
		/// Re-pulls the current definition from the Rest API, replacing what is stored in the file system.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private async void reDownloadToolStripMenuItem_Click( object sender, EventArgs e ) {

			if ( DefinitionUnderTest == null ) {
				MessageBox.Show( "Please open a Definition file first." );
				return;
			}

			if (MessageBox.Show( $"Are you sure you want to re-download {DefinitionUnderTest.SetName}? This will overwrite any changes you may have locally?", "Confirm", MessageBoxButtons.YesNo ) == DialogResult.Yes) {

				//This is a bit of a brute force way to re-download from the rest api.
				//First clear the Initializer cache, whcih clears both the definition cache and the response cache built into babelfish.
				Initializer.ClearCache( false );
				//Next delete the file. As the Definition cache would normally try and read from it.
				File.Delete( _definitionUnderTestFilePath );
				//Now download it.
				var sn = DefinitionUnderTest.GetSetName(true);
				var definition = await DefinitionCache.GetDefinitionAsync( DefinitionUnderTest.Type, sn );
				//Save
				_definitionUnderTestFilePath = definition.SaveToFile( DefinitionAPIClient.LocalStoreDirectory );
				//And load
				await LoadJsonIntoDefinitionUnderTestAsync( definition.SerializeToJson() );

				//Re-open in text editor for good measure, however, probable don't need to.
				openToEditButton_Click( null, null );
			}
		}

        private void moveForTestingButton_Click( object sender, EventArgs e ) {

			var myDocuments = Environment.GetFolderPath( Environment.SpecialFolder.MyDocuments );
			var orionsDefinitionFolder = new DirectoryInfo( $"{myDocuments}//My Matches//DATABASE" ); 
			var testingLocation = DefinitionUnderTest.SaveToFile( orionsDefinitionFolder );

			MessageBox.Show( $"{DefinitionUnderTest} saved to {testingLocation}" );
        }
    }
}
