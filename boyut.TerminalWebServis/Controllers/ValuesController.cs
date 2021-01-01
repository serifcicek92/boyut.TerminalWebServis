using boyut.SayimDataAccess;
using boyut.TerminalWebServis.ResourceAccess.Entities;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Results;

namespace boyut.TerminalWebServis.Controllers
{
    public class ValuesController : ApiController
    {

        private OracleConnection connection;
        private OracleCommand oracleCommand;
        private OracleDataReader oracleDataReader;
        private string query;
        private string query2;
        private string query3;
        public List<IlcIlaclar> ilaclar;
        //public List<IlacDBF> ilaclar;

        public ValuesController()
        {



            //Oracle Connection

            //connection = new OracleConnection();
            //connection.ConnectionString = "User Id = BOYUT_2008;Password=123;Data Source=192.168.0.10:1521/boyutdb2017";
            //connection.Open();

            Connection connection = new Connection();
            this.connection = connection.Conn();

            //STK_URUNLER.PARENTID = ILCILACLAR.ID
            //Command Tanımlamaları



            ilaclar = new List<IlcIlaclar>();


            //Queries
            query = "select * from STK_URUNLER where PARENTID = 985";
            query2 = "select * from ILCILACLAR where ID < 1070 and ID > 1060";
            query3 = "select " +
                        "ILC.ID AS ID," +
                        " MAX(ILC.ADI) AS ILCADI," +
                        " max(ILC.KODU) AS KODU," +
                        " max(ILC.BARKODU) AS BARKODU," +
                        " MAX(ILC.OZELKOD2) AS OZELKOD2," +
                        "MAX(ILC.DEPOCUFIYATI) AS DEPOCUFIYAT," +
                        "STK.SERINO," +
                        "STK.MIAD AS MIADI," +
                        "MAX(STK.REYON) AS REYON," +
                        "MAX(STK.RAF) AS RAF," +
                        "SUM(STK.MIKTAR) as MIKTAR " +
                "from ILCILACLAR ILC " +
                "LEFT JOIN STK_URUNLER " +
                "STK ON STK.PARENTET = 34 and STK.PARENTID = ilc.ID  AND STK.AKTIF = 1GROUP BY ILC.ID,STK.SERINO,STK.MIAD";



        }

        // GET api/values
        public IEnumerable<IlcIlaclar> Get()
        {

            oracleCommand = new OracleCommand(query3, this.connection);
            try
            {
                oracleDataReader = oracleCommand.ExecuteReader();
            }
            catch (OracleException e)
            {
                throw;
            }


            while (oracleDataReader.Read())
            {

                if (oracleDataReader.HasRows)
                {
                    int fyt;
                    if (oracleDataReader["DEPOCUFIYAT"].ToString() == "" || oracleDataReader["DEPOCUFIYAT"].ToString() == null)
                    {
                        fyt = 0;
                    }
                    else
                    {
                        fyt = int.Parse(oracleDataReader["DEPOCUFIYAT"].ToString().Split(',')[0]);
                    }


                    ilaclar.Add(new IlcIlaclar
                    {
                        Id = Convert.ToInt32(oracleDataReader["ID"]),
                        Adi = oracleDataReader["ILCADI"].ToString(),
                        Barkodu = oracleDataReader["BARKODU"].ToString(),
                        Kodu = int.Parse(oracleDataReader["KODU"].ToString()),
                        KutuTipi = oracleDataReader["OZELKOD2"].ToString(),
                        Fiyat = fyt,
                        Miad = oracleDataReader["MIADI"].ToString(),
                        Raf = oracleDataReader["RAF"].ToString(),
                        Reyon = oracleDataReader["REYON"].ToString()
                    });
                }

            }




            connection.Close();
            //return new string[] { "value1", "value2" };
            return ilaclar.ToList();
        }

