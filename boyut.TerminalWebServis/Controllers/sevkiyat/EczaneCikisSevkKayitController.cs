using boyut.SayimDataAccess;
using boyut.TerminalWebServis.Models;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace boyut.TerminalWebServis.Controllers
{
    public class EczaneCikisSevkKayitController : ApiController
    {
        // GET: api/EczaneCikisSevkKayit
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/EczaneCikisSevkKayit/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/EczaneCikisSevkKayit
        public String Post(Metin value)
        {
            serifsevkiyat.sevkiyatmalalis svk = new serifsevkiyat.sevkiyatmalalis();

            try
            {
                JObject jObject = JObject.Parse(value.DuzMetin);
                string eczkod = jObject["eczkod"].ToString();
                string takipno = jObject["takipno"].ToString();
                string fatno = jObject["fatno"].ToString();
                string sevkno = jObject["sevkno"].ToString();
                string plkno = jObject["plkno"].ToString();
                string ss = jObject["ss"].ToString();
                string ps = jObject["ps"].ToString();
                string bs = jObject["bs"].ToString();
                string ks = jObject["ks"].ToString();
                string don = svk.EczaneCikisSevkKayit(eczkod, takipno, fatno, sevkno, plkno, ss, ps, bs, ks);

                svk.cikis();

                InsertText insertText = new InsertText(@"C:\net\MalAlisLog", "Kayıt - EzaneCikisSevkKayit ++++++++ " + value.DuzMetin+"\n"+ new string(' ', 36)+don);
                return don;

            }
            catch (Exception e)
            {
                InsertText insertText = new InsertText(@"C:\net\MalAlisLog", "HATA - EzaneCikisSevkKayit +++++++++ " + value.DuzMetin + "\n" + new string(' ', 36) + e.Message);
                svk.cikis();

                throw;
            }

        }

        // PUT: api/EczaneCikisSevkKayit/5
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE: api/EczaneCikisSevkKayit/5
        public void Delete(int id)
        {
        }
    }
}
