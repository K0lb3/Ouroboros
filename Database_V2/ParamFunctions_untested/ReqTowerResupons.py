def ReqTowerResupons(json):
    this={}#ReqTowerResuponseres)
    #if(res==null)
    #return
    #this.TowerID=GlobalVars.SelectedTowerID
    #this.rtime=res.rtime
    #if(res.stats!=null)
        #this.Deserialize(res.stats)
    #else
        #TowerFloorParamfirstTowerFloor=MonoSingleton<GameManager>.Instance.FindFirstTowerFloor(this.TowerID)
        #if(firstTowerFloor!=null)
        #this.Deserialize(newJSON_ReqTowerResuponse.Json_TowerStatus()
            #fname=firstTowerFloor.iname,
            #questStates=QuestStates.New
            #})
        #if(res.pdeck!=null)
            #this.pdeck=newList<TowerResuponse.PlayerUnit>()
            #for(intindex=0index<res.pdeck.Length++index)
                #this.pdeck.Add(newTowerResuponse.PlayerUnit())
                #this.pdeck.dmg=res.pdeck.damage
                #this.pdeck.unitname=res.pdeck.uname
                #this.pdeck.is_died=res.pdeck.is_died
        #this.reset_cost=res.reset_cost
        #this.round=res.round
        #this.is_reset=res.is_reset==(byte)1
        #if(res.lot_enemies!=null&&res.lot_enemies.Length>0)
            #this.lot_enemies=newRandDeckResult[res.lot_enemies.Length]
            #for(intindex=0index<res.lot_enemies.Length++index)
            #this.lot_enemies=newRandDeckResult()
                #id=res.lot_enemies.id,
                #set_id=res.lot_enemies.set_id
                #}
            #this.Deserialize(res.edeck)
            #this.Deserialize(res.rank)
            #this.UpdateCurrentFloor()
        #
        #privatevoidDeserialize(JSON_ReqTowerResuponse.Json_RankStatusjson)
            #if(json==null)
            #return
            if 'turn_num' in json:
                this['turn_num'] = json['turn_num']
            if 'died_num' in json:
                this['died_num'] = json['died_num']
            if 'retire_num' in json:
                this['retire_num'] = json['retire_num']
            if 'recovery_num' in json:
                this['recover_num'] = json['recovery_num']
            if 'spd_rank' in json:
                this['speedRank'] = json['spd_rank']
            if 'tec_rank' in json:
                this['techRank'] = json['tec_rank']
            if 'spd_score' in json:
                this['spd_score'] = json['spd_score']
            if 'tec_score' in json:
                this['tec_score'] = json['tec_score']
            if 'ret_score' in json:
                this['ret_score'] = json['ret_score']
            if 'rcv_score' in json:
                this['rcv_score'] = json['rcv_score']
            if 'challenge_num' in json:
                this['challenge_num'] = json['challenge_num']
            if 'lose_num' in json:
                this['lose_num'] = json['lose_num']
            if 'reset_num' in json:
                this['reset_num'] = json['reset_num']
            if 'challenge_score' in json:
                this['challenge_score'] = json['challenge_score']
            if 'lose_score' in json:
                this['lose_score'] = json['lose_score']
            if 'reset_score' in json:
                this['reset_score'] = json['reset_score']
        #
        #publicvoidDeserialize(JSON_ReqTowerResuponse.Json_TowerPlayerUnitres)
            #if(res==null||res==null)
            #return
            #this.pdeck=newList<TowerResuponse.PlayerUnit>()
            #for(intindex=0index<res.Length++index)
                #this.pdeck.Add(newTowerResuponse.PlayerUnit())
                #this.pdeck.dmg=res.damage
                #this.pdeck.unitname=res.uname
                #this.pdeck.is_died=res.is_died
        #
        #publicvoidDeserialize(JSON_ReqTowerResuponse.Json_TowerEnemyUnitres)
            #if(res==null)
            #return
            #this.edeck=newList<TowerResuponse.EnemyUnit>()
            #for(intindex=0index<res.Length++index)
                #this.edeck.Add(newTowerResuponse.EnemyUnit())
                #this.edeck.hp=res.hp
                #this.edeck.jewel=res.jewel
                #this.edeck.eid=res.eid.ToString()
        #
        #publicvoidDeserialize(ReqTowerRank.JSON_TowerRankResponsejson)
            #if(json==null)
            #return
            #if(json.speed!=null)
                if 'speed' in json:
                    this['SpdRank'] = newTowerResuponse.TowerRankParam[json['speed'].Length]
                #for(intindex=0index<json.speed.Length++index)
                    #this.SpdRank=newTowerResuponse.TowerRankParam()
                    this['']
                    this['SpdRank']
                    if 'speed' in json:
                        this['SpdRank']['lv'] = json['speed'].lv
                    this['SpdRank']
                    if 'speed' in json:
                        this['SpdRank']['name'] = json['speed'].name
                    this['SpdRank']
                    if 'speed' in json:
                        this['SpdRank']['rank'] = json['speed'].rank
                    this['SpdRank']
                    if 'speed' in json:
                        this['SpdRank']['score'] = json['speed'].score
                    #this.SpdRank.unit=newUnitData()
                    #this.SpdRank.unit.Deserialize(json.speed.unit)
                    this['SpdRank']
                    if 'speed' in json:
                        this['SpdRank']['selected_award'] = json['speed'].selected_award
            #if(json.technical!=null)
                if 'technical' in json:
                    this['TecRank'] = newTowerResuponse.TowerRankParam[json['technical'].Length]
                #for(intindex=0index<json.technical.Length++index)
                    #this.TecRank=newTowerResuponse.TowerRankParam()
                    this['']
                    this['TecRank']
                    if 'technical' in json:
                        this['TecRank']['lv'] = json['technical'].lv
                    this['TecRank']
                    if 'technical' in json:
                        this['TecRank']['name'] = json['technical'].name
                    this['TecRank']
                    if 'technical' in json:
                        this['TecRank']['rank'] = json['technical'].rank
                    this['TecRank']
                    if 'technical' in json:
                        this['TecRank']['score'] = json['technical'].score
                    #this.TecRank.unit=newUnitData()
                    #this.TecRank.unit.Deserialize(json.technical.unit)
                    this['TecRank']
                    if 'technical' in json:
                        this['TecRank']['selected_award'] = json['technical'].selected_award
            #this.Deserialize(json.rank)
        #
        #privatevoidUpdateCurrentFloor()
            #if(this.status==null)
                #this.currentFloor=MonoSingleton<GameManager>.Instance.FindFirstTowerFloor(GlobalVars.SelectedTowerID)
            #else
                #this.currentFloor=MonoSingleton<GameManager>.Instance.FindTowerFloor(this.status.fname)
                #DebugUtility.Assert(this.currentFloor!=null,string.Format("フロア[{0}]が見つかりません",(object)this.status.fname))
                #if(this.currentFloor==null||this.status.state!=QuestStates.Cleared)
                #return
                #TowerFloorParamnextTowerFloor=MonoSingleton<GameManager>.Instance.FindNextTowerFloor(this.currentFloor.tower_id,this.currentFloor.iname)
                #if(nextTowerFloor==null)
                #return
                #this.currentFloor=nextTowerFloor
        #
        #publicvoidDeserialize(JSON_ReqTowerResuponse.Json_TowerStatusjson)
            #this.status=newTowerResuponse.Status()
            this['']
            this['status']
            if 'fname' in json:
                this['status']['fname'] = json['fname']
            this['status']
            if 'questStates' in json:
                this['status']['state'] = json['questStates']
            #TowerFloorParamtowerFloor=MonoSingleton<GameManager>.Instance.FindTowerFloor(this.status.fname)
            #if(towerFloor==null)
            #return
            #List<TowerFloorParam>towerFloors=MonoSingleton<GameManager>.Instance.FindTowerFloors(towerFloor.tower_id)
            #List<QuestParam>referenceQuestList=newList<QuestParam>()
            #for(shortindex=0(int)index<towerFloors.Count++index)
                #towerFloors[(int)index].FloorIndex=index
                #referenceQuestList.Add(towerFloors[(int)index].GetQuestParam())
            #QuestParamquestParam=towerFloor.GetQuestParam()
            #using(List<QuestParam>.Enumeratorenumerator=referenceQuestList.GetEnumerator())
                #while(enumerator.MoveNext())
                #enumerator.Current.state=QuestStates.New
            #this.SetQuestState(referenceQuestList,questParam,QuestStates.Cleared,true)
            #questParam.state=this.status.state
        #
        #privatevoidUpdateFloorQuestsState()
        #
        #privatevoidSetQuestState(List<QuestParam>referenceQuestList,QuestParamquestParam,QuestStatesstate,boolcond_recursive)
            #if(questParam==null)
            #return
            #questParam.state=state
            #if(!cond_recursive||questParam.cond_quests==null)
            #return
            #foreach(stringcondQuestinquestParam.cond_quests)
                #stringiname=condQuest
                #QuestParamquestParam1=referenceQuestList.Find((Predicate<QuestParam>)(q=>q.iname==iname))
                #this.SetQuestState(referenceQuestList,questParam1,state,cond_recursive)
        #
        #publicTowerFloorParamGetCurrentFloor()
            #returnthis.currentFloor
        #
        #publicvoidCalcDamage(List<Unit>player)
            #if(this.pdeck==null)
            #return
            #for(inti=0i<this.pdeck.Count++i)
                #Unitunit=player.Find((Predicate<Unit>)(x=>x.UnitParam.iname==this.pdeck[i].unitname))
                #if(unit!=null)
                    #intnum=Mathf.Min(this.pdeck[i].dmg,(int)unit.MaximumStatus.param.hp-1)
                    #unit.Damage(num,false)
        #
        #publicintGetPlayerUnitHP(UnitDatadata)
            #if(this.pdeck==null)
            #return(int)data.Status.param.hp
            #TowerResuponse.PlayerUnitplayerUnit=this.FindPlayerUnit(data)
            #if(playerUnit==null)
            #return(int)data.Status.param.hp
            #intnum=Mathf.Min(playerUnit.dmg,(int)data.Status.param.hp-1)
            #return(int)data.Status.param.hp-num
        #
        #publicintGetUnitDamage(UnitDataunit_data)
            #if(this.pdeck==null)
            #return0
            #TowerResuponse.PlayerUnitplayerUnit=this.FindPlayerUnit(unit_data)
            #if(playerUnit==null)
            #return0
            #returnplayerUnit.dmg
        #
        #publicvoidCalcEnemyDamage(List<Unit>enemy,boolis_menu=false)
            #if(this.edeck==null)
            #return
            #List<Unit>unitList=newList<Unit>()
            #for(intindex1=0index1<this.edeck.Count++index1)
                #intindex2=int.Parse(this.edeck[index1].eid)
                #if(index2>=0&&enemy.Count>index2)
                    #Unitunit=enemy[index2]
                    #if(unit!=null&&(!is_menu||!unit.IsGimmick))
                        #if(unit.IsGimmick&&this.edeck[index1].hp==0)
                            #unitList.Add(unit)
                        #else
                            #unit.Damage((int)unit.MaximumStatus.param.hp-this.edeck[index1].hp,false)
                            #unit.Gems=this.edeck[index1].jewel
                            #this.edeck[index1].id=unit.UnitData.UniqueID
                            #this.edeck[index1].iname=unit.UnitParam.iname
            #enemy.RemoveAll((Predicate<Unit>)(x=>
                #if(!x.IsGimmick)
                #return(int)x.CurrentStatus.param.hp<=0
                #returnfalse
                #}))
                #for(intindex=0index<unitList.Count++index)
                    #if(unitList!=null)
                    #enemy.Remove(unitList)
            #
            #publicintCalcRecoverCost()
                #TowerParamtower=MonoSingleton<GameManager>.Instance.FindTower(this.TowerID)
                #if(tower==null)
                #return0
                #doublenum=Math.Ceiling((TimeManager.FromUnixTime(this.rtime).AddMinutes(-1.0)-TimeManager.ServerTime).TotalMinutes)/(double)tower.unit_recover_minute
                #returnMathf.Clamp((int)Math.Ceiling((double)tower.unit_recover_coin*num),0,(int)tower.unit_recover_coin)
            #
            #publicboolExistDamagedUnit()
                #if(this.pdeck==null)
                #returnfalse
                #for(intindex=0index<this.pdeck.Count++index)
                    #if(this.pdeck.dmg>0)
                    #returntrue
                #returnfalse
            #
            #publicintGetDiedUnitNum()
                #intnum=0
                #if(this.pdeck==null)
                #returnnum
                #for(intindex=0index<this.pdeck.Count++index)
                    #if(this.pdeck.isDied)
                    #++num
                #returnnum
            #
            #publicList<UnitData>GetAvailableUnits()
                #returnMonoSingleton<GameManager>.Instance.Player.Units.FindAll((Predicate<UnitData>)(unitData=>unitData.Lv>=20))
            #
            #publicTowerResuponse.PlayerUnitFindPlayerUnit(UnitDataunit)
                #if(this.pdeck==null)
                #return(TowerResuponse.PlayerUnit)null
                #returnthis.pdeck.Find((Predicate<TowerResuponse.PlayerUnit>)(x=>x.unitname==unit.UnitParam.iname))
            #
            #publicvoidOnFloorReset()
                #this.edeck=(List<TowerResuponse.EnemyUnit>)null
                #this.lot_enemies=(RandDeckResult)null
            #
            #publicvoidOnFloorRanking(ReqTowerFloorRanking.Json_Responsejson)
                #this.FloorScores=newTowerScore.ViewParam()
                #this.FloorSpdRank=(TowerResuponse.TowerRankParam)null
                #this.FloorTecRank=(TowerResuponse.TowerRankParam)null
                #if(json==null)
                #return
                #if(json.score!=null)
                    this['']
                    this['FloorScores']
                    if 'score' in json:
                        this['FloorScores']['TurnNum'] = json['score'].turn_num
                    this['FloorScores']
                    if 'score' in json:
                        this['FloorScores']['DiedNum'] = json['score'].died_num
                    this['FloorScores']
                    if 'score' in json:
                        this['FloorScores']['RecoveryNum'] = json['score'].recovery_num
                    this['FloorScores']
                    if 'score' in json:
                        this['FloorScores']['RetireNum'] = json['score'].retire_num
                    this['FloorScores']
                    if 'score' in json:
                        this['FloorScores']['FloorResetNum'] = json['score'].reset_num
                    this['FloorScores']
                    if 'score' in json:
                        this['FloorScores']['LoseNum'] = json['score'].lose_num
                    this['FloorScores']
                    if 'score' in json:
                        this['FloorScores']['ChallengeNum'] = json['score'].challenge_num
                #if(json.speed!=null)
                    if 'speed' in json:
                        this['FloorSpdRank'] = newTowerResuponse.TowerRankParam[json['speed'].Length]
                    #for(intindex=0index<json.speed.Length++index)
                        #this.FloorSpdRank=newTowerResuponse.TowerRankParam()
                        this['']
                        this['FloorSpdRank']
                        if 'speed' in json:
                            this['FloorSpdRank']['lv'] = json['speed'].lv
                        this['FloorSpdRank']
                        if 'speed' in json:
                            this['FloorSpdRank']['name'] = json['speed'].name
                        this['FloorSpdRank']
                        if 'speed' in json:
                            this['FloorSpdRank']['rank'] = json['speed'].rank
                        this['FloorSpdRank']
                        if 'speed' in json:
                            this['FloorSpdRank']['score'] = json['speed'].score
                        #this.FloorSpdRank.unit=newUnitData()
                        #this.FloorSpdRank.unit.Deserialize(json.speed.unit)
                        this['FloorSpdRank']
                        if 'speed' in json:
                            this['FloorSpdRank']['selected_award'] = json['speed'].selected_award
                #if(json.technical==null)
                #return
                if 'technical' in json:
                    this['FloorTecRank'] = newTowerResuponse.TowerRankParam[json['technical'].Length]
                #for(intindex=0index<json.technical.Length++index)
                    #this.FloorTecRank=newTowerResuponse.TowerRankParam()
                    this['']
                    this['FloorTecRank']
                    if 'technical' in json:
                        this['FloorTecRank']['lv'] = json['technical'].lv
                    this['FloorTecRank']
                    if 'technical' in json:
                        this['FloorTecRank']['name'] = json['technical'].name
                    this['FloorTecRank']
                    if 'technical' in json:
                        this['FloorTecRank']['rank'] = json['technical'].rank
                    this['FloorTecRank']
                    if 'technical' in json:
                        this['FloorTecRank']['score'] = json['technical'].score
                    #this.FloorTecRank.unit=newUnitData()
                    #this.FloorTecRank.unit.Deserialize(json.technical.unit)
                    this['FloorTecRank']
                    if 'technical' in json:
                        this['FloorTecRank']['selected_award'] = json['technical'].selected_award
            #
            #publicboolCheckEnemyDeck()
                #returnthis.edeck!=null
            #
            #publicclassStatus
                #publicstringfname
                #publicQuestStatesstate
            #
            #publicclassPlayerUnit
                #publicstringunitname
                #publicintdmg
                #publicintis_died
                #
                #publicboolisDied
                    #get
                        #returnthis.is_died==1
            #
            #publicclassEnemyUnit
                #publiclongid
                #publicstringiname
                #publicstringeid
                #publicinthp
                #publicintjewel
            #
            #publicclassTowerRankParam
                #publicstringname
                #publicintlv
                #publicintrank
                #publicintscore
                #publicstringuid
                #publicUnitDataunit
                #publicstringselected_award
    #
return this
