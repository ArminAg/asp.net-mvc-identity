using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace asp.net_mvc_identity.Models
{
	public class ExtendedUser : IdentityUser
	{
		public ExtendedUser()
		{
			Addresses = new List<Address>();
		}

		public string FullName { get; set; }
		public virtual ICollection<Address> Addresses { get; set; }
	}
}