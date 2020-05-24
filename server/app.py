"""
 API:
/version
          returns dllVersion;scriptVersion
/join?team={team}&player={playername}
          returns current "data", usually 000000000000000000000000000
/init?player={playername}
          returns new team number
/coop?team={team}&player={playername}&data={data}
          returns "data", followed by a '{' separated list of users to map to items
/teamcheck?team={team}
          returns a json object:
              {"players": string array, "playeritems": string array, "data": string, messages: string array, "team": string}
"""
import flask
from redis import Redis
from flask import Flask, request, abort, Response
from datetime import datetime
import random
import pickle
import json
app = Flask(__name__)

FMTSTRING_JOIN = "{0} has connected."
FMTSTRING_REJOIN = "{0} has reconnected."
ITEM_NAMES = ["Lute", "Crown", "Crystal", "Herb", "Key", "TNT", 
              "Adamant", "Slab", "Ruby", "Rod", "Floater", "Chime", 
              "Tail", "Cube", "Bottle", "Oxyale", "Ship", "Canoe", 
              "Airship", "Bridge", "Canal", "SlabTranslation", 
              "EarthOrb", "FireOrb", "WaterOrb", "AirOrb", "EndGame"]

# Make the WSGI interface available at the top level so wfastcgi can get it.
wsgi_app = app.wsgi_app
class LogEntry():
    __slots__ = ["timestamp", "player", "item"]
    def __init__(self, player, item):
        self.timestamp = datetime.now()
        self.player = player
        self.item = item

class GameLogObject():
    __slots__ = ["team", "inittime", "log"]
    def __init__(self, team=""):
        self.team = team
        self.inittime = datetime.now()
        self.log = []

class GameObject():
    __slots__ = ["team", "players", "data", "playeritems", "limit"]
    def __init__(self, team=0, players=None, data="", playeritems=None, limit=0):
        self.team = team
        if players is None:
            players = []
        self.players = players
        self.data = data
        if playeritems is None:
            playeritems = []
        self.playeritems = playeritems
        self.limit = limit

class PlayerObject():
    __slots__ = ["name", "ip", "messages"]
    def __init__(self, name="", ip="", messages=None):
        self.name = name
        self.ip = ip
        if messages is None:
            messages = []
        self.messages = messages

class CoopDB():
    def __init__(self):
        self._redis = Redis(host=os.environ.get('REDIS_HOST', 'localhost'))

    def refreshKey(self, key: str, seconds=3600*24):
        self._redis.expire(key, seconds)

    def getGameObject(self, team: str) -> GameObject:
        dbGameObj = self._redis.get(team)
        if dbGameObj:
            return pickle.loads(dbGameObj)
        return None

    def getPlayerObject(self, player: str) -> PlayerObject:
        dbPlayerObj = self._redis.get(player)
        if dbPlayerObj:
            return pickle.loads(dbPlayerObj)
        return None

    def getGameLogObject(self, team: str) -> GameLogObject:
        key = "log{}".format(team)
        dbGameLogObject = self._redis.get(key)
        if key:
            return pickle.loads(dbGameLogObject)
        return None

    def saveGameObject(self, gameObject: GameObject):
        dbGameObj = pickle.dumps(gameObject)
        self._redis.set(gameObject.team, dbGameObj)
        self.refreshKey(gameObject.team)

    def savePlayerObject(self, playerObject: PlayerObject):
        dbPlayerObj = pickle.dumps(playerObject)
        self._redis.set(playerObject.name, dbPlayerObj)
        self.refreshKey(playerObject.name)

    def saveGameLogObject(self, gameLogObject: GameLogObject):
        dbGameLogObj = pickle.dumps(gameLogObject)
        key = "log{}".format(gameLogObject.team)
        self._redis.set(key, dbGameLogObj)
        self.refreshKey(key)

    
def addMessageToPlayers(message: str, playernames: list):
    db = CoopDB()
    for p in playernames:
        if p:
            pObj = db.getPlayerObject(p)
            if pObj:
                pObj.messages.append(message)
                db.savePlayerObject(pObj)


@app.route('/version')
def epVersion():
    # dll version, then script version
    return "0.13;0.13"

