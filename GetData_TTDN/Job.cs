using HtmlAgilityPack;
using log4net;
using Microsoft.Extensions.Configuration;
using Quartz;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace GetData_TTDN
{
    class Job : IJob
    {
        private static HttpClient client;
        private static readonly ILog log = LogManager.GetLogger(typeof(Job));
        public Task Execute(IJobExecutionContext context)
        {

            CrawlData();
            return Task.CompletedTask;
        }
        private async Task CrawlData()
        {
            log4net.Config.XmlConfigurator.ConfigureAndWatch(new FileInfo(AppDomain.CurrentDomain.BaseDirectory + "Log4net.config"));
            try
            {
                Uri uri = new Uri("http://192.168.68.72:8888/xac-thuc/dang-nhap/");
                CookieContainer cookies = new CookieContainer();
                HttpClientHandler handler = new HttpClientHandler();
                handler.CookieContainer = cookies;
                client = new HttpClient(handler);
                client.BaseAddress = uri;
                HttpResponseMessage response = await client.GetAsync(uri);
                var result_login = await response.Content.ReadAsStringAsync();
                var htmlDocument = new HtmlDocument();
                htmlDocument.LoadHtml(result_login);

                var point = htmlDocument.DocumentNode.SelectSingleNode("//form[@id='login-form']/input");
                string token = point.Attributes["value"].Value;

                List<KeyValuePair<string, string>> param = new List<KeyValuePair<string, string>>()
                              {
                              new KeyValuePair<string, string>("__RequestVerificationToken", token),
                              new KeyValuePair<string, string>("Username", "admin"),
                              new KeyValuePair<string, string>("Password", "123"),
                              };
                FormUrlEncodedContent form = new FormUrlEncodedContent(param);
                HttpResponseMessage post = client.PostAsync(uri, form).Result;

                IEnumerable<Cookie> responseCookies = cookies.GetCookies(new Uri("http://192.168.68.72:8888/")).Cast<Cookie>();
                string cookie1;
                foreach (Cookie cookie in responseCookies)
                {
                    if (cookie.Name == "Cookie1") cookie1 = cookie.Value;
                }

                var resData = client.GetAsync("http://192.168.68.72:8888/nang-luong-tai-tao/trang-chu").Result;
                var result = resData.Content.ReadAsStringAsync().Result;
                htmlDocument.LoadHtml(result);

                var GetElment = htmlDocument.DocumentNode.SelectNodes("//div[@class='m-widget1']/div").ToList();

                DateTime dateTimeVariable = DateTime.Now;
                foreach (var RawData in GetElment)
                {
                    var name = RawData.SelectSingleNode(".//div[contains(@class,'row')]/div[1]/h3/a").InnerText.ToString().Replace("\r", String.Empty).Replace("\n", String.Empty).Replace(" ", String.Empty);
                    var ht = RawData.SelectSingleNode(".//div[contains(@class,'row')]/div[2]/h3").InnerText.ToString().Replace("\r", String.Empty).Replace("\n", String.Empty).Replace(" ", String.Empty).Replace("MW", String.Empty);
                    var csln = RawData.SelectSingleNode(".//div[contains(@class,'row')]/div[3]/h3").InnerText.ToString().Replace("\r", String.Empty).Replace("\n", String.Empty).Replace(" ", String.Empty).Replace("MW", String.Empty);
                    var tk = RawData.SelectSingleNode(".//div[contains(@class,'row')]/div[4]/h3").InnerText.ToString().Replace("\r", String.Empty).Replace("\n", String.Empty).Replace(" ", String.Empty).Replace("MW", String.Empty);
                    var sln = RawData.SelectSingleNode(".//div[contains(@class,'row')]/div[5]/h3").InnerText.ToString().Replace("\r", String.Empty).Replace("\n", String.Empty).Replace(" ", String.Empty).Replace("MWh", String.Empty);

                    int Hientai = int.Parse(ht);
                    int CongSuatLonNhat = int.Parse(csln);
                    int ThietKe = int.Parse(tk);
                    int SanLuongNgay = int.Parse(sln);
                    using (var context = new DataContext())
                    {
                        var tt = new ThongTin
                        {
                            Name = name.ToString(),
                            HienTai = Hientai,
                            CongSuatLn = CongSuatLonNhat,
                            ThietKe = ThietKe,
                            SanLuongNgay = SanLuongNgay,
                            Time = DateTime.Now,
                        };
                        context.Add<ThongTin>(tt);
                        context.SaveChanges();
                        log.Info("\t GET DATE SUCCESS: " + tt.ToString() + "\n");
                        Console.OutputEncoding = Encoding.UTF8;
                        Console.WriteLine("\n" + tt.ToString());
                    }
                }
            }
            catch(Exception ex)
            {
                log.Info("ERROR" +ex.Message);         
            }
            
        }
    }
}