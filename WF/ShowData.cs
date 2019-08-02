using DisplayBLL;
using System;
using System.Text.RegularExpressions;
using Tcp;
using Tool;

namespace WF
{
    public partial class Form1 : BaseForm
    {
        TcpBLL tcpBll = new TcpBLL();
        public Form1()
        {
            //允许控件被其他线程访问
            CheckForIllegalCrossThreadCalls = false;
            //给FileOpetion的event添加方法
            //当向日志中写入数据时 触发事件将该数据添加到窗口上
            FileOperation.Send += new FileOperation.SendLog(AppendLog);
            //当清空了日志后 窗口重新加载日志
            FileOperation.ReLoadLog += new FileOperation.ReLoadLogDele(GetLog);

            ////给Tcp的处理数据事件添加行为
            TcpServer.ProcessDataEvent += new TcpServer.ProcessData(new DataProcessCommon().ProcessData);

            InitializeComponent();
            textBox1.Enabled = false;
        }

        //获取日志数据
        public void GetLog()
        {
            //行数的默认值
            int line = 100;
            string str = comboBox1.Text;
            comboBox1.Text = "";

            if (comboBox1.Text != null)
                if (comboBox1.Text.Trim() != "")
                    line = int.Parse(comboBox1.Text);

            //获取日志的后line行记录
            richTextBox1.Text = FileOperation.ReadLastLine(line);
            richTextBox1.ReadOnly = true;

            //文本框滚动条在底部
            richTextBox1.SelectionStart = richTextBox1.TextLength;
            richTextBox1.Focus();



            //while (true)
            //{
            //    string a = "";
            //}
        }

        //通过异步方式从日志文件中获取
        public void AysncGetLog()
        {
            Action action = () => { GetLog(); };
            action.BeginInvoke(null, null);
        }

        //将最新的日志添加到窗口
        public void AppendLog(string log)
        {
            richTextBox1.AppendText(log + "\n");
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            InitialDropList();

            AysncGetLog();
            BindIp();
        }

        //给行数列表赋值
        public void InitialDropList()
        {
            comboBox1.Items.Add("100");
            comboBox1.Items.Add("300");
            comboBox1.Items.Add("500");
            // comboBox1.SelectedIndex = 0;
        }

        //当有新日志插入时 让滚动条自动到底部
        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {
            richTextBox1.ScrollToCaret();
        }

        //打开端口设置窗口
        private void button1_Click(object sender, EventArgs e)
        {
            ShowForm(new OpenPort());
        }

        //打开新窗口  
        void ShowForm(BaseForm form)
        {
            form.ShowDialog();
        }

        //测点设置
        private void point_Click(object sender, EventArgs e)
        {
            ShowForm(new PointSetting());
        }

        //传感器设置
        private void button1_Click_1(object sender, EventArgs e)
        {
            ShowForm(new DeviceSetting());
        }

        //切换日志显示的行数
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            // GetLog();
            AysncGetLog();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            ShowForm(new MKSetting());
        }

        //查看监测数据
        private void button3_Click(object sender, EventArgs e)
        {
            ShowForm(new FinalData());
        }

        //修改ip地址
        private void button4_Click(object sender, EventArgs e)
        {
            string regr = "((?:(?:25[0-5]|2[0-4]\\d|((1\\d{2})|([1-9]?\\d)))\\.){3}(?:25[0-5]|2[0-4]\\d|((1\\d{2})|([1-9]?\\d))))";
            string name = button4.Text.Trim();

            if (name == "修改")
            {
                button4.Text = "确认修改";
                textBox1.Enabled = true;
            }
            else
            {
                string ip = textBox1.Text.Trim();
                string oldIp = tcpBll.GetIp();
                if (oldIp == ip)
                {
                    Show("要更新的ip的原来的相同");
                    textBox1.Enabled = false;
                    button4.Text = "修改";
                    return;
                }
                if (!Regex.IsMatch(ip, regr))
                {
                    Show("请输入正确的ip地址");
                    textBox1.Text = tcpBll.GetIp();
                    textBox1.Enabled = false;
                    button4.Text = "修改";
                    return;
                }
                if (tcpBll.SetIp(ip))
                {
                    Show("成功设置ip为 " + ip);
                }
                else
                {
                    textBox1.Text = tcpBll.GetIp();
                    Show("设置ip失败");
                }
                textBox1.Enabled = false;
                button4.Text = "修改";
            }

        }

        //显示ip地址
        public void BindIp()
        {
            textBox1.Text = tcpBll.GetIp();
        }
    }
}

