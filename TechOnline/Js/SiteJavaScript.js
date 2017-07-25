

// Validate Customer Registration Input
function validateInput()
{
    var v = document.getElementById("txtFirstName").value();
    
    try
    {
        new Date(  211, 22, 22);

    } catch (ex)
    {
    
    }

    alert(v);
}


function searchItem(event) {
    var keycode = (event.keyCode ? event.keyCode : event.which);
    if (keycode == '13') {
        window.location.href = "Login";
    }
}

function clickMe() {
    window.location.href = "Search";
    alert("My Day ");
}


