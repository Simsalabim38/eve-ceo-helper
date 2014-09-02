using eZet.EveLib.Modules;
using eZet.EveLib.Modules.Models;
using eZet.EveLib.Modules.Models.Character;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EveCeoHelper.CoreData
{
    class Industry
    {
        public Industry(Corporation corp)
        {
            _corp = corp;
            LoadIndustryJobs();
        }

        private Corporation _corp { get; set; }

        public DataSet IndyJobs { get; set; }

        private DataTable IndyTable { get; set; }

        private void LoadIndustryJobs()
        {
            IndyTable = new DataTable("IndustryJobs");
            EveApiResponse<IndustryJobs> indyJ = null;

            //IndyTable.Columns.Add();
            try
            {
               indyJ = _corp.GetIndustryJobs();
            }catch(Exception ex)
            { }

            SetColumnsInIndyTable();

            DataColumn[] PrimaryKeyColumns = new DataColumn[1];
            PrimaryKeyColumns[0] = IndyTable.Columns["JobID"];
            IndyTable.PrimaryKey = PrimaryKeyColumns;

            IndyJobs = new DataSet();
            IndyJobs.Tables.Add(IndyTable);
            DataRow row;


            if (indyJ != null || indyJ.Result.Jobs.Count != 0)
            {
                foreach (IndustryJobs.NewIndustryJob ijob in indyJ.Result.Jobs)
                {
                    row = IndyTable.NewRow();
                    row["JobID"] = ijob.JobId;
                    row["Status"] = ijob.Status;
                    row["JobRuns"] = ijob.Runs;
                    row["Activity"] = "test1";//EveOnlineApi.Eve.GetTypeName(ijob.ActivityId).Result.Types.First().TypeName;
                    row["Blueprint"] = ijob.BueprintTypeName;
                    row["Facility"] = "test2"; //EveOnlineApi.Eve.GetTypeName(ijob.FacilityId).Result.Types.First().TypeName;
                    row["InstallerName"] = ijob.InstallerName;
                    row["InstallDate"] = ijob.StartDateAsString;
                    row["EndDate"] = ijob.EndDateAsString;
                    IndyTable.Rows.Add(row);
                }
                
            }
        }

        private void SetColumnsInIndyTable()
        {
            IndyTable.Columns.Add("JobID", System.Type.GetType("System.Int64"));
            IndyTable.Columns.Add("Status", System.Type.GetType("System.String"));
            IndyTable.Columns.Add("JobRuns", System.Type.GetType("System.Int64"));
            IndyTable.Columns.Add("Activity", System.Type.GetType("System.String"));
            IndyTable.Columns.Add("Blueprint", System.Type.GetType("System.String"));
            IndyTable.Columns.Add("Facility", System.Type.GetType("System.String"));
            IndyTable.Columns.Add("InstallerName", System.Type.GetType("System.String"));
            IndyTable.Columns.Add("InstallDate", System.Type.GetType("System.String"));
            IndyTable.Columns.Add("EndDate", System.Type.GetType("System.String"));
        }

    }
}
