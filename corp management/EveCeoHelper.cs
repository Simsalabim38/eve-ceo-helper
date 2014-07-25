﻿using corp_management.Helper;
using eZet.EveLib.Modules;
using eZet.EveLib.Modules.Models;
using eZet.EveLib.Modules.Models.Account;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace corp_management
{
    public partial class EveCeoHelper : Form
    {
        public EveCeoHelper()
        {
            InitializeComponent();
        }

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
            ApiKey apiKey = EveOnlineApi.CreateApiKey(keyInt,verifCode);
            if (!apiKey.IsInitialized && apiKey.IsValidKey())
                apiKey.Init();

            // Corporation Key ?
            if(apiKey.KeyType == ApiKeyType.Corporation) 
            {
                CorporationKey cKey = (CorporationKey)apiKey.GetActualKey();
                Corporation corp = cKey.Corporation;

                // Retrieve corp details and fill Corp-Details tab with the control
                CorpDetails cDetails = new CorpDetails(corp);
                cDetails.Visible = true;
                cDetails.Location = new Point(10, 10);
                this.tabPage1.Controls.Add(cDetails);
            }


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
    }
}