using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using yuriPortal.Data.Models;

namespace yuriPortal.Core.Interfaces
{
	public interface IBoardRepository
	{
		List<dynamic> GetBoardList(string searchText = null);

		void SaveBoard(Board boards);

		void UpdateBoard(Board boards);

		Board ViewContent(int? boardId);
	}
}
