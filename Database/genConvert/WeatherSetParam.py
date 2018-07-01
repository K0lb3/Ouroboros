def WeatherSetParam(json):
    this={}#WeatherSetParamjson)
    #return
    if 'iname' in json:
        this['mIname'] = json['iname']
    if 'name' in json:
        this['mName'] = json['name']
    #this.mStartWeatherIdLists.Clear()
        #foreach(stringstrinjson.st_wth)
        #this.mStartWeatherIdLists.Add(str)
    #this.mStartWeatherRateLists.Clear()
        #foreach(intnuminjson.st_rate)
        #this.mStartWeatherRateLists.Add(num)
    #if(this.mStartWeatherIdLists.Count>this.mStartWeatherRateLists.Count)
        #this.mStartWeatherRateLists.Add(0)
    if 'ch_cl_min' in json:
        this['mChangeClockMin'] = json['ch_cl_min']
    if 'ch_cl_max' in json:
        this['mChangeClockMax'] = json['ch_cl_max']
    #if(this.mChangeClockMin>this.mChangeClockMax)
    #this.mChangeWeatherIdLists.Clear()
        #foreach(stringstrinjson.ch_wth)
        #this.mChangeWeatherIdLists.Add(str)
    #this.mChangeWeatherRateLists.Clear()
        #foreach(intnuminjson.ch_rate)
        #this.mChangeWeatherRateLists.Add(num)
    #return
    #this.mChangeWeatherRateLists.Add(0)
return this
