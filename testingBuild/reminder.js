
    /* var dt = new Date();
    document.getElementById('date-time').innerHTML=dt;*/

    function updateClock()
    {
        var dt = new Date();
        document.getElementById('dhour').innerHTML = dt.getHours();
        document.getElementById('dminute').innerHTML = dt.getMinutes();
        document.getElementById('dseconds').innerHTML = dt.getSeconds();
    } 
/*
 function setReminder()
    {
        var Hour = document.getElementById("hour").value;
        var Minute = document.getElementById("minute").value;
        var result = "Your Reminder is at " + Hour + ":" + Minute;

        
        document.getElementById('reminder_shower').textContent = result;

        document.getElementById('reminder').innerHTML = result;
        return;
        
    }
*/

    function testVariable() 
    {
            var rhour = document.getElementById("hour").value;
            var rminute = document.getElementById("minute").value;          
          
            var result = "next reminder is at " + rhour + ":" + rminute ;

            var dt = new Date();
            var chours = dt.getHours();
            var cminutes = dt.getMinutes();
            
            
            if(rhour == chours && rminute == cminutes )
            {
                window.alert("Times up bro");
                document.getElementById('reminder_shower').textContent = "times up"
                return;
            } 
        
            document.getElementById('reminder_shower').textContent = result;
            
            

    }
