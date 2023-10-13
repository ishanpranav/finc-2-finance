// time-series.js
// Copyright(c) 2023 Ishan Pranav. All rights reserved.
// Licensed under the MIT License.

const page = window.chrome.webview.hostObjects.page;
const chart = document.getElementById('chart');

const config = {
    type: 'line',
    options: {
        animation: false,
        elements: {
            point: {
                radius: 0
            },
            line: {
                tension: 0.3
            }
        },
        plugins: {
            title: {
                display: true
            },
            legend: {
                position: 'left',
                align: 'start'
            },
            zoom: {
                pan: {
                    enabled: true,
                    mode: 'xy'
                },
                zoom: {
                    wheel: {
                        enabled: true,
                    },
                    pinch: {
                        enabled: true
                    },
                    mode: 'xy'
                }
            }
        }
    }
};

async function main() {
    config.options.plugins.title.text = await page.title;
    config.data = JSON.parse(await page.getChartDataJson);
}

main().then(() => {
    new Chart(chart, config);
});
