using System;
using NodaTime;
using TFN.Api.Models.Base;
using TFN.Domain.Models.Entities;
using TFN.Domain.Models.ValueObjects;

namespace TFN.Api.Models.ResponseModels
{
    public class UserResponseModel : ResourceResponseModel
    {
        public string Username { get; private set; }
        public string ProfilePictureUrl { get; private set; }
        public string Email { get; private set; }
        public string Name { get; private set; }
        public DateTime Created { get; private set; }
        public BiographyResponseModel Biography { get; private set; }
        public CreditsResponseModel Credits { get; private set; }

        private UserResponseModel(Guid id, string username
            , string profilePictureUrl, string email, string name,
            BiographyResponseModel biography, DateTime created,
            CreditsResponseModel credits, string apiUrl)
            : base(GetHref(id, apiUrl),id)
        {
            Username = username;
            ProfilePictureUrl = profilePictureUrl;
            Email = email;
            Name = name;
            Biography = biography;
            Created = created;
            Credits = credits;
        }

        private static Uri GetHref(Guid id, string apiUrl)
        {
            return new Uri($"{apiUrl}/api/users/{id}");
        }

        public static UserResponseModel From(User user, Credits credits, string apiUrl)
        {
            return new UserResponseModel(
                user.Id,
                user.Username,
                user.ProfilePictureUrl,
                user.Email,
                user.Name,
                BiographyResponseModel.From(user.Biography),
                user.Created.ToDateTimeUtc(),
                CreditsResponseModel.From(credits,apiUrl), 
                apiUrl 
                );
        }
    }
}