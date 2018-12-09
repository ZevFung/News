using System;
using System.Collections.Generic;
using System.Text;

namespace News.Model.Entity
{
    public class Banner
    {
        public long Id { get; set; }
        public string Title { get; set; }
        public string Image { get; set; }
        public string Url { get; set; }
        public string Remark { get; set; }
        public DateTime Createtime { get; set; }
        public long CreateUser { get; set; }
        public DateTime PreviousUpdateTime { get; set; }
        public long PreviousUpdateUser { get; set; }
        public DateTime LastUpdateTime { get; set; }
        public long LastUpdateUser { get; set; }
    }
}
