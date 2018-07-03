from ParamFunctions._variables import RAWBIRTH,RAWELEMENT,ENUM,TRANSLATION,TAG_ARTIFACT
def ArtifactParam(json):
    this={}#ArtifactParamjson)
    #if(json==null)
    #returnfalse
    if 'iname' in json:
        this['iname'] = json['iname']
    if json['iname'] in TRANSLATION:
        loc = TRANSLATION[json['iname']]
        this['name'] = loc['name']
        this['kanji'] = json['name']
        this['expr'] = loc['expr']
        this['flavor'] = loc['flavor']
        this['spec'] = loc['spec']
    else:
        if 'name' in json:
            this['name'] = json['name']
            this['kanji'] = json['name']
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
        this['tag'] = TAG_ARTIFACT[json['tag']]
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
    if 'skills' in json:
        this['skills'] = json['skills']

    this['equip_effects']=[None]*5
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

    this['attack_effects']=[None]*5
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
    #abilities
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
        this['condition_birth'] = RAWBIRTH[json['birth']]
    if 'elem' in json:
        this['condition_element'] = RAWELEMENT[json['elem']]
    if 'eqrmin' in json:
        this['condition_raremin'] = json['eqrmin']
    if 'eqrmax' in json:
        this['condition_raremax'] = json['eqrmax']
    if 'units' in json:
        this['condition_units'] = json['units']

    if 'jobs' in json:
        this['condition_jobs'] = []
        for job in json['jobs']:
            this['condition_jobs'].append(job.replace('__HIDE__',''))

    return this
