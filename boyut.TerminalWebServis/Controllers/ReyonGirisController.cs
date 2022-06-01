﻿using boyut.BusinessLayer;
using boyut.SayimDataAccess;
using boyut.TerminalWebServis.Models;
using Boyut.CommonLibrary;
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
    public class ReyonGirisController : ApiController
    {
        // GET: api/ReyonGiris
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/ReyonGiris/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/ReyonGiris
        public String Post(Metin value)
        {
            UserContext uc = new UserContext();
            uc.AKTIFYIL = 2008;
            uc.AKTIFDIL = "TR";
            try
            {
                uc.USERID = 1;
            }
            catch { }


            SIP_SEVKIYATLARDsp sevkiyatlarBLt = new SIP_SEVKIYATLARDsp();
            sevkiyatlarBLt.UpdatePaketDurum("7483", "1K 2398892", "16", null, "30/05/2022", "D", uc);

            JObject jObject = JObject.Parse(value.DuzMetin);
            String sepet = jObject["sepetno"].ToString().Trim();

            sayim sym = new sayim();
            try
            {
                String donen = sym.sepetGiris(sepet, "");
                InsertText InsertLog = new InsertText(@"C:\net\AndroidSepetKaydetLog.txt", "SEPET : " +sepet);
                JObject boyutDonenJson = JObject.Parse(donen);
                SIP_SEVKIYATLARDsp sevkiyatlarBL = new SIP_SEVKIYATLARDsp();
                sevkiyatlarBL.UpdatePaketDurum(boyutDonenJson["sepetno"].ToString().Trim(), boyutDonenJson["faturano"].ToString(), boyutDonenJson["subeno"].ToString(), null,boyutDonenJson["tarih"].ToString(), "D", uc);
                return donen;
            }
            catch (Exception e)
            {
                InsertText InsertLog = new InsertText(@"C:\net\AndroidSepetKaydetLog.txt", "SEPET KAYDETMEDE HATA : " + e.Message);
                return "Hata";
            }
        }

        // PUT: api/ReyonGiris/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/ReyonGiris/5
        public void Delete(int id)
        {
        }
    }
}
