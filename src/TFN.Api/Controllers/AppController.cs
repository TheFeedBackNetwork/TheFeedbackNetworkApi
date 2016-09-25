using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TFN.Api.Extensions;

namespace TFN.Api.Controllers
{
    public class AppController : Controller
    {
        protected Guid UserId => HttpContext.GetUserId().Value;
    }
}
