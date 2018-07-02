def VersusTowerParam(json):
    this={}#VersusTowerParamjson)
    #if(json==null)
    #return
    if 'vstower_id' in json:
        this['VersusTowerID'] = json['vstower_id']
    if 'iname' in json:
        this['FloorName'] = json['iname']
    if 'floor' in json:
        this['Floor'] = json['floor']
    if 'rankup_num' in json:
        this['RankupNum'] = json['rankup_num']
    if 'win_num' in json:
        this['WinNum'] = json['win_num']
    if 'lose_num' in json:
        this['LoseNum'] = json['lose_num']
    if 'bonus_num' in json:
        this['BonusNum'] = json['bonus_num']
    if 'downfloor' in json:
        this['DownFloor'] = json['downfloor']
    if 'resetfloor' in json:
        this['ResetFloor'] = json['resetfloor']
    #if(json.winitem!=null&&json.win_itemnum!=null)
        #if(json.winitem.Length!=json.win_itemnum.Length)
        #Debug.LogError((object)"VersusTowerParam[WinItem]isInvalid")
        if 'winitem' in json:
            this['WinIteminame'] = newOString[json['winitem'].Length]
        #for(intindex=0index<json.winitem.Length++index)
        if 'winitem' in json:
            this['WinIteminame'] = json['winitem']
        #for(intindex=0index<json.win_itemnum.Length++index)
        if 'win_itemnum' in json:
            this['WinItemNum'] = json['win_itemnum']
    #if(json.joinitem!=null&&json.join_itemnum!=null)
        #if(json.joinitem.Length!=json.join_itemnum.Length)
        #Debug.LogError((object)"VersusTowerParam[LoseItem]isInvalid")
        if 'joinitem' in json:
            this['JoinIteminame'] = newOString[json['joinitem'].Length]
        #for(intindex=0index<json.joinitem.Length++index)
        if 'joinitem' in json:
            this['JoinIteminame'] = json['joinitem']
        #for(intindex=0index<json.join_itemnum.Length++index)
        if 'join_itemnum' in json:
            this['JoinItemNum'] = json['join_itemnum']
    #if(json.spbtl_item!=null&&json.spbtl_itemnum!=null)
        #if(json.spbtl_item.Length!=json.spbtl_itemnum.Length)
        #Debug.LogError((object)"VersusTowerParam[SpecialItem]isInvalid")
        if 'spbtl_item' in json:
            this['SpIteminame'] = newOString[json['spbtl_item'].Length]
        #for(intindex=0index<json.spbtl_item.Length++index)
        if 'spbtl_item' in json:
            this['SpIteminame'] = json['spbtl_item']
        #for(intindex=0index<json.spbtl_itemnum.Length++index)
        if 'spbtl_itemnum' in json:
            this['SpItemnum'] = json['spbtl_itemnum']
    #if(json.season_item!=null&&json.season_itemnum!=null&&json.season_itype!=null)
        #if(json.season_item.Length!=json.season_itemnum.Length)
        #Debug.LogError((object)"VersusTowerParam[SeasonItem]isInvalid")
        if 'season_item' in json:
            this['SeasonIteminame'] = newOString[json['season_item'].Length]
        if 'season_itype' in json:
            this['SeasonItemType'] = newVERSUS_ITEM_TYPE[json['season_itype'].Length]
        #for(intindex=0index<json.season_item.Length++index)
        if 'season_item' in json:
            this['SeasonIteminame'] = json['season_item']
        #for(intindex=0index<json.season_itype.Length++index)
        if 'season_itype' in json:
            this['SeasonItemType'] = ENUM['VERSUS_ITEM_TYPE'][json['season_itype']]
        #for(intindex=0index<json.season_itemnum.Length++index)
        if 'season_itemnum' in json:
            this['SeasonItemnum'] = json['season_itemnum']
    #if(string.IsNullOrEmpty(json.arrival_item))
    #return
    if 'arrival_item' in json:
        this['ArrivalIteminame'] = json['arrival_item']
    if 'arrival_type' in json:
        this['ArrivalItemType'] = ENUM['VERSUS_ITEM_TYPE'][json['arrival_type']]
    if 'arrival_num' in json:
        this['ArrivalItemNum'] = json['arrival_num']
return this
