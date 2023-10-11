// portfolio-set.js
// Copyright(c) 2023 Ishan Pranav. All rights reserved.
// Licensed under the MIT License.

const page = window.chrome.webview.hostObjects.page;
const chart = document.getElementById('chart');

function percentageCallback(value) {
    return (100 * value).toFixed(4) + "%";
}

const config = {
    type: 'scatter',
    options: {
        scales: {
            x: {
                ticks: {
                    callback: percentageCallback
                }
            },
            y: {
                ticks: {
                    callback: percentageCallback
                }
            }
        },
        elements: {
            point: {
                radius: 1
            },
        },
        plugins: {
            title: {
                display: true
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
