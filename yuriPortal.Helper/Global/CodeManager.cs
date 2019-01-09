using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace yuriPortal.Helper.Global
{
	public class CodeManager
	{
		public static List<SelectListItem> getCodeListByGroup2(string codeGroup)
		{
			//ApplicationDbContext db = new ApplicationDbContext();
			List<SelectListItem> codeList = new List<SelectListItem>();

			//codeList = db.CodeGroups.Where(y => y.CodeGroupID == codeGroup).OrderBy(x => x.CodeGroupID).Select(CodeGroups => new SelectListItem { Value = CodeGroups.CodeGroupID, Text = CodeGroups.CodeGroupText }).ToList();
			//ViewBag.. = new SelectList(groupList, "Value", "Text").Distinct();

			var groupTop = new SelectListItem()
			{
				Value = "0",
				Text = "--- select ---"
			};

			return codeList;
		}
	}
}
