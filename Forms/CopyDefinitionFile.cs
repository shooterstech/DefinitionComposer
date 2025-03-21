using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using NLog;
using Scopos.BabelFish.APIClients;
using Scopos.BabelFish.DataModel.Definitions;
using Scopos.BabelFish.Helpers;
using Scopos.BabelFish.Requests.DefinitionAPI;

namespace DefinitionComposer.Forms {
    public partial class CopyDefinitionFile : Form {

        private DefinitionAPIClient _definitionAPIClient;
        private Logger _logger = NLog.LogManager.GetCurrentClassLogger();

        private Definition _copyFromDefinition;

        public CopyDefinitionFile( DefinitionAPIClient definitionAPIClient, Definition definitionToCopy ) {
            InitializeComponent();
            _definitionAPIClient = definitionAPIClient;
            _copyFromDefinition = definitionToCopy;

            typeTextBox.Text = _copyFromDefinition.Type.ToString();
            copyFromTextBox.Text = _copyFromDefinition.SetName;
            var currentSetName = SetName.Parse( _copyFromDefinition.SetName );
            properNameTextBox.Text = $"COPY of {currentSetName.ProperName}";
        }

        private async void okButton_Click( object sender, EventArgs e ) {

            Definition = _copyFromDefinition.Clone();

            var currentSetName = SetName.Parse( _copyFromDefinition.SetName );
            var namespaceToUse = currentSetName.Namespace;

            var newSetName = SetName.Parse( $"v1.0:{namespaceToUse}:{properNameTextBox.Text}");
            Definition.SetName = newSetName.ToString();
            Definition.HierarchicalName = newSetName.ToHierarchicalNameString();
            Definition.Version = "1.1";

            var request = new GetDefinitionVersionPublicRequest( newSetName, Definition.Type );

            var response = await _definitionAPIClient.GetDefinitionVersionPublicAsync( request );

            if (response.StatusCode == System.Net.HttpStatusCode.NotFound) {
                //Not Found is what we want, as it means the definition doesn't exist yet.
                this.DialogResult = DialogResult.OK;
                this.Close();
            } else {
                MessageBox.Show( $"A Definition of type {Definition.Type} and Hierarchical Name {newSetName.ToHierarchicalNameString()} already exists." );
            }
        }

        public Definition Definition { get; private set; }

        private void cancelButton_Click( object sender, EventArgs e ) {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}
