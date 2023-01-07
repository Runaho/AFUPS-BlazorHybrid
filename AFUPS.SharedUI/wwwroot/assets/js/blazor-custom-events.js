
window.TriggerNavClose = () => {

    var body = document.getElementById("bodyClick");
    if (body != undefined) {
        body.click();
    }
}

window.TriggerNav=() => {
    document.getElementsByTagName("html")[0].classList.toggle("nav-open");
    document.getElementsByClassName("navbar-toggle")[0].classList.toggle("toggled");
}

window.TriggerFileInput = (element) => {
    element.click();
}
/*
window.SendNotification = (from, align, icon, _color, message, timer) => {
    color = _color;

    $.notify({
        icon: "nc-icon " + icon,
        message: message

    }, {
        type: color,
        timer: timer,
        placement: {
            from: from,
            align: align
        }
    });
}

window.ClearNotifications = () => {
    $.notifyClose();
}
*/