using boyut.SayimDBFDataAccess;
using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace boyut.TerminalWebServis.Controllers
{
    public class getdbfController : ApiController
    {
        private DBFConnect baglanti;
        private OleDbConnection conn;
        // GET: api/getdbf
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/getdbf/5
        public List<IlaclarDBF> Get(string id)
        {
            baglanti = new DBFConnect();
            List<IlaclarDBF> ilaclar = new List<IlaclarDBF>();

            conn = baglanti.conn;

            string barkod = id;
            
            //string sql = "select * from ilaclar where barkodu=@barkod";
            string sql = "select * from ilaclar Left Join Ilacprt ON ilaclar.ILACKODUBT = Ilacprt.ILACKODU where barkodu=@barkod";
            OleDbCommand ilacGetir = new OleDbCommand(sql, conn);
            ilacGetir.Parameters.AddWithValue("@barkod", barkod);

            OleDbDataReader dr = ilacGetir.ExecuteReader();
                
            IlaclarDBF ilaclarDBF = new IlaclarDBF();

            while (dr.Read())
            {
                ilaclarDBF.Barkodu = barkod;
                //ilaclarDBF.IlacAdi = dr.GetFieldValue<string>(dr.GetOrdinal("ILACADI"));
                //ilaclarDBF.IlacKodu = int.Parse(dr.GetFieldValue<string>(dr.GetOrdinal("ILACKODUBT")));
                //ilaclarDBF.Miktar = int.Parse(dr.GetFieldValue<string>(dr.GetOrdinal("MIKTARI")));
                //ilaclarDBF.Raf = dr.GetFieldValue<string>(dr.GetOrdinal("RAF"));
                //ilaclarDBF.Reyon = dr.GetFieldValue<string>(dr.GetOrdinal("REYON"));
                //ilaclarDBF.Fiyat = int.Parse(dr.GetFieldValue<string>(dr.GetOrdinal("FIYATI")));

                // var A = dr.GetFieldValue<string>(dr.GetOrdinal("ILACADI")).ToString();
                //var b = dr.GetFieldValue<double>(dr.GetOrdinal("ILACKODUBT"));
                //var c = dr.GetFieldValue<double>(dr.GetOrdinal("MIKTARI"));
                //var d = dr.GetFieldValue<string>(dr.GetOrdinal("RAF")).ToString();
                //var e = dr.GetFieldValue<string>(dr.GetOrdinal("REYON")).ToString();
                //var f = dr.GetFieldValue<double>(dr.GetOrdinal("FIYATI"));

                if (!dr.IsDBNull(dr.GetOrdinal("ILACADI"))) { ilaclarDBF.IlacAdi = dr.GetFieldValue<string>(dr.GetOrdinal("ILACADI")).ToString(); }
                if (!dr.IsDBNull(dr.GetOrdinal("ILACKODUBT"))) { ilaclarDBF.IlacKodu = int.Parse(dr.GetFieldValue<double>(dr.GetOrdinal("ILACKODUBT")).ToString()); }
                if (!dr.IsDBNull(dr.GetOrdinal("ilaclar.MIKTARI"))) { ilaclarDBF.Miktar = int.Parse(dr.GetFieldValue<double>(dr.GetOrdinal("ilaclar.MIKTARI")).ToString()); }
                if (!dr.IsDBNull(dr.GetOrdinal("ilaclar.RAF"))) { ilaclarDBF.Raf = dr.GetFieldValue<string>(dr.GetOrdinal("ilaclar.RAF")).ToString(); }
                if (!dr.IsDBNull(dr.GetOrdinal("ilaclar.REYON"))) { ilaclarDBF.Reyon = dr.GetFieldValue<string>(dr.GetOrdinal("ilaclar.REYON")).ToString(); }
                if (!dr.IsDBNull(dr.GetOrdinal("FIYATI"))) { ilaclarDBF.Fiyat = int.Parse(dr.GetFieldValue<double>(dr.GetOrdinal("FIYATI")).ToString()); }
                if (!dr.IsDBNull(dr.GetOrdinal("ilaclar.KUTUTIPI"))) { ilaclarDBF.KutuTipi = dr.GetFieldValue<string>(dr.GetOrdinal("ilaclar.KUTUTIPI")).ToString(); }
                if (!dr.IsDBNull(dr.GetOrdinal("MIAD"))) { ilaclarDBF.Miad = dr.GetFieldValue<string>(dr.GetOrdinal("MIAD")).ToString(); }

                ilaclar.Add(ilaclarDBF);
                break;
            }
            

            conn.Close();

            return ilaclar;
        }

        // POST: api/getdbf
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/getdbf/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/getdbf/5
        public void Delete(int id)
        {
        }
    }
}
