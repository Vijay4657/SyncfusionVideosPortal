
function radiobuttonChange() {
    document.getElementById("newviderlinkdiv").style.display = "none";
    document.getElementById("videoLinkDiv").style.display = "block";
}

function newradiobuttonChange() {
    document.getElementById("videoLinkDiv").style.display = "none";
    document.getElementById("newviderlinkdiv").style.display = "block";
    $("#errorvideolink").html("");
}

function videoUpload() {
    var title = $("#videotitle").val();
    var description = $("#videodescription").val();
    var islatest = document.getElementById("islatestvideo").checked;
    var isfeature = document.getElementById("isfeatureVideo").checked;
    var embeddedVideoLink = "";
    if (document.getElementById("existingLink").checked) {
        embeddedVideoLink = $("#existingvideoLink").val();
    }
    var thumbnailLink = $("#thumbnailLink").val();
    var platform = $("#platforms-filter").val();
    var tag = $("#tagLink").val();

    if (title == "" || title == null) {
        $("#errortitle").html("Required field");
    }

    if (description == "" || description == null) {
        $("#errordescription").html("Required field");
    }

    if (thumbnailLink == "" || thumbnailLink == null) {
        $("#errorthumbnail").html("Required field");
    }

    if ((document.getElementById("existingLink").checked) && (embeddedVideoLink == "" || embeddedVideoLink == null)) {
        $("#errorvideolink").html("Required field");
    }

    if (platform == "" || platform == null || platform == "Choose Platforms") {
        $("#errorplatform").html("Required field");
    }

    if (tag == "" || tag == null) {
        $("#errortag").html("Required field");
    }


    var errorTitlevalue = document.getElementById("errortitle").value;
    var errordescriptionvalue = document.getElementById("errordescription").value;
    var errorthumbnailvalue = document.getElementById("errorthumbnail").value;
    var errorplatformvalue = document.getElementById("errorplatform").value;
    var errortagvalue = document.getElementById("errortag").value;

    if ((errorTitlevalue == "" || errorTitlevalue == null || errorTitlevalue == undefined) && (errordescriptionvalue == "" || errordescriptionvalue == null || errordescriptionvalue == undefined)
        && (errorthumbnailvalue == "" || errorthumbnailvalue == null || errorthumbnailvalue == undefined) && (document.getElementById("existingLink").checked && ($("#errorvideolink").text() == "" || $("#errorvideolink").text() == null || $("#errorvideolink").text() == undefined))
        && (errorplatformvalue == "" || errorplatformvalue == null || errorplatformvalue == undefined) && (errortagvalue == "" || errortagvalue == null || errortagvalue == undefined)) {
        $.ajax({
            dataType: 'json',
            type: "POST",
            url: '/updatevideo',
            data: {
                'title': title,
                'description': description,
                'platform': platform,
                'isLatest': islatest,
                'isFeature': isfeature,
                'youTubeLink': embeddedVideoLink,
                'thumbnailLink': thumbnailLink,
                'tag': tag
            },
            error: function (response) {
                $('#errorvideo-upload').text("Unknown error occured");
                $('#errorvideo-upload').css('display', 'block');
            },
            success: function (response) {
                if (response.result === true) {
                    var slug = response.SlugTitle;
                    window.location.href = "/videos/" + platform + "/" + slug;
                }
                else {
                    
                }
            },
        });
    }
}
