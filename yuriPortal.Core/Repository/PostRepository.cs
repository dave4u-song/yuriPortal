using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Hosting;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using yuriPortal.Core.Interfaces;
using yuriPortal.Data.Models;
using yuriPortal.Web.ViewModel;

namespace yuriPortal.Core.Repository
{
	public class PostRepository : IPostRepository
	{
		ApplicationDbContext context = new ApplicationDbContext();

		public List<dynamic> GetPostList(string seachText = null, string seachCondition = null)
		{
			var results = (from brd in context.BoardPosts
						   join urs in context.UserProfiles on brd.CreateId equals urs.UserId into bru
						   from uid in bru.DefaultIfEmpty()
						   join type in context.CommCodes on brd.Status equals type.Code_Value into btype
						   from boardtype in btype.DefaultIfEmpty()
						   join att in context.AttachFiles on uid.UserPic equals att.FileId into upic
						   from userpic in upic.DefaultIfEmpty()
						   where boardtype.CodeGroupID == "POST_STATUS" && userpic.FileType == "USER_PROFILE"
						   select new
						   {
							   brd.Board_id,
							   brd.PostId,
							   brd.PostSubject,
							   brd.PostContent,
							   brd.CreateDt,
							   Status = boardtype.Code_Name,
							   UserFullName = uid.FirstName + " " + uid.LastName,
							   userpic = userpic.SaveAsFileName,
							   AttachCount = (from postfile in context.BoardAttachs where postfile.PostId == brd.PostId select postfile).Count(),
						   }).OrderByDescending(w => w.PostId);

			return results.ToList<dynamic>();
		}

		public BoardPost PostDetails(int? id)
		{
			var results = (from brd in context.BoardPosts
						   where brd.PostId == id
						   select new
						   {
							   brd.Board_id,
							   brd.PostId,
							   brd.PostSubject,
							   brd.PostContent,
							   brd.CreateDt,
							   brd.CreateId,
							   brd.UpdateDt,
							   brd.UpdateId
						   }).FirstOrDefault();


			if (results != null)
			{
				var viweModel = new BoardPost
				{
					Board_id = results.Board_id,
					PostId = results.PostId,
					PostSubject = results.PostSubject,
					PostContent = results.PostContent,
					CreateDt = results.CreateDt,
					CreateId = results.CreateId,
					UpdateDt = results.UpdateDt,
					UpdateId = results.UpdateId,
					LikeCount = (from postlike in context.BoardLikes where postlike.PostId == results.PostId && postlike.IsLike == 1 select postlike).Count(),
					DisLikeCount = (from postlike in context.BoardLikes where postlike.PostId == results.PostId && postlike.IsLike == 0 select postlike).Count(),
				};

				return viweModel;
			}
			else{
				return null;
			}
		}

