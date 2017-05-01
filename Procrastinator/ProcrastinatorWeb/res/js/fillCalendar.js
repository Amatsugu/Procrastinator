$(document).ready(function(){
	//Cache Elements
	var tile = $(".calTile");
	var dateDisplay = $("#datePanel");
	//Calculate Dates
	var today = new Date();
	dateDisplay.text(monthNames[today.getMonth()] + " " + today.getFullYear());
	var thsMonth = new Date(today.getFullYear(), today.getMonth(), 1);
	//Ready Calendar
	tile.remove();
	//Add previous month's overlap
	if(thsMonth.getDay() != 0){
		var d = thsMonth.getDay() - 1;
		var m = thsMonth.getMonth() - 1;
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
	var monthEnd = new Date(thsMonth.getFullYear(), thsMonth.getMonth(), getMonthLength(thsMonth.getMonth()));
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
	"January",
	"Febuary",
	"March",
	"April",
	"May",
	"June",
	"July",
	"August",
	"September",
	"October",
	"November",
	"December"
]
var monthLengths = [
	31,
	28,
	31,
	30,
	31,
	30,
	31,
	31,
	30,
	31,
	30,
	31
]

function getMonthLength(month){
	if(month < 0)
		month += 12;
	if(month > 12)
		month -= 12;
	return monthLengths[month];
}