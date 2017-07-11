using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using IdentityServer4.Services;
using IdentityServer.API.Models;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace IdentityServer.API.Controllers
{
    public class HomeController : Controller
    {
		private readonly IIdentityServerInteractionService _interaction;

		public HomeController(IIdentityServerInteractionService interaction)
		{
			_interaction = interaction;
		}

        // GET: /<controller>/
        public IActionResult Index()
        {
            return View();
        }

		public async Task<IActionResult> Error(string errorId)
		{
			var vm = new ErrorViewModel();

			var message = await _interaction.GetErrorContextAsync(errorId);

			vm.Error = message ?? null;

			return View("Error", vm);
		}
    }
}
