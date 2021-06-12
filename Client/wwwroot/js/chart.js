function ReportChart(datas , labels , chartTitle) {
    $(document).ready(function () {
        //=======================            Income BarChart               =========================//
        var options = {
            series: [{
                name: chartTitle,
                data: datas
            },],
            responsive: [
                {
                    breakpoint: 450,
                    options: {
                        //plotOptions: {
                        //    bar: {
                        //        horizontal: false
                        //    }
                        //},
                        //legend: {
                        //    position: "bottom"
                        //},
                        xaxis: {
                            labels: {
                                show: false,
                            }
                        },
                        //chart: {
                        //    width:300,
                        //    height: 305,
                        //},

                    }
                }
            ],
            chart: {
                type: 'bar',
                height: 305
            },
            plotOptions: {
                bar: {
                    horizontal: false,
                    columnWidth: '35%',
                    endingShape: 'flat'
                },
            },
            dataLabels: {
                enabled: false
            },
            stroke: {
                show: true,
                width: 1,
                colors: ['transparent']
            },
            colors: ['#05c46b', '#e74c3c', '#2980b9'],
            xaxis: {
                categories: labels,
                labels: {
                    style: {
                        fontSize: '12px',
                        fontFamily: 'yekan',
                        fontWeight: 500,
                    },
                }
            },
            yaxis: {
                type: "category",
                title: {
                    text: ' (تعداد)',
                    rotate: 360,
                    offsetX: 12,
                    offsetY: -125,
                    style: {
                        color: '#78909c',
                        fontSize: '10px',
                        fontFamily: 'yekan',
                        fontWeight: 500,
                    },
                },
                tickAmount: 2,
                labels: {
                    align: 'center',
                    style: {
                        fontFamily: 'yekan',
                        fontSize: 12,
                        fontWeight: 600,
                    },
                }
            },
            legend: {
                fontSize: '12px',
                fontFamily: 'yekan',
                fontWeight: 400,
                markers: {
                    radius: 15,
                    strokeWidth: 1,
                },
            },
            fill: {
                opacity: 1
            },
            tooltip: {
                style: {
                    fontSize: '12px',
                    fontFamily: 'yekan',
                },
                y: {
                    formatter: function (val) {
                        return val.toString();
                    },
                }
            }
        };

        var chart = new ApexCharts(document.querySelector("#reportChart"), options);
        chart.render();
    });
}
