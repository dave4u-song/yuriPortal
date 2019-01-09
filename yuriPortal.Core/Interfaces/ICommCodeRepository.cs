using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using yuriPortal.Data.Models;

namespace yuriPortal.Core.Interfaces
{
	public interface ICommCodeRepository
	{
		void SaveCode(CommCode codes, string userId);

		List<dynamic> GetCommCodes(string selCogdGroup = null);
	}
}
