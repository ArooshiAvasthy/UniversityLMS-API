using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UniversityApi.Models
{
    public class BlogType
    {
        public int BlogId { get; set; }
        public string Title { get; set; }
        public string Body { get; set; }
        public string Author { get; set; }
        public string IntroWords { get; set; }
        public string ImagePath2 { get; set; }
    }
}