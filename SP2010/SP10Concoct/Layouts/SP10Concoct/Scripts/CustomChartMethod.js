
function CreateChart(url, dvId, title, legendText, chartType) {

    $(document).ready(function () {

        $.ajax(
            {
                type: "POST",
                url: url,
                data: "",
                dataType: 'json',
                success: function (msg) {
                    var data = msg;
                    // alert(data);
                    initChart(data, dvId, title, legendText, chartType);
                }
            });
    });
}


function initChart(data, dvId, title, legendText, chartType) {

    var background = {
        type: 'linearGradient',
        x0: 0,
        y0: 0,
        x1: 0,
        y1: 1,
        colorStops: [{ offset: 0, color: '#d2e6c9' },
                             { offset: 1, color: 'white'}]
    };

    $('#' + dvId).jqChart({
        title: { text: title },
        animation: { duration: 1 },
        legend: { title: legendText },
        border: { strokeStyle: '#6ba851' },
        background: background,
        series: [
                            {
                                type: chartType,
                                labels: {
                                    // stringFormat: '%.1f%%',
                                    // valueType: 'percentage',
                                    font: '15px sans-serif',
                                    fillStyle: 'white'
                                },
                                data: data
                            }
                        ]
    });


    $('#' + dvId).bind('tooltipFormat', function (e, data) {
        var percentage = data.series.getPercentage(data.value);
        percentage = data.chart.stringFormat(percentage, '%.2f%%');

        return '<b>' + data.dataItem[0] + '</b></br>' +
                       data.value + ' (' + percentage + ')';
    });


}