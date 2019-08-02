using DisplayBLL;
using Model;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace WF
{
    /// <summary>
    /// 设置测点信息
    /// </summary>
    public partial class PointSetting : BaseForm
    {
        private static PointInfoBLL pointBll = new PointInfoBLL();
        public PointSetting()
        {
            InitializeComponent();
        }

        public class PointVM : PointInfo
        {
            //序号
            public int order { set; get; }
        }


        //初始化数据
        public void Initial()
        {
            //  List<PointInfo> models = pointBll.GetPointInfos();
            List<PointVM> models = new List<PointVM>();
            int i = 0;
            var pointInfos = pointBll.GetPointInfos();

            if (pointInfos == null)
                return;

            foreach (var item in pointInfos)
            {
                PointVM model = new PointVM()
                {
                    order = ++i,
                    pointName = item.pointName
                };
                models.Add(model);
            }

            dataGridView1.DataSource = models;
            dataGridView1.Columns[0].HeaderText = "序号";
            dataGridView1.Columns[0].ReadOnly = true;
            dataGridView1.Columns[1].HeaderText = "点名";
            dataGridView1.Columns[1].ReadOnly = true;
        }

        private void PointSetting_Load(object sender, EventArgs e)
        {
            Initial();
        }

        //添加点名
        private void button1_Click(object sender, EventArgs e)
        {
            OpenAddForm();
        }

        void OpenAddForm()
        {
            AddPoint point = new AddPoint(this);
            point.ShowDialog();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        //获取列表中某一行的点名
        string GetPort(int rowIndex)
        {
            return dataGridView1.Rows[rowIndex].Cells[1].Value.ToString();
        }

        //删除测点
        private void button2_Click(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentCell == null)
            {
                Show("请选择要删除的测点");
                return;
            }
            string pointName = GetPort(dataGridView1.CurrentCell.RowIndex);
            if (Ask("确定删除测点 " + pointName) != DialogResult.OK)
                return;

            PointInfoBLL bll = new PointInfoBLL();
            PointInfo model = new PointInfo()
            {
                pointName = pointName
            };

            if(bll.PointBeUsed(model))
            {
                Show("测点正在使用，无法删除");
                return;
            }

            if (bll.DeletePoint(model))
            {
                Initial();
                Show("删除成功");
            }
            else
                Show("删除点名 " + pointName + "失败");
        }
    }
}

