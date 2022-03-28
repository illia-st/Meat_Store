function func(number) {
    var digit = new String();
    if(number.length == 1){
        digit = number;
    }
    else{
        digit = number[1];
    }
    if (digit == "1") {
        document.getElementById(number).innerHTML = "порція";
        return;
    }
    if (digit > "1" && digit < "5") {
        document.getElementById(number).innerHTML = "порції";
        return;
    }
    document.getElementById(number).innerHTML = "порцій";
}