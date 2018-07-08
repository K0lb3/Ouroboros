from ParamFunctions._variables import ENUM,RAWBIRTH,RAWELEMENT
def QuestCondParam(json):
    this={}#QuestCondParamjson)
    if 'iname' in json:
        this['iname'] = json['iname']
    if 'plvmax' in json:
        this['plvmax'] = json['plvmax']
    if 'plvmin' in json:
        this['plvmin'] = json['plvmin']
    if 'ulvmax' in json:
        this['ulvmax'] = json['ulvmax']
    if 'ulvmin' in json:
        this['ulvmin'] = json['ulvmin']
    if 'sex' in json:
        this['sex'] = ENUM['ESex'][json['sex']]
    if 'rmax' in json:
        this['rmax'] = json['rmax']
    if 'rmin' in json:
        this['rmin'] = json['rmin']
    if 'hmax' in json:
        this['hmax'] = json['hmax']
    if 'hmin' in json:
        this['hmin'] = json['hmin']
    if 'wmax' in json:
        this['wmax'] = json['wmax']
    if 'wmin' in json:
        this['wmin'] = json['wmin']

    elems=[
        'el_none','el_fire','el_watr','el_wind','el_thdr','el_lit','el_drk'
        ]
    this['isElemLimit']={
        RAWELEMENT[ind]:json[elem]
        for ind,elem in enumerate(elems)
        if elem in json
    }
    
    this['job']=[
        json['jobset'+str(i)] if 'jobset'+str(i) in json else None
        for i in range(1,4)
    ]


    if 'unit' in json:
        this['unit'] = json['unit']

    if 'birth' in json:
        this['birth'] = [
            RAWBIRTH[birth]
            for birth in json['birth']
        ]

    this['party_type'] = ENUM['PartyCondType'][json['party_type']] if 'party_type' in json else 'None'
    #returntrue
    return this
