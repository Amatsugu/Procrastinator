function GetEventsFrom(date, callback)
{
	if(date.day)
		SendApiGetRequest("/api/event/" + date.year + "/" + date.month + "/" + date.day, callback);
	else
		SendApiGetRequest("/api/event/" + date.year + "/" + date.month, callback);
}

function GetEvent(id, callback)
{
	SendApiGetRequest("/api/event/" + id, callback);
}

function CreateEvent(event, callback)
{
	SendApiPostRequest("/api/event", event, callback);
}

function GetSticker(id, callback)
{
	SendApiGetRequest("/api/sticker/" + id, callback);
}

function GetUser(id, callback)
{
	SendApiGetRequest("/api/user/" + id, callback);
}


function SendApiGetRequest(url, callback){
	var output = {
		"error": false,
		"errorMessage":"",
		"data": {}
	}
	$.ajax({
		url: url,
		method: "GET",
		success: function(data){
			output.data = data; 	
			callback(output);
		},
		error: function(jqXHR, textStatus){
			output.error = true;
			output.errorMessage = jqXHR.statusText;
			callback(output);
		}
	});
}

function SendApiPostRequest(url, data, callback){
	var output = {
		"error": false,
		"errorMessage":"",
		"data": {}
	}
	$.ajax({
		url: url,
		data: JSON.stringify(data),
		method: "POST",
		contentType: 'application/json', 
		success: function(data){
			output.data = data; 	
			callback(output);
		},
		error: function(jqXHR, textStatus){
			output.error = true;
			output.errorMessage = jqXHR.statusText;
			callback(output);
		}
	});
}

function SendApiDeleteRequest(url, data, callback){
	var output = {
		"error": false,
		"errorMessage":"",
		"data": {}
	}
	$.ajax({
		url: url,
		data: data,
		method: "DELETE",
		contentType: 'application/json'
	}).done(function(data){
		output.data = data; 	
		callback(output);
	}).fail(function(jqXHR, textStatus){
		output.error = true;
		output.errorMessage = jqXHR.statusText;
		callback(output);
	});
}