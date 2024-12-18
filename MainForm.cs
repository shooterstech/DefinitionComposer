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
// using Amazon.Runtime.Internal.Util;
using NLog;
using Amazon.SecurityToken.Model;
using Newtonsoft.Json;
using Scopos.BabelFish.Helpers;

namespace DefinitionComposer {
	public partial class MainForm : Form {

		ClubsAPIClient ClubsClient = null;
		ClubDetail ClubDetail = null;
		TextInfo TextInfo = CultureInfo.CurrentCulture.TextInfo;
		private Logger Logger = NLog.LogManager.GetCurrentClassLogger();
		private Definition DefinitionUnderTest;
		private string DefinitionUnderTestPath = "";

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
			ownerIdTextBox_TextChanged( null, null );
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


			try {
				Definition definition;
				switch( GetSelectedDefinition() ) {
					case DefinitionType.ATTRIBUTE:
						definition = await DefinitionFactory.Build<Scopos.BabelFish.DataModel.Definitions.Attribute>( ownerIdTextBox.Text, properNameTextBox.Text, namespaceListBox.Text );
						break;
					case DefinitionType.COURSEOFFIRE:
						definition = await DefinitionFactory.Build<CourseOfFire>( ownerIdTextBox.Text, properNameTextBox.Text, namespaceListBox.Text );
						break;
					case DefinitionType.EVENTANDSTAGESTYLEMAPPING:
						definition = await DefinitionFactory.Build<EventAndStageStyleMapping>( ownerIdTextBox.Text, properNameTextBox.Text, namespaceListBox.Text );
						break;
					case DefinitionType.EVENTSTYLE:
						definition = await DefinitionFactory.Build<EventStyle>( ownerIdTextBox.Text, properNameTextBox.Text, namespaceListBox.Text );
						break;
					case DefinitionType.RANKINGRULES:
						definition = await DefinitionFactory.Build<RankingRule>( ownerIdTextBox.Text, properNameTextBox.Text, namespaceListBox.Text );
						break;
					case DefinitionType.RESULTLISTFORMAT:
						definition = await DefinitionFactory.Build<ResultListFormat>( ownerIdTextBox.Text, properNameTextBox.Text, namespaceListBox.Text );
						break;
					case DefinitionType.SCOREFORMATCOLLECTION:
						definition = await DefinitionFactory.Build<ScoreFormatCollection>( ownerIdTextBox.Text, properNameTextBox.Text, namespaceListBox.Text );
						break;
					case DefinitionType.STAGESTYLE:
						definition = await DefinitionFactory.Build<StageStyle>( ownerIdTextBox.Text, properNameTextBox.Text, namespaceListBox.Text );
						break;
					case DefinitionType.TARGET:
						definition = await DefinitionFactory.Build<Target>( ownerIdTextBox.Text, properNameTextBox.Text, namespaceListBox.Text );
						break;
					case DefinitionType.TARGETCOLLECTION:
						definition = await DefinitionFactory.Build<TargetCollection>( ownerIdTextBox.Text, properNameTextBox.Text, namespaceListBox.Text );
						break;
					default:
						//Shouldn't ever get here.
						throw new NotImplementedException();
				}

				DisciplineType discipline = (DisciplineType) Enum.Parse( typeof( DisciplineType ), disciplineComboBox.Text );
				definition.Discipline = discipline;

				definition.Subdiscipline = subDisciplineTextBox.Text;

				foreach( var tag in tagsTextBox.Text.Split( new[] { Environment.NewLine }, StringSplitOptions.None ) ) {
					definition.Tags.Add( tag );
				}

				DirectoryInfo definitionDirectory = new DirectoryInfo( definitionFolderBrowserDialog.SelectedPath );
				definition.SaveToFile( definitionDirectory );
			} catch (Exception ex) {
				Logger.Error( ex );
			}
		}

		private void properNameTextBox_TextChanged( object sender, EventArgs e ) {
			properNameTextBox.Text = TextInfo.ToTitleCase( properNameTextBox.Text );

		}

		private DefinitionType GetSelectedDefinition() {

			foreach (Control control in definitionTypeGroupBox.Controls) {
				if (control is RadioButton rb && rb.Checked) {
					var defTypeStr = (string)rb.Tag;
					var definitionType = (DefinitionType) Enum.Parse( typeof( DefinitionType ), defTypeStr );
					return definitionType;
				}
			}

			return DefinitionType.ATTRIBUTE;
		}

		private async void openToolStripMenuItem_Click( object sender, EventArgs e ) {

			try {
				openFileDialog1.InitialDirectory = definitionFolderBrowserDialog.SelectedPath;
				openFileDialog1.DefaultExt = ".json";
				openFileDialog1.Multiselect = false;

				if (openFileDialog1.ShowDialog() == DialogResult.OK) {

					string filePath = openFileDialog1.FileName;
					string json = File.ReadAllText( filePath );

					DefinitionUnderTest = JsonConvert.DeserializeObject<Definition>( json );
					DefinitionUnderTestPath = filePath;

					definitionTypeLabel.Text = DefinitionUnderTest.Type.Description();
					hierarchicalNameLabel.Text = DefinitionUnderTest.HierarchicalName.ToString();

					if (await DefinitionUnderTest.GetMeetsSpecificationAsync()) {
						specificationTextBox.Text = "Meets Specifications";
					} else {
						specificationTextBox.Text = string.Join( Environment.NewLine, DefinitionUnderTest.SpecificationMessages );
					}
				}
			} catch (Exception ex) {
				Logger.Error( ex );
				specificationTextBox.Text = ex.Message;
			}
		}

		private async void validateButton_Click( object sender, EventArgs e ) {

			try {
				string json = File.ReadAllText( DefinitionUnderTestPath );

				DefinitionUnderTest = JsonConvert.DeserializeObject<Definition>( json );

				definitionTypeLabel.Text = DefinitionUnderTest.Type.Description();
				hierarchicalNameLabel.Text = DefinitionUnderTest.HierarchicalName.ToString();

				if (await DefinitionUnderTest.GetMeetsSpecificationAsync()) {
					specificationTextBox.Text = "Meets Specifications";
				} else {
					specificationTextBox.Text = string.Join( Environment.NewLine, DefinitionUnderTest.SpecificationMessages );
				}
			} catch (Exception ex) {
				Logger.Error( ex );
				specificationTextBox.Text = ex.Message;
			}
		}
	}
}
