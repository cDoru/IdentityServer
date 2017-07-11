using IdentityServer4.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityServer.API.Mocks
{
	public static partial class MockData
	{
		public static IEnumerable<IdentityResource> IdentityResources
		{
			get
			{
				return new List<IdentityResource>
				{
					new IdentityResources.OpenId(),
					new IdentityResources.Profile()
				};
			}
		}
	}
}
