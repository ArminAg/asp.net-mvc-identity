using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
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
			string connectionString = @"Data Source=.\SQLEXPRESS;Database=AspNetMvcIdentity;trusted_connection=yes;";
			app.CreatePerOwinContext(() => new IdentityDbContext(connectionString));
			app.CreatePerOwinContext<UserStore<IdentityUser>>((opt, cont) => new UserStore<IdentityUser>(cont.Get<IdentityDbContext>()));
			app.CreatePerOwinContext<UserManager<IdentityUser>>((opt, cont) => new UserManager<IdentityUser>(cont.Get<UserStore<IdentityUser>>()));
		}
	}
}