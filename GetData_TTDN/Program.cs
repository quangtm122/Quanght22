using System;
using HtmlAgilityPack;
using System.Net.Http;
using System.Threading.Tasks;
using System.Linq;
using System.Collections.Generic;
using System.Net;

namespace GetData_TTDN
{
    class Program
    {
        static void Main(string[] args)
        {
            _ = CrawData();
            Console.ReadLine();
        }
        private static async Task CrawData()
        {

            using (var httpClient = new HttpClient())
            {
                var uri = new Uri("http://192.168.68.72:8888/");
                httpClient.BaseAddress = uri;
                httpClient.DefaultRequestHeaders.Add("Cookie", "__RequestVerificationToken=wb_uhdiDl8kgyPdHp2Znv5_RcvrSkUkwZG5XjLv7_gh5rUj9sOE0qOWI9hFni8qfaOLJ67OVj2XyY7jX8JQhybza_LfYAyr-mVm51osQtmM1; Cookie1=EE1F92A290CA15C6C7EFA8C23CDEE51C4AA6B4B167928F4982CB886C0F780C7BD74A4DF6808BA14C6F92EDBF6D991FC8D176140F19D790927D769B4D4A061E19C282F8530165AD3C61B52857769C369DE89C12DF853E22D6CF6C6AFB761FF8BA232EA63C221A61F35B95093CD1DDA6EFF9293B9AFC5FAEE05CCD4B989EA13495BDE2C922235F82D04E3341107A924464E7A4B61298CC4E98B89F4CA9A82A608C8CA07C7913D15590F6F7D0C2A4BB9F4C924C101245948E17338653084E8DB740EE9D34C2CCDBE4F7E09FBF0F850EC1B3");
                var response = httpClient.GetStringAsync(uri).Result;
                var htmlDocument = new HtmlDocument();
                htmlDocument.LoadHtml(response);

                var thongtins = new List<ThongTin>();
              
                /*var divs = htmlDocument.DocumentNode.Descendants("div")
                .Where(node => node.GetAttributeValue("class", "").Equals("m-widget1")).ToList();*/
                var products = htmlDocument.DocumentNode.SelectNodes("//div[@class='m-widget1']");
                foreach (HtmlNode product in products)
                {
                    var thongtin = new ThongTin
                    {
                        MT_Name = product.SelectSingleNode("(//div[contains(@class,'col')]//h3)[1]").InnerText.Trim('\r', '\n',' '),
                        MT_HT = product.SelectSingleNode("(//div[contains(@class,'col')]//h3)[2]").InnerText.Trim('\r', '\n', ' '),
                        MT_CSLN = product.SelectSingleNode("(//div[contains(@class,'col')]//h3)[3]").InnerText.Trim('\r', '\n', ' '),
                        MT_TK = product.SelectSingleNode("(//div[contains(@class,'col')]//h3)[4]").InnerText.Trim('\r', '\n', ' '),
                        MT_SLN = product.SelectSingleNode("(//div[contains(@class,'col')]//h3)[5]").InnerText.Trim('\r', '\n', ' '),

                        Gio_Name = product.SelectSingleNode("(//div[contains(@class,'col')]//h3)[6]").InnerText.Trim('\r', '\n', ' '),
                        Gio_HT = product.SelectSingleNode("(//div[contains(@class,'col')]//h3)[7]").InnerText.Trim('\r', '\n', ' '),
                        Gio_CSLN = product.SelectSingleNode("(//div[contains(@class,'col')]//h3)[8]").InnerText.Trim('\r', '\n', ' '),
                        Gio_TK = product.SelectSingleNode("(//div[contains(@class,'col')]//h3)[9]").InnerText.Trim('\r', '\n', ' '),
                        Gio_SLN = product.SelectSingleNode("(//div[contains(@class,'col')]//h3)[10]").InnerText.Trim('\r', '\n', ' '),

                        SK_Name = product.SelectSingleNode("(//div[contains(@class,'col')]//h3)[11]").InnerText.Trim('\r', '\n', ' '),
                        SK_HT = product.SelectSingleNode("(//div[contains(@class,'col')]//h3)[12]").InnerText.Trim('\r', '\n', ' '),
                        SK_CSLN = product.SelectSingleNode("(//div[contains(@class,'col')]//h3)[13]").InnerText.Trim('\r', '\n', ' '),
                        SK_TK = product.SelectSingleNode("(//div[contains(@class,'col')]//h3)[14]").InnerText.Trim('\r', '\n', ' '),
                        SK_SLN = product.SelectSingleNode("(//div[contains(@class,'col')]//h3)[15]").InnerText.Trim('\r', '\n', ' '),

                        Tong_Name = product.SelectSingleNode("(//div[contains(@class,'col')]//h3)[16]").InnerText.Trim('\r', '\n', ' '),
                        Tong_HT = product.SelectSingleNode("(//div[contains(@class,'col')]//h3)[17]").InnerText.Trim('\r', '\n', ' '),
                        Tong_CSLN = product.SelectSingleNode("(//div[contains(@class,'col')]//h3)[18]").InnerText.Trim('\r', '\n', ' '),
                        Tong_TK = product.SelectSingleNode("(//div[contains(@class,'col')]//h3)[19]").InnerText.Trim('\r', '\n', ' '),
                        Tong_SLN = product.SelectSingleNode("(//div[contains(@class,'col')]//h3)[20]").InnerText.Trim('\r', '\n', ' ')

                   
                    };
                    thongtins.Add(thongtin);
                }

            }

        }
      }
    }


