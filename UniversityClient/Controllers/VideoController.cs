using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using UniversityClient.Models;

namespace UniversityClient.Controllers
{
    public class VideoController : Controller
    {
        //
        // GET: /Video/
        //Static videos fetched from youtube
        public ActionResult GuestLectureVideos()
        {
            return View();
        }

        //Course related videos stored in solution and record maintained in DB
        public async Task<ActionResult> SpecificCourseVideos()
        {
            try
            {
                if (Session["UserName"] == null)
                {
                    return View("PleaseLogin");

                }
                else
                {
                    string apiUrl = "http://localhost:53496/api/videoapi/GetSpecificVideos/" + Session["Course"].ToString();

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
                            List<VideoModel> obj = new List<VideoModel>();
                            obj = oJS.Deserialize<List<VideoModel>>(data);
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

        public async Task<ActionResult> AllCourseVideos()
        {
            try
            {
                if (Session["UserName"] == null)
                {
                    return View("PleaseLogin");

                }
                else
                {
                    string apiUrl = "http://localhost:53496/api/videoapi/GetAllVideos";

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
                            List<VideoModel> obj = new List<VideoModel>();
                            obj = oJS.Deserialize<List<VideoModel>>(data);
                            return View(obj);

                        }

                        else
                            return View("Error");
                    }
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
          
        }
      
	}
}