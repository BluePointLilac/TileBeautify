using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using TileBeautify.Properties;

namespace TileBeautify {
    public partial class FormEditPicture : Form {
        public FormEditPicture() {
            InitializeComponent();
            this.Icon = Icon.ExtractAssociatedIcon(Application.ExecutablePath);
            ResizeFont.KeepFontSize(pnlCommand);
            ResizeLocation();
            AddPicTile();
            BindEvent();
        }

        //关联控件事件
        private void BindEvent() {
            btnSelectPic.Click += BtnSelectPic_Click;
            btnMask.Click += BtnMask_Click;
            btnEditPic.Click += BtnEditPic_Click;
            hscMovePic.Scroll += ScrollBarScroll;
            vscMovePic.Scroll += ScrollBarScroll;
            picEdited.MouseMove += PicEdited_MouseMove;
            picEdited.MouseWheel += PicEdited_MouseWheel;

            picEdited.MouseEnter += (sender, e) => {
                if (!picEdited.Focused) picEdited.Focus();
            };
            picEdited.MouseDown += (sender, e) => {
                pointDown = e.Location;
                isMove = true;
            };
            picEdited.MouseUp += (sender,e)=> isMove = false;

            //调节缩放系数
            trbZoom.Scroll += (sender, e) => {
                zoom = 1.0 + trbZoom.Value / 100.0;
                lblZoomFactor.Text = "缩放系数:" + zoom;
            };
        }

        //调整控件大小、位置
        private void ResizeLocation() {
            pnlBack.Location = new Point(20, 20);
            pnlBack.Width = Rows * Gtg + Gap + 2;
            pnlBack.Height = pnlBack.Width;
            pnlBack.BackColor = MyBackColor;
            picImage.Parent = pnlBack;
            picImage.Location = new Point(Gap, Gap);
            picImage.Width = Rows * Gtg - Gap;
            picImage.Height = picImage.Width;
            picImage.BackColor = MyBackColor;
            pnlTile.Parent = picImage;
            pnlTile.Location = new Point(0, 0);
            pnlTile.Width = Rows * Gtg - Gap;
            pnlTile.Height = pnlTile.Width;
            pnlTile.BackColor = Color.Transparent;
            picEdited.Parent = picImage;
            picEdited.SendToBack();
            hscMovePic.Left = pnlBack.Left;
            hscMovePic.Top = pnlBack.Bottom;
            hscMovePic.Width = pnlBack.Width;
            hscMovePic.Height = 20;
            vscMovePic.Left = pnlBack.Right;
            vscMovePic.Top = pnlBack.Top;
            vscMovePic.Height = pnlBack.Height;
            vscMovePic.Width = 20;

            //添加分隔线
            Label label = new Label {
                Parent = this,
                BorderStyle = BorderStyle.Fixed3D,
                Width = vscMovePic.Right - 6,
                Height = 2,
                Left = 4,
                Top = hscMovePic.Bottom
            };
            pnlCommand.Left = pnlBack.Left;
            pnlCommand.Top = label.Bottom + 4;
        }

        //添加小块、中块
        private void AddPicTile() {
            //添加小块
            for (int i = 0; i < Rows; i++) {
                for (int j = 0; j < Rows; j++) {
                    var p = new TilePic {
                        Parent = pnlTile,
                        Left = Gtg * i,
                        Top = Gtg * j,
                        Style = TileMode.Small,
                        BackColor = UnSelColor
                    };
                    p.DoubleClick += SendPicture;
                }
            }

            //添加中块
            for (int k = 0; k < Rows - 1; k++) {
                for (int l = 0; l < Rows - 1; l++) {
                    var p = new TilePic {
                        Parent = pnlTile,
                        Left = Gtg / 2 + Gtg * k,
                        Top = Gtg / 2 + Gtg * l,
                        SizeMode = PictureBoxSizeMode.StretchImage,
                        Style = TileMode.Medium,
                        BackColor = Color.Transparent
                    };
                    p.DoubleClick += SendPicture;
                }
            }
        }

        //双击选中的磁贴（保存其下面的图片），并启动编辑窗口
        private void SendPicture(object sender, EventArgs e) {
            var p = (TilePic)sender;
            if (p.BorderStyle == BorderStyle.FixedSingle) {
                var frm = new FormEditConfig {
                    Icon = this.Icon
                };
                if (myBitmap != null) {
                    double num = (double)myBitmap.Width / (double)picEdited.Width;
                    int pX = Convert.ToInt32(num * (double)(p.Left - picEdited.Left));
                    int pY = Convert.ToInt32(num * (double)(p.Top - picEdited.Top));
                    int num2 = Convert.ToInt32(num * (double)(p.Width - 1));
                    Bitmap part = PartImage.GetPart(myBitmap, pX, pY, num2, num2);
                    frm.picEditedView.Image = part;
                }
                pnlTile.Visible = false;
                frm.FormClosed += delegate (object a, FormClosedEventArgs b) {
                    pnlTile.Visible = true;
                };
                frm.ShowDialog();
            }
        }

