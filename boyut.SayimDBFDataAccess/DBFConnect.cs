using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace boyut.SayimDBFDataAccess
{
    public class DBFConnect
    {
        public OleDbConnection conn;
        public DBFConnect()
        {
            
            string cadena = @"Provider = Microsoft.Jet.OLEDB.4.0; Data Source = k:\data; Extended Properties = dBase IV; User ID=;Password=";
            //string cadena = @"Provider = Microsoft.Jet.OLEDB.4.0; Data Source = \\192.168.0.217\dbf; Extended Properties = dBase IV; User ID=;Password=";
            OleDbConnection conn = new OleDbConnection();
            conn.ConnectionString = cadena;
            conn.Open();

            this.conn = conn;
        }
    }
}
