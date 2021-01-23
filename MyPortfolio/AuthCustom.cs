using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Http.Filters;
using System.Web.Http.Results;

namespace MyPortfolio
{
    public class AuthCustom : AuthorizeAttribute, IAuthenticationFilter
    {
        public bool AllowMultiple
        {
            get { return false; }
        }

        public async Task AuthenticateAsync(HttpAuthenticationContext context, CancellationToken cancellationToken)
        {
            string authParameter = string.Empty;
            HttpRequestMessage request = context.Request;
            AuthenticationHeaderValue authozition = request.Headers.Authorization;

            if (authozition == null)
            {
                context.ErrorResult = new AuthenticationFailurResult("Missing Authorization Header", request);
                return;
            }
            if (authozition.Scheme != "Bearer")
            {
                context.ErrorResult = new AuthenticationFailurResult("Invalid Authorization Schema", request);
                return;
            }
            if (String.IsNullOrEmpty(authozition.Parameter))
            {
                context.ErrorResult = new AuthenticationFailurResult("Missing Token", request);
                return;
            }

            context.Principal = TokenManager.GetPrincipal(authozition.Parameter);
        }

        public async Task ChallengeAsync(HttpAuthenticationChallengeContext context, CancellationToken cancellationToken)
        {
            var result = await context.Result.ExecuteAsync(cancellationToken);
            if (result.StatusCode == HttpStatusCode.Unauthorized)
            {
                result.Headers.WwwAuthenticate.Add(new AuthenticationHeaderValue("Basic", "realm=localhost"));
            }
            context.Result = new ResponseMessageResult(result);
        }
    }
    public class AuthenticationFailurResult : IHttpActionResult
    {
        public string ReasonPhares;
        public HttpRequestMessage Request { get; set; }


        public AuthenticationFailurResult(string reasonPhrase, HttpRequestMessage request)
        {
            ReasonPhares = reasonPhrase;
            Request = request;
        }

        public Task<HttpResponseMessage> ExecuteAsync(CancellationToken cancellationToken)
        {
            return Task.FromResult(Execute());
        }

        public HttpResponseMessage Execute()
        {
            HttpResponseMessage responseMessage = new HttpResponseMessage(HttpStatusCode.Unauthorized);
            responseMessage.RequestMessage = Request;
            responseMessage.ReasonPhrase = ReasonPhares;
            return responseMessage;
        }
    }
}