using DisplayBLL;
using Model;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Forms;
using Tcp;
using Tool;

namespace WF
{
    public partial class OpenPort : BaseForm
    {
        public static readonly string IP;

        //初始化ip
        static OpenPort()
        {
            TcpBLL bll = new TcpBLL();
            IP = bll.GetIp();
        }

        public OpenPort()
        {
            InitializeComponent();
        }

        //用于在DataGridview上显示的数据模型
        public class PortVM
        {
            public string port { set; get; }
            public string xmno { set; get; }
        }

        private void OpenPort_Load(object sender, EventArgs e)
        {
            Initial();
        }

        //初始化数据
        public  void Initial()
        {
            //绑定端口信息
            GetPorts();
            //检查端口的开启状态
            TestPort();
        }

        //获取所有端口信息
        public void GetPorts()
        {
            XmPortBLL bll = new XmPortBLL();
            List<PortVM> models = new List<PortVM>();
            foreach (var item in bll.GetPorts())
            {
                PortVM model = new PortVM()
                {
                    port = item.port.ToString(),
                    xmno = item.xmno.ToString()
                };
                models.Add(model);
            }
            dataGridView1.DataSource = models;

            dataGridView1.Columns[0].HeaderText = "端口状态";
            dataGridView1.Columns[1].HeaderText = "端口号";
            dataGridView1.Columns[2].HeaderText = "项目编号";

            dataGridView1.Columns[1].ReadOnly = true;

        }

        ////点击复选框开启端口
        //private void checkBox_CheckedChanged(object sender, EventArgs e)
        //{
        //    CheckBox box = (CheckBox)sender;
        //    CheckState state = box.CheckState;
        //    int port = int.Parse(box.Text);
        //    try
        //    {
        //        if (TcpServer.StartListenPoret(IP, port))
        //        { 
        //            MessageBox.Show("成功开启端口" + port.ToString());
        //          //  Send("成功开启端口" + port.ToString());
        //        }
                    
        //    }
        //    catch (Exception ex)
        //    {
        //        FileOperation.WriteAppenFile(string.Format("开启端口{0}出错，{1}", port.ToString(), ex.Message));
        //        MessageBox.Show("开启端口" + port.ToString() + "失败");
        //        box.CheckState = CheckState.Unchecked;
        //    }
        //}

        //修改表格内容
        private void dataGridView1_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            //获取当前列
            int columnIndex = dataGridView1.CurrentCell.ColumnIndex;
            if (columnIndex != 2)
                return;

            //获取当前行
            int rowIndex = dataGridView1.CurrentCell.RowIndex;

            string port = GetPort(rowIndex);
            string xmno = GetXmno(rowIndex);

            if (xmno == "")
            {
                Show("请输入项目编号");
                return;
            }

            XmPortBLL bll = new XmPortBLL();
            if (bll.GetXmno(int.Parse(port)) == int.Parse(xmno))
                return;

            XmPortModel model = new XmPortModel()
            {
                port = int.Parse(port),
                xmno = int.Parse(xmno)
            };
            if (bll.ChangeXmno(model))
            {
                Show("修改成功");
            }
            else
            {
                Show("修改失败");
            }
        }

        //表格的内容的修改事件  开启端口
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            //获取当前行
            int rowIndex = dataGridView1.CurrentCell.RowIndex;
            //获取当前列
            int columnIndex = dataGridView1.CurrentCell.ColumnIndex;
            if (columnIndex != 0)
                return;
            DataGridViewCheckBoxCell ck = (DataGridViewCheckBoxCell)dataGridView1.Rows[rowIndex].Cells[0];

            //开启端口
            if (GetCheck(rowIndex) == false)
            {
                int port = int.Parse(GetPort(rowIndex));
                try
                {
                    if (TcpServer.StartListenPoret(IP, port))
                    {
                        Show("成功开启" + port.ToString() + "端口");
                        SetCheck(rowIndex, true);
                    }
                    else
                        Show("开启" + port.ToString() + "端口失败");
                }
                catch (Exception ex)
                {
                    FileOperation.WriteAppenFile(string.Format("开启端口{0} 出错{1}", port.ToString(), ex.Message));
                }
            }
            else
                SetCheck(rowIndex, true);
        }

        //获取某一行的项目编号
        string GetXmno(int rowIndex)
        {
            return dataGridView1.Rows[rowIndex].Cells[2].Value.ToString();
        }

        //获取列表中某一行的端口
        string GetPort(int rowIndex)
        {
            return dataGridView1.Rows[rowIndex].Cells[1].Value.ToString();
        }

        //获取checkbox状态
        bool GetCheck(int rowIndex)
        {
            DataGridViewCheckBoxCell ck = (DataGridViewCheckBoxCell)dataGridView1.Rows[rowIndex].Cells[0];
            return bool.Parse(ck.FormattedValue.ToString());
        }

        //设置cehckbox的值
        public void SetCheck(int rowIndex, bool value)
        {
            dataGridView1.Rows[rowIndex].Cells[0].Value = value;
        }

        //刷新端口状态
        private void button1_Click(object sender, EventArgs e)
        {
            TestPort();
        }

        //检测端口的开启状态
        void TestPort()
        {
            try
            {
                button1.Enabled = false;
                dataGridView1.Enabled = false;

                label1.Text = "正在获取端口开启状态";
                XmPortBLL bll = new XmPortBLL();
                TcpBLL tcpbll = new TcpBLL();
                //获取端口绑定的ip地址
                string ip = tcpbll.GetIp();

                List<Task> taskList = new List<Task>();
                TaskFactory factory = new TaskFactory();

                for (int i = 0; i < dataGridView1.Rows.Count; i++)
                {
                    int j = i;
                    taskList.Add(factory.StartNew(() =>
                    {
                        int port = int.Parse(GetPort(j));
                        //测试端口的连接
                        bool res = bll.TestPort(ip, port);
                        //设置checkbox的选中状态
                        SetCheck(j, res);
                    }));
                }

                ////用于等待其他几个检测端口的线程
                taskList.Add(factory.ContinueWhenAll(taskList.ToArray(), arr =>
                {
                    // Show("刷新端口成功");
                    button1.Enabled = true;
                    dataGridView1.Enabled = true;
                    label1.Text = "";
                }));
            }
            catch (Exception ex)
            {
                Show("获取端口状态失败");
                FileOperation.WriteAppenFile("获取端口状态失败 " + ex.Message);
            }
        }

        //添加端口
        private void button2_Click(object sender, EventArgs e)
        {
            OpenAddForm();
        }

        //打开新窗口
        void OpenAddForm()
        {
            AddPort form = new AddPort(this);
            form.ShowDialog();
        }

        //删除端口
        private void button3_Click(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentCell == null)
            {
                Show("请选择要删除的端口");
                return;
            }
            string port = GetPort(dataGridView1.CurrentCell.RowIndex);
            if (Ask("确定删除端口" + port) != DialogResult.OK)
                return;

            XmPortBLL bll = new XmPortBLL();
            if (bll.DeletePort(int.Parse(port)))
            {
                Initial();
                Show("删除成功");
                //关闭端口的tcp监听
            }
            else
                Show("删除端口" + port + "失败");
        }
    }
}
