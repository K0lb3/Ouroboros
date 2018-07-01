def MasterParam(json):
    this={}#MasterParamjson)
    #if(this.Loaded)
    #returntrue
    #DebugUtility.Verify((object)json,typeof(JSON_MasterParam))
    #this.mFixParam.Deserialize(json.Fix[0])
        if 'Unit' in json:
            this['mUnitParam'] = newList<UnitParam>
        if 'Unit' in json:
            this['mUnitDictionary'] = newDictionary<string,UnitParam>
            #//ISSUE:objectofacompiler-generatedtypeiscreated
            #//ISSUE:variableofacompiler-generatedtype
            #//ISSUE:referencetoacompiler-generatedfield
            #//ISSUE:referencetoacompiler-generatedmethod
                #this.mUnitParam.Add(unitParam)
            #//ISSUE:referencetoacompiler-generatedfield
            #unitParam.Deserialize(deserializeCAnonStorey21B.data)
            #//ISSUE:referencetoacompiler-generatedfield
            #if(this.mUnitDictionary.ContainsKey(deserializeCAnonStorey21B.data.iname))
                #//ISSUE:referencetoacompiler-generatedfield
                #thrownewException("重複エラー：Unit["+deserializeCAnonStorey21B.data.iname+"]")
            #//ISSUE:referencetoacompiler-generatedfield
            #this.mUnitDictionary.Add(deserializeCAnonStorey21B.data.iname,unitParam)
        if 'Skill' in json:
            this['mSkillParam'] = newList<SkillParam>
        if 'Skill' in json:
            this['mSkillDictionary'] = newDictionary<string,SkillParam>
            #//ISSUE:objectofacompiler-generatedtypeiscreated
            #//ISSUE:variableofacompiler-generatedtype
            #//ISSUE:referencetoacompiler-generatedfield
            #//ISSUE:referencetoacompiler-generatedmethod
                #this.mSkillParam.Add(skillParam)
            #//ISSUE:referencetoacompiler-generatedfield
            #skillParam.Deserialize(deserializeCAnonStorey21C.data)
            #//ISSUE:referencetoacompiler-generatedfield
            #if(this.mSkillDictionary.ContainsKey(deserializeCAnonStorey21C.data.iname))
                #//ISSUE:referencetoacompiler-generatedfield
                #thrownewException("重複エラー：Skill["+deserializeCAnonStorey21C.data.iname+"]")
            #//ISSUE:referencetoacompiler-generatedfield
            #this.mSkillDictionary.Add(deserializeCAnonStorey21C.data.iname,skillParam)
        #SkillParam.UpdateReplaceSkill(this.mSkillParam)
        if 'Buff' in json:
            this['mBuffEffectParam'] = newList<BuffEffectParam>
            #//ISSUE:objectofacompiler-generatedtypeiscreated
            #//ISSUE:variableofacompiler-generatedtype
            #//ISSUE:referencetoacompiler-generatedfield
            #//ISSUE:referencetoacompiler-generatedmethod
                #this.mBuffEffectParam.Add(buffEffectParam)
            #//ISSUE:referencetoacompiler-generatedfield
            #buffEffectParam.Deserialize(deserializeCAnonStorey21D.data)
        if 'Cond' in json:
            this['mCondEffectParam'] = newList<CondEffectParam>
            #//ISSUE:objectofacompiler-generatedtypeiscreated
            #//ISSUE:variableofacompiler-generatedtype
            #//ISSUE:referencetoacompiler-generatedfield
            #//ISSUE:referencetoacompiler-generatedmethod
                #this.mCondEffectParam.Add(condEffectParam)
            #//ISSUE:referencetoacompiler-generatedfield
            #condEffectParam.Deserialize(deserializeCAnonStorey21E.data)
        if 'Ability' in json:
            this['mAbilityParam'] = newList<AbilityParam>
        if 'Ability' in json:
            this['mAbilityDictionary'] = newDictionary<string,AbilityParam>
            #//ISSUE:objectofacompiler-generatedtypeiscreated
            #//ISSUE:variableofacompiler-generatedtype
            #//ISSUE:referencetoacompiler-generatedfield
            #//ISSUE:referencetoacompiler-generatedmethod
                #this.mAbilityParam.Add(abilityParam)
            #//ISSUE:referencetoacompiler-generatedfield
            #abilityParam.Deserialize(deserializeCAnonStorey21F.data)
            #//ISSUE:referencetoacompiler-generatedfield
            #if(this.mAbilityDictionary.ContainsKey(deserializeCAnonStorey21F.data.iname))
                #//ISSUE:referencetoacompiler-generatedfield
                #thrownewException("重複エラー：Ability["+deserializeCAnonStorey21F.data.iname+"]")
            #//ISSUE:referencetoacompiler-generatedfield
            #this.mAbilityDictionary.Add(deserializeCAnonStorey21F.data.iname,abilityParam)
        if 'Item' in json:
            this['mItemParam'] = newList<ItemParam>
        if 'Item' in json:
            this['mItemDictionary'] = newDictionary<string,ItemParam>
            #//ISSUE:objectofacompiler-generatedtypeiscreated
            #//ISSUE:variableofacompiler-generatedtype
            #//ISSUE:referencetoacompiler-generatedfield
            #//ISSUE:referencetoacompiler-generatedmethod
                #this.mItemParam.Add(itemParam)
            #//ISSUE:referencetoacompiler-generatedfield
            #itemParam.Deserialize(deserializeCAnonStorey220.data)
            #//ISSUE:referencetoacompiler-generatedfield
            #if(this.mItemDictionary.ContainsKey(deserializeCAnonStorey220.data.iname))
                #//ISSUE:referencetoacompiler-generatedfield
                #thrownewException("重複エラー：Item["+deserializeCAnonStorey220.data.iname+"]")
            #//ISSUE:referencetoacompiler-generatedfield
            #this.mItemDictionary.Add(deserializeCAnonStorey220.data.iname,itemParam)
        #this.AddUnitToItem()
        if 'Artifact' in json:
            this['mArtifactParam'] = newList<ArtifactParam>
        if 'Artifact' in json:
            this['mArtifactDictionary'] = newDictionary<string,ArtifactParam>
            #//ISSUE:objectofacompiler-generatedtypeiscreated
            #//ISSUE:variableofacompiler-generatedtype
            #//ISSUE:referencetoacompiler-generatedfield
            #//ISSUE:referencetoacompiler-generatedfield
                #//ISSUE:referencetoacompiler-generatedmethod
                    #this.mArtifactParam.Add(artifactParam)
                #//ISSUE:referencetoacompiler-generatedfield
                #artifactParam.Deserialize(deserializeCAnonStorey221.data)
                #//ISSUE:referencetoacompiler-generatedfield
                #if(this.mArtifactDictionary.ContainsKey(deserializeCAnonStorey221.data.iname))
                    #//ISSUE:referencetoacompiler-generatedfield
                    #thrownewException("重複エラー：Artifact["+deserializeCAnonStorey221.data.iname+"]")
                #//ISSUE:referencetoacompiler-generatedfield
                #this.mArtifactDictionary.Add(deserializeCAnonStorey221.data.iname,artifactParam)
        if 'Weapon' in json:
            this['mWeaponParam'] = newList<WeaponParam>
            #//ISSUE:objectofacompiler-generatedtypeiscreated
            #//ISSUE:variableofacompiler-generatedtype
            #//ISSUE:referencetoacompiler-generatedfield
            #//ISSUE:referencetoacompiler-generatedmethod
                #this.mWeaponParam.Add(weaponParam)
            #//ISSUE:referencetoacompiler-generatedfield
            #weaponParam.Deserialize(deserializeCAnonStorey222.data)
        if 'Recipe' in json:
            this['mRecipeParam'] = newList<RecipeParam>
            #this.mRecipeParam.Add(recipeParam)
            #recipeParam.Deserialize(json1)
        if 'Job' in json:
            this['mJobParam'] = newList<JobParam>
            #//ISSUE:objectofacompiler-generatedtypeiscreated
            #//ISSUE:variableofacompiler-generatedtype
            #//ISSUE:referencetoacompiler-generatedfield
            #//ISSUE:referencetoacompiler-generatedmethod
                #this.mJobParam.Add(jobParam)
                #//ISSUE:referencetoacompiler-generatedfield
            #//ISSUE:referencetoacompiler-generatedfield
            #jobParam.Deserialize(deserializeCAnonStorey223.data)
        if 'JobSet' in json:
            this['mJobSetParam'] = newList<JobSetParam>
        if 'Unit' in json:
            this['mJobsetDictionary'] = newDictionary<string,List<JobSetParam>>
            #//ISSUE:objectofacompiler-generatedtypeiscreated
            #//ISSUE:variableofacompiler-generatedtype
            #//ISSUE:referencetoacompiler-generatedfield
            #//ISSUE:referencetoacompiler-generatedmethod
                #this.mJobSetParam.Add(jobSetParam)
            #//ISSUE:referencetoacompiler-generatedfield
            #jobSetParam.Deserialize(deserializeCAnonStorey224.data)
            #if(!string.IsNullOrEmpty(jobSetParam.target_unit))
                #List<JobSetParam>jobSetParamList
                #if(this.mJobsetDictionary.ContainsKey(jobSetParam.target_unit))
                #else
                    #this.mJobsetDictionary.Add(jobSetParam.target_unit,jobSetParamList)
                #jobSetParamList.Add(jobSetParam)
        if 'Grow' in json:
            this['mGrowParam'] = newList<GrowParam>
            #//ISSUE:objectofacompiler-generatedtypeiscreated
            #//ISSUE:variableofacompiler-generatedtype
            #//ISSUE:referencetoacompiler-generatedfield
            #//ISSUE:referencetoacompiler-generatedmethod
                #this.mGrowParam.Add(growParam)
            #//ISSUE:referencetoacompiler-generatedfield
            #growParam.Deserialize(deserializeCAnonStorey225.data)
        if 'AI' in json:
            this['mAIParam'] = newList<AIParam>
            #//ISSUE:objectofacompiler-generatedtypeiscreated
            #//ISSUE:variableofacompiler-generatedtype
            #//ISSUE:referencetoacompiler-generatedfield
            #//ISSUE:referencetoacompiler-generatedmethod
                #this.mAIParam.Add(aiParam)
            #//ISSUE:referencetoacompiler-generatedfield
            #aiParam.Deserialize(deserializeCAnonStorey226.data)
        if 'Geo' in json:
            this['mGeoParam'] = newList<GeoParam>
            #//ISSUE:objectofacompiler-generatedtypeiscreated
            #//ISSUE:variableofacompiler-generatedtype
            #//ISSUE:referencetoacompiler-generatedfield
            #//ISSUE:referencetoacompiler-generatedmethod
                #this.mGeoParam.Add(geoParam)
            #//ISSUE:referencetoacompiler-generatedfield
            #geoParam.Deserialize(deserializeCAnonStorey227.data)
        if 'Rarity' in json:
            this['mRarityParam'] = newList<RarityParam>
            #RarityParamrarityParam
            #if(this.mRarityParam.Count>index)
            #else
                #this.mRarityParam.Add(rarityParam)
            #rarityParam.Deserialize(json.Rarity[index])
        if 'Shop' in json:
            this['mShopParam'] = newList<ShopParam>
            #ShopParamshopParam
            #if(this.mShopParam.Count>index)
            #else
                #this.mShopParam.Add(shopParam)
            #shopParam.Deserialize(json.Shop[index])
        if 'Player' in json:
            this['mPlayerParamTbl'] = newPlayerParam[json['Player'].Length]
            #this.mPlayerParamTbl[index].Deserialize(json1)
        if 'PlayerLvTbl' in json:
            this['mPlayerExpTbl'] = newOInt[json['PlayerLvTbl'].Length]
        if 'PlayerLvTbl' in json:
            this['mPlayerExpTbl[index]'] = json['PlayerLvTbl'][index]
        if 'UnitLvTbl' in json:
            this['mUnitExpTbl'] = newOInt[json['UnitLvTbl'].Length]
        if 'UnitLvTbl' in json:
            this['mUnitExpTbl[index]'] = json['UnitLvTbl'][index]
        if 'ArtifactLvTbl' in json:
            this['mArtifactExpTbl'] = newOInt[json['ArtifactLvTbl'].Length]
        if 'ArtifactLvTbl' in json:
            this['mArtifactExpTbl[index]'] = json['ArtifactLvTbl'][index]
        if 'AbilityRank' in json:
            this['mAbilityExpTbl'] = newOInt[json['AbilityRank'].Length]
        if 'AbilityRank' in json:
            this['mAbilityExpTbl[index]'] = json['AbilityRank'][index]
        if 'AwakePieceTbl' in json:
            this['mAwakePieceTbl'] = newOInt[json['AwakePieceTbl'].Length]
        if 'AwakePieceTbl' in json:
            this['mAwakePieceTbl[index]'] = json['AwakePieceTbl'][index]
        this['']
        this['mLocalNotificationParam']
        if 'LocalNotification' in json:
            this['mLocalNotificationParam']['msg_stamina'] = json['LocalNotification'][0].msg_stamina
        this['mLocalNotificationParam']
        if 'LocalNotification' in json:
            this['mLocalNotificationParam']['iOSAct_stamina'] = json['LocalNotification'][0].iOSAct_stamina
        this['mLocalNotificationParam']
        if 'LocalNotification' in json:
            this['mLocalNotificationParam']['limitSec_stamina'] = json['LocalNotification'][0].limitSec_stamina
            #if(trophyCategoryParam.Deserialize(json.TrophyCategory[index]))
                #trophyCategoryParamList.Add(trophyCategoryParam)
                #if(!dictionary.ContainsKey(trophyCategoryParam.hash_code))
                #dictionary.Add(trophyCategoryParam.hash_code,trophyCategoryParam)
            #if(trophyParam.Deserialize(json.Trophy[index]))
                #if(dictionary.ContainsKey(trophyParam.category_hash_code))
                #else
                #if(trophyParam.IsPlanningToUse())
                #trophyParamList.Add(trophyParam)
        #foreach(TrophyParamtrophyParaminthis.mTrophy)
        #this.mTrophyInameDict.Add(trophyParam.iname,trophyParam)
            #if(challengeCategoryParam.Deserialize(json.ChallengeCategory[index]))
            #challengeCategoryParamList.Add(challengeCategoryParam)
            #if(trophyParam.Deserialize(json.Challenge[index]))
                #trophyParamList.Add(trophyParam)
        #Array.Resize<TrophyParam>(refthis.mTrophy,length+this.mChallenge.Length)
        #Array.Copy((Array)this.mChallenge,0,(Array)this.mTrophy,length,this.mChallenge.Length)
        #foreach(TrophyParamtrophyParaminthis.mChallenge)
        #this.mTrophyInameDict.Add(trophyParam.iname,trophyParam)
    #this.CreateTrophyDict()
            #if(unlockParam.Deserialize(json.Unlock[index]))
            #unlockParamList.Add(unlockParam)
            #if(vipParam.Deserialize(json.Vip[index]))
            #vipParamList.Add(vipParam)
        if 'Mov' in json:
            this['mStreamingMovies'] = newJSON_StreamingMovie[json['Mov'].Length]
            this['']
            this['mStreamingMovies[index]']
            if 'Mov' in json:
                this['mStreamingMovies[index]']['alias'] = json['Mov'][index].alias
            this['mStreamingMovies[index]']
            if 'Mov' in json:
                this['mStreamingMovies[index]']['path'] = json['Mov'][index].path
            #if(bannerParam.Deserialize(json.Banner[index]))
            #bannerParamList.Add(bannerParam)
    #this.mJobParam[index].UpdateJobRankTransfarStatus(this)
            #unlockUnitDataParam.Deserialize(json.QuestClearUnlockUnitData[index])
            #unlockUnitDataParamList.Add(unlockUnitDataParam)
        if 'Award' in json:
            this['mAwardParam'] = newList<AwardParam>
        if 'Award' in json:
            this['mAwardDictionary'] = newDictionary<string,AwardParam>
            #//ISSUE:objectofacompiler-generatedtypeiscreated
            #//ISSUE:variableofacompiler-generatedtype
            #//ISSUE:referencetoacompiler-generatedfield
            #//ISSUE:referencetoacompiler-generatedfield
                #//ISSUE:referencetoacompiler-generatedmethod
                    #this.mAwardParam.Add(awardParam)
                #//ISSUE:referencetoacompiler-generatedfield
                #awardParam.Deserialize(deserializeCAnonStorey228.data)
                #if(this.mAwardDictionary.ContainsKey(awardParam.iname))
                #thrownewException("Overlap:Award["+awardParam.iname+"]")
                #this.mAwardDictionary.Add(awardParam.iname,awardParam)
            #if(loginInfoParam.Deserialize(json.LoginInfo[index]))
            #loginInfoParamList.Add(loginInfoParam)
            #collaboSkillParam.Deserialize(json.CollaboSkill[index])
            #collaboSkillParamList.Add(collaboSkillParam)
        #CollaboSkillParam.UpdateCollaboSkill(this.mCollaboSkillParam)
            #trickParam.Deserialize(json.Trick[index])
            #trickParamList.Add(trickParam)
            #breakObjParam.Deserialize(json.BreakObj[index])
            #breakObjParamList.Add(breakObjParam)
        if 'VersusMatchKey' in json:
            this['mVersusMatching'] = newList<VersusMatchingParam>
            #versusMatchingParam.Deserialize(json.VersusMatchKey[index])
            #this.mVersusMatching.Add(versusMatchingParam)
        if 'VersusMatchCond' in json:
            this['mVersusMatchCond'] = newList<VersusMatchCondParam>
            #versusMatchCondParam.Deserialize(json.VersusMatchCond[index])
            #this.mVersusMatchCond.Add(versusMatchCondParam)
        if 'TowerScore' in json:
            this['mTowerScore'] = newList<TowerScoreParam>
            #this.mTowerScore.Add(towerScoreParam)
        if 'TowerRank' in json:
            this['mTowerRankTbl'] = newOInt[json['TowerRank'].Length]
        if 'TowerRank' in json:
            this['mTowerRankTbl[index]'] = json['TowerRank'][index]
        if 'MultilimitUnitLv' in json:
            this['mMultiLimitUnitLv'] = newOInt[json['MultilimitUnitLv'].Length]
        if 'MultilimitUnitLv' in json:
            this['mMultiLimitUnitLv[index]'] = json['MultilimitUnitLv'][index]
            #presentItemParam.Deserialize(json.FriendPresentItem[index])
            #this.mFriendPresentItemParam.Add(presentItemParam.iname,presentItemParam)
            #weatherParam.Deserialize(json.Weather[index])
            #weatherParamList.Add(weatherParam)
    #returntrue
return this
