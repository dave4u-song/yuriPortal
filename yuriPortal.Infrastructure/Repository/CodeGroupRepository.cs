using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using yuriPortal.Core.Interfaces;
using yuriPortal.Core.Models;

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
	}
}