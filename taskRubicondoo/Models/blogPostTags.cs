using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace taskRubicondoo.Models
{
    public class BlogPostTags
    {
        [Key, Column(Order = 1)]
        public int PostId { get; set; }
        public virtual BlogPost Post { get; set; }

        [Key, Column(Order = 2)]
        public int TagId { get; set; }
        public virtual Tags Tag { get; set; }

        public DateTime createdAt { get; set; }
        public DateTime updatedAt { get; set; }
    }
}