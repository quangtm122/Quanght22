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
        public string HienTai { get; set; }
        public string CongSuatLn { get; set; }
        public string ThietKe { get; set; }
        public string SanLuongNgay { get; set; }

        public DateTime Time { get; set; }

        public override string ToString()
        {
            return "\n\t" + Name + ": " + HienTai + "; " + CongSuatLn + "; " + ThietKe + "; " + SanLuongNgay;
                                 
        }
    }
}
