using boyut.SayimDataAccess;
using Newtonsoft.Json.Linq;
using serifsayim;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Web.Http;

namespace boyut.TerminalWebServis.Controllers
{
    public class fxpGetTumIlaclarController : ApiController
    {
        public List<IlcIlaclar> ilaclar;
        // GET: api/fxpGetTumIlaclar
        public IEnumerable<IlcIlaclar> Get()
        {
            ilaclar = new List<IlcIlaclar>();
            sayim s = new sayim();
            String jstring = s.getTumIlaclar();
            String a = jstring.Substring(3252370, 3252400);
            String b;
            JArray j = JArray.Parse(jstring);

            foreach (JObject item in j)
            {
                int fyt;
                if (item.GetValue("fiyati").ToString() == "" || item.GetValue("fiyati").ToString() == null || item.GetValue("fiyati").ToString().Length<1)
                {
                    fyt = 0;
                }
                else
                {   
                    String fiytBol = Regex.Replace(item.GetValue("fiyati").ToString().Trim().Replace(".",""), "[A-Za-z ]", "");
                    fyt = (fiytBol == "0" ? 0 : int.Parse(fiytBol.Substring(0, fiytBol.Length-4)));
                    //fyt = int.Parse(item.GetValue("fiyati").ToString().Substring(item.GetValue("fiyati").ToString().Length-4, item.GetValue("fiyati").ToString().Length));
                }
                String Adi = item.GetValue("ilacadi").ToString();
                   String Barkodu = item.GetValue("barkodu").ToString();
                   int Kodu = int.Parse(item.GetValue("ilackodu").ToString());
                   String KutuTipi = item.GetValue("kututipi").ToString();
                   int Fiyat = fyt;
                   String Miad = "";
                   String Raf = item.GetValue("raf").ToString();
               String Reyon = item.GetValue("reyon").ToString();
                ilaclar.Add(new IlcIlaclar
                {
                    Id = 0,
                    Adi = item.GetValue("ilacadi").ToString(),
                    Barkodu = item.GetValue("barkodu").ToString(),
                    Kodu = int.Parse(item.GetValue("ilackodu").ToString()),
                    KutuTipi = item.GetValue("kututipi").ToString(),
                    Fiyat = fyt,
                    Miad = "",
                    Raf = item.GetValue("raf").ToString(),
                    Reyon = item.GetValue("reyon").ToString()
                });

            }

            return ilaclar.ToList();
        }

        // GET: api/fxpGetTumIlaclar/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/fxpGetTumIlaclar
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/fxpGetTumIlaclar/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/fxpGetTumIlaclar/5
        public void Delete(int id)
        {
        }
    }
}
