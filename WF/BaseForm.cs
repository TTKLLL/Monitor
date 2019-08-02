using System;
using System.Windows.Forms;

namespace WF
{
    public class BaseForm : Form
    {

        public void Show(string str)
        {
            MessageBox.Show(str);
        }

        public void CloseTheForm()
        {
            this.Close();
        }

        //询问消息框
        public DialogResult Ask(string str)
        {
            return MessageBox.Show(str, "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Asterisk);
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();

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
