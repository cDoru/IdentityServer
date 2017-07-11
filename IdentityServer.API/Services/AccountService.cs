using IdentityServer.API.Models.Account;
using IdentityServer4.Services;
using IdentityServer4.Stores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityServer.API.Services
{
    public class AccountService
    {
		private readonly IIdentityServerInteractionService _interaction;
		private readonly IClientStore _clientStore;

		public AccountService(IIdentityServerInteractionService interaction, IClientStore clientStore)
		{
			_interaction = interaction;
			_clientStore = clientStore;
		}

		public async Task<LoginViewModel> BuildLoginViewModelAsync(string returnUrl)
		{
			var context = await _interaction.GetAuthorizationContextAsync(returnUrl);

			var allowLocal = true;
			if (context?.ClientId != null)
			{
				var client = await _clientStore.FindEnabledClientByIdAsync(context.ClientId);
				if (client != null)
				{
					allowLocal = client.EnableLocalLogin;
				}
			}

			return new LoginViewModel {
				Username = context?.LoginHint,
				ReturnUrl = returnUrl,
				AllowRememberLogin = allowLocal && AccountOptions.AllowRememberLogin,
				EnableLocalLogin = AccountOptions.AllowLocalLogin
			};
		}

		public async Task<LoginViewModel> BuildLoginViewModelAsync(LoginPostModel model)
		{
			var vm = await BuildLoginViewModelAsync(model.ReturnUrl);
			vm.Username = model.Username;
			vm.RememberLogin = model.RememberLogin;
			return vm;
		}
	}
}
