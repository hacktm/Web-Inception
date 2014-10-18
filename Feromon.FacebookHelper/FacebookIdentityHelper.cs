using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Feromon.FacebookHelper
{
    public class FacebookIdentityHelper
    {
        public string GetAccessToken(ClaimsIdentity identity)
        {
            var claims = identity.Claims;
            var accessToken = claims.FirstOrDefault(x => x.Type == "urn:facebook:access_token");
            return accessToken != null ? accessToken.Value : null;
        }

        public string GetUserId(ClaimsIdentity identity)
        {
            var claims = identity.Claims;
            var userId = claims.FirstOrDefault(x => x.Type == "urn:facebook:id");
            return userId != null ? userId.Value : null;
        }
    }
}
