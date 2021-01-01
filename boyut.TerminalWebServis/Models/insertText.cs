using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace boyut.TerminalWebServis.Models
{
    public class InsertText
    {
        public InsertText(string url, string text)
        {
            //@"C:\net\AndroidLog.txt"
            try
            {
                if (!File.Exists(url))
                {
                    File.Create(url).Close();
                }
                File.AppendAllLines(url, new string[] {":::"+ DateTime.Now.ToString() + ":::\n" + text });
            }
            catch (Exception)
            {

                throw;
            }
            
        }
    }
}