using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;
using System.Diagnostics;


namespace EveCeoHelper.CoreData
{
    class Celestials
    {
        public Celestials()
        {

        }

        public static string GetCelestialNameFromID(long id)
        {
            
            const string filename = @"StaticData\mapDenormalize.sql";
            string sql = "select itemname from celestial where itemid = '" + Convert.ToString(id) + "';";
            var conn = new SQLiteConnection("Data Source=" + filename + ";Version=3;");
            string _returnValue = String.Empty;
            try
            {
                conn.Open();
                var da = new SQLiteCommand(sql, conn);
                _returnValue = (string)da.ExecuteScalar();
                
            } catch (Exception ex)
            {
                throw new Exception("Error while retrieving celestial name from database: " + ex.Message);
            }
            
            return _returnValue;
        }
    }
}
