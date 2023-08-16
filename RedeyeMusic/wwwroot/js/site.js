document.addEventListener("DOMContentLoaded", function () {
    const songItems = document.querySelectorAll(".song-item");
    const audioPlayer = document.getElementById("audioPlayer");
    const audioWrapper = document.getElementById("audioWrapper");

    // Hide audio wrapper initially
    audioWrapper.style.display = "none";

    songItems.forEach(songItem => {
        songItem.addEventListener("click", function () {
            const songUrl = songItem.getAttribute("data-src");
            const fullUrl = window.location.origin + '/' + songUrl;

            const songSrc = songItem.getAttribute("data-src");

            if (audioPlayer) {
                // Set the source and play the audio
                audioPlayer.src = fullUrl;
                audioPlayer.play();

                audioWrapper.style.display = "block";

                // Increment listen count via AJAX
                const songId = songItem.getAttribute("data-song-id");
                incrementListenCount(songId);
            }
        });
    });

    // Listen for audio ended event
    audioPlayer.addEventListener("ended", function () {
        // Hide the audio player wrapper when playback ends
        audioWrapper.style.display = "none";
    });

    // Function to increment listen count via API request
    function incrementListenCount(songId) {
        const requestData = JSON.stringify({ songId: songId });
        $.ajax({
            type: 'POST',
            url: 'https://localhost:7004/api/listen-count/increment',
            data: requestData,
            contentType: 'application/json',
            success: function (response) {
                console.log('Listen count incremented successfully');
            },
            error: function () {
                console.error('Failed to increment listen count');
            }
        });
    }
});
