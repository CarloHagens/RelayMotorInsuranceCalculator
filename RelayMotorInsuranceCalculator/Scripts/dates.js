$(function () {
    $('.datetimepicker').datetimepicker({
        locale: 'nl',
        date: moment(),
        format: 'DD-MM-YYYY HH:mm zz',
        widgetPositioning: {
            vertical: 'bottom',
            horizontal: 'left'
        },
        showTodayButton: true
    });
    $('.datepicker').datetimepicker({
        format: 'L',
        locale: 'nl',
        widgetPositioning: {
            vertical: 'bottom',
            horizontal: 'left'
        },
        showTodayButton: true
    });
    $('.timepicker').datetimepicker({
        format: 'LT',
        locale: 'nl',
        date: moment(),
        widgetPositioning: {
            vertical: 'bottom',
            horizontal: 'left'
        },
        showTodayButton: true
    });
    $('.datepickerinline').datetimepicker({
        format: 'L',
        locale: 'nl',
        inline: true,
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