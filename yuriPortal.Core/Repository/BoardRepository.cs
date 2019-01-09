using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using yuriPortal.Core.Interfaces;
using yuriPortal.Data.Models;

namespace yuriPortal.Core.Repository
{
	public class BoardRepository : IBoardRepository
	{
		ApplicationDbContext context = new ApplicationDbContext();

		public List<dynamic> GetBoardList(string searchText = null)
		{
			var results = (from brd in context.Boards
						   join urs in context.Users on brd.CreateId equals urs.Id into bru
						   from uid in bru.DefaultIfEmpty()
						   join type in context.CommCodes on brd.DefaultUI equals type.Code_Value into btype
						   from boardtype in btype.DefaultIfEmpty()
						   where boardtype.CodeGroupID == "BOARD_TYPE"
						   select new
						   {
							   brd.Board_id,
							   brd.Board_name,
							   brd.Description,
							   brd.IsAttach,
							   brd.IsComment,
							   brd.IsLike,
							   brd.IsDelete,
							   brd.CreateDt,
							   DefaultUI = boardtype.Code_Name,
							   UserName = uid.UserName
						   }).OrderBy(w => w.Board_id);

			return results.ToList<dynamic>();
		}

		public void SaveBoard(Board boards)
		{
			boards.UpdateDt = DateTime.Now;
			boards.CreateDt = DateTime.Now;
			boards.CreateId = System.Web.HttpContext.Current.User.Identity.GetUserId();
			boards.UpdateId = System.Web.HttpContext.Current.User.Identity.GetUserId();

			context.Boards.Add(boards);
			context.SaveChanges();
		}

		public void UpdateBoard(Board boards)
		{
			boards.UpdateDt = DateTime.Now;
			boards.UpdateId = System.Web.HttpContext.Current.User.Identity.GetUserId();

			var updateRow = (from brd in context.Boards
							 where brd.Board_id == boards.Board_id
							 select brd).Single();
			updateRow.Board_name = boards.Board_name;
			updateRow.Description = boards.Description;
			updateRow.IsAttach = boards.IsAttach;
			updateRow.IsComment = boards.IsComment;
			updateRow.IsLike = boards.IsLike;
			updateRow.DefaultUI = boards.DefaultUI;
			updateRow.UpdateDt = DateTime.Now;
			updateRow.UpdateId = System.Web.HttpContext.Current.User.Identity.GetUserId();
			context.SaveChanges();
		}

		public Board ViewContent(int? boardId){

			var results = (from brd in context.Boards
						   where brd.Board_id == boardId
						   select new
						   {
							   brd.Board_id,
							   brd.Board_name,
							   brd.Description,
							   brd.IsAttach,
							   brd.IsComment,
							   brd.IsLike,
							   brd.IsDelete,
							   brd.DefaultUI,
							   brd.CreateDt,
							   brd.CreateId,
							   brd.UpdateDt,
							   brd.UpdateId
						   }).FirstOrDefault();


			var viweModel = new Board
			{
				Board_id = results.Board_id,
				Board_name = results.Board_name,
				IsAttach = results.IsAttach,
				IsComment = results.IsComment,
				IsLike = results.IsLike,
				IsDelete = results.IsDelete,
				Description = results.Description,
				DefaultUI = results.DefaultUI,
				CreateDt = results.CreateDt,
				CreateId = results.CreateId,
				UpdateDt = results.UpdateDt,
				UpdateId = results.UpdateId
			};


			return viweModel;
		}
	}
}
