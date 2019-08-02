using DisplayBLL;
using Model;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace WF
{
    public partial class FinalData : BaseForm
    {
        DataBLL dataBll = new DataBLL();
        public FinalData()
        {
            InitializeComponent();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void InitialData()
        {
            List<DataVM> models = dataBll.GetDataModels();
            if (models == null)
                return;

            dataGridView1.DataSource = models;

            SetHead(0, "序号");
            SetHead(1, "传感器编号");
            SetHead(2, "传感器类型");
            SetHead(3, "点名");
            SetHead(4, "模块号");
            SetHead(5, "通道号");
            SetHead(6, "时间");
            SetHead(7, "项目编号");
            SetHead(8, "端口");
            SetHead(9, "值1");
            SetHead(10, "值2");
            SetHead(11, "值3");
        }

        private void FinalData_Load(object sender, EventArgs e)
        {
            //获取监测数据
            InitialData();
        }

        public void SetHead(int index, string name)
        {
            dataGridView1.Columns[index].HeaderText = name;
        }

    }
}
