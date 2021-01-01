using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace boyut.SayimDataAccess
{
    public class Connection
    {
        private OracleConnection connection;
        public OracleConnection Conn()
        {
            connection = new OracleConnection();
            //connection.ConnectionString = GetConnectionString(2008);
            //connection.ConnectionString = "User Id = BOYUT_2008;Password=123;Data Source=192.168.0.10:1521/boyutdb2019";
            connection.ConnectionString = "User Id = BOYUT_2008;Password=SQL123;Data Source=10.0.1.67:1521/seldbrdg";
            connection.Open();
            return connection;
        }

        //private string GetConnectionString(int AKTIFYIL)
        //{
        //    return ConfigurationManager.AppSettings["connstr"] +
        //            Boyut.Libraries.Cryptography.Symmetric.Rijndael.Decrypt(ConfigurationManager.AppSettings["connPwd"]) +
        //            ConfigurationManager.AppSettings["connstrUserId"] + "_2008;";
        //}

        public void Close()
        {
            connection.Close();
        }
    }
}
