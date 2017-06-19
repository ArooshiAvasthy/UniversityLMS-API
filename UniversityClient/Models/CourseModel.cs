using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UniversityClient.Models
{
    public class CourseModel
    {
        public int CourseID { get; set; }
        public string CourseName { get; set; }
        public string Stream { get; set; }
        public string Description { get; set; }
        public string ImagePath { get; set; }
    }
}