using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using taskRubicondoo.dbContext;
using taskRubicondoo.Models;
using taskRubicondoo.Helper;

namespace taskRubicondoo.Controllers
{
    public class TagsController : ApiController
    {
        public context ctx = new context();
        //get all tags from database
        public IHttpActionResult Get()
        {
            return Ok(new { tags = ctx.tag.Select(x => x.tagName).ToList() });
        }
    }
}
