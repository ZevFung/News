using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace News.Model.Request
{
    public class AddNewsClassify
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public int Sort { get; set; }
    }
}
