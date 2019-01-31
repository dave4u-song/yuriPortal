using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using yuriPortal.Data.Models;
using yuriPortal.Data.ViewModel;

namespace yuriPortal.Core.Interfaces
{
	public interface ICustomerRepository
	{
		List<CustomerViewModel> GetCustomerList(string seachText = null);

		CustomerViewModel GetCustomerDetail(string customerId, string defaultImagePath);

		string SaveCustomer(Customer custs);

		string DuplicateCheck(string CustomerName);

		void UpdateCustomer(Customer custInfo);

		int SaveNote(CustomerNote noteInfo);

		CustomerNote DetailCustomerNote(int? NoteId);

		void SaveCustomerPhoto(string custId, string fileName, string strExt, string strSaveFileName);

		List<CustomerNote> GetNoteList(string CustomerId);

		List<dynamic> GetActivityList(string seachText = null);
	}
}
