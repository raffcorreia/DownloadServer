using System;
using System.Data;
using System.Configuration;
using MySql.Data.MySqlClient;

namespace DownloadServer
{
    public static class DataBase
    {
        private static string connectionString = ConfigurationManager.ConnectionStrings["MySqlConnectionString"].ConnectionString;
        private static MySqlCommand cmd;
        private static MySqlConnection con;

        private static bool openConnection() 
        {
            try
            {
                con = new MySqlConnection(connectionString);
                cmd = con.CreateCommand();
                con.Open();

                return true;
            }
            catch (Exception)
            {
                cmd = null;
                con = null;
                return false;
            }
        }

        private static bool closeConnection()
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

        public static int ExecuteNonQuery(string sql)
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

        public static object ExecuteScalar(string sql)
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

        public static int ExecuteScalarInt(string sql)
        {
            int ret;            

            if (openConnection())
            {
                try
                {
                    cmd.CommandText = sql;
                    ret = int.Parse(cmd.ExecuteScalar().ToString()); 
                    return ret;
                }
                catch (Exception)
                {

                }
            }
            closeConnection();
            return 0;
        }


        public static DataSet ExecuteDataSet(string sql)
        {
            DataSet ret =null;
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
