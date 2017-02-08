using System;
using Microsoft.AspNetCore.Mvc;
using TFN.Domain.Interfaces.Services;

namespace TFN.Api.UI.ChangePassword
{
    public class ChangePasswordController : Controller
    {
        public IAccountEmailService AccountEmailService { get; private set; }
        public ChangePasswordController(IAccountEmailService accountEmailService)
        {
            AccountEmailService = accountEmailService;
        }

        [HttpGet]
        public IActionResult Post()
        {
            throw new NotImplementedException();
        }
    }
}
