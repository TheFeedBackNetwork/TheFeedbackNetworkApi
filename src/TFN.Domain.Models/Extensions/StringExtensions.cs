using System;
using System.Net.Mail;

namespace TFN.Domain.Models.Extensions
{
    public static class StringExtensions
    {
        public static bool IsUrl(this string url)
        {
            if (string.IsNullOrEmpty(url) || string.IsNullOrWhiteSpace(url))
            {
                return false;
            }
            bool isValidUrl;

            try
            {
                Uri uriResult;
                isValidUrl = Uri.TryCreate(url, UriKind.RelativeOrAbsolute, out uriResult) &&
                             (uriResult.Scheme == Uri.UriSchemeHttp || uriResult.Scheme == Uri.UriSchemeHttps);
            }
            catch (Exception)
            {
                return false;
            }

                

            if (!isValidUrl)
            {
                return false;
            }

            return true;
        }

        public static bool IsEmail(this string email)
        {
            try
            {
                var addr = new MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }

        public static string GetOrigin(this string url)
        {
            if (url != null && (url.StartsWith("http://") || url.StartsWith("https://")))
            {
                var idx = url.IndexOf("//", StringComparison.Ordinal);
                if (idx > 0)
                {
                    idx = url.IndexOf("/", idx + 2, StringComparison.Ordinal);
                    if (idx >= 0)
                    {
                        url = url.Substring(0, idx);
                    }
                    return url;
                }
            }

            return null;
        }

    }
}
