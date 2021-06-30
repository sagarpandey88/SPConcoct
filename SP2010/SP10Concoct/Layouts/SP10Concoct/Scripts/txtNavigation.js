function initTextboxNavigation() {

    //Specific code for partial postbacks can go in here.

    $(document).ready(function () {

        var headerChk = $(".chkHeader input");
        var itemChk = $(".chkItem input");

        headerChk.click(function () {
            itemChk.each(function () {
                this.checked = headerChk[0].checked;
                if (this.checked)
                    this.parentNode.parentNode.parentNode.style.backgroundColor = '#C3FDB8';
                else
                    this.parentNode.parentNode.parentNode.style.backgroundColor = '';

            })
        });

        itemChk.each(function () {
            $(this).click(function () {
                if (this.checked == false) { headerChk[0].checked = false; }
            })
        });

    });

    (function ($, undefined) {

        $.fn.getCursorRightPosition = function () {
            var el = $(this).get(0);
            var pos = 0;

            if ('selectionStart' in el) {
                pos = el.value.length - el.selectionStart;
            }
            else if ('selection' in document) {
                el.focus();
                var Sel = document.selection.createRange();
                var SelLength = document.selection.createRange().text.length;
                Sel.moveStart('character', -el.value.length);
                //  pos = Sel.text.length - SelLength;
                pos = el.value.length - Sel.text.length;
            }


            return pos;
        }
    })(jQuery);


    (function ($, undefined) {
        $.fn.getCursorLeftPosition = function () {
            var el = $(this).get(0);
            var pos = 0;
            if ('selectionStart' in el) {
                pos = el.selectionStart;
            } else if ('selection' in document) {
                el.focus();
                var Sel = document.selection.createRange();
                var SelLength = document.selection.createRange().text.length;
                Sel.moveStart('character', -el.value.length);
                //  pos = Sel.text.length - SelLength;
                pos = Sel.text.length;
            }
            return pos;
        }
    })(jQuery);






    $(document).ready(function () {

        $('input').keydown(function (e) {
            if (e.which == 39)//right
            {
                if ($(this).getCursorRightPosition() == 0)
                    $(this).closest('td').next().find('input').focus();
            }
            else if (e.which == 37)  //left
            {
                if ($(this).getCursorLeftPosition() == 0)
                    $(this).closest('td').prev().find('input').focus();

            }
            else if (e.which == 40)  //down

                $(this).closest('tr').next().find('td:eq(' + $(this).closest('td').index() + ')').find('input').focus();
            else if (e.which == 38) //up
                $(this).closest('tr').prev().find('td:eq(' + $(this).closest('td').index() + ')').find('input').focus();
        });
    });




}