using DisplayBLL;
using System;
using System.Windows.Forms;

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

            dataGridView1.Columns[0].DataPropertyName = "deviceId";
            dataGridView1.Columns[0].HeaderText = "传感器编号";
            dataGridView1.Columns[1].DataPropertyName = "type";
            dataGridView1.Columns[1].HeaderText = "传感器类型";
            dataGridView1.Columns[2].DataPropertyName = "company";
            dataGridView1.Columns[2].HeaderText = "生产厂商";
            dataGridView1.Columns[3].DataPropertyName = "deviceinfo";
            dataGridView1.Columns[3].HeaderText = "传感器信息";

            dataGridView1.Columns[4].DataPropertyName = "mkno";
            dataGridView1.Columns[4].HeaderText = "模块编号";
            dataGridView1.Columns[5].DataPropertyName = "tdnos";
            dataGridView1.Columns[5].HeaderText = "通道号";
            dataGridView1.Columns[6].DataPropertyName = "pointName";
            dataGridView1.Columns[6].HeaderText = "点名";
            dataGridView1.Columns[7].DataPropertyName = "port";
            dataGridView1.Columns[7].HeaderText = "端口";
            dataGridView1.Columns[8].DataPropertyName = "xmno";
            dataGridView1.Columns[8].HeaderText = "项目编号";

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
