$(document).ready(function () {

    $(".delete-button").click(function () {

        $("#confirmDeleteModal").modal("show");
        

        var songId = $(this).data("song-id");


        $("#songIdInput").val(songId);
    });


    $("#confirmDeleteButton").click(function () {
        var antiForgeryToken = $("input[name='__RequestVerificationToken']").val();
        var password = $("#passwordInput").val();


        var songId = $("#songIdInput").val();


        var formData = new FormData();
        formData.append("id", songId);
        formData.append("password", password);

        $.ajax({
            url: "/Song/Delete",
            method: "POST",
            data: formData,
            processData: false,
            contentType: false,
            headers: {
                RequestVerificationToken: antiForgeryToken
            },
        }).done(function () {
            localStorage.setItem('deleteSongSuccess', 'true');
            
            window.location.href = "/Song/Mine";

        }).fail(function (xhr) {

            toastr.error(xhr.responseText);
        }).always(function () {
            $("#confirmDeleteModal").modal("hide");
            $("#passwordInput").val("");
            $("#confirmDeleteButton").prop("disabled", false);
        });
    });


    $("#confirmDeleteModal").on("hide.bs.modal", function () {
        $("#passwordInput").val("");
    });
    $("#cancelButton").click(function () {
        $("#passwordInput").val("");
        location.reload();
    });
    if (localStorage.getItem('deleteSongSuccess') === 'true') {
        toastr.success("Successfully deleted Song!");
        // Clear the success message from localStorage
        localStorage.removeItem('deleteSongSuccess');
    }
});