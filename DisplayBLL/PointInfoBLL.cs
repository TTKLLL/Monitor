using DAL;
using Model;
using System;
using System.Collections.Generic;
using System.Data;
using Tool;

namespace DisplayBLL
{
    /// <summary>
    /// 测点相关操作
    /// </summary>
    public class PointInfoBLL
    {
        //获取测点信息
        public List<PointInfo> GetPointInfos()
        {
            string sql = "SELECT * FROM `pointinfo` order by pointName";

            List<PointInfo> models = new List<PointInfo>();
            DataTable dt = SqlHelper.GetTable(sql);
            if (dt.Rows.Count == 0)
                return null;
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                PointInfo model = new PointInfo()
                {
                    pointName = dt.Rows[i]["pointName"].ToString(),
                    //tdno = dt.Rows[i]["tdno"].ToString(),
                    //t0 = dt.Rows[i]["t0"].ToString(),
                    //k0 = dt.Rows[i]["k0"].ToString()
                };
                models.Add(model);
            }
            return models;
            //  return SqlHelper.GetList<PointInfo>(sql);
        }

        string FormatValue(string value)
        {
            return value == "" ? "0" : value;
        }

        //添加测点
        public bool AddPoint(PointInfo model)
        {
            try
            {
                //string sql = string.Format("INSERT INTO pointinfo(pointName, tdno, t0, k0 ) VALUES('{0}', '{1}', {2}, {3})",
                //model.pointName, model.tdno, FormatValue(model.t0), FormatValue(model.k0));
                string sql = string.Format("INSERT INTO pointinfo(pointName) values('{0}')", model.pointName);

                return SqlHelper.ExecuteNoQuery(sql);
            }
            catch (Exception ex)
            {
                FileOperation.WriteAppenFile(string.Format("添加点名为{0}的测点信息出错，{1}", model.pointName, ex.Message));
                throw ex;
            }
        }

        ////修改测点
        //public bool UpdatePoint(PointInfo model)
        //{
        //    try
        //    {
        //        string sql = string.Format("update pointinfo set tdno = '{0}', t0 = '{1}', k0 = '{2}' where pointName = '{3}'",
        //    model.tdno, FormatValue(model.t0), FormatValue(model.k0), model.pointName);

        //        bool res = SqlHelper.ExecuteNoQuery(sql);
        //        if (res == false)
        //            FileOperation.WriteAppenFile(string.Format("更新点名为{0}的测点信息出错}", model.pointName));
        //        return res;
        //    }
        //    catch (Exception ex)
        //    {
        //        FileOperation.WriteAppenFile(string.Format("更新点名为{0}的测点信息出错，{1}", model.pointName, ex.Message));
        //        throw ex;
        //    }
        //}


        /// <summary>
        /// 判断点名是否已经被使用
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool PointBeUsed(PointInfo model)
        {
            try
            {
                string sql = string.Format("select tdno from td where pointName = '{0}' ", model.pointName);
                return SqlHelper.GetTable(sql).Rows.Count > 0 ? true : false;
            }
            catch (Exception ex)
            {
                FileOperation.WriteAppenFile("判断点名为" + model.pointName + "的测点是否使用出错 " + ex.Message);
                throw ex;
            }
        }

        //删除测点
        public bool DeletePoint(PointInfo model)
        {  
            try
            {
                string sql = string.Format("delete from pointinfo where pointName = '{0}' ", model.pointName);
                bool res = SqlHelper.ExecuteNoQuery(sql);
                if (res == false)
                {
                    FileOperation.WriteAppenFile(string.Format("删除点名为{0}的测点信息失败", model.pointName));
                }
                return res;
            }
            catch (Exception ex)
            {
                FileOperation.WriteAppenFile(string.Format("删除点名为{0}的测点信息出错, {1}", model.pointName, ex.Message));
                throw ex;
            }
        }

        //判断测点是否存在
        public bool PointIsExist(string pointName)
        {
            try
            {
                string sql = string.Format("select pointName from pointInfo where pointName = '{0}'", pointName);
                return SqlHelper.GetTable(sql).Rows.Count > 0 ? true : false;
            }
            catch (Exception ex)
            {
                FileOperation.WriteAppenFile(string.Format("判断点名{0} 是否存在出错 {1}", pointName, ex.Message));
                throw ex;
            }

        }
    }
}
