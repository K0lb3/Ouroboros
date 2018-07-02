def ArtifactParam(json):
    this={}#ArtifactParamjson)
    #if(json==null)
    #returnfalse
    if 'iname' in json:
        this['iname'] = json['iname']
    if 'name' in json:
        this['name'] = json['name']
    if 'expr' in json:
        this['expr'] = json['expr']
    if 'flavor' in json:
        this['flavor'] = json['flavor']
    if 'spec' in json:
        this['spec'] = json['spec']
    if 'asset' in json:
        this['asset'] = json['asset']
    if 'voice' in json:
        this['voice'] = json['voice']
    if 'icon' in json:
        this['icon'] = json['icon']
    if 'tag' in json:
        this['tag'] = json['tag']
    if 'type' in json:
        this['type'] = ENUM['ArtifactTypes'][json['type']]
    if 'rini' in json:
        this['rareini'] = json['rini']
    if 'rmax' in json:
        this['raremax'] = json['rmax']
    if 'kakera' in json:
        this['kakera'] = json['kakera']
    if 'maxnum' in json:
        this['maxnum'] = json['maxnum']
    if 'notsmn' in json:
        this['is_create'] = json['notsmn']==0
    #this.skills=(string)null
    #if(json.skills!=null)
        #for(intindex=0index<json.skills.Length++index)
        if 'skills' in json:
            this['skills'] = json['skills']
    #Array.Clear((Array)this.equip_effects,0,this.equip_effects.Length)
    if 'equip1' in json:
        this['equip_effects'][0] = json['equip1']
    if 'equip2' in json:
        this['equip_effects'][1] = json['equip2']
    if 'equip3' in json:
        this['equip_effects'][2] = json['equip3']
    if 'equip4' in json:
        this['equip_effects'][3] = json['equip4']
    if 'equip5' in json:
        this['equip_effects'][4] = json['equip5']
    #Array.Clear((Array)this.attack_effects,0,this.attack_effects.Length)
    if 'attack1' in json:
        this['attack_effects'][0] = json['attack1']
    if 'attack2' in json:
        this['attack_effects'][1] = json['attack2']
    if 'attack3' in json:
        this['attack_effects'][2] = json['attack3']
    if 'attack4' in json:
        this['attack_effects'][3] = json['attack4']
    if 'attack5' in json:
        this['attack_effects'][4] = json['attack5']
    #this.abil_inames=(string)null
    #this.abil_levels=(int)null
    #this.abil_rareties=(int)null
    #this.abil_shows=(int)null
    #this.abil_conds=(string)null
    #if(json.abils!=null&&json.ablvs!=null&&(json.abrares!=null&&json.abshows!=null)&&(json.abconds!=null&&json.abils.Length==json.ablvs.Length&&(json.abils.Length==json.abrares.Length&&json.abils.Length==json.abshows.Length))&&json.abils.Length==json.abconds.Length)
        if 'abils' in json:
            this['abil_inames'] = newstring[json['abils'].Length]
        if 'ablvs' in json:
            this['abil_levels'] = newint[json['ablvs'].Length]
        if 'abrares' in json:
            this['abil_rareties'] = newint[json['abrares'].Length]
        if 'abshows' in json:
            this['abil_shows'] = newint[json['abshows'].Length]
        if 'abconds' in json:
            this['abil_conds'] = newstring[json['abconds'].Length]
        #for(intindex=0index<json.ablvs.Length++index)
            if 'abils' in json:
                this['abil_inames'] = json['abils']
            if 'ablvs' in json:
                this['abil_levels'] = json['ablvs']
            if 'abrares' in json:
                this['abil_rareties'] = json['abrares']
            if 'abshows' in json:
                this['abil_shows'] = json['abshows']
            if 'abconds' in json:
                this['abil_conds'] = json['abconds']
    if 'kc' in json:
        this['kcoin'] = json['kc']
    if 'tc' in json:
        this['tcoin'] = json['tc']
    if 'ac' in json:
        this['acoin'] = json['ac']
    if 'mc' in json:
        this['mcoin'] = json['mc']
    if 'pp' in json:
        this['pcoin'] = json['pp']
    if 'buy' in json:
        this['buy'] = json['buy']
    if 'sell' in json:
        this['sell'] = json['sell']
    if 'ecost' in json:
        this['enhance_cost'] = json['ecost']
    if 'eqlv' in json:
        this['condition_lv'] = json['eqlv']
    if 'sex' in json:
        this['condition_sex'] = ENUM['ESex'][json['sex']]
    if 'birth' in json:
        this['condition_birth'] = json['birth']
    if 'elem' in json:
        this['condition_element'] = ENUM['EElement'][json['elem']]
    #this.condition_units=(string)null
    #this.condition_jobs=(string)null
    if 'eqrmin' in json:
        this['condition_raremin'] = json['eqrmin']
    if 'eqrmax' in json:
        this['condition_raremax'] = json['eqrmax']
    #if(json.units!=null&&json.units.Length>0)
        #for(intindex=0index<json.units.Length++index)
        if 'units' in json:
            this['condition_units'] = json['units']
    #if(json.jobs!=null&&json.jobs.Length>0)
        #for(intindex=0index<json.jobs.Length++index)
        if 'jobs' in json:
            this['condition_jobs'] = json['jobs']
    #returntrue
return this
