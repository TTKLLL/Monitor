using System;
using System.Windows.Forms;

namespace WF
{
    public class BaseForm : Form
    {
        //用于往消息查看窗口添加日志的事件
        //public  delegate void SendLog(string log);
        //public event SendLog Send;

        public void Show(string str)
        {
            MessageBox.Show(str);
        }

        //询问消息框
        public DialogResult Ask(string str)
        {
            return MessageBox.Show(str, "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Asterisk);
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // BaseForm
            // 
            this.ClientSize = new System.Drawing.Size(284, 262);
            this.Name = "BaseForm";
            this.Load += new System.EventHandler(this.BaseForm_Load);
            this.ResumeLayout(false);

        }

      

        private void BaseForm_Load(object sender, EventArgs e)
        {

        }
    }
}