		public List<BoardComment> CommentList(int? id)
		{
			var comments = context.Database.SqlQuery<CommentViewModel>(@"WITH tree (PostId, CommetId, level, ParentId, CommentContent, CreateDt, CreateId, UpdateDt, UpdateId, FullName, UserPic, rn) as 
					(
						SELECT c.PostId, c.CommetId, 1 as level, c.ParentId, c.CommentContent, c.CreateDt, c.CreateId, c.UpdateDt, c.UpdateId, u.FirstName + ' ' + u.LastName As FullName,
								(select SaveAsFileName from AttachFile where fileId = u.UserPic) as UserPic,
								convert(varchar(max),right(1000000000 + row_number() over (order by CommetId desc),10)) rn
						FROM BoardComment c
						inner join UserProfile u
						on	
							c.CreateId = u.UserId
						WHERE postid = @PostId and ParentId = 0

						UNION ALL

						SELECT c2.PostId, c2.CommetId, tree.level + 1, c2.ParentId, c2.CommentContent, c2.CreateDt, c2.CreateId, c2.UpdateDt, c2.UpdateId, 
						u.FirstName + ' ' + u.LastName As FullName, 
						(select SaveAsFileName from AttachFile where fileId = u.UserPic) as UserPic, 
						rn + '/' + convert(varchar(max),right(1000000000 + row_number() over (order by c2.CommetId desc),10))
						FROM BoardComment c2 
						inner join UserProfile u
						on	
							c2.CreateId = u.UserId
						INNER JOIN tree 
						ON 
							tree.CommetId = c2.parentid
					)
					SELECT PostId, CommetId, level as CommentLevel, ParentId, CommentContent, CreateDt, CreateId, UpdateDt, UpdateId, FullName, UserPic
					FROM tree order by RN", new SqlParameter("PostId", id))
							.Select(x => new BoardComment()
							{
								CommetId = x.CommetId,
								PostId = x.PostId,
								CommentLevel = x.CommentLevel,
								ParentId = x.ParentId,
								CommentContent = x.CommentContent,
								CreateDt = x.CreateDt,
								CreateId = x.CreateId,
								UpdateDt = x.UpdateDt,
								UpdateId = x.UpdateId,
								FullName = x.FullName,
								UserPic = x.UserPic
							}).ToList();

			return comments;

		}

		public List<SelectListItem> GetBoardList(){

			List<SelectListItem> groupList = new List<SelectListItem>();

			groupList = context.Boards.OrderBy(x => x.Board_name).Select(BoardList => new SelectListItem { Value = BoardList.Board_id.ToString(), Text = BoardList.Board_name }).ToList();

			var groupTop = new SelectListItem()
			{
				Value = "0",
				Text = "--- select ---"
			};

			groupList.Insert(0, groupTop);

			return groupList;
		}

		public BoardPost ViewPost(int? postId){

			var results = (from brd in context.BoardPosts
						   where brd.PostId == postId
						   select new
						   {
							   brd.Board_id,
							   brd.PostId,
							   brd.PostSubject,
							   brd.PostContent,
							   brd.Status,
							   brd.CreateDt,
							   brd.CreateId,
							   brd.UpdateDt,
							   brd.UpdateId
						   }).FirstOrDefault();


			var viweModel = new BoardPost
			{
				Board_id = results.Board_id,
				PostId = results.PostId,
				PostSubject = results.PostSubject,
				PostContent = results.PostContent,
				Status = results.Status,
				CreateDt = results.CreateDt,
				CreateId = results.CreateId,
				UpdateDt = results.UpdateDt,
				UpdateId = results.UpdateId
			};

			return viweModel;
		}

		public void SavePost(BoardPost postInfo, string[] PostAttachs){
			if (string.IsNullOrEmpty(postInfo.PostId.ToString()) || postInfo.PostId == 0)
			{
				postInfo.Status = "0001";
				postInfo.UpdateDt = DateTime.Now;
				postInfo.CreateDt = DateTime.Now;
				postInfo.CreateId = System.Web.HttpContext.Current.User.Identity.GetUserId();
				postInfo.UpdateId = System.Web.HttpContext.Current.User.Identity.GetUserId();

				context.BoardPosts.Add(postInfo);
				context.SaveChanges();

			}
			else
			{
				var updateRow = (from bpost in context.BoardPosts
								 where bpost.PostId == postInfo.Board_id
								 select bpost).Single();

				updateRow.PostSubject = postInfo.PostSubject;
				updateRow.PostContent = postInfo.PostContent;
				updateRow.UpdateDt = DateTime.Now;
				updateRow.UpdateId = System.Web.HttpContext.Current.User.Identity.GetUserId();

				var deletRow = (from bAttach in context.BoardAttachs
								where bAttach.PostId == postInfo.PostId
								select bAttach).FirstOrDefault();

				context.BoardAttachs.Remove(deletRow);
			}

			//foreach (var item in postInfo.PostAttach)
			for (int i = 0; i < postInfo.PostAttach.Length; i++)
			{
				var boardAttach = new BoardAttach();
				boardAttach.PostId = postInfo.PostId;
				//boardAttach.FileId = postInfo.PostAttach[i];

				JavaScriptSerializer jsonSerializer = new JavaScriptSerializer();
				dynamic dobj = jsonSerializer.Deserialize<dynamic>(postInfo.PostAttach[i]);

				//string obj = JsonConvert.DeserializeObject<string>(postInfo.PostAttach[i].ToString());
				boardAttach.FileId = dobj[0].ToString();

				context.BoardAttachs.Add(boardAttach);
				context.SaveChanges();
			}
		}

		public int SaveComment(BoardComment commntInfo)
		{
			if (string.IsNullOrEmpty(commntInfo.CommetId.ToString()) || commntInfo.CommetId == 0)
			{
				commntInfo.UpdateDt = DateTime.Now;
				commntInfo.CreateDt = DateTime.Now;
				commntInfo.CreateId = System.Web.HttpContext.Current.User.Identity.GetUserId();
				commntInfo.UpdateId = System.Web.HttpContext.Current.User.Identity.GetUserId();

				context.BoardComments.Add(commntInfo);
				context.SaveChanges();
			}
			else
			{
				var updateRow = (from bcomm in context.BoardComments
								 where bcomm.CommetId == commntInfo.CommetId
								 select bcomm).Single();

				updateRow.CommentContent = commntInfo.CommentContent;
				updateRow.UpdateDt = DateTime.Now;
				updateRow.UpdateId = System.Web.HttpContext.Current.User.Identity.GetUserId();
			}

			return commntInfo.CommetId; ;
		}


		public BoardComment ViewComment(int? commentId){
			var results = (from brd in context.BoardComments
						   join urs in context.UserProfiles on brd.CreateId equals urs.UserId into bru
						   from uid in bru.DefaultIfEmpty()
						   join att in context.AttachFiles on uid.UserPic equals att.FileId into upic
						   from userpic in upic.DefaultIfEmpty()
						   where brd.CommetId == commentId && userpic.FileType == "USER_PROFILE"
						   select new
						   {
							   brd.PostId,
							   brd.CommetId,
							   brd.CommentContent,
							   brd.CommentLevel,
							   brd.ParentId,
							   brd.CreateDt,
							   brd.CreateId,
							   brd.UpdateDt,
							   brd.UpdateId,
							   FullName = uid.FirstName + " " + uid.LastName,
							   userpic = userpic.SaveAsFileName
						   }).FirstOrDefault();


			var viweModel = new BoardComment
			{
				CommetId = results.CommetId,
				PostId = results.PostId,
				CommentLevel = results.CommentLevel,
				ParentId = results.ParentId,
				CommentContent = results.CommentContent,
				CreateDt = results.CreateDt,
				CreateId = results.CreateId,
				UpdateDt = results.UpdateDt,
				UpdateId = results.UpdateId,
				FullName = results.FullName,
				UserPic = results.userpic
			};

			return viweModel;
		}

		public string[] SaveAttachFiles(HttpRequestBase requstWeb){
			string OriginFileName = "";
			string strSaveFileName = "";
			string strExt = "";

			int arrSeq = 0;

			//var uploadIds;
			PostFileViewModels postFile = new PostFileViewModels();
			string[] uploadIds = new string[] { };

			try
			{
				foreach (string fileName in requstWeb.Files)
				{
					uploadIds = new string[requstWeb.Files.Count];

					HttpPostedFileBase file = requstWeb.Files[fileName];

					OriginFileName = file.FileName;

					if (file != null && file.ContentLength > 0)
					{

						var saveDir = new DirectoryInfo(string.Format("{0}Files", HostingEnvironment.MapPath(@"\")));

						string pathString = System.IO.Path.Combine(saveDir.ToString(), "PostAttach");

						bool isExists = System.IO.Directory.Exists(pathString);

						if (!isExists)
							System.IO.Directory.CreateDirectory(pathString);


						strExt = Path.GetExtension(OriginFileName);

						var nowDt = DateTime.Now;
						string strNow = nowDt.ToString("yyyyMMddHHmm");

						Random rnd = new Random();
						int fileSeq = rnd.Next(999999);

						strSaveFileName = strNow + "_" + fileSeq.ToString() + strExt;
						var path = string.Format("{0}\\{1}", pathString, strSaveFileName);

						file.SaveAs(path);

						var guid = Guid.NewGuid();

						postFile.FiledId = guid.ToString();
						postFile.PostId = 0;

						//uploadIds.Add(guid.ToString());

						uploadIds[arrSeq] = guid.ToString();

						arrSeq++;

						AttachFile files = new AttachFile();
						files.FileId = guid.ToString();
						files.FileType = "POST_ATTACH";
						files.OriginFileName = OriginFileName;
						files.FileExtension = strExt.Substring(1);
						files.SaveAsFileName = strSaveFileName;
						files.FilePath = "/Files/PostAttach/";
						files.IsDelete = 0;
						files.CreateId = System.Web.HttpContext.Current.User.Identity.GetUserId();
						files.CreateDt = DateTime.Now;

						context.AttachFiles.Add(files);
						context.SaveChanges();


					}
				}

				return uploadIds;

			}
			catch (Exception ex)
			{
				return null;
			}

		}



	}
}
