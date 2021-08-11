using boyut.SayimDataAccess;
using boyut.TerminalWebServis.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace boyut.TerminalWebServis.Controllers
{
    public class SvkSubeCepVeFirmaController : ApiController
    {
        // GET: api/SvkSubeCepVeFirma
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/SvkSubeCepVeFirma/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/SvkSubeCepVeFirma
        public string Post(Metin value)
        {
            serifsevkiyat.sevkiyatmalalis svk = new serifsevkiyat.sevkiyatmalalis();
            try
            {
                string jsonstr = svk.getSubeCepVeFirma();
                svk.cikis();
                InsertText insertText = new InsertText(@"C:\net\MalAlisLog", "Sorgu - SvkSubeCepVeFirma   ++++++++ " + value.DuzMetin + "\n" + new string(' ', 36) + jsonstr);

                return jsonstr;
            }
            catch (Exception e)
            {
                InsertText insertText = new InsertText(@"C:\net\MalAlisLog", "Sorgu - SvkSubeCepVeFirma   ++++++++ " + value.DuzMetin + "\n" + new string(' ', 36) + e.Message);
                throw;
            }

        }

        // PUT: api/SvkSubeCepVeFirma/5
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE: api/SvkSubeCepVeFirma/5
        public void Delete(int id)
        {
        }
    }
}
