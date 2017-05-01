using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore.Query.Internal;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Primitives;

namespace SampleApplication.Middlewares
{
    public class AuthenticationMiddlewareOptions : AuthenticationOptions
    {
        public string HeaderName { get; } = "Authorization";

        public AuthenticationMiddlewareOptions()
        {
            AutomaticAuthenticate = true;
            AutomaticChallenge = true;
        }
    }

    // Very easy sample for showing a custom middleware
    // For real authentication consider using IdentityServer4
    public class AuthenticationMiddleware
        : Microsoft.AspNetCore.Authentication.AuthenticationMiddleware<AuthenticationMiddlewareOptions>
    {
        public AuthenticationMiddleware(RequestDelegate next, IOptions<AuthenticationMiddlewareOptions> options,
            ILoggerFactory loggerFactory, UrlEncoder encoder) : base(next, options, loggerFactory, encoder)
        {
        }

        protected override AuthenticationHandler<AuthenticationMiddlewareOptions> CreateHandler()
        {
            return new AuthenticationMiddlewareHandler();
        }
    }

    public class AuthenticationMiddlewareHandler : AuthenticationHandler<AuthenticationMiddlewareOptions>
    {
        protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            StringValues headerValues;
            if (!Context.Request.Headers.TryGetValue(Options.HeaderName, out headerValues))
            {
                return AuthenticateResult.Fail("Authorization Header not found");
            }

            string value = headerValues.First();

            if (value != "unicorn")
            {
                return AuthenticateResult.Fail("API Key not recognized");
            }

            var identity = new ClaimsIdentity("apikey");
            var ticket = new AuthenticationTicket(new ClaimsPrincipal(identity), null, "apikey");

            return AuthenticateResult.Success(ticket);
        }
    }
}