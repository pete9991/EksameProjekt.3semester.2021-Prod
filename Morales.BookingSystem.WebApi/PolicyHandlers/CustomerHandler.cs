using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Morales.BookingSystem.Security;
using Morales.BookingSystem.Security.Models;

namespace Morales.BookingSystem.PolicyHandlers
{
    public class CustomerHandler : AuthorizationHandler<CustomerHandler>, IAuthorizationRequirement
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, CustomerHandler handler)
        {
            var defaultContext = context.Resource as DefaultHttpContext;
            if (defaultContext != null)
            {
                var user = defaultContext.Items["LoginUser"] as LoginUser;
                if (user != null)
                {
                    var authService = defaultContext.HttpContext.RequestServices.GetRequiredService<IAuthService>();
                    var permission = authService.GetPermissions(user.Id);
                    if (permission.Exists(p => p.Name.Equals("Customer")))
                    {
                        context.Succeed(handler);
                    }
                    else
                    {
                        context.Fail();
                    }
                }
            }
            else
            {
                context.Fail();
            }
            return Task.CompletedTask;
            ;
        }
    }
    }
