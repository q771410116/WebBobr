using HtmlAgilityPack;
using System;
using System.Collections.Generic;

namespace Books.Http.Rule
{
    /// <summary>
    /// https://yazhouseba.co/SPA8n_2.htm
    /// </summary>
    internal class Rule1 : BaseRule
    {
        public Rule1()
        {
            tag = "llwma";
            max_page = 15;
        }


        public override string NextPage()
        {
            if (Index > max_page)
            {
                return "";
            }
            else
            {
                return string.Format("https://yazhouseba.co/1gDq0_{0}.htm", Index++);
            }
        }

        public override List<string> ParsePageList(HtmlDocument doc)
        {
            var htmlnode = doc.DocumentNode.SelectNodes("//div[@class='articleList']/p/a");

            string url = string.Empty;

            List<string> sounds = new List<string>();
            for (int i = 0; i < htmlnode.Count; i++)
            {
                url = htmlnode[i].GetAttributeValue("href", "");
                sounds.Add(String.Format("https://yazhouseba.co/{0}", url));
            }

            return sounds;


        }

        public override string ParseTitle(HtmlDocument doc)
        {
            // /html/body/div[1]/div/div[2]/h1
            var htmlnode = doc.DocumentNode.SelectSingleNode("//html/body/div[1]/div/div[2]/h1");

            if (htmlnode != null)
            {
                var text = htmlnode.InnerText;
                return text;
            }

            return "";
        }


        public override string ParseContent(HtmlDocument doc)
        {
            // /html/body/div[1]/div/div[2]/div[1]/div
            var htmlnode = doc.DocumentNode.SelectSingleNode("//html/body/div[1]/div/div[2]/div[1]/div");
            if (htmlnode != null)
            {
                var text = htmlnode.InnerText;
                var last = text.LastIndexOf("==记住==亚洲色吧网");
                if (last > 0)
                {
                    text = text.Substring(0, last);
                }
                return text;
            }
            return "";


        }


    }
}
