load();

function getAllUrlParams(url) {
    var queryString = url ? url.split('?')[1] : window.location.search.slice(1);
    var obj = {};
    if (queryString) {
      queryString = queryString.split('#')[0];
      var arr = queryString.split('&');
      for (var i = 0; i < arr.length; i++) {
        var a = arr[i].split('=');
        var paramName = a[0];
        var paramValue = typeof (a[1]) === 'undefined' ? true : a[1];
  
        paramName = paramName.toLowerCase();
        if (typeof paramValue === 'string') paramValue = paramValue.toLowerCase();
        if (paramName.match(/\[(\d+)?\]$/)) {
          var key = paramName.replace(/\[(\d+)?\]/, '');
          if (!obj[key]) obj[key] = [];
          if (paramName.match(/\[\d+\]$/)) {
            var index = /\[(\d+)\]/.exec(paramName)[1];
            obj[key][index] = paramValue;
          } else {
            obj[key].push(paramValue);
          }
        } else {
          if (!obj[paramName]) {
            obj[paramName] = paramValue;
          } else if (obj[paramName] && typeof obj[paramName] === 'string'){
            obj[paramName] = [obj[paramName]];
            obj[paramName].push(paramValue);
          } else {
            obj[paramName].push(paramValue);
          }
        }
      }
    }
    return obj;
}

function load() {
    var id = getAllUrlParams(window.location.href).name;
    loadCrypto(id);
    loadChart(id);
}

function loadCrypto(id) {
    var url = "https://api.coingecko.com/api/v3/coins/markets?vs_currency=usd&ids="+id+"&order=market_cap_desc&per_page=100&page=1&sparkline=false";
    var liveprice = {
        "async": true,
        "scroosDomain": true,
        "url": url,

        "method": "GET",
        "headers": {}
    }

    $.ajax(liveprice).done(function (response) {
        buildPage(response);
    });
}

function buildPage(data) {
    var table = document.getElementById("crypto");
    for (var i = 0; i < data.length; i++) {
        var row;
        var symbol = data[i].symbol;
        var price = data[i].current_price;
        var percentageChange = data[i].price_change_percentage_24h;
        var priceChange = data[i].price_change_24h;

        if (percentageChange < 0) {
            row = `<tr>
                        <td style="width: 10px;"><img src="${data[i].image}" class="cryptologoLarge"/></td>
                        <td>${symbol.toUpperCase()}</td>
                        <td>$${price.toLocaleString()}</td>
                        <td class="cryptoRed">${priceChange.toLocaleString()}</td>
                        <td class="cryptoRed">(${percentageChange.toLocaleString()}%)</td>
                    </tr>`
        } else {
            row = `<tr>
                        <td><img src="${data[i].image}" class="cryptologoLarge"/></td>
                        <td>${symbol.toUpperCase()}</td>
                        <td>$${price.toLocaleString()}</td>
                        <td class="cryptoGreen">${priceChange.toLocaleString()}</td>
                        <td class="cryptoGreen">(${percentageChange.toLocaleString()}%)</td>
                    </tr>`
        }

        table.innerHTML += row
    }
}

function loadChart(id) {
    var url = "https://api.coingecko.com/api/v3/coins/"+id+"/market_chart?vs_currency=usd&days=14&interval=daily";

    var liveprice = {
        "async": true,
        "scroosDomain": true,
        "url": url,

        "method": "GET",
        "headers": {}
    }
    $.ajax(liveprice).done(function (response) {
        buildChart(response);
    });
}

function buildChart(data) {
    var time = data.prices.map(x => x[0]);
    var date = [];
    time.forEach(function(t) {
        var read = new Date(t).toLocaleDateString();
        date.push(read);
    });
    new Chart(document.getElementById("chart"), {
        type: 'line',
        data: {
          labels: date,
          datasets: [{ 
              data: data.prices,
              label: "Bitcoin",
              borderColor: "#3e95cd",
              fill: true
            }
          ]
        }
    });
}