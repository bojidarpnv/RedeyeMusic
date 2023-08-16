$(document).ready(function () {
    $('.add-to-playlist').click(function () {
        var songId = $(this).data('song-id');
        var modalId = '#addToPlaylistModal-' + songId;
        var selectedPlaylistIds = [];

        // Log selected playlist IDs before opening the modal
        console.log(`Before opening modal - ${modalId} - selectedPlaylistIds:`, selectedPlaylistIds);

        $('input[name="playlist-checkbox"][data-preselected="True"]:checked', modalId).each(function () {
            var playlistId = $(this).val();
            if (selectedPlaylistIds.indexOf(playlistId) === -1) {
                selectedPlaylistIds.push(playlistId);
            }
        });

        // Store the array in modal's data attribute
        $(modalId).data('selected-playlist-ids', selectedPlaylistIds);
        $(modalId).on('hidden.bs.modal', function () {
            $(modalId).data('selected-playlist-ids', []);
        });
        // Log selected playlist IDs after opening the modal
        console.log(`After opening modal - ${modalId} - selectedPlaylistIds:`, $(modalId).data('selected-playlist-ids'));

        $(modalId).modal('show');
    });

    $('.create-playlist-btn').click(function () {
        var antiForgeryToken = $("input[name='__RequestVerificationToken']").val();
        var songId = $(this).closest('.modal').data('song-id');
        var modalId = '#addToPlaylistModal-' + songId;
        var newPlaylistName = $('#newPlaylistName').val();

        var selectedPlaylistIds = $(modalId).data('selected-playlist-ids');

        // Log selected playlist IDs before sending in AJAX request
        console.log(`Before sending AJAX - ${modalId} - selectedPlaylistIds:`, selectedPlaylistIds);

        // Perform AJAX request to create a playlist
        $.ajax({
            type: 'POST',
            url: '/Playlist/Create',
            data: {
                songId: songId,
                playlistName: newPlaylistName,
                selectedPlaylistIds: selectedPlaylistIds,
            },
            headers: {
                RequestVerificationToken: antiForgeryToken
            },
            success: function () {
                // Handle success
                window.location.reload();
                $(modalId).modal('hide');
                toastr.success("Successfully created playlist!");
            },
            error: function () {
                // Handle error
            }
        });
    });

    $('.save-playlist-btn').click(function () {
        var antiForgeryToken = $("input[name='__RequestVerificationToken']").val();
        var songId = $(this).data('song-id');
        var modalId = '#addToPlaylistModal-' + songId;

        var selectedPlaylistIds = $(modalId).data('selected-playlist-ids');

        // Log selected playlist IDs before sending in AJAX request
        console.log(`Before sending AJAX - ${modalId} - selectedPlaylistIds:`, selectedPlaylistIds);

        // Perform AJAX request to update playlists
        $.ajax({
            type: 'POST',
            url: '/Playlist/Update',
            data: {
                songId: songId,
                selectedPlaylistIds: selectedPlaylistIds,
            },
            headers: {
                RequestVerificationToken: antiForgeryToken
            },
            success: function (data) {
                // Handle success
                $(modalId).modal('hide');
                toastr.success("Successfully saved playlists!");
            },
            error: function () {
                // Handle error
            }
        });
    });
});
