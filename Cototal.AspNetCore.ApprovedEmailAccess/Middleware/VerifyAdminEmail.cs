using Microsoft.AspNetCore.Http;
using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Cototal.AspNetCore.ApprovedEmailAccess.Middleware
{
    /**
     * This assumes the [Authorize] tag is in use. AdminEmails should be separated by a semi-colon
     */
    public class VerifyAdminEmail
    {
        private readonly RequestDelegate _next;
        private readonly IVerifyAdminEmailOptions _options;

        public VerifyAdminEmail(RequestDelegate next, IVerifyAdminEmailOptions options)
        {
            _next = next;
            _options = options;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var user = context.User;
            if (user == null)
            {
                await _next(context);
                return;
            }

            var id = user.Identities.FirstOrDefault();
            if (id == null)
            {
                await _next(context);
                return;
            }

            var email = id.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email);
            if (email == null)
            {
                await _next(context);
                return;
            }
            if (_options.AdminEmails.Count() == 0 || !_options.AdminEmails.Contains(email.Value) || email.Issuer != _options.ProviderName)
            {
                context.Response.StatusCode = 401;
                await context.Response.WriteAsync("Unauthorized");
                return;
            }

            await _next(context);
        }
    }
}
