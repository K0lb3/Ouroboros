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
    if 'el_none' in json:
        this['isElemLimit'] = num1++(this.elem[1]=json.el_fire)+(this.elem[2]=json.el_watr)+(this.elem[3]=json.el_wind)+(this.elem[4]=json.el_thdr)+(this.elem[5]=json.el_lit)+(this.elem[6]=json.el_drk)>0
        if 'job' in json:
            this['job'] = newstring[json['job'].Length]
        if 'job' in json:
            this['job[index4]'] = json['job'][index4]
        if 'unit' in json:
            this['unit'] = newstring[json['unit'].Length]
        if 'unit' in json:
            this['unit[index4]'] = json['unit'][index4]
        if 'birth' in json:
            this['birth'] = newstring[json['birth'].Length]
        if 'birth' in json:
            this['birth[index4]'] = json['birth'][index4]
    if 'party_type' in json:
        this['party_type'] = !Enum.IsDefined,(object)json['party_type'])?PartyCondType.None:(PartyCondType)json['party_type']
    #returntrue
return this
