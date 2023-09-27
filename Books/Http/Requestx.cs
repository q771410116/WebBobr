using HtmlAgilityPack;
using System;
using System.IO;
using System.Net;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace Books.Http
{
    internal class Requestx
    {
        public HtmlDocument Execute(string html_url)
        {
            if (string.IsNullOrEmpty(html_url))
            {
                return null;
            }

            Console.WriteLine("request: {0}", html_url);

            HttpWebRequest request = null;

            //如果是发送HTTPS请求 
            if (html_url.StartsWith("https", StringComparison.OrdinalIgnoreCase))
            {
                ServicePointManager.ServerCertificateValidationCallback = new RemoteCertificateValidationCallback(CheckValidationResult);
                //ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls | SecurityProtocolType.Ssl3;
                //ServicePointManager.SecurityProtocol = SecurityProtocolType.Ssl3 | SecurityProtocolType.Tls;
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;
                request = WebRequest.Create(html_url) as HttpWebRequest;
                request.ProtocolVersion = HttpVersion.Version10;
            }
            else
            {
                request = WebRequest.Create(html_url) as HttpWebRequest;
            }

            //Post请求方式
            request.ContentType = "text/html; charset=utf-8";
            request.Method = "GET";
            request.Accept = "text/html,application/xhtml+xml,application/xml;q=0.9,image/avif,image/webp,image/apng,*/*;q=0.8,application/signed-exchange;v=b3;q=0.7";
            request.Referer = "https://www.kujiang.com/";
            request.UserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/117.0.0.0 Safari/537.36";
            request.Headers.Add("Cookie", "Hm_lvt_7b96be8523c85ec46a3640cb8217de23=1695794144; fontFamily=null; fontColor=null; fontSize=null; bg=null; Xs_Readlist_Cookie=765; Xs_Novel_Cookie=%7Bnovel%3A%5B%7B%22novelname%22%3A%22%u7F51%u6E38%u5F00%u5C40%u83B7%u5F97%u795E%u7EA7%u5929%u8D4B%22%2C%22novelurl%22%3A%22/book/765/%22%2C%22chaptername%22%3A%22%u7B2C5%u7AE0%20%u9876%u7EA7%u5F3A%u5316%uFF0C+25%uFF01%22%2C%22chapterurl%22%3A%22/book/765/chapter/708594.html%22%7D%5D%7D; Hm_lpvt_7b96be8523c85ec46a3640cb8217de23=1695795819");


            // 获得响应流
            HttpWebResponse response = (System.Net.HttpWebResponse)request.GetResponse();

            if (response.StatusCode == HttpStatusCode.OK)
            {
                StreamReader myreader = new StreamReader(response.GetResponseStream(), Encoding.UTF8);
                // 获得数据
                string responseText = myreader.ReadToEnd();


                // 用htmlagilitypack 解析网页内容
                var doc = new HtmlAgilityPack.HtmlDocument();

                //加载html
                doc.LoadHtml(responseText);

                return doc;
            }
            else
            {
                return null;
            }

        }


        private static bool CheckValidationResult(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors errors)
        {
            return true; //总是接受 
        }
    }
}
