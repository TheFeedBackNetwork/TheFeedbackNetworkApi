using System;
using Microsoft.AspNetCore.SignalR;
using TFN.Api.Extensions;

namespace TFN.Api.Hubs
{
    public class AppHub : Hub
    {
        protected Guid UserId => Context.GetUserId().Value;

        protected string Username => Context.GetUsername();
    }
}