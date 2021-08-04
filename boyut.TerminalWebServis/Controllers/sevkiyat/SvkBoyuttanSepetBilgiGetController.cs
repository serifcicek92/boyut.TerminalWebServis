using boyut.SayimDataAccess;
using boyut.TerminalWebServis.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using serifsevkiyat;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace boyut.TerminalWebServis.Controllers
{
    public class SvkBoyuttanSepetBilgiGetController : ApiController
    {
        // GET: api/SvkLocalSepetBilgiGet
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/SvkLocalSepetBilgiGet/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/SvkLocalSepetBilgiGet
        public String Post(SvkBarkod donen)
        {
            sevkiyatmalalis malalis = new sevkiyatmalalis();
            if (donen.taransinmi == "true")
            {
                string jsonArrayStr = malalis.barkodOkut(donen.barkod, donen.taransinmi);
                //List<SvkOkunanBilgi> okunanBilgiList = 
                //    JsonConvert.DeserializeObject<List<SvkOkunanBilgi>>(malalis.barkodOkut(donen.barkod, donen.taransinmi));
                malalis.cikis();
                return jsonArrayStr;

            }
            else
            {
                string jobjstr = malalis.barkodOkut(donen.barkod, donen.taransinmi);
                //SvkOkunanBilgi okunanBilgi = 
                //    JsonConvert.DeserializeObject<SvkOkunanBilgi>(malalis.barkodOkut(donen.barkod, donen.taransinmi));
                malalis.cikis();
                return jobjstr;
            }

            return null;
        }

        // PUT: api/SvkLocalSepetBilgiGet/5
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE: api/SvkLocalSepetBilgiGet/5
        public void Delete(int id)
        {

        }
    }
}
