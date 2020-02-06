namespace TileBeautify {
    partial class FormEditPicture {
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
            this.hscMovePic = new System.Windows.Forms.HScrollBar();
            this.pnlBack = new System.Windows.Forms.Panel();
            this.pnlTile = new System.Windows.Forms.Panel();
            this.picEdited = new System.Windows.Forms.PictureBox();
            this.picImage = new System.Windows.Forms.PictureBox();
            this.lblZoomScale = new System.Windows.Forms.Label();
            this.lblZoomFactor = new System.Windows.Forms.Label();
            this.vscMovePic = new System.Windows.Forms.VScrollBar();
            this.trbZoom = new System.Windows.Forms.TrackBar();
            this.btnEditPic = new System.Windows.Forms.Button();
            this.btnSelectPic = new System.Windows.Forms.Button();
            this.btnMask = new System.Windows.Forms.Button();
            this.pnlCommand = new System.Windows.Forms.Panel();
            this.pnlBack.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picEdited)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picImage)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trbZoom)).BeginInit();
            this.pnlCommand.SuspendLayout();
            this.SuspendLayout();
            // 
            // hscMovePic
            // 
            this.hscMovePic.Location = new System.Drawing.Point(448, 186);
            this.hscMovePic.Name = "hscMovePic";
            this.hscMovePic.Size = new System.Drawing.Size(212, 37);
            this.hscMovePic.TabIndex = 71;
            this.hscMovePic.Visible = false;
            // 
            // pnlBack
            // 
            this.pnlBack.BackColor = System.Drawing.Color.PeachPuff;
            this.pnlBack.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlBack.Controls.Add(this.pnlTile);
            this.pnlBack.Controls.Add(this.picEdited);
            this.pnlBack.Controls.Add(this.picImage);
            this.pnlBack.Location = new System.Drawing.Point(12, 12);
            this.pnlBack.Name = "pnlBack";
            this.pnlBack.Size = new System.Drawing.Size(421, 323);
            this.pnlBack.TabIndex = 73;
            // 
            // pnlTile
            // 
            this.pnlTile.BackColor = System.Drawing.Color.BlanchedAlmond;
            this.pnlTile.Location = new System.Drawing.Point(207, 59);
            this.pnlTile.Name = "pnlTile";
            this.pnlTile.Size = new System.Drawing.Size(119, 151);
            this.pnlTile.TabIndex = 52;
            // 
            // picEdited
            // 
            this.picEdited.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.picEdited.Cursor = System.Windows.Forms.Cursors.NoMove2D;
            this.picEdited.Location = new System.Drawing.Point(54, 59);
            this.picEdited.Name = "picEdited";
            this.picEdited.Size = new System.Drawing.Size(124, 151);
            this.picEdited.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picEdited.TabIndex = 47;
            this.picEdited.TabStop = false;
            this.picEdited.Visible = false;
            // 
            // picImage
            // 
            this.picImage.BackColor = System.Drawing.Color.LightBlue;
            this.picImage.Location = new System.Drawing.Point(24, 19);
            this.picImage.Name = "picImage";
            this.picImage.Size = new System.Drawing.Size(355, 279);
            this.picImage.TabIndex = 51;
            this.picImage.TabStop = false;
            // 
            // lblZoomScale
            // 
            this.lblZoomScale.AutoSize = true;
            this.lblZoomScale.BackColor = System.Drawing.Color.Transparent;
            this.lblZoomScale.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblZoomScale.Location = new System.Drawing.Point(0, 20);
            this.lblZoomScale.Name = "lblZoomScale";
            this.lblZoomScale.Size = new System.Drawing.Size(84, 17);
            this.lblZoomScale.TabIndex = 5;
            this.lblZoomScale.Text = "缩放比例:32%";
            // 
            // lblZoomFactor
            // 
            this.lblZoomFactor.AutoSize = true;
            this.lblZoomFactor.BackColor = System.Drawing.Color.Transparent;
            this.lblZoomFactor.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblZoomFactor.Location = new System.Drawing.Point(0, 3);
            this.lblZoomFactor.Name = "lblZoomFactor";
            this.lblZoomFactor.Size = new System.Drawing.Size(83, 17);
            this.lblZoomFactor.TabIndex = 3;
            this.lblZoomFactor.Text = "缩放系数:1.05";
            // 
            // vscMovePic
            // 
            this.vscMovePic.Location = new System.Drawing.Point(525, 108);
            this.vscMovePic.Name = "vscMovePic";
            this.vscMovePic.Size = new System.Drawing.Size(33, 171);
            this.vscMovePic.TabIndex = 72;
            this.vscMovePic.Visible = false;
            // 
            // trbZoom
            // 
            this.trbZoom.AutoSize = false;
            this.trbZoom.BackColor = System.Drawing.SystemColors.Control;
            this.trbZoom.Enabled = false;
            this.trbZoom.Location = new System.Drawing.Point(87, 3);
            this.trbZoom.Maximum = 25;
            this.trbZoom.Minimum = 1;
            this.trbZoom.Name = "trbZoom";
            this.trbZoom.Size = new System.Drawing.Size(200, 32);
            this.trbZoom.TabIndex = 2;
            this.trbZoom.Value = 5;
            // 
            // btnEditPic
            // 
            this.btnEditPic.AutoSize = true;
            this.btnEditPic.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnEditPic.Enabled = false;
            this.btnEditPic.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnEditPic.Location = new System.Drawing.Point(296, 3);
            this.btnEditPic.Name = "btnEditPic";
            this.btnEditPic.Size = new System.Drawing.Size(52, 31);
            this.btnEditPic.TabIndex = 61;
            this.btnEditPic.Text = "编辑";
            this.btnEditPic.UseVisualStyleBackColor = true;
            // 
            // btnSelectPic
            // 
            this.btnSelectPic.AutoSize = true;
            this.btnSelectPic.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnSelectPic.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnSelectPic.Location = new System.Drawing.Point(441, 3);
            this.btnSelectPic.Name = "btnSelectPic";
            this.btnSelectPic.Size = new System.Drawing.Size(84, 31);
            this.btnSelectPic.TabIndex = 1;
            this.btnSelectPic.Text = "选取图片";
            this.btnSelectPic.UseVisualStyleBackColor = true;
            // 
            // btnMask
            // 
            this.btnMask.AutoSize = true;
            this.btnMask.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnMask.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnMask.Location = new System.Drawing.Point(369, 3);
            this.btnMask.Name = "btnMask";
            this.btnMask.Size = new System.Drawing.Size(52, 31);
            this.btnMask.TabIndex = 60;
            this.btnMask.Text = "蒙版";
            this.btnMask.UseVisualStyleBackColor = true;
            // 
            // pnlCommand
            // 
            this.pnlCommand.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.pnlCommand.BackColor = System.Drawing.SystemColors.Control;
            this.pnlCommand.Controls.Add(this.trbZoom);
            this.pnlCommand.Controls.Add(this.lblZoomScale);
            this.pnlCommand.Controls.Add(this.lblZoomFactor);
            this.pnlCommand.Controls.Add(this.btnEditPic);
            this.pnlCommand.Controls.Add(this.btnSelectPic);
            this.pnlCommand.Controls.Add(this.btnMask);
            this.pnlCommand.Location = new System.Drawing.Point(12, 377);
            this.pnlCommand.Name = "pnlCommand";
            this.pnlCommand.Size = new System.Drawing.Size(528, 38);
            this.pnlCommand.TabIndex = 74;
            // 
            // FormEditPicture
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(669, 476);
            this.Controls.Add(this.hscMovePic);
            this.Controls.Add(this.pnlBack);
            this.Controls.Add(this.vscMovePic);
            this.Controls.Add(this.pnlCommand);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "FormEditPicture";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "TileBeautify-编辑图片";
            this.pnlBack.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.picEdited)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picImage)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trbZoom)).EndInit();
            this.pnlCommand.ResumeLayout(false);
            this.pnlCommand.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.HScrollBar hscMovePic;
        private System.Windows.Forms.Panel pnlBack;
        private System.Windows.Forms.Panel pnlTile;
        private System.Windows.Forms.PictureBox picEdited;
        private System.Windows.Forms.PictureBox picImage;
        private System.Windows.Forms.Label lblZoomScale;
        private System.Windows.Forms.Label lblZoomFactor;
        private System.Windows.Forms.VScrollBar vscMovePic;
        private System.Windows.Forms.TrackBar trbZoom;
        private System.Windows.Forms.Button btnEditPic;
        private System.Windows.Forms.Button btnSelectPic;
        private System.Windows.Forms.Button btnMask;
        private System.Windows.Forms.Panel pnlCommand;
    }
}