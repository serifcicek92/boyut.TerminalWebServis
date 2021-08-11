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
    public class SvkEtiketBasController : ApiController
    {
        // GET: api/SvkEtiketBas
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/SvkEtiketBas/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/SvkEtiketBas
        public string Post(Metin jsonStr)
        {
            // faturano ve etiket yazıcı no 
            //FaturadanEtiketBas
            //etiketBaski 
            serifsevkiyat.sevkiyatmalalis ss = new serifsevkiyat.sevkiyatmalalis();

            try
            {
                JObject jObject = JObject.Parse(jsonStr.DuzMetin);
                String faturano = jObject["faturano"].ToString();
                String etiketYazici = jObject["etiketyazici"].ToString();
                String sepetNo = jObject["sepetNo"].ToString();
                if (faturano != "" && faturano != null)
                {
                    string don = ss.FaturadanEtiketBas(faturano, etiketYazici);
                    ss.cikis();
                    
                    InsertText insertText = new InsertText(@"C:\net\MalAlisLog", "Kayıt - SvkEtiketBas "+new string(' ',11)+"++++ " + jsonStr.DuzMetin + "\n" + new string(' ', 36) + don);

                    return don;
                }
                if (sepetNo != "" && sepetNo != null)
                {
                    string don = ss.etiketBaski(sepetNo, etiketYazici);
                    InsertText insertText = new InsertText(@"C:\net\MalAlisLog", "Kayıt - SvkEtiketBas " + new string(' ', 11) + "++++ " + jsonStr.DuzMetin + "\n" + new string(' ', 36) + don);
                    ss.cikis();
                    return don;
                }
            }
            catch (Exception e)
            {
                InsertText insertText = new InsertText(@"C:\net\MalAlisLog", "Kayıt - SvkEtiketBas " + new string(' ', 11) + "++++ " + jsonStr.DuzMetin + "\n" + new string(' ', 36) + e.Message);
                ss.cikis();

            }

            return "";
        }

        // PUT: api/SvkEtiketBas/5
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE: api/SvkEtiketBas/5
        public void Delete(int id)
        {
        }
    }
}
