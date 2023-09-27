using Books.Domain;
using System.Collections.Generic;

namespace Books.Http.Write
{
    public interface IWrite
    {
        /// <summary>
        /// 当前写入进度
        /// </summary>
        int Index { get; set; }

        /// <summary>
        /// 总写入进度
        /// </summary>
        int Total { get; set; }

        /// <summary>
        /// 已获取数据
        /// </summary>
        List<Chapter> DataList { get; set; }

        /// <summary>
        /// 总任务数据
        /// </summary>
        List<string> TitleList { get; set; }

        /// <summary>
        /// 获取文件名
        /// </summary>
        string GetFilePath();

        /// <summary>
        /// 返回需要写入的数据
        /// </summary>
        Chapter GetChapter();

        /// <summary>
        /// 添加数据
        /// </summary>
        /// <param name="data"></param>
        void SetChapter(Chapter data);
    }
}
