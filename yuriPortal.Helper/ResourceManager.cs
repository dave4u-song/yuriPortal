using System;
using System.Collections.Generic;
using System.Linq;
using System.Resources;
using System.Text;
using System.Threading.Tasks;
using yuriPortal.Data.Models;

namespace yuriPortal.Helper
{
	public class ResourceManager
	{
		public void CreateReource(List<Language> list, string langCode)
		{
			try
			{
				// Create a instance of ResourceWriter and specify the name of the resource file.
				System.Resources.ResourceWriter RWObj = new ResourceWriter(AppDomain.CurrentDomain.BaseDirectory + "/Resources/LanguageResource." + langCode + ".resx");

				for (int i = 0; i < list.Count; i++)
				{
					RWObj.AddResource(list[i].LangId, list[i].LangTrans);
				}

				RWObj.Close();
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}
		}


	}
}
