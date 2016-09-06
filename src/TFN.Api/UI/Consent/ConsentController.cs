using System.Linq;
using System.Threading.Tasks;
using IdentityServer4.Models;
using IdentityServer4.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using TFN.Mvc.Constants;

namespace TheFeedBackNetworkApi.UI.Consent
{
    public class ConsentController : Controller
    {
        private readonly ILogger<ConsentController> Logger;
        private readonly IClientStore ClientStore;
        private readonly IUserInteractionService Interaction;
        private readonly IScopeStore ScopeStore;
        private readonly ILocalizationService Localization;

        public ConsentController(
            ILogger<ConsentController> logger,
            IUserInteractionService interaction,
            IClientStore clientStore,
            IScopeStore scopeStore,
            ILocalizationService localization)
        {
            Logger = logger;
            Interaction = interaction;
            ClientStore = clientStore;
            ScopeStore = scopeStore;
            Localization = localization;
        }

        [HttpGet(RoutePaths.ConsentUrl, Name = "Consent")]
        public async Task<IActionResult> Index(string returnUrl)
        {
            var vm = await BuildViewModelAsync(returnUrl);
            if (vm != null)
            {
                return View("Index", vm);
            }

            return View("Error");
        }

        [HttpPost(RoutePaths.ConsentUrl)]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index(string button, ConsentInputModel model)
        {
            var request = await Interaction.GetConsentContextAsync(model.ReturnUrl);
            ConsentResponse response = null;

            if (button == "no")
            {
                response = ConsentResponse.Denied;
            }
            else if (button == "yes" && model != null)
            {
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
                await Interaction.GrantConsentAsync(request, response);
                return Redirect(model.ReturnUrl);
            }

            var vm = await BuildViewModelAsync(model.ReturnUrl, model);
            if (vm != null)
            {
                return View("Index", vm);
            }

            return View("Error");
        }

        //async Task<IActionResult> BuildConsentResponse(string id, string[] scopesConsented, bool rememberConsent)
        //{
        //    if (id != null)
        //    {
        //        var request = await Interaction.GetRequestAsync(id);
        //    }

        //    return View("Error");
        //}

        async Task<ConsentViewModel> BuildViewModelAsync(string returnUrl, ConsentInputModel model = null)
        {
            if (returnUrl != null)
            {
                var request = await Interaction.GetConsentContextAsync(returnUrl);
                if (request != null)
                {
                    var client = await ClientStore.FindClientByIdAsync(request.ClientId);
                    if (client != null)
                    {
                        var scopes = await ScopeStore.FindScopesAsync(request.ScopesRequested);
                        if (scopes != null && scopes.Any())
                        {
                            return new ConsentViewModel(model, returnUrl, request, client, scopes, Localization);
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
                    Logger.LogError("No consent request matching id: {0}", returnUrl);
                }
            }
            else
            {
                Logger.LogError("No returnUrl passed");
            }

            return null;
        }
    }
}
