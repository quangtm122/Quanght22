using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GetData_TTDN
{

    class ThongTin
    {

        public string MT_Name { get; set; }
        public string MT_HT { get; set; }
        public string MT_CSLN { get; set; }
        public string MT_TK { get; set; }
        public string MT_SLN { get; set; }
        public string Gio_Name { get; set; }
        public string Gio_HT { get; set; }
        public string Gio_CSLN { get; set; }
        public string Gio_TK { get; set; }
        public string Gio_SLN { get; set; }
        public string SK_Name { get; set; }
        public string SK_HT { get; set; }
        public string SK_CSLN { get; set; }
        public string SK_TK { get; set; }
        public string SK_SLN { get; set; }
        public string Tong_Name { get; set; }
        public string Tong_HT { get; set; }
        public string Tong_CSLN { get; set; }
        public string Tong_TK { get; set; }
        public string Tong_SLN { get; set; }
    
        public override string ToString()
        {
            return "\n\t" + MT_Name + ": " + MT_HT + "; " + MT_CSLN + "; " + MT_TK + "; " + MT_SLN + ";" +
                                 "\n\t" + Gio_Name + ": " + Gio_HT + "; " + Gio_CSLN + "; " + Gio_TK + ";" + "" + Gio_SLN + "" +
                                 "\n\t" + SK_Name + ": " + SK_HT + "; " + SK_CSLN + "; " + "" + SK_TK + "; " + SK_SLN + "" +
                                 "\n\t" + Tong_Name + ": " + Tong_HT + ";" + Tong_CSLN + ";" + Tong_TK + ";" + Tong_SLN;
        }
    }
}
