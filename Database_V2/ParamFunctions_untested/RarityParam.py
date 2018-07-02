def RarityParam(json):
    this={}#RarityParamjson)
    #if(json==null)
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
    #if(this.EquipEnhanceParam==null)
    #this.EquipEnhanceParam=newRarityEquipEnhanceParam()
    #intlength=json.eq_enhcap+1
    #this.EquipEnhanceParam.rankcap=(OInt)length
    this['']
    this['EquipEnhanceParam']
    if 'eq_costscale' in json:
        this['EquipEnhanceParam']['cost_scale'] = json['eq_costscale']
    #this.EquipEnhanceParam.ranks=(RarityEquipEnhanceParam.RankParam)null
    #if(length>0)
        #if(json.eq_points==null||json.eq_num1==null||(json.eq_num2==null||json.eq_num3==null))
        #returnfalse
        #this.EquipEnhanceParam.ranks=newRarityEquipEnhanceParam.RankParam[length]
        #for(intindex=0index<length++index)
            #this.EquipEnhanceParam.ranks=newRarityEquipEnhanceParam.RankParam()
            this['EquipEnhanceParam']
            this['EquipEnhanceParam']['ranks']
            if 'eq_points' in json:
                this['EquipEnhanceParam']['ranks']['need_point'] = json['eq_points']
        #stringstrArray=newstring[3]
            #json.eq_item1,
            #json.eq_item2,
            #json.eq_item3
            #}
            #intnumArray=newint[3]
                #json.eq_num1,
                #json.eq_num2,
                #json.eq_num3
                #}
                #for(intindex1=0index1<strArray.Length++index1)
                    #for(intindex2=0index2<length++index2)
                        #this.EquipEnhanceParam.ranks[index2].return_item[index1]=newReturnItem()
                        #this.EquipEnhanceParam.ranks[index2].return_item[index1].iname=strArray[index1]
                        #this.EquipEnhanceParam.ranks[index2].return_item[index1].num=(OInt)numArray[index1][index2]
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
            if 'card_lvcap' in json:
                this['ConceptCardLvCap'] = json['card_lvcap']
            if 'card_awake_count' in json:
                this['ConceptCardAwakeCountMax'] = json['card_awake_count']
            #returntrue
return this
