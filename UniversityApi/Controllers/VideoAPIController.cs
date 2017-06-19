using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using AutoMapper;
using DAL;
using UniversityApi.Models;

namespace UniversityApi.Controllers
{
    public class VideoAPIController : ApiController
    {
        VideoDAL videoObj = new VideoDAL();

        [Route("api/videoapi/GetSpecificVideos/{course}")]
        public List<VideoType> GetSpecificVideos(string course)
        {
            try
            {
                var data = videoObj.GetSpecificVideos(course);
                List<VideoType> courseList = new List<VideoType>();
                Mapper.Initialize(cfg => { cfg.CreateMap<Video, VideoType>(); });

                if (data.Any())
                {
                    //Mapper initialization and creation
                    Mapper.Initialize(cfg => { cfg.CreateMap<Video, VideoType>(); });
                    foreach (var item in data)
                    {
                        VideoType infoModel = Mapper.Map<Video, VideoType>(item);
                        courseList.Add(infoModel);
                    }
                }

                return courseList;
            }
           catch(Exception ex)
            {
                throw ex;
            }
        }

        [Route("api/videoapi/GetAllVideos")]
        public List<VideoType> GetAllVideos()
        {
            try
            {
                var data = videoObj.GetAllVideos();
                List<VideoType> courseList = new List<VideoType>();
                Mapper.Initialize(cfg => { cfg.CreateMap<Video, VideoType>(); });

                if (data.Any())
                {
                    //Mapper initialization and creation
                    Mapper.Initialize(cfg => { cfg.CreateMap<Video, VideoType>(); });
                    foreach (var item in data)
                    {
                        VideoType infoModel = Mapper.Map<Video, VideoType>(item);
                        courseList.Add(infoModel);
                    }
                }

                return courseList;
            }
           
            catch(Exception ex)
            {
                throw ex;
            }
        }
    }


}
