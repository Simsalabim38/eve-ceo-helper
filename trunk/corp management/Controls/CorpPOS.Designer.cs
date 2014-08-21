namespace EveCeoHelper
{
    partial class CorpPOS
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
            this.POS_treeView = new System.Windows.Forms.TreeView();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.listBoxPOSDetails = new System.Windows.Forms.ListView();
            this.SuspendLayout();
            // 
            // POS_treeView
            // 
            this.POS_treeView.Location = new System.Drawing.Point(16, 41);
            this.POS_treeView.Name = "POS_treeView";
            this.POS_treeView.Size = new System.Drawing.Size(249, 451);
            this.POS_treeView.TabIndex = 0;
            this.POS_treeView.NodeMouseClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.POS_treeView_NodeMouseClick);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(48, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "POS List";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(282, 16);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(64, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "POS Details";
            // 
            // listBoxPOSDetails
            // 
            this.listBoxPOSDetails.Alignment = System.Windows.Forms.ListViewAlignment.Left;
            this.listBoxPOSDetails.FullRowSelect = true;
            this.listBoxPOSDetails.GridLines = true;
            this.listBoxPOSDetails.Location = new System.Drawing.Point(285, 41);
            this.listBoxPOSDetails.Name = "listBoxPOSDetails";
            this.listBoxPOSDetails.Size = new System.Drawing.Size(468, 451);
            this.listBoxPOSDetails.TabIndex = 5;
            this.listBoxPOSDetails.UseCompatibleStateImageBehavior = false;
            this.listBoxPOSDetails.View = System.Windows.Forms.View.SmallIcon;
            // 
            // CorpPOS
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.listBoxPOSDetails);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.POS_treeView);
            this.Name = "CorpPOS";
            this.Size = new System.Drawing.Size(771, 509);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TreeView POS_treeView;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ListView listBoxPOSDetails;
    }
}
