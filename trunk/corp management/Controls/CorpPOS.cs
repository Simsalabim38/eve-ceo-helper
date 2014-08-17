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

        public void CorpPOSLoad()
        {
            if(Corp == null)
            {
                throw new Exception("Please set Corp property before using Load method.");
            }

            EveApiResponse<StarbaseList> poses = Corp.GetStarbaseList();

            if(poses != null)
            {
                foreach(eZet.EveLib.Modules.Models.Corporation.StarbaseList.Starbase pos in poses.Result.Starbases)
                {
                    string typeName = EveOnlineApi.Eve.GetTypeName(pos.TypeId).Result.Types.First().TypeName;
                    
                    POS_treeView.Nodes.Add(typeName, GetCelestialNameFromID(pos.MoonId));
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


    }
}
