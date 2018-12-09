using System;
using System.Collections.Generic;
using System.Text;

namespace News.Model.Response
{
    public class BannerModel
    {
        public long Id { get; set; }
        public string Title { get; set; }
        public string Image { get; set; }
        public string Url { get; set; }
        public string Remark { get; set; }
    }
}
