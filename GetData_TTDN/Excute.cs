/*using GetData_TTDN;
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
using System.Text.RegularExpressions;
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

        private void TimerElapsed(object sender, ElapsedEventArgs e)
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


                        var GetElment = htmlDocument.DocumentNode.SelectNodes("//div[@class='m-widget1']/div").ToList();
                        var thongtins = new List<ThongTin>();
                        DateTime dateTimeVariable = DateTime.Now;
                        Console.WriteLine("CREATE SUCCESS AT: " + DateTime.Now);
                        foreach (var RawData in GetElment)
                        {
                            var name = RawData.SelectSingleNode(".//div[contains(@class,'row')]/div[1]/h3/a").InnerText.ToString().Replace("\r", String.Empty).Replace("\n", String.Empty).Replace(" ", String.Empty);
                            var hientai = RawData.SelectSingleNode(".//div[contains(@class,'row')]/div[2]/h3").InnerText.ToString().Replace("\r", String.Empty).Replace("\n", String.Empty).Replace(" ", String.Empty);
                            var csln = RawData.SelectSingleNode(".//div[contains(@class,'row')]/div[3]/h3").InnerText.ToString().Replace("\r", String.Empty).Replace("\n", String.Empty).Replace(" ", String.Empty);
                            var tk = RawData.SelectSingleNode(".//div[contains(@class,'row')]/div[4]/h3").InnerText.ToString().Replace("\r", String.Empty).Replace("\n", String.Empty).Replace(" ", String.Empty);
                            var sln = RawData.SelectSingleNode(".//div[contains(@class,'row')]/div[5]/h3").InnerText.ToString().Replace("\r", String.Empty).Replace("\n", String.Empty).Replace(" ", String.Empty);


                         *//*   var tt = new ThongTin
                            {
                                Name = name,
                                HienTai = hientai,
                                CongSuatLn = csln,
                                ThietKe = tk,
                                SanLuongNgay = sln,
                            };
                            thongtins.Add(tt);*//*
                       
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
                               *//* log.InfoFormat(soLieu.ToString(), Encoding.UTF8);*//*
                            }
                            
                        }
                       
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
*/