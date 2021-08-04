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
            if (okunanBilgiGetir!=null)
            {
                sevkiyatmalalis svk = new sevkiyatmalalis();
                string JSONDonen = svk.FaturadanCepSepetBilgiGetir(okunanBilgiGetir.FaturaTarihi, okunanBilgiGetir.Hesapkodu, okunanBilgiGetir.FaturaNo);
                svk.cikis();
                return JSONDonen;
            }
            return "";
            //SvkBWServisSepetBilgi okunanBilgiGetir = Newtonsoft.Json.JsonConvert.DeserializeObject<SvkBWServisSepetBilgi>(metin.DuzMetin);

        }

        // PUT: api/FaturadanCepSepetBilgiGetir/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/FaturadanCepSepetBilgiGetir/5
        public void Delete(int id)
        {
        }
    }
}
