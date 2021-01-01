using boyut.SayimDataAccess;
using boyut.TerminalWebServis.Models;
using Newtonsoft.Json.Linq;
using serifsayim;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace boyut.TerminalWebServis.Controllers
{
    public class fxpSubeNoController : ApiController
    {
        // GET: api/fxpSubeNo
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/fxpSubeNo/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/fxpSubeNo
        public JObject Post(Metin value)
        {
            sayim s = new sayim();
            try
            {
                String subeNo = s.getSubeNo();
                String jsonString = "{'subeNo':'" + subeNo + "'}";
                JObject j = JObject.Parse(jsonString);
                return j;
            }
            catch (Exception e)
            {

                InsertText InsertLog = new InsertText(@"C:\net\AndroidLog.txt", "\nDLL HATASI : " + e.Message);
            }
            finally
            {
                s.DBKAPAT();
                s = null;
            }

                return null;


        }

        // PUT: api/fxpSubeNo/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/fxpSubeNo/5
        public void Delete(int id)
        {
        }
    }
}
