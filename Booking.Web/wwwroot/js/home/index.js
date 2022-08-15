$(document).ready(function () {
    $('#txtStartDate').datepicker({
        autoclose: true,
        clearBtn: true,
        startDate: '+1d',
        endDate: '+30d'
    });
    $('#txtEndDate').datepicker({
        autoclose: true,
        clearBtn: true,
        startDate: '+1d',
        endDate: '+30d'
    });

    $('#txtStartDate').on("change", function () {
        //when chosen the from date, the end date can be from that point forward
        var startVal = $('#txtStartDate').val();
        $('#txtEndDate').data('datepicker').setStartDate(startVal);
    });
    $('#txtEndDate').on("change", function () {
        //when chosen the end date, start can go just up until that point
        var endVal = $('#txtEndDate').val();
        $('#txtStartDate').data('datepicker').setEndDate(endVal);
    });
});