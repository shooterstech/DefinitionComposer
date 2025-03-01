using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;
using System.Text.Json;
using System.Windows.Forms;
using NLog;
using Scopos.BabelFish.APIClients;
using Scopos.BabelFish.DataModel.Clubs;
using Scopos.BabelFish.DataModel.Definitions;
using Scopos.BabelFish.Helpers;
using Scopos.BabelFish.Runtime;

namespace DefinitionComposer {
    public partial class MainForm : Form {

		ClubsAPIClient _clubsAPIClient = null;
		DefinitionAPIClient _definitionAPIClient = null;

		ClubDetail ClubDetail = null;
		TextInfo TextInfo = CultureInfo.CurrentCulture.TextInfo;
		private Logger _logger = NLog.LogManager.GetCurrentClassLogger();

		public MainForm() {
			InitializeComponent();

            DefinitionAPIClient.OnLocalStorageDirectoryChange += OnDefinitionClientLocalStoreDirectoryChange;

            //Todo read the x-api-key and local store directory from an application configuration file
            //x-api-key for the Definition Composer. Set to have a Application - Basic usage plan.
            //This will also set the LocalStoreDirectory for the DefinitionClient
            Initializer.Initialize( "tHBW68fgL0ajeCLvNP91l83q9xl26OiA6zh5LivC", @"c:\temp", false );

            _clubsAPIClient = new ClubsAPIClient();
            _definitionAPIClient = new DefinitionAPIClient();

            loadSettings();
		}

		private void MainForm_FormClosing( object sender, FormClosingEventArgs e ) {
			saveSettings();
		}

		private void loadSettings() {

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
            }
		}

        private async void openToolStripMenuItem_Click( object sender, EventArgs e ) {

			try {
				openFileDialog1.InitialDirectory = DefinitionAPIClient.LocalStoreDirectory.FullName;
				openFileDialog1.DefaultExt = ".json";
				openFileDialog1.Multiselect = false;

				if (openFileDialog1.ShowDialog() == DialogResult.OK) {

					string filePath = openFileDialog1.FileName;
					string json = File.ReadAllText( filePath );

					DefinitionUnderTest = JsonSerializer.Deserialize<Definition>( json, SerializerOptions.SystemTextJsonDeserializer );

					if (await DefinitionUnderTest.GetMeetsSpecificationAsync()) {
						specificationTextBox.Text = "Meets Specifications";
					} else {
						specificationTextBox.Text = string.Join( Environment.NewLine, DefinitionUnderTest.SpecificationMessages );
					}
				}
			} catch (Exception ex) {
				_logger.Error( ex );
				specificationTextBox.Text = ex.Message;
			}
		}

		private async void validateButton_Click( object sender, EventArgs e ) {

			try {
				string fullPath = $"{DefinitionAPIClient.LocalStoreDirectory}\\{DefinitionUnderTest.GetRelativePath()}";
				string json = File.ReadAllText( fullPath );

				DefinitionUnderTest = JsonSerializer.Deserialize<Definition>( json, SerializerOptions.SystemTextJsonDeserializer );

				if (await DefinitionUnderTest.GetMeetsSpecificationAsync()) {
					specificationTextBox.Text = "Meets Specifications";
				} else {
					specificationTextBox.Text = string.Join( Environment.NewLine, DefinitionUnderTest.SpecificationMessages );
				}
			} catch (Exception ex) {
				_logger.Error( ex );
				specificationTextBox.Text = ex.Message;
			}
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
	}
}
