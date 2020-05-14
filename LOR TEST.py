import requests, time
API_KEY = 'API_HERE'
params = dict(api_key=API_KEY)
url = "https://americas.api.riotgames.com/lor/ranked/v1/leaderboards"
playerdict = {}
response = requests.get(url, params=params)
print(response.status_code)
json = response.json()
for x in json['players']:
    playerdict[str(x['name'])] = str(x['rank'])
    string = str(x['name']) + ' is rank: ' + str(x['rank'])
    print(string)
while True:
    print("Doing stuff...")
    # do your stuff
    response = requests.get(url, params=params)
    json = response.json()
    for x in json['players']:
        if str(x['name']) not in playerdict and playerdict[str(x['name'])] != int(x['rank']):
            string = " "
            if str(x['name']) not in playerdict:
                string = 'NEW PLAYER: ' + str(x['name']) + ' is rank: ' + str(x['rank'])
            else:
                string = (str(x['name']) + ' WENT FROM: ' + playerdict[str(x['name'])] + ' TO: ' + str(x['rank']))
            playerdict[str(x['name'])] = int(x['rank'])
            print(string)
    time.sleep(10)