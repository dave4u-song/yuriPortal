
function getDatefromJson(value) {
	var pattern = /Date\(([^)]+)\)/;
	var results = pattern.exec(value);
	var dt = new Date(parseFloat(results[1]));

	return (dt.getMonth() + 1) + "/" + dt.getDate() + "/" + dt.getFullYear();
}


function setBoolValue(val) {

	var strText = "False";

	if (val == "1")
		strText = "True";

	return strText;
}

function getNavigationNum(current, { range, pages, start = 1 }) {
	const paging = [];
	var i = Math.min(pages + start - range, Math.max(start, current - (range / 2 | 0)));
	const end = i + range;

	while (i < end) {
		paging.push(i++)
	}

	return paging;
}


function SetPageNavigation(totalPage, currentPage, pagesize) {
	var template = "";
	var TotalPages = totalPage;
	var CurrentPage = currentPage;
	var PageNumberArray = [];

	var pageCount = (TotalPages / pagesize) + (TotalPages % pagesize != 0) ? 1 : 0;
	const pageNums = { range: pageCount, pages: TotalPages };

	PageNumberArray = getNavigationNum(currentPage, pageNums);

	var FirstPage = 1;
	var LastPage = totalPage;
	if (totalPage != currentPage) {
		var ForwardOne = currentPage + 1;
	}
	var BackwardOne = 1;
	if (currentPage > 1) {
		BackwardOne = currentPage - 1;
	}

	template = template + '<ul class="pagination">';

	if (currentPage == 1) {
		template = template + '<li class="first disabled"><a href="#"><i class="fa fa-angle-double-left"></i></a></li>' +
			'<li class="prev disabled"><a href="#"><i class="fa fa-angle-left"></i></a></li>';
	}
	else {
		template = template + '<li class="first"><a href="#" onclick="BindDataToList(' + FirstPage + ')"><i class="fa fa-angle-double-left"></i></a></li>' +
			'<li class="prev"><a href="#" onclick="BindDataToList(' + BackwardOne + ')"><i class="fa fa-angle-left"></i></a></li>';
	}

	for (var i = 0; i < PageNumberArray.length; i++) {

		if (CurrentPage == PageNumberArray[i])
			template = template + '<li class="active"><a href = "#">' + PageNumberArray[i] + '</a></li>';
		else
			template = template + '<li><a onclick="BindDataToList(' + PageNumberArray[i] + ')" href="#">' + PageNumberArray[i] + '</a></li>';
	}

	if (currentPage >= TotalPages) {
		template = template + '<li class="next disabled"><a href="#"><i class="fa fa-angle-right"></i></a></li>';
		template = template + '<li class="last disabled"><a href="#"><i class="fa fa-angle-double-right"></i></a></li></ul>';
	}
	else {
		template = template + '<li class="next"><a href="#" onclick="BindDataToList(' + ForwardOne + ')"><i class="fa fa-angle-right"></i></a></li>';
		template = template + '<li class="last"><a href="#" onclick="BindDataToList(' + LastPage + ')"><i class="fa fa-angle-double-right"></i></a></li></ul>';
	}

	$("#pagingArea").append(template);

}


function OpenPopupWithPage(url) {

	$.magnificPopup.open(
		{
			type: 'ajax',
			closeOnBgClick: false,
			closeBtnInside: false,
			enableEscapeKey: true,
			mainClass: 'mfp-fade',
			items: {
				src: url
			},
			callbacks: {
				open: function () {
					$.magnificPopup.instance.close = function () {
						$.magnificPopup.proto.close.call(this);
					}
				},
				ajaxContentAdded: function () {
					var m = this;
					this.content.find('.modal-dismiss').on('click', function (e) {
						e.preventDefault();
						m.close();
					});
				}
			},
		});
}

var stack_bar_bottom = { "dir1": "up", "dir2": "right", "spacing1": 0, "spacing2": 0 };
function msgNotify(title, msg, msgType) {
	var notice = new PNotify({
		title: title,
		text: msg,
		type: msgType,
		delay: 1000,
		addclass: 'stack-bar-bottom',
		stack: stack_bar_bottom,
		width: "70%",
		desktop: true,
		buttons: {
			closer: true,
			sticker: false
		}
	});
	notice.get().click(function () {
		notice.remove();
	});
}

function successNotify(title, msg) {
	var notice = new PNotify({
		title: title,
		text: msg,
		type: 'success',
		addclass: 'stack-bar-bottom',
		stack: stack_bar_bottom,
		width: "70%"
	});
}

function infoNotify() {
	var notice = new PNotify({
		title: 'Notification',
		text: 'Some notification text.',
		type: 'info',
		addclass: 'stack-bar-bottom',
		stack: stack_bar_bottom,
		width: "70%"
	});
}

function errorNotify() {
	var notice = new PNotify({
		title: 'Notification',
		text: 'Some notification text.',
		type: 'error',
		addclass: 'stack-bar-bottom',
		stack: stack_bar_bottom,
		width: "70%"
	});
}

