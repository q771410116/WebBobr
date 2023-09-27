using Books.Domain;
using Books.Http.Rule;
using Books.Http.Write;
using System.IO;
using System.Threading;

namespace Books.Http
{
    /// <summary>
    /// 爬虫管理器
    /// </summary>
    internal class CrawlerStart
    {
        public void Start(IRule rule, int index)
        {
            // 获取网页列表
            rule.Index = index;
            string html_url = rule.GetCatalogueUrl();

            if ("" == html_url)
            {
                System.Console.WriteLine("all task completed");
                return;
            }

            // 请求页面
            Requestx requestx = new Requestx();
            // 获取解析器
            var doc = requestx.Execute(html_url);
            // 解析列表
            var list = rule.ParsePageList(doc);

            if (list != null && list.Count > 0)
            {
                var write = rule.GetWriteModel();
                write.TitleList = list;
                write.Total = list.Count;
                write.Index = 0;


                // 启动写文件线程
                ThreadPool.QueueUserWorkItem(new WaitCallback(WriteBookData), write);


                // 请求列表
                foreach (var item in list)
                {
                    //启动工作者线程
                    ThreadPool.QueueUserWorkItem(new WaitCallback(p =>
                    {
                        RunContentHandle(item, rule, write);
                    }));
                }
            }

        }



        public void RunContentHandle(string url, IRule rule, IWrite write)
        {
            // 请求页面
            Requestx requestx = new Requestx();

            // 获取文章页面
            var doc = requestx.Execute(url);

            // 解析文章标题
            var title = rule.ParseTitle(doc);

            // 解析文章内容
            var content = rule.ParseContent(doc);

            var book = new Chapter();
            book.Title = title;
            book.Content = content;
            book.Url = url;

            // 缓存到写入器
            PushBook(write, book);
        }

        public void WriteBookData(object state)
        {
            var rule = state as IWrite;

            System.Console.WriteLine("write started...");

            string fileName = rule.GetFilePath();

            StreamWriter writer = new StreamWriter(fileName, true);

            while (rule.Index < rule.Total)
            {
                var data = PopBook(rule);

                if (data != null)
                {
                    System.Console.WriteLine("write : {0}", data.Title);
                    writer.WriteLine(data.Title);
                    writer.WriteLine(data.Content);
                    writer.WriteLine(writer.NewLine);
                    writer.Flush();

                    rule.Index++;
                }

                Thread.Sleep(10);
            }

            System.Console.WriteLine("write completed...");
            writer.Close();
            writer = null;

            //// 开启下一个页面
            //Start(rule, rule.Index + 1);
        }



        public Chapter PopBook(IWrite rule)
        {
            lock (this)
            {
                if (rule.DataList.Count > 0)
                {
                    var data = rule.GetChapter();
                    return data;
                }
                return null;
            }
        }

        public void PushBook(IWrite rule, Chapter book)
        {
            lock (this)
            {
                rule.SetChapter(book);
            }
        }

    }
}
