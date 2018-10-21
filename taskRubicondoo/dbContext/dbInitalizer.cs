using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using taskRubicondoo.Models;

namespace taskRubicondoo.dbContext
{
    public class dbInitalizer : CreateDatabaseIfNotExists<context>
    {
        protected override void Seed(context context)
        {
            var tag1 = new Tags { slug = "andorid", tagName = "Andorid", createdAt = DateTime.Now, updatedAt = DateTime.Now };
            var tag2 = new Tags { slug = "windows", tagName = "Windows", createdAt = DateTime.Now, updatedAt = DateTime.Now };
            var tag3 = new Tags { slug = "code", tagName = "Code", createdAt = DateTime.Now, updatedAt = DateTime.Now };

            context.tag.Add(tag1);
            context.tag.Add(tag2);
            context.tag.Add(tag3);

            var post1 = new BlogPost
            {
                slug = "heading-1",
                title = "Heading-1",
                description = "description 1",
                body = "body 1",
                createdAt = DateTime.Now,
                updatedAt = DateTime.Now,
            };

            var post2 = new BlogPost
            {
                slug = "heading-2",
                title = "Heading-2",
                description = "description 2",
                body = "body 2",
                createdAt = DateTime.Now,
                updatedAt = DateTime.Now,
            };

            context.blogPost.Add(post1);
            context.blogPost.Add(post2);
            

            var postTag1 = new BlogPostTags { Post = post1, Tag = tag1, createdAt = DateTime.Now, updatedAt = DateTime.Now };
            var postTag2 = new BlogPostTags { Post = post1, Tag = tag2, createdAt = DateTime.Now, updatedAt = DateTime.Now };
            var postTag3 = new BlogPostTags { Post = post2, Tag = tag2, createdAt = DateTime.Now, updatedAt = DateTime.Now };
            var postTag4 = new BlogPostTags { Post = post2, Tag = tag3, createdAt = DateTime.Now, updatedAt = DateTime.Now };

            context.blogPostTag.Add(postTag1);
            context.blogPostTag.Add(postTag2);
            context.blogPostTag.Add(postTag3);
            context.blogPostTag.Add(postTag4);

            base.Seed(context);
        }
    }
}