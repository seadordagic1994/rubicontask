using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using taskRubicondoo.Models;

namespace taskRubicondoo.dbContext
{
    public class context: DbContext
    {
        public context() : base("name=MyCS")
        {
            Database.SetInitializer(new dbInitalizer());
        }

        public DbSet<BlogPost> blogPost { get; set; }
        public DbSet<Tags> tag { get; set; }
        public DbSet<BlogPostTags> blogPostTag { get; set; }
    }
}