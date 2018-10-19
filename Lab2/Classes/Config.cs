using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace Lab2
{
    public class Config
    {
        public MySqlConnection conn;

        public Config()
        {
            conn = new MySqlConnection("server=127.0.0.1; user=espirj96; password=x370796; database=espirj96; port=3306");
            //{
            //    ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["TicTacToe"].ConnectionString
            //};
        }

        public void DBConnect()
        {
            try
            {
                conn.Open();
            } catch (SqlException e)
            {
                Console.WriteLine(e.Message);
            }
        }

        public void DBDisconnect()
        {
            try
            {
                conn.Close();
            } catch (SqlException e)
            {
                Console.WriteLine(e.Message);
            }
        }

        public void DBTest()
        {
            try
            {
                conn.Open();
            } catch (SqlException e)
            {
                Console.WriteLine(e.Message);
            } finally
            {
                conn.Close();
            }
        }

    }
}
