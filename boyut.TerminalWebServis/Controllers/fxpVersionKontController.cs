﻿using boyut.SayimDataAccess;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace boyut.TerminalWebServis.Controllers
{
    public class fxpVersionKontController : ApiController
    {
        // GET: api/fxpVersionKont
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/fxpVersionKont/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/fxpVersionKont
        public String Post(Metin value)
        {
            Double gelen = Double.Parse(value.DuzMetin.Trim('"').Trim('\\'),CultureInfo.InvariantCulture);

            if (File.Exists(@"C:\net\AndroidSayimVersion.txt"))
            {
                Double version = Double.Parse(File.ReadAllText(@"C:\net\AndroidSayimVersion.txt").Trim(), CultureInfo.InvariantCulture);
                
                if(version > gelen)
                {
                    return "TRUE";
                }
                else
                {
                    return "FALSE";
                }
            }
            else
            {
                File.Create(@"C:\net\AndroidSayimVersion.txt").Close();
                File.WriteAllText(@"C:\net\AndroidSayimVersion.txt", gelen.ToString().Replace(",","."));
                return "FALSE";
            }
            return "";
        }

        // PUT: api/fxpVersionKont/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/fxpVersionKont/5
        public void Delete(int id)
        {
        }
    }
}