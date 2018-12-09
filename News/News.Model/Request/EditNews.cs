using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace News.Model.Request
{
    public class EditNews
    {
        [Required]
        public long Id { get; set; }
        [Required]
        public long NewsClassifyId { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string Image { get; set; }
        [Required]
        public string Content { get; set; }
        [Required]
        public int Sort { get; set; }
    }
}
