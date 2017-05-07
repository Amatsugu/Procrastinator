var thisMonth;
var sidePanel;
var eventElement;
var calPanel;
var today = new Date();
var dateDisplay;
var calTile;
$(function(){
	//Cache Elements
	calTile = $(".calTile");
	dateDisplay = $("#datePanel");
	calPanel = $("#calPanel");
	sidePanel = $("#sidePanel");
	//Ready Calendar
	calTile.remove();
	eventElement = $(".event").remove();
	renderCal(new Date(today.getFullYear(), today.getMonth(), 1));
	calPanel.mousewheel(function(e){
		console.log(e.deltaY);
		if(e.deltaY == 0)
			return;
		var tiles = $(".calTile");
		var hasTriggered = false;
		tiles.fadeOut(500, function(){
			tiles.remove();
			if(hasTriggered)
				return;
			hasTriggered = true;
			var m = thisMonth.getMonth() - e.deltaY;
			var y = thisMonth.getFullYear();
			if(thisMonth.getMonth() - e.deltaY > 12)
				y++;
			var tDate = new Date(y, m, 1);
			renderCal(tDate);
		});
		e.preventDefault();
	});
	populateSidePanel({
		year: today.getFullYear(),
		month: today.getMonth(),
		day: today.getDate()
	});
	$.ajax({
		url:"/res/frame/createEvent.html"
	}).done(function(e){
		OpenWindow(e, "Create Event");
	});
});


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

function tileClick(e)
{
	var t = $(e.currentTarget);
	if(!t.hasClass("calOtherTile")){
		var targetDay = t.children(".day").text();
		var targetMonth = thisMonth.getMonth();
		var targetYear =  thisMonth.getFullYear();
		$(".selectedTile").removeClass("selectedTile");
		t.addClass("selectedTile");
		console.log(targetDay);
		populateSidePanel({
			year: targetYear,
			month: targetMonth,
			day: targetDay
		});
		//TODO: Request Date
	}
}

function populateSidePanel(date)
{
	GetEventsFrom(date, function(e){
		console.log(e);
		if(e.error)
			sidePanel.html("<div class='info'>An Error Occured: " + e.errorMessage + "</div>");
	});
}