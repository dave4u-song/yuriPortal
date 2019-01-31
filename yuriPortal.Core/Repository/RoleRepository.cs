using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using yuriPortal.Core.Interfaces;
using yuriPortal.Data.Models;

namespace yuriPortal.Core.Repository
{
	public class RoleRepository : IRoleRepository
	{
		ApplicationDbContext context = new ApplicationDbContext();

		public List<dynamic> GetRoleList(string seachText = null)
		{
			var results = from role in context.Roles
						  select new
						  {
							  role.Id,
							  role.Name,
						  };

			if (!string.IsNullOrEmpty(seachText))
			{
				results = results.Where(w => w.Name.Contains(seachText));
			}

			results = results.OrderBy(w => w.Name);

			return results.ToList<dynamic>();
		}

	}
}
