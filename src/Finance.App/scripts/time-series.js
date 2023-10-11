// time-series.js
// Copyright(c) 2023 Ishan Pranav. All rights reserved.
// Licensed under the MIT License.

const page = window.chrome.webview.hostObjects.page;
const chart = document.getElementById('chart');

const config = {
    type: 'line',
    options: {
        elements: {
            point: {
                radius: 0
            },
            line: {
                tension: 1
            }
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
