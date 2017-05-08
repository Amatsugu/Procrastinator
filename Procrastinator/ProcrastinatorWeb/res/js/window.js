var win;
var content;
var title;
var blackout;
$(function()
{
	content = $("#windowContent");
	title = $("#window #title");
	win = $("#windowContainer");
	blackout = $("#blackout");
	blackout.click(closeWindow);
	$("#window #closeButton").click(closeWindow);
});

function OpenWindow(data, titleText)
{
	content.html(data);
	title.text(titleText);
	win.fadeIn();
	blackout.fadeIn();
}

function closeWindow(e)
{
	win.fadeOut();
	blackout.fadeOut();
}