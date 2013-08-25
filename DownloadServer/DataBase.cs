using System;
using System.Data;
using System.Configuration;
using MySql.Data.MySqlClient;

namespace DownloadServer
{
    public class DataBase
    {
        private string connectionString = ConfigurationManager.ConnectionStrings["MySqlConnectionString"].ConnectionString;
        private MySqlCommand cmd;
        private MySqlConnection con;

        private bool openConnection() 
        {
            try
            {
                if (con == null)
                {
                    con = new MySqlConnection(connectionString);
                }
                if (cmd == null)
                {
                    cmd = con.CreateCommand();
                }
                if (con.State != ConnectionState.Open)
                {
                    con.Open();
                }
                return true;
            }
            catch (Exception)
            {
                cmd = null;
                con = null;
                return false;
            }
        }

        private bool closeConnection()
        {
            try
            {
                if (con.State != System.Data.ConnectionState.Closed)
                {
                    con.Close();
                }
                con.Dispose();
            }
            catch (Exception)
            {
                cmd = null;
                con = null;
                return false;
            }
            cmd = null;
            con = null;
            return true;
        }

        public int ExecuteNonQuery(string sql)
        {
            int ret;

            if (openConnection())
            {
                try
                {
                    cmd.CommandText = sql;
                    ret = cmd.ExecuteNonQuery();
                }
                catch (Exception)
                {
                    ret = -2;
                }
            }
            else
            {
                ret = -1;
            }
            closeConnection();
            return ret;
        }

        public object ExecuteScalar(string sql)
        {
            object ret = null;

            if (openConnection())
            {
                try
                {
                    cmd.CommandText = sql;
                    ret = cmd.ExecuteScalar();
                }
                catch (Exception)
                {
                    
                }
            }
            closeConnection();
            return ret;
        }

        public int ExecuteScalarInt(string sql)
        {
            int ret = -1;

            if (openConnection())
            {
                try
                {
                    cmd.CommandText = sql;
                    int.TryParse(cmd.ExecuteScalar().ToString(), out ret);
                }
                catch (Exception)
                {
                    
                }
            }
            closeConnection();
            return ret;
        }


        public DataSet ExecuteDataSet(string sql)
        {
            DataSet ret = null;
            MySqlDataAdapter da;
            
            if (openConnection())
            {
                try
                {
                    da = new MySqlDataAdapter(sql, con);
                    ret = new DataSet();
                    if (da.Fill(ret) <= 0)
                    {
                        ret = null;
                    }
                }
                catch (Exception)
                {
                    da = null;
                }
            }
            closeConnection();
            return ret;
        }
    }
}
