using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace boyut.TerminalWebServis.Models
{
    public class SvkBWServisSepetBilgi
    {
        public string PaketTipi { get; set; }
        public string PaketKodu { get; set; }
        public string PaketSayisi { get; set; }
        public string Hesapkodu { get; set; }
        public string FaturaNo { get; set; }
        public string FaturaTarihi { get; set; }

    }
}
//{"FaturaNo":"5K 1027853","FaturaTarihi":"29/07/2021","Hesapkodu":"1200900035","PaketKodu":"1194","PaketSayisi":"1","PaketTipi":"S"}