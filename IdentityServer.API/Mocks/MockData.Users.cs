using IdentityServer4.Test;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace IdentityServer.API.Mocks
{
	public static partial class MockData
	{
		public static IEnumerable<TestUser> TestUsers
		{
			get
			{
				return new[]
				{
					new TestUser
					{
						SubjectId = "1",
						Username = "alex",
						Password = "pass",

						Claims =
						{
							new Claim("name", "alex")
						}
					}
				};
			}
		}
	}
}
