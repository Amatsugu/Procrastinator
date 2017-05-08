var error;
var form;
var user;
var pass;
var pass2;
$(document).ready(function(){
	error = $("#formError");
	user = $("input[name = username]").on("propertychange change keyup input paste", function(e){
		e.preventDefault();
		console.log(validateUsername(user.val()));
	});
	pass = $("input[name = password]").on("propertychange change keyup input paste", function(e){
		e.preventDefault();
		if(validatePassword(pass.val()))
			pass.removeClass("invalid");
		else
			pass.addClass("invalid");
	});
	pass2 = $("input[name = password2]").on("propertychange change keyup input paste", function(e){
		e.preventDefault();
		if((pass.val() == pass2.val()) && validatePassword(pass2.val()))
			pass2.removeClass("invalid");
		else
			pass2.addClass("invalid");
	});
	form = $("form").submit(function(e){
		e.preventDefault();
		user.trigger("change");
		pass.trigger("change");
		pass2.trigger("change");
		if($(".invalid").length > 0){
			error.text("Registration information invalid");
			error.fadeIn();
			return;
		}
		$.ajax({
			url:"/auth/register",
			type:"POST",
			data: form.serialize(),
			success:function(r){
				location.assign("/");
			},
			error:function(e)
			{
				error.text("Something went wrong during registration");
				error.fadeIn();
			}
		});
	});
});

function validatePassword(password)
{
	return password.length >= 8;
}


function validateUsername(username)
{
	if(username.length < 3){
		user.addClass("invalidInput");
		return;
	}
	$.ajax({
		url:"/auth/checkuser/",
		type:"POST",
		async:true,
		data: username,
		success:function(e)
		{
			user.removeClass("invalid");
		},
		error:function(xhr, s, e)
		{
			user.addClass("invalid");
		}
	});
}