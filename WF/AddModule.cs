using DisplayBLL;
using Model;
using System;

namespace WF
{
    public partial class AddModule : BaseForm
    {
        private MKBLL mkBll = new MKBLL();
        public delegate void Reload();
        //重新加载模块信息
        public static event Reload ReLoadModules;

        public AddModule()
        {
            InitializeComponent();
        }

        //获取端口号
        public void InitialPorts()
        {
            XmPortBLL bll = new XmPortBLL();
            foreach (var item in bll.GetPorts())
            {
                comboBox1.Items.Add(item.port);
            }
        }

        //添加模块
        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text.Trim() == "")
            {
                Show("请输入模块号");
                return;
            }
            if (comboBox1.Text.Trim() == "")
            {
                Show("请选择端口");
                return;
            }
            ModuleModel model = new ModuleModel()
            {
                mkno = textBox1.Text.Trim(),
                port = comboBox1.Text.Trim()
            };
            if (mkBll.AddModule(model))
            {
                Show("添加成功");
                if (ReLoadModules != null)
                    ReLoadModules();
                textBox1.Text = "";
            }
            else
            {
                Show("添加失败");
            }

        }

        private void AddModule_Load(object sender, EventArgs e)
        {
            InitialPorts();
        }

        //判断模块号是否存在
        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (mkBll.IsHaveModule(textBox1.Text.Trim()))
            {
                label3.Text = "该模块已经存在";
                button1.Enabled = false;
            }
            else
            {
                label3.Text = "";
                button1.Enabled = true;
            }
        }

        //关闭窗口
        private void button2_Click(object sender, EventArgs e)
        {
            CloseTheForm();
        }

       
    }
}
