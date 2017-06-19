using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace UniversityClient.Models
{
    public class BlogModel
    {

        // This attributes allows your HTML Content to be sent up
        public int BlogId { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public string IntroWords { get; set; }
        public string ImagePath2 { get; set; }
        [AllowHtml]
        public string Body { get; set; }
        public BlogModel()
        {

        }
    }
}