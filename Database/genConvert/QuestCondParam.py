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
    #intnum1=0
    #this.elem=newint[Enum.GetValues(typeof(EElement)).Length]
    if 'el_none' in json:
        this['isElemLimit'] = num1++(this.elem[1]=json.el_fire)+(this.elem[2]=json.el_watr)+(this.elem[3]=json.el_wind)+(this.elem[4]=json.el_thdr)+(this.elem[5]=json.el_lit)+(this.elem[6]=json.el_drk)>0
    #intnum2=0
    #this.jobset=newint[4]
    #intjobset1=this.jobset
    #intindex1=num2
    #intnum3=index1+1
    #intjobset1_1=json.jobset1
    #jobset1[index1]=jobset1_1
    #intjobset2=this.jobset
    #intindex2=num3
    #intnum4=index2+1
    #intjobset2_1=json.jobset2
    #jobset2[index2]=jobset2_1
    #intjobset3=this.jobset
    #intindex3=num4
    #intnum5=index3+1
    #intjobset3_1=json.jobset3
    #jobset3[index3]=jobset3_1
    #if(json.job!=null)
        if 'job' in json:
            this['job'] = newstring[json['job'].Length]
        #for(intindex4=0index4<this.job.Length++index4)
        if 'job' in json:
            this['job'][4] = json['job'][index4]
    #if(json.unit!=null)
        if 'unit' in json:
            this['unit'] = newstring[json['unit'].Length]
        #for(intindex4=0index4<this.unit.Length++index4)
        if 'unit' in json:
            this['unit'][4] = json['unit'][index4]
    #if(json.birth!=null)
        if 'birth' in json:
            this['birth'] = newstring[json['birth'].Length]
        #for(intindex4=0index4<this.birth.Length++index4)
        if 'birth' in json:
            this['birth'][4] = json['birth'][index4]
    if 'party_type' in json:
        this['party_type'] = !Enum.IsDefined,(object)json['party_type'])?PartyCondType.None:(PartyCondType)json['party_type']
    #returntrue
return this
