def VipParam(json):
    this={}#VipParamjson)
    #if(json==null)
    #returnfalse
    if 'exp' in json:
        this['NextRankNeedPoint'] = json['exp']
    if 'ticket' in json:
        this['Ticket'] = json['ticket']
    if 'buy_coin_bonus' in json:
        this['BuyCoinBonus'] = json['buy_coin_bonus']
    if 'buy_coin_num' in json:
        this['BuyCoinNum'] = json['buy_coin_num']
    if 'buy_stmn_num' in json:
        this['BuyStaminaNum'] = json['buy_stmn_num']
    if 'reset_elite' in json:
        this['ResetEliteNum'] = json['reset_elite']
    if 'reset_arena' in json:
        this['ResetArenaNum'] = json['reset_arena']
    #returntrue
return this
