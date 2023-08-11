$(document).ready(function () {
    $('.add-to-playlist').click(function () {
        var songId = $(this).data('song-id');
        $('#addToPlaylistModal-' + songId).data('song-id', songId); // Store songId in the modal's data attribute
        $('#addToPlaylistModal-' + songId).modal('show');
    });

    $('.create-playlist-btn').click(function () {
        var songId = $(this).closest('.modal').data('song-id'); // Retrieve songId from the modal's data attribute
        var newPlaylistName = $('#newPlaylistName').val();
        var selectedPlaylistIds = [];

        $('input[name="playlist-checkbox"]:checked').each(function () {
            selectedPlaylistIds.push($(this).val()); // Use the value attribute as the playlist ID
        });
        

        $.ajax({
            type: 'POST',
            url: '/Playlist/Create', // Change the URL to the correct route
            data: {
                songId: songId,
                playlistName: newPlaylistName,
                selectedPlaylistIds: selectedPlaylistIds
            },
            success: function () {
                // Handle success
                location.reload();
                $('#addToPlaylistModal-' + songId).modal('hide');
                toastr.success("Succesfully created playlist!")

            },
            error: function () {
                // Handle error
            }
        });
    });

    $('.save-playlist-btn').click(function () {
        var selectedPlaylistIds = []; // Array to store selected playlist IDs
        $('input[name="playlist-checkbox"]:checked').each(function () {
            selectedPlaylistIds.push($(this).val()); // Use the value attribute as the playlist ID
        });

        var songId = $(this).data('song-id'); // Get the song ID
        console.log('Song ID:', songId);
        console.log('Selected Playlist IDs:', selectedPlaylistIds)
        // Perform AJAX request to update playlists
        $.ajax({
            type: 'POST',
            url: '/Playlist/Update', // Change the URL to the correct route
            data: {
                songId: songId,
                selectedPlaylistIds: selectedPlaylistIds
            },
            success: function (data) {
                // Handle success, close the modal, or show a success message
                $('#addToPlaylistModal-' + songId).modal('hide');

                toastr.success("Succesfully saved playlists!")
            },
            error: function () {
                // Handle error
            }
        });
    });
});
