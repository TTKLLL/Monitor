using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Model;
using DisplayBLL;

namespace WF
{
    public partial class DeviceSetting : BaseForm
    {
        private static DeviceBLL deviceBll = new DeviceBLL(); 
        public DeviceSetting()
        {
            InitializeComponent();
        }

        public void Initial()
        {
           dataGridView1.DataSource = deviceBll.GetDevices();
            dataGridView1.Columns[0].HeaderText = "传感器编号";
            dataGridView1.Columns[0].ReadOnly = true;

            dataGridView1.Columns[1].HeaderText = "传感器类型";
            dataGridView1.Columns[2].HeaderText = "生产厂商";
            dataGridView1.Columns[3].HeaderText = "传感器信息";
        }

        private void DeviceSetting_Load(object sender, EventArgs e)
        {
            Initial();
        }

        //添加传感器
        private void button1_Click(object sender, EventArgs e)
        {
            OpenAddForm();
        }

        private void OpenAddForm()
        {
            AddDevice form = new AddDevice(this);
            form.ShowDialog();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        //删除传感器
        private void button2_Click(object sender, EventArgs e)
        {

        }
    }
}
