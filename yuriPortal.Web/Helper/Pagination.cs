using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace yuriPortal.Web.Helper
{
	public class PagedData<T> where T : class
	{
		public IEnumerable<T> Data { get; set; }
		public int TotalCount { get; set; }
		public int TotalPages { get; set; }
		public int CurrentPage { get; set; }
		public int PageSize { get; set; }
	}

	public static class Pagination
	{
		public static PagedData<T> PagedResult<T>(this List<T> list, int PageNumber, int PageSize) where T : class
		{
			var result = new PagedData<T>();

			result.Data = list.Skip(PageSize * (PageNumber - 1)).Take(PageSize).ToList();
			result.TotalCount = Convert.ToInt32(Math.Ceiling((double)list.Count()));
			result.TotalPages = Convert.ToInt32(Math.Ceiling((double)list.Count() / PageSize));
			result.CurrentPage = PageNumber;
			result.PageSize = PageSize;

			return result;
		}

		public static string GetPagingHtml(int totalCount, int PageNumber, int PageSize)
		{
			StringBuilder sbReturn = new StringBuilder();

			int totalPages = Convert.ToInt32(Math.Ceiling((double)totalCount / PageSize));
			sbReturn.AppendFormat("<ul class=\"pagination\">");
			
			sbReturn.AppendFormat("<li class=\"first disabled\">");
			sbReturn.AppendFormat("<a href = \"#\"><i class=\"fa fa-angle-double-left\"></i></a>");
			sbReturn.AppendFormat("</li>");

			sbReturn.AppendFormat("<li class=\"prev disabled\">");
			sbReturn.AppendFormat("<a href = \"#\"><i class=\"fa fa-angle-left\"></i></a>");
			sbReturn.AppendFormat("</li>");



			for (int i = 1; i <= totalPages; i++) {
				
				if (i == PageNumber)
					sbReturn.AppendFormat("<li class=\"active\"><a href = \"#\"> {0} </a></li>", i.ToString());
				else
					sbReturn.AppendFormat("<li><a href = \"#\"> {0} </a></li>", i.ToString());
			}

			sbReturn.AppendFormat("<li class=\"next\">");
			sbReturn.AppendFormat("<a href = \"#\"><i class=\"fa fa-angle-double-right\"></i></a>");
			sbReturn.AppendFormat("</li>");

			sbReturn.AppendFormat("<li class=\"last\">");
			sbReturn.AppendFormat("<a href = \"#\"><i class=\"fa fa-angle-right\"></i></a>");
			sbReturn.AppendFormat("</li>");

			sbReturn.AppendFormat("</ul>");
			return sbReturn.ToString();
		}
	}


}