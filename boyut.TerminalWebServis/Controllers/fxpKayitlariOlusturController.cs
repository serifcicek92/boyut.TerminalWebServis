using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using boyut.SayimDataAccess;
using boyut.TerminalWebServis.Models;
using Newtonsoft.Json.Linq;
using serifsayim;

namespace boyut.TerminalWebServis.Controllers
{
    public class fxpKayitlariOlusturController : ApiController
    {
        // GET: api/fxpKayitlariOlustur
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/fxpKayitlariOlustur/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/fxpKayitlariOlustur
        public JObject Post(Metin metin)
        {
            JArray jArray = JArray.Parse(metin.DuzMetin);

            String log = "Kayıt Ekleme İşlemi \nEkleyen : " + jArray.First["Ekleyen"].ToString() + "\n Tanım : " + jArray.First["Tanim"].ToString() + "\n Tarih : " + jArray.First["SayimDosyasi"].ToString().ToLower().Replace("say", "").Substring(0, 5);
            InsertText insertLog = new InsertText(@"C:\net\AndroidLog.txt", log);
            insertLog = null;
            string hatadurum = "ok";
            sayim s = new sayim();

            foreach (var item in jArray)
            {
                try
                {
                    String olustur = s.Sayimbilgilerigir(item["Reyon"].ToString(),
                   item["Raf"].ToString(),
                   item["Tanim"].ToString(),
                   item["Ekleyen"].ToString(),
                   item["Ekleyen"].ToString(),
                   item["EklemeZamani"].ToString(),
                   item["Kodu"].ToString(),
                   item["Fiyat"].ToString(),
                   item["Kututipi"].ToString(),
                   item["Miad"].ToString(),
                   item["SeriNo"].ToString(),
                   item["Miktar"].ToString(),
                   item["KDegisti"].ToString(),
                   item["SayimDosyasi"].ToString().ToLower());

                }
                catch (Exception e)
                {
                    hatadurum = "false";
                    InsertText InsertLog = new InsertText(@"C:\net\AndroidLog.txt", "KAYIT HATASI======= \n Kodu : " + item["Kodu"].ToString() + "\n Sayım Dosyası : " + item["SayimDosyasi"].ToString());
                }
                finally
                {
                    s.DBKAPAT();
                }

            }
            s.wlogout();
            s.DBKAPAT();
            s = null;

            string jsonString = "{'durum':'"+hatadurum+"'}";
            JObject jObject = JObject.Parse(jsonString);
            return jObject;
        }

        // PUT: api/fxpKayitlariOlustur/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/fxpKayitlariOlustur/5
        public void Delete(int id)
        {
        }
    }
}
