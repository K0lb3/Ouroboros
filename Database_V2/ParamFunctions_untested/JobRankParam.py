def JobRankParam(json):
    this={}#JobRankParamjson)
    #if(json==null)
    #returnfalse
    if 'chcost' in json:
        this['JobChangeCost'] = json['chcost']
    if 'chitm1' in json:
        this['JobChangeItems'][0] = json['chitm1']
    if 'chitm2' in json:
        this['JobChangeItems'][1] = json['chitm2']
    if 'chitm3' in json:
        this['JobChangeItems'][2] = json['chitm3']
    if 'chnum1' in json:
        this['JobChangeItemNums'][0] = json['chnum1']
    if 'chnum2' in json:
        this['JobChangeItemNums'][1] = json['chnum2']
    if 'chnum3' in json:
        this['JobChangeItemNums'][2] = json['chnum3']
    if 'cost' in json:
        this['cost'] = json['cost']
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
    #this.learnings=(OString)null
    #intlength=0
    this['status']
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
    #if(!string.IsNullOrEmpty(json.learn1))
    #++length
    #if(!string.IsNullOrEmpty(json.learn2))
    #++length
    #if(!string.IsNullOrEmpty(json.learn3))
    #++length
    #if(length>0)
        #this.learnings=newOString[length]
        #intnum1=0
        #if(!string.IsNullOrEmpty(json.learn1))
        if 'learn1' in json:
            this['learnings'][n][u][m][1][+][+] = json['learn1']
        #if(!string.IsNullOrEmpty(json.learn2))
        if 'learn2' in json:
            this['learnings'][n][u][m][1][+][+] = json['learn2']
        #if(!string.IsNullOrEmpty(json.learn3))
            #OStringlearnings=this.learnings
            #intindex=num1
            #intnum2=index+1
            #learnings=(OString)json.learn3
    #returntrue
return this
