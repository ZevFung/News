using System;
using System.Collections.Generic;
using System.Text;

namespace News.Model.Entity
{
    public class News
    {
        public long Id { get; set; }
        public long NewsClassifyId { get; set; }
        public string Title { get; set; }
        public string Image { get; set; }
        public string Content { get; set; }
        public int Sort { get; set; }
        public DateTime Createtime { get; set; }
        public long CreateUser { get; set; }
        public DateTime PreviousUpdateTime { get; set; }
        public long PreviousUpdateUser { get; set; }
        public DateTime LastUpdateTime { get; set; }
        public long LastUpdateUser { get; set; }
        public virtual ICollection<Comment> Comments { get; set; }
        public virtual NewsClassify NewsClassify { get; set; }
    }
}
