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
	public interface ICodeGroupRepository
	{
		void Add(CodeGroup p);
		void Edit(CodeGroup p);
		void Remove(string Id);
		void CreateGroup(CodeGroup groups);
		void UpdateCodeStatus(string groupVal, string codeVal, string action);

		CodeGroup FindById(string Id);
		List<SelectListItem> GetCodeGroups(); 
		
	}
} 
