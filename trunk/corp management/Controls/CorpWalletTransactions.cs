using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using corp_management.Helper;
using eZet.EveLib.Modules;

namespace corp_management.Controls
{
    public partial class CorpWalletTransactions : UserControl
    {
        public CorpWalletTransactions()
        {
            InitializeComponent();
        }

        public Corporation Corp { get; set; }

        public void LoadDataGrid()
        {
            CorpHelper helper = new CorpHelper(Corp);
            DateTime startDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1, 0, 0, 1);
            DataTable dt = helper.GetMasterWalletTransactions(startDate, DateTime.Now, 0);
            this.corpWalletDataGridView.DataSource = dt;
        }
    }
}
