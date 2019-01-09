using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using yuriPortal.Data.Models;
using yuriPortal.Core.Interfaces;

namespace yuriPortal.Core.Repository
{
	public class CommCodeRepository : ICommCodeRepository
	{
		ApplicationDbContext context = new ApplicationDbContext();

		public List<dynamic> GetCommCodes(string selCogdGroup = null)
		{
			//List<dynamic> codes = new List<dynamic>();

			if (selCogdGroup != null && !selCogdGroup.Equals("0"))
			{
				var results = (from c in context.CommCodes
						 join u in context.Users on c.CreateId equals u.Id
						 join cg in context.CodeGroups on c.CodeGroupID equals cg.CodeGroupID
						 where (c.CodeGroupID == selCogdGroup)
						 select new { cg.CodeGroupText, cg.CodeGroupID, c.Code_Value, c.Code_Name, c.Description, c.IsDelete, c.CreateDt, u.UserName }).OrderBy(w => w.CodeGroupText);

				return results.ToList<dynamic>();

			}
			else
			{
				var results = (from c in context.CommCodes
						   join u in context.Users on c.CreateId equals u.Id
						   join cg in context.CodeGroups on c.CodeGroupID equals cg.CodeGroupID
						   //where (c.CodeGroupID == "") && (c.CreateId == u.Id)
						   select new { cg.CodeGroupText, cg.CodeGroupID, c.Code_Value, c.Code_Name, c.Description, c.IsDelete, c.CreateDt, u.UserName }).OrderBy(w => w.CodeGroupText);

				return results.ToList<dynamic>();
			}
		}


		public void SaveCode(CommCode codes, string userId){
			codes.IsDelete = 0;
			codes.UpdateDt = DateTime.Now;
			codes.CreateDt = DateTime.Now;
			codes.CreateId = userId;
			codes.UpdateId = userId;

			context.CommCodes.Add(codes);
			context.SaveChanges();
		}

	}
}
