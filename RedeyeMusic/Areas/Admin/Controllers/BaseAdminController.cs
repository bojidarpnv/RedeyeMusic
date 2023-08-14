using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using static RedeyeMusic.Common.GeneralApplicationConstants;
namespace RedeyeMusic.Web.Areas.Admin.Controllers
{
    [Area(AdminAreaName)]
    [Authorize(Roles = AdminRoleName)]
    public class BaseAdminController : Controller
    {

    }
}
