window.onload = function () {
    $("#datetimepicker").data('xdsoft_datetimepicker').setOptions({ format: 'Y-m-d H:i:s' });

    //var map, infoWindow;
    //function initMap() {
    //    map = new google.maps.Map(document.getElementById('map'), {
    //        center: { lat: -34.397, lng: 150.644 },
    //        zoom: 16
    //    });
    //    infoWindow = new google.maps.InfoWindow;

    //    // Try HTML5 geolocation.
    //    if (navigator.geolocation) {
    //        navigator.geolocation.getCurrentPosition(function (position) {
    //            var pos = {
    //                lat: position.coords.latitude,
    //                lng: position.coords.longitude
    //            };

    //            infoWindow.setPosition(pos);
    //            infoWindow.setContent('Location found.');
    //            infoWindow.open(map);
    //            map.setCenter(pos);
    //        }, function () {
    //            handleLocationError(true, infoWindow, map.getCenter());
    //        });
    //    } else {
    //        // Browser doesn't support Geolocation
    //        handleLocationError(false, infoWindow, map.getCenter());
    //    }
    //}

    //function handleLocationError(browserHasGeolocation, infoWindow, pos) {
    //    infoWindow.setPosition(pos);
    //    infoWindow.setContent(browserHasGeolocation ?
    //        'Error: The Geolocation service failed.' :
    //        'Error: Your browser doesn\'t support geolocation.');
    //    infoWindow.open(map);
    //}

}

jQuery('#datetimepicker').datetimepicker({
    onChangeDateTime: logic,
    onShow: logic,
    onChangeDateTime: function (dp, $input) {
        //alert($input.val())
        $("#DateTime").val($input.val());
    }
});

var logic = function (currentDateTime) {
    // 'this' is jquery object datetimepicker
    if (currentDateTime.getDay() == 6) {
        this.setOptions({
            minTime: '11:00'
        });
    } else
        this.setOptions({
            minTime: '8:00'
        });
};

tinymce.init({
    selector: "textarea",
    convert_urls: false,
    theme: "modern",
    paste_data_images: true,
    height: "300",
    plugins: [
        "advlist autolink lists link image charmap print preview hr anchor pagebreak",
        "searchreplace wordcount visualblocks visualchars code fullscreen",
        "insertdatetime media nonbreaking save table contextmenu directionality",
        "emoticons template paste textcolor colorpicker textpattern"
    ],
    toolbar1: "insertfile undo redo | styleselect | bold italic | alignleft aligncenter alignright alignjustify | bullist numlist outdent indent | link image",
    toolbar2: "print preview media | forecolor backcolor emoticons",
    image_advtab: true,
    file_picker_callback: function (callback, value, meta) {
        if (meta.filetype == 'image') {
            $('#upload').trigger('click');
            $('#upload').on('change', function () {
                var file = this.files[0];
                var reader = new FileReader();
                reader.onload = function (e) {
                    callback(e.target.result, {
                        alt: ''
                    });
                };
                reader.readAsDataURL(file);
            });
        }
    },
    templates: [{
        title: 'Test template 1',
        content: 'Test 1'
    }, {
        title: 'Test template 2',
        content: 'Test 2'
    }]
});


