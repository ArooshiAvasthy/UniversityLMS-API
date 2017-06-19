using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using UniversityClient.Models;

namespace UniversityClient.Controllers
{
    public class TinyMCEController : Controller
    {
        //
        // GET: /TinyMCE/
        public ActionResult Index()
        {
            if (Session["UserName"] == null)
            {
                return View("PleaseLogin");

            }
            else
            {
                return View();
            }

        }

        // An action that will accept your Html Content
        [HttpPost]
        public async Task<ActionResult> Index(BlogModel model)
        {
            try
            {
                string apiUrl = "http://localhost:53496/api/blogApi/PostBlog";

                model.Author = Session["UserName"].ToString();
                model.ImagePath2 = "~/Images/Blog_70x70.png";
               //model.Body = Regex.Replace(model.Body, @"<[^>]+>|&nbsp;", "").Trim();

                using (HttpClient client = new HttpClient())
                {
                    HttpResponseMessage responseMessage = await client.PostAsJsonAsync(apiUrl, model);
                    if (responseMessage.IsSuccessStatusCode)
                    {

                        return RedirectToAction("BlogPage");
                    }

                    else
                        return RedirectToAction("Error");
                }

            }

            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<ActionResult> BlogPage()
        {

            try
            {
                string apiUrl = "http://localhost:53496/api/blogApi/GetBlog";

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
                        List<BlogModel> obj = new List<BlogModel>();
                        obj = oJS.Deserialize<List<BlogModel>>(data);

                        return View(obj);

                        //int pageSize = 3;
                        //int pageNumber = (page ?? 1);
                        //return View(obj.ToPagedList(pageNumber, pageSize));
                        //return View(obj.OrderByDescending(x => x.BlogId));
                    }

                    else
                        return View("Error");
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }

          }

        public async Task<ActionResult> BlogPageTopFive()
        {

            try
            {
                string apiUrl = "http://localhost:53496/api/blogApi/GetBlog";

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
                        List<BlogModel> obj = new List<BlogModel>();
                        obj = oJS.Deserialize<List<BlogModel>>(data);
                        var firstFiveItems = obj.Take(5);
                        return View(firstFiveItems);
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

        public async Task<ActionResult> BlogDetails(string Title)
        {
            try
            {
                string apiUrl = "http://localhost:53496/api/blogApi/GetBlogDetails/" + Title;

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
                        BlogModel obj = new BlogModel();
                        obj = oJS.Deserialize<BlogModel>(data);
                        return View(obj);

                    }

                    else
                        return View("Error");
                }
            }
           catch(Exception ex)
            {
                throw ex;
            }
        }
	}
}