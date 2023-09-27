using Books.Http.Rule;
using System.Collections.Generic;
using System.IO;
using System.Threading;

namespace Books.Http
{
    /// <summary>
    /// 爬虫管理器
    /// </summary>
    internal class CrawlerStart2
    {

        public void Start(BaseRule rule)
        {

            // 启动写入线程
            ThreadPool.QueueUserWorkItem(new WaitCallback(WriteBookData), rule);


            // RunContentHandle(rule);
        }






        public void RunContentHandle(BaseRule rule)
        {
            //启动工作者线程
            ThreadPool.QueueUserWorkItem(new WaitCallback(p =>
            {
                // 获取网页列表
                string html_url = rule.NextPage();

                // 请求页面
                Requestx requestx = new Requestx();
                // 获取解析器
                var doc = requestx.Execute(html_url);
                // 解析列表
                var list = rule.ParsePageList(doc);

                // 送入到写入器
                PushBook(list);

            }));
        }

        public void WriteBookData(object state)
        {
            BaseRule rule = state as BaseRule;

            System.Console.WriteLine("write started...");

            string fileName = "test_word1.txt";

            StreamWriter writer = new StreamWriter(fileName, true);



            while (index > 0)
            {
                var data = PopBook();

                if (data != null)
                {
                    writer.Write(data);
                    writer.Flush();
                    index--;
                    Thread.Sleep(10);
                }
                else
                {

                    RunContentHandle(rule);
                    Thread.Sleep(200000);
                }
            }

            System.Console.WriteLine("write completed...");
            writer.Close();
            writer = null;
        }

        private int index = 589;
        private List<string> datas = new List<string>();

        public string PopBook()
        {
            lock (this)
            {
                if (datas.Count > 0)
                {
                    var data = datas[0];
                    datas.RemoveAt(0);
                    return data;
                }

                return null;
            }
        }

        public void PushBook(List<string> book)
        {
            lock (this)
            {
                datas.AddRange(book);
            }
        }

    }
}
