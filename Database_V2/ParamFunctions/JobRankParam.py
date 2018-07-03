def JobRankParam(json):
    this={}#JobRankParamjson)
    if 'chcost' in json:
        this['JobChangeCost'] = json['chcost']
    this['JobChangeItems']=[None]*3
    if 'chitm1' in json:
        this['JobChangeItems'][0] = json['chitm1']
    if 'chitm2' in json:
        this['JobChangeItems'][1] = json['chitm2']
    if 'chitm3' in json:
        this['JobChangeItems'][2] = json['chitm3']
    this['JobChangeItemsNums']=[None]*3
    if 'chnum1' in json:
        this['JobChangeItemNums'][0] = json['chnum1']
    if 'chnum2' in json:
        this['JobChangeItemNums'][1] = json['chnum2']
    if 'chnum3' in json:
        this['JobChangeItemNums'][2] = json['chnum3']
    if 'cost' in json:
        this['cost'] = json['cost']
    this['equips']=[None]*6
    if 'eqid1' in json:
        this['equips'][0] = json['eqid1']
    if 'eqid2' in json:
        this['equips'][1] = json['eqid2']
    if 'eqid3' in json:
        this['equips'][2] = json['eqid3']
    if 'eqid4' in json:
        this['equips'][3] = json['eqid4']
    if 'eqid5' in json:
        this['equips'][4] = json['eqid5']
    if 'eqid6' in json:
        this['equips'][5] = json['eqid6']

    this['status']={}
    if 'hp' in json:
        this['status']['hp'] = json['hp']
    this['status']
    if 'mp' in json:
        this['status']['mp'] = json['mp']
    this['status']
    if 'atk' in json:
        this['status']['atk'] = json['atk']
    this['status']
    if 'def' in json:
        this['status']['def'] = json['def']
    this['status']
    if 'mag' in json:
        this['status']['mag'] = json['mag']
    this['status']
    if 'mnd' in json:
        this['status']['mnd'] = json['mnd']
    this['status']
    if 'dex' in json:
        this['status']['dex'] = json['dex']
    this['status']
    if 'spd' in json:
        this['status']['spd'] = json['spd']
    this['status']
    if 'cri' in json:
        this['status']['cri'] = json['cri']
    this['status']
    if 'luk' in json:
        this['status']['luk'] = json['luk']
    if 'avoid' in json:
        this['avoid'] = json['avoid']
    if 'inimp' in json:
        this['inimp'] = json['inimp']

    this['learnings']=[]
    for i in range(1,4):
        learn='learn'+str(i)
        if learn in json:
            this['learnings'].append(json[learn])
    if len(this['learnings'])==0:
        del this['learnings']

    return this
