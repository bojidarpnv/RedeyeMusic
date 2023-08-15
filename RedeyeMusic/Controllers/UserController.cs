using Griesoft.AspNetCore.ReCaptcha;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using RedeyeMusic.Data.Models;
using RedeyeMusic.Web.ViewModels.User;
using static RedeyeMusic.Common.GeneralApplicationConstants;
using static RedeyeMusic.Common.NotificationMessagesConstants;
namespace RedeyeMusic.Web.Controllers
{
    public class UserController : Controller
    {
        private readonly SignInManager<ApplicationUser> signInManager;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IUserStore<ApplicationUser> userStore;
        private readonly IMemoryCache memoryCache;
        public UserController(SignInManager<ApplicationUser> signInManager, UserManager<ApplicationUser> userManager, IUserStore<ApplicationUser> userStore, IMemoryCache memoryCache)
        {
            this.signInManager = signInManager;
            this.userManager = userManager;
            this.userStore = userStore;
            this.memoryCache = memoryCache;
        }
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        [ValidateRecaptcha(Action = nameof(Register),
            ValidationFailedAction = ValidationFailedAction.ContinueRequest)]
        public async Task<IActionResult> Register(RegisterFormModel model)
        {
            if (!ModelState.IsValid)
            {
                return this.View(model);
            }
            ApplicationUser user = new ApplicationUser()
            {

                FirstName = model.FirstName,
                LastName = model.LastName,

            };
            await this.userManager.SetEmailAsync(user, model.Email);
            await this.userManager.SetUserNameAsync(user, model.Email);

            IdentityResult result = await this.userManager.CreateAsync(user, model.Password);
            if (!result.Succeeded)
            {
                foreach (IdentityError error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
                return this.View(model);
            }

            await this.signInManager.SignInAsync(user, isPersistent: false);
            this.memoryCache.Remove(UsersCacheKey);
            this.TempData[SuccessMessage] = "Successful registration!";
            return RedirectToAction("Index", "Home");
        }
        [HttpGet]
        public async Task<IActionResult> Login(string? returnUrl = null)
        {
            await HttpContext.SignOutAsync(IdentityConstants.ExternalScheme);

            LoginFormModel model = new LoginFormModel()
            {
                ReturnUrl = returnUrl,
            };
            return this.View(model);
        }
        [HttpPost]
        [ValidateRecaptcha(Action = nameof(Login),
            ValidationFailedAction = ValidationFailedAction.ContinueRequest)]
        public async Task<IActionResult> Login(LoginFormModel model)
        {
            if (!ModelState.IsValid)
            {
                return this.View(model);
            }

            Microsoft.AspNetCore.Identity.SignInResult? result =
                await this.signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, false);
            if (!result.Succeeded)
            {
                this.TempData[ErrorMessage] = "There was an error while signing in! Please try again later!";
                return this.View(model);
            }
            return this.Redirect(model.ReturnUrl ?? "/Home/Index");
        }
    }
}
