$(function () {
    $('.claimdatepicker').datetimepicker({
        format: 'L',
        locale: 'en-gb',
        showTodayButton: true,
        minDate: moment().subtract(5, 'years'),
        maxDate: moment()
    });
    $('.agedatepicker').datetimepicker({
        format: 'L',
        locale: 'en-gb',
        minDate: moment().subtract(130, 'years'),
        maxDate: moment().subtract(5, 'years')

    });

    $('.datepicker').datetimepicker({
        format: 'L',
        locale: 'en-gb',
        showTodayButton: true

    });
    $.validator.addMethod('date',
        function (value) {
            try {
                moment(value, "DD-MM-YYYY");
            } catch (err) {
                return false;
            }
            return true;
        });
});