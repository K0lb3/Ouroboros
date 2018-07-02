def AppealChargeParam_(json):
    this={}#AppealChargeParam_json)
    #if(_json==null)
    #thrownewInvalidJSONException()
    if 'fields' in json:
        this['m_AppealId'] = _json['fields'].appeal_id
    if 'fields' in json:
        this['m_BeforeImg'] = _json['fields'].before_img_id
    if 'fields' in json:
        this['m_AfterImg'] = _json['fields'].after_img_id
    #try
        #if(!string.IsNullOrEmpty(_json.fields.start_at))
        if 'fields' in json:
            this['m_StartAt'] = TimeManager.GetUnixSec)
        #if(string.IsNullOrEmpty(_json.fields.end_at))
        #return
        if 'fields' in json:
            this['m_EndAt'] = TimeManager.GetUnixSec)
    #catch(Exceptionex)
        #DebugUtility.LogError(ex.ToString())
return this
