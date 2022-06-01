using Boyut.CommonLibrary;
using Oracle.DataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace boyut.DataLayer
{
    public class SIP_SEVKIYATLAR
    {
        public void UpdatePaketDurum(string paketKodu, string faturaNo, string subekodu, string takipno, string faturatarihi, string durum, int AKTIFYIL)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required, new TimeSpan(2, 0, 0)))
                {
                    using (OracleConnection conn = new OracleConnection(ApplicationSettings.GetConnectionString(AKTIFYIL)))
                    {
                        conn.Open();
                        using (OracleCommand cmd = conn.CreateCommand())
                        {
                            if (!paketKodu.Equals("") && !faturaNo.Equals("") && !faturatarihi.Equals(""))
                            {
                                cmd.CommandType = CommandType.StoredProcedure;
                                cmd.BindByName = true;
                                cmd.CommandText = "SIP_SEVKIYAT.UP_PAKETDURUM";
                                cmd.Parameters.Clear();
                                cmd.Parameters.Add("LPAKETKODU", paketKodu);
                                cmd.Parameters.Add("LFATURANO", faturaNo);
                                cmd.Parameters.Add("LSUBEKODU", subekodu);
                                cmd.Parameters.Add("LTAKIPNO", takipno);
                                cmd.Parameters.Add("LFATURATARIHI", faturatarihi.ToDateTime());
                                cmd.Parameters.Add("LDURUM", durum);
                                cmd.ExecuteNonQuery();
                            }
                        }
                    }
                    ts.Complete();
                }
            }
            catch (OracleException exp)
            {
                throw ExceptionHelper.GetException(exp.Message, exp.Number);
            }
            catch (Exception exp)
            {
                throw exp;
            }
        }

        public DataSet getPaketDurumlari(string eczanekodu, string subeno, int AKTIFYIL)
        {
            DataSet result = new DataSet();

            try
            {
                TransactionOptions transactionOptions = new TransactionOptions { IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted, Timeout = new TimeSpan(2, 0, 0) };
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required, transactionOptions))
                {
                    using (OracleConnection conn = new OracleConnection(ApplicationSettings.GetConnectionString(AKTIFYIL)))
                    {
                        conn.Open();
                        using (OracleCommand cmd = conn.CreateCommand())
                        {
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.BindByName = true;

                            cmd.CommandText = "SIP_SEVKIYAT.SEL_PAKETDURUMLARI";
                            cmd.Parameters.Add(new OracleParameter("ResCur", OracleDbType.RefCursor, ParameterDirection.Output));
                            cmd.Parameters.Add("LHESAPKODU", eczanekodu);
                            cmd.Parameters.Add("LSUBEKODU", subeno);

                            using (OracleDataAdapter da = new OracleDataAdapter(cmd))
                            {
                                da.Fill(result);
                            }
                        }
                    }
                    ts.Complete();
                }
            }
            catch (OracleException exp)
            {
                throw ExceptionHelper.GetException(exp.Message, exp.Number);
            }
            catch (Exception exp)
            {
                throw exp;
            }

            return result;
        }
    }
}
