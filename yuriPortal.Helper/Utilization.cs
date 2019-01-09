using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace yuriPortal.Helper
{
	public class Utilization
	{
		public void FileWebUpload(){
			;
		}

		/// <summary>
		/// File Web Download
		/// </summary>
		/// <param name="file_name"></param>
		/// <param name="url"></param>
		public void FileWebDownload(string file_name, string url)
		{
			//string url = "http://4rapiddev.com/wp-includes/images/logo.jpg";
			//string file_name = Server.MapPath(".") + "\\logo.jpg";

			//save_file_from_url(file_name, url);

			//Response.Write("The file has been saved at: " + file_name);

			byte[] content;
			HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
			WebResponse response = request.GetResponse();

			Stream stream = response.GetResponseStream();

			using (BinaryReader br = new BinaryReader(stream))
			{
				content = br.ReadBytes(500000);
				br.Close();
			}
			response.Close();

			FileStream fs = new FileStream(file_name, FileMode.Create);
			BinaryWriter bw = new BinaryWriter(fs);
			try
			{
				bw.Write(content);
			}
			finally
			{
				fs.Close();
				bw.Close();
			}
		}

		public static List<SelectListItem> addTopEmptyValue(List<SelectListItem> selectlist)
		{

			var top = new SelectListItem()
			{
				Value = "0",
				Text = "--- select ---"
			};

			selectlist.Insert(0, top);

			return selectlist;
		}

		public static string CheckNullValue(string val){

			if (string.IsNullOrEmpty(val))
				return "";
			else
				return val;
		}

	}
}
