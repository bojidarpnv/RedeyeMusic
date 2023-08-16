$(document).ready(function () {
    $('.add-to-playlist').click(function () {
        var songId = $(this).data('song-id');
        var modalId = '#addToPlaylistModal-' + songId;

        // Array to store selected playlist IDs
        var selectedPlaylistIds = [];

        // Log selected playlist IDs before opening the modal
        console.log(`Before opening modal - ${modalId} - selectedPlaylistIds:`, selectedPlaylistIds);

        // Collect selected playlist IDs from checkboxes
        $('input[name="playlist-checkbox"][data-preselected="True"]:checked', modalId).each(function () {
            var playlistId = $(this).val();
            if (selectedPlaylistIds.indexOf(playlistId) === -1) {
                selectedPlaylistIds.push(playlistId);
            }
        });

        // Store the array in modal's data attribute
        $(modalId).data('selected-playlist-ids', selectedPlaylistIds);

        // Clear the array when modal is closed
        $(modalId).on('hidden.bs.modal', function () {
            $(modalId).data('selected-playlist-ids', []);
        });

        // Log selected playlist IDs after opening the modal
        console.log(`After opening modal - ${modalId} - selectedPlaylistIds:`, $(modalId).data('selected-playlist-ids'));

        // Show the modal
        $(modalId).modal('show');
    });
    $('body').on('change', 'input[name="playlist-checkbox"]', function () {
        var modalId = $(this).closest('.modal').attr('id');
        var selectedPlaylistIds = $('#' + modalId).data('selected-playlist-ids') || [];

        var playlistId = $(this).val();
        if ($(this).prop('checked')) {
            if (selectedPlaylistIds.indexOf(playlistId) === -1) {
                selectedPlaylistIds.push(playlistId);
            }
        } else {
            var index = selectedPlaylistIds.indexOf(playlistId);
            if (index !== -1) {
                selectedPlaylistIds.splice(index, 1);
            }
        }

        $('#' + modalId).data('selected-playlist-ids', selectedPlaylistIds);
    });
    $('.create-playlist-btn').click(function () {
        var antiForgeryToken = $("input[name='__RequestVerificationToken']").val();
        var songId = $(this).data('song-id');
        var modalId = '#addToPlaylistModal-' + songId;
        var newPlaylistName = $('#newPlaylistName').val();

        // Get selected playlist IDs from modal's data attribute
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
                localStorage.setItem('createPlaylistSuccess', 'true');
                $(modalId).modal('hide');
                window.location.reload();
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

        // Get selected playlist IDs from modal's data attribute
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
                localStorage.setItem('createPlaylistSuccess', 'true');
                $(modalId).modal('hide');
                window.location.reload();
            },
            error: function () {
                // Handle error
            }
        });
    });

    if (localStorage.getItem('createPlaylistSuccess') === 'true') {
        toastr.success("Successfully created playlist!");
        // Clear the success message from localStorage
        localStorage.removeItem('createPlaylistSuccess');
    }

    // Rest of your code...
});
