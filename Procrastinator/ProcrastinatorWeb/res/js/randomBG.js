$(function()
{
	var bg = Math.floor(Math.random() * 5);
	$("#background").css("background-image"," url(/res/img/bg/BG"+ bg +".jpg)");
	$("#blurBg").css("background-image"," url(/res/img/bg/BG"+ bg +"Blur.jpg)");
});