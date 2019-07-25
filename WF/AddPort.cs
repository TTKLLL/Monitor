using DisplayBLL;
using Model;
using System;
using System.Windows.Forms;

namespace WF
{
    public partial class AddPort : BaseForm
    {
        //指向上一个窗口
        OpenPort last;
        public AddPort(OpenPort _last)
        {
            InitializeComponent();
            last = _last;
        }

        //判断是不是纯数字
        public bool isNumber(string str)
        {
            try
            {
                int.Parse(str);
                return true;
            }
            catch
            {
                return false;
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {
        }

        //添加端口
        private void button1_Click(object sender, EventArgs e)
        {
            string port = textBox1.Text;
            string xmno = textBox2.Text;
            if (port == "" || xmno == "")
            {
                Show("请输入端口号和项目编号");
                return;
            }

            if (isNumber(port) == false || isNumber(xmno) == false)
            {
                Show("请输入正确的端口号");
                return;
            }

            XmPortBLL bll = new XmPortBLL();
            if (bll.PortIsExist(int.Parse(port)) == true)
            {
                Show("该端口已存在");
                return;
            }

            XmPortModel model = new XmPortModel()
            {
                port = int.Parse(port),
                xmno = int.Parse(xmno)
            };
            if (bll.AddXmPort(model))
            {
                Show("添加端口成功");
                textBox1.Text = "";
                textBox2.Text = "";
            }
            else
            {
                Show("添加失败");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            CloseForm();
        }

        private void AddPort_Load(object sender, EventArgs e)
        {

        }

        void CloseForm()
        {
            this.Close();
        }

        private void AddPort_FormClosed(object sender, FormClosedEventArgs e)
        {
            //刷新上一个窗口的数据
            last.Initial();
        }
    }
}
