using GetData_TTDN;
using HtmlAgilityPack;
using log4net;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace Test2schedule
{
    public class Excute
    {
        private static readonly ILog log = LogManager.GetLogger(typeof(Excute));
        private readonly Timer time;
        public Excute()
        {
            time = new Timer(5000) { AutoReset = true };
            time.Elapsed += TimerElapsed;

        }

        private async void TimerElapsed(object sender, ElapsedEventArgs e)
        {
            using (var httpClient = new HttpClient())
            {
                string token = "";
                string urltoken = "http://192.168.68.72:8888/xac-thuc/dang-nhap/";
                using (HttpClient client = new HttpClient())
                {
                    HttpResponseMessage message = client.GetAsync(urltoken).Result;
                    if (message.IsSuccessStatusCode)
                    {
                            string data = message.Content.ReadAsStringAsync().Result;
                            var htmlDocument = new HtmlDocument();
                            htmlDocument.LoadHtml(data);
                            var point = htmlDocument.DocumentNode.SelectSingleNode("//form[@id='login-form']/input");
                            token = point.Attributes["value"].Value;

                            client.DefaultRequestHeaders.Add("Referer", "http://192.168.68.72:8888/xac-thuc/dang-nhap");

                            List<KeyValuePair<string, string>> param = new List<KeyValuePair<string, string>>()
                            {
                            new KeyValuePair<string, string>("__RequestVerificationToken", token),
                            new KeyValuePair<string, string>("Username", "admin"),
                            new KeyValuePair<string, string>("Password", "123"),
                            };
                        FormUrlEncodedContent form = new FormUrlEncodedContent(param);
                        HttpResponseMessage message1 = client.PostAsync(urltoken, form).Result;
                        string result = message1.Content.ReadAsStringAsync().Result;
                        htmlDocument.LoadHtml(result);

                        var thongtins = new List<ThongTin>();

                        //LẤY DỮ LIỆU TỪ WEB
                        var products = htmlDocument.DocumentNode.SelectNodes("//div[@class='m-widget1']");
                        ThongTin tt = new ThongTin();
                        foreach (HtmlNode product in products)
                        {
 
                            tt.MT_Name = product.SelectSingleNode("(//div[contains(@class,'col')]//h3)[1]").InnerText.Trim('\r', '\n', ' ');
                            tt.MT_HT = product.SelectSingleNode("(//div[contains(@class,'col')]//h3)[2]").InnerText.Replace("\r", String.Empty).Replace("\n", String.Empty).Replace(" ", String.Empty);
                            tt.MT_CSLN = product.SelectSingleNode("(//div[contains(@class,'col')]//h3)[3]").InnerText.Replace("\r", String.Empty).Replace("\n", String.Empty).Replace(" ", String.Empty);
                            tt.MT_TK = product.SelectSingleNode("(//div[contains(@class,'col')]//h3)[4]").InnerText.Replace("\r", String.Empty).Replace("\n", String.Empty).Replace(" ", String.Empty);
                            tt.MT_SLN = product.SelectSingleNode("(//div[contains(@class,'col')]//h3)[5]").InnerText.Replace("\r", String.Empty).Replace("\n", String.Empty).Replace(" ", String.Empty);

                            tt.Gio_Name = product.SelectSingleNode("(//div[contains(@class,'col')]//h3)[6]").InnerText.Replace("\r", String.Empty).Replace("\n", String.Empty).Replace(" ", String.Empty);
                            tt.Gio_HT = product.SelectSingleNode("(//div[contains(@class,'col')]//h3)[7]").InnerText.Replace("\r", String.Empty).Replace("\n", String.Empty).Replace(" ", String.Empty);
                            tt.Gio_CSLN = product.SelectSingleNode("(//div[contains(@class,'col')]//h3)[8]").InnerText.Replace("\r", String.Empty).Replace("\n", String.Empty).Replace(" ", String.Empty);
                            tt.Gio_TK = product.SelectSingleNode("(//div[contains(@class,'col')]//h3)[9]").InnerText.Replace("\r", String.Empty).Replace("\n", String.Empty).Replace(" ", String.Empty);
                            tt.Gio_SLN = product.SelectSingleNode("(//div[contains(@class,'col')]//h3)[10]").InnerText.Replace("\r", String.Empty).Replace("\n", String.Empty).Replace(" ", String.Empty);

                            tt.SK_Name = product.SelectSingleNode("(//div[contains(@class,'col')]//h3)[11]").InnerText.Replace("\r", String.Empty).Replace("\n", String.Empty).Replace(" ", String.Empty);
                            tt.SK_HT = product.SelectSingleNode("(//div[contains(@class,'col')]//h3)[12]").InnerText.Replace("\r", String.Empty).Replace("\n", String.Empty).Replace(" ", String.Empty);
                            tt.SK_CSLN = product.SelectSingleNode("(//div[contains(@class,'col')]//h3)[13]").InnerText.Replace("\r", String.Empty).Replace("\n", String.Empty).Replace(" ", String.Empty);
                            tt.SK_TK = product.SelectSingleNode("(//div[contains(@class,'col')]//h3)[14]").InnerText.Replace("\r", String.Empty).Replace("\n", String.Empty).Replace(" ", String.Empty);
                            tt.SK_SLN = product.SelectSingleNode("(//div[contains(@class,'col')]//h3)[15]").InnerText.Replace("\r", String.Empty).Replace("\n", String.Empty).Replace(" ", String.Empty);

                            tt.Tong_Name = product.SelectSingleNode("(//div[contains(@class,'col')]//h3)[16]").InnerText.Replace("\r", String.Empty).Replace("\n", String.Empty).Replace(" ", String.Empty);
                            tt.Tong_HT = product.SelectSingleNode("(//div[contains(@class,'col')]//h3)[17]").InnerText.Replace("\r", String.Empty).Replace("\n", String.Empty).Replace(" ", String.Empty);
                            tt.Tong_CSLN = product.SelectSingleNode("(//div[contains(@class,'col')]//h3)[18]").InnerText.Replace("\r", String.Empty).Replace("\n", String.Empty).Replace(" ", String.Empty);
                            tt.Tong_TK = product.SelectSingleNode("(//div[contains(@class,'col')]//h3)[19]").InnerText.Replace("\r", String.Empty).Replace("\n", String.Empty).Replace(" ", String.Empty);
                            tt.Tong_SLN = product.SelectSingleNode("(//div[contains(@class,'col')]//h3)[20]").InnerText.Replace("\r", String.Empty).Replace("\n", String.Empty).Replace(" ", String.Empty);       
                        }
                        Console.Write("Sucess!!! " + "At:" + DateTime.Now + "\n");

                        try
                        {
                            log.Info("Lấy dữ liệu thành công \n " + tt.ToString() + "\n");
                        }
                        catch (Exception ex)
                        {

                            log.Error(ex.Message);
                        }

                        string MyConnection = "Data Source=ADMIN\\SQLEXPRESS;Initial Catalog=TrainingCrawler;User ID=sa;Password=123";
                        SqlConnection conn = new SqlConnection(MyConnection);
                        conn.Open();

                        DateTime dateTimeVariable = DateTime.Now;
                        using (SqlCommand sqlCmd1 = new SqlCommand { CommandText = "INSERT INTO Detail ([HienTai], [CongSuatLon], [ThietKe], [SanLuongNgay], [EnergyName_Id], [Time]) VALUES (@HienTai, @CongSuatLon, @ThietKe, @SanLuongNgay, @EnergyName_Id, @Time)", Connection = conn })
                        {
                            sqlCmd1.Parameters.AddWithValue("@HienTai", tt.MT_HT);
                            sqlCmd1.Parameters.AddWithValue("@CongSuatLon", tt.MT_CSLN);
                            sqlCmd1.Parameters.AddWithValue("@ThietKe", tt.MT_TK);
                            sqlCmd1.Parameters.AddWithValue("@SanLuongNgay", tt.MT_SLN);
                            sqlCmd1.Parameters.AddWithValue("@EnergyName_Id", '1');
                            sqlCmd1.Parameters.AddWithValue("@time", dateTimeVariable);
                            sqlCmd1.ExecuteNonQuery();
                        }
                        using (SqlCommand sqlCmd2 = new SqlCommand { CommandText = "INSERT INTO Detail ([HienTai], [CongSuatLon], [ThietKe], [SanLuongNgay], [EnergyName_Id], [Time]) VALUES (@HienTai, @CongSuatLon, @ThietKe, @SanLuongNgay, @EnergyName_Id, @Time)", Connection = conn })
                        {
                            sqlCmd2.Parameters.AddWithValue("@HienTai", tt.Gio_HT);
                            sqlCmd2.Parameters.AddWithValue("@CongSuatLon", tt.Gio_CSLN);
                            sqlCmd2.Parameters.AddWithValue("@ThietKe", tt.Gio_TK);
                            sqlCmd2.Parameters.AddWithValue("@SanLuongNgay", tt.Gio_SLN);
                            sqlCmd2.Parameters.AddWithValue("@EnergyName_Id", '2');
                            sqlCmd2.Parameters.AddWithValue("@time", dateTimeVariable);
                            sqlCmd2.ExecuteNonQuery();
                        }
                        using (SqlCommand sqlCmd3 = new SqlCommand { CommandText = "INSERT INTO Detail ([HienTai], [CongSuatLon], [ThietKe], [SanLuongNgay], [EnergyName_Id], [Time]) VALUES (@HienTai, @CongSuatLon, @ThietKe, @SanLuongNgay, @EnergyName_Id, @Time)", Connection = conn })
                        {
                            sqlCmd3.Parameters.AddWithValue("@HienTai", tt.SK_HT);
                            sqlCmd3.Parameters.AddWithValue("@CongSuatLon", tt.SK_CSLN);
                            sqlCmd3.Parameters.AddWithValue("@ThietKe", tt.SK_TK);
                            sqlCmd3.Parameters.AddWithValue("@SanLuongNgay", tt.SK_SLN);
                            sqlCmd3.Parameters.AddWithValue("@EnergyName_Id", '3');
                            sqlCmd3.Parameters.AddWithValue("@time", dateTimeVariable);
                            sqlCmd3.ExecuteNonQuery();
                        }
                        using (SqlCommand sqlCmd4 = new SqlCommand { CommandText = "INSERT INTO Detail ([HienTai], [CongSuatLon], [ThietKe], [SanLuongNgay], [EnergyName_Id], [Time]) VALUES (@HienTai, @CongSuatLon, @ThietKe, @SanLuongNgay, @EnergyName_Id, @Time)", Connection = conn })
                        {
                            sqlCmd4.Parameters.AddWithValue("@HienTai", tt.Tong_HT);
                            sqlCmd4.Parameters.AddWithValue("@CongSuatLon", tt.Tong_CSLN);
                            sqlCmd4.Parameters.AddWithValue("@ThietKe", tt.Tong_TK);
                            sqlCmd4.Parameters.AddWithValue("@SanLuongNgay", tt.Tong_SLN);
                            sqlCmd4.Parameters.AddWithValue("@EnergyName_Id", '4');
                            sqlCmd4.Parameters.AddWithValue("@time", dateTimeVariable);
                            sqlCmd4.ExecuteNonQuery();
                        }
                        conn.Close();
                    }
                }
            }
        }

        public void Start()
        {
            time.Start();
            log.Info("Start success!");
        }

        public void Stop()
        {
            time.Stop();
            log.Info("Stop success!");
        }
    }
}
