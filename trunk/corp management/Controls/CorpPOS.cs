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
using EveCeoHelper.Helper;
using EveCeoHelper.CoreData;


namespace EveCeoHelper
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

            POS poses = new POS(Corp);
            poses.InitializePOSData();

            //foreach(DataRow row in poses.POSdataSet.Tables[0].Rows)
            //{

            //}

            POSes = Corp.GetStarbaseList().Result.Starbases;

            if (POSes != null)
            {
                foreach (eZet.EveLib.Modules.Models.Corporation.StarbaseList.Starbase pos in POSes)
                {
                    string typeName = EveOnlineApi.Eve.GetTypeName(pos.TypeId).Result.Types.First().TypeName;

                    POS_treeView.Nodes.Add(pos.ItemId.ToString(), Celestials.GetCelestialNameFromID(pos.MoonId));
                }
            }
        }

        /*private void POS_treeView_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {

            listBoxPOSDetails.Items.Clear();

            //DataRow row = Convert.ToInt64(e.Node.Name);
            
            EveApiResponse<StarbaseDetails> posDetails = Corp.GetStarbaseDetails(selectedPOS.ItemId);
            
            string posLocation = Celestials.GetCelestialNameFromID(selectedPOS.LocationId);
            EveOnlineRowCollection<StarbaseDetails.FuelEntry> fuelInfo = posDetails.Result.Fuel;

            ImageList imgLst = GetImageListForPOSES();

            listBoxPOSDetails.SmallImageList = imgLst;
            int fuelQuantity = 0;
            foreach(StarbaseDetails.FuelEntry fi in fuelInfo)
            {
                string fuelType = EveOnlineApi.Eve.GetTypeName(fi.TypeId).Result.Types[0].TypeName;

                if (!fuelType.Equals("Strontium Clathrates"))
                    fuelQuantity = fi.Quantity;

                listBoxPOSDetails.Items.Add(fi.Quantity.ToString(), fi.TypeId.ToString());

            }

            listBoxPOSDetails.Items.Add("Tower-type: " + EveOnlineApi.Eve.GetTypeName(selectedPOS.TypeId).Result.Types.First().TypeName);
            listBoxPOSDetails.Items.Add("Location: " + posLocation);
            listBoxPOSDetails.Items.Add("Status: " + Convert.ToString(selectedPOS.State));
            listBoxPOSDetails.Items.Add("Online since: " + posDetails.Result.OnlineTimestamp.ToShortDateString());
            //listBoxPOSDetails.Items.Add(CalculateOfflineDate(fuelQuantity, EveOnlineApi.Eve.GetTypeName(selectedPOS.TypeId).Result.Types.First().TypeName));
            //listBoxPOSDetails.Items.Add()
        }*/

        private ImageList GetImageListForPOSES()
        {
            int[] fuelblocks = new int[] { 4247,4051,4312,4246,16275 };
            ImageList fuelpics = new ImageList();
            foreach(int typeId in fuelblocks)
            {
                PictureBox pic = new PictureBox() { ImageLocation = @"http://image.eveonline.com/Type/" + typeId + "_32.png" };
                pic.Load();
                fuelpics.Images.Add(typeId.ToString(),pic.Image);
            }

            return fuelpics;
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
