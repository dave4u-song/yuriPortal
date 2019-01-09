using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;

namespace yuriPortal.Web.Helper
{
	public class Utils
	{
		public static string SetDateTimeFormat(DateTime dateTime) {

			CultureInfo provider = CultureInfo.InvariantCulture;
			string format = "MM/dd/yyyy HH:mm";


			return DateTime.ParseExact(Convert.ToString(dateTime), format, provider).ToString();
		}
	}
}