using System;
using Tool;

namespace WF
{
    public partial class Form1 : BaseForm
    {
        public Form1()
        {
            //给FileOpetion的event添加方法
            //当向日志中写入数据时 触发事件将该数据添加到窗口上
            FileOperation.Send += new FileOperation.SendLog(AppendLog);
            //当清空了日志后 窗口重新加载日志
            FileOperation.ReLoadLog += new FileOperation.ReLoadLogDele(GetLog);
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            InitialDropList();
            GetLog();
        }

        //将最新的日志添加到窗口
        public void AppendLog(string log)
        {
            richTextBox1.AppendText(log + "\n");
        }

        //给行数列表赋值
        public void InitialDropList()
        {
            comboBox1.Items.Add("100");
            comboBox1.Items.Add("300");
            comboBox1.Items.Add("500");
            comboBox1.SelectedIndex = 0;
        }

        //获取日志数据
        public void GetLog()
        {
            int line = int.Parse(comboBox1.Text);
            //获取日志的后line行记录
            richTextBox1.Text = FileOperation.ReadLastLine(line);
            richTextBox1.SelectionStart = richTextBox1.TextLength;
            richTextBox1.ReadOnly = true;
        }

        //当有新日志插入时 让滚动条自动到底部
        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {
            richTextBox1.SelectionStart = richTextBox1.TextLength;
        }

        //打开端口设置窗口
        private void button1_Click(object sender, EventArgs e)
        {
            ShowForm(new OpenPort());
        }

        //打开新窗口   并给新窗口的事件添加多播
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
            GetLog();
        }

    }

}




