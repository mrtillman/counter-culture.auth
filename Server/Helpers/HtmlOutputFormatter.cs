using System;
using System.Text;
using Microsoft.AspNetCore.Mvc.Formatters;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using CounterCulture.Repositories.Models;

namespace CounterCulture.Secure.Helpers {

    public class HtmlOutputFormatter : StringOutputFormatter
    {
        public HtmlOutputFormatter()
        {
            SupportedMediaTypes.Add("text/html");
        }
    }

}