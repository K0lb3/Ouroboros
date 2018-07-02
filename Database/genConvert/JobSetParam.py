def JobSetParam(json):
    this={}#JobSetParamjson)
    #if(json==null)
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
    #this.lock_jobs=(JobSetParam.JobLock)null
    #intlength=0
    #if(!string.IsNullOrEmpty(json.ljob1))
    #++length
    #if(!string.IsNullOrEmpty(json.ljob2))
    #++length
    #if(!string.IsNullOrEmpty(json.ljob3))
    #++length
    #if(length>0)
        #this.lock_jobs=newJobSetParam.JobLock[length]
        #intindex=0
        #if(!string.IsNullOrEmpty(json.ljob1))
            #this.lock_jobs=newJobSetParam.JobLock()
            this['']
            this['lock_jobs']
            if 'ljob1' in json:
                this['lock_jobs']['iname'] = json['ljob1']
            this['lock_jobs']
            if 'llv1' in json:
                this['lock_jobs']['lv'] = json['llv1']
            #++index
        #if(!string.IsNullOrEmpty(json.ljob2))
            #this.lock_jobs=newJobSetParam.JobLock()
            if 'ljob2' in json:
                this['lock_jobs']['iname'] = json['ljob2']
            if 'llv2' in json:
                this['lock_jobs']['lv'] = json['llv2']
            #++index
        #if(!string.IsNullOrEmpty(json.ljob3))
            #this.lock_jobs=newJobSetParam.JobLock()
            if 'ljob3' in json:
                this['lock_jobs']['iname'] = json['ljob3']
            if 'llv3' in json:
                this['lock_jobs']['lv'] = json['llv3']
            #intnum=index+1
    #returntrue
return this
