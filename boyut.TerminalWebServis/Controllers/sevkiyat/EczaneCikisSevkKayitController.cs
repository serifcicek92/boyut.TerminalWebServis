using boyut.SayimDataAccess;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace boyut.TerminalWebServis.Controllers.sevkiyat
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
            serifsevkiyat.sevkiyatmalalis svk = new serifsevkiyat.sevkiyatmalalis();


            string don = svk.EczaneCikisSevkKayit(eczkod, takipno, fatno, sevkno, plkno, ss, ps, bs, ks);
            svk.cikis();
            return don;
        }

        // PUT: api/EczaneCikisSevkKayit/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/EczaneCikisSevkKayit/5
        public void Delete(int id)
        {
        }
    }
}
