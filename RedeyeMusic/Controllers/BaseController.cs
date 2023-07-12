using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace RedeyeMusic.Web.Controllers
{
    [Authorize]
    public class BaseController : Controller
    {
    }
}
