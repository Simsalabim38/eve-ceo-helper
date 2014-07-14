using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace corp_management.Helper
{
    public partial class CorpDetails : UserControl
    {

        public CorpDetails()
        {
            InitializeComponent();
        }

        public CorpDetails(eZet.EveLib.Modules.Corporation corp)
        {
            InitializeComponent();
            FillCorpDetails(corp);
        }

        private void FillCorpDetails(eZet.EveLib.Modules.Corporation corp)
        {
            // TODO: Complete member initialization
            this.Name = "CorpDetails"; // Name of the control -> mandatory

            this.text_corp_name.Text = corp.CorporationName;
            this.text_alliance_name.Text = corp.AllianceName;
            

        }
    }
}
