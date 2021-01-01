using boyut.SayimDataAccess;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using serifsayim;
using boyut.TerminalWebServis.Models;

namespace boyut.TerminalWebServis.Controllers
{
    public class fxpKontKullController : ApiController
    {
        // GET: api/fxpKontKull
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/fxpKontKull/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/fxpKontKull
        public JObject Post(Kullanici kullanici)
        {
            string metin = "";
            sayim fxpSayim = new sayim();
            
            //System.Diagnostics.Process.Start("net.exe","use K:");

            string donenAd = fxpSayim.sifreKontrol(kullanici.Sifre);
            if (donenAd=="0")
            {
                metin = @"{'Giris':'false','KAdi':''}";
            }
            else
            {
                metin = @"{'Giris':'true','KAdi':'"+donenAd+"'}";
                InsertText insertText = new InsertText(@"C:\net\AndroidLog.txt", "Kullanıcı Girişi : " + donenAd);
            }
            JObject json = JObject.Parse(metin);

            return json;
        }

        // PUT: api/fxpKontKull/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/fxpKontKull/5
        public void Delete(int id)
        {
        }
    }
}
