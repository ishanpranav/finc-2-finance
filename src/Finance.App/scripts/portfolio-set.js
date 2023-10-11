// portfolio-set.js
// Copyright(c) 2023 Ishan Pranav. All rights reserved.
// Licensed under the MIT License.

const page = window.chrome.webview.hostObjects.page;
const chart = document.getElementById('chart');

const config = {
    type: 'scatter',
    data: {
        datasets: [{
            label: "Investment Opportunity Set"
        }]
    },
    options: {
        elements: {
            point: {
                radius: 1,
                backgroundColor: 'rgb(255, 99, 132)'
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
    config.data.datasets[0].data = JSON.parse(await page.getChartDataJson);
}

main().then(() => {
    new Chart(chart, config);
});
