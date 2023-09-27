using Books.Http.Write;
using System.Collections.Generic;

namespace Books.Http.Rule
{
    /// <summary>
    /// 爬虫规则
    /// </summary>
    public interface IRule
    {
        /// <summary>
        /// 目录页面索引
        /// </summary>
        int Index { get; set; }

        /// <summary>
        /// 获取目录页面的Url
        /// </summary>
        /// <returns></returns>
        string GetCatalogueUrl();

        /// <summary>
        /// 爬取所有章节的地址url
        /// </summary>
        /// <param name="doc"></param>
        /// <returns></returns>
        List<string> ParsePageList(HtmlAgilityPack.HtmlDocument doc);

        /// <summary>
        /// 爬取标题
        /// </summary>
        /// <param name="doc"></param>
        /// <returns></returns>
        string ParseTitle(HtmlAgilityPack.HtmlDocument doc);

        /// <summary>
        /// 爬取内容
        /// </summary>
        /// <param name="doc"></param>
        /// <returns></returns>
        string ParseContent(HtmlAgilityPack.HtmlDocument doc);

        /// <summary>
        /// 获取写入模型
        /// </summary>
        /// <returns></returns>
        IWrite GetWriteModel();
    }
}
