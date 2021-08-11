using boyut.SayimDataAccess;
using boyut.TerminalWebServis.Models;
using serifsevkiyat;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace boyut.TerminalWebServis.Controllers
{
    public class SvkPlakaKontrolController : ApiController
    {
        // GET: api/SvkPlakaKontrol
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/SvkPlakaKontrol/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/SvkPlakaKontrol
        public string Post(Metin value)
        {
            sevkiyatmalalis malAlis = new sevkiyatmalalis();
            try
            {
                string donen = malAlis.barkodOkut(value.DuzMetin, false);
                malAlis.cikis();
                InsertText insertText = new InsertText(@"C:\net\MalAlisLog", "Sorgu - SvkPlakaKontrol     ++++++++ " + value.DuzMetin + "\n" + new string(' ', 36) + donen);

                return donen;
            }
            catch (Exception e)
            {
                InsertText insertText = new InsertText(@"C:\net\MalAlisLog", "Sorgu - SvkPlakaKontrol     ++++++++ " + value.DuzMetin + "\n" + new string(' ', 36) + e.Message);
                malAlis.cikis();
                throw;
            }

        }

        // PUT: api/SvkPlakaKontrol/5
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE: api/SvkPlakaKontrol/5
        public void Delete(int id)
        {
        }
    }
}
