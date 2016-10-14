$(document).ready(function () {
    if (window.location != window.parent.location) {
        window.parent.location = '/login.aspx';
        alert('Your session has ended; please log in to the CMS again.');
    }
});