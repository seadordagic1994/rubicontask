using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using taskRubicondoo.dbContext;
using taskRubicondoo.Models;
using taskRubicondoo.viewModels;
using taskRubicondoo.Helper;

namespace taskRubicondoo.Controllers
{
    
    public class BlogController : ApiController
    {
        //Context class, that cointans models that represent tables in the database
        public context ctx = new context();
        //get single post by slug
        [Route("api/posts/{slug}")]
        public IHttpActionResult GetSinglePost(string slug)
        {
            //viewPosts is a function that returns multiple posts with or without querystring, or a single post by slug (depends on parameters)
            return Ok(viewPosts(slug));
        }
        //get multiple posts
        [Route("api/posts")]
        public IHttpActionResult GetMultiplePosts(string tag = "")
        {
            return Ok(viewPosts(null, tag));
        }
        //create a new post
        [Route("api/posts")]
        public IHttpActionResult PostNewPost(dynamic blogPost)
        {
            // deserializing json object into a view model
            blogPostVM  model = JsonConvert.DeserializeObject<blogPostVM>(blogPost.blogPost.ToString());

            //function in "blogPostVM" class that checks if everthing is ok
            if (model.CheckRequired())
                return BadRequest("Invalid data");
            //helperClass is a static class that contains functions that will manuiplate with data and return things we need
            string newSlug = helperClasses.createSlug(model.title);

            if (ctx.blogPost.Where(x=> x.slug == newSlug).SingleOrDefault() != null)
                return BadRequest("Post already exists");

            //creating a new list of tags 
            List<Tags> tagList = new List<Tags>();

            //adding tags to "Tags" table (if they don't already exist)
            foreach (string s in blogPost.blogPost.tagList)
            {
                Tags tagFromDb = ctx.tag.Where(x=> x.tagName == s).SingleOrDefault();
                if (tagFromDb == null)
                {
                    Tags tag = new Tags();
                    tag.slug = helperClasses.createSlug(s);
                    tag.tagName = s;
                    tag.createdAt = DateTime.Now;
                    tag.updatedAt = DateTime.Now;
                    ctx.tag.Add(tag);
                    ctx.SaveChanges();
                    tagList.Add(tag);
                }
                else
                {
                    tagList.Add(tagFromDb);
                }
            }
            //creating new post
            BlogPost newPost = new BlogPost();
            newPost.slug = helperClasses.createSlug(model.title);
            newPost.title = model.title;
            newPost.description = model.description;
            newPost.body = model.body;
            newPost.createdAt = DateTime.Now;
            newPost.updatedAt = DateTime.Now;
            ctx.blogPost.Add(newPost);

            ctx.SaveChanges();

            //joining tags to post in "BlogPostTags" table (many to many relationship with composite primary key)
            foreach (Tags tag in tagList)
            {
                BlogPostTags blogPostTag = new BlogPostTags();
                blogPostTag.PostId = newPost.Id;
                blogPostTag.TagId = tag.Id;
                blogPostTag.createdAt = DateTime.Now;
                blogPostTag.updatedAt = DateTime.Now;
                ctx.blogPostTag.Add(blogPostTag);
            }
            ctx.SaveChanges();

            return Ok(viewPosts(newPost.slug));
        }
        //editing single post by slug
        [Route("api/posts/{slug}")]
        public IHttpActionResult PutBlogPost(string slug, dynamic blogPost)
        {
            if (ctx.blogPost.Where(x=> x.slug == slug).SingleOrDefault() == null)
                return BadRequest("Post doesn't exists");

            blogPostVM model = JsonConvert.DeserializeObject<blogPostVM>(blogPost.blogPost.ToString());

            BlogPost singlePost = ctx.blogPost.Where(x=> x.slug == slug).FirstOrDefault();
            singlePost.slug = String.IsNullOrEmpty(model.title) ? singlePost.slug : helperClasses.createSlug(model.title);
            singlePost.title = String.IsNullOrEmpty(model.title) ? singlePost.title : model.title;
            singlePost.body = String.IsNullOrEmpty(model.body) ? singlePost.body : model.body;
            singlePost.description = String.IsNullOrEmpty(model.description) ? singlePost.description : model.description;
            singlePost.updatedAt = DateTime.Now;

            ctx.SaveChanges();

            return Ok(viewPosts(singlePost.slug));
        }
        //deleting single post by slug
        [Route("api/posts/{slug}")]
        public IHttpActionResult DeletePost(string slug)
        {
            BlogPost post = ctx.blogPost.Where(x => x.slug == slug).SingleOrDefault();
            if (post == null)
                return BadRequest("Post doesn't exists");

            ctx.blogPost.Remove(post);
            ctx.SaveChanges();

            return Ok(viewPosts());

        }
        //this function is used to view posts. All posts by decending from recent, posts filtered by tag and single post by slug
        public dynamic viewPosts(string slug = null, string tag = "")
        {
            if(slug == null)
            {
                List<dynamic> posts = new List<dynamic>();
                foreach (BlogPost postFromDb in tag == "" ? ctx.blogPost.OrderByDescending(x=> x.Id).ToList() : ctx.blogPostTag.Where(x=> x.Tag.tagName == tag).Select(x=> x.Post).ToList())
                {
                    posts.Add(new
                    {
                        slug = postFromDb.slug,
                        title = postFromDb.title,
                        description = postFromDb.description,
                        body = postFromDb.body,
                        tagList = ctx.blogPostTag.Where(x => x.Post.slug == postFromDb.slug).Select(x => x.Tag.tagName).ToList(),
                        createdAt = postFromDb.createdAt.ToUniversalTime(),
                        updatedAt = postFromDb.updatedAt.ToUniversalTime(),
                    });
                  
                }
                return new
                {
                    blogPosts = posts,
                    postsCount = posts.Count
                };
            }
            else
            {
                BlogPost postFromDb = ctx.blogPost.Where(x=> x.slug == slug).SingleOrDefault();
                return new
                {
                    blogPost = new
                    {
                        slug = postFromDb.slug,
                        title = postFromDb.title,
                        description = postFromDb.description,
                        body = postFromDb.body,
                        tagList = ctx.blogPostTag.Where(x => x.Post.slug == postFromDb.slug).Select(x => x.Tag.tagName).ToList(),
                        createdAt = postFromDb.createdAt.ToUniversalTime(),
                        updatedAt = postFromDb.updatedAt.ToUniversalTime()
                    }
                };
            }
        }
    }
}
