namespace corp_management.Helper
{
    partial class CorpDetails
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.CorpName_Label = new System.Windows.Forms.Label();
            this.text_corp_name = new System.Windows.Forms.Label();
            this.AlianceName_Label = new System.Windows.Forms.Label();
            this.text_alliance_name = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // CorpName_Label
            // 
            this.CorpName_Label.AutoSize = true;
            this.CorpName_Label.Location = new System.Drawing.Point(13, 17);
            this.CorpName_Label.Name = "CorpName_Label";
            this.CorpName_Label.Size = new System.Drawing.Size(95, 13);
            this.CorpName_Label.TabIndex = 0;
            this.CorpName_Label.Text = "Corporation Name:";
            // 
            // text_corp_name
            // 
            this.text_corp_name.AutoSize = true;
            this.text_corp_name.Location = new System.Drawing.Point(114, 17);
            this.text_corp_name.Name = "text_corp_name";
            this.text_corp_name.Size = new System.Drawing.Size(35, 13);
            this.text_corp_name.TabIndex = 1;
            this.text_corp_name.Text = "label0";
            // 
            // AlianceName_Label
            // 
            this.AlianceName_Label.AutoSize = true;
            this.AlianceName_Label.Location = new System.Drawing.Point(13, 34);
            this.AlianceName_Label.Name = "AlianceName_Label";
            this.AlianceName_Label.Size = new System.Drawing.Size(47, 13);
            this.AlianceName_Label.TabIndex = 2;
            this.AlianceName_Label.Text = "Alliance:";
            // 
            // text_alliance_name
            // 
            this.text_alliance_name.AutoSize = true;
            this.text_alliance_name.Location = new System.Drawing.Point(114, 34);
            this.text_alliance_name.Name = "text_alliance_name";
            this.text_alliance_name.Size = new System.Drawing.Size(35, 13);
            this.text_alliance_name.TabIndex = 3;
            this.text_alliance_name.Text = "label1";
            // 
            // CorpDetails
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.text_alliance_name);
            this.Controls.Add(this.AlianceName_Label);
            this.Controls.Add(this.text_corp_name);
            this.Controls.Add(this.CorpName_Label);
            this.Name = "CorpDetails";
            this.Size = new System.Drawing.Size(676, 477);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label CorpName_Label;
        private System.Windows.Forms.Label text_corp_name;
        private System.Windows.Forms.Label AlianceName_Label;
        private System.Windows.Forms.Label text_alliance_name;
    }
}
