using IdentityServer4.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityServer.API.Mocks
{
	public static partial class MockData
	{
		public static IEnumerable<ApiResource> ApiResources
		{
			get
			{
				return new List<ApiResource>
				{
					new ApiResource("api1", "API 1")
				};
			}
		}
	}
}
