import requests
import json
import csv
response = requests.get('https://api.coindesk.com/v1/bpi/currentprice.json')
data = response.json()
print(data["bpi"]["USD"]["rate"])

filename = "A:\Project2\Remove Authentication\CryptoTradingApp-P2\BackEnd\stockprices.json"

ditctionary ={
    "customerID" : -1,
    "cryptoName" : data["chartName"],
    "alertPrice" : data["bpi"]["USD"]["rate"]
}

json_object = json.dumps(ditctionary, indent = 4)

with open(filename, "w") as outfile:
    outfile.write(json_object)
