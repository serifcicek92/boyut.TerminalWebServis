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
    public class SayimFisleriController : ApiController
    {
        private OracleConnection connection;
        private OracleCommand oracleCommand;
        private OracleDataReader oracleDataReader;
        // GET: api/SayimFisleri
        public IEnumerable<SayimFisi> Get()
        {
            Connection baglanti = new Connection();
            this.connection = baglanti.Conn();
            string sql = "Select * From STK_SAYIMFISLERI ORDER BY ID DESC";
            oracleCommand = new OracleCommand(sql, this.connection);
            try
            {
                oracleDataReader = oracleCommand.ExecuteReader();
            }
            catch (Exception)
            {

                throw;
            }
            List<SayimFisi> sayimFisleri = new List<SayimFisi>();
            while (oracleDataReader.Read())
            {
                sayimFisleri.Add(new SayimFisi
                {
                    Id = Convert.ToInt32(oracleDataReader["ID"]),
                    Aktif = Convert.ToInt32(oracleDataReader["AKTIF"]),
                    CepSubeId = Convert.ToInt32(oracleDataReader["CEPSUBEID"]),
                    EkleyenId = Convert.ToInt32(oracleDataReader["EKLEYENID"]),
                    GuncelleyenId = Convert.ToInt32(oracleDataReader["GUNCELLEYENID"]),
                    SayimTarihi = (oracleDataReader["SAYIMTARIHI"].ToString()),
                });
            }


            connection.Close();

            return sayimFisleri;
        }

        // GET: api/SayimFisleri/5 ÇALIŞMIYAN
        public string Get(SayimFisi sayimFisi)
        {
            string donenDeger = "false";
            Connection cn = new Connection();
            this.connection = cn.Conn();
            string sql = "select * from STK_SAYIMFISLERI " +
                "where SAYIMTARIHI = TO_DATE(':tarih','dd-mm-yyyy') AND AKTIF = 1 AND CEPSUBEID = cepsubeid";
            this.oracleCommand = new OracleCommand(sql, connection);

            oracleCommand.Parameters.Add(new OracleParameter("tarih", sayimFisi.SayimTarihi.ToString()));
            oracleCommand.Parameters.Add(new OracleParameter("cepsubeid", sayimFisi.CepSubeId.ToString()));
            OracleDataReader dataReader = oracleCommand.ExecuteReader();

            if (dataReader.HasRows)
            {
                donenDeger = "true";
            }

            return donenDeger;
        }

        // POST: api/SayimFisleri
        public IEnumerable<SayimFisi> Post(SayimFisi value)
        {
            Connection baglanti = new Connection();
            this.connection = baglanti.Conn();


            string sql = "INSERT INTO STK_SAYIMFISLERI" +
            "(CEPSUBEID,EKLEYENID,GUNCELLEYENID,AKTIF,EKLEMEZAMANI,SAYIMTARIHI) values " +
            "(:cepsubeid,:ekleyenid,:guncelleyenid,:aktif,SYSDATE,TO_DATE(:sayimtarihi,'dd-mm-yyyy'))";
            oracleCommand = new OracleCommand(sql, connection);

            oracleCommand.Parameters.Add(new OracleParameter("cepsubeid", value.CepSubeId.ToString()));
            oracleCommand.Parameters.Add(new OracleParameter("ekleyenid", Convert.ToInt32(value.EkleyenId)));
            oracleCommand.Parameters.Add(new OracleParameter("guncelleyenid", Convert.ToInt32(value.GuncelleyenId)));
            oracleCommand.Parameters.Add(new OracleParameter("aktif", 1));
            oracleCommand.Parameters.Add(new OracleParameter("sayimtarihi", value.SayimTarihi.ToString()));

            oracleCommand.ExecuteNonQuery();






            string metin = @"{'Giris':'" + "dene" + "','Depo':'" + "dene" + "'}";
            var data = JObject.Parse(metin);

            connection.Close();

            var fis = Get();

            return fis;
        }

        [Route("api/SayimFisleri/Kontrol")]
        public string Post2(SayimFisi sayimFisi)
        {
            string donenDeger = "false";
            Connection cn = new Connection();
            this.connection = cn.Conn();
            string sql = "select * from STK_SAYIMFISLERI " +
                "where SAYIMTARIHI = TO_DATE(:tarih,'dd-mm-yyyy') and AKTIF = 1 and CEPSUBEID = :cepsubeid";
            this.oracleCommand = new OracleCommand(sql, connection);

            oracleCommand.Parameters.Add(new OracleParameter("tarih", sayimFisi.SayimTarihi.ToString()));
            oracleCommand.Parameters.Add(new OracleParameter("cepsubeid", sayimFisi.CepSubeId.ToString()));
            OracleDataReader dataReader = oracleCommand.ExecuteReader();

            if (dataReader.HasRows)
            {
                donenDeger = "true";
            }
            connection.Close();

            return donenDeger;
        }

        // PUT: api/SayimFisleri/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/SayimFisleri/5
        public void Delete(int id)
        {
        }
    }
    public class SayimFisi
    {
        public int Id { get; set; }
        public int CepSubeId { get; set; }
        public int EkleyenId { get; set; }
        public int GuncelleyenId { get; set; }
        public int Aktif { get; set; }
        public String SayimTarihi { get; set; }

    }
}
