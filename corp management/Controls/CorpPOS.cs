using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using eZet.EveLib.Modules;
using eZet.EveLib.Modules.Models;
using eZet.EveLib.Modules.Models.Corporation;
using System.IO;


namespace corp_management.Controls
{
    public partial class CorpPOS : UserControl
    {
        public CorpPOS()
        {
            InitializeComponent();
        }

        public Corporation Corp { get; set; }

        public EveOnlineRowCollection<StarbaseList.Starbase> POSes { get; set; }

        public void CorpPOSLoad()
        {
            if(Corp == null)
            {
                throw new Exception("Please set Corp property before using Load method.");
            }

            POSes = Corp.GetStarbaseList().Result.Starbases;

            if (POSes != null)
            {
                foreach (eZet.EveLib.Modules.Models.Corporation.StarbaseList.Starbase pos in POSes)
                {
                    string typeName = EveOnlineApi.Eve.GetTypeName(pos.TypeId).Result.Types.First().TypeName;
                    
                    POS_treeView.Nodes.Add(pos.ItemId.ToString(), GetCelestialNameFromID(pos.MoonId));
                }
            }
        }

        public string GetCelestialNameFromID(long id)
        {
            string csvFile = @".\StaticData\mapDenormalize.csv";
            string _id = id.ToString();
            char csvSeperator = ',';
            string resultLine = String.Empty;

            foreach(string line in File.ReadLines(csvFile))
            {
                foreach (string value in line.Replace("\"", "").Split('\r', '\n', csvSeperator))
                    if (value.Trim() == _id.Trim())
                    {
                        resultLine = line;
                        break;
                    }
                if (!String.IsNullOrEmpty(resultLine))
                    break;
            }

            return resultLine.Split(',')[11].Replace("\"","");
        }

        private void POS_treeView_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {

            listBoxPOSDetails.Items.Clear();

            StarbaseList.Starbase selectedPOS = GetStarBaseFromCollection(Convert.ToInt64(e.Node.Name));
            
            EveApiResponse<StarbaseDetails> posDetails = Corp.GetStarbaseDetails(selectedPOS.ItemId);
            
            //posDetails.Result.
            string posLocation = GetCelestialNameFromID(selectedPOS.LocationId);
            EveOnlineRowCollection<StarbaseDetails.FuelEntry> fuelInfo = posDetails.Result.Fuel;
            
            foreach(StarbaseDetails.FuelEntry fi in fuelInfo)
            {
                string fuelType = EveOnlineApi.Eve.GetTypeName(fi.TypeId).Result.Types[0].TypeName;
                listBoxPOSDetails.Items.Add("Fuel: " + fuelType + " Amount: " + fi.Quantity.ToString());
            }

            listBoxPOSDetails.Items.Add("Location: " + posLocation);
            listBoxPOSDetails.Items.Add("Status: " + Convert.ToString(selectedPOS.State));
            listBoxPOSDetails.Items.Add("Online since: " + posDetails.Result.OnlineTimestamp.ToShortDateString());
            //listBoxPOSDetails.Items.Add()
        }

        private StarbaseList.Starbase GetStarBaseFromCollection(long itemID)
        {
            StarbaseList.Starbase p = null;
            foreach (eZet.EveLib.Modules.Models.Corporation.StarbaseList.Starbase pos in POSes)
            {
                if (pos.ItemId == itemID)
                {
                    p = pos;
                    break;
                }
            }

            return p;
        }


    }
}
