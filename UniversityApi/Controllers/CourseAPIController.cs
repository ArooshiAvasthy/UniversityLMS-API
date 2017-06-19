using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using UniversityApi.Models;
using AutoMapper;
using DAL;

namespace UniversityApi.Controllers
{
    public class CourseAPIController : ApiController
    {
       CourseDAL courseObj = new CourseDAL();

       [Route("api/courseapi/GetDisplayAllCourses")]
        public List<CourseType> GetDisplayAllCourses()
       {
           try
           {
               var data = courseObj.GetAllCourses();
               List<CourseType> courseList = new List<CourseType>();
               Mapper.Initialize(cfg => { cfg.CreateMap<Course, CourseType>(); });

               if (data.Any())
               {
                   //Mapper initialization and creation
                   Mapper.Initialize(cfg => { cfg.CreateMap<Course, CourseType>(); });
                   foreach (var item in data)
                   {
                       CourseType infoModel = Mapper.Map<Course, CourseType>(item);
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

        [Route("api/courseapi/PostEnrolledUser")]
        public void PostEnrolledUser([FromBody] infoType enrollmentInfo)
       {
           try
           {
               string username = enrollmentInfo.Name;
               string coursename = enrollmentInfo.Coursename;

               courseObj.EnrolledUser(username, coursename);
           }
           catch(Exception ex)
           {
               throw ex;
           }
       }


        [Route("api/courseapi/GetCourseFiles/{courseName}")]      
        public List<FileType> GetCourseFiles(string courseName)
        {
            try
            {
                var files = courseObj.GetFiles(courseName);
                Mapper.Initialize(cfg => { cfg.CreateMap<PDFFile, FileType>(); });
                List<FileType> fileList = new List<FileType>();
                if (files.Any())
                {
                    foreach (var item in files)
                    {
                        FileType fileObj = Mapper.Map<PDFFile, FileType>(item);
                        fileList.Add(fileObj);
                    }
                }
                return fileList;
            }
           catch(Exception ex)
            {
                throw ex;
            }
        }

        [Route("api/courseapi/GetSpecificFile/{id:int}")]
        public FileType GetSpecificFile(int id)
        {
            try
            {
                var data = courseObj.GetSpecificFile(id);
                Mapper.Initialize(cfg => { cfg.CreateMap<PDFFile, FileType>(); });

                FileType fileObj = Mapper.Map<PDFFile, FileType>(data);


                return fileObj;
            }
           catch(Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        [Route("api/courseapi/PostFile")]
        public void PostFile([FromBody] FileType fileObj)
        {
            try
            {
                Mapper.Initialize(cfg => { cfg.CreateMap<FileType, PDFFile>(); });
                PDFFile obj = Mapper.Map<FileType, PDFFile>(fileObj);
                courseObj.AddFile(obj);
            
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}
