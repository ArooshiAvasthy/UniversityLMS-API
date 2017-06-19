using Newtonsoft.Json;
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
    public class ImageController : Controller
    {
        //
        // GET: /Image/
        public async Task<ActionResult> ImageGallery()
        {
            try
            {
                string apiUrl = "http://localhost:53496/api/Imageapi/GetImages";

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
                        List<ImageModel> obj = new List<ImageModel>();
                        obj = JsonConvert.DeserializeObject<List<ImageModel>>(data);
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

        //Test
        public async Task<ActionResult> ImageGallery2()
        {
            try
            {
                string apiUrl = "http://localhost:53496/api/Imageapi/GetImages";

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
                        List<ImageModel> obj = new List<ImageModel>();
                        obj = JsonConvert.DeserializeObject<List<ImageModel>>(data);
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
    }
}