var thisMonth;
var sidePanel;
var eventElement;
var calPanel;
var today = new Date();
var dateDisplay;
var calTile;
var hasTriggered = false;
var calPanelHead;
var sidePanelHead;
var theWindow;
$(function(){
	//Cache Elements
	calTile = $(".calTile");
	dateDisplay = $("#datePanel");
	calPanel = $("#calPanel");
	calPanelHead = $("#calHeader");
	sidePanel = $("#sidePanel");
	sidePanelHead = $("#sidePanelTitle");
	theWindow = $(window);
	//Ready Calendar
	calTile.remove();
	eventElement = $(".event").remove();
	renderCal(new Date(today.getFullYear(), today.getMonth(), 1));
	calPanel.mousewheel(function(e){
		console.log(e.deltaY);
		if(e.deltaY == 0)
			return;
		hasTriggered = false;
		var tiles = $(".calTile");
		tiles.fadeOut(500, function()
		{
			tiles.remove();
			scrollCal(e.deltaY);
		});
		e.preventDefault();
	});
	$("#mainPanel").swipe({
		swipe:function(e, d, di, du, fc, fd)
		{
			console.log(d);
			if(d == "up")
			{
				hasTriggered = false;
				var tiles = $(".calTile");
				tiles.fadeOut(500, function()
				{
					tiles.remove();
					scrollCal(-1);
				});
				e.preventDefault();
			}
			else if(d == "down")
			{
				hasTriggered = false;
				var tiles = $(".calTile");
				tiles.fadeOut(500, function()
				{
					tiles.remove();
					scrollCal(1);
				});
				e.preventDefaul
			}else if(d == "left")
			{	
				switchPanel(2);
			}else if(d == "right")
			{
				switchPanel(1);
			}
		}
	});
	theWindow.resize(function()
	{
		if(theWindow.width() > 1030)
		{
			sidePanel.show();
			sidePanelHead.show();
			calPanel.show();
			calPanelHead.show();
		}/*else
		{
			sidePanel.hide();
			sidePanelHead.hide();
			calPanel.show();
			calPanelHead.show();
		}*/
	});
	populateSidePanel({
		year: today.getFullYear(),
		month: today.getMonth(),
		day: today.getDate()
	});
	
});

function switchPanel(panel)
{
	//console.log($(window).width());
	if(theWindow.width() > 1030)
	{
		return;
	}
	console.log(panel);
	if(panel == 2)
	{
		calPanel.fadeOut();
		calPanelHead.fadeOut(function(){
			sidePanel.fadeIn();
			sidePanelHead.fadeIn();
		});
	}else
	{
		sidePanel.fadeOut();
		sidePanelHead.fadeOut(function(){
			calPanel.fadeIn();
			calPanelHead.fadeIn();
		});
	}
}

function scrollCal(dir)
{
	console.log(hasTriggered);
	if(hasTriggered)
		return;
	hasTriggered = true;
	var m = thisMonth.getMonth() - dir;
	var y = thisMonth.getFullYear();
	if(thisMonth.getMonth() - dir > 12)
		y++;
	var tDate = new Date(y, m, 1);
	renderCal(tDate);
};

function ShowCreateEvent(selectedDate)
{
	$.ajax({
		url:"/res/frame/createEvent.html"
	}).done(function(e){
		OpenWindow(e, "Create Event");
		ReadyCreateForm();
		var startDay = $("input[name=startDate]");
		var endDay = $("input[name=endDate]");
		var startTime = $("input[name=startTime]");
		var endTime = $("input[name=endTime]");
		startDay.val(selectedDate.getFullYear() + "-" + formatNumber(selectedDate.getMonth(), 2) + "-" + formatNumber(selectedDate.getDate(), 2));
		endDay.val(selectedDate.getFullYear() + "-" + formatNumber(selectedDate.getMonth(), 2) + "-" + formatNumber(selectedDate.getDate(), 2));
		var now = new Date();
		var end = now.getHours() + 1;
		if(end >= 24)
		{
			end = 0;	
		}
		startTime.val(formatNumber(now.getHours(), 2) + ":" + formatNumber(now.getMinutes(), 2) + ":" + "00");
		endTime.val(formatNumber(end, 2) + ":" + formatNumber(now.getMinutes(), 2) + ":" + "00");
	});
}

