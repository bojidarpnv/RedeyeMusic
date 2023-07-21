document.addEventListener("DOMContentLoaded", function () {
    // Function to play audio
    function playAudio(songUrl, startTime = 0) {
        const audioPlayer = document.getElementById('musicPlayer');
        const baseUrl = window.location.origin; // Get the base URL of your application
        const fullUrl = baseUrl + '/' + songUrl; // Construct the full URL for the audio file

        audioPlayer.src = fullUrl;
        audioPlayer.currentTime = startTime; // Set the current playback time

        // Check if the browser supports the `play` method and play the audio
        if (audioPlayer.play) {
            const playPromise = audioPlayer.play();
            if (playPromise !== undefined) {
                playPromise
                    .then(_ => {
                        // Playback started; show playing UI or any other actions needed
                    })
                    .catch(error => {
                        // Auto-play was prevented; show paused UI or any other actions needed
                        console.error('Auto-play was prevented:', error.message);
                    });
            }
        }
    }

    // Function to store the current playback time in localStorage
    function updatePlaybackTime() {
        const audioPlayer = document.getElementById('musicPlayer');
        const currentPlaybackTime = audioPlayer.currentTime;
        const currentSongUrl = localStorage.getItem('currentSongUrl');
        if (currentSongUrl) {
            localStorage.setItem('currentPlaybackTime', currentPlaybackTime);
        }
    }

    // Attach event to update playback time on play
    const audioPlayer = document.getElementById('musicPlayer');
    audioPlayer.addEventListener('play', updatePlaybackTime);

    // Attach click event to song links
    const songLinks = document.getElementsByClassName("song");
    for (let i = 0; i < songLinks.length; i++) {
        const songLink = songLinks[i];
        const songUrl = songLink.getAttribute("data-src");
        songLink.addEventListener("click", function (event) {
            event.preventDefault(); // Prevent the default behavior (e.g., navigating to a new page)

            // Store the current song URL and playback time in localStorage
            const currentTime = audioPlayer.currentTime;
            localStorage.setItem('currentSongUrl', songUrl);
            localStorage.setItem('currentPlaybackTime', currentTime);

            playAudio(songUrl, currentTime); // Pass the current playback time as the second argument
        });
    }

    // Retrieve and play the last played audio on page load
    const lastPlayedSongUrl = localStorage.getItem('currentSongUrl');
    const lastPlaybackTime = parseFloat(localStorage.getItem('currentPlaybackTime'));
    if (lastPlayedSongUrl && !isNaN(lastPlaybackTime)) {
        playAudio(lastPlayedSongUrl, lastPlaybackTime);
    }
});
