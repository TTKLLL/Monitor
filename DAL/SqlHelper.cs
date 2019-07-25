using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Odbc;
using Tool;

namespace DAL
{
    public class SqlHelper
    {
        //执行sql获取泛型集合
        public static List<T> GetList<T>(string sql, int xmno = 0)
        {
            try
            {
                OdbcConnection conn =  DbContext.GetConn(xmno);
                conn.Open();
                OdbcDataAdapter oda = new OdbcDataAdapter(sql, conn);
                OdbcCommand com = new OdbcCommand(sql, conn);
                var reader = com.ExecuteReader();
                Type type = typeof(T);

                List<T> models = new List<T>();
                while (reader.Read())
                {
                    T t = (T)Activator.CreateInstance(typeof(T));
                    //遍历T中的属性
                    foreach (var prop in type.GetProperties())
                        prop.SetValue(t, reader[prop.Name] is DBNull ? null : reader[prop.Name], null);
                    models.Add(t);
                }
                conn.Close();
                return models;
            }
            catch (Exception ex)
            {
                FileOperation.WriteAppenFile(ex.Message);
                
                throw ex;
            }
            finally
            {
                
            }
        }

        public static DataTable GetTable(string sql, int xmno = 0)
        {
            OdbcConnection conn = DbContext.GetConn(xmno);
            conn.Open();
            try
            {
                using (OdbcDataAdapter oda = new OdbcDataAdapter(sql, conn))
                {
                    DataTable dt = new DataTable();
                    oda.Fill(dt);
                    oda.Dispose();
                    return dt;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                conn.Close();
            }
        }

        public static bool ExecuteNoQuery(string sql, int xmno = 0)
        {
            OdbcConnection conn = DbContext.GetConn(xmno);
            conn.Open();
            try
            {
                using (OdbcCommand com = new OdbcCommand(sql, conn))
                {
                    return com.ExecuteNonQuery() > 0 ? true : false;
                }
            }
            catch (Exception)
            {
                return false;
            }
            finally
            {
                conn.Close();
            }
        }
    }

}
