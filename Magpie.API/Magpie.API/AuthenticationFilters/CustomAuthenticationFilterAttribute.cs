using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http.Filters;

namespace Magpie.API.AuthenticationFilters
{
    public class CustomAuthenticationFilterAttribute : Attribute, IAuthenticationFilter
    {
        public async Task AuthenticateAsync(HttpAuthenticationContext context, CancellationToken cancellationToken)
        {
            await Task.Run(() =>
            {
                IPrincipal incomingPrincipal = context.ActionContext.RequestContext.Principal;
                IPrincipal genericPrincipal = new GenericPrincipal(new GenericIdentity("Rob", "CustomIdentification"), new string[] { "Staff" });
                context.Principal = genericPrincipal;
            });
        }

        public async Task ChallengeAsync(HttpAuthenticationChallengeContext context, CancellationToken cancellationToken)
        {
            await Task.Run(() =>
            {
                IPrincipal incomingPrincipal = context.ActionContext.RequestContext.Principal;
            });
        }

        public bool AllowMultiple
        {
            get { return false; }
        }
    }
}