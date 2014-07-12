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
    public partial class ApiKeyInputForm : Form
    {
        public string Key { get; set; }
        public string Vcode { get; set; }
        public ApiKeyInputForm()
        {
            InitializeComponent();
            Microsoft.Win32.RegistryKey key;
            key = Microsoft.Win32.Registry.CurrentUser.CreateSubKey("EveCeoHelper");
            if (key.GetValue("save") != null && key.GetValue("save").ToString() == "yes")
            {
                textboxKey.Text = key.GetValue("key").ToString();
                textBoxVerifCode.Text = key.GetValue("verif").ToString();
                checkBox1.Checked = true;
            }
            else
            {
                checkBox1.Checked = false;
            }
        }

        public void button1_Click(object sender, EventArgs e)
        {
            Key = textboxKey.Text.Trim();
            Vcode = textBoxVerifCode.Text.Trim();
            Microsoft.Win32.RegistryKey key;
            key = Microsoft.Win32.Registry.CurrentUser.CreateSubKey("EveCeoHelper");
            if (checkBox1.Checked)
            {              
                key.SetValue("key", textboxKey.Text.Trim());
                key.SetValue("verif", textBoxVerifCode.Text.Trim());
                key.SetValue("save", "yes");
                key.Close();
            }
            else
            {                
                key.SetValue("key", "");
                key.SetValue("verif", "");
                key.SetValue("save", "no");
                key.Close();
            }
            this.Close();
        }

    }
}
