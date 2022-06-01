using Boyut.CommonLibrary;
using Npgsql;
using NpgsqlTypes;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace boyut.DataLayer
{
    public class SIP_SEVKIYATLAR_PG
    {
        public void UpdatePaketDurum(string paketKodu, string faturaNo, string subekodu, string takipno, string faturatarihi, string durum, int AKTIFYIL)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required, new TimeSpan(2, 0, 0)))
                {
                    using (NpgsqlConnection conn = new NpgsqlConnection(ApplicationSettings.GetConnectionStringPG(AKTIFYIL)))
                    {
                        conn.Open();
                        using (NpgsqlCommand cmd = conn.CreateCommand())
                        {
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.CommandText = "boyut_2008.sip_sevkiyat$up_paketdurum";
                            if (!paketKodu.Equals("") && !faturaNo.Equals("") && !faturatarihi.Equals(""))
                            {
                                cmd.Parameters.Clear();
                                cmd.Parameters.AddWithValue("lpaketkodu", NpgsqlDbType.Text, paketKodu);
                                cmd.Parameters.AddWithValue("lfaturano", NpgsqlDbType.Text, faturaNo);
                                cmd.Parameters.AddWithValue("lsubekodu", NpgsqlDbType.Text, subekodu);
                                cmd.Parameters.AddWithValue("ltakipno", NpgsqlDbType.Text, takipno);
                                cmd.Parameters.AddWithValue("lfaturatarihi", NpgsqlDbType.Timestamp, faturatarihi.ToDateTime());
                                cmd.Parameters.AddWithValue("ldurum", NpgsqlDbType.Text, durum);
                                cmd.ExecuteNonQuery();
                            }
                        }
                    }
                    ts.Complete();
                }
            }
            catch (NpgsqlException exp)
            {
                throw ExceptionHelper.GetException(exp.Message, exp.ErrorCode);
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
                using (NpgsqlConnection conn = new NpgsqlConnection(ApplicationSettings.GetConnectionStringPG(AKTIFYIL)))
                {
                    conn.Open();
                    using (NpgsqlCommand cmd = new NpgsqlCommand("boyut_2008.sip_sevkiyat$sel_paketdurumlari"))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("lhesapkodu", NpgsqlDbType.Text, eczanekodu);
                        cmd.Parameters.AddWithValue("lsubekodu", NpgsqlDbType.Text, subeno);
                        using (NpgsqlDataAdapter da = new NpgsqlDataAdapter())
                        {
                            da.Fill(result, "SIP_PAKETDURUMLARI");
                        }
                    }
                }
            }
            catch (NpgsqlException exp)
            {
                throw ExceptionHelper.GetException(exp.Message, exp.ErrorCode);
            }
            catch (Exception exp)
            {
                throw exp;
            }
            DataHelper.SerializationHelper(result);
            return result;
        }
    }
}
