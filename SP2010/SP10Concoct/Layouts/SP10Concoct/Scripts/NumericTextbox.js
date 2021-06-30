//Developed by Saga Panda

$(document).ready(function () {
    ///	<summary>
    ///		1: obj.NumericTextbox({ NoOfDigitsAfterDecimal: 2 });
    ///	</summary>
    $.fn.NumericTextbox = function () {
        var args = arguments[0] || {}; // It's your object of arguments
        var noOfDecimals = args.NoOfDigitsAfterDecimal;
        var allowNegativeNumbers = args.AllowNegativeNumbers;

        this.each(function (i) {
            $('#' + this.id).blur(function () { CheckNumerics(this, noOfDecimals, allowNegativeNumbers); });
            $('#' + this.id).keydown(function (event) {
                var isDecimal = false;
                if (noOfDecimals > 0)
                    isDecimal = true;
                return jsDecimals(this, event, isDecimal, allowNegativeNumbers);
            });

        });
    };

});

function CheckNumerics(txt, decimals, allowNegativeNumbers) {
    var val = txt.value;

    if (txt.value == '') {
        OnErrorHighlight(txt, '');
    }
    else {
        if (decimals <= 0) {
            var re = /^[0-9]+$/;
            if (!re.test(val)) {
                txt.value = '';
                OnErrorHighlight(txt, 'error');
            }
            else {
                OnErrorHighlight(txt, '');
            }
        }
        else {
            var reString = "";
            if (allowNegativeNumbers) {
                reString = "^[+-]?[0-9]{1,9}(?:\.[0-9]{1," + decimals + "})?$";
            }
            else {
                reString = "^[+]?[0-9]{1,9}(?:\.[0-9]{1," + decimals + "})?$";
            }

            var re = new RegExp(reString);

            if (!re.test(val)) {
                txt.value = '';
                OnErrorHighlight(txt, 'error');
            }
            else {
                OnErrorHighlight(txt, '');
            }
        }
    }

}

function jsDecimals(obj, e, isDecimal, isNegative) {

    var evt = (e) ? e : window.event;
    var key = (evt.keyCode) ? evt.keyCode : evt.which;
    if (key != null) {
        key = parseInt(key, 10);

        var ctrlDown = evt.ctrlKey;
        if (ctrlDown && evt.altKey) return true;
            // Check for ctrl+c, v and x
        else if (ctrlDown && key == 67) return true; // c
        else if (ctrlDown && key == 86) return true; // v
        else if (ctrlDown && key == 88) return true;  // x

        if (obj.value.indexOf('-') != 0)
            obj.value = obj.value.replace("-", "");


        if ((key < 48 || key > 57) && (key < 96 || key > 105)) {
            if (!jsIsUserFriendlyChar(obj, key, isDecimal, isNegative)) {
                return false;
            }
        }
        else {
            if (evt.shiftKey) {
                return false;
            }
        }
    }
    return true;
}

// Function to check for user friendly keys  
//------------------------------------------
function jsIsUserFriendlyChar(obj, val, isDecimal, isNegative) {
    // Backspace, Tab, Enter, Insert, and Delete

    if (val == 8 || val == 9 || val == 13 || val == 45 || val == 46) {
        return true;
    }


    // Ctrl, Alt, CapsLock, Home, End, and Arrows  
    if ((val > 16 && val < 21) || (val > 34 && val < 41)) {
        return true;
    }

    if (isDecimal) {

        if (val == 190 || val == 110) {  //Check dot key code should be allowed
            if (obj.value.indexOf('.') > -1) {
                return false; //Check if already a dot is present
            }
            return true;
        }
    }

    if (isNegative) {

        if (obj.value.indexOf('-') > -1) {

            return false; //Check if already a negative is present
        }
        if (navigator.userAgent.toLowerCase().indexOf('firefox') > -1 && val == 173) {
            return true;
        }
        if (val == 189 || val == 109) {  //Check negative key
            return true;
        }
    }

    // The rest  
    return false;
}


function OnErrorHighlight(obj, flag) {
    var bgColor = '';
    if (flag == 'error') {
        bgColor = '#ffbacf';
    }
    //$(obj).animate({}, 250);
    $(obj).css({ backgroundColor: bgColor });

}
