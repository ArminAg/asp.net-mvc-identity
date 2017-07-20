using asp.net_mvc_identity.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

[assembly: OwinStartup(typeof(asp.net_mvc_identity.Startup))]
namespace asp.net_mvc_identity
{
	public class Startup
	{
		public void Configuration(IAppBuilder app)
		{
			string connectionString = @"Data Source=.;Database=AspNetMvcIdentity2;trusted_connection=yes;";
			app.CreatePerOwinContext(() => new ExtendedUserDbContext(connectionString));
			app.CreatePerOwinContext<UserStore<ExtendedUser>>((opt, cont) => 
				new UserStore<ExtendedUser>(cont.Get<ExtendedUserDbContext>()));
			app.CreatePerOwinContext<UserManager<ExtendedUser>>((opt, cont) => 
				new UserManager<ExtendedUser>(cont.Get<UserStore<ExtendedUser>>()));
			app.CreatePerOwinContext<SignInManager<ExtendedUser, string>>((opt, cont) => 
				new SignInManager<ExtendedUser, string>(cont.Get<UserManager<ExtendedUser>>(), cont.Authentication));

			app.UseCookieAuthentication(new CookieAuthenticationOptions
			{
				AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie
			});
		}
	}
}