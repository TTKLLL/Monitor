using DAL;
using Model;
using System;
using System.Collections.Generic;
using System.Data.Odbc;

namespace DisplayBLL
{
    /// <summary>
    /// 菜单BLL
    /// </summary>
    public class MenueBLL
    {
        /// <summary>
        /// 获取一级菜单
        /// </summary>
        /// <returns></returns>
        public List<MenueModel> GetFirstMenue()
        {
            try
            {
                string sql = "select * from menue where parentId = 0";
                List<MenueModel> models = SqlHelper.GetList<MenueModel>(sql);
                return models;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 判断某个菜单是否有子菜单
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool HasChild(object id)
        {
            try
            {
                string sql = string.Format(" select count(1) from menue where parentId = {0} ", int.Parse(id.ToString()));
                return SqlHelper.GetTable(sql).Rows.Count > 0 ? true : false;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 获取子菜单
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public List<MenueModel> GetChild(object id)
        {
            try
            {
                string sql = string.Format(" select * from menue where parentId = {0} ", int.Parse(id.ToString()));
                List<MenueModel> models = SqlHelper.GetList<MenueModel>(sql);
                return models;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 添加菜单
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool AddNode(MenueModel model)
        {
            try
            {
                string sql = string.Format(" insert into  Menue(name, parentId) values('{0}', {1}) ",
                model.name, model.parentId);
                return SqlHelper.ExecuteNoQuery(sql);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 删除节点
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool DeleteNode(object id)
        {
            OdbcConnection conn = DbContext.GetConn();
            conn.Open();
            OdbcTransaction trans = conn.BeginTransaction();
            try
            {
                foreach (var item in GetNodeIds(int.Parse(id.ToString())))
                {
                    string sql = string.Format("delete from menue where id = {0}", item);
                    OdbcCommand comm = new OdbcCommand(sql, conn);
                    comm.Transaction = trans;
                    if (comm.ExecuteNonQuery() <= 0)
                    {
                        trans.Rollback();
                        return false;
                    }
                }
                trans.Commit();
                return true;
            }
            catch (Exception ex)
            {
                trans.Rollback();
                throw ex;
            }
            finally
            {
                trans.Dispose();
                conn.Close();
            }
        }

        /// <summary>
        /// 递归获取要删除的节点的id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public List<int> GetNodeIds(int id)
        {
            try
            {
                List<int> ids = new List<int>();
                ids.Add(id);
                if (HasChild(id))
                {
                    foreach (var item in GetChild(id))
                    {
                        ids.AddRange(GetNodeIds(item.id));
                    }
                }
                return ids;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
