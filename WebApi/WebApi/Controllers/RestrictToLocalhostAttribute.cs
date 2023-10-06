using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace WebApi.Controllers
{
    public class RestrictToLocalhostAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var remoteIp = context.HttpContext.Connection.RemoteIpAddress.MapToIPv4();

            if (IsLocal(remoteIp.GetAddressBytes()) == false)
            {
                context.Result = new UnauthorizedResult();
                return;
            }

            base.OnActionExecuting(context);
        }

        private static bool IsLocal(byte[] ipv4)
        {
            return ipv4[0] == 172 && (ipv4[1] & 0xf0) == 16;
        }
    }
}
