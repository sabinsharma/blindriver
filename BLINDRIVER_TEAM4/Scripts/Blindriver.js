$(document).ready(function () {

    $("#patientDetail").click(function (e) {

        //console.log("testing ajax");
        e.preventDefault();

        var id = $(this).data(id);
        //alert(id);
        $.ajax({
            url: '../Patients/Details',
            data: id,
            type: "get",
            cache: false,
            success: function (data) {
                //$("#PatientInfo").empty().append(data)
                $("#page_inner #PatientInfo").empty().append(data);
                //console.log($("#page_inner #PatientInfo").attr("id"));
                //console.log(data);

            }
        });//end of ajax
    })//end of patientDetail onclick function

    $("#createPatient").click(function (e) {
        alert("Hello");
        e.preventDefault();
        $.ajax({
            url: '../Patients/Create',
            type: 'get',
            cache: false,
            success: function (data) {
                $("#page_inner #PatientInfo").empty().append(data);
            }
        });//end of ajax function
    });//end of click function.

});//end of document ready function

    /*Source:http://stackoverflow.com/questions/16971079/jquery-to-get-parameter-id-from-mvc-url
     * Author:Nikola Mitev
     * Date:answered Jun 6 '13 at 20:02
     * Viewed on:2017/03/30
     */

    //function GetURLParameter(sPageURL) {
        
    //    var indexOfLastSlash = sPageURL.lastIndexOf("/");

    //    if (indexOfLastSlash > 0 && sPageURL.length - 1 != indexOfLastSlash)
    //        return sPageURL.substring(indexOfLastSlash + 1);
    //    else
    //        return 0;
    //}
    
