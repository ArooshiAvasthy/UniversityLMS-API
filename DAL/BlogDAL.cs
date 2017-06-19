using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace DAL
{
    public class BlogDAL
    {
        University2Entities1 obj = new University2Entities1();

        public List<Blog> GetBlogs()
        {
            try
            {
                var blogs = (from c in obj.Blogs
                             select c).ToList<Blog>();
                return blogs;
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public void PostBlogData(Blog data)
        {
            try
            {
                data.IntroWords = Regex.Replace(data.Body, @"<[^>]+>|&nbsp;", "").Trim();
                char[] spaceSeparator = new char[] { ' ' };
                string[] authors = data.IntroWords.Split(spaceSeparator, StringSplitOptions.None);

                data.IntroWords = "";
                int len = authors.Length;

                //Two words less of the complete passage
                for (int i = 0; i < len - 2; i++)
                {
                    data.IntroWords = data.IntroWords + " " + authors[i];
                }
                data.IntroWords = data.IntroWords + "..........";
                //data.IntroWords = authors[0] + " " + authors[1] + " " + authors[2] + " " + authors[3] + " " + authors[4] + " " + authors[5] + "............. ";

                obj.Blogs.Add(data);

                obj.SaveChanges();
            }
           catch(Exception ex)
            {
                throw ex;
            }
        }

        public Blog GetBlogDetails(string title)
        {
            try
            {
                var data = (from c in obj.Blogs
                            where c.Title == title
                            select c).FirstOrDefault();
                return data;
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
    }
}
