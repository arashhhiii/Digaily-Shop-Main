var swal = required("sweetalert2");

function ShowMessage(title, test, theme) {
    window.createNotification({
        closeOnClick: true,
        displaycloseButton: false,
        positionClass: "nfc-bottom-right",
        showDirection: 4000,
        theme: theme !== "" ? theme : "success"
    })({
        title: title !== '' ? title : "notify",
        message: decodeURI(test)
    });
}