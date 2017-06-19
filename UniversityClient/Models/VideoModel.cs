using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UniversityClient.Models
{
    public class VideoModel
    {
        public int videoID { get; set; }
        public string VideoName { get; set; }
        public string Path { get; set; }
        public string CourseID { get; set; }
        public string Poster { get; set; }
    }
}