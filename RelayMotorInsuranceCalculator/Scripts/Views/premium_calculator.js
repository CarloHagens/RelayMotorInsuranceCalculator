function AddDriver() {
          $.ajax({
              url: '/MotorInsurance/AddDriver',
              type: "POST",
              data: $("#Form").serialize(),
              success: function (data) {
                  $('#Drivers').html(data);
                  ReloadPickerValidation();
              }
          });
      }

function RemoveDriver(index) {
    $.ajax({
        url: '/MotorInsurance/RemoveDriver',
        type: "POST",
        data: $("#Form").serialize() + "&index=" + index,
        success: function (data) {
            $('#Drivers').html(data);
            ReloadPickerValidation();
        }
    });
}

function AddClaim(index) {
    $.ajax({
        url: '/MotorInsurance/AddClaim',
        type: "POST",
        data: $("#Form").serialize() + "&index=" + index,
        success: function (data) {
            $('#Drivers').html(data);
            ReloadPickerValidation();
        }
    });
}

function RemoveClaim(driver, claim) {
    $.ajax({
        url: '/MotorInsurance/RemoveClaim',
        type: "POST",
        data: $("#Form").serialize() + "&driver=" + driver + "&claim=" + claim,
        success: function (data) {
            $('#Drivers').html(data);
            ReloadPickerValidation();
        }
    });
}

function Calculate() {
    if ($('#Form').valid()) {
        $.ajax({
            url: '/MotorInsurance/Calculate',
            type: "POST",
            data: $("#Form").serialize(),
            success: function (data) {
                ReloadPickerValidation();
                ShowAlert(data.messageType, data.message);
            }
        });
    }
}

function ReloadPickerValidation() {
    $('#Form').removeData('unobtrusiveValidation');
    $('#Form').removeData('validator');
    $.validator.unobtrusive.parse('#Form');
    $('.agedatepicker').datetimepicker({
        format: 'L',
        locale: 'en-gb',
        minDate: moment().subtract(130, 'years'),
        maxDate: moment()

    });
    $('.claimdatepicker').datetimepicker({
        format: 'L',
        locale: 'en-gb',
        showTodayButton: true,
        minDate: moment().subtract(5, 'years'),
        maxDate: moment()
    });
}