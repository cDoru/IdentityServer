using IdentityServer.API.Mocks;
using IdentityServer.API.Models.Account;
using IdentityServer.API.Services;
using IdentityServer4.Services;
using IdentityServer4.Stores;
using IdentityServer4.Test;
using Microsoft.AspNetCore.Http.Authentication;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityServer.API.Controllers
{
	public class AccountController : Controller
	{
		private readonly TestUserStore _users;
		private readonly IIdentityServerInteractionService _interaction;
		private readonly AccountService _accountService;

		public AccountController(IIdentityServerInteractionService interaction, IClientStore clientStore, TestUserStore users = null)
		{
			_users = users ?? new TestUserStore(MockData.TestUsers.ToList());
			_interaction = interaction;
			_accountService = new AccountService(interaction, clientStore);
		}

		// GET: /<controller>/
		[HttpGet]
		public async Task<IActionResult> Login(string returnUrl)
		{
			var vm = await _accountService.BuildLoginViewModelAsync(returnUrl);

			return View(vm);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Login(LoginPostModel model)
		{
			if (!ModelState.IsValid)
				return View(model);

			if (!_interaction.IsValidReturnUrl(model.ReturnUrl))
			{
				// If hacker bleat
				ModelState.AddModelError("", "Bad request");
				return View(model);
			}

			if (!_users.ValidateCredentials(model.Username, model.Password))
			{
				ModelState.AddModelError("", AccountOptions.InvalidCredentialsErrorMessage);
				return View(model);
			}

			AuthenticationProperties authProperties = null;
			if (AccountOptions.AllowRememberLogin && model.RememberLogin)
				authProperties = new AuthenticationProperties {
					IsPersistent = true,
					ExpiresUtc = DateTimeOffset.UtcNow.Add(AccountOptions.RememberMeLoginDuration)
				};

			var user = _users.FindByUsername(model.Username);
			await HttpContext.Authentication.SignInAsync(user.SubjectId, user.Username, authProperties);

			return Redirect(model.ReturnUrl);
		}
	}
}
