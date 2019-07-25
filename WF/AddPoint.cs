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
    public partial class AddPoint : BaseForm
    {
        PointSetting last;
        public AddPoint(PointSetting _last)
        {
            InitializeComponent();
            last = _last;
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }


        //添加测点
        private void button1_Click(object sender, EventArgs e)
        {
            string pointName = textBox1.Text;
            if(pointName == "")
            {
                Show("请输入点名");
                return;
            }

            PointInfoBLL bll = new PointInfoBLL();
            if(bll.PointIsExist(pointName))
            {
                Show("点名" + pointName + "已存在");
                textBox1.Text = "";
                return;
            }

            PointInfo model = new PointInfo()
            {
                pointName = pointName
            };

            if(bll.AddPoint(model))
            {
                textBox1.Text = "";
                Show("添加点名" + pointName + "成功");
            }
            else
                Show("添加点名" + pointName + "失败");

        }

        //关闭
        private void button2_Click(object sender, EventArgs e)
        {
            CloseForm();
        }

        void CloseForm()
        {
            this.Close();
        }

        private void AddPoint_FormClosed(object sender, FormClosedEventArgs e)
        {
            //刷新上一个窗口的数据
            last.Initial();
        }

        private void AddPoint_Load(object sender, EventArgs e)
        {

        }
    }
}
