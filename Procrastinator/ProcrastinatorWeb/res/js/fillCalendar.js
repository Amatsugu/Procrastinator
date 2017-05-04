var thisMonth;
$(function(){
	//Cache Elements
	var tile = $(".calTile");
	var dateDisplay = $("#datePanel");
	//Calculate Dates
	var today = new Date();
	dateDisplay.text(monthNames[today.getMonth()] + " " + today.getFullYear());
	thisMonth = new Date(today.getFullYear(), today.getMonth(), 1);
	//Ready Calendar
	tile.remove();
	//Add previous month's overlap
	if(thisMonth.getDay() != 0){
		var d = thisMonth.getDay() - 1;
		var m = thisMonth.getMonth() - 1;
		var sd = getMonthLength(m) - d;
		for(var i = sd; i <= getMonthLength(m); i++){
			tile.clone()
			.addClass("calOtherTile")
			.appendTo("#calPanel")
			.children(".day")
			.text(i);
		}	
	}
	//Fill Days
	for(var i = 1; i <= getMonthLength(today.getMonth()); i++){
		
		if(i == today.getDate())
		{
			tile.clone()
			.addClass("calCurrentTile")
			.appendTo("#calPanel")
			.children(".day")
			.text(i);
		}else{
			tile.clone()
			.appendTo("#calPanel")
			.children(".day")
			.text(i);
		}
	}
	
	//Add Next month's overlap
	var monthEnd = new Date(thisMonth.getFullYear(), thisMonth.getMonth(), getMonthLength(thisMonth.getMonth()));
	if(monthEnd.getDay() != 6){
		var sd = 6 - monthEnd.getDay();
		for(var i = 1; i <= sd; i++){
			tile.clone()
			.addClass("calOtherTile")
			.appendTo("#calPanel")
			.children(".day")
			.text(i);
		}	
	}
	
	$(".calTile").on("click", tileClick);

	
	//Fill event list
	var el = $("#sidePanel");
	var e = $("#sidePanel .event");
	if(el.is(":visible")){
		for(var i = 0; i < 50; i++){
			e.clone().appendTo(el);
		}
	}
});


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
	if(!$(e.currentTarget).hasClass("calOtherTile")){
		var targetDay = $(e.currentTarget).children(".day").text();
		var targetDate = new Date(thisMonth.getFullYear(), thisMonth.getMonth(), parseInt(targetDay));
		console.log(targetDate);
		//TODO: Request Date
	}
}