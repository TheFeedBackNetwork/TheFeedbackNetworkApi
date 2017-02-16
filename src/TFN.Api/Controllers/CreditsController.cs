using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TFN.Api.Authorization.Models.Resource;
using TFN.Api.Authorization.Operations;
using TFN.Domain.Interfaces.Services;
using TFN.Mvc.HttpResults;

namespace TFN.Api.Controllers
{
    [Route("api/credits")]
    public class CreditsController : AppController
    {
        public ICreditService CreditService { get; private set; }
        public IAuthorizationService AuthorizationService { get; private set; }
        public CreditsController(ICreditService creditService, IAuthorizationService authorizationService)
        {
            CreditService = creditService;
            AuthorizationService = authorizationService;
        }

        [HttpGet("{creditId:Guid}", Name = "GetCredits")]
        [Authorize("credits.read")]
        public async Task<IActionResult> GetCredits(Guid creditId)
        {
            var credit = await CreditService.GetAsync(creditId);

            if (credit == null)
            {
                return NotFound();
            }

            var authZModel = CreditsAuthorizationModel.From(credit);

            if (!await AuthorizationService.AuthorizeAsync(User, authZModel, CreditsOperations.Read))
            {
                return new HttpForbiddenResult("An attempt to read credits was attempted, but the authorization policy challenged the request");
            }

        }

    }
}