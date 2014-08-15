namespace corp_management.Controls
{
    partial class CorpWalletTransactions
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
            this.corpWalletDataGridView = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.corpWalletDataGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // corpWalletDataGridView
            // 
            this.corpWalletDataGridView.AllowUserToAddRows = false;
            this.corpWalletDataGridView.AllowUserToDeleteRows = false;
            this.corpWalletDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.corpWalletDataGridView.Location = new System.Drawing.Point(13, 55);
            this.corpWalletDataGridView.Name = "corpWalletDataGridView";
            this.corpWalletDataGridView.Size = new System.Drawing.Size(655, 352);
            this.corpWalletDataGridView.TabIndex = 0;
            // 
            // CorpWalletTransactions
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.corpWalletDataGridView);
            this.Name = "CorpWalletTransactions";
            this.Size = new System.Drawing.Size(685, 422);
            ((System.ComponentModel.ISupportInitialize)(this.corpWalletDataGridView)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView corpWalletDataGridView;
    }
}
