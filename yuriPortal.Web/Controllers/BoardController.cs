using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using yuriPortal.Web.Helper;
using yuriPortal.Core;
using yuriPortal.Data.Models;
using yuriPortal.Core.Interfaces;

namespace yuriPortal.Web.Controllers
{
    public class BoardController : Controller
    {
		IBoardRepository dbBoard;

		public BoardController() { }

		public BoardController(IBoardRepository dbBoard)
		{
			this.dbBoard = dbBoard;
		}

		// GET: Board
		public ActionResult Index()
        {
			return View();
        }

		public ActionResult GetBoardList(int pageNumber = 1, int pageSize = 15, string seachTest = null){

			try
			{
				List<dynamic> boards = new List<dynamic>();

				boards = dbBoard.GetBoardList(seachTest);

				var pagedData = Pagination.PagedResult(boards, pageNumber, pageSize);
				return Json(pagedData, JsonRequestBehavior.AllowGet);
			}
			catch (Exception ex)
			{
				var error = ex.Message.ToString();
				return Json(new { success = false, SaveError = "Error" }, JsonRequestBehavior.AllowGet);
			}
		}

		public ActionResult CreateBoard()
		{
			ViewBag.UpdateBoard = false;
			return PartialView("CreateBoard");
		}

		// POST: Code/Create
		[HttpPost]
		public ActionResult SaveBoard(Board boards)
		{
			try
			{
				// TODO: Add insert logic here
				if (ModelState.IsValid)
				{
					dbBoard.SaveBoard(boards);
					return Json(new { success = true, responseText = "Board is successfuly saved!" }, JsonRequestBehavior.AllowGet);

				}
				else
				{

					return Json(new { success = false, responseText = "Board is not saved." }, JsonRequestBehavior.AllowGet);
				}
			}
			catch (Exception ex)
			{
				var error = ex.Message.ToString();
				return Content(error);
			}
		}

		// POST: Code/Create
		[HttpPost]
		public ActionResult UpdateBoard(Board boards)
		{
			try
			{
				// TODO: Add insert logic here
				if (ModelState.IsValid)
				{
					dbBoard.UpdateBoard(boards);

					return Json(new { success = true, responseText = "Board is successfuly saved!" }, JsonRequestBehavior.AllowGet);
				}
				else
				{

					return Json(new { success = false, responseText = "Board is not saved." }, JsonRequestBehavior.AllowGet);
				}
			}
			catch (Exception ex)
			{
				var error = ex.Message.ToString();
				return Json(new { success = false, ErrorMsg = "Unable to save changes. Try again, and if the problem persists, see your system administrator." }, JsonRequestBehavior.AllowGet);
			}
		}

		public ActionResult ViewContent(int? boardId)
		{
			ViewBag.UpdateBoard = true;
			return PartialView("CreateBoard", dbBoard.ViewContent(boardId));
		}


	}
}