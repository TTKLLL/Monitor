using DAL;
using Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using Tool;

namespace DisplayBLL
{
    /// <summary>
    /// 处理传感器相关的BLL
    /// </summary>
    public class DeviceBLL
    {
        MKBLL mkBll = new MKBLL();
                //根据类别获取传感器数据
        public List<DeviceVM> GetDevices(string dataType = "")
        {
            try
            {
                string firstSql = "select distinct device.*, mkno, pointName from device left join td on device.deviceId = td.deviceId";
                DataTable dt = SqlHelper.GetTable(firstSql);
                List<DeviceVM> devices = new List<DeviceVM>();
                if (dt.Rows.Count == 0)
                    return null;
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    DeviceVM model = new DeviceVM()
                    {
                        deviceId = dt.Rows[i]["deviceId"].ToString(),
                        deviceInfo = dt.Rows[i]["deviceInfo"].ToString(),
                        company = dt.Rows[i]["company"].ToString(),
                        type = dt.Rows[i]["type"].ToString(),
                        mkno = dt.Rows[i]["mkno"].ToString(),
                        pointName = dt.Rows[i]["pointName"].ToString()
                    };

                    StringBuilder builder = new StringBuilder();
                    //获取该传感器的通道列表
                    string sql = string.Format("select  tdno from device left join td  on device.deviceId = td.deviceId " +
                        " where device.deviceId = '{0}' order by tdno", model.deviceId);
                    DataTable theDt = SqlHelper.GetTable(sql);
                    if (theDt.Rows.Count == 0)
                        builder.Append("");
                    else
                    {
                        for (int j = 0; j < theDt.Rows.Count; j++)
                            builder.Append(theDt.Rows[j]["tdno"].ToString() + " ");
                    }
                    model.tdnos = builder.ToString();

                    //获取项目编号
                    model.xmno = GetXmnoByMkno(model.mkno).ToString();
                    //通过模块号得到端口号
                    model.port = mkBll.GetPortBymkno(model.mkno);

                    devices.Add(model);
                }

                return devices;
            }
            catch (Exception ex)
            {
                FileOperation.WriteAppenFile("获取传感器设备信息失败");
                throw ex;
            }
        }

        //添加传感器
        public bool AddDevice(Device model)
        {
            try
            {
                string sql = string.Format("insert into device(deviceid, type, company, deviceInfo) values('{0}', '{1}', '{2}', '{3}')",
                model.deviceId, model.type, model.company, model.deviceInfo);
                return SqlHelper.ExecuteNoQuery(sql);
            }
            catch (Exception ex)
            {
                FileOperation.WriteAppenFile(string.Format("添加编号为{0}的{1}传感器出错 {2}", model.deviceId, model.type, ex.Message));
                throw ex;
            }
        }

        //判断传感器时候已经接入到通信模块
        public TD IsDeviceOnMk(Device model)
        {
            try
            {
                string sql = string.Format("select * from td where deviceId = '{0}'", model.deviceId);
                DataTable dt = SqlHelper.GetTable(sql);
                if (dt.Rows.Count == 0)
                    return null;

                TD tdModel = new TD()
                {
                    mkno = dt.Rows[0]["mkno"].ToString(),
                    tdno = dt.Rows[0]["tdno"].ToString()
                };
                return tdModel;
            }
            catch (Exception ex)
            {
                FileOperation.WriteAppenFile(string.Format("判断编号为{0}的{1}传感器是否已接入出错 {1}",
                    model.deviceId, model.type, ex.Message));
                throw ex;
            }
        }

        //传感器
        public bool DeleteDevice(Device model)
        {
            try
            {
                string sql = string.Format("delete from device where deviceId = '{0}'", model.deviceId);
                return SqlHelper.ExecuteNoQuery(sql);
            }
            catch (Exception ex)
            {
                FileOperation.WriteAppenFile(string.Format("删除编号为{0}的{1}传感器出错 {1}",
                    model.deviceId, model.type, ex.Message));
                throw ex;
            }
        }

        //修改传感器数据
        public bool UpdateDevice(Device model)
        {
            try
            {
                string sql = string.Format("update device set deviceInfo = '{0}', company = '{1}', type = '{2}' where deviceId = '{3}'",
                    model.deviceInfo, model.company, model.type, model.deviceId);
                return SqlHelper.ExecuteNoQuery(sql);
            }
            catch (Exception ex)
            {
                FileOperation.WriteAppenFile(string.Format("修改编号为{0}的{1}传感器出错 {1}",
                    model.deviceId, model.type, ex.Message));
                throw ex;
            }
        }

        //判断传感器是否已存在
        public bool DeviceIsExist(string deviceId)
        {
            try
            {
                string sql = string.Format("select deviceId from device where deviceId = '{0}'", deviceId);
                return SqlHelper.GetTable(sql).Rows.Count > 0 ? true : false;
            }
            catch (Exception ex)
            {
                FileOperation.WriteAppenFile(string.Format("判断编号为{0}的传感器是否存在出错", deviceId));
                throw ex;
            }
        }

        //获取未接入的传感器
        public List<Device> GetUnUsedDevice()
        {
            try
            {
                string sql = string.Format("select * from device where deviceId not in (select deviceId from td)");
                return SqlHelper.GetList<Device>(sql);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //根据传感器id获取传传感器信息
        public Device GetDeviceById(string deviceId)
        {
            try
            {
                string sql = string.Format("select * from device where deviceId = '{0}'", deviceId);
                return SqlHelper.GetList<Device>(sql).First();
            }
            catch (Exception ex)
            {
                FileOperation.WriteAppenFile("获取编号为" + deviceId + "的传感器出错");
                throw ex;
            }
        }

        //通过模块号得到项目编号
        public int GetXmnoByMkno(string mkno)
        {
            try
            {
                string sql = string.Format("select xmno from xmport, module where xmport.port = module.port and mkno = '{0}'", mkno);
                return int.Parse(SqlHelper.GetTable(sql).Rows[0]["xmno"].ToString());

            }
            catch (Exception ex)
            {
                FileOperation.WriteAppenFile("获取模块号为" + mkno + "对用的项目编号出错 " + ex.Message);
                throw ex;
            }
        }
    }
}
