document.addEventListener("DOMContentLoaded", function () {
    const defaultVolume = 0.2;
    // Function to play audio
    function playAudio(songUrl, startTime = 0) {
        const audioPlayer = document.getElementById('musicPlayer');
        const baseUrl = window.location.origin; // Get the base URL of your application
        const fullUrl = baseUrl + '/' + songUrl; // Construct the full URL for the audio file

        audioPlayer.src = fullUrl;
        audioPlayer.currentTime = startTime; // Set the current playback time
        audioPlayer.volume = defaultVolume;
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
function getListenCount() {
    async function fetchListenCount(songId) {
        
        
        const response = await fetch(`https://localhost:7004/api/listen-count?songId=${songId}`);
        if (response.ok) {
            const data = await response.json();
            return data.listenCount;
        } else {
            return -1; 
        }
    }

    
    async function updateListenCounts() {
        const songElements = document.querySelectorAll('.song-item');
        for (const songElement of songElements) {
            const songId = songElement.dataset.songId; 
            
            const listenCountPlaceholder = songElement.querySelector('.listen-count-placeholder');
            if (songId) {
                const listenCount = await fetchListenCount(songId);
                if (listenCount >= 0) {
                    listenCountPlaceholder.textContent = listenCount;
                } else {
                    listenCountPlaceholder.textContent = 'Error fetching listen count';
                }
            } else {
                listenCountPlaceholder.textContent = 'N/A'; 
            }
        }
    }

    
    document.addEventListener('DOMContentLoaded', () => {
        updateListenCounts();
    });
}
