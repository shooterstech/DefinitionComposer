using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Scopos.BabelFish.APIClients;
using Scopos.BabelFish.DataModel.Definitions;

namespace DefinitionComposer.Forms {
    public partial class DownloadDefinitionFile : Form {

        private DefinitionAPIClient _definitionAPIClient;

        public DownloadDefinitionFile( DefinitionAPIClient definitionAPIClient ) {
            InitializeComponent();

            this._definitionAPIClient = definitionAPIClient;
            definitionTypeComboBox.DataSource = Enum.GetValues( typeof( DefinitionType ) );
        }

        private async void downloadButton_Click( object sender, EventArgs e ) {

            SetName sn;

            if ( ! SetName.TryParse( setNameTextBox.Text, out sn)) {
                messageTextBox.Text = $"Unable to parse SetName '{setNameTextBox.Text}'.";
                return;
            }

            try {
                this.Definition = await DefinitionCache.GetDefinitionAsync( (DefinitionType)definitionTypeComboBox.SelectedValue, sn );
                this.DialogResult = DialogResult.OK;
                this.Close();
            } catch (DefinitionNotFoundException ex) {
                MessageBox.Show( ex.Message, "Not Found" );
            }
        }

        private void closeButton_Click( object sender, EventArgs e ) {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        public Definition Definition { get; private set; }
    }
}
