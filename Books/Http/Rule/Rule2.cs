using HtmlAgilityPack;
using System.Collections.Generic;

namespace Books.Http.Rule
{
    /// <summary>
    /// https://www.chazidian.com/dict/w/f_xiaoxuecihui/
    /// </summary>
    internal class Rule2 : BaseRule
    {
        public Rule2()
        {
            tag = "llwma";
            max_page = 20;
        }


        public override string NextPage()
        {
            if (Index > max_page)
            {
                return "";
            }
            else
            {
                return string.Format("https://www.chazidian.com/dict/w/f_xiaoxuecihui/?page={0}", Index++);
            }
        }

        public override List<string> ParsePageList(HtmlDocument doc)
        {
            var htmlnode = doc.DocumentNode.SelectNodes("//html/body/div[2]/div/div/div[2]/div[2]/ul/li");

            string url = string.Empty;

            List<string> sounds = new List<string>();
            for (int i = 0; i < htmlnode.Count; i++)
            {
                var node = htmlnode[i].SelectSingleNode("//a");
                url = htmlnode[i].FirstChild.InnerText;
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
