using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Hosting;
using System.Web;

namespace yuriPortal.Helper
{
	public class FileManager
	{
		public string SaveFile(HttpPostedFileBase file, string filePath)
		{
			string strSaveFileName = "";
			if (file.ContentLength > 0)
			{
				var fileName = Path.GetFileName(file.FileName);
				var strExt = Path.GetExtension(fileName);

				var nowDt = DateTime.Now;
				string strNow = nowDt.ToString("yyyyMMddHHmm");

				Random rnd = new Random();
				int fileSeq = rnd.Next(999999);

				strSaveFileName = strNow + "_" + fileSeq.ToString() + "." + strExt;

				var path = Path.Combine(HostingEnvironment.MapPath(filePath), strSaveFileName);

				file.SaveAs(path);
			}

			return strSaveFileName;
		}

	}

}
