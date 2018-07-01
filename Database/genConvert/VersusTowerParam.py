def VersusTowerParam(json):
    this={}#VersusTowerParamjson)
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
        #Debug.LogError((object)"VersusTowerParam[WinItem]isInvalid")
        if 'winitem' in json:
            this['WinIteminame'] = newOString[json['winitem'].Length]
        if 'win_itemnum' in json:
            this['WinItemNum'] = newOInt[json['win_itemnum'].Length]
        if 'winitem' in json:
            this['WinIteminame[index]'] = json['winitem'][index]
        if 'win_itemnum' in json:
            this['WinItemNum[index]'] = json['win_itemnum'][index]
        #Debug.LogError((object)"VersusTowerParam[LoseItem]isInvalid")
        if 'joinitem' in json:
            this['JoinIteminame'] = newOString[json['joinitem'].Length]
        if 'join_itemnum' in json:
            this['JoinItemNum'] = newOInt[json['join_itemnum'].Length]
        if 'joinitem' in json:
            this['JoinIteminame[index]'] = json['joinitem'][index]
        if 'join_itemnum' in json:
            this['JoinItemNum[index]'] = json['join_itemnum'][index]
        #Debug.LogError((object)"VersusTowerParam[SpecialItem]isInvalid")
        if 'spbtl_item' in json:
            this['SpIteminame'] = newOString[json['spbtl_item'].Length]
        if 'spbtl_itemnum' in json:
            this['SpItemnum'] = newOInt[json['spbtl_itemnum'].Length]
        if 'spbtl_item' in json:
            this['SpIteminame[index]'] = json['spbtl_item'][index]
        if 'spbtl_itemnum' in json:
            this['SpItemnum[index]'] = json['spbtl_itemnum'][index]
        #Debug.LogError((object)"VersusTowerParam[SeasonItem]isInvalid")
        if 'season_item' in json:
            this['SeasonIteminame'] = newOString[json['season_item'].Length]
        if 'season_itype' in json:
            this['SeasonItemType'] = newVERSUS_ITEM_TYPE[json['season_itype'].Length]
        if 'season_itemnum' in json:
            this['SeasonItemnum'] = newOInt[json['season_itemnum'].Length]
        if 'season_item' in json:
            this['SeasonIteminame[index]'] = json['season_item'][index]
        if 'season_itype' in json:
            this['SeasonItemType[index]'] = ENUM['VERSUS_ITEM_TYPE'][json['season_itype']]
        if 'season_itemnum' in json:
            this['SeasonItemnum[index]'] = json['season_itemnum'][index]
    #if(string.IsNullOrEmpty(json.arrival_item))
    #return
    if 'arrival_item' in json:
        this['ArrivalIteminame'] = json['arrival_item']
    if 'arrival_type' in json:
        this['ArrivalItemType'] = ENUM['VERSUS_ITEM_TYPE'][json['arrival_type']]
    if 'arrival_num' in json:
        this['ArrivalItemNum'] = json['arrival_num']
return this
