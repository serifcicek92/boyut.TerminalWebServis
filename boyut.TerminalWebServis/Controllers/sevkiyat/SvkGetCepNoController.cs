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
    public class SvkGetCepNoController : ApiController
    {
        // GET: api/SvkGetCepNo
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/SvkGetCepNo/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/SvkGetCepNo
        public string Post(Metin metin)
        {
            sevkiyatmalalis svk = new sevkiyatmalalis();
            try
            {
                string cepno = svk.getCepNo().ToString().Trim();
                svk.cikis();
                InsertText insertText = new InsertText(@"C:\net\MalAlisLog", "Sorgu - SvkGetCepNo         ++++++++ " + metin.DuzMetin + "\n" + new string(' ', 36) + cepno);

                return "{'cepno':'" + cepno + "'}";
            }
            catch (Exception e)
            {
                InsertText insertText = new InsertText(@"C:\net\MalAlisLog", "Sorgu - SvkGetCepNo         ++++++++ " + metin.DuzMetin + "\n" + new string(' ', 36) + e.Message);
                svk.cikis();
                throw;
            }

        }

        // PUT: api/SvkGetCepNo/5
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE: api/SvkGetCepNo/5
        public void Delete(int id)
        {
        }
    }
}
