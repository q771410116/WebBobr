using Books.Domain;
using Books.Http.Write;
using HtmlAgilityPack;
using System.Collections.Generic;
using System.Text;

namespace Books.Http.Rule
{
    /// <summary>
    /// 小说爬取
    /// https://www.aishangba8.com/
    /// </summary>
    public class AishangbaRule : IRule
    {
        public int Index { get; set; }


        /// <summary>
        /// 获取文章列表页链接
        /// </summary>
        /// <returns></returns>
        public string GetCatalogueUrl()
        {
            return "https://www.aishangba8.com/102_102537/";
        }

        /// <summary>
        /// 解析所有文章列表
        /// </summary>
        public List<string> ParsePageList(HtmlDocument doc)
        {
            //return new List<string>() { "https://www.aishangba8.com/102_102537/8234125.html" };

            var htmlnode = doc.DocumentNode.SelectNodes("/html/body/div/div[6]/div/dl/dd");

            List<string> sounds = new List<string>();
            for (int i = 0; i < htmlnode.Count; i++)
            {
                var node = htmlnode[i].FirstChild;
                string url = node.GetAttributeValue("href", "");

                sounds.Add("https://www.aishangba8.com" + url);
            }

            return sounds;
        }

        /// <summary>
        /// 解析页面标题
        /// </summary>
        /// <param name="doc"></param>
        /// <returns></returns>
        public string ParseTitle(HtmlDocument doc)
        {
            var htmlnode = doc.DocumentNode.SelectSingleNode("/html/body/div/div[5]/div/div[2]/h1");

            if (htmlnode != null)
            {
                var text = htmlnode.InnerText;
                return text;
            }

            return "";
        }

        /// <summary>
        /// 解析内容
        /// </summary>
        /// <param name="doc"></param>
        /// <returns></returns>
        public string ParseContent(HtmlDocument doc)
        {
            // /html/body/div[1]/div/div[2]/div[1]/div
            var htmlnode = doc.DocumentNode.SelectSingleNode("/html/body/div/div[5]/div/div[3]");

            if (htmlnode != null && htmlnode.HasChildNodes)
            {
                StringBuilder sb = new StringBuilder();

                foreach (var node in htmlnode.ChildNodes)
                {
                    if (node.InnerText.StartsWith("由于各种问题"))
                    {
                        break;
                    }

                    if (!string.IsNullOrEmpty(node.InnerText))
                    {
                        sb.Append(node.InnerText);
                        sb.Append("\n");
                    }
                }

                return sb.ToString();
            }

            return "";
        }

        public IWrite GetWriteModel()
        {
            return new AishangbaWrite();
        }
    }



    public class AishangbaWrite : IWrite
    {
        public int Index { get; set; }
        public int Total { get; set; }
        public List<Chapter> DataList { get; set; }
        public List<string> TitleList { get; set; }

        public AishangbaWrite()
        {
            DataList = new List<Chapter>();
        }

        public Chapter GetChapter()
        {
            var url = TitleList[Index];

            foreach (var data in DataList)
            {
                if (data.Url == url)
                {
                    DataList.Remove(data);
                    return data;
                }
            }

            return null;
        }

        public string GetFilePath()
        {
            return "67372.txt";
        }

        public void SetChapter(Chapter data)
        {
            DataList.Add(data);
        }
    }
}
