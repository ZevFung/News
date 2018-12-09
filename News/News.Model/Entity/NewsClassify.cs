using System;
using System.Collections.Generic;
using System.Text;

namespace News.Model.Entity
{
    public class NewsClassify
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public int Sort { get; set; }
        public DateTime Createtime { get; set; }
        public long CreateUser { get; set; }
        public DateTime PreviousUpdateTime { get; set; }
        public long PreviousUpdateUser { get; set; }
        public DateTime LastUpdateTime { get; set; }
        public long LastUpdateUser { get; set; }
        public virtual ICollection<News> Newss { get; set; }
    }
}
