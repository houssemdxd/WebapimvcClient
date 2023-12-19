using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebAppTemplate.Services
{
    using WebAppTemplate.Models;
    public class DefaultServices
    {
        public List<Article> getArticles()
        {
            try
            {
                return Article.Access.Get();
            }
            catch (Exception)
            {
                // handle exceptions
                return null;
            }
        }
    }
}