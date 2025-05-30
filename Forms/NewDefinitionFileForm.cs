﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using NLog;
using Scopos.BabelFish.APIClients;
using Scopos.BabelFish.DataModel.Definitions;
using Scopos.BabelFish.Requests.DefinitionAPI;

namespace DefinitionComposer.Forms {
    public partial class NewDefinitionFileForm : Form {

        private ClubsAPIClient _clubsAPIClient;
        private DefinitionAPIClient _definitionAPIClient;
        private Logger _logger = NLog.LogManager.GetCurrentClassLogger();

        public NewDefinitionFileForm(ClubsAPIClient clubsAPIClient, DefinitionAPIClient definitionAPIClient ) {
            InitializeComponent();

            _clubsAPIClient = clubsAPIClient;
            _definitionAPIClient = definitionAPIClient;

            ownerIdTextBox.Text = "OrionAcct000001";
        }

        private async void ownerIdTextBox_TextChanged( object sender, EventArgs e ) {

            var getClubDetailResponse = await _clubsAPIClient.GetClubDetailPublicAsync( ownerIdTextBox.Text );
            if (getClubDetailResponse.StatusCode == System.Net.HttpStatusCode.OK) {
                var clubDetail = getClubDetailResponse.ClubDetail;

                namespaceListBox.Items.Clear();
                var namespaces = new List<string>();
                foreach (var ns in clubDetail.NamespaceList) {
                    namespaces.Add( ns.Namespace );
                    namespaceListBox.Items.Add( ns.Namespace );
                }

                StringBuilder clubInfo = new StringBuilder();
                clubInfo.AppendLine( $"Club Name: {clubDetail.Name}" );
                clubInfo.AppendLine( $"Hometown: {clubDetail.Hometown}" );
                clubInfo.Append( "Namespace: " );
                clubInfo.AppendLine( string.Join( ", ", namespaces ) );

                ownerInformationTextBox.Text = clubInfo.ToString();

            } else {
                ownerInformationTextBox.Text = $"Unable to look up. {getClubDetailResponse.StatusCode}";
            }

        }

        private DefinitionType GetSelectedDefinitionType() {

            foreach (Control control in definitionTypeGroupBox.Controls) {
                if (control is RadioButton rb && rb.Checked) {
                    var defTypeStr = (string)rb.Tag;
                    var definitionType = (DefinitionType)Enum.Parse( typeof( DefinitionType ), defTypeStr );
                    return definitionType;
                }
            }

            return DefinitionType.ATTRIBUTE;
        }

        private async void okButton_Click( object sender, EventArgs e ) {

            if (namespaceListBox.SelectedItem == null || string.IsNullOrEmpty( (string) namespaceListBox.SelectedItem )) {
                MessageBox.Show( "Please select a namespace." );
                return;
            } else if (string.IsNullOrEmpty( properNameTextBox.Text) ) {
                MessageBox.Show( "Please type in a Proper Name" );
                    return;
            }

            try {
                Definition definition;
                switch (GetSelectedDefinitionType()) {
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

                //Use a default Discipline and subdiscipline
                definition.Discipline = DisciplineType.NA;
                definition.Subdiscipline = string.Empty;

                //Set default values
                definition.SetDefaultValues();

                //validate this is indeed a new definition
                var namespaceToUse = (string) namespaceListBox.SelectedItem;
                var setName = SetName.Parse( $"v1.0:{namespaceToUse}:{properNameTextBox.Text}" );
                definition.SetName = setName.ToString();
                definition.HierarchicalName = setName.ToHierarchicalNameString();
                definition.Version = "1.1";
                var request = new GetDefinitionVersionPublicRequest(setName, GetSelectedDefinitionType() );

                var response = await _definitionAPIClient.GetDefinitionVersionPublicAsync( request );

                if (response.StatusCode == System.Net.HttpStatusCode.NotFound) {
                    //Not Found is what we want, as it means the definition doesn't exist yet.
                    this.Definition = definition;
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                } else {
                    MessageBox.Show( $"A Definition of tyhpe {GetSelectedDefinitionType()} and Hierarchical Name {setName.ToHierarchicalNameString()} already exists." );
                }

            } catch (Exception ex) {
                _logger.Error( ex );
                MessageBox.Show( ex.Message );
            }
        }

        public Definition Definition { get; private set; }

        private void closeButton_Click( object sender, EventArgs e ) {
            DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}
