//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;


//	class JsTreeNode
//	{
//	}
//}


using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace yuriPortal.Helper
{
	public class JsTreeNode
	{
		public JsTreeAttribute attr;
		public JsTreeNode[] childrens;
		public string text
		{
			get;
			set;
		}
		public string id
		{
			get;
			set;
		}
		public string icons
		{
			get;
			set;
		}
		public string state
		{
			get;
			set;
		}
		public string type
		{
			get;
			set;
		}
		public string path
		{
			get;
			set;
		}
		public Boolean children
		{
			get;
			set;
		}
		
	}

	public class JsTreeAttribute
	{
		public string id;
		public bool selected;
	}
}