        //移动图片
        private bool isMove = false;//是否为移动状态
        private Point pointDown = default;//鼠标按下时的坐标
        private Point pointMove = default;//鼠标移动时的坐标
        private void PicEdited_MouseMove(object sender, MouseEventArgs e) {
            if (isMove) {
                pointMove = e.Location;
                int nx = picEdited.Location.X;
                int ny = picEdited.Location.Y;
                nx += pointMove.X - pointDown.X;
                ny += pointMove.Y - pointDown.Y;

                //限制移动范围
                if (nx > 0) nx = 0;
                if (ny > 0) ny = 0;
                if (nx + picEdited.Width < ML) nx = ML - picEdited.Width;
                if (ny + picEdited.Height < ML) ny = ML - picEdited.Height;

                picEdited.Location = new Point(nx, ny);
                UpdateScroll();
            }
        }

        //缩放图片
        private double zoom = 1.05;//鼠标滚轮缩放系数
        private void PicEdited_MouseWheel(object sender, MouseEventArgs e) {
            int ex = e.X;
            int ey = e.Y;
            int ow = picEdited.Width;
            int oh = picEdited.Height;
            int nw, nh;
            if (e.Delta > 0) {
                //放大
                nw = Convert.ToInt32(picEdited.Width * zoom);
                nh = Convert.ToInt32(picEdited.Height * zoom);

                //限制放大范围
                if (nw > Convert.ToInt32(myBitmap.Width / 3.125)) {
                    nw = Convert.ToInt32(myBitmap.Width / 3.125);
                    nh = Convert.ToInt32(nw * (oh / (double)ow));
                }
                if (nh > Convert.ToInt32(myBitmap.Height / 3.125)) {
                    nh = Convert.ToInt32(myBitmap.Height / 3.125);
                    nw = Convert.ToInt32(nh * (ow / (double)oh));
                }

            }
            else {
                //缩小
                nw = Convert.ToInt32(picEdited.Width / zoom);
                nh = Convert.ToInt32(picEdited.Height / zoom);

                //限制缩小范围
                if (nw < ML) {
                    nw = ML;
                    nh = Convert.ToInt32(nw * (oh / (double)ow));
                }
                if (nh < ML) {
                    nh = ML;
                    nw = Convert.ToInt32(nh * (ow / (double)oh));
                }
            }
            picEdited.Size = new Size(nw, nh);

            //计算缩放比例
            double scale = 100.0 * Math.Round(nw / (double)myBitmap.Width, 2);
            lblZoomScale.Text = "缩放比例:" + scale + "%";


            //锚点缩放
            //偏移坐标：vx,vy；新坐标：nx,ny
            int vx = Convert.ToInt32(ex * (double)(ow - picEdited.Width) / ow);
            int vy = Convert.ToInt32(ey * (double)(oh - picEdited.Height) / oh);
            int nx = picEdited.Left + vx;
            int ny = picEdited.Top + vy;

            //限制锚点缩放范围
            if (nx > 0) nx = 0;
            else {
                if (nx < ML - picEdited.Width) {
                    nx = ML - picEdited.Width;
                }
            }
            if (ny > 0) ny = 0;
            else {
                if (ny < ML - picEdited.Height) {
                    ny = ML - picEdited.Height;
                }
            }

            picEdited.Location = new Point(nx, ny);
            UpdateScroll();
        }

        //更新滚动条位置
        private void UpdateScroll() {
            //活动范围加上滚动条滑块自身宽度9
            //??? 滑块宽度为什么是9我也不知道
            int h = picEdited.Width - ML + 9;
            int v = picEdited.Height - ML + 9;
            if (h > 9) {
                hscMovePic.Enabled = true;
                hscMovePic.Maximum = h;
                hscMovePic.Value = -picEdited.Location.X;
            }
            else {
                hscMovePic.Enabled = false;
            }
            if (v > 9) {
                vscMovePic.Enabled = true;
                vscMovePic.Maximum = v;
                vscMovePic.Value = -picEdited.Location.Y;
            }
            else {
                vscMovePic.Enabled = false;
            }
        }

        //用滚动条调节图片位置
        private void ScrollBarScroll(object sender, ScrollEventArgs e) {
            int x = -hscMovePic.Value;
            int y = -vscMovePic.Value;
            picEdited.Location = new Point(x, y);
        }

