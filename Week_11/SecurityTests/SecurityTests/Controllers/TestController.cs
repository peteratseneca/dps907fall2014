using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
// more...
using System.Security.Claims;

namespace SecurityTests.Controllers
{
    public class TestController : ApiController
    {
        // How to use this controller:
        
        // 1. Load/run this app (in localhost)
        // 2. Load/run the Authorization Server (AS) app (also in localhost)

        // 3. In the AS, if necessary, create three or four accounts; 
        // look at the role names (below) that are being tested

        // 4. Load/run Fiddler, and work with the AS first
        // 5. For each account, call the token endpoint to obtain a token;
        // you can copy-paste them to a plain text file (and identify them)

        // 6. In Fiddler, work with this app and controller next;
        // execute requests that will test the accounts against the controller's methods



        // GET: api/Test
        // Anonymous
        public IEnumerable<string> Get()
        {
            // Container for user and claims info
            List<string> allClaims = new List<string>();

            // Is this request authenticated?
            allClaims.Add("Authenticated = " + (User.Identity.IsAuthenticated ? "Yes" : "No"));
            if (User.Identity.IsAuthenticated)
            {
                // Cast the generic principal to a claims-carrying identity
                var identity = User.Identity as ClaimsIdentity;
                // Extract only the claims
                var claims = identity.Claims
                    .Select(c => new { Type = c.Type, Value = c.Value })
                    .AsEnumerable();
                foreach (var claim in claims)
                {
                    // Create a readable string
                    allClaims.Add(claim.Type + " = " + claim.Value);
                }
            }

            return allClaims;
        }

        // The remaining tests will return...
        // if successfully authorized - a simple string message
        // if authorization fails - HTTP 401

        // Note the URI pattern for these tests
        // They aren't 'real world' or 'best practice', but they're simple

        // Any account
        [Authorize]
        [Route("api/test/{id}/anyaccount")]
        public IEnumerable<string> GetAnyAccount(int id)
        {
            return new string[] { "any account", "works correctly" };
        }

        // Role "User"
        [Authorize(Roles="User")]
        [Route("api/test/{id}/role/user")]
        public IEnumerable<string> GetRoleUser(int id)
        {
            return new string[] { "role user", "works correctly" };
        }

        // Role "Student"
        [Authorize(Roles = "Student")]
        [Route("api/test/{id}/role/student")]
        public IEnumerable<string> GetRolestudent(int id)
        {
            return new string[] { "role student", "works correctly" };
        }

        // Role "Admin"
        [Authorize(Roles = "Admin")]
        [Route("api/test/{id}/role/admin")]
        public IEnumerable<string> GetRoleAdmin(int id)
        {
            return new string[] { "role admin", "works correctly" };
        }

    }

}
