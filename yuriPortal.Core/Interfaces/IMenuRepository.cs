using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using yuriPortal.Data.Models;
using yuriPortal.Data.ViewModel;

namespace yuriPortal.Core.Interfaces
{
	public interface IMenuRepository
	{
		List<Menu> GetMenus(string parenMenuID = null);

		List<MenuViewModel> GetMenuNode(string id);

		MenuViewModel GetMenuInfo(string id);

		List<MenuViewModel> MenuList();

		void SaveMenu(Menu menuInfo);

		void UpdateMenu(Menu menuInfo);

		void SavePath(AppPath appPaths, string userId);

		void UpdatePath(AppPath appPaths, string userId);

		List<AppPath> GetAppsPathList(string searchText = null);

		AppPath ViewContent(string AppPathId);

		List<MenuViewModel> AdminMenuList();
	}
}
