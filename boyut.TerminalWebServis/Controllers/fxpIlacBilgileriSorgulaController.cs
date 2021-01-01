using boyut.SayimDataAccess;
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
    //İLAÇ PARTİLERİNDEN SORGULA
    public class fxpIlacBilgileriSorgulaController : ApiController
    {
        // GET: api/fxpIlacBilgileriSorgula
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/fxpIlacBilgileriSorgula/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/fxpIlacBilgileriSorgula
        public JObject Post(Metin value)
        {
            JObject jObject = JObject.Parse(value.DuzMetin);
            String kodu = jObject["kodu"].ToString();
            String subeNo = jObject["subeNo"].ToString();
            
            //jsonString = "{'Miktari':'" + ALLTRIM(STR(mktr)) + "','Reyon':'" + ryn + "','Raf':'" + ALLTRIM(rf) + "','Cfiyati':'" + ALLTRIM(STR(cfyt)) + "','Kututipi':'" + ALLTRIM(ktip) + "'}"
            sayim fxpSayim = new sayim();
            JObject donen = JObject.Parse(fxpSayim.getIlacPrt(kodu,subeNo));
            String miktari = donen["Miktari"].ToString();
            String reyon = donen["Reyon"].ToString();
            String raf = donen["Raf"].ToString();
            String kutuTipi = donen["Kututipi"].ToString();
            String cfiyati = donen["Cfiyati"].ToString();

            JObject jsonStr = JObject.Parse("{'Miktar':'"+miktari+ "','Reyon':'" + reyon + "','Raf':'" + raf + "','KutuTipi':'" + kutuTipi + "','Cfiyati':'"+cfiyati+"'}"); 
            return jsonStr;
        }

        // PUT: api/fxpIlacBilgileriSorgula/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/fxpIlacBilgileriSorgula/5
        public void Delete(int id)
        {
        }
    }
}
