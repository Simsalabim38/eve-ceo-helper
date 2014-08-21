using eZet.EveLib.Modules;
using eZet.EveLib.Modules.Models;
using eZet.EveLib.Modules.Models.Corporation;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EveCeoHelper.CoreData
{
    class POS
    {
        public POS(Corporation _corp)
        {
            if (_corp == null)
                throw new Exception("POS class cannot be initialized without a valid Corporation object.");

            Corp = _corp;
        }

        private Corporation Corp { get; set; }

        private EveOnlineRowCollection<StarbaseList.Starbase> POSes { get; set; }

        private DataTable POSList { get; set; }

        public DataSet POSdataSet { get; set; }

        public void InitializePOSData()
        {
            POSes = Corp.GetStarbaseList().Result.Starbases;
            POSList = new DataTable("POSList");

            // Add needed columns to datatable
            POSList.Columns.Add(new DataColumn("POSid", System.Type.GetType("System.Int64")));
            POSList.Columns.Add(new DataColumn("POSTypeName", System.Type.GetType("System.String")));
            POSList.Columns.Add(new DataColumn("POSMoonId", System.Type.GetType("System.Int64")));
            POSList.Columns.Add(new DataColumn("POSMoonName", System.Type.GetType("System.String")));
            POSList.Columns.Add(new DataColumn("POSFuelid1", System.Type.GetType("System.Int64")));
            POSList.Columns.Add(new DataColumn("POSFuelName1", System.Type.GetType("System.String")));
            POSList.Columns.Add(new DataColumn("POSFuelQuantity1", System.Type.GetType("System.Int64")));
            POSList.Columns.Add(new DataColumn("POSFuelid2", System.Type.GetType("System.Int64")));
            POSList.Columns.Add(new DataColumn("POSFuelName2", System.Type.GetType("System.String")));
            POSList.Columns.Add(new DataColumn("POSFuelQuantity2", System.Type.GetType("System.Int64")));
            POSList.Columns.Add(new DataColumn("POSStatus", System.Type.GetType("System.String")));
            POSList.Columns.Add(new DataColumn("POSOnlineSince", System.Type.GetType("System.String")));
            POSList.Columns.Add(new DataColumn("POSOfflineDate", System.Type.GetType("System.String")));
            POSList.Columns.Add(new DataColumn("POSLocationName", System.Type.GetType("System.String")));

            DataColumn[] PrimaryKeyColumns = new DataColumn[1];
            PrimaryKeyColumns[0] = POSList.Columns["POSid"];
            POSList.PrimaryKey = PrimaryKeyColumns;

            POSdataSet = new DataSet();
            POSdataSet.Tables.Add(POSList);
            DataRow row;

            foreach(StarbaseList.Starbase pos in POSes)
            {
                row = POSList.NewRow();
                try
                {
                    row["POSid"] = pos.ItemId;
                } catch(Exception ex)
                {
                    System.Windows.Forms.MessageBox.Show(ex.Message);
                }
                row["POSTypeName"] = EveOnlineApi.Eve.GetTypeName(pos.TypeId).Result.Types.First().TypeName;
                row["POSMoonId"] = pos.MoonId;
                row["POSMoonName"] = Celestials.GetCelestialNameFromID(pos.MoonId);

                EveApiResponse<StarbaseDetails> posDetails = Corp.GetStarbaseDetails(pos.ItemId);
                int _cnt = 0;
                int fuelQuantity = 0;
                foreach(StarbaseDetails.FuelEntry fe in posDetails.Result.Fuel)
                {
                    string fuelType = EveOnlineApi.Eve.GetTypeName(fe.TypeId).Result.Types[0].TypeName;
                    
                    if (!fuelType.Equals("Strontium Clathrates"))
                        fuelQuantity = fe.Quantity;

                    ++_cnt;
                    row["POSFuelid" + _cnt.ToString()] = fe.TypeId;
                    row["POSFuelName" + _cnt.ToString()] = fuelType;
                    row["POSFuelQuantity" + _cnt.ToString()] = fe.Quantity;
                }

                row["POSStatus"] = Convert.ToString(pos.State);
                row["POSOnlineSince"] = posDetails.Result.OnlineTimestamp.ToShortDateString();
                row["POSOfflineDate"] = CalculateOfflineDate(fuelQuantity, EveOnlineApi.Eve.GetTypeName(pos.TypeId).Result.Types.First().TypeName);
                row["POSLocationName"] = Celestials.GetCelestialNameFromID(pos.LocationId);
                
                POSList.Rows.Add(row);
            }

        }

        private string CalculateOfflineDate(int quantity, string towerType)
        {
            DateTime offDate = new DateTime();

            if (towerType.ToLower().Contains("small"))
            {
                double days = quantity / 240;
                offDate = DateTime.Now.AddDays(days);
            }
            else if (towerType.ToLower().Contains("medium"))
            {
                double days = quantity / 480;
                offDate = DateTime.Now.AddDays(days);
            }
            else
            {
                double days = quantity / 960;
                offDate = DateTime.Now.AddDays(days);
            }

            return "Goes offline on: " + offDate.ToShortDateString();
        }

    }
}
