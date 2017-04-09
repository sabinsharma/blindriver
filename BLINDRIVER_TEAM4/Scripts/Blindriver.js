$(document).ready(function () {

    
    function listPatientInfo() {
        $.post('/Patients/Index').done(function (response) {
            $("#patientInfo").empty().append(response);
            $("#patinetInfo h2").html('Patient Information');

        });//end of ajax
    }
    



    //when the patient information link in the main menu is clicked then it will load the patient information
       $("#linkPatient").click(function () {
        
           listPatientInfo();

    });//end of patient anchor tag click function

    //When the create button in patient information page is clicked it will load the form for users to add the information
       $("#patientInfo").on('click',"#newPatient",function () {
        $.post('/Patients/Create').done(function (response) {
            $("#patientInfo").empty().append(response);
        });
    });//end of new patient click function

       
    //When the user will click submit button in the form to add the patient information, it will save the data.
       $("#patientInfo").on('click', "#btnAddPatient", function () {
           var Patient = {
           FirstName: $("#FirstName").val(),
           MiddleName: $("#MiddleName").val(),
           LastName:$("#LastName").val(),
           Address:$("#Address").val(),
           PostalCode:$("#PostalCode").val(),
           Active:$("#Active").val()
       }
           //console.log(Patient);
           $.post('/Patients/SavePatientInfo', { patient:Patient }, function (response) {
               listPatientInfo(); //list the patient information
           }).fail(function (e) {

               alert("error:"+e.responseText);
           });//end of $post ajax function

       });//end of add patient button click

       var patientEditInfo = {};

    //when the user will click in edit button, get all the value and insert it into the form
       $("#patientInfo").on('click', "#btnEditPatientInfo", function () {
           //end of form submit function
           var id = $(this).data('id');
           $.post('/Patients/Edit', { id: id }, function (response) {
               $("#patientInfo").empty().append(response);
               patientEditInfo
           }).fail(function (e) {
               alert("error: " + e.responseText);
           });//end of post ajax function
       });


    //when update button in Edit form of patient is clicked this function is calle to save the updated information.
       $("#patientInfo").on('submit',"#frmEditPatientInfo",function (e) {
           test("test val");
           e.preventDefault();
       });

       function test(value) {
           if (value === '')
             alert("clicked");
           else
             alert(value);
    }

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