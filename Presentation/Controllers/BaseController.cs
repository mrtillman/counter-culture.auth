using System;
using System.Web.Http;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Presentation.Services;
using Presentation.Models;
using Presentation.Utilities;
using Microsoft.AspNetCore.Mvc.Versioning;

namespace Presentation.Controllers
{
    [ApiVersion( "1.0" )]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public abstract class BaseController : ControllerBase {

        // public BaseController(ICacheService CacheService)
        // {
        //     Cache = CacheService;
        // }

        // protected ICacheService Cache { get; set; }

    }
}