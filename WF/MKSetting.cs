using DisplayBLL;
using Model;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace WF
{
    /// <summary>
    /// 通信模块信息设置
    /// </summary>
    public partial class MKSetting : BaseForm
    {

        MKBLL bll = new MKBLL();
        public static string MKNO;

        public MKSetting()
        {
            InitializeComponent();
            //当添加一个模块后 刷新模块列表
            AddModule.ReLoadModules += InitialMkInfo;
        }

        public class MOduleVM : ModuleModel
        {
            public int order { set; get; }
        }

        private void MKSetting_Load(object sender, EventArgs e)
        {

            //初始化模块信息
            InitialMkInfo();

            //让ReLoadTdInfo事件的行为是重新加载通道信息
            AddTd.ReLoadTdInfo += InitialTdInfo;

        }

        //获取通信模块信息 
        public void InitialMkInfo()
        {
            ShowMkInfo();

            //获取通信模块的数据
            List<MOduleVM> models = new List<MOduleVM>();
            int order = 0;
            var modules = bll.GetModuleModels();
            if (models == null)
                return;

            foreach (var item in modules)
            {
                MOduleVM model = new MOduleVM()
                {
                    order = ++order,
                    mkno = item.mkno,
                    port = item.port
                };
                models.Add(model);
            }
            dataGridView1.DataSource = models;

            dataGridView1.Columns[0].HeaderText = "序号";
            dataGridView1.Columns[0].ReadOnly = true;
            dataGridView1.Columns[1].HeaderText = "模块编号";
            dataGridView1.Columns[1].ReadOnly = true;
            dataGridView1.Columns[2].HeaderText = "端口号";
        }

        //获取某个通信模块的通道信息
        public void InitialTdInfo(string mkno)
        {
            ShowTdInfo(mkno);
            List<TDVM> models = bll.GetTds(mkno);
            if (models == null)
                return;

            dataGridView2.DataSource = models;

            dataGridView2.Columns[0].HeaderText = "通道号";
            dataGridView2.Columns[1].HeaderText = "传感器编号";
            dataGridView2.Columns[2].HeaderText = "传感器类型";
            dataGridView2.Columns[3].HeaderText = "点名";
            dataGridView2.Columns[4].Visible = false;
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        //显示模块信息
        public void ShowMkInfo()
        {
            dataGridView1.Visible = true;
            button1.Visible = true;
            button2.Visible = true;
            button4.Visible = true;

            dataGridView2.Visible = false;
            button3.Visible = false;
            button5.Visible = false;
            button6.Visible = false;

            label1.Text = "通信模块";

        }

        //显示通道信息
        public void ShowTdInfo(string mkno)
        {
            dataGridView1.Visible = false;
            button1.Visible = false;
            button2.Visible = false;
            button4.Visible = false;

            dataGridView2.Visible = true;
            button3.Visible = true;
            button5.Visible = true;
            button6.Visible = true;
            label1.Text = "模块 " + mkno + " 的通道";
        }

        //查看某个通信模块的通道信息
        private void button4_Click(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentCell == null)
            {
                Show("请选择一个通信模块");
                return;
            }
            string mkno = GetMKno(dataGridView1.CurrentCell.RowIndex);
            MKNO = mkno;
            InitialTdInfo(mkno);
        }

        //获取当前行的模块编号
        public string GetMKno(int rowIndex)
        {
            return dataGridView1.Rows[rowIndex].Cells[1].Value.ToString();
        }

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
        }

        //添加通道
        private void button3_Click(object sender, EventArgs e)
        {
            AddTd form = new AddTd(MKNO);
            form.ShowDialog();
        }

        //修改通道信息
        private void button5_Click(object sender, EventArgs e)
        {
            //获取当前的数据
            if (dataGridView1.CurrentCell == null)
            {
                Show("请选择一个通信模块");
                return;
            }
            int rowIndex = dataGridView2.CurrentCell.RowIndex;
            string tdno = dataGridView2.Rows[rowIndex].Cells[0].Value.ToString();
            string deviceid = dataGridView2.Rows[rowIndex].Cells[1].Value.ToString();
            string type = dataGridView2.Rows[rowIndex].Cells[2].Value.ToString();
            string pointName = dataGridView2.Rows[rowIndex].Cells[3].Value.ToString();

            TDVM model = new TDVM()
            {
                mkno = MKNO,
                tdno = tdno,
                deviceId = deviceid,
                type = type,
                pointName = pointName
            };
            AddTd form = new AddTd(model);
            form.ShowDialog();
        }

        private void MKSetting_FormClosing(object sender, FormClosingEventArgs ags)
        {
            //显示模块信息
            if (dataGridView1.Visible == false)
            {
                ags.Cancel = true;
                ShowMkInfo();
            }

        }

        //返回  显示模块信息
        private void button6_Click(object sender, EventArgs e)
        {
            ShowMkInfo();
        }

        //添加模块
        private void button1_Click(object sender, EventArgs e)
        {
            AddModule form = new AddModule();
            form.ShowDialog();
        }
    }
}


