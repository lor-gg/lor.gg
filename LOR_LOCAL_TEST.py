import requests, time, json
port = 21337
url = "http://127.0.0.1:" + str(port) + "/static-decklist"
with open("./sets/cards.json", "r", encoding='utf-8' ) as json_file:
    database = json.load(json_file)
    while True:
        try:
            response = requests.get(url)
        except requests.ConnectionError:
            print("LOR NOT RUNNING")
            time.sleep(1)
            continue
        content = response.json()
        if content["DeckCode"] is None:
            print("NOT IN GAME")
            time.sleep(1)
            continue
        else:
            print("\n\nCARDS IN DECK\n")
            for card in content["CardsInDeck"]:
                found = False
                for data in database:
                    if card == data['cardCode']:
                        # odd but to get number of cards in deck you use card val as key
                        # content["CardsInDeck"][card]
                        print(data['name'] + " Number of copies: " + str(content["CardsInDeck"][card]))
                        found = True
                        break
        time.sleep(10)