using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Runtime.Serialization.Formatters;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using UniversityClient.Models;

namespace UniversityClient.Controllers
{
    public class CourseController : Controller
    {
        //
        // GET: /Course/
        [HttpGet]
        public async Task<ActionResult> AllCourses()
        {
            try
            {
                if (Session["UserName"] == null)
                {
                    return View("PleaseLogin");

                }
                else
                {
                    string apiUrl = "http://localhost:53496/api/courseapi/GetDisplayAllCourses";

                    using (HttpClient client = new HttpClient())
                    {
                        client.BaseAddress = new Uri(apiUrl);
                        client.DefaultRequestHeaders.Accept.Clear();
                        client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

                        HttpResponseMessage response = await client.GetAsync(apiUrl);
                        if (response.IsSuccessStatusCode)
                        {
                            var data = await response.Content.ReadAsStringAsync();

                            JavaScriptSerializer oJS = new JavaScriptSerializer();
                            List<CourseModel> obj = new List<CourseModel>();
                            obj = oJS.Deserialize<List<CourseModel>>(data);
                            return View(obj);


                        }

                        else
                            return View("Error");
                    }
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public async Task<ActionResult> Enroll(string CourseName)
        {
            try
            {
                string apiUrl = "http://localhost:53496/api/courseapi/PostEnrolledUser/";

                InfoModel info = new InfoModel();
                info.Name = Session["UserName"].ToString();
                info.Coursename = CourseName;

                using (HttpClient client = new HttpClient())
                {
                    HttpResponseMessage responseMessage = await client.PostAsJsonAsync(apiUrl,info);
                    if (responseMessage.IsSuccessStatusCode)
                    {

                        return RedirectToAction("CoursePage", "Course", new { coursename = CourseName});
                    }

                    else
                        return RedirectToAction("Error");
                }

            }
            
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public async Task<ActionResult> CoursePage(string coursename)
        {
            try
            {
                string apiUrl = "http://localhost:53496/api/courseapi/GetCourseFiles/"+coursename;

                Session["Course"] = coursename;
                using (HttpClient client = new HttpClient())
                {
                    client.BaseAddress = new Uri(apiUrl);
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

                    HttpResponseMessage response = await client.GetAsync(apiUrl);
                    if (response.IsSuccessStatusCode)
                    {
                        var data = await response.Content.ReadAsStringAsync();

                        var jsonSettings = new JsonSerializerSettings
                        {
                            TypeNameHandling = TypeNameHandling.Objects,
                            TypeNameAssemblyFormat = FormatterAssemblyStyle.Simple
                        };

                        JavaScriptSerializer oJS = new JavaScriptSerializer();
                        List<FileModel> obj = new List<FileModel>();
                        obj = JsonConvert.DeserializeObject<List<FileModel>>(data, jsonSettings);
                        return View(obj);


                    }

                    else
                        return View("Error");
                }

            }

            catch (Exception ex)
            {
                throw ex;
            }
        }

        //Download File
        [HttpGet]
        public async Task<FileResult> DownLoadFile(int id)
        {
            try
            {
                string apiUrl = "http://localhost:53496/api/courseapi/GetSpecificFile/" + id;

                using (HttpClient client = new HttpClient())
                {
                    client.BaseAddress = new Uri(apiUrl);
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

                    HttpResponseMessage response = await client.GetAsync(apiUrl);
                    if (response.IsSuccessStatusCode)
                    {
                        var data = await response.Content.ReadAsStringAsync();

                        var jsonSettings = new JsonSerializerSettings
                        {
                            TypeNameHandling = TypeNameHandling.Objects,
                            TypeNameAssemblyFormat = FormatterAssemblyStyle.Simple
                        };

                        JavaScriptSerializer oJS = new JavaScriptSerializer();
                        FileModel FileById = new FileModel();
                        FileById = JsonConvert.DeserializeObject<FileModel>(data, jsonSettings);

                        return File(FileById.data, "application/pdf/doc", FileById.Name);

                    }

                    else
                        throw new Exception(String.Format("No report found with id {0}", id));
                }

            }
            catch(Exception ex)
            {
                throw ex;
            }
           
        }  

        //Upload File
        public ActionResult FileUpload()
        {
            return View();
        }


        [HttpPost]
        public async Task<ActionResult> FileUpload(HttpPostedFileBase files)
        {
            try
            {
                String FileExt = Path.GetExtension(files.FileName).ToUpper();

                if (FileExt == ".PDF")
                {
                    Stream str = files.InputStream;
                    BinaryReader Br = new BinaryReader(str);
                    Byte[] FileDet = Br.ReadBytes((Int32)str.Length);

                    FileModel Fd = new Models.FileModel();
                    Fd.Name = files.FileName;
                    Fd.data = FileDet;
                    Fd.Type = "application/pdf";
                    Fd.Courses = Session["Course"].ToString();
                    string apiUrl2 = "http://localhost:53496/api/courseapi/PostFile";
                    using (HttpClient client = new HttpClient())
                    {
                        HttpResponseMessage responseMessage = await client.PostAsJsonAsync(apiUrl2, Fd);
                    }
                    //return RedirectToAction("FileUpload");
                    return RedirectToAction("CoursePage", new { coursename = Fd.Courses });
                }

                else if (FileExt == ".PPT")
                {
                    Stream str = files.InputStream;
                    BinaryReader Br = new BinaryReader(str);
                    Byte[] FileDet = Br.ReadBytes((Int32)str.Length);

                    FileModel Fd = new Models.FileModel();
                    Fd.Name = files.FileName;
                    Fd.data = FileDet;
                    Fd.Type = "application/ppt";
                    Fd.Courses = Session["Course"].ToString();
                    string apiUrl2 = "http://localhost:53496/api/courseapi/PostFile";
                    using (HttpClient client = new HttpClient())
                    {
                        HttpResponseMessage responseMessage = await client.PostAsJsonAsync(apiUrl2, Fd);
                    }
                    //return RedirectToAction("FileUpload");
                    return RedirectToAction("CoursePage", new { coursename = Fd.Courses });
                }

                else if (FileExt == ".DOCX")
                {
                    Stream str = files.InputStream;
                    BinaryReader Br = new BinaryReader(str);
                    Byte[] FileDet = Br.ReadBytes((Int32)str.Length);

                    FileModel Fd = new Models.FileModel();
                    Fd.Name = files.FileName;
                    Fd.data = FileDet;
                    Fd.Type = "application/docx";
                    Fd.Courses = Session["Course"].ToString();
                    string apiUrl2 = "http://localhost:53496/api/courseapi/PostFile";
                    using (HttpClient client = new HttpClient())
                    {
                        HttpResponseMessage responseMessage = await client.PostAsJsonAsync(apiUrl2, Fd);
                    }
                    //return RedirectToAction("FileUpload");
                    return RedirectToAction("CoursePage", new { coursename = Fd.Courses });
                }
                else if (FileExt == ".XLSX")
                {
                    Stream str = files.InputStream;
                    BinaryReader Br = new BinaryReader(str);
                    Byte[] FileDet = Br.ReadBytes((Int32)str.Length);

                    FileModel Fd = new Models.FileModel();
                    Fd.Name = files.FileName;
                    Fd.data = FileDet;
                    Fd.Type = "application/excel";
                    Fd.Courses = Session["Course"].ToString();
                    string apiUrl2 = "http://localhost:53496/api/courseapi/PostFile";
                    using (HttpClient client = new HttpClient())
                    {
                        HttpResponseMessage responseMessage = await client.PostAsJsonAsync(apiUrl2, Fd);
                    }
                    return RedirectToAction("CoursePage", new { coursename = Fd.Courses });
                }
                else
                {

                    //ViewBag.FileStatus = "Invalid file format.";
                    return View("Invalid");

                }

            }
            catch(Exception ex)
            {
                throw ex;
            }
           
        }

	}
}