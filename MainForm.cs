using Scopos.BabelFish.APIClients;
using Scopos.BabelFish.DataModel.Clubs;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Scopos.BabelFish.DataModel.Definitions;
using System.Globalization;

namespace DefinitionComposer {
	public partial class MainForm : Form {

		ClubsAPIClient ClubsClient = null;
		ClubDetail ClubDetail = null;
		TextInfo TextInfo = CultureInfo.CurrentCulture.TextInfo;

		public MainForm() {
			InitializeComponent();

			//x-api-key for the Definition Composer. Set to have a Application - Basic usage plan.
			Scopos.BabelFish.Runtime.Settings.XApiKey = "tHBW68fgL0ajeCLvNP91l83q9xl26OiA6zh5LivC";
			ClubsClient = new ClubsAPIClient();
			loadSettings();
		}

		private void MainForm_FormClosing( object sender, FormClosingEventArgs e ) {
			saveSettings();
		}

		private void definitionFolderTextBox_DoubleClick( object sender, EventArgs e ) {
			definitionFolderBrowserDialog.ShowNewFolderButton = true;
			
			var result = definitionFolderBrowserDialog.ShowDialog();

			if (result == DialogResult.OK) {
				definitionFolderTextBox.Text = definitionFolderBrowserDialog.SelectedPath;
			}
		}

		private void loadSettings() {
			ComposerSettings settings = new ComposerSettings();

			definitionFolderBrowserDialog.RootFolder = Environment.SpecialFolder.MyDocuments;
			if (!string.IsNullOrEmpty( settings.DefinitionDirectory )
				&& Directory.Exists( settings.DefinitionDirectory ) ) {
				
				definitionFolderBrowserDialog.SelectedPath = settings.DefinitionDirectory;
				definitionFolderTextBox.Text = definitionFolderBrowserDialog.SelectedPath;
			} else {
				definitionFolderBrowserDialog.RootFolder = Environment.SpecialFolder.MyDocuments;
			}

			ownerIdTextBox.Text = settings.OwnerId;
		}

		private void saveSettings() {
			ComposerSettings settings = new ComposerSettings();

			settings.DefinitionDirectory = definitionFolderBrowserDialog.SelectedPath;
			settings.OwnerId = ownerIdTextBox.Text;

			settings.Save();
		}

		private async void ownerIdTextBox_TextChanged( object sender, EventArgs e ) {

			var getClubDetailResponse = await ClubsClient.GetClubDetailPublicAsync( ownerIdTextBox.Text );
			if ( getClubDetailResponse.StatusCode == System.Net.HttpStatusCode.OK) {
				ClubDetail = getClubDetailResponse.ClubDetail;


				namespaceListBox.Items.Clear();
				var namespaces = new List<string>();
				foreach (var ns in ClubDetail.NamespaceList) {
					namespaces.Add( ns.Namespace );
					namespaceListBox.Items.Add( ns.Namespace );
				}

				StringBuilder clubInfo = new StringBuilder();
				clubInfo.AppendLine( $"Club Name: {ClubDetail.Name}" );
				clubInfo.AppendLine( $"Hometown: {ClubDetail.Hometown}" );
				clubInfo.Append( "Namespace: " );
				clubInfo.AppendLine( string.Join( ", ", namespaces ) );

				ownerInformationTextBox.Text = clubInfo.ToString();

			} else {
				ownerInformationTextBox.Text = $"Unable to look up. {getClubDetailResponse.StatusCode}";
			}
		}

		private async void saveButton_Click( object sender, EventArgs e ) {

			

			var definition = await DefinitionFactory.Build<StageStyle>( ownerIdTextBox.Text, properNameTextBox.Text, namespaceListBox.Text );


			DirectoryInfo definitionDirectory = new DirectoryInfo( definitionFolderBrowserDialog.SelectedPath );
			definition.SaveToFile( definitionDirectory );
		}

		private void properNameTextBox_TextChanged( object sender, KeyEventArgs e ) {

			properNameTextBox.Text = TextInfo.ToTitleCase( properNameTextBox.Text );
		}
	}
}
