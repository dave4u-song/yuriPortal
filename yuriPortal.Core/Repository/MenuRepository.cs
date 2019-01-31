using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using yuriPortal.Core.Interfaces;
using yuriPortal.Data.Models;
using yuriPortal.Data.ViewModel;

namespace yuriPortal.Core.Repository
{
	public class MenuRepository : IMenuRepository
	{
		ApplicationDbContext context = new ApplicationDbContext();

		public List<Menu> GetMenus(string parenMenuID = null)
		{
			if (string.IsNullOrEmpty(parenMenuID))
				parenMenuID = "MN01";

			var list = context.Menus.Where(c => c.MenuLevel == 1).ToList();


			return list;
		}

		public void SaveMenu(Menu menuInfo)
		{
			menuInfo.IsDelete = 0;
			menuInfo.UpdateDt = DateTime.Now;
			menuInfo.CreateDt = DateTime.Now;
			menuInfo.CreateId = System.Web.HttpContext.Current.User.Identity.GetUserId();
			menuInfo.UpdateId = System.Web.HttpContext.Current.User.Identity.GetUserId();

			context.Menus.Add(menuInfo);
			context.SaveChanges();
		}

		public void UpdateMenu(Menu menuInfo)
		{
			var item = context.Menus.Where(x => x.MenuId == menuInfo.MenuId).Single();

			item.MenuName = menuInfo.MenuName;
			item.MenuLevel = menuInfo.MenuLevel;
			item.OrderNo = menuInfo.OrderNo;
			item.AppPathId = menuInfo.AppPathId;
			item.UpdateDt = DateTime.Now;
			item.UpdateId = System.Web.HttpContext.Current.User.Identity.GetUserId();

			context.SaveChanges();
		}

		public AppPath ViewContent(string AppPathId)
		{
			var results = (from app in context.AppPaths
						   where app.AppPathId == AppPathId
						   select new
						   {
							   app.AppPathId,
							   app.AppPathName,
							   app.Description,
							   app.PagePath,
							   app.Parameters,
							   app.IsDelete,
							   app.CreateDt,
							   app.CreateId,
							   app.UpdateDt,
							   app.UpdateId
						   }).FirstOrDefault();


			var viweModel = new AppPath
			{
				AppPathId = results.AppPathId,
				AppPathName = results.AppPathName,
				Description = results.Description,
				PagePath = results.PagePath,
				Parameters = results.Parameters,
				IsDelete = results.IsDelete,
				CreateDt = results.CreateDt,
				CreateId = results.CreateId,
				UpdateDt = results.UpdateDt,
				UpdateId = results.UpdateId
			};

			return viweModel;
		}

