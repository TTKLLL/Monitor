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
    public partial class AddDevice : BaseForm
    {
       private DeviceSetting last;
        public AddDevice(DeviceSetting _last)
        {
            InitializeComponent();
            last = _last;
        }

        private void AddDevice_Load(object sender, EventArgs e)
        {

        }

         //关闭当前窗口是重载传感器列表
        private void AddDevice_FormClosed(Object sender, FormClosedEventArgs e)
        {
            last.Initial();
        }

        public void ClearInput()
        {
            DeviceIdText.Text = "";
            TypeText.Text = "";
            CompanyText.Text = "";
            InfoText.Text = "";
        }

        //添加传感器信息
        private void button1_Click(object sender, EventArgs e)
        {
            string deviceId = DeviceIdText.Text.Trim();
            string type = TypeText.Text.Trim();
            string company = CompanyText.Text.Trim();
            string info = InfoText.Text.Trim();

            if(deviceId == "")
            {
                Show("请输入传感器编号");
                return;
            }

            DeviceBLL bll = new DeviceBLL();
            if (bll.DeviceIsExist(deviceId))
            {
                Show(string.Format("编号为{0}的传感器已存在", deviceId));
                ClearInput();
                return;
            }

            if (type == "")
            {
                Show("请输入传感器类型");
                return;
            }

           
            Device model = new Device()
            {
                deviceId = deviceId,
                type = type,
                company = company,
                deviceInfo = info
            };
            

            if(bll.AddDevice(model))
            {
                ClearInput();
                Show(string.Format("添加编号为{0}的{1}传感器成功", model.deviceId, model.type));
            }
            else
                Show(string.Format("添加编号为{0}的{1}传感器失败", model.deviceId, model.type));
        }

        //关闭窗口
        private void button2_Click(object sender, EventArgs e)
        {
            CloseForm();
        }

        void CloseForm()
        {
            this.Close();
        }
    }
}
