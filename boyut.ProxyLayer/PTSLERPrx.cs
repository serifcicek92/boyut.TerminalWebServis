using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace boyut.ProxyLayer
{
    public class PTSLERPrx
    {
        public static DataSet GetData(string paketBarkodu)
        {
            try
            {
                //DataSet _mn = Activator.GetObject(typeof(IATS_ACENTELERDsp), ApplicationSettings.GetServiceURL() + "ATS_ACENTELERDsp");
                //return _mn.GetData(ds, ClientSecurity.UserInfo);
                return null;
            }
            catch (Exception exp)
            {
                throw exp;
            }
        }
    }
}
