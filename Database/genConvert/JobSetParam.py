def JobSetParam(json):
    this={}#JobSetParamjson)
    #returnfalse
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
    #if(!string.IsNullOrEmpty(json.ljob1))
    #++length
    #if(!string.IsNullOrEmpty(json.ljob2))
    #++length
    #if(!string.IsNullOrEmpty(json.ljob3))
    #++length
    #if(length>0)
        #if(!string.IsNullOrEmpty(json.ljob1))
            this['']
            this['lock_jobs[index]']
            if 'ljob1' in json:
                this['lock_jobs[index]']['iname'] = json['ljob1']
            this['lock_jobs[index]']
            if 'llv1' in json:
                this['lock_jobs[index]']['lv'] = json['llv1']
            #++index
        #if(!string.IsNullOrEmpty(json.ljob2))
            if 'ljob2' in json:
                this['lock_jobs[index]']['iname'] = json['ljob2']
            if 'llv2' in json:
                this['lock_jobs[index]']['lv'] = json['llv2']
            #++index
        #if(!string.IsNullOrEmpty(json.ljob3))
            if 'ljob3' in json:
                this['lock_jobs[index]']['iname'] = json['ljob3']
            if 'llv3' in json:
                this['lock_jobs[index]']['lv'] = json['llv3']
    #returntrue
return this
