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
    public class fxpHataKaydetController : ApiController
    {
        // GET: api/fxpHataKaydet
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/fxpHataKaydet/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/fxpHataKaydet
        public void Post(Metin metin)
        {
            InsertText InsertLog = new InsertText(@"C:\net\ChazdanGelenHatalar.txt", "-HATA::" + metin.DuzMetin);
        }

        // PUT: api/fxpHataKaydet/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/fxpHataKaydet/5
        public void Delete(int id)
        {
        }
    }
}
