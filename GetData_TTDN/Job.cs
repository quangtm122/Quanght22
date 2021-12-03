using HtmlAgilityPack;
using log4net;
using Quartz;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace GetData_TTDN
{
    class Job : IJob
    {
        private static readonly ILog log = LogManager.GetLogger(typeof(Job));
        public Task Execute(IJobExecutionContext context)
        {
           
            CrawlData();
            return Task.CompletedTask;
        }
       
        private async Task CrawlData()
        {
            log4net.Config.XmlConfigurator.ConfigureAndWatch(new FileInfo(AppDomain.CurrentDomain.BaseDirectory + "Log4net.config"));
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

                        
                        var GetElment = htmlDocument.DocumentNode.SelectNodes("//div[@class='m-widget1']/div").ToList();
                        var thongtins = new List<ThongTin>();
                        DateTime dateTimeVariable = DateTime.Now;
                        foreach (var RawData in GetElment)
                        {
                            var name = RawData.SelectSingleNode(".//div[contains(@class,'row')]/div[1]/h3/a").InnerText.ToString().Replace("\r", String.Empty).Replace("\n", String.Empty).Replace(" ", String.Empty);
                            var hientai = RawData.SelectSingleNode(".//div[contains(@class,'row')]/div[2]/h3").InnerText.ToString().Replace("\r", String.Empty).Replace("\n", String.Empty).Replace(" ", String.Empty);
                            var csln = RawData.SelectSingleNode(".//div[contains(@class,'row')]/div[3]/h3").InnerText.ToString().Replace("\r", String.Empty).Replace("\n", String.Empty).Replace(" ", String.Empty);
                            var tk = RawData.SelectSingleNode(".//div[contains(@class,'row')]/div[4]/h3").InnerText.ToString().Replace("\r", String.Empty).Replace("\n", String.Empty).Replace(" ", String.Empty);
                            var sln = RawData.SelectSingleNode(".//div[contains(@class,'row')]/div[5]/h3").InnerText.ToString().Replace("\r", String.Empty).Replace("\n", String.Empty).Replace(" ", String.Empty);

                            using (var context = new DataContext())
                            {
                                var tt = new ThongTin
                                {
                                    Name = Regex.Replace(name, @"\s", ""),
                                    HienTai = Regex.Replace(hientai, @"\s", ""),
                                    CongSuatLn = Regex.Replace(csln, @"\s", ""),
                                    ThietKe = Regex.Replace(tk, @"\s", ""),
                                    SanLuongNgay = Regex.Replace(sln, @"\s", ""),
                                    Time = DateTime.Now,
                                };
                                context.Add<ThongTin>(tt);
                                context.SaveChanges();
                                log.Info("\t GET DATE SUCCESS: " + tt.ToString() + "\n");
                            } 
                        }
                        Console.WriteLine("CREATE SUCCESS AT: " + DateTime.Now);
                    }
                }
            }
        }
    }
}