		public List<AppPath> GetAppsPathList(string searchText = null)
		{
			var treeNodes = context.Database.SqlQuery<AppPath>(@"SELECT a.AppPathId, a.AppPathName, a.PagePath, a.Parameters, a.IsDelete, a.Description, 
								a.CreateDt,
								a.CreateId,
								a.UpdateDt,
								a.UpdateId,
								u.FirstName + ' ' + u.LastName As FullUserName 
							FROM dbo.AppPath a
							INNER JOIN  dbo.UserProfile u
							ON
								a.CreateId = u.UserId
							WHERE a.AppPathName LIKE '%'+IsNull(@searchText, '')+'%'
							", new SqlParameter("searchText", searchText)).ToList();

					treeNodes.Select(x => new AppPath()
					{
						AppPathId = x.AppPathId,
						AppPathName = x.AppPathName,
						PagePath = x.PagePath,
						Parameters = x.Parameters,
						Description = x.Description,
						IsDelete = x.IsDelete,
						FullUserName = x.FullUserName
					}).ToList();

			return treeNodes;
		}


		public void SavePath(AppPath appPaths, string userId)
		{
			var guid = Guid.NewGuid();

			appPaths.AppPathId = guid.ToString();
			appPaths.IsDelete = 0;
			appPaths.UpdateDt = DateTime.Now;
			appPaths.CreateDt = DateTime.Now;
			appPaths.CreateId = userId;
			appPaths.UpdateId = userId;

			context.AppPaths.Add(appPaths);
			context.SaveChanges();
		}

		public void UpdatePath(AppPath appPaths, string userId)
		{
			var item = context.AppPaths.Where(x => x.AppPathId == appPaths.AppPathId).Single();

			item.AppPathName	= appPaths.AppPathName;
			item.PagePath		= appPaths.PagePath;
			item.Parameters		= appPaths.Parameters;
			item.Description	= appPaths.Description;
			item.UpdateDt		= DateTime.Now;
			item.UpdateId		= userId;

			context.SaveChanges();
		}


		public MenuViewModel GetMenuInfo(string id){
			var treeNodes = context.Database.SqlQuery<MenuViewModel>(@"SELECT c.MenuId, c.MenuName, c.ParentMenuId, c.OrderNo, c.IsDelete, a.AppPathId, a.PagePath 
							FROM Menu c
							left outer join dbo.AppPath a
							on
								c.AppPathId = a.AppPathId
							where MenuId = @MenuId
							", new SqlParameter("MenuId", id))
					.Select(x => new MenuViewModel()
					{
						MenuId = x.MenuId,
						MenuName = x.MenuName,
						MenuLevel = x.MenuLevel,
						ParentMenuId = x.ParentMenuId,
						OrderNo = x.OrderNo,
						IsDelete = x.IsDelete,
						AppPathId = x.AppPathId,
						PagePath = x.PagePath
					}).FirstOrDefault();

			return treeNodes;
		}

		public List<MenuViewModel> GetMenuNode(string id)
		{
			if (string.IsNullOrEmpty(id) || id == "#")
				id = "MN01";

			var treeNodes = context.Database.SqlQuery<MenuViewModel>(@"SELECT c.MenuId, 1 as level, c.MenuName, c.ParentMenuId, c.OrderNo, c.IsDelete, 
										(select count(*) from Menu Where ParentMenuId = c.MenuId) childCnt
								FROM Menu c
								where ParentMenuId = @ParentId order by OrderNo
							", new SqlParameter("ParentId", id))
							.Select(x => new MenuViewModel()
							{
								MenuId = x.MenuId,
								MenuName = x.MenuName,
								MenuLevel = x.MenuLevel,
								ParentMenuId = x.ParentMenuId,
								OrderNo = x.OrderNo,
								IsDelete = x.IsDelete,
								ChildCnt = x.ChildCnt
							}).ToList();

			return treeNodes;
		}

		public List<MenuViewModel> MenuList()
		{
			var comments = context.Database.SqlQuery<MenuViewModel>(@"WITH tree (MenuId, level, MenuName, ParentMenuId, OrderNo, IsDelete, CreateDt, CreateId, UpdateDt, UpdateId, FullName, rn) as 
							(
								SELECT c.MenuId, 1 as level, c.MenuName, c.ParentMenuId, c.OrderNo, c.IsDelete, c.CreateDt, c.CreateId, c.UpdateDt, c.UpdateId, u.FirstName + ' ' + u.LastName As FullName,
										convert(varchar(max),right(1000000000 + row_number() over (order by OrderNo),10)) rn
								FROM Menu c
								inner join UserProfile u
								on	
									c.CreateId = u.UserId
								where ParentMenuId = 'MN01'

								UNION ALL

								SELECT c2.MenuId, tree.level + 1, c2.MenuName, c2.ParentMenuId, c2.OrderNo, c2.IsDelete, c2.CreateDt, c2.CreateId, c2.UpdateDt, c2.UpdateId, u.FirstName + ' ' + u.LastName As FullName,
										rn + '/' + convert(varchar(max),right(1000000000 + row_number() over (order by c2.OrderNo),10)) 
								FROM Menu c2
								inner join UserProfile u
								on	
									c2.CreateId = u.UserId
								INNER JOIN tree 
								ON 
									tree.MenuId = c2.ParentMenuId
							)
							SELECT MenuId, MenuName, level as MenuLevel, ParentMenuId, OrderNo, CreateDt, CreateId, UpdateDt, UpdateId, FullName
							FROM tree order by RN
							")
							.Select(x => new MenuViewModel()
							{
								MenuId = x.MenuId,
								MenuName = x.MenuName,
								MenuLevel = x.MenuLevel,
								ParentMenuId = x.ParentMenuId,
								OrderNo = x.OrderNo,
								IsDelete = x.IsDelete,
								CreateDt = x.CreateDt,
								CreateId = x.CreateId,
								UpdateDt = x.UpdateDt,
								UpdateId = x.UpdateId,
								FullName = x.FullName,
							}).ToList();

			return comments;

		}

		public List<MenuViewModel> AdminMenuList()
		{
			var comments = context.Database.SqlQuery<MenuViewModel>(@"WITH tree (MenuId, level, MenuName, ParentMenuId, AppPathId, rn) as 
							(
								SELECT c.MenuId, 1 as level, c.MenuName, c.ParentMenuId, c.AppPathId,
										convert(varchar(max),right(1000000000 + row_number() over (order by OrderNo),10)) rn
								FROM Menu c
								where ParentMenuId = 'MN000003' and IsDelete = 0
								UNION ALL
								SELECT c2.MenuId, tree.level + 1, c2.MenuName, c2.ParentMenuId, c2.AppPathId,
										rn + '/' + convert(varchar(max),right(1000000000 + row_number() over (order by c2.OrderNo),10)) 
								FROM Menu c2
								INNER JOIN tree 
								ON tree.MenuId = c2.ParentMenuId
								Where c2.IsDelete = 0
							)
							SELECT MenuId, MenuName, level as MenuLevel, ParentMenuId, a.PagePath
							FROM tree 
							Left outer join AppPath a
							on	tree.AppPathId = a.AppPathId
							order by RN
							")
							.Select(x => new MenuViewModel()
							{
								MenuId = x.MenuId,
								MenuName = x.MenuName,
								MenuLevel = x.MenuLevel,
								ParentMenuId = x.ParentMenuId,
								PagePath = x.PagePath,
							}).ToList();

			return comments;

		}
	}
}
