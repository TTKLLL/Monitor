using DAL;
using Model;
using System;
using System.Collections.Generic;
using System.Data;
using Tool;

namespace DisplayBLL
{
    /// <summary>
    /// 处理传感器相关的BLL
    /// </summary>
    public class DeviceBLL
    {
        //根据类别获取传感器数据
        public List<Device> GetDevices(string dataType = "")
        {
            try
            {
                string sql = string.Format("select * from device where type like '%{0}%' order by deviceId ", dataType);
                return SqlHelper.GetList<Device>(sql);
            }
            catch (Exception ex)
            {
                FileOperation.WriteAppenFile("获取传感器数据失败");
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
           catch(Exception ex)
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
            catch(Exception ex)
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
            catch(Exception ex)
            {
                FileOperation.WriteAppenFile(string.Format("判断编号为{0}的传感器是否存在出错", deviceId));
                throw ex;
            }
        }
    }
}
