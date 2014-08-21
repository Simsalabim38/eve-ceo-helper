using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EveCeoHelper.CoreData
{
    class Celestials
    {
        public Celestials()
        {

        }

        public static string GetCelestialNameFromID(long id)
        {
            string csvFile = @".\StaticData\mapDenormalize.csv";
            string _id = id.ToString();
            char csvSeperator = ',';
            string resultLine = String.Empty;

            foreach (string line in File.ReadLines(csvFile))
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

            return resultLine.Split(',')[11].Replace("\"", "");
        }
    }
}
