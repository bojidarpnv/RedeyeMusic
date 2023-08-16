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

                // Display the audio player wrapper
                audioWrapper.style.display = "block";
            }
        });
    });

    // Listen for audio ended event
    audioPlayer.addEventListener("ended", function () {
        // Hide the audio player wrapper when playback ends
        audioWrapper.style.display = "none";
    });
});



///////////////////////////////////////////////////////////////////////////////////


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
