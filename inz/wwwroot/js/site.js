// Write your JavaScript code.

var mediaPlayer = document.getElementById("audioPlayer");
var progressbar = document.getElementById('seekbar');

window.onload = function () {

    $(document).on('click', '.ajax', function (e) {

        var url = $(this).attr("formaction");

        $.ajax({
            url: url,
            success: function (data) {
                $('#renderbody').fadeOut(1000, function () {
                    $("#renderbody").html(data);
                    $("#renderbody").fadeIn(1000);
                    window.history.pushState("", "", url);
                })
            }
        });
    });

    $(document).on('submit', '.ajaxSearch', function (e) {

        e.preventDefault();

        var url = $(this).attr("action");
        var search = $(".ajaxSearch").serialize();

        $.ajax({
            url: url,
            data: $(".ajaxSearch").serialize(),
            success: function (data) {
                $('#renderbody').fadeOut(1000, function () {
                    $("#renderbody").html(data);
                    $("#renderbody").fadeIn(1000);
                    window.history.pushState("", "", url + '?' + search);
                })
            }
        });
    });


    $(document).on('submit', '.ajaxForm', function (e) {
        
        e.preventDefault();

        var url = $(this).attr("action");
        var search = $(".ajaxForm").serialize();

        $.ajax({
            url: url,
            data: $(".ajaxForm").serialize(),
            success: function (data) {
                $('#renderbody').fadeOut(1000, function () {
                    $("#renderbody").html(data);
                    $("#renderbody").fadeIn(1000);
                    window.history.pushState("", "", url + '?' + search);
                })
            }
        });
    });



    $(document).on('submit', '.ajaxCreate', function (e) {

            e.preventDefault();

            $("#renderbody").fadeOut(500);

            $('.loader').fadeIn(1000);
            $('.loader').css("display", "block");

            var url = $(this).attr("action");


            var form = $(this);
            var formdata = false;
            if (window.FormData) {
                formdata = new FormData(form[0]);
            }

            $.ajax({
                type: 'POST',
                url: url,
                data: formdata ? formdata : form.serialize(),
                cache: false,
                contentType: false,
                processData: false,
                success: function (data) {
                    $('.loader').css("display", "none").fadeOut(1000);
                    $("#renderbody").html(data).fadeIn(1000);
                    window.history.pushState("", "", url);
                }
            });
    });
}

function change(song, title, artist, imgAlbum) {

    $('.player').show();

    var imgCoverAlbum = document.getElementById('albumCover');

    if (imgAlbum != "") {
        imgCoverAlbum.src = "https://pracainzynierska.blob.core.windows.net/imgalbum/" + imgAlbum;
    }
    else {
        imgCoverAlbum.src = "https://www.tunefind.com/i/album-art-empty.png";
    }

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

function SetVolume(val) {
    mediaPlayer.volume = val / 100;
}