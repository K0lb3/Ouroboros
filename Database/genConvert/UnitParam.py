def UnitParam(json):
    this={}#UnitParamjson)
    #if(json==null)
    #returnfalse
    if 'no' in json:
        this['no'] = json['no']
    if 'iname' in json:
        this['iname'] = json['iname']
    if 'name' in json:
        this['name'] = json['name']
    if 'ai' in json:
        this['ai'] = json['ai']
    if 'mdl' in json:
        this['model'] = json['mdl']
    if 'grow' in json:
        this['grow'] = json['grow']
    if 'piece' in json:
        this['piece'] = json['piece']
    if 'birth' in json:
        this['birth'] = json['birth']
    if 'birth_id' in json:
        this['birthID'] = json['birth_id']
        this['birth'] = json['birth']
    if 'skill' in json:
        this['skill'] = json['skill']
    if 'ability' in json:
        this['ability'] = json['ability']
    if 'ma_quest' in json:
        this['ma_quest'] = json['ma_quest']
    if 'sw' in json:
        this['sw'] = Math.Max(json['sw'],1)
    if 'sh' in json:
        this['sh'] = Math.Max(json['sh'],1)
    if 'sex' in json:
        this['sex'] = ENUM['ESex'][json['sex']]
    if 'rare' in json:
        this['rare'] = json['rare']
    if 'raremax' in json:
        this['raremax'] = json['raremax']
    if 'type' in json:
        this['type'] = ENUM['EUnitType'][json['type']]
    if 'elem' in json:
        this['element'] = ENUM['EElement'][json['elem']]
    if 'hero' in json:
        this['hero'] = json['hero']
    if 'search' in json:
        this['search'] = json['search']
    if 'stop' in json:
        this['stopped'] = (json['stop']!=0)
    if 'notsmn' in json:
        this['summon'] = json['notsmn']==0
    #if(!string.IsNullOrEmpty(json.available_at))
        #try
            if 'available_at' in json:
                this['available_at'] = DateTime.Parse
        #catch
            #this.available_at=DateTime.MaxValue
    if 'height' in json:
        this['height'] = json['height']
    if 'weight' in json:
        this['weight'] = json['weight']
    if 'jt' in json:
        this['jobtype'] = ENUM['JobTypes'][json['jt']]
    if 'role' in json:
        this['role'] = ENUM['RoleTypes'][json['role']]
    if 'mov' in json:
        this['mov'] = json['mov']
    if 'jmp' in json:
        this['jmp'] = json['jmp']
    if 'inimp' in json:
        this['inimp'] = json['inimp']
    if 'ma_rarity' in json:
        this['ma_rarity'] = json['ma_rarity']
    if 'ma_lv' in json:
        this['ma_lv'] = json['ma_lv']
        if 'skins' in json:
            this['skins'] = json['skins']
    #if(UnitParam.NoJobStatus.IsExistParam(json))
        #this.no_job_status=newUnitParam.NoJobStatus()
        #this.no_job_status.SetParam(json)
    #if(this.type==EUnitType.EventUnit)
    if 'djob' in json:
        this['djob'] = json['djob']
    if 'dbuki' in json:
        this['dbuki'] = json['dbuki']
    if 'dskl' in json:
        this['default_skill'] = json['dskl']
        if 'dabi' in json:
            this['default_abilities'] = newOString[json['dabi'].Length]
        if 'dabi' in json:
            this['default_abilities[index]'] = json['dabi'][index]
    #returntrue
    #if(json.jobsets!=null)
        #for(intindex=0index<this.jobsets.Length++index)
        if 'jobsets' in json:
            this['jobsets'] = json['jobsets']
    #if(json.tag!=null)
    if 'tag' in json:
        this['tags'] = json['tag'].Split
    #if(this.ini_status==null)
    #this.ini_status=newUnitParam.Status()
    #this.ini_status.SetParamIni(json)
    #this.ini_status.SetEnchentParamIni(json)
    #if(this.max_status==null)
    #this.max_status=newUnitParam.Status()
    #this.max_status.SetParamMax(json)
    #this.max_status.SetEnchentParamMax(json)
    this['ini_status']
    this['ini_status']['param']
    if 'hp' in json:
        this['ini_status']['param']['hp'] = json['hp']
    this['ini_status']['param']
    if 'mp' in json:
        this['ini_status']['param']['mp'] = json['mp']
    this['ini_status']['param']
    if 'atk' in json:
        this['ini_status']['param']['atk'] = json['atk']
    this['ini_status']['param']
    if 'def' in json:
        this['ini_status']['param']['def'] = json['def']
    this['ini_status']['param']
    if 'mag' in json:
        this['ini_status']['param']['mag'] = json['mag']
    this['ini_status']['param']
    if 'mnd' in json:
        this['ini_status']['param']['mnd'] = json['mnd']
    this['ini_status']['param']
    if 'dex' in json:
        this['ini_status']['param']['dex'] = json['dex']
    this['ini_status']['param']
    if 'spd' in json:
        this['ini_status']['param']['spd'] = json['spd']
    this['ini_status']['param']
    if 'cri' in json:
        this['ini_status']['param']['cri'] = json['cri']
    this['ini_status']['param']
    if 'luk' in json:
        this['ini_status']['param']['luk'] = json['luk']
    this['ini_status']
    this['ini_status']['enchant_resist']
    if 'rpo' in json:
        this['ini_status']['enchant_resist']['poison'] = json['rpo']
    this['ini_status']['enchant_resist']
    if 'rpa' in json:
        this['ini_status']['enchant_resist']['paralyse'] = json['rpa']
    this['ini_status']['enchant_resist']
    if 'rst' in json:
        this['ini_status']['enchant_resist']['stun'] = json['rst']
    this['ini_status']['enchant_resist']
    if 'rsl' in json:
        this['ini_status']['enchant_resist']['sleep'] = json['rsl']
    this['ini_status']['enchant_resist']
    if 'rch' in json:
        this['ini_status']['enchant_resist']['charm'] = json['rch']
    this['ini_status']['enchant_resist']
    if 'rsn' in json:
        this['ini_status']['enchant_resist']['stone'] = json['rsn']
    this['ini_status']['enchant_resist']
    if 'rbl' in json:
        this['ini_status']['enchant_resist']['blind'] = json['rbl']
    this['ini_status']['enchant_resist']
    if 'rns' in json:
        this['ini_status']['enchant_resist']['notskl'] = json['rns']
    this['ini_status']['enchant_resist']
    if 'rnm' in json:
        this['ini_status']['enchant_resist']['notmov'] = json['rnm']
    this['ini_status']['enchant_resist']
    if 'rna' in json:
        this['ini_status']['enchant_resist']['notatk'] = json['rna']
    this['ini_status']['enchant_resist']
    if 'rzo' in json:
        this['ini_status']['enchant_resist']['zombie'] = json['rzo']
    this['ini_status']['enchant_resist']
    if 'rde' in json:
        this['ini_status']['enchant_resist']['death'] = json['rde']
    this['ini_status']['enchant_resist']
    if 'rkn' in json:
        this['ini_status']['enchant_resist']['knockback'] = json['rkn']
    this['ini_status']['enchant_resist']
    if 'rdf' in json:
        this['ini_status']['enchant_resist']['resist_debuff'] = json['rdf']
    this['ini_status']['enchant_resist']
    if 'rbe' in json:
        this['ini_status']['enchant_resist']['berserk'] = json['rbe']
    this['ini_status']['enchant_resist']
    if 'rcs' in json:
        this['ini_status']['enchant_resist']['stop'] = json['rcs']
    this['ini_status']['enchant_resist']
    if 'rcu' in json:
        this['ini_status']['enchant_resist']['fast'] = json['rcu']
    this['ini_status']['enchant_resist']
    if 'rcd' in json:
        this['ini_status']['enchant_resist']['slow'] = json['rcd']
    this['ini_status']['enchant_resist']
    if 'rdo' in json:
        this['ini_status']['enchant_resist']['donsoku'] = json['rdo']
    this['ini_status']['enchant_resist']
    if 'rra' in json:
        this['ini_status']['enchant_resist']['rage'] = json['rra']
    this['ini_status']['enchant_resist']
    if 'rsa' in json:
        this['ini_status']['enchant_resist']['single_attack'] = json['rsa']
    this['ini_status']['enchant_resist']
    if 'raa' in json:
        this['ini_status']['enchant_resist']['area_attack'] = json['raa']
    this['ini_status']['enchant_resist']
    if 'rdc' in json:
        this['ini_status']['enchant_resist']['dec_ct'] = json['rdc']
    this['ini_status']['enchant_resist']
    if 'ric' in json:
        this['ini_status']['enchant_resist']['inc_ct'] = json['ric']
    this['']
    this['max_status']
    this['max_status']['param']
    if 'mhp' in json:
        this['max_status']['param']['hp'] = json['mhp']
    this['max_status']['param']
    if 'mmp' in json:
        this['max_status']['param']['mp'] = json['mmp']
    this['max_status']['param']
    if 'matk' in json:
        this['max_status']['param']['atk'] = json['matk']
    this['max_status']['param']
    if 'mdef' in json:
        this['max_status']['param']['def'] = json['mdef']
    this['max_status']['param']
    if 'mmag' in json:
        this['max_status']['param']['mag'] = json['mmag']
    this['max_status']['param']
    if 'mmnd' in json:
        this['max_status']['param']['mnd'] = json['mmnd']
    this['max_status']['param']
    if 'mdex' in json:
        this['max_status']['param']['dex'] = json['mdex']
    this['max_status']['param']
    if 'mspd' in json:
        this['max_status']['param']['spd'] = json['mspd']
    this['max_status']['param']
    if 'mcri' in json:
        this['max_status']['param']['cri'] = json['mcri']
    this['max_status']['param']
    if 'mluk' in json:
        this['max_status']['param']['luk'] = json['mluk']
    this['max_status']
    this['max_status']['enchant_resist']
    if 'mrpo' in json:
        this['max_status']['enchant_resist']['poison'] = json['mrpo']
    this['max_status']['enchant_resist']
    if 'mrpa' in json:
        this['max_status']['enchant_resist']['paralyse'] = json['mrpa']
    this['max_status']['enchant_resist']
    if 'mrst' in json:
        this['max_status']['enchant_resist']['stun'] = json['mrst']
    this['max_status']['enchant_resist']
    if 'mrsl' in json:
        this['max_status']['enchant_resist']['sleep'] = json['mrsl']
    this['max_status']['enchant_resist']
    if 'mrch' in json:
        this['max_status']['enchant_resist']['charm'] = json['mrch']
    this['max_status']['enchant_resist']
    if 'mrsn' in json:
        this['max_status']['enchant_resist']['stone'] = json['mrsn']
    this['max_status']['enchant_resist']
    if 'mrbl' in json:
        this['max_status']['enchant_resist']['blind'] = json['mrbl']
    this['max_status']['enchant_resist']
    if 'mrns' in json:
        this['max_status']['enchant_resist']['notskl'] = json['mrns']
    this['max_status']['enchant_resist']
    if 'mrnm' in json:
        this['max_status']['enchant_resist']['notmov'] = json['mrnm']
    this['max_status']['enchant_resist']
    if 'mrna' in json:
        this['max_status']['enchant_resist']['notatk'] = json['mrna']
    this['max_status']['enchant_resist']
    if 'mrzo' in json:
        this['max_status']['enchant_resist']['zombie'] = json['mrzo']
    this['max_status']['enchant_resist']
    if 'mrde' in json:
        this['max_status']['enchant_resist']['death'] = json['mrde']
    this['max_status']['enchant_resist']
    if 'mrkn' in json:
        this['max_status']['enchant_resist']['knockback'] = json['mrkn']
    this['max_status']['enchant_resist']
    if 'mrdf' in json:
        this['max_status']['enchant_resist']['resist_debuff'] = json['mrdf']
    this['max_status']['enchant_resist']
    if 'mrbe' in json:
        this['max_status']['enchant_resist']['berserk'] = json['mrbe']
    this['max_status']['enchant_resist']
    if 'mrcs' in json:
        this['max_status']['enchant_resist']['stop'] = json['mrcs']
    this['max_status']['enchant_resist']
    if 'mrcu' in json:
        this['max_status']['enchant_resist']['fast'] = json['mrcu']
    this['max_status']['enchant_resist']
    if 'mrcd' in json:
        this['max_status']['enchant_resist']['slow'] = json['mrcd']
    this['max_status']['enchant_resist']
    if 'mrdo' in json:
        this['max_status']['enchant_resist']['donsoku'] = json['mrdo']
    this['max_status']['enchant_resist']
    if 'mrra' in json:
        this['max_status']['enchant_resist']['rage'] = json['mrra']
    this['max_status']['enchant_resist']
    if 'mrsa' in json:
        this['max_status']['enchant_resist']['single_attack'] = json['mrsa']
    this['max_status']['enchant_resist']
    if 'mraa' in json:
        this['max_status']['enchant_resist']['area_attack'] = json['mraa']
    this['max_status']['enchant_resist']
    if 'mrdc' in json:
        this['max_status']['enchant_resist']['dec_ct'] = json['mrdc']
    this['max_status']['enchant_resist']
    if 'mric' in json:
        this['max_status']['enchant_resist']['inc_ct'] = json['mric']
    if 'ls1' in json:
        this['leader_skills'][0] = json['ls1']
    if 'ls2' in json:
        this['leader_skills'][1] = json['ls2']
    if 'ls3' in json:
        this['leader_skills'][2] = json['ls3']
    if 'ls4' in json:
        this['leader_skills'][3] = json['ls4']
    if 'ls5' in json:
        this['leader_skills'][4] = json['ls5']
    if 'ls6' in json:
        this['leader_skills'][5] = json['ls6']
    if 'recipe1' in json:
        this['recipes'][0] = json['recipe1']
    if 'recipe2' in json:
        this['recipes'][1] = json['recipe2']
    if 'recipe3' in json:
        this['recipes'][2] = json['recipe3']
    if 'recipe4' in json:
        this['recipes'][3] = json['recipe4']
    if 'recipe5' in json:
        this['recipes'][4] = json['recipe5']
    if 'recipe6' in json:
        this['recipes'][5] = json['recipe6']
    if 'img' in json:
        this['image'] = json['img']
    if 'vce' in json:
        this['voice'] = json['vce']
    #this.flag.Set(2,json.no_trw==0)
    #this.flag.Set(3,json.no_kb==0)
    if 'unlck_t' in json:
        this['unlock_time'] = json['unlck_t']
        if 'jidx' in json:
            this['job_option_index'] = newOString[json['jidx'].Length]
        if 'jidx' in json:
            this['job_option_index[index]'] = json['jidx'][index]
        if 'jimgs' in json:
            this['job_images'] = newOString[json['jimgs'].Length]
        if 'jimgs' in json:
            this['job_images[index]'] = json['jimgs'][index]
        if 'jvcs' in json:
            this['job_voices'] = newOString[json['jvcs'].Length]
        if 'jvcs' in json:
            this['job_voices[index]'] = json['jvcs'][index]
    if 'no_trw' in json:
        this['is_throw'] = (json['no_trw']==0)
    if 'no_kb' in json:
        this['is_knock_back'] = (json['no_kb']==0)
    #returntrue
return this
