using boyut.SayimDataAccess;
using boyut.TerminalWebServis.Models;
using Newtonsoft.Json.Linq;
using serifsayim;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace boyut.TerminalWebServis.Controllers
{
    public class fxpRafDegistirController : ApiController
    {
        // GET: api/fxpRafDegistir
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/fxpRafDegistir/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/fxpRafDegistir
        public JObject Post(Metin value)
        {
            JObject j = JObject.Parse(value.DuzMetin);

            String aa = "bbb";
            string bb = aa;
           

            sayim guncelle = new sayim();
            String a =   guncelle.rafDegistir(j["Kodu"].ToString(), j["cno"].ToString(), j["Fiyat"].ToString(), j["Draf"].ToString(),j["BeslemeYeri"].ToString(),j["Miktar"].ToString());

            string jsonString = "{'durum':'"+a+"ok'}";
            JObject jObject = JObject.Parse(jsonString);

            InsertText log = new InsertText(@"C:\net\AndroidLog.txt", "Raf Değiştirme İşlemi \n Kodu: " + j["Kodu"].ToString() + "\n Cep No : " + j["cno"].ToString() + "\n Raf : " + j["Draf"].ToString() + "\n Besleme Yeri : " + j["BeslemeYeri"].ToString());
            return jObject;
        }

        // PUT: api/fxpRafDegistir/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/fxpRafDegistir/5
        public void Delete(int id)
        {
        }
    }
}
