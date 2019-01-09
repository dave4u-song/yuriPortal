using System;
using System.Web;
using System.Web.Mvc;
using System.Web.WebPages;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace yuriPortal.Helper
{
    public class MvcHtmlHelper
    {
		public static System.Web.Mvc.HtmlHelper Html
		{
			get { return ((System.Web.Mvc.WebViewPage)WebPageContext.Current.Page).Html; }
		}

		public static System.Web.Mvc.AjaxHelper Ajax
		{
			get { return ((System.Web.Mvc.WebViewPage)WebPageContext.Current.Page).Ajax; }
		}

		public static System.Web.Mvc.UrlHelper Url
		{
			get { return ((System.Web.Mvc.WebViewPage)WebPageContext.Current.Page).Url; }
		}
	}
}
