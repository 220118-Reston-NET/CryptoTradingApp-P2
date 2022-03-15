import requests
import json
import csv
response = requests.get('https://api.coindesk.com/v1/bpi/currentprice.json')
data = response.json()
print(data["bpi"]["USD"]["rate"])

filename = "stockprices.json"

ditctionary ={
    "cryptoName" : data["chartName"],
    "currentPrice" : data["bpi"]["USD"]["rate"]
}

json_object = json.dumps(ditctionary, indent = 4)

with open(filename, "w") as outfile:
    outfile.write(json_object)
