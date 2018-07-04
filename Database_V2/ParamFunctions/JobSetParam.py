def JobSetParam(json):
    this={}#JobSetParamjson)
    if 'iname' in json:
        this['iname'] = json['iname']
    if 'job' in json:
        this['job'] = json['job']
    if 'cjob' in json:
        this['jobchange'] = json['cjob']
    if 'target_unit' in json:
        this['target_unit'] = json['target_unit']
    if 'lrare' in json:
        this['lock_rarity'] = json['lrare']
    if 'lplus' in json:
        this['lock_awakelv'] = json['lplus']

    this['lock_jobs']={}
    for i in range(1,4):
        if 'ljob'+str(i) in json:
            this['lock_jobs']['iname'] = json['ljob'+str(i)]
        if 'llv'+str(i) in json:
            this['lock_jobs']['lv'] = json['llv'+str(i)]
    return this