function formatNumber(num, len)
{
	var n = num.toString();
	while(n.length < len)
	{
		n = "0" + n;
	}
	return n;
}


function renderCal(targetMonth){
	//Calculate Dates
	thisMonth = targetMonth;
	dateDisplay.text(monthNames[targetMonth.getMonth()] + " " + targetMonth.getFullYear());
	//Add previous month's overlap
	if(targetMonth.getDay() != 0){
		var d = targetMonth.getDay() - 1;
		var m = targetMonth.getMonth() - 1;
		var sd = getMonthLength(m) - d;
		for(var i = sd; i <= getMonthLength(m); i++){
			calTile.clone()
			.addClass("calOtherTile")
			.appendTo(calPanel)
			.hide()
			.children(".day")
			.text(i);
		}	
	}
	//Fill Days
	for(var i = 1; i <= getMonthLength(targetMonth.getMonth()); i++){
		
		if(i == today.getDate() && targetMonth.getMonth() == today.getMonth())
		{
			calTile.clone()
			.addClass("calCurrentTile")
			.addClass("selectedTile")
			.appendTo(calPanel)
			.hide()
			.children(".day")
			.text(i);
		}else{
			calTile.clone()
			.appendTo(calPanel)
			.hide()
			.children(".day")
			.text(i);
		}
	}
	
	//Add Next month's overlap
	var monthEnd = new Date(targetMonth.getFullYear(), targetMonth.getMonth(), getMonthLength(targetMonth.getMonth()));
	if(monthEnd.getDay() != 6){
		var sd = 6 - monthEnd.getDay();
		for(var i = 1; i <= sd; i++){
			calTile.clone()
			.addClass("calOtherTile")
			.appendTo(calPanel)
			.hide()
			.children(".day")
			.text(i);
		}	
	}
	$(".calTile").on("click", tileClick).fadeIn();
}

var monthNames = [
	"January", "Febuary", "March",
	"April", "May", "June",
	"July", "August", "September",
	"October", "November", "December"
];
var monthLengths = [31,28,31,30,31,30,31,31,30,31,30,31];

function getMonthLength(month){
	if(month < 0)
		month += 12;
	if(month > 12)
		month -= 12;
	return monthLengths[month];
}

var selectedDay;

function tileClick(e)
{
	var t = $(e.currentTarget);
	if(!t.hasClass("calOtherTile")){
		var targetDay = t.children(".day").text();
		var targetMonth = thisMonth.getMonth();
		var targetYear =  thisMonth.getFullYear();
		if(t.hasClass("selectedTile"))
		{
			ShowCreateEvent(new Date(targetYear, targetMonth, targetDay));
		}else
		{
			$(".selectedTile").removeClass("selectedTile");
			t.addClass("selectedTile");
			console.log(targetDay);
			selectedDay = {
				year: targetYear,
				month: targetMonth,
				day: targetDay
			};
			populateSidePanel(selectedDay);
		}
	}
}

function populateSidePanel(date)
{
	selectedDay = date;
	GetEventsFrom(date, function(e)
	{
		if(e.error)
		{
			sidePanel.html("<div class='info'>An Error Occured: " + e.errorMessage + "</div>");
		}
		console.log(e.data);
		if(e.data.length == 0)
		{
			sidePanel.html("<div class='info'>There are no events!</div>");
		}else
		{
			sidePanel.html("");
			for(var i = 0; i < e.data.length; i++)
			{
				var ev = eventElement.clone().appendTo(sidePanel);
				var header = ev.children(".header");
				header.children(".name").text(e.data[i].name);
				var date = new Date(Date.parse(e.data[i].date));
				console.log(date.toTimeString());
				if(e.data[i].allDay)
				{
					header.children(".time").text("All Day");
				}else
				{
					var m = date.getMinutes();
					var o = "AM";
					var h = date.getHours() + 1;
					if(h > 12)
					{
						h -= 12;
						o = "PM";
					}
					header.children(".time").text(h + ":" + m + " " + o);
				}
				ev.children(".body").text(e.data[i].description);
				ev.fadeIn();
			}
		}
	});
}