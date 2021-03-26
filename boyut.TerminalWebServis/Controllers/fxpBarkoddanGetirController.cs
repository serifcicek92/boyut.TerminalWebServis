using boyut.SayimDataAccess;
using Boyut.CommonLibrary;
using Boyut.ProxyLayer.IlacPL;
using Newtonsoft.Json.Linq;
using serifsayim;
using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using boyut.TerminalWebServis.Models;

namespace boyut.TerminalWebServis.Controllers
{
    public class fxpBarkoddanGetirController : ApiController
    {
        // GET: api/fxpBarkoddanGetir
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/fxpBarkoddanGetir/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/fxpBarkoddanGetir
        public JObject Post(Metin value)
        {
            JObject jObject = JObject.Parse(value.DuzMetin);
            String brkd = jObject["barkodu"].ToString();
            String sbNo = jObject["subeNo"].ToString();

            sayim fxpSayim = new sayim();
            JObject donen;

            string paketMiadi = "";
            if (brkd.Substring(0, 2) != "00")

            {
                string hataYaz =  fxpSayim.getIlacBilgileri(brkd, sbNo);
                //String dooo = fxpSayim.getIlacBilgileri(brkd, sbNo);
                try
                {
                    donen = JObject.Parse(fxpSayim.getIlacBilgileri(brkd, sbNo));
                    hataYaz += "bura oldu";
                    hataYaz += donen["ilacadi"].ToString();
                    return getBilgiler(donen, paketMiadi);

                }
                catch (Exception e)
                {
                    Random r = new Random(999);
                    InsertText InsertLog = new InsertText(@"C:\net\AndroidLog"+r+".txt", "HATA::::::"+hataYaz+"\nDLL HATASI : " + e.Message);
                    return null;
                }
                finally
                {
                    fxpSayim.DBKAPAT();
                    fxpSayim = null;
                }
            }
            else
            {
                ClientSecurity.UserInfo.USERID = 1;

                ClientSecurity.UserInfo.AKTIFYIL = 2008;
                ClientSecurity.UserInfo.USERNAME = "admin"; //ApplicationSettings.ReadConfig("userName").Decrypt();
                ClientSecurity.UserInfo.PASSWORD = "admin0805".ToMD5(); //ApplicationSettings.ReadConfig("passWord").Decrypt().ToMD5();

                try
                {
                    DataSet ds = ILCFATURALARPrx.GetKareKodDetailsByPaketBarkodu(brkd);
                    string brkd2 = ds.Tables[0].Rows[0]["barkod"].ToString();
                    paketMiadi = ds.Tables[0].Rows[0]["MIADI"].ToString().Substring(0, 10);
                    paketMiadi = paketMiadi.Substring(8, 2) + paketMiadi.Substring(3, 2) + paketMiadi.Substring(0, 2);

                    try
                    {
                        donen = JObject.Parse(fxpSayim.getIlacBilgileri(brkd, sbNo));
                        return getBilgiler(donen, paketMiadi);
                    }
                    catch (Exception e)
                    {
                        InsertText InsertLog = new InsertText(@"C:\net\AndroidLog.txt", "DLL HATASI : " + e.Message);
                        return null;

                    }
                    finally
                    {
                        fxpSayim.DBKAPAT();
                        fxpSayim = null;
                    }

                }
                catch (Exception e)
                {
                    InsertText InsertLog = new InsertText(@"C:\net\AndroidLog.txt", "PAKET OKUTMA HATASI : \n Web Servise Bağlamnılamıyor " + e.Message);
                }
               


                //throw;
            }



            return null;
           
        }

        public JObject getBilgiler(JObject donen, string paketMiadi)
        {
            String ilacAdi = donen["ilacadi"].ToString();
            String ilacKodu = donen["ilackodu"].ToString();
            String miktari = donen["miktari"].ToString();
            String reyon = (donen["reyon"].ToString() == "" ? donen["reyon2"].ToString() : (donen["reyon"].ToString() == "" || donen["reyon"] == null ? "" : donen["reyon"].ToString()));
            String raf = donen["raf"].ToString();
            String kutuTipi = donen["kututipi"].ToString();
            String kutuTipi2 = (donen["kututipi2"] == null ? "" : donen["kututipi2"].ToString());
            if (kutuTipi.Trim() == "" || kutuTipi == null && kutuTipi2.Trim() != "" && kutuTipi2 != null)
            {
                kutuTipi = kutuTipi2;
            }

            String cfiyati = donen["fiyati"].ToString();
            String beslemeyer = donen["beslemeyer"].ToString().Replace("\\", "\\\\");

            String miktari2 = ((donen["miktari2"] == null) ? "" : donen["miktari2"].ToString());
            String raf2 = (donen["raf2"] == null) ? "" : donen["raf2"].ToString();
            String cfiyati2 = (donen["fiyati2"] == null) ? "" : donen["fiyati2"].ToString();

            String hatalar = "";
            //String hatalar = donen["hatalar"].ToString();
            JObject jsonStr = JObject.Parse("{'Miktar':'" + miktari + "','Miktar2':'" + miktari2 + "','IlacKodu':'" + ilacKodu + "'," + "'beslemeyer':'" + beslemeyer + "'," + "'IlacAdi':'" + ilacAdi + "','Reyon':'" + reyon + "','Raf':'" + raf + "','Raf2':'" + raf2 + "','PaketMiadi':'" + paketMiadi + "','KutuTipi':'" + kutuTipi + "','Cfiyati':'" + cfiyati + "','Cfiyati2':'" + cfiyati2 + "','Hatalar':'" + hatalar + "'}");
            
            InsertText InsertLog = new InsertText(@"C:\net\AndroidLog.txt", "Barkot Okutma:\n"+"{'Miktar':'" + miktari + "','Miktar2':'" + miktari2 + "','IlacKodu':'" + ilacKodu + "'," + "'beslemeyer':'" + beslemeyer + "'," + "'IlacAdi':'" + ilacAdi + "','Reyon':'" + reyon + "','Raf':'" + raf + "','Raf2':'" + raf2 + "','PaketMiadi':'" + paketMiadi + "','KutuTipi':'" + kutuTipi + "','Cfiyati':'" + cfiyati + "','Cfiyati2':'" + cfiyati2 + "','Hatalar':'" + hatalar + "'}"+"\n ");

            //JObject jsonStr = JObject.Parse("{'Hatalar':'"+hatalar+"'}");
            return jsonStr;
        }

        // PUT: api/fxpBarkoddanGetir/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/fxpBarkoddanGetir/5
        public void Delete(int id)
        {
        }
    }
}
