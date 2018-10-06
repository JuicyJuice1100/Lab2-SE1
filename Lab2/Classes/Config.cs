using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab2
{
    public class Config
    {
        public SqlConnection conn;

        public Config()
        {
            conn = new SqlConnection
            {
                ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["TicTacToe"].ConnectionString
            };
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

    }
}
