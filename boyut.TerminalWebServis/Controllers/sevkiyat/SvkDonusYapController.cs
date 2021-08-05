using boyut.SayimDataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace boyut.TerminalWebServis.Controllers
{
    public class SvkDonusYapController : ApiController
    {
        // GET: api/SvkDonusYap
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/SvkDonusYap/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/SvkDonusYap
        public string Post(Metin value)
        {
            string plakano = value.DuzMetin;
            if (plakano!="")
            {
                serifsevkiyat.sevkiyatmalalis svk = new serifsevkiyat.sevkiyatmalalis();
                string don = svk.donusYap(plakano);
                svk.cikis();
                return don;
            }
            return "false";
        }

        // PUT: api/SvkDonusYap/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/SvkDonusYap/5
        public void Delete(int id)
        {
        }
    }
}
