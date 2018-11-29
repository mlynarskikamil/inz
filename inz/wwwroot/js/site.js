// Write your JavaScript code.

var mediaPlayer = document.getElementById("audioPlayer");
var progressbar = document.getElementById('seekbar');

function change(song, title, artist) {

    $('.player').show();

    mediaPlayer.src = song;
    mediaPlayer.load();
    mediaPlayer.play();

    $('#pause').show();
    $('#play').hide();

    var songTitle = document.getElementById("songTitle");
    songTitle.textContent = title;

    var artistTitle = document.getElementById("artistTitle");
    artistTitle.textContent = artist;
}

function playAudio() {
    if (mediaPlayer.paused) {
        mediaPlayer.play();
        $('#pause').show();
        $('#play').hide();
    } else {
        mediaPlayer.pause();
        $('#play').show();
        $('#pause').hide();
    }
} 

function udpateProgress() {
    progressbar.value = (mediaPlayer.currentTime / mediaPlayer.duration);
};