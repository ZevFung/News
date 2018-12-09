using System;
using System.Collections.Generic;
using System.Text;

namespace News.Model.Response
{
    public class NewsModel
    {
        public long Id { get; set; }
        public long NewsClassifyId { get; set; }
        public string Title { get; set; }
        public string Image { get; set; }
        public string Content { get; set; }
        public int Sort { get; set; }
        public int Count { get; set; }
        public string Createtime { get; set; }
    }
}