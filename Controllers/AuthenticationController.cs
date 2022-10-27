using Microsoft.AspNetCore.Mvc;

namespace OpenBankClient.Controllers
{
    [Route("[controller]/[action]")]
    public class AuthenticationController : Controller
    {
        public IActionResult Index(string redirectUri)
        {

            return LocalRedirect(redirectUri);
        }
    }
}