        // GET api/values/5
        [Route("api/values/{barkod}")]
        public IEnumerable<IlcIlaclar> Get(string barkod)
        {
            query3 = "select " +
                        "ILC.ID AS ID," +
                        " MAX(ILC.ADI) AS ILCADI," +
                        " max(ILC.KODU) AS KODU," +
                        " max(ILC.BARKODU) AS BARKODU," +
                        " MAX(ILC.OZELKOD2) AS OZELKOD2," +
                        "MAX(ILC.DEPOCUFIYATI) AS DEPOCUFIYAT," +
                        "STK.SERINO," +
                        "NVL(STK.MIAD,' ') as MIAD," +
                        "MAX(STK.REYON) AS REYON," +
                        "MAX(STK.RAF) AS RAF," +
                        "SUM(NVL(STK.MIKTAR,0)) as MIKTAR " +
                "from ILCILACLAR ILC " +
                "LEFT JOIN STK_URUNLER " +
                "STK ON STK.PARENTET = 34 and STK.PARENTID = ilc.ID  AND STK.AKTIF = 1 " +
                "WHERE BARKODU = '8699514091523'" +
                " GROUP BY ILC.ID,STK.SERINO,STK.MIAD";

           String query4 = "select " +
                        "ILC.ID AS ID," +
                        " MAX(ILC.ADI) AS ILCADI," +
                        " max(ILC.KODU) AS KODU," +
                        " max(ILC.BARKODU) AS BARKODU," +
                        " MAX(ILC.OZELKOD2) AS OZELKOD2," +
                        "MAX(ILC.DEPOCUFIYATI) AS DEPOCUFIYAT," +
                        "STK.SERINO," +
                        "NVL(STK.MIAD,' ') as MIAD," +
                        "MAX(STK.REYON) AS REYON," +
                        "MAX(STK.RAF) AS RAF," +
                        "SUM(NVL(STK.MIKTAR,0)) as MIKTAR " +
                "from ILCILACLAR ILC " +
                "LEFT JOIN STK_URUNLER " +
                "STK ON STK.PARENTET = 34 and STK.PARENTID = ilc.ID  AND STK.AKTIF = 1 " +
                "WHERE BARKODU = :barkod" +
                " GROUP BY ILC.ID,STK.SERINO,STK.MIAD";

            String query5 = "select * from ILCILACLAR where BARKODU = :barkod";
            oracleCommand = new OracleCommand(query4, connection);
            oracleCommand.BindByName = true;
            oracleCommand.Parameters.Add(new OracleParameter("barkod", barkod));

            OracleDataReader dr = oracleCommand.ExecuteReader();
            while (dr.Read())
            {
                if (dr.HasRows)
                {
                    int? miktar;
                    if (dr["MIKTAR"].ToString() == null || dr["MIKTAR"].ToString() == "")
                    {
                        miktar = null;
                    }
                    else { miktar = Convert.ToInt32(dr["MIKTAR"].ToString()); }

                    ilaclar.Add(new IlcIlaclar
                    {
                        Id = Convert.ToInt32(dr["ID"]),
                        Adi = dr["ILCADI"].ToString(),
                        Barkodu = dr["BARKODU"].ToString(),
                        Kodu = Convert.ToInt32(dr["KODU"].ToString()),
                        OzelKod2 = dr["OZELKOD2"].ToString(),
                        Fiyat = Convert.ToInt32(dr["DEPOCUFIYAT"].ToString()),
                        Raf = dr["RAF"].ToString(),
                        Reyon = dr["REYON"].ToString(),
                        Miktar = miktar,
                        Miad = dr["MIAD"].ToString()
                    });
                }

            }

            connection.Close();

            return ilaclar.ToList();
        }

        // POST api/values
        public void Post([FromBody]string value)
        {
            //var formData = GetFormData();
            //var userId = formData["userId"];
        }

        // PUT api/values/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        public void Delete(int id)
        {
        }
    }
}
