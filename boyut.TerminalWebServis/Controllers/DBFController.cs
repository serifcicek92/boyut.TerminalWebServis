using boyut.SayimDBFDataAccess;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace boyut.TerminalWebServis.Controllers
{
    public class DBFController : ApiController
    {
        private DBFConnect baglanti;
        private OleDbConnection conn;
        public IEnumerable<IlaclarDBF> Get()
        {
            conn = baglanti.conn;

            String sql = "select * from ilaclar";

            OleDbCommand komut = new OleDbCommand(sql, conn);
            OleDbDataReader dr = komut.ExecuteReader();
            string gelen_veriler = "";
            while (dr.Read())
            {
                gelen_veriler += "ilaç adı: "+dr.GetFieldValue<String>(dr.GetOrdinal("Ilacadi"))+"/n";
            }

            string a = gelen_veriler;

            //OleDbDataAdapter adapter = new OleDbDataAdapter(sql, conn);
            //DataSet ds = new DataSet();
            //adapter.Fill(ds);
            conn.Close();

            return null;
           
        }

        // GET api/values/5
        public List<IlaclarDBF> Get(int barkod)
        {
            List<IlaclarDBF> ilaclar = new List<IlaclarDBF>();

            conn = baglanti.conn;
            
            string sql = "select * from ilaclar where BARKODU=@barkod";
            OleDbCommand ilacGetir = new OleDbCommand(sql, conn);
            ilacGetir.Parameters.AddWithValue("@barkod", barkod);

            OleDbDataReader dr = ilacGetir.ExecuteReader();
            dr.Read();
            IlaclarDBF ilaclarDBF = new IlaclarDBF();
            ilaclarDBF.Barkodu = barkod.ToString();
            ilaclarDBF.IlacAdi = dr.GetFieldValue<string>(dr.GetOrdinal("ilacadi")).ToString();
            ilaclarDBF.IlacKodu = int.Parse(dr.GetFieldValue<string>(dr.GetOrdinal("ilackodubt")));
            ilaclarDBF.Miktar = int.Parse(dr.GetFieldValue<string>(dr.GetOrdinal("Miktari")));
            ilaclarDBF.Raf = dr.GetFieldValue<string>(dr.GetOrdinal("raf")).ToString();
            ilaclarDBF.Reyon = dr.GetFieldValue<string>(dr.GetOrdinal("reyon")).ToString();
            ilaclarDBF.Fiyat = int.Parse(dr.GetFieldValue<string>(dr.GetOrdinal("fiyati")));
            ilaclar.Add(ilaclarDBF);

            conn.Close();

            return ilaclar.ToList();
        }

        //// POST api/values
        //public void Post([FromBody]string value)
        //{
        //    //var formData = GetFormData();
        //    //var userId = formData["userId"];
        //}

        //// PUT api/values/5
        //public void Put(int id, [FromBody]string value)
        //{
        //}

        //// DELETE api/values/5
        //public void Delete(int id)
        //{
        //}
    }
}
