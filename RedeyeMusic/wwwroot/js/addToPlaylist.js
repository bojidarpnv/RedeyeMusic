$(document).ready(function () {
    // Array to store selected playlist IDs
    
    var selectedPlaylistIds = [];

    $('input[name="playlist-checkbox"][data-preselected="True"]:checked').each(function () {
        var playlistId = $(this).val();
        if (selectedPlaylistIds.indexOf(playlistId) === -1) {
            selectedPlaylistIds.push(playlistId);
        }
        
    });

    // Listen for checkbox changes
    $('input[name="playlist-checkbox"]').on('change', function () {
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
    });

    $('.add-to-playlist').click(function () {
        var antiForgeryToken = $("input[name='__RequestVerificationToken']").val();
        var songId = $(this).data('song-id');

        $('#addToPlaylistModal-' + songId).data('song-id', songId); // Store songId in the modal's data attribute
        $('#addToPlaylistModal-' + songId).modal('show');

        // Don't clear the selectedPlaylistIds array here
    });

    $('.create-playlist-btn').click(function () {
        var antiForgeryToken = $("input[name='__RequestVerificationToken']").val();
        var songId = $(this).closest('.modal').data('song-id'); // Retrieve songId from the modal's data attribute
        var newPlaylistName = $('#newPlaylistName').val();

        // Perform AJAX request to create a playlist
        $.ajax({
            type: 'POST',
            url: '/Playlist/Create', // Change the URL to the correct route
            data: {
                songId: songId,
                playlistName: newPlaylistName,
                selectedPlaylistIds: selectedPlaylistIds,
            },
            headers: {
                // Include the anti-forgery token in the request headers
                RequestVerificationToken: antiForgeryToken
            },

            success: function () {
                // Handle success
                
                $('#addToPlaylistModal-' + songId).modal('hide');
                toastr.success("Successfully created playlist!");
                localStorage.removeItem('selectedPlaylistIds');
                selectedPlaylistIds = [];
            },
            error: function () {
                // Handle error
                selectedPlaylistIds = [];
            }
        });
    });

    $('.save-playlist-btn').click(function () {
        var antiForgeryToken = $("input[name='__RequestVerificationToken']").val();
        var songId = $(this).data('song-id'); // Get the song ID

        // Perform AJAX request to update playlists
        $.ajax({
            type: 'POST',
            url: '/Playlist/Update', // Change the URL to the correct route
            data: {
                songId: songId,
                selectedPlaylistIds: selectedPlaylistIds,
            },
            headers: {
                // Include the anti-forgery token in the request headers
                RequestVerificationToken: antiForgeryToken
            },

            success: function (data) {
                // Handle success, close the modal, or show a success message
                $('#addToPlaylistModal-' + songId).modal('hide');
                toastr.success("Successfully saved playlists!");
                localStorage.removeItem('selectedPlaylistIds');
                selectedPlaylistIds = [];
            },
            error: function () {
                // Handle error
                selectedPlaylistIds = [];
            }
        });
    });

});
