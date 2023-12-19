using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Http;

namespace WebAppTemplate.Controllers
{
    using WebAppTemplate.Services;
    [System.Web.Http.RoutePrefix("api")]
    public class ArticleController : ApiController
    {
        public DefaultServices _dservice = new DefaultServices();
        // GET: api/Article
        public IHttpActionResult Get()
        {
            var p = _dservice.getArticles();
            if (p == null)
            {
                return NotFound();
            }
            return Ok(p);
            //return new string[] { "value1", "value2" };
        }

        // GET: api/Article/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Article
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/Article/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Article/5
        public void Delete(int id)
        {
        }
    }
}
