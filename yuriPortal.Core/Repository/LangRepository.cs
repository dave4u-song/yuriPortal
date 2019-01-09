using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using yuriPortal.Core.Interfaces;
using yuriPortal.Data.Models;

namespace yuriPortal.Core.Repository
{
	public class LangRepository : ILangRepository
	{
		ApplicationDbContext context = new ApplicationDbContext();

		public List<dynamic> GetLangList(string langCode = null, string seachText = null)
		{
			var results = from lang in context.Languages
						  select new
						  {
							  lang.LangId,
							  lang.LangCode,
							  lang.DefaultText,
							  lang.DefaultTrans,
							  lang.LangTrans
						  };

			if (!string.IsNullOrEmpty(langCode))
			{
				results = results.Where(w => w.LangCode.Contains(langCode));
			}

			if (!string.IsNullOrEmpty(seachText))
			{
				results = results.Where(w => w.DefaultText.Contains(seachText));
			}

			results = results.OrderBy(w => w.LangId);

			return results.ToList<dynamic>();
		}

		public Language ViewContent(string LangId)
		{
			var results = (from brd in context.Languages
						   where brd.LangId == LangId
						   select new
						   {
							   brd.LangId,
							   brd.DefaultText,
							   brd.DefaultTrans,
							   brd.LangCode,
							   brd.LangTrans,
							   brd.CreateDt
						   }).FirstOrDefault();


			var viweModel = new Language
			{
				LangId = results.LangId,
				DefaultText = results.DefaultText,
				DefaultTrans = results.DefaultTrans,
				LangCode = results.LangCode,
				LangTrans = results.LangTrans,
				CreateDt = results.CreateDt
			};

			return viweModel;
		}

		public void SaveLang(Language langs)
		{
			langs.UpdateDt = DateTime.Now;
			langs.CreateDt = DateTime.Now;
			langs.CreateId = System.Web.HttpContext.Current.User.Identity.GetUserId();
			langs.UpdateId = System.Web.HttpContext.Current.User.Identity.GetUserId();

			context.Languages.Add(langs);
			context.SaveChanges();
		}

		public void UpdateLang(Language langs)
		{
			langs.UpdateDt = DateTime.Now;
			langs.UpdateId = System.Web.HttpContext.Current.User.Identity.GetUserId();

			var updateRow = (from brd in context.Languages
							 where brd.LangId == langs.LangId
							 select brd).Single();

			updateRow.DefaultText = langs.DefaultText;
			updateRow.DefaultTrans = langs.DefaultTrans;
			updateRow.LangTrans = langs.LangTrans;
			updateRow.UpdateDt = DateTime.Now;
			updateRow.UpdateId = System.Web.HttpContext.Current.User.Identity.GetUserId();

			context.SaveChanges();
		}

	}
}
