def WeatherSetParam(json):
    this={}#WeatherSetParamjson)
    #if(json==null)
    #return
    if 'iname' in json:
        this['mIname'] = json['iname']
    if 'name' in json:
        this['mName'] = json['name']
    #this.mStartWeatherIdLists.Clear()
    #if(json.st_wth!=null)
        #foreach(stringstrinjson.st_wth)
        #this.mStartWeatherIdLists.Add(str)
    #this.mStartWeatherRateLists.Clear()
    #if(json.st_rate!=null)
        #foreach(intnuminjson.st_rate)
        #this.mStartWeatherRateLists.Add(num)
    #if(this.mStartWeatherIdLists.Count>this.mStartWeatherRateLists.Count)
        #for(intindex=0index<this.mStartWeatherIdLists.Count-this.mStartWeatherRateLists.Count++index)
        #this.mStartWeatherRateLists.Add(0)
    if 'ch_cl_min' in json:
        this['mChangeClockMin'] = json['ch_cl_min']
    if 'ch_cl_max' in json:
        this['mChangeClockMax'] = json['ch_cl_max']
    #if(this.mChangeClockMin>this.mChangeClockMax)
    #this.mChangeClockMax=this.mChangeClockMin
    #this.mChangeWeatherIdLists.Clear()
    #if(json.ch_wth!=null)
        #foreach(stringstrinjson.ch_wth)
        #this.mChangeWeatherIdLists.Add(str)
    #this.mChangeWeatherRateLists.Clear()
    #if(json.ch_rate!=null)
        #foreach(intnuminjson.ch_rate)
        #this.mChangeWeatherRateLists.Add(num)
    #if(this.mChangeWeatherIdLists.Count<=this.mChangeWeatherRateLists.Count)
    #return
    #for(intindex=0index<this.mChangeWeatherIdLists.Count-this.mChangeWeatherRateLists.Count++index)
    #this.mChangeWeatherRateLists.Add(0)
return this
