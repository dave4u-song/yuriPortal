using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using yuriPortal.Core.Interfaces;
using yuriPortal.Data.Models;
using yuriPortal.Data.ViewModel;
using yuriPortal.Helper;
using yuriPortal.Web.Helper;

namespace yuriPortal.Web.Controllers
{
    public class MenuController : Controller
    {
		IMenuRepository dbLMenu;

		public MenuController() { }

		public MenuController(IMenuRepository dbLMenu)
		{
			this.dbLMenu = dbLMenu;
		}

		// GET: Menu
		public ActionResult Index()
        {
            return View();
        }

		public ActionResult MenuList()
		{
			List<Menu> items = dbLMenu.GetMenus();

			List<JsTreeNode> JsTrees = new List<JsTreeNode>();

			for (int i = 0; i < items.Count; i++)
			{
				JsTreeNode jNode = new JsTreeNode();

				jNode.text = items[i].MenuName;
				jNode.state = "closed";
				JsTrees.Add(jNode);

			}
				//foreach (var layer in items)
				//{

				//	var sublayers = items.Where(i => i.ParentMenuId == layer.MenuId && i.ParentMenuId != "MN01");

				//	if (sublayers.Any())
				//	{
				//		JsTrees.Add(layer);
				//	}

				//	foreach (var sublayer in sublayers)
				//	{
				//		layer.ChildLayers.Add(sublayer);
				//	}
				//}


				//foreach (var node in items){
				//	jNode.text = node.MenuName;
				//	jNode.state = "closed";
				//	jNode.children = null;
				//	jNode.attr = new JsTreeAttribute { id = node.MenuId, selected = false };
				//	JsTrees.Add(jNode);

				//}

				//for(int i = 0; i< items.Count; i++){
				//	JsTreeNode jNode = new JsTreeNode();

				//	jNode.text = items[i].MenuName;
				//	jNode.state = "closed";
				//	jNode.attr = new JsTreeAttribute { id = items[i].MenuId, selected = false };

				//	if (i < items.Count - 1 && items[i].ParentMenuId != items[i + 1].ParentMenuId)
				//	{
				//		JsTreeNode childJsNode = new JsTreeNode();


				//		var children = new JsTreeNode[] { };

				//		children[].text = items[i].MenuName;
				//		childJsNode.state = "closed";
				//		childJsNode.attr = new JsTreeAttribute { id = items[i].MenuId, selected = false };
				//		childJsNode.children = null;



				//		JsTrees.Add(jNode);
				//	}
				//	else {
				//		jNode.children = null;
				//		JsTrees.Add(jNode);
				//	}

				//}

				return Json(JsTrees, JsonRequestBehavior.AllowGet);
		}

			public JsonResult GetRoot()
		{
			List<Menu> items = dbLMenu.GetMenus();

			return new JsonResult { Data = items, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
		}

		public ActionResult GetChildren(string id)
		{
			List<MenuViewModel> items = dbLMenu.GetMenuNode(id);

			List<JsTreeNode> JsTrees = new List<JsTreeNode>();

			foreach (var node in items)
			{
				JsTreeNode jNode = new JsTreeNode();

				jNode.id = node.MenuId;
				jNode.text = node.MenuName;

				if (node.ChildCnt > 0)
				{
					jNode.children = true;
				}
				else
				{
					jNode.children = false;
					jNode.icons = "file";
				}

				jNode.state = "closed";
				jNode.attr = new JsTreeAttribute { id = node.MenuId, selected = false };
				JsTrees.Add(jNode);
			}

			return Json(JsTrees, JsonRequestBehavior.AllowGet);
		}

		public ActionResult GetMenuInfo(string id)
		{
			MenuViewModel items = dbLMenu.GetMenuInfo(id);
			return Json(items, JsonRequestBehavior.AllowGet);
		}

		public JsonResult SaveMenu(Menu menuInfo)
		{

			try
			{
				// TODO: Add insert logic here
				dbLMenu.SaveMenu(menuInfo);

				return Json(new { success = true }, JsonRequestBehavior.AllowGet);
			}
			catch (Exception ex)
			{
				var error = ex.Message.ToString();
				return Json(new { success = false, SaveError = "Error" }, JsonRequestBehavior.AllowGet);
			}
		}

		public JsonResult UpdateMenu(Menu menuInfo)
		{

			try
			{
				// TODO: Add insert logic here
				dbLMenu.UpdateMenu(menuInfo);

				return Json(new { success = true, responseText = "The menu is successfuly updated!" }, JsonRequestBehavior.AllowGet);
			}
			catch (Exception ex)
			{
				var error = ex.Message.ToString();
				return Json(new { success = false, SaveError = "Error" }, JsonRequestBehavior.AllowGet);
			}
		}


		public ActionResult FindApps(){
			return PartialView("FindApps");
		}

		public ActionResult FindAppsPath(string searchText = null)
		{
			MenuViewModel items = dbLMenu.GetMenuInfo(searchText);
			return Json(items, JsonRequestBehavior.AllowGet);
		}

		public ActionResult GetAppspath(int pageNumber = 1, int pageSize = 15, string seachText = null)
		{

			try
			{
				List<AppPath> lists = new List<AppPath>();
				lists = dbLMenu.GetAppsPathList(seachText);

				var pagedData = Pagination.PagedResult(lists, pageNumber, pageSize);
				return Json(pagedData, JsonRequestBehavior.AllowGet);
			}
			catch (Exception ex)
			{
				var error = ex.Message.ToString();
				return Json(new { success = false, SaveError = error }, JsonRequestBehavior.AllowGet);
			}
		}


		// GET: AppsPath
		public ActionResult AppsList()
		{
			return View();
		}

		public ActionResult CreateApps(string id = null){

			ViewBag.AppPathId = id;
			return PartialView("CreateApps");
		}


		public ActionResult ViewApps(string AppsPathId = null)
		{
			ViewBag.AppPathId = AppsPathId;
			return PartialView("CreateApps", dbLMenu.ViewContent(AppsPathId));
		}

		public JsonResult SavePath(AppPath appPath){

			try
			{
				// TODO: Add insert logic here
				if (ModelState.IsValid)
				{
					if (string.IsNullOrEmpty(appPath.AppPathId))
						dbLMenu.SavePath(appPath, System.Web.HttpContext.Current.User.Identity.GetUserId());
					else
						dbLMenu.UpdatePath(appPath, System.Web.HttpContext.Current.User.Identity.GetUserId());
				}


				return Json(new { success = true }, JsonRequestBehavior.AllowGet);
			}
			catch (Exception ex)
			{
				var error = ex.Message.ToString();
				return Json(new { success = false, SaveError = "Error" }, JsonRequestBehavior.AllowGet);
			}
		}

		public ActionResult ManageRoleMap(string MenuID = null)
		{
			//ViewBag.MenuID = MenuID;
			//dbLMenu.ViewContent(MenuID)
			return PartialView("ManageRoleMap");
		}



	}
}