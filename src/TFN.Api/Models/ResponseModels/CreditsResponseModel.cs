using System;
using TFN.Api.Models.Base;
using TFN.Domain.Models.Entities;

namespace TFN.Api.Models.ResponseModels
{
    public class CreditsResponseModel : ResourceResponseModel
    {
        public Guid UserId { get; private set; }
        public string Username { get; private set; }
        public int TotalCredits { get; private set; }

        private CreditsResponseModel(Guid id, Guid userId, string username, int totalCredits, string apiUrl)
            : base(GetHref(id,apiUrl),id)
        {
            UserId = userId;
            Username = username;
            TotalCredits = totalCredits;
        }

        private static Uri GetHref(Guid creditId, string apiUrl)
        {
            return new Uri($"{apiUrl}/api/credits/{creditId}");
        }

        internal static CreditsResponseModel From(Credits credits, string apiUrl)
        {
            return new CreditsResponseModel(
                credits.Id,
                credits.UserId,
                credits.Username,
                credits.TotalCredits,
                apiUrl
                );
        }
    }
}