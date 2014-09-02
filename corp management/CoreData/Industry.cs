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
                    row["Status"] = GetActivityStatusByID(int.Parse(ijob.Status));
                    row["JobRuns"] = ijob.Runs;
                    row["Activity"] = GetActivityNameByID(ijob.ActivityId);
                    row["Blueprint"] = ijob.BueprintTypeName;
                    row["Facility"] = (new Helper.CorpHelper(_corp).GetCorpFacilityNameByID(ijob.FacilityId));
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

        private string GetActivityNameByID(int ActivityID)
        {
            Dictionary<int, string> ActivityMapping = new Dictionary<int, string>();
            ActivityMapping.Add(0, "None");
            ActivityMapping.Add(1, "Manufactoring");
            ActivityMapping.Add(2, "Researching");
            ActivityMapping.Add(3, "Time efficiency");
            ActivityMapping.Add(4, "Materials efficency");
            ActivityMapping.Add(5, "Copying");
            ActivityMapping.Add(6, "Duplicating");
            ActivityMapping.Add(7, "Reverse Engineering");
            ActivityMapping.Add(8, "Invention");

            return ActivityMapping[ActivityID];
        }

        private string GetActivityStatusByID(int ID)
        {
            Dictionary<int, string> ActivityStatus = new Dictionary<int, string>();
            ActivityStatus.Add(1, "Active");
            ActivityStatus.Add(2, "Paused");
            ActivityStatus.Add(3, "Ready");
            ActivityStatus.Add(102, "Cancelled");
            ActivityStatus.Add(103, "Reverted");
            ActivityStatus.Add(104, "Delivered");
            ActivityStatus.Add(105, "Failed");

            return ActivityStatus[ID];
        }

    }
}
