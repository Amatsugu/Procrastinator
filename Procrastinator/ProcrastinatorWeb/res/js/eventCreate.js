var startTime;
var endTime;
var allDay;
$(function()
{
	startTime = $("input[name=startTime]");
	endTime = $("input[name=endTime]");
	allDay = $("input[name=allDay]");
	allDay.click(function(e)
	{
		console.log(allDay.prop('checked'));
		if(allDay.prop('checked'))
		{
			startTime.fadeOut();
			endTime.fadeOut();
		}else
		{
			startTime.fadeIn();
			endTime.fadeIn();
		}
	});
})