using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Web.Http;
using System.Web.Http.Cors;
using UniversityApi.Models;
using AutoMapper;
using DAL;

namespace UniversityApi.Controllers
{
    [EnableCors("*", "*", "*")]
    public class LoginAPIController : ApiController
    {
        [HttpGet]
        [BasicAuthentication]
        [ActionName("GetUser")]
        public string GetUser()
        {
            try
            {
                string username = Thread.CurrentPrincipal.Identity.Name;
                if (!string.IsNullOrEmpty(username))
                    return "true";
                else
                    return "false";
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [Route("api/loginapi/PostNewUser")]
        public bool PostNewUser([FromBody] UserType user)
        {
            try
            {
                LoginDAL obj = new DAL.LoginDAL();
                Mapper.Initialize(cfg => { cfg.CreateMap<UserType, User>(); });
                User newuser = Mapper.Map<UserType, User>(user);
                bool response= obj.AddUser(newuser);
                if (response)
                    return true;
                else
                    return false;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
