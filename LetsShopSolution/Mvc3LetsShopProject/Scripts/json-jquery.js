$(document).ready(function(){ 
	//attach a jQuery live event to the button
	$('#getdata-button').live('click', function(){
		$.getJSON('json-data.php', function(data) {	
			//alert(data); //uncomment this for debug
			//alert (data.item1+" "+data.item2+" "+data.item3); //further debug
			$('#showdata').html("<p>item1="+data.item1+" item2="+data.item2+" item3="+data.item3+"</p>");
		});
	});
});