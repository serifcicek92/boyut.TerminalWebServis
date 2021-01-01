using boyut.SayimDataAccess;
using Newtonsoft.Json.Linq;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace boyut.TerminalWebServis.Controllers
{
    public class SayimKaydetController : ApiController
    {
        private OracleConnection oracleConnection;
        private OracleCommand oracleCommand;
        private OracleDataReader oracleDataReader;
        OracleTransaction oracleTransaction;
        // GET: api/SayimKaydet
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/SayimKaydet/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/SayimKaydet
        [HttpPost]
        public String Post(veri value)
        {
            JArray j = JArray.Parse(value.veris);

            String log = j.First["Adi"].ToString();
            try
            {
                Connection connection = new Connection();
                this.oracleConnection = connection.Conn();
                String query = @"INSERT INTO BOYUT_2008.STK_SAYIMLAR(ILCILACID, KODU, ADI, MIKTAR, FIYAT, KUTUTIPI, MIAD, CEPSUBEID, REYON, RAF, EKLEYENID, SERINO, EKLEMEZAMANI, GUNCELLEYENID, GUNCELLEMEZAMANI, AKTIF, BARKOD, SAYIMTARIHI, SAYIMFISIID,YERI) 
    VALUES(:ilcilacid, :kodu, :adi, :miktar, :fiyat, :kututipi, :miad, :cepsubeid, :reyon, :raf, :ekleyenid, :serino, TO_DATE(:eklemezamani,'dd-mm-yyyy'), :guneclleyenid, TO_DATE(:guncellemezamani,'dd-mm-yyyy'), :aktif, :barkod, TO_DATE(:sayimtarihi,'dd-mm-yyyy'), :sayimFisiId,:yeri)";
                
                foreach (var item in j)
                {

                    oracleCommand = new OracleCommand(query, oracleConnection);
                    oracleTransaction = oracleConnection.BeginTransaction(System.Data.IsolationLevel.ReadCommitted);
                    oracleCommand.Transaction = oracleTransaction;

                    oracleCommand.Parameters.Add(new OracleParameter("ilcilacid", Convert.ToInt32(item["IlcIlacId"])));
                    oracleCommand.Parameters.Add(new OracleParameter("kodu", Convert.ToInt32(item["Kodu"])));
                    oracleCommand.Parameters.Add(new OracleParameter("adi", item["Adi"].ToString()));
                    oracleCommand.Parameters.Add(new OracleParameter("miktar", Convert.ToInt32(item["Miktar"])));
                    oracleCommand.Parameters.Add(new OracleParameter("fiyat", Convert.ToInt32(item["Fiyat"])));
                    oracleCommand.Parameters.Add(new OracleParameter("kututipi", item["Kututipi"].ToString()));
                    oracleCommand.Parameters.Add(new OracleParameter("miad", item["Miad"].ToString()));
                    oracleCommand.Parameters.Add(new OracleParameter("cepsubeid", item["Depo"].ToString()));
                    oracleCommand.Parameters.Add(new OracleParameter("reyon", item["Reyon"].ToString()));
                    oracleCommand.Parameters.Add(new OracleParameter("raf", item["Raf"].ToString()));
                    oracleCommand.Parameters.Add(new OracleParameter("ekleyenid", (item["EkleyenId"].ToString() == "null" ? 0 : Convert.ToInt32(item["EkleyenId"]))));
                    oracleCommand.Parameters.Add(new OracleParameter("serino", (item["SeriNo"].ToString() == "" || item["SeriNo"].ToString() == null ? "0" :item["SeriNo"].ToString())));
                    oracleCommand.Parameters.Add(new OracleParameter("eklemezamani", item["EklemeZamani"].ToString()));
                    oracleCommand.Parameters.Add(new OracleParameter("guneclleyenid", Convert.ToInt32(item["GuncelleyenId"])));
                    oracleCommand.Parameters.Add(new OracleParameter("guncellemezamani", item["GuncellemeZamani"].ToString()));
                    oracleCommand.Parameters.Add(new OracleParameter("aktif", Convert.ToInt32(item["Aktif"])));
                    oracleCommand.Parameters.Add(new OracleParameter("barkod", item["Barkod"].ToString()));
                    oracleCommand.Parameters.Add(new OracleParameter("sayimtarihi", item["SayimTarihi"].ToString()));
                    oracleCommand.Parameters.Add(new OracleParameter("sayimFisiId", Convert.ToInt32(item["SayimFisiId"])));
                    oracleCommand.Parameters.Add(new OracleParameter("yeri", item["Yeri"].ToString()));


                    //oracleCommand.Parameters.Add(new OracleParameter("id", item["Id"]));
                    //oracleCommand.Parameters.Add(new OracleParameter("kodu", item["Kodu"]));
                    //oracleCommand.Parameters.Add(new OracleParameter("adi", item["Adi"]));
                    //oracleCommand.Parameters.Add(new OracleParameter("miktar", item["Miktar"]));
                    //oracleCommand.Parameters.Add(new OracleParameter("fiyat", item["Fiyat"]));
                    //oracleCommand.Parameters.Add(new OracleParameter("kututipi", item["Kututipi"]));
                    //oracleCommand.Parameters.Add(new OracleParameter("miad", item["Miad"]));
                    //oracleCommand.Parameters.Add(new OracleParameter("depo", item["Depo"]));
                    //oracleCommand.Parameters.Add(new OracleParameter("reyon", item["Reyon"]));
                    //oracleCommand.Parameters.Add(new OracleParameter("raf", item["Raf"]));
                    //oracleCommand.Parameters.Add(new OracleParameter("ekleyenid", item["EkleyenId"]));
                    //oracleCommand.Parameters.Add(new OracleParameter("serino", item["SeriNo"]));
                    //oracleCommand.Parameters.Add(new OracleParameter("eklemezamani", item["EklemeZamani"]));
                    //oracleCommand.Parameters.Add(new OracleParameter("guneclleyenid", item["GuncelleyenId"]));
                    //oracleCommand.Parameters.Add(new OracleParameter("guncellemezamani", item["GuncellemeZamani"]));
                    //oracleCommand.Parameters.Add(new OracleParameter("aktif", item["Aktif"]));
                    //oracleCommand.Parameters.Add(new OracleParameter("barkod", item["Barkod"]));

                    oracleCommand.ExecuteNonQuery();
                    oracleTransaction.Commit();
                    

                }
                oracleConnection.Close();
                

            }
                catch (Exception)
            {
                oracleTransaction.Rollback();
                throw;
            }
            

            
            return "oke";
        }

        // PUT: api/SayimKaydet/5
        public void Put(int id, [FromBody]string value)
        {
            string asdf = value;
        }

        // DELETE: api/SayimKaydet/5
        public void Delete(int id)
        {
        }
    }
    public class veri
    {
        public string veris { get; set; }
    }
}
