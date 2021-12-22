using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GetData_TTDN
{

    public partial class ThongTin
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int HienTai { get; set; }
        public int CongSuatLn { get; set; }
        public int ThietKe { get; set; }
        public int SanLuongNgay { get; set; }

        public DateTime Time { get; set; }

        public override string ToString()
        {
            return "\t" + Name + ": " + HienTai + "; " + CongSuatLn + "; " + ThietKe + "; " + SanLuongNgay;
                                 
        }

    }
}
