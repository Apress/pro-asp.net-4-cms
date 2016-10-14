$(document).ready(function () {
    $(".createSite").click(function () {
        return confirm('Are you sure you want to create this site?');
    });

    $(".updateSite").click(function () {
        return confirm('Are you sure you want to update the entry for this site?');
    });
});