using Books.Http;
using Books.Http.Rule;
using System;
using System.Threading;
using System.Windows.Forms;

namespace Books
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ThreadPool.SetMaxThreads(1, 1);

            var rule = new AishangbaRule();

            CrawlerStart start = new CrawlerStart();
            start.IsDebug = false;
            start.Start(rule, 1);
        }

        private void button2_Click(object sender, EventArgs e)
        {


            //ThreadPool.SetMaxThreads(10, 10);

            //var rule = new Rule2();

            //var start = new CrawlerStart2();
            //start.Start(rule);
        }
    }
}
