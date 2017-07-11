using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityServer.API.Models.Account
{
    public class LoginViewModel : LoginPostModel
    {
		public bool AllowRememberLogin { get; set; }
		public bool EnableLocalLogin { get; set; }
	}
}
