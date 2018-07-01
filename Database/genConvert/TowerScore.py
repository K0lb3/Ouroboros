def TowerScore(json):
    this={}#TowerScorejson)
    #returnfalse
    if 'rank' in json:
        this['Rank'] = json['rank']
    if 'score' in json:
        this['Score'] = json['score']
    if 'turn' in json:
        this['TurnCnt'] = json['turn']
    if 'died' in json:
        this['DiedCnt'] = json['died']
    if 'retire' in json:
        this['RetireCnt'] = json['retire']
    if 'recover' in json:
        this['RecoverCnt'] = json['recover']
    #returntrue
return this
