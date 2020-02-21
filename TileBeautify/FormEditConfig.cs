using System;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace TileBeautify
{
    public partial class FormEditConfig : Form
    {
        /* 定义说明
         * MyName：几乎所有文件夹名
         * StartMenuExe：开始菜单exe程序文件夹路径
         * StartMenuUrl：开始菜单url程序文件夹路径
         * configPath：配置文件夹路径
         * pickColorCurPath：吸管光标文件路径
         * openURlExePath：打开url链接程序文件路径
         * myUrlExePath：url磁贴程序文件路径
         * myUrlIniPath：url信息文件路径
         */
        public static readonly string MyName = "TileBeautify";
        public static readonly string StartMenuExe = Shortcut.UserStartMenu + "\\@" + MyName + "\\";
        public static readonly string StartMenuUrl = Shortcut.UserStartMenu + "\\@TileURL\\";
        private static readonly string configPath = "./\\Config";
        private static readonly string pickColorCurPath = configPath + "\\PickColor.cur";
        private static readonly string openURlExePath = configPath + "\\OpenURl.exe";
        private static readonly string urlFolder = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\" + MyName + "\\URL";
        private static readonly string myUrlExePath = urlFolder + "\\我的网址.exe";
        private static readonly string myUrlIniPath = urlFolder + "\\我的网址.ini";
        private static readonly string gitHubUrl = "https://github.com/BluePointLilac/TileBeautify/releases";

        private string myExePath;//选取的程序的文件路径
        private string urlPath = null;//url链接地址
        private bool isAlreadyHasPic;//是否已存在图片
        private bool mightHasPic = false;//可能存在可缩放图片
        private string iconPath;//图标资源文件路径
        private int iconIndex;//图标序号

        public FormEditConfig()
        {
            InitializeComponent();
            InitializeControl();
            BindEvent();
            picColorView.BackColor = ThemeColor.GetThemeColor();

            if (!Directory.Exists(urlFolder))
            {
                Directory.CreateDirectory(urlFolder);
            }
            if (File.Exists(openURlExePath))
            {
                if (!File.Exists(myUrlExePath))
                {
                    File.Copy(openURlExePath, myUrlExePath);
                }
            }
        }

        private void InitializeControl()
        {
            this.Icon = Icon.ExtractAssociatedIcon(Application.ExecutablePath);
            Image image = this.Icon.ToBitmap();
            ShowMyIcon(image);
            ShowPic(null);

            cmbCommand.SelectedIndex = 0;
            cmbTileShowMode.SelectedIndex = 0;
            cmbFontColor.SelectedIndex = 0;
            Graphics g = this.CreateGraphics();
            float a = g.DpiX / 96;
            txtNameView.Text = MyName;
            txtPathView.Text = "拖动程序图标到预览窗格,或点击\"选取程序\"按钮";
            lblTileName.Parent = pic150x150View;
            lblTileName.Left = Convert.ToInt32(4 * a);
            lblTileName.ForeColor = Color.White;
            lblTileName.BackColor = Color.Transparent;
            lblTileName.Font = new Font(lblTileName.Font.FontFamily, 8.2f);
            new ResizeFont().KeepFontSize(this);
            lblTileName.Top = pic150x150View.Height - lblTileName.Height - Convert.ToInt32(4 * a);
            chkShowName.Left = grpName.Width - chkShowName.Width - Convert.ToInt32(2 * a);

            //添加分隔线
            new Label
            {
                Parent = grpCommand,
                BorderStyle = BorderStyle.Fixed3D,
                Width = grpCommand.Width - 2,
                Height = 1,
                Left = 1,
                Top = txtPathView.Top - 1
            };
        }

        //绑定控件事件
        private void BindEvent()
        {
            grpName.Paint += RepaintGroupBox.Grp_Paint;
            grpColor.Paint += RepaintGroupBox.Grp_Paint;
            grpPreview.Paint += RepaintGroupBox.Grp_Paint;
            grpPic.Paint += RepaintGroupBox.Grp_Paint;
            grpIcon.Paint += RepaintGroupBox.Grp_Paint;
            grpCommand.Paint += RepaintGroupBox.Grp_Paint;
            grpBluePoint.Paint += RepaintGroupBox.Grp_Paint;

            chkShowName.CheckedChanged += (sender, e) => lblTileName.Visible = chkShowName.Checked;
            txtNameView.TextChanged += (sender, e) => lblTileName.Text = txtNameView.Text;

            pic150x150View.SizeModeChanged += (sender, e) =>
            {
                pic70x70View.SizeMode = pic150x150View.SizeMode;
                pic44x44View.SizeMode = pic150x150View.SizeMode;
            };

            cmbFontColor.SelectedIndexChanged += (sender, e) =>
            {
                lblTileName.ForeColor = (cmbFontColor.SelectedIndex == 1) ? Color.Black : Color.White;
            };

            picColorView.BackColorChanged += (sender, e) =>
            {
                pic150x150View.BackColor = picColorView.BackColor;
                pic70x70View.BackColor = picColorView.BackColor;
                pic44x44View.BackColor = picColorView.BackColor;
                txtCodeView.Text = ColorTranslator.ToHtml(picColorView.BackColor).ToLower();
            };

            btnSelectExe.Click += (sender, e) =>
            {
                if (cmbCommand.SelectedIndex == 0) SelectExe(sender, e);
                else InputUrl();
            };

            btnAboutMe.Click += (sender, e) =>
            {
                var frm = new FormAboutMe() { Icon = this.Icon };
                frm.ShowDialog();
            };

            btnReplaceName.Click += ReplaceName;
            btnInputColorCode.Click += InputColorCode;
            btnSelectColor.Click += SelectColor;
            btnPickColor.Click += PickColor;
            btnReplaceIcon.Click += ReplaceIcon;
            cmbTileShowMode.SelectedIndexChanged += ShowPicOrIcon;
            cmbCommand.SelectedIndexChanged += UseExeOrUrl;
            chkUseEditedPic.CheckedChanged += CheckUseEditedPic;
            btnSaveStyle.Click += SaveStyle;
            btnClearStyle.Click += CLearStyle;
            ElevatedDragDropManager.Instance.EnableDragDrop(grpPreview.Handle);
            ElevatedDragDropManager.Instance.ElevatedDragDrop += ElevatedDragDrop;
        }

        //选择exe还是url
        private void UseExeOrUrl(object sender, EventArgs e)
        {
            if (cmbCommand.SelectedIndex == 1)
            {
                btnSelectExe.Text = "输入链接";
                txtPathView.Text = null;
                if (!File.Exists(openURlExePath))
                {
                    cmbCommand.SelectedIndex = 0;
                    var mb = MessageBox.Show("缺失OpenURL.exe文件,请前往GitHub下载完整版", "错误提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation);
                    if (mb == DialogResult.OK)
                    {
                        Process.Start("explorer.exe", gitHubUrl);
                    }
                }
                else InputUrl();
            }
            else
            {
                btnSelectExe.Text = "选取程序";
                txtPathView.Text = "拖动程序图标到预览窗格,或点击\"选取程序\"按钮";
            }
        }

        //输入url链接
        private void InputUrl()
        {
            var action1 = new Action<FormInputBox>(FormLoadForInputUrl);
            var action2 = new Action<FormInputBox>(BtnOkClickForInputUrl);
            var frm = new FormInputBox(action1, action2);
            frm.ShowDialog();

            if (urlPath == null)
            {
                cmbCommand.SelectedIndex = 0;
            }
            else
            {
                txtPathView.Text = urlPath;
                if (File.Exists(myUrlIniPath)) File.Delete(myUrlIniPath);
                File.WriteAllText(myUrlIniPath, urlPath, Encoding.Default);
                txtNameView.Text = "我的网址";
                myExePath = myUrlExePath;
                btnSaveStyle.Enabled = true;
                if (picEditedView.Image != null)
                {
                    cmbTileShowMode.Enabled = true;
                }
                picColorView.BackColor = ThemeColor.GetThemeColor();
                iconIndex = 0;

                //创建一个Internet快捷方式文件，获取默认浏览器图标
                iconPath = urlFolder + "\\" + MyName + ".url";
                StreamWriter sw = File.CreateText(iconPath);
                sw.WriteLine("[InternetShortcut]");
                sw.WriteLine("URL=" + gitHubUrl);
                sw.Close();

                Icon icon = Icon.ExtractAssociatedIcon(iconPath);
                picIconView.Image = icon.ToBitmap();
                ShowPic(myExePath);
            }
        }

        //输入url链接输入框窗体初始化
        private void FormLoadForInputUrl(FormInputBox frm)
        {
            frm.Text = "请输入URL地址或文件夹路径";
            frm.txtInput.Text = txtPathView.Text;
            frm.Icon = this.Icon;
        }

        //输入url链接点击ok按钮事件
        private void BtnOkClickForInputUrl(FormInputBox frm)
        {
            urlPath = frm.txtInput.Text;
            frm.Dispose();
        }

        //最后要生成样式时修改文件名（复制）
        private void ReplaceUrlFileName()
        {
            if (cmbCommand.SelectedIndex == 1)
            {
                DeleteUrlExe();
                string text = txtNameView.Text;
                string newUrlExePath = urlFolder + "\\" + text + ".exe";
                string newUrlIniPath = urlFolder + "\\" + text + ".ini";
                File.Copy(myUrlExePath, newUrlExePath);
                File.Copy(myUrlIniPath, newUrlIniPath);
                File.SetAttributes(newUrlIniPath, FileAttributes.Hidden);
                myExePath = newUrlExePath;
            }
        }

        //清理相同的URL程序
        private void DeleteUrlExe()
        {
            var di = new DirectoryInfo(urlFolder);
            foreach (var fi in di.GetFiles())
            {
                string fileName = fi.FullName;
                string format = Path.GetExtension(fileName);
                if (format == ".ini")
                {
                    string myUrlPath = File.ReadAllText(fileName);
                    if (myUrlPath == urlPath)
                    {
                        string tileName = Path.GetFileNameWithoutExtension(fileName);
                        if (tileName != "我的网址")
                        {
                            //删除链接相同的ini文件
                            File.Delete(fileName);
                            //删除对应的exe文件
                            string exePath = urlFolder + "\\" + tileName + ".exe";
                            if (File.Exists(exePath)) File.Delete(exePath);
                            //删除对应的xml文件
                            string xmlPath = urlFolder + "\\" + tileName + ".VisualElementsManifest.xml";
                            if (File.Exists(xmlPath)) File.Delete(xmlPath);
                            //删除对应的png文件
                            string picPath = urlFolder + "\\" + MyName + "\\" + tileName + ".png";
                            if (File.Exists(picPath)) File.Delete(picPath);
                            //删除对应的快捷方式
                            string lnkPath = StartMenuUrl + tileName + ".lnk";
                            if (File.Exists(lnkPath)) File.Delete(lnkPath);
                        }
                    }
                }
            }
        }

        //删除样式文件
        private void CLearStyle(object sender, EventArgs e)
        {
            var mb = MessageBox.Show("此操作将会删除程序的所有样式文件\n并将磁贴磁贴还原为默认样式\n确认继续你的操作？", "温馨提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation);
            if (mb == DialogResult.OK)
            {
                string tileName = txtNameView.Text;
                TileXml tileXml = new TileXml(myExePath);
                try
                {
                    if (File.Exists(tileXml.XmlPath)) File.Delete(tileXml.XmlPath);
                    if (File.Exists(tileXml.Square150x150Logo)) File.Delete(tileXml.Square150x150Logo);
                    if (File.Exists(tileXml.Square70x70Logo)) File.Delete(tileXml.Square70x70Logo);
                    if (File.Exists(tileXml.Square44x44Logo)) File.Delete(tileXml.Square44x44Logo);

                    Shortcut shortcut = new Shortcut();
                    if (cmbCommand.SelectedIndex == 0)
                    {
                        shortcut.CreateShortcut(myExePath, tileName, iconPath, iconIndex, false);
                    }
                    else
                    {
                        shortcut.CreateShortcut(myExePath, tileName, iconPath, iconIndex, true);
                    }
                }
                catch (Exception) { }

                if (!File.Exists(tileXml.XmlPath))
                {
                    new FormMyMessageBox("磁贴样式文件已删除，磁贴已还原为默认样式").Show();
                }
                else
                {
                    new FormMyMessageBox("删除失败,请尝试手动删除程序安装路径下的\n程序同名的.VisualElementsManifest.xml文件\n以及TileBeautify文件夹").Show();
                    Process.Start("explorer", "/select,\"" + tileXml.XmlPath + "\"");
                }
            }
        }

        //生成样式文件
        private void SaveStyle(object sender, EventArgs e)
        {
            string tileName = txtNameView.Text;
            if (cmbCommand.SelectedIndex == 1)
            {
                if (tileName == "我的网址")
                {
                    new FormMyMessageBox("请更改你的磁贴名称").Show();
                    ReplaceName(null, null);
                    return;
                }

                var di = new DirectoryInfo(urlFolder);
                foreach (var fi in di.GetFiles())
                {
                    string fileName = Path.GetFileNameWithoutExtension(fi.FullName);
                    if (tileName == fileName)
                    {
                        string iniPath = urlFolder + "\\" + fileName + ".ini";
                        string urlPath = File.ReadAllText(iniPath, Encoding.Default);
                        var mb = MessageBox.Show("已存在同名文件URL磁贴程序，确认覆盖？" +
                            "\n链接：" + urlPath, "错误提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation);

                        if (mb == DialogResult.OK)
                        {
                            File.Delete(fi.FullName);
                            File.Delete(iniPath);
                        }
                        else return;
                    }
                }
                ReplaceUrlFileName();
            }

            TileXml tileXml = new TileXml(myExePath)
            {
                BackgroundColor = txtCodeView.Text,
                ForegroundText = (cmbFontColor.SelectedIndex == 1) ? "dark" : "light",
                ShowNameOnSquare150x150Logo = (!chkShowName.Checked) ? "off" : "on"
            };
            string picFolder = tileXml.ExeFolder + MyName;
            string picPath = picFolder + "\\" + tileXml.ExeName + ".png";

            if (cmbTileShowMode.SelectedIndex == 1)
            {
                if (chkUseEditedPic.Checked == true)
                {
                    string shortPicPath = MyName + "\\" + tileXml.ExeName + ".png";
                    tileXml.Square150x150Logo = shortPicPath;
                    tileXml.Square70x70Logo = shortPicPath;
                    tileXml.Square44x44Logo = shortPicPath;
                }
                else
                {
                    tileXml.Square150x150Logo = tileXml.ReadXml("Square150x150Logo");
                    tileXml.Square70x70Logo = tileXml.ReadXml("Square70x70Logo");
                    tileXml.Square44x44Logo = tileXml.ReadXml("Square44x44Logo");
                }
            }
            else
            {
                tileXml.Square150x150Logo = string.Empty;
                tileXml.Square70x70Logo = string.Empty;
                tileXml.Square44x44Logo = string.Empty;
            }

            try
            {
                if (chkUseEditedPic.Checked == true)
                {
                    Directory.CreateDirectory(picFolder);
                    picEditedView.Image.Save(picPath, ImageFormat.Png);
                }

                if (Directory.Exists(picFolder))
                {
                    SystemHidenFile.HideFile(picFolder);
                }

                if (cmbTileShowMode.SelectedIndex == 0)
                {
                    if (Directory.Exists(picFolder))
                    {
                        if (File.Exists(picPath))
                        {
                            File.Delete(picPath);
                            //文件夹中没有文件时删除
                            if (Directory.GetDirectories(picFolder).Length <= 0 && Directory.GetFiles(picFolder).Length <= 0)
                            {
                                Directory.Delete(picFolder);
                            }
                        }
                    }
                }
                tileXml.WriteXml();
                Shortcut shortcut = new Shortcut();
                if (cmbCommand.SelectedIndex == 0)
                {
                    shortcut.CreateShortcut(myExePath, tileName, iconPath, iconIndex, false);
                }
                else
                {
                    shortcut.CreateShortcut(myExePath, tileName, iconPath, iconIndex, true);
                }
                new FormMyMessageBox("已成功修改程序磁贴样式,请手动将其固定到开始屏幕").Show();
            }
            catch (Exception)
            {
                new FormMyMessageBox("无法在程序目录写入样式文件\n可能为开启了自我保护的安全软件").Show();
            }
        }

        //显示图片还是图标
        private void ShowPicOrIcon(object sender, EventArgs e)
        {
            if (cmbTileShowMode.SelectedIndex == 0)
            {
                chkUseEditedPic.Checked = false;
                chkUseEditedPic.Enabled = false;
                UseIcoAsPic();
            }
            else
            {
                if (picEditedView.Image != null)
                {
                    if (isAlreadyHasPic)
                    {
                        chkUseEditedPic.Checked = false;
                        chkUseEditedPic.Enabled = true;
                    }
                    else
                    {
                        chkUseEditedPic.Checked = true;
                        chkUseEditedPic.Enabled = false;
                    }
                }
            }
        }

        //是否使用已裁切好的图片
        private void CheckUseEditedPic(object sender, EventArgs e)
        {
            if (cmbTileShowMode.SelectedIndex == 1)
            {
                if (chkUseEditedPic.Checked == true)
                {
                    var mb = DialogResult.OK;
                    if (isAlreadyHasPic)
                    {
                        mb = MessageBox.Show("当前程序已存在图片样式文件，确认替换？", "温馨提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Asterisk);
                    }
                    else
                    {
                        if (mightHasPic)
                        {
                            mb = MessageBox.Show("当前程序可能已存在开发者自定义的可缩放图片样式文件，确认替换？", "温馨提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Asterisk);
                        }
                    }

                    if (mb == DialogResult.OK)
                    {
                        pic150x150View.SizeMode = PictureBoxSizeMode.StretchImage;
                        pic150x150View.Image = picEditedView.Image;
                        pic70x70View.Image = picEditedView.Image;
                        pic44x44View.Image = picEditedView.Image;
                    }
                    else
                    {
                        chkUseEditedPic.Checked = false;
                    }
                }
                else
                {
                    ShowPic(myExePath);
                }
            }
        }

        //屏幕取色
        private void PickColor(object sender, EventArgs e)
        {
            var frm = new FormPickColor
            {
                PickedColor = picColorView.BackColor
            };
            ReplaceCursor.SetCursor(pickColorCurPath, frm.picScreenShot);
            frm.ShowDialog();
            picColorView.BackColor = frm.PickedColor;
        }

        //颜色选择框中选色
        private void SelectColor(object sender, EventArgs e)
        {
            var cd = new ColorDialog
            {
                FullOpen = true,
                Color = picColorView.BackColor
            };
            cd.ShowDialog();
            cd.Dispose();
            picColorView.BackColor = cd.Color;
        }

        //输入HTML颜色代码
        private void InputColorCode(object sender, EventArgs e)
        {
            var action1 = new Action<FormInputBox>(FormLoadForInputHTMLCode);
            var action2 = new Action<FormInputBox>(BtnOkClickForInputHTMLCode);
            var frm = new FormInputBox(action1, action2);
            frm.ShowDialog();
        }

        //输入HTML颜色代码的输入框窗体初始化
        private void FormLoadForInputHTMLCode(FormInputBox frm)
        {
            frm.Icon = this.Icon;
            frm.Text = "请输入HTML颜色代码";
            frm.txtInput.Text = txtCodeView.Text;
        }

        //输入HTML颜色代码的点击ok按钮事件
        private void BtnOkClickForInputHTMLCode(FormInputBox frm)
        {
            string text = frm.txtInput.Text;
            try
            {
                //考虑到用户懒惰行为：不输入#，不带#的纯数字解析值不同
                text = "#" + text;
                ColorTranslator.FromHtml(text);
            }
            catch (Exception)
            {
                try
                {
                    text = frm.txtInput.Text;
                    ColorTranslator.FromHtml(text);
                }
                catch (Exception)
                {
                    new FormMyMessageBox("请输入正确的HTML颜色代码").Show();
                    return;
                }
            }
            picColorView.BackColor = ColorTranslator.FromHtml(text);
            frm.Dispose();
        }

        //修改磁贴名称
        private void ReplaceName(object sender, EventArgs e)
        {
            var action1 = new Action<FormInputBox>(FormLoadForRePlaceName);
            var action2 = new Action<FormInputBox>(BtnOkClickForRePlaceName);
            var frm = new FormInputBox(action1, action2);
            frm.ShowDialog();
        }

        //修改磁贴名称的输入框窗体初始化
        private void FormLoadForRePlaceName(FormInputBox frm)
        {
            frm.Icon = this.Icon;
            frm.Text = "请输入新名称";
            frm.txtInput.Text = txtNameView.Text;
        }

        //修改磁贴名称的点击ok按钮事件
        private void BtnOkClickForRePlaceName(FormInputBox frm)
        {
            string text = frm.txtInput.Text;
            string[] errorStrs = { "\\", "/", ":", "*", "\"", "<", ">", "|" };

            string allErrorStr = null;
            foreach (var str in errorStrs)
            {
                allErrorStr += (str + " ");
            }

            foreach (var str in errorStrs)
            {
                if (text.Contains(str))
                {
                    new FormMyMessageBox("请勿输入以下文件名中不支持的字符\n" + allErrorStr).Show();
                    return;
                }
            }
            txtNameView.Text = text;
            frm.Dispose();
        }

        //更改图标
        private void ReplaceIcon(object sender, EventArgs e)
        {
            var frm = new FormReplaceIcon(iconPath)
            {
                Icon = this.Icon
            };
            frm.ShowDialog();
            if (frm.IconIndex == -1) return;
            iconPath = frm.IconPath;
            iconIndex = frm.IconIndex;
            Image image = new TileIcon().ToImage(iconPath, iconIndex);
            ShowMyIcon(image);
        }

        //将图标显示出来
        private void ShowMyIcon(Image image)
        {
            if (image == null) return;
            Graphics g = this.CreateGraphics();
            float scale = 96 / g.DpiX;
            Image newImage = PictureZoom.ZoomPic(image, scale);
            picIconView.Image = newImage;
            UseIcoAsPic();
        }

        //文件拖拽
        private void ElevatedDragDrop(object sender, ElevatedDragDropArgs e)
        {
            if (e.HWnd == grpPreview.Handle)
            {
                string filePath = e.Files[0];
                string fileName = Path.GetFileNameWithoutExtension(filePath);
                filePath = GetRealPath(filePath);
                if (filePath == null) return;
                FillInfo(filePath, fileName);
            }
        }

        //打开对话框选取程序
        private void SelectExe(object sender, EventArgs e)
        {
            var ofd = new OpenFileDialog
            {
                Title = "请选择一个exe应用程序或者其快捷方式",
                Filter = "应用程序|*.exe",
                //禁止直接获取到快捷方式目标路径，以便于获取快捷方式名称
                DereferenceLinks = false
            };
            if (ofd.ShowDialog() != DialogResult.OK) return;
            string filePath = ofd.FileName;
            if (filePath == null) return;
            string fileName = Path.GetFileNameWithoutExtension(filePath);
            filePath = GetRealPath(filePath);
            if (filePath == null) return;
            FillInfo(filePath, fileName);
        }

        //获取文件真实路径
        private static string GetRealPath(string filePath)
        {
            string format = Path.GetExtension(filePath).ToLower();
            if (format == ".exe") return filePath;
            else if (format == ".lnk")
            {
                var wsh = new IWshRuntimeLibrary.WshShell();
                var shortcut = (IWshRuntimeLibrary.IWshShortcut)wsh.CreateShortcut(filePath);
                filePath = shortcut.TargetPath;
                if (!File.Exists(filePath))
                {
                    new FormMyMessageBox("此快捷方式指向的文件路径不存在\n或者为不支持修改样式的UWP应用").Show();
                    return null;
                }
                format = Path.GetExtension(filePath).ToLower();
                if (format == ".exe") return filePath;
                else
                {
                    new FormMyMessageBox("这不是exe程序的快捷方式").Show();
                    return null;
                }
            }
            else return null;
        }

        //根据xml内容填充信息
        private void FillInfo(string filePath, string fileNeme)
        {
            cmbTileShowMode.SelectedIndex = 0;
            cmbCommand.SelectedIndex = 0;
            myExePath = filePath;
            txtPathView.Text = filePath;
            btnSaveStyle.Enabled = true;
            txtNameView.Text = fileNeme;
            Image image = Icon.ExtractAssociatedIcon(filePath).ToBitmap();
            ShowMyIcon(image);
            iconIndex = 0;
            iconPath = filePath;
            ShowPic(filePath);

            if (picEditedView.Image != null || isAlreadyHasPic) cmbTileShowMode.Enabled = true;

            TileXml tileXml = new TileXml(filePath);
            if (!File.Exists(tileXml.XmlPath))
            {
                chkShowName.Checked = true;
                cmbFontColor.SelectedIndex = 0;
                picColorView.BackColor = ThemeColor.GetThemeColor();
                btnClearStyle.Enabled = false;
            }
            else
            {
                chkShowName.Checked = (tileXml.ShowNameOnSquare150x150Logo == "off") ? false : true;
                cmbFontColor.SelectedIndex = (tileXml.ForegroundText == "dark") ? 1 : 0;
                picColorView.BackColor = ColorTranslator.FromHtml(tileXml.BackgroundColor);
                btnClearStyle.Enabled = true;
            }
        }

        //显示图片
        private void ShowPic(string filePath)
        {
            TileXml tileXml = new TileXml(filePath);
            if (!File.Exists(tileXml.XmlPath))
            {
                isAlreadyHasPic = false;
                UseIcoAsPic();
                return;
            }
            mightHasPic = tileXml.MightHasPic;
            string square150x150Logo = tileXml.Square150x150Logo;
            string square70x70Logo = tileXml.Square70x70Logo;
            string square44x44Logo = tileXml.Square44x44Logo;

            int num = 0;
            if (File.Exists(square150x150Logo)) num++;
            if (File.Exists(square70x70Logo)) num += 2;
            if (File.Exists(square44x44Logo)) num += 4;

            if (num == 0)
            {
                isAlreadyHasPic = false;
                UseIcoAsPic();
                return;
            }
            else if (num == 1)
            {
                pic150x150View.ImageLocation = square150x150Logo;
                pic70x70View.ImageLocation = square150x150Logo;
                pic44x44View.ImageLocation = square150x150Logo;
            }
            else if (num == 2)
            {
                pic150x150View.ImageLocation = square70x70Logo;
                pic70x70View.ImageLocation = square70x70Logo;
                pic44x44View.ImageLocation = square70x70Logo;
            }
            else if (num == 3)
            {
                pic150x150View.ImageLocation = square150x150Logo;
                pic70x70View.ImageLocation = square70x70Logo;
                pic44x44View.ImageLocation = square70x70Logo;
            }
            else if (num == 4)
            {
                pic150x150View.ImageLocation = square44x44Logo;
                pic70x70View.ImageLocation = square44x44Logo;
                pic44x44View.ImageLocation = square44x44Logo;
            }
            else if (num == 5)
            {
                pic150x150View.ImageLocation = square150x150Logo;
                pic70x70View.ImageLocation = square150x150Logo;
                pic44x44View.ImageLocation = square44x44Logo;
            }
            else if (num == 6)
            {
                pic150x150View.ImageLocation = square70x70Logo;
                pic70x70View.ImageLocation = square70x70Logo;
                pic44x44View.ImageLocation = square44x44Logo;
            }
            else if (num == 7)
            {
                pic150x150View.ImageLocation = square150x150Logo;
                pic70x70View.ImageLocation = square70x70Logo;
                pic44x44View.ImageLocation = square44x44Logo;
            }
            isAlreadyHasPic = true;
            cmbTileShowMode.SelectedIndex = 1;
            pic150x150View.SizeMode = PictureBoxSizeMode.StretchImage;
        }

        //使用图标磁贴
        private void UseIcoAsPic()
        {
            cmbTileShowMode.SelectedIndex = 0;
            pic150x150View.SizeMode = PictureBoxSizeMode.CenterImage;
            Image image = picIconView.Image;
            pic150x150View.Image = image;
            pic70x70View.Image = PictureZoom.ZoomPic(image, 0.75);
            pic44x44View.Image = PictureZoom.ZoomPic(image, 0.5625);
        }
    }
}
