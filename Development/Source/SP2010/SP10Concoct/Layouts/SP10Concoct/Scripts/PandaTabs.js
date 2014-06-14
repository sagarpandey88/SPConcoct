/****************************************/
/*			PANDA TABS					*/
/****************************************/

//Load Time Functions
$(document).ready(function () {
    //Hide all the tabs at load time
    $('div[isTab=true]').toggle();

    //Attach on click listner to all the anchor elements with isTabSwitch=true attribute
    $('a[isTabSwitch=true]').click(function () {
        ToggleTab(this);

    });
});




function ToggleTab(tabSwitch) {
    $('div[isTab=true]').hide(500);
    $('#' + tabSwitch.value).toggle(500);
}