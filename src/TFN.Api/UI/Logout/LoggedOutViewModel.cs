using IdentityServer4.Models;
using System.Collections.Generic;
using System.Linq;

namespace TFN.UI.Logout
{
    public class LoggedOutViewModel
    {
        public string PostLogoutRedirectUri { get; set; }
        public string ClientName { get; set; }
        public string SignOutIframeUrl { get; set; }
    }
}
