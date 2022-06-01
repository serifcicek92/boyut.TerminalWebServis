using boyut.DataLayer;
using Boyut.CommonLibrary;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace boyut.BusinessLayer
{
    public class SIP_SEVKIYATLARDsp
    {
        public void UpdatePaketDurum(string paketKodu, string faturaNo, string subekodu, string takipno, string faturatarihi, string durum, UserContext uc)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required, new TimeSpan(2, 0, 0)))
                {
                    SIP_SEVKIYATLAR sevkiyatlarDL = new SIP_SEVKIYATLAR();
                    sevkiyatlarDL.UpdatePaketDurum(paketKodu, faturaNo, subekodu, takipno, faturatarihi, durum, uc.AKTIFYIL);
                    if (ConfigurationManager.AppSettings["PostgreGecisSIP_"].ToString().Equals("1"))
                    {
                        SIP_SEVKIYATLAR_PG sevkiyatlarPGDL = new SIP_SEVKIYATLAR_PG();
                        sevkiyatlarPGDL.UpdatePaketDurum(paketKodu, faturaNo, subekodu, takipno, faturatarihi, durum, uc.AKTIFYIL);
                    }
                    ts.Complete();
                }
            }
            catch (Exception exp)
            {
                throw ExceptionHelper.GetUserException(exp, uc.AKTIFDIL);
            }

        }
        public DataSet getPaketDurumlari(string eczanekodu, string subeno, UserContext uc)
        {
            try
            {
                if (ConfigurationManager.AppSettings["SIPDBSELWITHPG"].ToString().Equals("0"))
                {
                    SIP_SEVKIYATLAR sevkiyatlarDL = new SIP_SEVKIYATLAR();
                    return sevkiyatlarDL.getPaketDurumlari(eczanekodu, subeno, uc.AKTIFYIL);
                }
                else
                {
                    SIP_SEVKIYATLAR_PG sevkiyatlarPGDL = new SIP_SEVKIYATLAR_PG();
                    return sevkiyatlarPGDL.getPaketDurumlari(eczanekodu, subeno, uc.AKTIFYIL);
                }
            }
            catch (Exception exp)
            {
                throw ExceptionHelper.GetUserException(exp, uc.AKTIFDIL);
            }
        }
    }
}
