using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using yuriPortal.Data.Models;

namespace yuriPortal.Core.Interfaces
{
	public interface ILangRepository
	{
		List<dynamic> GetLangList(string langCode = null, string seachText = null);

		Language ViewContent(string LangId);

		void SaveLang(Language langs);

		void UpdateLang(Language langs);
	}
}
