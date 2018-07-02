def CustomTargetParam(json):
    this={}#CustomTargetParamjson)
    if 'iname' in json:
        this['iname'] = json['iname']
    #if(json.units!=null)
        #for(intindex=0index<json.units.Length++index)
        if 'units' in json:
            this['units'] = json['units']
    #if(json.jobs!=null)
        #for(intindex=0index<json.jobs.Length++index)
        if 'jobs' in json:
            this['jobs'] = json['jobs']
    #if(json.unit_groups!=null)
        #for(intindex=0index<json.unit_groups.Length++index)
        if 'unit_groups' in json:
            this['unit_groups'] = json['unit_groups']
    #if(json.job_groups!=null)
        #for(intindex=0index<json.job_groups.Length++index)
        if 'job_groups' in json:
            this['job_groups'] = json['job_groups']
    if 'first_job' in json:
        this['first_job'] = json['first_job']
    if 'second_job' in json:
        this['second_job'] = json['second_job']
    if 'third_job' in json:
        this['third_job'] = json['third_job']
    if 'sex' in json:
        this['sex'] = ENUM['ESex'][json['sex']]
    if 'birth_id' in json:
        this['birth_id'] = json['birth_id']
    #stringstrArray=newstring[6]
        #json.dark.ToString(),
        #json.shine.ToString(),
        #json.thunder.ToString(),
        #json.wind.ToString(),
        #json.water.ToString(),
        #json.fire.ToString()
        #}
        #stringempty=string.Empty
        #foreach(stringstrinstrArray)
        #empty+=str
        #this.element=Convert.ToInt32(empty,2)
        #returntrue
return this
