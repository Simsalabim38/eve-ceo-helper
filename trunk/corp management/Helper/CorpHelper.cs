using eZet.EveLib.Modules.Models;
using eZet.EveLib.Modules.Models.Character;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace corp_management.Helper
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
        public Dictionary<string,decimal> GetCorporationTaxInformation(DateTime startDate, DateTime stopDate)
        {
            Dictionary<string,decimal> _taxData = new Dictionary<string,decimal>();

            EveApiResponse<WalletJournal> wallet = _corp.GetWalletJournal(1000, 5000, 0);
            List<WalletJournal.JournalEntry> filteredJournals = new List<WalletJournal.JournalEntry>();
            List<int> _validRefTypes = new List<int>() { 17, 85, 33, 34, 93, 94, 96, 97 };
            for (int i = 0; i < wallet.Result.Journal.Count(); i++)
            {
                WalletJournal.JournalEntry jEntry = wallet.Result.Journal[i];

                if(!_validRefTypes.Contains(jEntry.RefTypeId))
                    continue;

                //if (jEntry.RefTypeId != 85 || jEntry.RefTypeId != 17)
                //    continue;

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
            }

            // Add "Total sum" to the Dictionary
            _taxData.Add("Total", taxTotal);

            return _taxData;
        }
    }
}
