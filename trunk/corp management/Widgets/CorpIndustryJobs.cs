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
using EveCeoHelper.CoreData;

namespace EveCeoHelper.Widgets
{
    public partial class CorpIndustryJobs : UserControl
    {
        public CorpIndustryJobs()
        {
            InitializeComponent();
        }

        public Corporation Corp { get; set; }

        /// <summary>
        /// Loads the IndustryJobs into the DataSet
        /// </summary>
        public void LoadIndyJobs()
        {
            Industry indy = new Industry(Corp);
            dataGridView1.DataSource = indy.IndyJobs.Tables[0];
        }
    }
}