@app.route('/init')
def epInit():
    db = CoopDB()
    player = request.args['player']
    limit = request.args['limit']

    random.seed()
    team = 0
    j = 0
    for i in range(10000):
        j = i
        team = str(random.randint(1000, 9999))
        if not db.getGameObject(team):
            break
    if j > 9998:
        return 'could not get team'
    playerIP = request.remote_addr
    if (request.headers.get("X-Forwarded-For")):
        playerIP = request.headers["X-Forwarded-For"]

    gameobject = GameObject()
    gameobject.data = "000000000000000000000000000"
    gameobject.playeritems = ["","","","","","","","","","","","","","","","","","","","","","","","","","",""]
    gameobject.team = team
    gameobject.limit = int(limit)
    gameobject.players.append(player)

    playerobject = PlayerObject()
    playerobject.name = player
    playerobject.ip = playerIP

    gamelogobject = GameLogObject(team = team)


    db.saveGameObject(gameobject)
    db.savePlayerObject(playerobject)
    db.saveGameLogObject(gamelogobject)

    return team

@app.route('/join')
def epJoin():
    db = CoopDB()
    player = request.args['player']
    team = request.args['team']

    playerIP = request.remote_addr
    if (request.headers.get("X-Forwarded-For")):
        playerIP = request.headers["X-Forwarded-For"]
    cachedPlayer = db.getPlayerObject(player)
    if cachedPlayer:
        if not playerIP == cachedPlayer.ip:
            return "Player name already taken"

    gameobject = db.getGameObject(team)
    if gameobject:
        if (gameobject.limit > 0) and not (len(gameobject.players) < gameobject.limit):
            if player not in gameobject.players:
                abort(Response("Error: game is currently full"))
        playerobject = PlayerObject(name=player, ip=playerIP)
        if not player in gameobject.players:
            addMessageToPlayers(FMTSTRING_JOIN.format(player), [p for p in gameobject.players if p is not player])
            playerobject.messages.append(FMTSTRING_JOIN.format(player))
            gameobject.players.append(player)
        else:
            addMessageToPlayers(FMTSTRING_REJOIN.format(player), [p for p in gameobject.players])
        

        db.saveGameObject(gameobject)
        db.savePlayerObject(playerobject)

        return gameobject.data
    else:
        abort(Response("Error: game does not exist"))
    
@app.route('/coop')
def epCoop():
    db = CoopDB()
    team = request.args['team']
    player = request.args['player']
    data = request.args['data']

    gameobject = db.getGameObject(team)
    playerobject = db.getPlayerObject(player)
    gamelogobject = db.getGameLogObject(team)
    playerIP = request.remote_addr
    if (request.headers.get("X-Forwarded-For")):
        playerIP = request.headers["X-Forwarded-For"]
        print("Got IP from X-Forwarded-For header: {}".format(playerIP))
    else:
        print("Got IP from request object: {}".format(playerIP))

    if not gameobject:
        abort(Response("Error: game does not exist"))
    if not player in gameobject.players:
        abort(Response("Error: player is not in this game"))
    if not playerobject:
        abort(Response("Error: player does not exist"))
    if not playerIP == playerobject.ip:
        abort(Response("Error: player host is incorrect"))
    if not len(data) ==  len(gameobject.data):
        abort(Response("Error: data is malformed"))

    newdata = []
    for i in range(len(data)):
        if (data[i] == "1") or (gameobject.data[i] == "1"):
            newdata.append("1")
            #check if item is newly required
            if gameobject.data[i] == "0":
                gameobject.playeritems[i] = player
                logentry = LogEntry(player = player, item = ITEM_NAMES[i])
                gamelogobject.log.append(logentry)
        else:
            newdata.append("0")
    newdata_s = ''.join(newdata)
    gameobject.data = newdata_s

    #outdata = newdata_s + "{" + '{'.join(gameobject.playeritems)
    outobj = {'data':gameobject.data, 'playeritems':gameobject.playeritems, 'messages':playerobject.messages}
    playerobject.messages = []
    outdata = json.dumps(outobj)


    db.saveGameObject(gameobject)
    db.savePlayerObject(playerobject)
    db.saveGameLogObject(gamelogobject)

    return outdata


@app.route('/log')
def epLog():
    db = CoopDB()
    team = request.args['team']

    gameObj = db.getGameObject(team)
    if not gameObj:
        abort(Response("Error: game not found"))
    gameLogObj = db.getGameLogObject(team)
    gameLogOut = {'game': gameLogObj.team, 'inittime': gameLogObj.inittime.timestamp()}
    gameLogOut['log'] = [{'timestamp': i.timestamp.timestamp(), 'player': i.player, 'item': i.item} for i in gameLogObj.log]
    jsonOut = json.dumps(gameLogOut)
    return jsonOut


if __name__ == '__main__':
    import os
    HOST = os.environ.get('SERVER_HOST', 'localhost')
    try:
        PORT = int(os.environ.get('SERVER_PORT', '5555'))
    except ValueError:
        PORT = 5555
    app.run(HOST, PORT)
