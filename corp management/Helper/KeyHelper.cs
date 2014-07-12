using eZet.EveLib.Modules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace corp_management.Helper
{
    class EveApiKeyHelper
    {
        /// <summary>
        /// Verifies if a passed in Key is of type Corporation
        /// </summary>
        /// <param name="keyID">EVE KeyID</param>
        /// <param name="vCode">EVE verification code</param>
        /// <returns>true or false</returns>
        public static bool IsCorpKey(string keyID, string vCode)
        {
            long _keyID = 0;

            if (string.IsNullOrEmpty(keyID))
                throw new ArgumentException("KeyID cannot be empty or null");

            if (string.IsNullOrEmpty(vCode))
                throw new ArgumentException("vCode cannot be empty or null");

            if(!long.TryParse(keyID, out _keyID))
                throw new ArgumentException("invalid keyID");

            var key = new ApiKey(_keyID, vCode);

            if (key.KeyType == ApiKeyType.Corporation)
                return true;
            else
                return false;

        }
    }
}
