using System;
using System.Web.Http;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using CounterCulture.Services;
using CounterCulture.Models;
using CounterCulture.Utilities;
using Microsoft.AspNetCore.Mvc.Versioning;

namespace CounterCulture.Controllers
{
    [ApiVersion( "1.0" )]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public abstract class BaseController : ControllerBase {

        public BaseController(ICacheService CacheService)
        {
            Cache = CacheService;
        }

        protected ICacheService Cache { get; set; }

    }
}