def JobParam(json):
    this={}#JobParamjson)
    #returnfalse
    if 'iname' in json:
        this['iname'] = json['iname']
    if 'name' in json:
        this['name'] = json['name']
    if 'expr' in json:
        this['expr'] = json['expr']
    if 'mdl' in json:
        this['model'] = json['mdl']
    if 'ac2d' in json:
        this['ac2d'] = json['ac2d']
    if 'mdlp' in json:
        this['modelp'] = json['mdlp']
    if 'pet' in json:
        this['pet'] = json['pet']
    if 'buki' in json:
        this['buki'] = json['buki']
    if 'origin' in json:
        this['origin'] = json['origin']
    if 'type' in json:
        this['type'] = ENUM['JobTypes'][json['type']]
    if 'role' in json:
        this['role'] = ENUM['RoleTypes'][json['role']]
    if 'wepmdl' in json:
        this['wepmdl'] = json['wepmdl']
    if 'jmov' in json:
        this['mov'] = json['jmov']
    if 'jjmp' in json:
        this['jmp'] = json['jjmp']
    if 'atkskl' in json:
        this['atkskill[0]'] = string.IsNullOrEmpty?string.Empty:json['atkskl']
    if 'atkfi' in json:
        this['atkskill[1]'] = string.IsNullOrEmpty?string.Empty:json['atkfi']
    if 'atkwa' in json:
        this['atkskill[2]'] = string.IsNullOrEmpty?string.Empty:json['atkwa']
    if 'atkwi' in json:
        this['atkskill[3]'] = string.IsNullOrEmpty?string.Empty:json['atkwi']
    if 'atkth' in json:
        this['atkskill[4]'] = string.IsNullOrEmpty?string.Empty:json['atkth']
    if 'atksh' in json:
        this['atkskill[5]'] = string.IsNullOrEmpty?string.Empty:json['atksh']
    if 'atkda' in json:
        this['atkskill[6]'] = string.IsNullOrEmpty?string.Empty:json['atkda']
    if 'fixabl' in json:
        this['fixed_ability'] = json['fixabl']
    if 'artifact' in json:
        this['artifact'] = json['artifact']
    if 'ai' in json:
        this['ai'] = json['ai']
    if 'master' in json:
        this['master'] = json['master']
    if 'me_abl' in json:
        this['MapEffectAbility'] = json['me_abl']
    if 'is_me_rr' in json:
        this['IsMapEffectRevReso'] = json['is_me_rr']!=0
    if 'desc_ch' in json:
        this['DescCharacteristic'] = json['desc_ch']
    if 'desc_ot' in json:
        this['DescOther'] = json['desc_ot']
    #Array.Clear((Array)this.ranks,0,this.ranks.Length)
            #if(!this.ranks[index].Deserialize(json.ranks[index]))
            #returnfalse
    #returntrue
return this
