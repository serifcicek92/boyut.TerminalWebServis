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
    public class testController : ApiController
    {
        // GET: api/test
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2x" };
        }

        // GET: api/test/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/test
        public String Post([FromBody] Metin value)
        {
            serifsevkiyat.sevkiyatmalalis sevk = new sevkiyatmalalis();
            //try
            //{
            //    sevk.test();
            //    sevk.barkodOkut("s7799");
            //    return "oldu";
            //}
            //catch (Exception E)
            //{

            //    throw E;
            //}
            return null;

        }

        // PUT: api/test/5
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE: api/test/5
        public void Delete(int id)
        {
        }
    }
}
