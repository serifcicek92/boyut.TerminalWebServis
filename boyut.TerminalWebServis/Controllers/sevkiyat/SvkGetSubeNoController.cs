using boyut.SayimDataAccess;
using serifsevkiyat;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace boyut.TerminalWebServis.Controllers
{ 
    public class SvkGetSubeNoController : ApiController
    {
        // GET: api/SvkGetSubeNo
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/SvkGetSubeNo/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/SvkGetSubeNo
        public string Post(Metin metin)
        {
            sevkiyatmalalis svk = new sevkiyatmalalis();
            string subeno = svk.getSubeNo().ToString().Trim();
            svk.cikis();
            return "{'subeno':'"+subeno+"'}";

        }

        // PUT: api/SvkGetSubeNo/5
        public void Put(int id, [FromBody]string value)
        {

        }

        // DELETE: api/SvkGetSubeNo/5
        public void Delete(int id)
        {
        }
    }
}
