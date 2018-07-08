def WeatherSetParam(json):
    this={}#WeatherSetParamjson)
    if 'iname' in json:
        this['mIname'] = json['iname']
    if 'name' in json:
        this['mName'] = json['name']

    if('st_wth' in json and len(json['st_wth'])!=0):
        this['mStartWeatherIdLists']=json['st_wth']
    if('st_rate' in json and len(json['st_rate'])!=0):
        this['mStartWeatherRateLists']=json['st_rate']
    for i in range(0,   len(json['st_wth']) - len(json['st_rate']) ):
        this['mStartWeatherRateLists'].append(0)

    this['mChangeClockMin'] = json['ch_cl_min'] if 'ch_cl_min' in json else 0
    this['mChangeClockMax'] = json['ch_cl_max'] if 'ch_cl_max' in json else 0
    if this['mChangeClockMin'] > this['mChangeClockMax']:
        this['mChangeClockMax'] = this['mChangeClockMin']


    if('ch_wth' in json and len(json['ch_wth'])!=0):
        this['mChangeWeatherIdLists']=json['ch_wth']
    if('ch_rate' in json and len(json['ch_rate'])!=0):
        this['mChangeWeatherRateLists']=json['ch_rate']
    if 'ch_wth' in json and 'ch_rate' in json:
        for i in range(0,   len(json['ch_wth']) - len(json['ch_rate']) ):
            this['mChangeWeatherRateLists'].append(0)
    return this
