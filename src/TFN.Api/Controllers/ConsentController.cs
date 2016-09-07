using IdentityServer4.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Linq;
using System.Threading.Tasks;
using IdentityServer4.Models;
using IdentityServer4.Stores;
using TFN.Api.Models.InputModels;
using TFN.Api.Models.ViewModels;

namespace TFN.Api.Controllers
{
    public class ConsentController : Controller
    {
        private readonly ILogger<ConsentController> Logger;
        private readonly IClientStore ClientStore;
        private readonly IScopeStore ScopeStore;
        private readonly IIdentityServerInteractionService Interaction;
        
        public ConsentController(
            ILogger<ConsentController> logger,
            IIdentityServerInteractionService interaction,
            IClientStore clientStore,
            IScopeStore scopeStore)
        {
            Logger = logger;
            Interaction = interaction;
            ClientStore = clientStore;
            ScopeStore = scopeStore;
        }

        [HttpGet]
        public async Task<IActionResult> Index(string returnUrl)
        {
            var vm = await BuildViewModelAsync(returnUrl);
            if (vm != null)
            {
                return View("Index", vm);
            }

            return View("Error");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index(ConsentInputModel model)
        {
            // parse the return URL back to an AuthorizeRequest object
            var request = await Interaction.GetAuthorizationContextAsync(model.ReturnUrl);
            ConsentResponse response = null;

            // user clicked 'no' - send back the standard 'access_denied' response
            if (model.Button == "no")
            {
                response = ConsentResponse.Denied;
            }
            // user clicked 'yes' - validate the data
            else if (model.Button == "yes" && model != null)
            {
                // if the user consented to some scope, build the response model
                if (model.ScopesConsented != null && model.ScopesConsented.Any())
                {
                    response = new ConsentResponse
                    {
                        RememberConsent = model.RememberConsent,
                        ScopesConsented = model.ScopesConsented
                    };
                }
                else
                {
                    ModelState.AddModelError("", "You must pick at least one permission.");
                }
            }
            else
            {
                ModelState.AddModelError("", "Invalid Selection");
            }

            if (response != null)
            {
                // communicate outcome of consent back to identityserver
                await Interaction.GrantConsentAsync(request, response);

                // redirect back to authorization endpoint
                return Redirect(model.ReturnUrl);
            }

            var vm = await BuildViewModelAsync(model.ReturnUrl, model);
            if (vm != null)
            {
                return View("Index", vm);
            }

            return View("Error");
        }

        async Task<ConsentViewModel> BuildViewModelAsync(string returnUrl, ConsentInputModel model = null)
        {
            var request = await Interaction.GetAuthorizationContextAsync(returnUrl);
            if (request != null)
            {
                var client = await ClientStore.FindClientByIdAsync(request.ClientId);
                if (client != null)
                {
                    var scopes = await ScopeStore.FindScopesAsync(request.ScopesRequested);
                    if (scopes != null && scopes.Any())
                    {
                        return new ConsentViewModel(model, returnUrl, request, client, scopes);
                    }
                    else
                    {
                        Logger.LogError("No scopes matching: {0}", request.ScopesRequested.Aggregate((x, y) => x + ", " + y));
                    }
                }
                else
                {
                    Logger.LogError("Invalid client id: {0}", request.ClientId);
                }
            }
            else
            {
                Logger.LogError("No consent request matching request: {0}", returnUrl);
            }

            return null;
        }
    }
}