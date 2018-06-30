namespace SRPG
{
    using System;
    using System.Runtime.CompilerServices;
    using UnityEngine;

    [Pin(0x4b1, "方向入力完了", 1, 0x4b1), Pin(0x514, "スキル選択開始", 1, 0x514), Pin(0x515, "スキル選択終了", 1, 0x515), Pin(0x578, "アイテム選択開始", 1, 0x578), Pin(0x579, "アイテム選択終了", 1, 0x579), Pin(0x5dc, "入力打ち切り (マルチプレイ)", 1, 0x5dc), Pin(0x5dd, "BANされた (マルチプレイ)", 1, 0x5dd), Pin(0x5de, "切断された (マルチプレイ)", 1, 0x5de), Pin(0x5df, "進行異常 (マルチプレイ)", 1, 0x5df), ShowInInspector, Pin(0x1bbf, "ユニット交代確認終了", 1, 0x1bbf), Pin(0x1bbe, "ユニット交代確認開始", 1, 0x1bbe), Pin(0x1bbd, "ユニット交代選択終了", 1, 0x1bbd), Pin(0x1bbc, "ユニット交代選択開始", 1, 0x1bbc), Pin(0x1b5a, "パネルからターゲット切り替え", 0, 0x1b5a), Pin(0x1b59, "ギミックからターゲット切り替え", 0, 0x1b59), Pin(0x1b58, "ユニットからターゲット切り替え", 0, 0x1b58), Pin(0x1770, "フレンド選択", 1, 0x1770), Pin(0x1388, "購入ウインドウ表示", 1, 0x1388), Pin(0x7d1, "バトル終了", 1, 0x7d1), Pin(0x7d0, "バトル開始", 1, 0x7d0), Pin(0x60e, "初顔合わせ通知", 1, 0x60e), Pin(0x640, "グリッド上イベント選択開始", 1, 0x640), Pin(0x641, "グリッド上イベント選択終了", 1, 0x641), Pin(0x40b, "投げるターゲット選択開始", 1, 0x40b), Pin(0x408, "ターゲット選択終了", 1, 0x408), Pin(0x406, "ターゲット選択開始", 1, 0x406), Pin(0x3fc, "コマンドが選択された", 1, 0x3fc), Pin(0x3f2, "コマンド選択開始", 1, 0x3f2), Pin(0x3ee, "マップ終了", 1, 0x3ee), Pin(0x3ed, "マップ開始", 1, 0x3ed), Pin(0x3ea, "クエスト終了 (敗亡)", 1, 0x3ea), Pin(0x3e9, "クエスト終了 (勝利)", 1, 0x3e9), Pin(0x3e8, "クエスト開始", 1, 0x3e8), NodeType("Battle/Events"), Pin(0x40c, "投げるターゲット選択終了", 1, 0x40c), Pin(0x410, "移動先の選択開始", 1, 0x410), Pin(0x411, "移動先の選択完了", 1, 0x411), Pin(0x41a, "マップ確認開始", 1, 0x41a), Pin(0x424, "ユニット行動開始", 1, 0x424), Pin(0x42e, "詠唱スキル発動", 1, 0x42e), Pin(0x44c, "メインターゲット選択", 1, 0x44c), Pin(0x44d, "メインターゲット選択解除", 1, 0x44d), Pin(0x456, "サブターゲット選択", 1, 0x456), Pin(0x457, "サブターゲット選択解除", 1, 0x457), Pin(0x4b0, "方向入力開始", 1, 0x4b0)]
    public class FlowNode_BattleUI : FlowNodePersistent
    {
        [StringIsGameObjectID]
        public string CommandObjectID;
        [StringIsGameObjectID]
        public string QueueObjectID;
        [StringIsGameObjectID]
        public string SkillListID;
        [StringIsGameObjectID]
        public string ItemListID;
        [StringIsGameObjectID]
        public string VirtualStickID;
        [StringIsGameObjectID]
        public string QuestStatusID;
        [StringIsGameObjectID]
        public string CameraControllerID;
        [StringIsGameObjectID]
        public string FukanCameraID;
        [StringIsGameObjectID]
        public string NextTargetButtonID;
        [StringIsGameObjectID]
        public string PrevTargetButtonID;
        [StringIsGameObjectID]
        public string MapHeightID;
        [StringIsGameObjectID]
        public string ElementDiagram;
        [StringIsGameObjectID]
        public string ArenaActionCountID;
        [StringIsGameObjectID]
        public string PlayerActionCountID;
        [StringIsGameObjectID]
        public string EnemyActionCountID;
        [StringIsGameObjectID]
        public string RankingQuestActionCountID;
        [StringIsGameObjectID]
        public string UnitChgListID;
        [StringIsGameObjectID]
        public string WeatherInfoID;
        public TargetPlate Prefab_TargetMain;
        public TargetPlate Prefab_TargetSub;
        public TargetPlate Prefab_ObjectTarget;
        public TargetPlate Prefab_TrickTarget;
        [NonSerialized]
        public TargetPlate TargetMain;
        [NonSerialized]
        public TargetPlate TargetSub;
        [NonSerialized]
        public TargetPlate TargetObjectSub;
        [NonSerialized]
        public TargetPlate TargetTrickSub;
        [StringIsLocalEventID]
        public string QuestStart;
        [StringIsLocalEventID]
        public string QuestStart_Short;
        [StringIsLocalEventID]
        public string QuestStart_Arena;
        [StringIsLocalEventID]
        public string QuestStart_VS;
        [StringIsLocalEventID]
        public string QuestStart_RankMatch;
        [StringIsLocalEventID]
        public string QuestEnd;
        [StringIsLocalEventID]
        public string QuestWin;
        [StringIsLocalEventID]
        public string QuestWin_Arena;
        [StringIsLocalEventID]
        public string QuestWin_Versus;
        [StringIsLocalEventID]
        public string QuestLose;
        [StringIsLocalEventID]
        public string QuestLose_Arena;
        [StringIsLocalEventID]
        public string QuestLose_Versus;
        [StringIsLocalEventID]
        public string QuestDraw_Versus;
        [StringIsLocalEventID]
        public string QuestWin_Audience;
        [StringIsLocalEventID]
        public string Result;
        [StringIsLocalEventID]
        public string Result_MP;
        [StringIsLocalEventID]
        public string Result_Arena;
        [StringIsLocalEventID]
        public string Result_Tower;
        [StringIsLocalEventID]
        public string Result_Versus;
        [StringIsLocalEventID]
        public string Result_MultiTower;
        [StringIsLocalEventID]
        public string Result_RankMatch;
        [StringIsLocalEventID]
        public string PlayAgain;
        [StringIsLocalEventID]
        public string MapViewStart;
        [StringIsLocalEventID]
        public string MapViewEnd;
        [StringIsLocalEventID]
        public string MapViewSelectUnit;
        [StringIsLocalEventID]
        public string MapViewSelectGrid;
        [StringIsLocalEventID]
        public string VersusStart;
        [StringIsLocalEventID]
        public string VersusEnd;
        [StringIsLocalEventID]
        public string MPSelectContinueWaitingStart;
        [StringIsLocalEventID]
        public string MPSelectContinueWaitingEnd;
        private VirtualStick2 mVirtualStick;
        private UnitAbilitySkillList mSkillListRef;
        private UnitCommands mCommandWindow;
        private BattleInventory mItemWindow;
        private BattleUnitChg mUnitChgWindow;
        public bool IsEnableUnit;
        public bool IsEnableGimmick;
        public bool IsEnableTrick;
        [CompilerGenerated]
        private bool <IsInputDirectionStarted>k__BackingField;

        public FlowNode_BattleUI()
        {
            base..ctor();
            return;
        }

        public void ClearEnableAll()
        {
            this.IsEnableUnit = 0;
            this.IsEnableGimmick = 0;
            this.IsEnableTrick = 0;
            return;
        }

        public void Hide()
        {
            CanvasGroup group;
            group = base.GetComponent<CanvasGroup>();
            if ((group != null) == null)
            {
                goto Label_0025;
            }
            group.set_alpha(0f);
            group.set_blocksRaycasts(0);
        Label_0025:
            return;
        }

        public void HideTargetWindows()
        {
            if ((this.TargetMain != null) == null)
            {
                goto Label_001D;
            }
            this.TargetMain.ForceClose(1);
        Label_001D:
            if ((this.TargetSub != null) == null)
            {
                goto Label_003A;
            }
            this.TargetSub.ForceClose(1);
        Label_003A:
            if ((this.TargetObjectSub != null) == null)
            {
                goto Label_0057;
            }
            this.TargetObjectSub.ForceClose(1);
        Label_0057:
            if ((this.TargetTrickSub != null) == null)
            {
                goto Label_0074;
            }
            this.TargetTrickSub.ForceClose(1);
        Label_0074:
            return;
        }

        public bool IsNeedFlip()
        {
            return ((this.IsEnableTrick == null) ? 0 : ((this.IsEnableUnit != null) ? 1 : this.IsEnableGimmick));
        }

        public override void OnActivate(int pinID)
        {
            int num;
            num = pinID;
            switch ((num - 0x1b58))
            {
                case 0:
                    goto Label_001F;

                case 1:
                    goto Label_0062;

                case 2:
                    goto Label_009F;
            }
            goto Label_0119;
        Label_001F:
            if ((this.TargetSub != null) == null)
            {
                goto Label_003B;
            }
            this.TargetSub.Close();
        Label_003B:
            if ((this.TargetTrickSub != null) == null)
            {
                goto Label_0119;
            }
            this.TargetTrickSub.Open();
            this.OnMapViewSelectGrid();
            goto Label_0119;
        Label_0062:
            if ((this.TargetObjectSub != null) == null)
            {
                goto Label_007E;
            }
            this.TargetObjectSub.Close();
        Label_007E:
            if ((this.TargetTrickSub != null) == null)
            {
                goto Label_0119;
            }
            this.TargetTrickSub.Open();
            goto Label_0119;
        Label_009F:
            if ((this.TargetTrickSub != null) == null)
            {
                goto Label_00BB;
            }
            this.TargetTrickSub.Close();
        Label_00BB:
            if ((this.TargetSub != null) == null)
            {
                goto Label_00ED;
            }
            if (this.IsEnableUnit == null)
            {
                goto Label_00ED;
            }
            this.TargetSub.Open();
            this.OnMapViewSelectUnit();
            goto Label_0114;
        Label_00ED:
            if ((this.TargetObjectSub != null) == null)
            {
                goto Label_0119;
            }
            if (this.IsEnableGimmick == null)
            {
                goto Label_0119;
            }
            this.TargetObjectSub.Open();
        Label_0114:;
        Label_0119:
            return;
        }

        public void OnArenaLose()
        {
            FlowNode_TriggerLocalEvent.TriggerLocalEvent(this, this.QuestLose_Arena);
            return;
        }

        public void OnArenaWin()
        {
            FlowNode_TriggerLocalEvent.TriggerLocalEvent(this, this.QuestWin_Arena);
            return;
        }

        public void OnAudienceWin()
        {
            FlowNode_TriggerLocalEvent.TriggerLocalEvent(this, this.QuestWin_Audience);
            return;
        }

        public void OnBan()
        {
            this.Output(0x5dd);
            return;
        }

        public void OnBattleEnd()
        {
            this.Output(0x7d1);
            return;
        }

        public void OnBattleStart()
        {
            this.Output(0x7d0);
            return;
        }

        public void OnCastSkillStart()
        {
            this.Output(0x42e);
            return;
        }

        public void OnCommandSelect()
        {
            this.Output(0x3fc);
            return;
        }

        public void OnCommandSelectStart()
        {
            this.Output(0x3f2);
            return;
        }

        public void OnDisconnected()
        {
            this.Output(0x5de);
            return;
        }

        public void OnFirstContact()
        {
            this.Output(0x60e);
            return;
        }

        public void OnGridEventSelectEnd()
        {
            this.Output(0x641);
            return;
        }

        public void OnGridEventSelectStart()
        {
            this.Output(0x640);
            return;
        }

        public void OnInputDirectionEnd()
        {
            this.Output(0x4b1);
            this.IsInputDirectionStarted = 0;
            return;
        }

        public void OnInputDirectionStart()
        {
            this.Output(0x4b0);
            this.IsInputDirectionStarted = 1;
            return;
        }

        public void OnInputMoveEnd()
        {
            this.Output(0x411);
            return;
        }

        public void OnInputMoveStart()
        {
            this.Output(0x410);
            return;
        }

        public void OnInputTimeLimit()
        {
            this.Output(0x5dc);
            return;
        }

        public void OnItemSelectEnd()
        {
            this.Output(0x579);
            return;
        }

        public void OnItemSelectStart()
        {
            this.Output(0x578);
            return;
        }

        public void OnMainTargetDeselect()
        {
            this.Output(0x44d);
            return;
        }

        public void OnMainTargetSelect()
        {
            this.Output(0x44c);
            return;
        }

        public void OnMapEnd()
        {
            this.Output(0x3ee);
            return;
        }

        public void OnMapModeStart()
        {
            this.Output(0x41a);
            return;
        }

        public void OnMapStart()
        {
            this.Output(0x3ed);
            return;
        }

        public void OnMapViewEnd()
        {
            FlowNode_TriggerLocalEvent.TriggerLocalEvent(this, this.MapViewEnd);
            return;
        }

        public void OnMapViewSelectGrid()
        {
            FlowNode_TriggerLocalEvent.TriggerLocalEvent(this, this.MapViewSelectGrid);
            return;
        }

        public void OnMapViewSelectUnit()
        {
            FlowNode_TriggerLocalEvent.TriggerLocalEvent(this, this.MapViewSelectUnit);
            return;
        }

        public void OnMapViewStart()
        {
            FlowNode_TriggerLocalEvent.TriggerLocalEvent(this, this.MapViewStart);
            return;
        }

        public void OnMPSelectContinueWaitingEnd()
        {
            FlowNode_TriggerLocalEvent.TriggerLocalEvent(this, this.MPSelectContinueWaitingEnd);
            return;
        }

        public void OnMPSelectContinueWaitingStart()
        {
            FlowNode_TriggerLocalEvent.TriggerLocalEvent(this, this.MPSelectContinueWaitingStart);
            return;
        }

        public void OnQuestEnd()
        {
            FlowNode_TriggerLocalEvent.TriggerLocalEvent(this, this.QuestEnd);
            return;
        }

        public void OnQuestLose()
        {
            FlowNode_TriggerLocalEvent.TriggerLocalEvent(this, this.QuestLose);
            return;
        }

        public void OnQuestStart()
        {
            FlowNode_TriggerLocalEvent.TriggerLocalEvent(this, this.QuestStart);
            return;
        }

        public void OnQuestStart_Arena()
        {
            FlowNode_TriggerLocalEvent.TriggerLocalEvent(this, this.QuestStart_Arena);
            return;
        }

        public void OnQuestStart_RankMatch()
        {
            FlowNode_TriggerLocalEvent.TriggerLocalEvent(this, this.QuestStart_RankMatch);
            return;
        }

        public void OnQuestStart_Short()
        {
            FlowNode_TriggerLocalEvent.TriggerLocalEvent(this, this.QuestStart_Short);
            return;
        }

        public void OnQuestStart_VS()
        {
            FlowNode_TriggerLocalEvent.TriggerLocalEvent(this, this.QuestStart_VS);
            return;
        }

        public void OnQuestWin()
        {
            FlowNode_TriggerLocalEvent.TriggerLocalEvent(this, this.QuestWin);
            return;
        }

        public void OnResult()
        {
            FlowNode_TriggerLocalEvent.TriggerLocalEvent(this, this.Result);
            return;
        }

        public void OnResult_Arena()
        {
            FlowNode_TriggerLocalEvent.TriggerLocalEvent(this, this.Result_Arena);
            return;
        }

        public void OnResult_MP()
        {
            FlowNode_TriggerLocalEvent.TriggerLocalEvent(this, this.Result_MP);
            return;
        }

        public void OnResult_MultiTower()
        {
            FlowNode_TriggerLocalEvent.TriggerLocalEvent(this, this.Result_MultiTower);
            return;
        }

        public void OnResult_RankMatch()
        {
            FlowNode_TriggerLocalEvent.TriggerLocalEvent(this, this.Result_RankMatch);
            return;
        }

        public void OnResult_Tower()
        {
            FlowNode_TriggerLocalEvent.TriggerLocalEvent(this, this.Result_Tower);
            return;
        }

        public void OnResult_Versus()
        {
            FlowNode_TriggerLocalEvent.TriggerLocalEvent(this, this.Result_Versus);
            return;
        }

        public void OnSequenceError()
        {
            this.Output(0x5df);
            return;
        }

        public void OnSkillSelectEnd()
        {
            this.Output(0x515);
            return;
        }

        public void OnSkillSelectStart()
        {
            this.Output(0x514);
            return;
        }

        public void OnSubTargetDeselect()
        {
            this.Output(0x457);
            return;
        }

        public void OnSubTargetSelect()
        {
            this.Output(0x456);
            return;
        }

        public void OnSupportSelectStart()
        {
            FlowNode_TriggerLocalEvent.TriggerLocalEvent(this, this.PlayAgain);
            return;
        }

        public void OnTargetSelectEnd()
        {
            this.Output(0x408);
            return;
        }

        public void OnTargetSelectStart()
        {
            this.Output(0x406);
            return;
        }

        public void OnThrowTargetSelectEnd()
        {
            this.Output(0x40c);
            return;
        }

        public void OnThrowTargetSelectStart()
        {
            this.Output(0x40b);
            return;
        }

        public void OnUnitChgConfirmEnd()
        {
            this.Output(0x1bbf);
            return;
        }

        public void OnUnitChgConfirmStart()
        {
            this.Output(0x1bbe);
            return;
        }

        public void OnUnitChgSelectEnd()
        {
            this.Output(0x1bbd);
            return;
        }

        public void OnUnitChgSelectStart()
        {
            this.Output(0x1bbc);
            return;
        }

        public void OnUnitStart()
        {
            this.Output(0x424);
            return;
        }

        public void OnVersusDraw()
        {
            FlowNode_TriggerLocalEvent.TriggerLocalEvent(this, this.QuestDraw_Versus);
            return;
        }

        public void OnVersusEnd()
        {
            FlowNode_TriggerLocalEvent.TriggerLocalEvent(this, this.VersusEnd);
            return;
        }

        public void OnVersusLose()
        {
            FlowNode_TriggerLocalEvent.TriggerLocalEvent(this, this.QuestLose_Versus);
            return;
        }

        public void OnVersusStart()
        {
            FlowNode_TriggerLocalEvent.TriggerLocalEvent(this, this.VersusStart);
            return;
        }

        public void OnVersusWin()
        {
            FlowNode_TriggerLocalEvent.TriggerLocalEvent(this, this.QuestWin_Versus);
            return;
        }

        private void Output(int pinID)
        {
            base.ActivateOutputLinks(pinID);
            return;
        }

        public void Show()
        {
            CanvasGroup group;
            group = base.GetComponent<CanvasGroup>();
            if ((group != null) == null)
            {
                goto Label_0025;
            }
            group.set_alpha(1f);
            group.set_blocksRaycasts(1);
        Label_0025:
            return;
        }

        private void Start()
        {
            if ((this.Prefab_TargetMain != null) == null)
            {
                goto Label_0039;
            }
            this.TargetMain = Object.Instantiate<TargetPlate>(this.Prefab_TargetMain);
            this.TargetMain.get_transform().SetParent(base.get_transform(), 0);
        Label_0039:
            if ((this.Prefab_TargetSub != null) == null)
            {
                goto Label_0072;
            }
            this.TargetSub = Object.Instantiate<TargetPlate>(this.Prefab_TargetSub);
            this.TargetSub.get_transform().SetParent(base.get_transform(), 0);
        Label_0072:
            if ((this.Prefab_ObjectTarget != null) == null)
            {
                goto Label_00AB;
            }
            this.TargetObjectSub = Object.Instantiate<TargetPlate>(this.Prefab_ObjectTarget);
            this.TargetObjectSub.get_transform().SetParent(base.get_transform(), 0);
        Label_00AB:
            if ((this.Prefab_TrickTarget != null) == null)
            {
                goto Label_00E4;
            }
            this.TargetTrickSub = Object.Instantiate<TargetPlate>(this.Prefab_TrickTarget);
            this.TargetTrickSub.get_transform().SetParent(base.get_transform(), 0);
        Label_00E4:
            return;
        }

        private T UpdateReference<T>(ref T obj, string path) where T: Component
        {
            object[] objArray1;
            GameObject obj2;
            if ((((T) *(obj)) == null) == null)
            {
                goto Label_00A9;
            }
            obj2 = GameObjectID.FindGameObject(path);
            if ((obj2 == null) == null)
            {
                goto Label_0040;
            }
            Debug.LogError(path + " は存在しません");
            return (T) null;
        Label_0040:
            *(obj) = (T) obj2.GetComponent(typeof(T));
            if ((((T) *(obj)) == null) == null)
            {
                goto Label_00A9;
            }
            objArray1 = new object[] { path, " は ", typeof(T), " を含みません" };
            Debug.LogError(string.Concat(objArray1));
            return (T) null;
        Label_00A9:
            return *(obj);
        }

        public VirtualStick2 VirtualStick
        {
            get
            {
                return this.UpdateReference<VirtualStick2>(&this.mVirtualStick, this.VirtualStickID);
            }
        }

        public UnitAbilitySkillList SkillWindow
        {
            get
            {
                return this.UpdateReference<UnitAbilitySkillList>(&this.mSkillListRef, this.SkillListID);
            }
        }

        public UnitCommands CommandWindow
        {
            get
            {
                return this.UpdateReference<UnitCommands>(&this.mCommandWindow, this.CommandObjectID);
            }
        }

        public BattleInventory ItemWindow
        {
            get
            {
                return this.UpdateReference<BattleInventory>(&this.mItemWindow, this.ItemListID);
            }
        }

        public BattleUnitChg UnitChgWindow
        {
            get
            {
                return this.UpdateReference<BattleUnitChg>(&this.mUnitChgWindow, this.UnitChgListID);
            }
        }

        public bool IsInputDirectionStarted
        {
            [CompilerGenerated]
            get
            {
                return this.<IsInputDirectionStarted>k__BackingField;
            }
            [CompilerGenerated]
            private set
            {
                this.<IsInputDirectionStarted>k__BackingField = value;
                return;
            }
        }
    }
}

