using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Threading;
using System.Web;
using System.Web.Http.Controllers;
using System.Net.Http;
using System.Net;
using System.Web.Http.Filters;
using DAL;

namespace UniversityApi
{
    public class BasicAuthentication: AuthorizationFilterAttribute
    {
        public override void OnAuthorization(HttpActionContext actionContext)
        {

            if (actionContext.Request.Headers.Authorization == null)
            {
                actionContext.Response = actionContext.Request.CreateResponse(HttpStatusCode.Unauthorized);
            }

            else
            {
                //string authenticationToken = actionContext.Request.Headers.Authorization.Parameter;
                string decodedAuthenticationToken = actionContext.Request.Headers.Authorization.Parameter;
                string[] usernamePasswordRoleArray = decodedAuthenticationToken.Split(':');
                string username = usernamePasswordRoleArray[0];
                string password = usernamePasswordRoleArray[1];
                //string role = usernamePasswordRoleArray[2];

                if (LoginDAL.validateCredentials(username, password))
                {
                    Thread.CurrentPrincipal = new GenericPrincipal(new GenericIdentity(username), null);
                    //actionContext.Response = actionContext.Request.CreateResponse(HttpStatusCode.OK);
                }
                else
                {
                    actionContext.Response = actionContext.Request.CreateResponse(HttpStatusCode.Unauthorized);

                }
            }
        }
    }
}