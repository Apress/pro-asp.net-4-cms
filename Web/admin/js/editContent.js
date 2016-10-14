var isDirty = false;
var panelSpeed = 160;

$(document).ready(function () {

    // Hides the buckets by default
    if ($("#chkHeader").attr("checked") == false) {
        $("#pnlHeaderEmbeddables").hide();
    }

    if ($("#chkPrimaryNav").attr("checked") == false) {
        $("#pnlPrimaryNavEmbeddables").hide();
    }

    if ($("#chkContent").attr("checked") == false) {
        $("#pnlContentEmbeddables").hide();
    }

    if ($("#chkSubNav").attr("checked") == false) {
        $("#pnlSubNavEmbeddables").hide();
    }

    if ($("#chkFooter").attr("checked") == false) {
        $("#pnlFooterEmbeddables").hide();
    }

    // Marks the page as "dirty" and requiring of user notification before leaving
    $('input[type=text]').keydown(function () {
        isDirty = true;
    });

    $('input[type=submit]').click(function () {
        isDirty = false;
    });

    $('#lnkAddEmbeddable').click(function () {
        isDirty = false;
    });

    // Triggers alerts when users disable potentially valuable features of a page
    $(".chkNoIndex input[type=checkbox]").click(function () {
        var stateOfController = $(".chkNoIndex input[type=checkbox]").attr("checked");

        if (stateOfController == false) {
            return confirm('If you mark a page as invisible, search engines will not be permitted to index it.\r\n\r\nAre you sure you want to keep this page from being indexed?\r\n\r\nClick OK to mark the page as invisible, or click Cancel to leave it visible.');
        }
    });

    $(".chkNoFollow input[type=checkbox]").click(function () {
        var stateOfController = $(".chkNoFollow input[type=checkbox]").attr("checked");

        if (stateOfController == false) {
            return confirm('If you indicate links should not be followed, search engines will not be permitted to index links found within your page.\r\n\r\nAre you sure you want to keep the links on this page from being indexed?\r\n\r\nClick OK to restrict the links from being indexed, or click Cancel to permit indexing.');
        }
    });

    // Triggers alerts for publication features
    $(".promoteVersion").click(function () {
        return confirm('Are you sure you want to use this version of the content?\r\n\r\nThe CMS will automatically save your current version, so you can undo this action at any time if necessary.');
    });

    $(".deleteVersion").click(function () {
        return confirm('Are you sure you want to delete this version of the content?\r\n\r\nThe CMS will mark this content for deletion in 7 days; you can retrieve it within that window if necessary.');
    });

    // Animates the bucket contents on the editor depending on whether they are selected
    $("#chkHeader").click(function () {
        var stateOfController = $("#chkHeader").attr("checked");

        if (stateOfController == false) {
            $("#pnlHeaderEmbeddables").hide(panelSpeed);
        }
        else {
            $("#pnlHeaderEmbeddables").show(panelSpeed);
        }
    });

    $("#chkPrimaryNav").click(function () {
        var stateOfController = $("#chkPrimaryNav").attr("checked");

        if (stateOfController == false) {
            $("#pnlPrimaryNavEmbeddables").hide(panelSpeed);
        }
        else {
            $("#pnlPrimaryNavEmbeddables").show(panelSpeed);
        }
    });

    $("#chkContent").click(function () {
        var stateOfController = $("#chkContent").attr("checked");

        if (stateOfController == false) {
            $("#pnlContentEmbeddables").hide(panelSpeed);
        }
        else {
            $("#pnlContentEmbeddables").show(panelSpeed);
        }
    });

    $("#chkSubNav").click(function () {
        var stateOfController = $("#chkSubNav").attr("checked");

        if (stateOfController == false) {
            $("#pnlSubNavEmbeddables").hide(panelSpeed);
        }
        else {
            $("#pnlSubNavEmbeddables").show(panelSpeed);
        }
    });

    $("#chkFooter").click(function () {
        var stateOfController = $("#chkFooter").attr("checked");

        if (stateOfController == false) {
            $("#pnlFooterEmbeddables").hide(panelSpeed);
        }
        else {
            $("#pnlFooterEmbeddables").show(panelSpeed);
        }
    });

    // Displays a message to the user before abandoning a "dirty" page
    window.onbeforeunload = (function () {
        if (isDirty) {
            return ('Page contents have been modified; leaving this page will abandon these changes.');
        }
    });

    // Stops the edit buttons from performing their default PostBack behavior
    $('.editEmbeddable').click(function (e) {
        e.preventDefault();
    });

    // Displays the modal dialog with a gray mask on the background
    $.fn.DisplayDialog = (function (id, em) {
        $('#mask').css({ 'width': $(window).width(), 'height': $(window).height() });

        // start up the embeddableLoader 
        $('#dialogFrame').attr('src', 'embeddableLoader.aspx?id=' + id + "&em=" + em);

        $('#mask').fadeTo(500, .5);

        // center the embeddable editor popup with room for the nav bar
        $('#dialog').css('top', (($(window).height() / 2 - $('#dialog').height() / 2) - 93));
        $('#dialog').css('left', $(window).width() / 2 - $('#dialog').width() / 2);

        $('#dialog').fadeIn(500);
    });

    // Closes the modal dialog
    $('#dialog .close').click(function (e) {
        e.preventDefault();
        $('#mask').hide();
        $('#dialog').hide();
    });
});
    