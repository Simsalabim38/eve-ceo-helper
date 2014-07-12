using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using eZet.EveLib.Modules;
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
            if (keyInt == null || keyInt == 0)
                this.Close();
            verifCode = f.Vcode;
            ApiKey apiKey = EveOnlineApi.CreateApiKey(keyInt,verifCode);
            if (!apiKey.IsInitialized && apiKey.IsValidKey())
                apiKey.Init();
        }

        private void vScrollBar1_Scroll(object sender, ScrollEventArgs e)
        {

        }
    }
}