        //打开对话框选取图片
        private Bitmap myBitmap;
        private void BtnSelectPic_Click(object sender, EventArgs e) {
            var ofd = new OpenFileDialog {
                Title = "请选择一张图片",
                Filter = "图片文件|*.jpg;*.png;*.bmp;*.jpeg;*.gif"
                + "|jpg文件|*.jpg" + "|png文件|*.png" + "|bmp文件|*.bmp"
                + "|jpeg文件|*.jpeg" + "|gif文件|*.gif" + "|所有文件|*.*"
            };
            if(ofd.ShowDialog() != DialogResult.OK) return;
            string fileName = ofd.FileName;
            try {
                new Bitmap(fileName, true);
            }
            catch {
                MessageBox.Show("图片读取失败\n请重新选择正确格式的图片文件", "错误提示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            Bitmap bitmap = new Bitmap(fileName);
            if (bitmap.Width < 320 || bitmap.Height < 320) {
                MessageBox.Show("图片分辨率太小了\n请重新选择320x320分辨率以上的图片", "错误提示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else {
                myBitmap = new Bitmap(fileName);
                picEdited.Location = new Point(0, 0);
                //按150：48 = 3.125 的比例缩小图片尺寸
                //故最大缩放比例约为32%
                picEdited.Width = Convert.ToInt32(myBitmap.Width / 3.125);
                picEdited.Height = Convert.ToInt32(myBitmap.Height / 3.125);
                picEdited.Image = myBitmap;
                ShowBackImage(picEdited);
                btnEditPic.Enabled = true;
                UpdateScroll();
            }
            bitmap.Dispose();
        }

        //显示背景图片(控件截图法)
        private void ShowBackImage(Control control) {
            Bitmap bitmap = new Bitmap(picImage.Width, picImage.Height);
            Rectangle rectangle = new Rectangle(0, 0, picImage.Width, picImage.Height);
            control.DrawToBitmap(bitmap, rectangle);
            picImage.Image = bitmap;
        }

        //蒙版预览加黑
        private void BtnMask_Click(object sender, EventArgs e) {
            //添加蒙版
            if (btnMask.Text == "蒙版") {
                btnMask.Text = "取消";
                btnEditPic.Enabled = false;
                btnSelectPic.Enabled = false;
                pnlBack.BackColor = Color.Black;

                if (picImage.Image != null) {
                    pnlTile.Enabled = false;
                    pnlTile.BackColor = Color.Black;
                    foreach (PictureBox p in pnlTile.Controls) {
                        if (p.BorderStyle == BorderStyle.FixedSingle) {
                            p.Image = PartImage.GetPart(picImage.Image, p.Left, p.Top, p.Width, p.Height);
                        }
                    }
                }
                else {
                    picImage.Visible = false;
                }
            }

            //取消蒙版
            else {
                btnMask.Text = "蒙版";
                btnSelectPic.Enabled = true;
                if (picImage.Image != null) {
                    btnEditPic.Enabled = true;
                    pnlTile.Enabled = true;
                    pnlTile.BackColor = Color.Transparent;
                }
                else {
                    picImage.Visible = true;
                }
                pnlBack.BackColor = MyBackColor;
                foreach (PictureBox p in pnlTile.Controls) {
                    p.Image = null;
                }
            }
        }

        //点击“编辑”\"完成"按钮
        private void BtnEditPic_Click(object sender, EventArgs e) {
            if (btnEditPic.Text == "编辑") {
                btnEditPic.Text = "完成";
                picImage.Image = null;
                pnlBack.BackColor = Color.Black;
                btnSelectPic.Enabled = false;
                btnMask.Enabled = false;
                pnlTile.Visible = false;
                picEdited.Visible = true;
                hscMovePic.Visible = true;
                vscMovePic.Visible = true;
                trbZoom.Enabled = true;
            }
            else {
                btnEditPic.Text = "编辑";
                ShowBackImage(picImage);
                pnlBack.BackColor = MyBackColor;
                btnSelectPic.Enabled = true;
                btnMask.Enabled = true;
                pnlTile.Visible = true;
                picEdited.Visible = false;
                hscMovePic.Visible = false;
                vscMovePic.Visible = false;
                trbZoom.Enabled = false;
            }
        }

        /* 定义说明
         * Rows：小块的行数列数
         * Gap：块与块的中间间隔距离
         * SL：小块的边长
         * ML：中块边长，也是选图、移动、缩放最小边长
         * Gtg：间隔与间隔的距离,中块未被选中时与被选中时左边缘(及上边缘)间隔的两倍
         * UnSelColor：块未被选中时半透明色,中块显示加号底色
         * MyBackColor:初始背景颜色
         * TileMode：块的样式:小块,中块
         */
        private static readonly int Rows = 10;
        private static readonly int Gap = 4;
        private static readonly int SL = 48;
        private static readonly int ML = 2 * SL + Gap;
        private static readonly int Gtg = Gap + SL;
        private static readonly Color UnSelColor = Color.FromArgb(100, Color.Black);
        private static readonly Color MyBackColor = Color.FromArgb(97, 154, 195);
        public enum TileMode { Small, Medium }
        public TileMode Style { get; set; }

        //磁贴图片框类
        private class TilePic : PictureBox {
            public TileMode Style { get; set; }

            public TilePic() {
                Height = (Width = SL);
                MouseClick += PicTile_MouseClick;
                MouseMove += PicTile_MouseMove;
                MouseHover += PicTile_MouseHover;
                MouseLeave += PicTile_MouseLeave;
            }

            private void PicTile_MouseClick(object sender, MouseEventArgs e) {
                var p = (TilePic)sender;
                //选中块
                if (e.Button == MouseButtons.Left) {
                    if (p.Style == TileMode.Small) {
                        p.BorderStyle = BorderStyle.FixedSingle;
                        p.BackColor = Color.Transparent;
                    }
                    else {
                        if (p.Style == TileMode.Medium && p.Width == SL && p.Image != null) {
                            p.Image = null;
                            p.BorderStyle = BorderStyle.FixedSingle;
                            p.BackColor = Color.Transparent;
                            p.Left -= Gtg / 2;
                            p.Top -= Gtg / 2;
                            p.Width = ML;
                            p.Height = p.Width;
                        }
                    }
                }

                //取消选中块
                else if (e.Button == MouseButtons.Right) {
                    p.BorderStyle = BorderStyle.None;
                    if (p.Style == TileMode.Small) {
                        p.BackColor = UnSelColor;
                    }
                    else {
                        if (p.Style == TileMode.Medium && p.Width == ML) {
                            p.SendToBack();
                            p.Left += Gtg / 2;
                            p.Top += Gtg / 2;
                            p.Width = SL;
                            p.Height = p.Width;
                        }
                    }
                }

                //禁止显示添加中块按钮
                var list1 = new List<TilePic>();
                var list2 = new List<TilePic>();
                foreach (TilePic pic in p.Parent.Controls) {
                    if (pic.BorderStyle == BorderStyle.FixedSingle) {
                        list1.Add(pic);
                    }
                    else {
                        if (pic.Style == TileMode.Medium && pic.BorderStyle == BorderStyle.None) {
                            list2.Add(pic);
                        }
                    }
                }
                foreach (TilePic p2 in list2) {
                    int num = 0;
                    foreach (TilePic p1 in list1) {
                        if (IsOverlap(p2, p1)) {
                            num++;
                        }
                    }
                    if (num == 0) {
                        p2.Enabled = true;
                    }
                    else {
                        p2.Enabled = false;
                    }
                }
            }

            private void PicTile_MouseLeave(object sender, EventArgs e) {
                var p = (TilePic)sender;
                if (p.BorderStyle == BorderStyle.None) {
                    if (p.Style == TileMode.Small) {
                        p.BackColor = UnSelColor;
                    }
                    else {
                        if (p.Style == TileMode.Medium && p.Width == SL) {
                            p.SendToBack();
                            p.Image = null;
                            p.BackColor = Color.Transparent;
                        }
                    }
                }
            }

            private void PicTile_MouseHover(object sender, EventArgs e) {
                var p = (TilePic)sender;
                if (p.Style == TileMode.Medium && p.BorderStyle == BorderStyle.None) {
                    p.Image = Resources.add;
                    p.BackColor = UnSelColor;
                    p.BringToFront();
                }
            }

            private void PicTile_MouseMove(object sender, MouseEventArgs e) {
                var p = (TilePic)sender;
                if (p.Style == TileMode.Small && p.BorderStyle == BorderStyle.None) {
                    p.BackColor = Color.FromArgb(40, Color.Black);
                }
            }

            //判断未选中中块是否与被选中块重叠(用中块中心位置判断)
            private static bool IsOverlap(TilePic pm, TilePic ps) {
                bool flag = pm.Left + SL / 2 < ps.Left - Gap / 2
                         || pm.Left + SL / 2 > ps.Right + Gap / 2
                         || pm.Top + SL / 2 < ps.Top - Gap / 2
                         || pm.Top + SL / 2 > ps.Bottom + Gap / 2;
                return !flag;
            }
        }
    }
}