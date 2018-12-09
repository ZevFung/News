using System;
using System.Collections.Generic;
using System.Text;

namespace News.Model.Entity
{
    public class Comment
    {
        public long Id { get; set; }
        public long NewsId { get; set; }
        public string Content { get; set; }
        public DateTime Createtime { get; set; }
        public long CreateUser { get; set; }
        public virtual News News { get; set; }
    }
}
