namespace TileBeautify {
    partial class FormReplaceIcon {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnOK = new System.Windows.Forms.Button();
            this.lblSelectICOTopic = new System.Windows.Forms.Label();
            this.btnBrowse = new System.Windows.Forms.Button();
            this.pnlICO = new System.Windows.Forms.Panel();
            this.txtPath = new System.Windows.Forms.TextBox();
            this.lblFindICOTopic = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(244, 357);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(61, 26);
            this.btnCancel.TabIndex = 13;
            this.btnCancel.Text = "取消";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(161, 357);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(61, 26);
            this.btnOK.TabIndex = 12;
            this.btnOK.Text = "确定";
            this.btnOK.UseVisualStyleBackColor = true;
            // 
            // lblSelectICOTopic
            // 
            this.lblSelectICOTopic.AutoSize = true;
            this.lblSelectICOTopic.Location = new System.Drawing.Point(14, 82);
            this.lblSelectICOTopic.Name = "lblSelectICOTopic";
            this.lblSelectICOTopic.Size = new System.Drawing.Size(155, 12);
            this.lblSelectICOTopic.TabIndex = 11;
            this.lblSelectICOTopic.Text = "从以下列表中选择一个图标:";
            // 
            // btnBrowse
            // 
            this.btnBrowse.Location = new System.Drawing.Point(244, 41);
            this.btnBrowse.Name = "btnBrowse";
            this.btnBrowse.Size = new System.Drawing.Size(61, 26);
            this.btnBrowse.TabIndex = 10;
            this.btnBrowse.Text = "浏览";
            this.btnBrowse.UseVisualStyleBackColor = true;
            // 
            // pnlICO
            // 
            this.pnlICO.BackColor = System.Drawing.Color.White;
            this.pnlICO.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlICO.Location = new System.Drawing.Point(16, 108);
            this.pnlICO.Name = "pnlICO";
            this.pnlICO.Size = new System.Drawing.Size(289, 218);
            this.pnlICO.TabIndex = 9;
            // 
            // txtPath
            // 
            this.txtPath.BackColor = System.Drawing.Color.White;
            this.txtPath.Location = new System.Drawing.Point(16, 44);
            this.txtPath.Name = "txtPath";
            this.txtPath.ReadOnly = true;
            this.txtPath.Size = new System.Drawing.Size(222, 21);
            this.txtPath.TabIndex = 8;
            // 
            // lblFindICOTopic
            // 
            this.lblFindICOTopic.AutoSize = true;
            this.lblFindICOTopic.Location = new System.Drawing.Point(14, 16);
            this.lblFindICOTopic.Name = "lblFindICOTopic";
            this.lblFindICOTopic.Size = new System.Drawing.Size(119, 12);
            this.lblFindICOTopic.TabIndex = 7;
            this.lblFindICOTopic.Text = "查找此文件中的图标:";
            // 
            // FormReplaceIcon
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
            this.ClientSize = new System.Drawing.Size(318, 399);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.lblSelectICOTopic);
            this.Controls.Add(this.btnBrowse);
            this.Controls.Add(this.pnlICO);
            this.Controls.Add(this.txtPath);
            this.Controls.Add(this.lblFindICOTopic);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormReplaceIcon";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "更改图标";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Label lblSelectICOTopic;
        private System.Windows.Forms.Button btnBrowse;
        private System.Windows.Forms.Panel pnlICO;
        private System.Windows.Forms.TextBox txtPath;
        private System.Windows.Forms.Label lblFindICOTopic;
    }
}