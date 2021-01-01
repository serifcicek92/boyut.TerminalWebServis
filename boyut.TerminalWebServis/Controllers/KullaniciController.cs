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
    public class KullaniciController : ApiController
    {
        // GET: api/Kullanici
        public IEnumerable<string> Get()
        {

            return new string[] { "value1", "value2" };
        }

        // GET: api/Kullanici/5
        public string Get(int id)
        {
            return id.ToString();
        }

        // POST: api/Kullanici
        public JObject Post(Kullanici value)
        {
            bool giris = false;
            string depo = "";
            string sql = "SELECT ID,KULLANICIADI,KULLANICIPAROLA,CEPSUBEID " +
                "FROM KULLANICILAR " +
                "WHERE KullaniciAdi = :kullaniciAdi and " +
                "KULLANICIPAROLA = :sifre";
            string id = "";
            Connection baglanti = new Connection();
            OracleCommand command = new OracleCommand(sql, baglanti.Conn());

            command.BindByName = true;
            command.Parameters.Add(new OracleParameter("kullaniciAdi", value.KullaniciAdi));
            command.Parameters.Add(new OracleParameter("Sifre", value.Sifre));

            OracleDataReader oracleDataReader = command.ExecuteReader();
            while (oracleDataReader.Read())
            {
                if (oracleDataReader.HasRows)
                {
                    giris = true;
                    depo = oracleDataReader["CEPSUBEID"].ToString();
                    id = oracleDataReader["ID"].ToString();

                }

            }
            baglanti.Close();
            //System.Threading.Thread.Sleep(5000);

            string metin = @"{'Giris':'"+giris+"','Depo':'"+depo+ "','Id':'" + id + "'}";
            var data = JObject.Parse(metin);


            return data;
        }

        // PUT: api/Kullanici/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Kullanici/5
        public void Delete(int id)
        {
        }
    }
    
}