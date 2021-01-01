using boyut.SayimDataAccess;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace boyut.TerminalWebServis.Controllers
{
    public class LogKaydetController : ApiController
    {
        // GET: api/LogKaydet
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/LogKaydet/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/LogKaydet
        public JObject Post(Metin value)
        {
            if (!File.Exists(@"C:\net\AndroidLog.txt"))
            {
                File.Create(@"C:\net\AndroidLog.txt").Close();
            }
            File.AppendAllLines(@"C:\net\AndroidLog.txt", new string[] { DateTime.Now.ToString()+":::"+value.DuzMetin });

            return null;
        }

        // PUT: api/LogKaydet/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/LogKaydet/5
        public void Delete(int id)
        {
        }
    }
}
