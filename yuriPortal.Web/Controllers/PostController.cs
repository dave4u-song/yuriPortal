using Microsoft.AspNet.Identity;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using yuriPortal.Web.Helper;
using yuriPortal.Core;
using yuriPortal.Data.Models;
using yuriPortal.Web.ViewModel;
using yuriPortal.Core.Interfaces;

namespace yuriPortal.Web.Controllers
{
    public class PostController : Controller
    {
		IPostRepository dbPost;

		public PostController() { }

		public PostController(IPostRepository dbPost)
		{
			this.dbPost = dbPost;
		}
		// GET: Post
		public ActionResult Index()
        {

			return View();
        }

		public ActionResult GetPostList(int pageNumber = 1, int pageSize = 15, string seachText = null, string seachCondition = null)
		{
			try
			{
				List<dynamic> posts = new List<dynamic>();

				posts = dbPost.GetPostList(seachText, seachCondition);

				var pagedData = Pagination.PagedResult(posts, pageNumber, pageSize);
				return Json(pagedData, JsonRequestBehavior.AllowGet);
			}
			catch (Exception ex)
			{
				var error = ex.Message.ToString();
				return Json(new { success = false, SaveError = "Error" }, JsonRequestBehavior.AllowGet);
			}
		}


		// GET: Post/Details/5
		public ActionResult Details(int? id)
        {
			ViewBag.CommentList = dbPost.CommentList(id);
			return PartialView("Details", dbPost.PostDetails(id));
		}

		// GET: Post/Create
		public ActionResult Create()
        {
			ViewBag.groupList = dbPost.GetBoardList();

			return View();
        }

        // POST: Post/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

		public ActionResult ViewPost(int? postId)
		{
			ViewBag.UpdateBoard = true;
			return PartialView("CreateBoard", dbPost.ViewPost(postId));
		}

		// POST: Post/Create
		[HttpPost]
		public ActionResult SavePost(BoardPost postInfo, string[] PostAttachs)
		{
			try
			{
				if (ModelState.IsValid)
				{
					dbPost.SavePost(postInfo, PostAttachs);

					return Json(new { success = true }, JsonRequestBehavior.AllowGet);
				}

				return Json(new { success = true }, JsonRequestBehavior.AllowGet);
			}
			catch (Exception ex)
			{
				var error = ex.Message.ToString();
				return Json(new { success = false, SaveError = error }, JsonRequestBehavior.AllowGet);
			}
		}

		// POST: Post/Create
		[HttpPost]
		public ActionResult SaveComment(BoardComment commntInfo)
		{
			if (ModelState.IsValid)
			{
				int commentId = dbPost.SaveComment(commntInfo);

				return Json(dbPost.ViewComment(commentId), JsonRequestBehavior.AllowGet);
			}
			else{

				return Json(new { success = false, SaveError = "Not Saved" }, JsonRequestBehavior.AllowGet);
			}
			
		}

		// POST: Post/Create
		[HttpPost]
		public ActionResult PublishPost(FormCollection collection)
		{
			try
			{
				// TODO: Add insert logic here

				return RedirectToAction("Index");
			}
			catch
			{
				return View();
			}
		}


		// GET: Post/Edit/5
		public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Post/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Post/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Post/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

		public ActionResult SaveAttachFiles()
		{
			HttpRequestBase request = HttpContext.Request;

			string[] uploadIds = dbPost.SaveAttachFiles(request);

			return Json(uploadIds, JsonRequestBehavior.AllowGet);
		}


	}
}
