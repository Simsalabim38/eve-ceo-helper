using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using eZet.EveLib.Modules.Models;
using eZet.EveLib.Modules;
using eZet.EveLib.Modules.Models.Character;

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
            
            // Retrieves more detail corp details.
            corp.EnableCacheLoad = false;
            corp.EnableCacheStore = false;
            
            //var test = corp.GetCorporationSheet();
           // var test2 = corp.GetStarbaseList();
            //var test3 = corp.GetStarbaseDetails(1015149294913);


            // TODO: Retrieve Journal Tax data
            DateTime start = new DateTime(2014, 7, 1);
            DateTime stop = DateTime.Now;

            CorpHelper corpHelper = new CorpHelper(corp);
            Dictionary<string,decimal> tData = corpHelper.GetCorporationTaxInformation(start, stop);
            
            ListBox lBox = new ListBox();
            lBox.Location = new Point(10, 60);
            lBox.Size = new Size(500, 400);

            int posYIncrementor = 10;
            foreach(KeyValuePair<string,decimal> dicEntry in tData)
            {
                posYIncrementor += 20;
                string entry = dicEntry.Key + "   " + dicEntry.Value.ToString();
                lBox.Items.Add(entry);
            }
            this.Controls.Add(lBox);

        }
    }
}
