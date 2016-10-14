var panelSpeed = 160;

$(document).ready(function () {
    /* Hide the panels by default, showing only the first */
    $(".collapseWrapper h3").next().hide();
    $(".collapseWrapper h3:eq(0)").next().show();

    /* Hide the JavaScript-disabled message */
    $("#nonScript").hide();

    $(".collapseWrapper h3").click(function () {
        /* If the current panel is open, no action is necessary. Otherwise, close the rest and open the one clicked on. */
        if ($(this).next().is(':hidden')) {
            $(".collapseWrapper h3").next().slideUp(panelSpeed);
            $(this).next().slideToggle(panelSpeed);
        }
    });
});