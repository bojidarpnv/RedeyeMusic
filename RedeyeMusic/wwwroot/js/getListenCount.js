document.addEventListener('DOMContentLoaded', async () => {
    

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

    updateListenCounts();
    setInterval(updateListenCounts, 5000);
});
