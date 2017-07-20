using asp.net_mvc_identity.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace asp.net_mvc_identity.Controllers
{
    public class AccountController : Controller
    {
		public UserManager<ExtendedUser> UserManager => HttpContext.GetOwinContext().Get<UserManager<ExtendedUser>>();
		public SignInManager<ExtendedUser, string> SignInManager => HttpContext.GetOwinContext().Get<SignInManager<ExtendedUser, string>>();

		public ActionResult Login()
		{
			return View();
		}

		[HttpPost]
		public async Task<ActionResult> Login(LoginModel model)
		{
			var signInStatus = await SignInManager.PasswordSignInAsync(model.Username, model.Password, true, true);

			switch (signInStatus)
			{
				case SignInStatus.Success:
					return RedirectToAction("Index", "Home");
				case SignInStatus.LockedOut:
					break;
				case SignInStatus.RequiresVerification:
					break;
				case SignInStatus.Failure:
					break;
				default:
					ModelState.AddModelError("", "Invalid Credentials");
					return View(model);
			}
			return View();
		}
		
        public ActionResult Register()
        {
            return View();
        }

		[HttpPost]
		public async Task<ActionResult> Register(RegisterModel model)
		{
			//var identityUser = await UserManager.FindByNameAsync(model.Username);
			//if (identityUser != null)
			//{
			//	return RedirectToAction("Index", "Home");
			//}

			var user = new ExtendedUser
			{
				UserName = model.Username,
				FullName = model.FullName,

			};
			user.Addresses.Add(new Address
			{
				AddressLine = model.AddressLine,
				Country = model.Country,
				UserId = user.Id
			});
			var identityResult = await UserManager.CreateAsync(user, model.Password);

			if (identityResult.Succeeded)
			{
				return RedirectToAction("Index", "Home");
			}

			ModelState.AddModelError("", identityResult.Errors.FirstOrDefault());

			return View(model);
		}
    }
}