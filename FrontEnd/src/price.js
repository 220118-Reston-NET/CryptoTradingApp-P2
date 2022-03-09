updatePrices();
function updatePrices() {
    var liveprice = {
        "async": true,
        "scroosDomain": true,
        "url": "https://api.coingecko.com/api/v3/coins/markets?vs_currency=usd&order=market_cap_desc",

        "method": "GET",
        "headers": {}
    }

    $.ajax(liveprice).done(function (response) {
        buildTable(response);
    });
}

function buildTable(data) {
    var table = document.getElementById("crypto");

    for (var i = 0; i < data.length; i++) {
        var symbol = data[i].symbol;
        var price = data[i].current_price;
        var change = data[i].price_change_percentage_24h;
        var cap = data[i].market_cap;
        var row;

        if (change < 0) {
            row = `<tr>
                        <td><img src="${data[i].image}" class="cryptologo"/></td>
                        <td>${data[i].name}</td>
                        <td>${symbol.toUpperCase()}</td>
                        <td>$${price.toLocaleString()}</td>
                        <td class="cryptoRed">${change.toLocaleString()}%</td>
                        <td>$${cap.toLocaleString()}</td>
                    </tr>`
        } else {
            row = `<tr>
                        <td><img src="${data[i].image}" class="cryptologo"/></td>
                        <td>${data[i].name}</td>
                        <td>${symbol.toUpperCase()}</td>
                        <td>$${price.toLocaleString()}</td>
                        <td class="cryptoGreen">+${change.toLocaleString()}%</td>
                        <td>$${cap.toLocaleString()}</td>
                    </tr>`
        }
        table.innerHTML += row
      }
}