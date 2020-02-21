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
            this.lblSelectIconTopic = new System.Windows.Forms.Label();
            this.btnBrowse = new System.Windows.Forms.Button();
            this.txtPath = new System.Windows.Forms.TextBox();
            this.lblFindIconTopic = new System.Windows.Forms.Label();
            this.flpIcon = new System.Windows.Forms.FlowLayoutPanel();
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
            // lblSelectIconTopic
            // 
            this.lblSelectIconTopic.AutoSize = true;
            this.lblSelectIconTopic.Location = new System.Drawing.Point(14, 82);
            this.lblSelectIconTopic.Name = "lblSelectIconTopic";
            this.lblSelectIconTopic.Size = new System.Drawing.Size(155, 12);
            this.lblSelectIconTopic.TabIndex = 11;
            this.lblSelectIconTopic.Text = "从以下列表中选择一个图标:";
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
            // txtPath
            // 
            this.txtPath.BackColor = System.Drawing.Color.White;
            this.txtPath.Location = new System.Drawing.Point(16, 44);
            this.txtPath.Name = "txtPath";
            this.txtPath.ReadOnly = true;
            this.txtPath.Size = new System.Drawing.Size(222, 21);
            this.txtPath.TabIndex = 8;
            // 
            // lblFindIconTopic
            // 
            this.lblFindIconTopic.AutoSize = true;
            this.lblFindIconTopic.Location = new System.Drawing.Point(14, 16);
            this.lblFindIconTopic.Name = "lblFindIconTopic";
            this.lblFindIconTopic.Size = new System.Drawing.Size(119, 12);
            this.lblFindIconTopic.TabIndex = 7;
            this.lblFindIconTopic.Text = "查找此文件中的图标:";
            // 
            // flpIcon
            // 
            this.flpIcon.AutoScroll = true;
            this.flpIcon.BackColor = System.Drawing.Color.White;
            this.flpIcon.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.flpIcon.Location = new System.Drawing.Point(16, 108);
            this.flpIcon.Name = "flpIcon";
            this.flpIcon.Size = new System.Drawing.Size(289, 218);
            this.flpIcon.TabIndex = 0;
            // 
            // FormReplaceIcon
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
            this.ClientSize = new System.Drawing.Size(318, 399);
            this.Controls.Add(this.flpIcon);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.lblSelectIconTopic);
            this.Controls.Add(this.btnBrowse);
            this.Controls.Add(this.txtPath);
            this.Controls.Add(this.lblFindIconTopic);
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
        private System.Windows.Forms.Label lblSelectIconTopic;
        private System.Windows.Forms.Button btnBrowse;
        private System.Windows.Forms.TextBox txtPath;
        private System.Windows.Forms.Label lblFindIconTopic;
        private System.Windows.Forms.FlowLayoutPanel flpIcon;
    }
}