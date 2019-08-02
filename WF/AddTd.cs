using DisplayBLL;
using Model;
using System;
using System.Collections.Generic;

namespace WF
{
    /// <summary>
    /// 添加通道
    /// </summary>
    public partial class AddTd : BaseForm
    {
        //重新加载通道信息
        public delegate void ReLoad(string mkno);
        public static event ReLoad ReLoadTdInfo;
        //通道信息
        public static TDVM model = new TDVM();

        //修改通道信息
        public AddTd(TDVM _model)
        {
            InitializeComponent();

            //在页面上绑定数据
            button1.Visible = false;
            button3.Visible = true;
            model = _model;
            textBox1.Text = model.tdno;
            setFormText("修改通道信息");
            textBox1.Enabled = false;
        }

        //添加通道信息
        public AddTd(string _mkno)
        {
            InitializeComponent();

            //重新初始化对象
            model = new TDVM();
            model.mkno = _mkno;

            button1.Visible = true;
            button3.Visible = false;
            setFormText("添加通道信息");
        }

        //在窗口上显示模块编号
        public void InitialComboBox()
        {
            label2.Text = model.mkno;
            PointInfoBLL pointBll = new PointInfoBLL();
            int index = 0;

            foreach (var item in pointBll.GetPointInfos())
            {
                comboBox1.Items.Add(item.pointName);
                if (model.pointName != null)
                {
                    if (model.pointName == item.pointName)
                        comboBox1.SelectedIndex = index;
                }

                index++;
            }

            index = 0;
            DeviceBLL deviceBll = new DeviceBLL();
            List<Device> devices = deviceBll.GetUnUsedDevice();
            //当为更新操作时 将当前的传感器信息加入到列表中
            if (model.deviceId != null)
                devices.Add(deviceBll.GetDeviceById(model.deviceId));

            foreach (var item in devices)
            {
                comboBox2.Items.Add(item.deviceId.Trim() + " " + item.type.Trim());

                if (model.deviceId != null)
                {
                    if (model.deviceId == item.deviceId)
                        comboBox2.SelectedIndex = index;
                }
                index++;
            }
        }



        //初始化测点数据和传感器数据
        private void AddTd_Load(object sender, EventArgs e)
        {
            InitialComboBox();
        }

        //添加通道
        private void button1_Click(object sender, EventArgs e)
        {
            MKBLL bll = new MKBLL();
            if (textBox1.Text.Trim() == "")
            {
                Show("请输入通道号");
                return;
            }

            TD newModel = GetNewModel();
            if (bll.AddTd(newModel))
            {
                Show("添加通道成功");
                textBox1.Text = "";

                //重新绑定未使用的传感器
               // InitialComboBox();

                //调用行为 更新通道信息
                if (ReLoadTdInfo != null)
                    ReLoadTdInfo(model.mkno);
            }
            else
            {
                Show("添加通道失败");
            }
        }

        //判断通道是否已使用
        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            string tdno = textBox1.Text.Trim();
            MKBLL bll = new MKBLL();

            if (textBox1.Text != model.tdno)
            {
                if (bll.TdIsUsed(model.mkno, tdno))
                {
                    label6.Text = "该通道已被使用";
                    button1.Enabled = false;
                    button3.Enabled = false;
                }
                else
                {
                    label6.Text = "";
                    button1.Enabled = true;
                    button3.Enabled = true;
                }
            }
        }

        //关闭该窗口
        private void button2_Click(object sender, EventArgs e)
        {
            CloseForm();
        }

        public void CloseForm()
        {
            this.Close();
        }

        //设置窗口名称
        public void setFormText(string str)
        {
            this.Text = "通信模块设置->" + str;
        }

        //根据创建上的数据实例化对象
        public TD GetNewModel()
        {
            TD newModel = new TD()
            {
                tdno = textBox1.Text.Trim(),
                pointName = comboBox1.Text.Trim(),
                deviceId = comboBox2.Text.Split(' ')[0],
                mkno = model.mkno
            };
            return newModel;
        }

        //更新通道信息
        private void button3_Click(object sender, EventArgs e)
        {
            TD newModel = GetNewModel();
            MKBLL bll = new MKBLL();
            if (bll.UpdateTd(newModel))
            {
                if (ReLoadTdInfo != null)
                    ReLoadTdInfo(model.mkno);
                Show("更新成功");
                CloseForm();
            }
            else
            {
                Show("更新失败");
            }
        }
    }
}
