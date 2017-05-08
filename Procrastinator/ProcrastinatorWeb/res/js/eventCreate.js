var startTime;
var endTime;
var allDay;
function ReadyCreateForm()
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
	$("input").on("propertychange change keyup input paste", validate);
	$("select").on("propertychange change keyup input paste", validate);

	var form = $("form[name=event]");
	form.submit(function(e){
		e.preventDefault();
		console.log("submit");
		validate();
		if($(".invalid").length > 0)
		{
			return;
		}
		var startDate = new Date(Date.parse($("input[name=startDate]").val() + " " + $("input[name=startTime]").val()));
		var endDate = new Date(Date.parse($("input[name=endDate]").val() + " " + $("input[name=endTime]").val()));
		var data = {
			Name: $("input[name=eventName]").val(),
			Date: startDate,
			EndDate: endDate,
			AllDay: allDay.prop('checked'),
			Style: $("select[name=eventStyle]").val(),
			Description: $("textarea[name=description]").val()
		};
		console.log(data);
		CreateEvent(data, function(d)
		{
			console.log(d);
			if(d.error)
			{

			}else
			{
				closeWindow();
				populateSidePanel(selectedDay);
			}
		});
	});
}


function validate()
{
	//Validate
	var name = $("input[name=eventName]");
	if(checkIfEmpty(name.val()))
	{
		name.addClass("invalid");
	}else
		name.removeClass("invalid");
	var style = $("select[name=eventStyle]");
	if(checkIfEmpty(style.val()))
	{
		style.addClass("invalid");
	}else
	{
		style.removeClass("invalid");
	}
}

function checkIfEmpty(val)
{
	return (val == "" || val == null || val == undefined);
}