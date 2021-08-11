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
            serifsevkiyat.sevkiyatmalalis sevk = new serifsevkiyat.sevkiyatmalalis();
            try
            {

                string don = sevk.plakaSevkSaatiEkle(plk.DuzMetin);
                sevk.cikis();
                InsertText insertText = new InsertText(@"C:\net\MalAlisLog", "Kayıt - PlakaSevkSaatiEkle +++++++++ " + plk.DuzMetin + "\n" + new string(' ', 36) + don);

                return don;
            }
            catch (Exception e)
            {

                InsertText insertText = new InsertText(@"C:\net\MalAlisLog", "Hata - EzaneCikisSevkKayit +++++++++ " + plk.DuzMetin + "\n" + new string(' ', 36) + e.Message);
                sevk.cikis();
                return "";
            }


            return "";
        }

        // PUT: api/PlakaSevkSaatiEkle/5
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE: api/PlakaSevkSaatiEkle/5
        public void Delete(int id)
        {
        }
    }
}
