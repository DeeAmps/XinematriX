using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using XinematriX.Api.Controllers;

namespace XinematriX.WebAdmin.Providers
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = false)]
    public class AuthorizationHelper : Attribute, IResourceFilter
    {
        public AuthorizationHelper() { }

        public void OnResourceExecuted(ResourceExecutedContext context) { }

        public void OnResourceExecuting(ResourceExecutingContext context)
        {
            var tkValue = context.HttpContext.Request.Cookies["XTkCookie"];
            var auth = AdminController.Instance.ValidToken(tkValue).Result;
            if (!auth)
            {
                var response = context.HttpContext.Response;
                response.StatusCode = StatusCodes.Status401Unauthorized;
                context.Result = new RedirectToActionResult("Login","Login", null);
            }
        }
    }
}
