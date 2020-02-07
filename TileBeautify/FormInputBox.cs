using System;
using System.Windows.Forms;

namespace TileBeautify {
    public partial class FormInputBox : Form {
        public FormInputBox(Action<FormInputBox> actionFrmLoad, Action<FormInputBox> actionBtnOKClick) {
            InitializeComponent();
            this.AcceptButton = btnOK;
            this.CancelButton = btnCancel;

            ///在窗体加载、点击“确认”、“取消”按钮后分别发生啥子
            ///主要事件就这三，如果想要其他事件，可在调用处添加事件绑定
            ///FormInputBox类如无必要，绝不要再往里面添加任何代码
            ///已在.Designer.cs里面将所有控件设为public,故在调用FormInputBox类时可直接访问所有控件
            ///虽然可直接引用Microsoft.VisualBasic中的InputBox,但是就是想自己写一个
            this.Load += (sender, e) => actionFrmLoad(this);
            btnOK.Click += (sender, e) => actionBtnOKClick(this);
            btnCancel.Click += (sender, e) => {
                this.txtInput.Text = null;
                this.Dispose();
            };
        }
    }
}
