using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using UniversityApi.Models;
using DAL;
using AutoMapper;

namespace UniversityApi.Controllers
{
    public class BlogApiController : ApiController
    {
        BlogDAL blogObj = new BlogDAL();

        [Route("api/blogApi/GetBlog")]
        public List<BlogType> GetBlog()
        {
            try
            {
                var data = blogObj.GetBlogs();
                Mapper.Initialize(cfg => { cfg.CreateMap<Blog, BlogType>(); });
                List<BlogType> blogList = new List<BlogType>();

                if (data.Any())
                {
                    foreach (var item in data)
                    {
                        BlogType obj = Mapper.Map<Blog, BlogType>(item);
                        blogList.Add(obj);
                    }
                }

                return blogList;
            }
           catch(Exception ex)
            {
                throw ex;
            }
        }

        [Route("api/blogApi/PostBlog")]
        public void PostBlog([FromBody] BlogType blogData)
        {
            try
            {
                Mapper.Initialize(cfg => { cfg.CreateMap<BlogType, Blog>(); });
                Blog obj = Mapper.Map<BlogType, Blog>(blogData);

                blogObj.PostBlogData(obj);
            }
           catch(Exception ex)
            {
                throw ex;
            }
           
        }

        [Route("api/blogApi/GetBlogDetails/{title}")]
        public BlogType GetBlogDetails(string title)
        {
            try
            {
                var data = blogObj.GetBlogDetails(title);
                Mapper.Initialize(cfg => { cfg.CreateMap<Blog, BlogType>(); });
                BlogType obj = Mapper.Map<Blog, BlogType>(data);

                return obj;
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
    }
}
