using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace WebApi.Controllers
{
    /// <summary>
    /// Filters addresses, allowing only those that are in the <see href="https://www.rfc-editor.org/rfc/rfc1918">Private Address Space</see>.
    /// </summary>
    public class RestrictToPrivateAddressSpaceAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var remoteIp = context.HttpContext.Connection.RemoteIpAddress.MapToIPv4();

            if (IsPrivate(remoteIp.GetAddressBytes()) == false)
            {
                context.Result = new UnauthorizedResult();
                return;
            }

            base.OnActionExecuting(context);
        }

        /// <summary>
        /// Checks whether the ip address is in <see href="https://www.rfc-editor.org/rfc/rfc1918">Private Address Space</see>.
        /// Private range goes from 172.16.0.0 to 172.31.255.255.
        /// </summary>
        /// <param name="ipv4">Byte array of ipv4 address.</param>
        /// <returns>true, if the address is in Private Address Space.</returns>
        private static bool IsPrivate(byte[] ipv4)
        {
            return ipv4[0] == 172 && (ipv4[1] & 0xf0) == 16;
        }
    }
}
