using eZet.EveLib.Modules;
using eZet.EveLib.Modules.Models;
using eZet.EveLib.Modules.Models.Character;
using eZet.EveLib.Modules.Models.Corporation;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EveCeoHelper.Helper
{
    class CorpHelper
    {

        private eZet.EveLib.Modules.Corporation _corp { get; set; }

        /// <summary>
        /// Constructor
        /// </summary>
        public CorpHelper()
        {

        }

        /// <summary>
        /// Constructor with Corp details
        /// </summary>
        /// <param name="corp"></param>
        public CorpHelper(eZet.EveLib.Modules.Corporation corp)
        {
            if (corp != null)
                _corp = corp;
            else
                throw new ArgumentException("corp object cannot be null or empty");
        }

        /// <summary>
        /// Retrieves tax information from a Corporation
        /// </summary>
        /// <param name="startDate">Start date</param>
        /// <param name="stopDate">End date</param>
        /// <returns>Dictionary with User name and summarized tax amount</returns>
        public DataTable GetCorporationTaxInformation(DateTime startDate, DateTime stopDate)
        {
            DataTable dt = new DataTable("CorpTaxData");
            dt.Columns.Add("Name"); // Undefined column
            dt.Columns.Add("Isk", typeof(decimal)); // Specific type column

            Dictionary<string,decimal> _taxData = new Dictionary<string,decimal>();

            EveApiResponse<WalletJournal> wallet = _corp.GetWalletJournal(1000, 5000, 0);
            List<WalletJournal.JournalEntry> filteredJournals = new List<WalletJournal.JournalEntry>();
            List<int> _validRefTypes = new List<int>() { 17, 85, 33, 34, 93, 94, 96, 97 };
            for (int i = 0; i < wallet.Result.Journal.Count(); i++)
            {
                WalletJournal.JournalEntry jEntry = wallet.Result.Journal[i];

                if(!_validRefTypes.Contains(jEntry.RefTypeId))
                    continue;

                if (jEntry.Date < startDate)
                    continue;

                if (jEntry.Date > stopDate)
                    continue;

                filteredJournals.Add(wallet.Result.Journal[i]);

            }

            var taxTotal = filteredJournals.Select(x => x.Amount).Sum();
            var taxUser = filteredJournals.Select(x => x.ParticipantName).Distinct();

            // Loop through the list of users and fetch the summary of all Journal entries for each user
            foreach(string user in taxUser)
            {
                _taxData.Add(user, 
                                (filteredJournals.Where(
                                    x => x.ParticipantName == user &&
                                    x.Amount > 0)
                                ).Select(x => x.Amount).Sum()
                            );
                dt.Rows.Add(user, (filteredJournals.Where(
                                    x => x.ParticipantName == user &&
                                    x.Amount > 0)
                                ).Select(x => x.Amount).Sum());  
            }

            // Add "Total sum" to the Dictionary
            _taxData.Add("Total", taxTotal);
            dt.Rows.Add("Total", taxTotal);
            
            return dt;
        }

        public DataTable GetMasterWalletTransactions(DateTime startDate, DateTime stopDate, int pageNumber)
        {
            DataTable dt = new DataTable("MasterWalletTrans");
            int _pNr = 0;
            if (pageNumber == 0)
                pageNumber = _pNr;

            EveApiResponse<WalletTransactions> wallet = new EveApiResponse<WalletTransactions>();
            wallet = _corp.GetWalletTransactions(1000, 5000, _pNr);



            return dt;
        }

        public string GetCorpImage()
        {
            eZet.EveLib.Modules.Image img = new eZet.EveLib.Modules.Image();
            string _file = String.Empty;

            if (!File.Exists(Directory.GetCurrentDirectory() + @"\" + _corp.CorporationId.ToString() + "_128.png"))
            {
                try
                {
                    string _returnValue = img.GetCorporationLogo(_corp.CorporationId, eZet.EveLib.Modules.Image.CorporationLogoSize.X128, Directory.GetCurrentDirectory());
                    _file = _returnValue;
                }
                catch (Exception ex)
                {
                    return _file;
                }
            }
            else
            {
                _file = Directory.GetCurrentDirectory() + @"\" + _corp.CorporationId.ToString() + "_128.png";
            }
            return _file;
        }

        public string GetCorpFacilityNameByID(long FacilityID)
        {
            string fName = String.Empty;
            EveApiResponse<Facilities> response = _corp.GetFacilities();
            EveOnlineRowCollection<eZet.EveLib.Modules.Models.Corporation.Facilities.Facility> facilities = response.Result.FacilityEntries;
            foreach(eZet.EveLib.Modules.Models.Corporation.Facilities.Facility f in facilities)
            {
                if(f.FacilityId.Equals(FacilityID))
                {
                    fName = EveOnlineApi.Eve.GetTypeName(f.TypeId).Result.Types.First().TypeName;
                    break;
                }
            }
            return fName;
        }
    }
}
