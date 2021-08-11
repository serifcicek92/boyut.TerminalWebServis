using boyut.SayimDataAccess;
using boyut.TerminalWebServis.Models;
using Newtonsoft.Json;
using serifsevkiyat;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace boyut.TerminalWebServis.Controllers
{
    public class FaturadanCepSepetBilgiGetirController : ApiController
    {
        // GET: api/FaturadanCepSepetBilgiGetir
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/FaturadanCepSepetBilgiGetir/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/FaturadanCepSepetBilgiGetir
        public String Post(SvkBWServisSepetBilgi okunanBilgiGetir)
        {
            sevkiyatmalalis svk = new sevkiyatmalalis();
            try
            {
                if (okunanBilgiGetir != null)
                {
                    string JSONDonen = svk.FaturadanCepSepetBilgiGetir(okunanBilgiGetir.FaturaTarihi, okunanBilgiGetir.Hesapkodu, okunanBilgiGetir.FaturaNo);
                    svk.cikis();
                    InsertText insertText = new InsertText(@"C:\net\MalAlisLog", "Sorgu - FaturadanCepSepetBilgiGetir+ Fatura No :" + okunanBilgiGetir.FaturaNo +
                        "Fatura Tarihi :"+okunanBilgiGetir.FaturaTarihi+ "\n" + new string(' ', 36) + JSONDonen);
                    return JSONDonen;
                }
            }
            catch (Exception e)
            {
                InsertText insertText = new InsertText(@"C:\net\MalAlisLog", "Hata - FaturadanCepSepetBilgiGetir++ Fatura No :" + okunanBilgiGetir.FaturaNo +
                       "Fatura Tarihi :" + okunanBilgiGetir.FaturaTarihi + "\n" + new string(' ', 36) + e.Message);
                svk.cikis();

            }

            return "";
            //SvkBWServisSepetBilgi okunanBilgiGetir = Newtonsoft.Json.JsonConvert.DeserializeObject<SvkBWServisSepetBilgi>(metin.DuzMetin);

        }

        // PUT: api/FaturadanCepSepetBilgiGetir/5
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE: api/FaturadanCepSepetBilgiGetir/5
        public void Delete(int id)
        {
        }
    }
}
