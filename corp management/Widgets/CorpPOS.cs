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


namespace EveCeoHelper.Widgets
{
    public partial class CorpPOS : UserControl
    {
        public CorpPOS()
        {
            InitializeComponent();
        }

        public Corporation Corp { get; set; }

        public DataSet POSes { get; set; }

        public void CorpPOSLoad()
        {
            if(Corp == null)
            {
                throw new Exception("Please set Corp property before using Load method.");
            }

            // Initialize POS class and load the data into the dataset
            POS poses = new POS(Corp);
            poses.InitializePOSData();
            POSes = poses.POSdataSet;

            // First add all root-nodes
            foreach (DataRow row in poses.POSdataSet.Tables[0].Rows)
            {
                TreeNode n = POS_treeView.Nodes.Find(row["POSLocationName"].ToString(), true).FirstOrDefault();
                if(n == null)
                    POS_treeView.Nodes.Add(row["POSLocationName"].ToString(), row["POSLocationName"].ToString());
            }

            // Now add all sub-nodes with final pos data
            foreach(DataRow row in poses.POSdataSet.Tables[0].Rows)
            {
                TreeNode node = POS_treeView.Nodes.Find(row["POSLocationName"].ToString(), true).FirstOrDefault();
                if (node != null)
                {
                    node.Nodes.Add(row["POSid"].ToString(), row["POSMoonName"].ToString());
                    continue;
                }
            }

        }

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

        private void POS_treeView_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            long number1 = 0;
            bool canConvert = long.TryParse(e.Node.Name.ToString(), out number1);
            if (canConvert == true)
            {
                DataRow row = POSes.Tables[0].Rows.Find(e.Node.Name);
                if(row != null)
                {
                    listBoxPOSDetails.Items.Clear();
                    ImageList imgLst = GetImageListForPOSES();
                    listBoxPOSDetails.SmallImageList = imgLst;

                    // Add Fuel In
                    listBoxPOSDetails.Items.Add(row["POSFuelQuantity1"].ToString(), row["POSFuelid1"].ToString());
                    listBoxPOSDetails.Items.Add(row["POSFuelQuantity2"].ToString(), row["POSFuelid2"].ToString());
                    listBoxPOSDetails.Items.Add("Tower-type: " + row["POSTypeName"].ToString());
                    listBoxPOSDetails.Items.Add("Location: " + row["POSLocationName"].ToString());
                    listBoxPOSDetails.Items.Add("Status: " + row["POSStatus"].ToString());
                    listBoxPOSDetails.Items.Add("Online since: " + row["POSOnlineSince"].ToString());
                    listBoxPOSDetails.Items.Add(row["POSOfflineDate"].ToString());
                }
            }
            else
            {
                return;
            }
            
        }


    }
}
