

function OpenPopup(modalId, modalContainId, url, FormId) {
    loaderstart();
    $('#' + modalContainId + '').empty();
    $('#' + modalContainId + '').load(url, function (response, status, xhr) {
        if (response.indexOf("<meta charset=\"utf-8\" />") != -1) {
            $('#' + modalId + '').modal('hide');
            location.reload();
            return;
        }
        $('#' + modalId + '').modal({
            keyboard: true
        }, 'show');
        bindForm(this, modalId, modalContainId, FormId);
        loaderstop();
    });
}


function bindForm(dialog, modalId, modalContainid, FormId) {
    $('#' + FormId + '', dialog).submit(function () {
        $.ajax({
            url: this.action,
            type: this.method,
            data: $(this).serialize(),
            crossDomain: true,
            success: function (html, status, xhr) {
                if (html.success) {
                    $('#' + modalId + '').modal('hide');
                    if (html.url != null) {
                        location.href = html.url;
                    }
                    else {
                        location.reload();
                    }
                } else {
                    $('#' + modalContainid + '').html(html);

                    bindForm(dialog, modalId, modalContainid, FormId);
                }
            },
            error: function (e) {
                console.log(e)
            }
        });
        return false;
    });
}

function Delete(e, url) {

    swal.fire({
        title: "Are you sure want to delete this record?",
        text: "",
        type: "warning",
        showCancelButton: true,
        confirmButtonClass: "btn-danger",
        confirmButtonText: "Yes",
        cancelButtonText: "No",
        closeOnConfirm: false,
        closeOnCancel: false
    }).then((isConfirm) => {
        if (isConfirm.value) {
            var data = { Id: parseInt($(e).attr('data-id')) }
            $.post(url, data, function (data) {
                if (data == "Error") {
                    swal.fire({ title: "Permission", text: "You have not permission to delete RAWS.", type: "error", showConfirmButton: false });
                } else {
                    location.reload();
                }
            });
            swal.fire({ title: "Deleted!", text: "Your record has been deleted.", type:"success", showConfirmButton: false });
        }
    });
}

