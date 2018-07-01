def RarityParam(json):
    this={}#RarityParamjson)
    #returnfalse
    if 'unitcap' in json:
        this['UnitLvCap'] = json['unitcap']
    if 'jobcap' in json:
        this['UnitJobLvCap'] = json['jobcap']
    if 'awakecap' in json:
        this['UnitAwakeLvCap'] = json['awakecap']
    if 'piece' in json:
        this['UnitUnlockPieceNum'] = json['piece']
    if 'ch_piece' in json:
        this['UnitChangePieceNum'] = json['ch_piece']
    if 'ch_piece_select' in json:
        this['UnitSelectChangePieceNum'] = json['ch_piece_select']
    if 'rareup_cost' in json:
        this['UnitRarityUpCost'] = json['rareup_cost']
    if 'gain_pp' in json:
        this['PieceToPoint'] = json['gain_pp']
    this['']
    this['EquipEnhanceParam']
    if 'eq_costscale' in json:
        this['EquipEnhanceParam']['cost_scale'] = json['eq_costscale']
    #if(length>0)
        #returnfalse
            this['EquipEnhanceParam']
            this['EquipEnhanceParam']['ranks[index]']
            if 'eq_points' in json:
                this['EquipEnhanceParam']['ranks[index]']['need_point'] = json['eq_points'][index]
    if 'af_lvcap' in json:
        this['ArtifactLvCap'] = json['af_lvcap']
    if 'af_upcost' in json:
        this['ArtifactCostRate'] = json['af_upcost']
    if 'af_unlock' in json:
        this['ArtifactCreatePieceNum'] = json['af_unlock']
    if 'af_gousei' in json:
        this['ArtifactGouseiPieceNum'] = json['af_gousei']
    if 'af_change' in json:
        this['ArtifactChangePieceNum'] = json['af_change']
    if 'af_unlock_cost' in json:
        this['ArtifactCreateCost'] = json['af_unlock_cost']
    if 'af_gousei_cost' in json:
        this['ArtifactRarityUpCost'] = json['af_gousei_cost']
    if 'af_change_cost' in json:
        this['ArtifactChangeCost'] = json['af_change_cost']
    this['']
    this['GrowStatus']
    if 'hp' in json:
        this['GrowStatus']['hp'] = json['hp']
    this['GrowStatus']
    if 'mp' in json:
        this['GrowStatus']['mp'] = json['mp']
    this['GrowStatus']
    if 'atk' in json:
        this['GrowStatus']['atk'] = json['atk']
    this['GrowStatus']
    if 'def' in json:
        this['GrowStatus']['def'] = json['def']
    this['GrowStatus']
    if 'mag' in json:
        this['GrowStatus']['mag'] = json['mag']
    this['GrowStatus']
    if 'mnd' in json:
        this['GrowStatus']['mnd'] = json['mnd']
    this['GrowStatus']
    if 'dex' in json:
        this['GrowStatus']['dex'] = json['dex']
    this['GrowStatus']
    if 'spd' in json:
        this['GrowStatus']['spd'] = json['spd']
    this['GrowStatus']
    if 'cri' in json:
        this['GrowStatus']['cri'] = json['cri']
    this['GrowStatus']
    if 'luk' in json:
        this['GrowStatus']['luk'] = json['luk']
    if 'drop' in json:
        this['DropSE'] = json['drop']
    #returntrue
return this
