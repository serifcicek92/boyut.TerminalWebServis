using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace boyut.SayimDataAccess
{
    public class IlcIlaclar
    {
        public int Id { get; set; }
        public string Adi { get; set; }
        public int? Miktar { get; set; }
        public int Fiyat { get; set; }
        public string OzelKod2 { get; set; }
        public string Miad { get; set; }
        public int SeriNo { get; set; }
        public int Kodu { get; set; }

        public string Barkodu { get; set; }
        public string Raf { get; set; }
        public string Reyon { get; set; }
        public string KutuTipi { get; set; }

        //9 adet alan ILCILACLAR ve STK_URUNLER

    }
}
