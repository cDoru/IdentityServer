using IdentityServer4;
using IdentityServer4.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityServer.API.Mocks
{
	public static partial class MockData
	{
		public static IEnumerable<Client> Clients
		{
			get
			{
				return new List<Client>
				{
					new Client
					{
						ClientId = "js",
						ClientName = "JavaScript Client",

						AllowedGrantTypes = GrantTypes.Implicit,
						AllowAccessTokensViaBrowser = true,

						RedirectUris = { "http://localhost:5002/callback.html" },
						PostLogoutRedirectUris = { "http://localhost:5002/" },
						AllowedCorsOrigins = { "http://localhost:5002" },

						AllowedScopes =
						{
							IdentityServerConstants.StandardScopes.OpenId,
							IdentityServerConstants.StandardScopes.Profile,
							"api1"
						}
					}
				};
			}
		}
	}
}
