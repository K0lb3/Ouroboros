def VersusCoin(json):
    this={}#VersusCoinjson)
    #if(json==null)
    #return
    if 'iname' in json:
        this['iname'] = json['iname']
    if 'coin_iname' in json:
        this['coin_iname'] = json['coin_iname']
    if 'win_cnt' in json:
        this['win_cnt'] = json['win_cnt']
    if 'lose_cnt' in json:
        this['lose_cnt'] = json['lose_cnt']
    if 'draw_cnt' in json:
        this['draw_cnt'] = json['draw_cnt']
return this
