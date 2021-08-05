using boyut.SayimDataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace boyut.TerminalWebServis.Controllers
{
    public class PlakaSevkSaatiEkleController : ApiController
    {
        // GET: api/PlakaSevkSaatiEkle
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/PlakaSevkSaatiEkle/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/PlakaSevkSaatiEkle
        public string Post(Metin plk)
        {
            try
            {
                serifsevkiyat.sevkiyatmalalis sevk = new serifsevkiyat.sevkiyatmalalis();
                string don = sevk.plakaSevkSaatiEkle(plk.DuzMetin);
                sevk.cikis();
                return don;
            }
            catch (Exception)
            {

                throw;
                return "";
            }
           

            return "";
        }

        // PUT: api/PlakaSevkSaatiEkle/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/PlakaSevkSaatiEkle/5
        public void Delete(int id)
        {
        }
    }
}
