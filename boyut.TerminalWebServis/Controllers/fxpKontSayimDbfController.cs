using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using serifsayim;
using boyut.SayimDataAccess;
namespace boyut.TerminalWebServis.Controllers
{
    public class fxpKontSayimDbfController : ApiController
    {
        // GET: api/fxpKontSayimDbf
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/fxpKontSayimDbf/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/fxpKontSayimDbf
        public JObject Post(Metin value)
        {
            String[] ayrilan = value.DuzMetin.Split('.');
            Array.Reverse(ayrilan);


            string depoNo = (ayrilan[0] == null) ? "01" : (ayrilan[0]=="00") ? "01" : ayrilan[0];

            string dbfadi = "say"+ayrilan[1].Substring(ayrilan[1].Length-1)+ayrilan[2]+ayrilan[3]+".d"+depoNo;
            sayim s = new sayim();
            string varmi = s.sayimDosyasiVarmi(dbfadi);
            if (varmi == "1")
            {
                varmi = "{'dosya':'var'}";
            }
            else
            {
                varmi = "{'dosya':'yok'}";
            }

            JObject jObject = JObject.Parse(varmi);
            // 
            return jObject;
        }

        // PUT: api/fxpKontSayimDbf/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/fxpKontSayimDbf/5
        public void Delete(int id)
        {
        }
    }
}
