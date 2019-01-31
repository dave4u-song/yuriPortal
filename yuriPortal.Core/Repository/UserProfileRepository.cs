using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using yuriPortal.Core.Interfaces;
using yuriPortal.Data.Models;
using yuriPortal.Data.ViewModel;

namespace yuriPortal.Core.Repository
{
	public class UserProfileRepository : IUserProfileRepository
	{
		ApplicationDbContext context = new ApplicationDbContext();

		public List<dynamic> GetUsers(string searchText = null)
		{

			var results = from us in context.Users
						  join prof in context.UserProfiles on us.Id equals prof.UserId into usp
						  from profile in usp.DefaultIfEmpty()
						  select new
						  {
							  us.Id,
							  us.UserName,
							  PhoneNumber = us.PhoneNumber ?? "",
							  us.IsDelete,
							  us.LastAccessDt,
							  FirstName = profile.FirstName ?? "",
							  LastName = profile.LastName ?? "",
							  City = profile.City ?? "",
							  Province = profile.Province ?? "",
							  LanguageCd = profile.LanguageCd ?? ""
						  };

			return results.ToList<dynamic>();

		}

		public UserSaveViewModel GetUserDetail(string userNm, string defaultImagePath)
		{
			var results = (from us in context.Users
						   join pro in context.UserProfiles on us.Id equals pro.UserId into usp
						   from profile in usp.DefaultIfEmpty()
						   join pic in context.AttachFiles on profile.UserPic equals pic.FileId into photo
						   from userphoto in photo.DefaultIfEmpty()
						   where us.UserName == userNm
						   select new
						   {
							   us.Id,
							   us.UserName,
							   us.Email,
							   PhoneNumber = us.PhoneNumber ?? "",
							   us.IsDelete,
							   us.LastAccessDt,
							   FirstName = profile.FirstName ?? "",
							   LastName = profile.LastName ?? "",
							   Street = profile.Street ?? "",
							   City = profile.City ?? "",
							   Province = profile.Province ?? "",
							   Country = profile.Country ?? "",
							   PostalCode = profile.PostalCode ?? "",
							   Gender = profile.Gender ?? "",
							   LanguageCd = profile.LanguageCd ?? "",
							   UserPic = userphoto.FileId == null ? defaultImagePath : userphoto.FilePath + userphoto.SaveAsFileName
						   }).FirstOrDefault();

			var viweModel = new UserSaveViewModel
			{
				UserId = results.Id,
				UserName = results.UserName,
				FirstName = results.FirstName,
				LastName = results.LastName,
				PhoneNumber = results.PhoneNumber,
				IsDelete = results.IsDelete,
				AddressCity = results.City,
				AddressStreet = results.Street,
				AddressProvince = results.Province,
				AddressCountry = results.Country,
				Email = results.Email,
				Language = results.LanguageCd,
				Gender = results.Gender,
				PostalCode = results.PostalCode,
				UserPic = results.UserPic
			};

			return viweModel;
		}


		public void UpdateUserProfile(UserSaveViewModel userInfo)
		{
			var item = context.UserProfiles.Where(x => x.UserId == userInfo.UserId).Single();

			item.FirstName = userInfo.FirstName;
			item.LastName = userInfo.LastName;
			item.Street = userInfo.AddressStreet;
			item.City = userInfo.AddressCity;

			item.Province = userInfo.AddressProvince;
			item.Country = userInfo.AddressCountry;

			item.PostalCode = userInfo.PostalCode;
			item.Gender = userInfo.Gender;
			item.LanguageCd = userInfo.Language;

			context.SaveChanges();
		}

		public void SaveUserProfile(string userId, UserSaveViewModel userInfo)
		{
			UserProfile userProfile = new UserProfile()
			{
				UserId			= userId,
				FirstName		= userInfo.FirstName,
				LastName		= userInfo.LastName,
				Street			= userInfo.AddressStreet,
				City			= userInfo.AddressCity,
				Province		= userInfo.AddressProvince,
				Country			= userInfo.AddressCountry,
				PostalCode		= userInfo.PostalCode,
				Gender			= userInfo.Gender,
				LanguageCd		= userInfo.PostalCode
			};

			context.UserProfiles.Add(userProfile);
			context.SaveChanges();
		}

		public void SaveUserPhoto(string UserName, string fileName, string strExt, string strSaveFileName)
		{
			var guid = Guid.NewGuid();
			
			AttachFile files = new AttachFile();
			files.FileId = guid.ToString();
			files.FileType = "USER_PROFILE";
			files.OriginFileName = fileName;
			files.FileExtension = strExt;
			files.SaveAsFileName = strSaveFileName;
			files.FilePath = "/Files/Userpic/";
			files.IsDelete = 0;
			files.CreateId = System.Web.HttpContext.Current.User.Identity.GetUserId();
			files.CreateDt = DateTime.Now;

			context.AttachFiles.Add(files);

			var users = context.UserProfiles.Find(UserName);

			var userpro = context.UserProfiles.Where(x => x.UserId == UserName);
			foreach (var item in userpro)
			{
				item.UserPic = guid.ToString();
			}

			context.SaveChanges();
		}
	}
}
