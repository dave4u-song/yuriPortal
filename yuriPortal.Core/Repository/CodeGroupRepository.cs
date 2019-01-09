using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using yuriPortal.Core.Interfaces;
using yuriPortal.Data.Models;
using yuriPortal.Core;
using Microsoft.AspNet.Identity;

namespace yuriPortal.Core.Repository
{

	public class CodeGroupRepository : ICodeGroupRepository
	{
		ApplicationDbContext context = new ApplicationDbContext();

		public void Add(CodeGroup p)
		{
			context.CodeGroups.Add(p);
			context.SaveChanges();
		}

		public void Edit(CodeGroup p)
		{
			context.Entry(p).State = System.Data.Entity.EntityState.Modified;
		}

		public CodeGroup FindById(string Id)
		{
			var result = (from r in context.CodeGroups where r.CodeGroupID == Id select r).FirstOrDefault();
			return result;
		}

		public List<SelectListItem> GetCodeGroups() {

			List<SelectListItem> groupList = new List<SelectListItem>();

			groupList = context.CodeGroups.OrderBy(x => x.CodeGroupID).Select(CodeGroups => new SelectListItem { Value = CodeGroups.CodeGroupID, Text = CodeGroups.CodeGroupText }).ToList();


			return groupList;
		}

		public void Remove(string Id) { 
			CodeGroup p = context.CodeGroups.Find(Id); 
			context.CodeGroups.Remove(p); 
			context.SaveChanges(); 
		}


		public void CreateGroup(CodeGroup groups) {
			groups.UpdateDt = DateTime.Now;
			groups.CreateDt = DateTime.Now;
			groups.CreateId = System.Web.HttpContext.Current.User.Identity.GetUserId();
			groups.UpdateId = System.Web.HttpContext.Current.User.Identity.GetUserId();

			context.CodeGroups.Add(groups);
			context.SaveChanges();
		}

		public void UpdateCodeStatus(string groupVal, string codeVal, string action){
			var cols = context.CommCodes.Where(x => x.CodeGroupID == groupVal && x.Code_Value == codeVal);
			foreach (var item in cols)
			{
				if (action == "Delete")
					item.IsDelete = 1;
				else
					item.IsDelete = 0;
			}

			context.SaveChanges();
		}

	}
}