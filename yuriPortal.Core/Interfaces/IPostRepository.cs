using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using yuriPortal.Data.Models;

namespace yuriPortal.Core.Interfaces
{
	public interface IPostRepository
	{
		List<dynamic> GetPostList(string seachText = null, string seachCondition = null);

		BoardPost PostDetails(int? id);

		List<BoardComment> CommentList(int? id);

		List<SelectListItem> GetBoardList();

		BoardPost ViewPost(int? postId);

		void SavePost(BoardPost postInfo, string[] PostAttachs);

		int SaveComment(BoardComment commntInfo);

		BoardComment ViewComment(int? commentId);

		string[] SaveAttachFiles(HttpRequestBase requstWeb);

	}
}
