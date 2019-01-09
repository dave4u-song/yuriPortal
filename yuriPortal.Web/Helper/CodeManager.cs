using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using yuriPortal.Data.Models;
using yuriPortal.Core;

namespace yuriPortal.Web.Helper
{
	public class CodeManager
	{
		public static List<SelectListItem> getCodeListByGroup(string codeGroup)
		{
			ApplicationDbContext db = new ApplicationDbContext();
			List<SelectListItem> codeList = new List<SelectListItem>();

			//codeList = db.CodeGroups.Where(y => y.CodeGroupID == codeGroup)
			//						.OrderBy(x => x.CodeGroupID)
			//						.Select(CodeGroups => new SelectListItem { Value = CodeGroups.CodeGroupID, Text = CodeGroups.CodeGroupText }).ToList();

			codeList = db.CodeGroups.Where(y => y.CodeGroupID == codeGroup).Join(db.CommCodes,
								cg => cg.CodeGroupID,
								cc => cc.CodeGroupID,
								(cg, cc) => new SelectListItem { Value = cc.Code_Value, Text = cc.Code_Name }).ToList();

			//var results = from cg in db.CodeGroups
			//			  join cc in db.CommCodes on cg.CodeGroupID equals cc.CodeGroupID
			//			  where cg.CodeGroupID == codeGroup
			//			  select new SelectListItem { Value = cc.Code_Value, Text = cc.Code_Name };

			var groupTop = new SelectListItem()
			{
				Value = "0",
				Text = "Choose one"
			};

			codeList.Insert(0, groupTop);

			return codeList.ToList();
		}

		public static List<SelectListItem> getCodeListByGroup(string codeGroup, bool isAll)
		{
			ApplicationDbContext db = new ApplicationDbContext();
			List<SelectListItem> codeList = new List<SelectListItem>();

			//codeList = db.CodeGroups.Where(y => y.CodeGroupID == codeGroup)
			//						.OrderBy(x => x.CodeGroupID)
			//						.Select(CodeGroups => new SelectListItem { Value = CodeGroups.CodeGroupID, Text = CodeGroups.CodeGroupText }).ToList();

			codeList = db.CodeGroups.Where(y => y.CodeGroupID == codeGroup).Join(db.CommCodes,
								cg => cg.CodeGroupID,
								cc => cc.CodeGroupID,
								(cg, cc) => new SelectListItem { Value = cc.Code_Value, Text = cc.Code_Name }).ToList();

			//var results = from cg in db.CodeGroups
			//			  join cc in db.CommCodes on cg.CodeGroupID equals cc.CodeGroupID
			//			  where cg.CodeGroupID == codeGroup
			//			  select new SelectListItem { Value = cc.Code_Value, Text = cc.Code_Name };

			if (isAll)
			{
				var groupTop = new SelectListItem()
				{
					Value = "0",
					Text = "Choose one"
				};

				codeList.Insert(0, groupTop);
			}

			return codeList.ToList();
		}

		public static List<SelectListItem> getCodeListByGroup(string codeGroup, string selectValue)
		{
			ApplicationDbContext db = new ApplicationDbContext();
			List<SelectListItem> codeList = new List<SelectListItem>();

			//codeList = db.CodeGroups.Where(y => y.CodeGroupID == codeGroup)
			//						.OrderBy(x => x.CodeGroupID)
			//						.Select(CodeGroups => new SelectListItem { Value = CodeGroups.CodeGroupID, Text = CodeGroups.CodeGroupText }).ToList();

			codeList = db.CodeGroups.Where(y => y.CodeGroupID == codeGroup).Join(db.CommCodes,
								cg => cg.CodeGroupID,
								cc => cc.CodeGroupID,
								(cg, cc) => new SelectListItem { Value = cc.Code_Value, Text = cc.Code_Name }).ToList();

			//var results = from cg in db.CodeGroups
			//			  join cc in db.CommCodes on cg.CodeGroupID equals cc.CodeGroupID
			//			  where cg.CodeGroupID == codeGroup
			//			  select new SelectListItem { Value = cc.Code_Value, Text = cc.Code_Name };


			var groupTop = new SelectListItem()
			{
				Value = "0",
				Text = "Choose one"
			};

			codeList.Insert(0, groupTop);

			//codeList[0].Selected

			foreach(var item in codeList){
				if (item.Value == selectValue)
					item.Selected = true;
			}

			return codeList.ToList();
		}


		public static List<SelectListItem> getCodeListByGroup(string codeGroup, string selectValue, bool isAll)
		{
			ApplicationDbContext db = new ApplicationDbContext();
			List<SelectListItem> codeList = new List<SelectListItem>();

			codeList = db.CodeGroups.Where(y => y.CodeGroupID == codeGroup).Join(db.CommCodes,
								cg => cg.CodeGroupID,
								cc => cc.CodeGroupID,
								(cg, cc) => new SelectListItem { Value = cc.Code_Value, Text = cc.Code_Name }).ToList();


			if (isAll)
			{
				var groupTop = new SelectListItem()
				{
					Value = "0",
					Text = "Choose one"
				};

				codeList.Insert(0, groupTop);
			}

			foreach (var item in codeList)
			{
				if (item.Value == selectValue)
					item.Selected = true;
			}

			return codeList.ToList();
		}


	}
}