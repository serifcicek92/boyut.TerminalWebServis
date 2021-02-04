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
    public class ReyonGirisController : ApiController
    {
        // GET: api/ReyonGiris
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/ReyonGiris/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/ReyonGiris
        public String Post(Metin value)
        {
            JObject jObject = JObject.Parse(value.DuzMetin);
            String sepet = jObject["sepetno"].ToString().Trim();

            sayim sym = new sayim();
            try
            {
                String donen = sym.sepetGiris(sepet, "");
                InsertText InsertLog = new InsertText(@"C:\net\AndroidSepetKaydetLog.txt", "SEPET : " +sepet);
                return donen;
            }
            catch (Exception e)
            {
                InsertText InsertLog = new InsertText(@"C:\net\AndroidSepetKaydetLog.txt", "SEPET KAYDETMEDE HATA : " + e.Message);
                return "Hata";
            }
        }

        // PUT: api/ReyonGiris/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/ReyonGiris/5
        public void Delete(int id)
        {
        }
    }
}
