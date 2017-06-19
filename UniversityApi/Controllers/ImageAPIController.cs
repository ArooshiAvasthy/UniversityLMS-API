using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using DAL;
using AutoMapper;
using UniversityApi.Models;

namespace UniversityApi.Controllers
{
    public class ImageAPIController : ApiController
    {
        ImageDAL imgObj = new ImageDAL();

        [Route("api/imageapi/GetImages")]
        public List<ImageType> GetImages()
        {
            try
            {
                var data = imgObj.GetImages();
                List<ImageType> imgList = new List<ImageType>();
                Mapper.Initialize(cfg => { cfg.CreateMap<Image, ImageType>(); });

                if (data.Any())
                {
                    //Mapper initialization and creation
                    Mapper.Initialize(cfg => { cfg.CreateMap<Image, ImageType>(); });
                    foreach (var item in data)
                    {
                        ImageType infoModel = Mapper.Map<Image, ImageType>(item);
                        imgList.Add(infoModel);
                    }
                }

                return imgList;
            }
           
              catch(Exception ex)
            {
                throw ex;
            }
        }
    }
}
