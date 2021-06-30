function GetItemIDFromQS() {
    var vars = [], hash;
    var hashes = window.location.href.slice(window.location.href.indexOf('?') + 1).split('&');
    for (var i = 0; i < hashes.length; i++) {
        hash = hashes[i].split('=');
        vars.push(hash[0]);
        vars[hash[0]] = hash[1];
    }

    var itemID = vars["ID"].toString();
    return itemID;

}

function showimagepreview(input) {
    if (input.files && input.files[0]) {
        var filerdr = new FileReader();
        filerdr.onload = function (e) {
            $('#imgprvw').attr('src', e.target.result);
        }
        filerdr.readAsDataURL(input.files[0]);
    }
}

//Helper functions
function trim(str) {
    return str.replace(/^\s+|\s+$/g, "");
}