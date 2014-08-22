using EveCeoHelper.Helper;
using eZet.EveLib.Modules;
using eZet.EveLib.Modules.Models;
using eZet.EveLib.Modules.Models.Account;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace EveCeoHelper
{
    public partial class EveCeoHelper : Form
    {
        public EveCeoHelper()
        {
            InitializeComponent();
        }


        public Corporation currentCorp { get; set; }

        private void Form2_Load(object sender, EventArgs e)
        {
            string key = "";
            string verifCode = "";
            ApiKeyInputForm f = new ApiKeyInputForm();
            f.ShowDialog();
            key = f.Key;
            int keyInt = 0;
            keyInt = Convert.ToInt32(key);
            if (keyInt == 0)
                this.Close();

            verifCode = f.Vcode;
            if(String.IsNullOrEmpty(verifCode))
            {
                MessageBox.Show("Verification code cannot be null or empty, please try again...");
                return;
            }

            ApiKey apiKey = EveOnlineApi.CreateApiKey(keyInt,verifCode);
            if (!apiKey.IsInitialized && apiKey.IsValidKey())
                apiKey.Init();

            // Corporation Key ?
            if(apiKey.KeyType == ApiKeyType.Corporation) 
            {
                CorporationKey cKey = (CorporationKey)apiKey.GetActualKey();
                Corporation corp = cKey.Corporation;
                currentCorp = corp;

                // Retrieve corp details and fill Corp-Details tab with the control
                CorpDetails cDetails = new CorpDetails(corp);
                cDetails.Visible = true;
                cDetails.Location = new Point(10, 10);
                this.tabPage1.Controls.Add(cDetails);
            }
            else
            {
                MessageBox.Show("Currently we support only Corporation api keys, sorry!");
                return;
                // Handle Private API Key
            }

            /*
            DateTime todayDate = DateTime.Now;
            DateTime start = new DateTime(todayDate.Year, todayDate.Month, 1, 0, 0, 1);
            DateTime stop = DateTime.Now;

            TaxContrDatePickerStart.Value = start;
            TaxContrDatePickerStop.Value = stop;

            CorpHelper corpHelper = new CorpHelper(currentCorp);
            DataTable dt = corpHelper.GetCorporationTaxInformation(start, stop);
            // Fill Total tax Label
            TaxContributionTotalText.Text = Decimal.ToOACurrency((decimal)dt.Rows[dt.Rows.Count - 1].ItemArray[1]) + " ISK";
            dt.Rows[dt.Rows.Count - 1].Delete();

            dataGridView1.DataSource = dt;
            dataGridView1.Sort(dataGridView1.Columns[1], ListSortDirection.Descending);*/

            pictureBox1.Load(@"http://image.eveonline.com/Corporation/" + currentCorp.CorporationId + "_128.png");
                //corpHelper.GetCorpImage());

            //corpPOS1.Corp = currentCorp;
            //corpPOS1.CorpPOSLoad();

        }

        private void vScrollBar1_Scroll(object sender, ScrollEventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {

        }

        private void tabPage8_Click(object sender, EventArgs e)
        {

        }

        private void tabPage15_Click(object sender, EventArgs e)
        {
            
        }

        private void tabPage13_Click(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// Update the tax contribution grid by Start and Stop Date from Datepicker
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ReloadTaxData_Click(object sender, EventArgs e)
        {
            CorpHelper corpHelper = new CorpHelper(currentCorp);

            DataTable dt = corpHelper.GetCorporationTaxInformation(TaxContrDatePickerStart.Value, TaxContrDatePickerStop.Value);
            // Fill Total tax Label
            TaxContributionTotalText.Text = dt.Rows[dt.Rows.Count - 1].ItemArray[1].ToString() + " ISK";
            dt.Rows[dt.Rows.Count - 1].Delete();

            dataGridView1.DataSource = dt;
            dataGridView1.Sort(dataGridView1.Columns[1], ListSortDirection.Descending);
        }

        private void tabPage18_Click(object sender, EventArgs e)
        {
            
        }

        private void tabPage19_Click(object sender, EventArgs e)
        {

        }


        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AboutDialog about = new AboutDialog();
            about.ShowDialog();
        }
    }
}
