using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using yuriPortal.Data.ViewModel;

namespace yuriPortal.Core.Interfaces
{
	public interface IUserProfileRepository
	{
		void SaveUserProfile(string userId, UserSaveViewModel userInfo);
		void SaveUserPhoto(string UserName, string fileName, string strExt, string strSaveFileName);
		List<dynamic> GetUsers(string searchText = null);

		UserSaveViewModel GetUserDetail(string userNm, string defaultImagePath);
	}
}
