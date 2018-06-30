namespace SRPG
{
    using GR;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;
    using UnityEngine;

    [Pin(0x20, "クリア演出終了", 1, 0x20), Pin(11, "クリア演出開始", 0, 11), Pin(0xc9, "ミッション不達成（敵が復活した）", 1, 0xc9), Pin(200, "ミッション不達成", 1, 200)]
    public class TowerQuestResult : QuestResult
    {
        private const int INPUT_START_EFFECT = 11;
        private const int OUTPUT_END_EFFECT = 0x20;
        private const int OUTPUT_MISSION_FAILURE = 200;
        private const int OUTPUT_MISSION_FAILURE_RESET = 0xc9;
        [Description("塔報酬画面用入手アイテムのゲームオブジェクト"), SerializeField, HeaderBar("▼ヴェーダ用")]
        private GameObject TowerTreasureListItem;
        [SerializeField]
        private GameObject m_RewardPanel;
        [SerializeField, Description("クリア条件の星にトリガーを送る間隔 (秒数)")]
        private float TowerItem_TriggerInterval;
        [SerializeField]
        private float DeadAlpha;
        [SerializeField]
        private SRPG.TowerClear TowerClear;
        [SerializeField]
        private GameObject Window;
        [SerializeField]
        private string PreRewardAnimationTrigger;
        [SerializeField]
        private string PostRewardAnimationTrigger;
        [SerializeField]
        private float PreRewardAnimationDelay;
        [SerializeField]
        private float PostRewardAnimationDelay;
        private List<GameObject> mTowerListItems;
        private List<HpBarSlider> mHpBar;
        private List<TowerUnitIsDead> canvas_group;
        private bool mContinueTowerItemAnimation;
        private bool mContinueTowerItem;
        private BattleCore.Record m_QuestRecord;
        private eTowerResultFlags m_TowerResultFlags;

        public TowerQuestResult()
        {
            this.TowerItem_TriggerInterval = 1f;
            this.DeadAlpha = 0.5f;
            this.mTowerListItems = new List<GameObject>();
            this.mHpBar = new List<HpBarSlider>();
            this.canvas_group = new List<TowerUnitIsDead>();
            base..ctor();
            return;
        }

        public override void Activated(int pinID)
        {
            if ((SceneBattle.Instance != null) == null)
            {
                goto Label_0034;
            }
            if (SceneBattle.Instance.Battle == null)
            {
                goto Label_0034;
            }
            this.m_QuestRecord = SceneBattle.Instance.Battle.GetQuestRecord();
        Label_0034:
            this.UpdateResultFlags();
            if (pinID != 11)
            {
                goto Label_0088;
            }
            if (this.ResultFlags_IsOn(8) == null)
            {
                goto Label_007B;
            }
            this.Window.SetActive(0);
            this.TowerClear.Refresh();
            this.TowerClear.get_gameObject().SetActive(1);
            goto Label_0083;
        Label_007B:
            FlowNode_GameObject.ActivateOutputLinks(this, 0x20);
        Label_0083:
            goto Label_008F;
        Label_0088:
            base.Activated(pinID);
        Label_008F:
            return;
        }

        [DebuggerHidden]
        public override IEnumerator AddExp()
        {
            <AddExp>c__Iterator141 iterator;
            iterator = new <AddExp>c__Iterator141();
            iterator.<>f__this = this;
            return iterator;
        }

        public override void AddExpPlayer()
        {
            Transform transform;
            int num;
            GameObject obj2;
            Unit unit;
            TowerResuponse.PlayerUnit unit2;
            GameObject obj3;
            GameObject obj4;
            CanvasGroup group;
            <AddExpPlayer>c__AnonStorey3B0 storeyb;
            transform = ((base.UnitList != null) == null) ? base.UnitListItem.get_transform().get_parent() : base.UnitList.get_transform();
            num = 0;
            goto Label_00AE;
        Label_0039:
            obj2 = Object.Instantiate<GameObject>(base.UnitListItem);
            obj2.get_transform().SetParent(transform, 0);
            this.canvas_group.Add(obj2.GetComponentInChildren<TowerUnitIsDead>());
            base.mUnitListItems.Add(obj2);
            if (base.mCurrentQuest.type != 7)
            {
                goto Label_0091;
            }
            this.mHpBar.Add(obj2.GetComponentInChildren<HpBarSlider>());
        Label_0091:
            DataSource.Bind<UnitData>(obj2, base.mUnits[num]);
            obj2.SetActive(1);
            num += 1;
        Label_00AE:
            if (num < base.mUnits.Count)
            {
                goto Label_0039;
            }
            if (base.mCurrentQuest.type != 7)
            {
                goto Label_0265;
            }
            storeyb = new <AddExpPlayer>c__AnonStorey3B0();
            storeyb.<>f__this = this;
            storeyb.i = 0;
            goto Label_024E;
        Label_00EC:
            unit = SceneBattle.Instance.Battle.Player.Find(new Predicate<Unit>(storeyb.<>m__42A));
            if (unit != null)
            {
                goto Label_0119;
            }
            goto Label_023E;
        Label_0119:
            if (MonoSingleton<GameManager>.Instance.Player.FindUnitDataByUniqueID(unit.UnitData.UniqueID) != null)
            {
                goto Label_013D;
            }
            goto Label_023E;
        Label_013D:
            this.mHpBar[storeyb.i].slider.set_maxValue((float) unit.TowerStartHP);
            this.mHpBar[storeyb.i].slider.set_minValue(0f);
            this.mHpBar[storeyb.i].slider.set_value((float) unit.CurrentStatus.param.hp);
            unit2 = MonoSingleton<GameManager>.Instance.TowerResuponse.FindPlayerUnit(base.mUnits[storeyb.i]);
            if (unit2 == null)
            {
                goto Label_0227;
            }
            if (unit2.isDied == null)
            {
                goto Label_0227;
            }
            obj3 = base.mUnitListItems[storeyb.i];
            obj3.get_transform().GetChild(0).get_gameObject().GetComponent<CanvasGroup>().set_alpha(this.DeadAlpha);
        Label_0227:
            GameParameter.UpdateAll(base.mUnitListItems[storeyb.i]);
        Label_023E:
            storeyb.i += 1;
        Label_024E:
            if (storeyb.i < this.mHpBar.Count)
            {
                goto Label_00EC;
            }
        Label_0265:
            return;
        }

        public override void CreateItemObject(List<QuestResult.DropItemData> items, Transform parent)
        {
            this.SetTowerResult(this.TowerTreasureListItem.get_transform().get_parent(), this.TowerTreasureListItem, null);
            return;
        }

        public void EndTowerClear()
        {
            FlowNode_GameObject.ActivateOutputLinks(this, 0x20);
            return;
        }

        private bool IsHealEnd(List<HealParanm> param)
        {
            int num;
            num = 0;
            goto Label_002A;
        Label_0007:
            if (param[num].hp_heal <= param[num].hpGained)
            {
                goto Label_0026;
            }
            return 0;
        Label_0026:
            num += 1;
        Label_002A:
            if (num < param.Count)
            {
                goto Label_0007;
            }
            return 1;
        }

        public void OnTowerItemAnimationEnd()
        {
            this.mContinueTowerItemAnimation = 1;
            return;
        }

        public void OnTowerItemEnd()
        {
            this.mContinueTowerItem = 1;
            return;
        }

        [DebuggerHidden]
        public override IEnumerator PlayAnimationAsync()
        {
            <PlayAnimationAsync>c__Iterator142 iterator;
            iterator = new <PlayAnimationAsync>c__Iterator142();
            iterator.<>f__this = this;
            return iterator;
        }

        [DebuggerHidden]
        private IEnumerator RecvHealAnimation()
        {
            <RecvHealAnimation>c__Iterator140 iterator;
            iterator = new <RecvHealAnimation>c__Iterator140();
            iterator.<>f__this = this;
            return iterator;
        }

        private bool ResultFlags_IsOff(eTowerResultFlags flags)
        {
            return ((this.m_TowerResultFlags & flags) == 0);
        }

        private bool ResultFlags_IsOn(eTowerResultFlags flags)
        {
            return (((this.m_TowerResultFlags & flags) == 0) == 0);
        }

        private void ResultFlags_Set(eTowerResultFlags flags, bool value)
        {
            if (value == null)
            {
                goto Label_0012;
            }
            this.ResultFlags_SetOn(flags);
            goto Label_0019;
        Label_0012:
            this.ResultFlags_SetOff(flags);
        Label_0019:
            return;
        }

        private void ResultFlags_SetOff(eTowerResultFlags flags)
        {
            this.m_TowerResultFlags &= ~flags;
            return;
        }

        private void ResultFlags_SetOn(eTowerResultFlags flags)
        {
            this.m_TowerResultFlags |= flags;
            return;
        }

        private void SetAnimationBool(string name, bool value)
        {
            if (string.IsNullOrEmpty(name) != null)
            {
                goto Label_0029;
            }
            if ((base.MainAnimator != null) == null)
            {
                goto Label_0029;
            }
            base.MainAnimator.SetBool(name, value);
        Label_0029:
            return;
        }

        private void SetTowerResult(Transform parent, GameObject ItemObject, List<ItemData> data)
        {
            TowerFloorParam param;
            TowerRewardParam param2;
            List<TowerRewardItem> list;
            int num;
            ItemData data2;
            TowerRewardItem item;
            int num2;
            GameObject obj2;
            GameParameter[] parameterArray;
            int num3;
            TowerRewardUI dui;
            ArtifactParam param3;
            ArtifactIcon icon;
            ArtifactData data3;
            GameObject obj3;
            RectTransform transform;
            <SetTowerResult>c__AnonStorey3AF storeyaf;
            if (base.mCurrentQuest.type != 7)
            {
                goto Label_02C7;
            }
            param = MonoSingleton<GameManager>.Instance.FindTowerFloor(GlobalVars.SelectedQuestID);
            list = new List<TowerRewardItem>(MonoSingleton<GameManager>.Instance.FindTowerReward(param.reward_id).GetTowerRewardItem());
            if (data == null)
            {
                goto Label_00C8;
            }
            num = 0;
            goto Label_00BC;
        Label_004B:
            data2 = data[num];
            if (data2 != null)
            {
                goto Label_0060;
            }
            goto Label_00B8;
        Label_0060:
            item = new TowerRewardItem();
            item.iname = data2.Param.iname;
            item.type = 0;
            item.num = data[num].Num;
            item.visible = 1;
            item.is_new = data[num].IsNew;
            list.Add(item);
        Label_00B8:
            num += 1;
        Label_00BC:
            if (num < data.Count)
            {
                goto Label_004B;
            }
        Label_00C8:
            num2 = 0;
            goto Label_02B3;
        Label_00D0:
            storeyaf = new <SetTowerResult>c__AnonStorey3AF();
            storeyaf.item = list[num2];
            if (storeyaf.item.visible == null)
            {
                goto Label_02AD;
            }
            if (storeyaf.item.type != 1)
            {
                goto Label_010E;
            }
            goto Label_02AD;
        Label_010E:
            obj2 = Object.Instantiate<GameObject>(ItemObject);
            obj2.get_transform().SetParent(parent, 0);
            this.mTowerListItems.Add(obj2);
            obj2.get_transform().set_localScale(ItemObject.get_transform().get_localScale());
            DataSource.Bind<TowerRewardItem>(obj2, list[num2]);
            obj2.SetActive(1);
            parameterArray = obj2.GetComponentsInChildren<GameParameter>();
            num3 = 0;
            goto Label_0182;
        Label_0170:
            parameterArray[num3].Index = num2;
            num3 += 1;
        Label_0182:
            if (num3 < ((int) parameterArray.Length))
            {
                goto Label_0170;
            }
            dui = obj2.GetComponentInChildren<TowerRewardUI>();
            if ((dui != null) == null)
            {
                goto Label_01AA;
            }
            dui.Refresh();
        Label_01AA:
            if (storeyaf.item.type != 6)
            {
                goto Label_0248;
            }
            param3 = MonoSingleton<GameManager>.Instance.MasterParam.GetArtifactParam(storeyaf.item.iname);
            DataSource.Bind<ArtifactParam>(obj2, param3);
            icon = obj2.GetComponentInChildren<ArtifactIcon>();
            if ((icon == null) == null)
            {
                goto Label_01FD;
            }
            goto Label_02C0;
        Label_01FD:
            icon.set_enabled(1);
            icon.UpdateValue();
            if (MonoSingleton<GameManager>.Instance.Player.Artifacts.Find(new Predicate<ArtifactData>(storeyaf.<>m__429)) != null)
            {
                goto Label_02C0;
            }
            storeyaf.item.is_new = 1;
            goto Label_02C0;
        Label_0248:
            if ((base.Prefab_NewItemBadge != null) == null)
            {
                goto Label_02AD;
            }
            if (storeyaf.item.is_new == null)
            {
                goto Label_02AD;
            }
            transform = Object.Instantiate<GameObject>(base.Prefab_NewItemBadge).get_transform() as RectTransform;
            transform.get_gameObject().SetActive(1);
            transform.set_anchoredPosition(Vector2.get_zero());
            transform.SetParent(obj2.get_transform(), 0);
        Label_02AD:
            num2 += 1;
        Label_02B3:
            if (num2 < list.Count)
            {
                goto Label_00D0;
            }
        Label_02C0:
            ItemObject.SetActive(0);
        Label_02C7:
            return;
        }

        [DebuggerHidden]
        public IEnumerator StartTowerTreasureAnimation()
        {
            <StartTowerTreasureAnimation>c__Iterator143 iterator;
            iterator = new <StartTowerTreasureAnimation>c__Iterator143();
            iterator.<>f__this = this;
            return iterator;
        }

        [DebuggerHidden]
        protected IEnumerator TowerTreasureAnimation(List<GameObject> ListItems)
        {
            <TowerTreasureAnimation>c__Iterator144 iterator;
            iterator = new <TowerTreasureAnimation>c__Iterator144();
            iterator.ListItems = ListItems;
            iterator.<$>ListItems = ListItems;
            iterator.<>f__this = this;
            return iterator;
        }

        private void UpdateResultFlags()
        {
            TowerResuponse resuponse;
            TowerFloorParam param;
            TowerParam param2;
            bool flag;
            bool[] flagArray;
            int num;
            if (base.mCurrentQuest == null)
            {
                goto Label_0138;
            }
            resuponse = MonoSingleton<GameManager>.Instance.TowerResuponse;
            param = MonoSingleton<GameManager>.Instance.FindTowerFloor(base.mQuestName);
            param2 = MonoSingleton<GameManager>.Instance.FindTower(param.tower_id);
            if (this.m_QuestRecord.result != 1)
            {
                goto Label_0055;
            }
            this.ResultFlags_SetOn(1);
            goto Label_005C;
        Label_0055:
            this.ResultFlags_SetOn(2);
        Label_005C:
            if (base.mCurrentQuest.HasMission() == null)
            {
                goto Label_0102;
            }
            flag = 1;
            flagArray = new bool[this.m_QuestRecord.bonusCount];
            num = 0;
            goto Label_00D5;
        Label_0088:
            if ((this.m_QuestRecord.bonusFlags & (1 << (num & 0x1f))) == null)
            {
                goto Label_00A6;
            }
            flagArray[num] = 1;
        Label_00A6:
            if (base.mCurrentQuest.IsMissionClear(num) == null)
            {
                goto Label_00BE;
            }
            flagArray[num] = 1;
        Label_00BE:
            if (flagArray[num] != null)
            {
                goto Label_00CF;
            }
            flag = 0;
            goto Label_00E7;
        Label_00CF:
            num += 1;
        Label_00D5:
            if (num < this.m_QuestRecord.bonusCount)
            {
                goto Label_0088;
            }
        Label_00E7:
            if (flag == null)
            {
                goto Label_00FA;
            }
            this.ResultFlags_SetOn(0x20);
            goto Label_0102;
        Label_00FA:
            this.ResultFlags_SetOn(0x10);
        Label_0102:
            if (resuponse.IsNotClear != null)
            {
                goto Label_0123;
            }
            if (resuponse.IsRoundClear != null)
            {
                goto Label_0123;
            }
            if (param2.is_view_ranking != null)
            {
                goto Label_0130;
            }
        Label_0123:
            this.ResultFlags_Set(8, 0);
            goto Label_0138;
        Label_0130:
            this.ResultFlags_Set(8, 1);
        Label_0138:
            return;
        }

        private bool IsEnableMission
        {
            get
            {
                if (this.ResultFlags_IsOn(0x20) == null)
                {
                    goto Label_000F;
                }
                return 1;
            Label_000F:
                if (this.ResultFlags_IsOn(0x10) == null)
                {
                    goto Label_001E;
                }
                return 1;
            Label_001E:
                return 0;
            }
        }

        private bool IsUnlockNextFloor
        {
            get
            {
                if (this.ResultFlags_IsOn(0x20) == null)
                {
                    goto Label_001B;
                }
                if (this.ResultFlags_IsOn(1) == null)
                {
                    goto Label_001B;
                }
                return 1;
            Label_001B:
                if (this.IsEnableMission != null)
                {
                    goto Label_0028;
                }
                return 1;
            Label_0028:
                return 0;
            }
        }

        private bool IsEnemyReset
        {
            get
            {
                if (this.ResultFlags_IsOn(0x10) == null)
                {
                    goto Label_001B;
                }
                if (this.ResultFlags_IsOn(1) == null)
                {
                    goto Label_001B;
                }
                return 1;
            Label_001B:
                return 0;
            }
        }

        [CompilerGenerated]
        private sealed class <AddExp>c__Iterator141 : IEnumerator, IDisposable, IEnumerator<object>
        {
            internal int $PC;
            internal object $current;
            internal TowerQuestResult <>f__this;

            public <AddExp>c__Iterator141()
            {
                base..ctor();
                return;
            }

            [DebuggerHidden]
            public void Dispose()
            {
                this.$PC = -1;
                return;
            }

            public bool MoveNext()
            {
                uint num;
                bool flag;
                num = this.$PC;
                this.$PC = -1;
                switch (num)
                {
                    case 0:
                        goto Label_002D;

                    case 1:
                        goto Label_0064;

                    case 2:
                        goto Label_008C;

                    case 3:
                        goto Label_00B4;

                    case 4:
                        goto Label_00F6;
                }
                goto Label_00FD;
            Label_002D:
                if (this.<>f__this.PreExpAnimationDelay <= 0f)
                {
                    goto Label_0064;
                }
                this.$current = new WaitForSeconds(this.<>f__this.PreExpAnimationDelay);
                this.$PC = 1;
                goto Label_00FF;
            Label_0064:
                this.$current = this.<>f__this.StartCoroutine(this.<>f__this.RecvExpAnimation());
                this.$PC = 2;
                goto Label_00FF;
            Label_008C:
                this.$current = this.<>f__this.StartCoroutine(this.<>f__this.RecvHealAnimation());
                this.$PC = 3;
                goto Label_00FF;
            Label_00B4:
                this.<>f__this.SetExpAnimationEnd();
                if (this.<>f__this.PostExpAnimationDelay <= 0f)
                {
                    goto Label_00F6;
                }
                this.$current = new WaitForSeconds(this.<>f__this.PostExpAnimationDelay);
                this.$PC = 4;
                goto Label_00FF;
            Label_00F6:
                this.$PC = -1;
            Label_00FD:
                return 0;
            Label_00FF:
                return 1;
                return flag;
            }

            [DebuggerHidden]
            public void Reset()
            {
                throw new NotSupportedException();
            }

            object IEnumerator<object>.Current
            {
                [DebuggerHidden]
                get
                {
                    return this.$current;
                }
            }

            object IEnumerator.Current
            {
                [DebuggerHidden]
                get
                {
                    return this.$current;
                }
            }
        }

        [CompilerGenerated]
        private sealed class <AddExpPlayer>c__AnonStorey3B0
        {
            internal int i;
            internal TowerQuestResult <>f__this;

            public <AddExpPlayer>c__AnonStorey3B0()
            {
                base..ctor();
                return;
            }

            internal bool <>m__42A(Unit x)
            {
                return (x.UnitData.UniqueID == this.<>f__this.mUnits[this.i].UniqueID);
            }
        }

        [CompilerGenerated]
        private sealed class <PlayAnimationAsync>c__Iterator142 : IEnumerator, IDisposable, IEnumerator<object>
        {
            internal int <i>__0;
            internal int[] <oldMemberLv>__1;
            internal int <i>__2;
            internal PlayerData <pd>__3;
            internal int <rest>__4;
            internal int $PC;
            internal object $current;
            internal TowerQuestResult <>f__this;

            public <PlayAnimationAsync>c__Iterator142()
            {
                base..ctor();
                return;
            }

            [DebuggerHidden]
            public void Dispose()
            {
                this.$PC = -1;
                return;
            }

            public bool MoveNext()
            {
                uint num;
                bool flag;
                num = this.$PC;
                this.$PC = -1;
                switch (num)
                {
                    case 0:
                        goto Label_0049;

                    case 1:
                        goto Label_00E1;

                    case 2:
                        goto Label_0109;

                    case 3:
                        goto Label_0121;

                    case 4:
                        goto Label_0168;

                    case 5:
                        goto Label_0203;

                    case 6:
                        goto Label_0220;

                    case 7:
                        goto Label_0238;

                    case 8:
                        goto Label_02F0;

                    case 9:
                        goto Label_0480;

                    case 10:
                        goto Label_04FC;

                    case 11:
                        goto Label_0534;
                }
                goto Label_055E;
            Label_0049:
                if (this.<>f__this.IsEnableMission == null)
                {
                    goto Label_0183;
                }
                GameUtility.SetGameObjectActive(this.<>f__this.mMissionDetail, 1);
                if (this.<>f__this.mResultData.StartBonusFlags == this.<>f__this.mResultData.Record.bonusFlags)
                {
                    goto Label_0194;
                }
                this.<>f__this.TriggerAnimation(this.<>f__this.PreStarAnimationTrigger);
                if (this.<>f__this.PreStarAnimationDelay <= 0f)
                {
                    goto Label_00E1;
                }
                this.$current = new WaitForSeconds(this.<>f__this.PreStarAnimationDelay);
                this.$PC = 1;
                goto Label_0560;
            Label_00E1:
                this.$current = this.<>f__this.StartCoroutine(this.<>f__this.ObjectiveAnimation());
                this.$PC = 2;
                goto Label_0560;
            Label_0109:
                goto Label_0121;
            Label_010E:
                this.$current = null;
                this.$PC = 3;
                goto Label_0560;
            Label_0121:
                if (this.<>f__this.mContinueStarAnimation == null)
                {
                    goto Label_010E;
                }
                if (this.<>f__this.PostStarAnimationDelay <= 0f)
                {
                    goto Label_0168;
                }
                this.$current = new WaitForSeconds(this.<>f__this.PostStarAnimationDelay);
                this.$PC = 4;
                goto Label_0560;
            Label_0168:
                this.<>f__this.TriggerAnimation(this.<>f__this.PostStarAnimationTrigger);
                goto Label_0194;
            Label_0183:
                GameUtility.SetGameObjectActive(this.<>f__this.mMissionDetail, 0);
            Label_0194:
                if (this.<>f__this.IsUnlockNextFloor == null)
                {
                    goto Label_030B;
                }
                GameUtility.SetGameObjectActive(this.<>f__this.m_RewardPanel, 1);
                this.<>f__this.SetAnimationBool(this.<>f__this.PreRewardAnimationTrigger, 1);
                if (this.<>f__this.PreRewardAnimationDelay <= 0f)
                {
                    goto Label_0203;
                }
                this.$current = new WaitForSeconds(this.<>f__this.PreRewardAnimationDelay);
                this.$PC = 5;
                goto Label_0560;
            Label_0203:
                this.$current = this.<>f__this.StartTowerTreasureAnimation();
                this.$PC = 6;
                goto Label_0560;
            Label_0220:
                goto Label_0238;
            Label_0225:
                this.$current = null;
                this.$PC = 7;
                goto Label_0560;
            Label_0238:
                if (this.<>f__this.mContinueTowerItemAnimation == null)
                {
                    goto Label_0225;
                }
                this.<>f__this.SetAnimationBool(this.<>f__this.PostRewardAnimationTrigger, 1);
                this.<i>__0 = 0;
                goto Label_029E;
            Label_026B:
                this.<>f__this.canvas_group[this.<i>__0].canvas_group.set_alpha(1f);
                this.<i>__0 += 1;
            Label_029E:
                if (this.<i>__0 < this.<>f__this.canvas_group.Count)
                {
                    goto Label_026B;
                }
                if (this.<>f__this.PostRewardAnimationDelay <= 0f)
                {
                    goto Label_02F0;
                }
                this.$current = new WaitForSeconds(this.<>f__this.PostRewardAnimationDelay);
                this.$PC = 8;
                goto Label_0560;
            Label_02F0:
                this.<>f__this.TriggerAnimation(this.<>f__this.PostStarAnimationTrigger);
                goto Label_032D;
            Label_030B:
                this.<>f__this.Window.SetActive(0);
                GameUtility.SetGameObjectActive(this.<>f__this.m_RewardPanel, 0);
            Label_032D:
                this.<oldMemberLv>__1 = new int[this.<>f__this.mUnits.Count];
                this.<i>__2 = 0;
                goto Label_038A;
            Label_0354:
                this.<oldMemberLv>__1[this.<i>__2] = this.<>f__this.mUnits[this.<i>__2].Lv;
                this.<i>__2 += 1;
            Label_038A:
                if (this.<i>__2 < this.<>f__this.mUnits.Count)
                {
                    goto Label_0354;
                }
                if ((this.<>f__this.ResultSkipButton != null) == null)
                {
                    goto Label_03D1;
                }
                this.<>f__this.ResultSkipButton.get_gameObject().SetActive(1);
            Label_03D1:
                if (this.<>f__this.IsEnemyReset == null)
                {
                    goto Label_03F6;
                }
                FlowNode_GameObject.ActivateOutputLinks(this.<>f__this, 0xc9);
                goto Label_0557;
            Label_03F6:
                if (this.<>f__this.IsUnlockNextFloor != null)
                {
                    goto Label_041B;
                }
                FlowNode_GameObject.ActivateOutputLinks(this.<>f__this, 200);
                goto Label_0557;
            Label_041B:
                this.<>f__this.StartCoroutine(this.<>f__this.AddExp());
                this.<>f__this.TriggerAnimation(this.<>f__this.PreItemAnimationTrigger);
                if (this.<>f__this.PreItemAnimationDelay <= 0f)
                {
                    goto Label_0480;
                }
                this.$current = new WaitForSeconds(this.<>f__this.PreItemAnimationDelay);
                this.$PC = 9;
                goto Label_0560;
            Label_0480:
                this.<pd>__3 = MonoSingleton<GameManager>.Instance.Player;
                this.<rest>__4 = this.<pd>__3.ChallengeMultiMax - this.<pd>__3.ChallengeMultiNum;
                if (this.<>f__this.mCurrentQuest == null)
                {
                    goto Label_04DE;
                }
                if (this.<>f__this.mCurrentQuest.IsMulti == null)
                {
                    goto Label_04DE;
                }
                if (this.<rest>__4 < 0)
                {
                    goto Label_04FC;
                }
            Label_04DE:
                this.$current = this.<>f__this.StartTreasureAnimation();
                this.$PC = 10;
                goto Label_0560;
            Label_04FC:
                if (this.<>f__this.PostItemAnimationDelay <= 0f)
                {
                    goto Label_0534;
                }
                this.$current = new WaitForSeconds(this.<>f__this.PostItemAnimationDelay);
                this.$PC = 11;
                goto Label_0560;
            Label_0534:
                this.<>f__this.TriggerAnimation(this.<>f__this.PostItemAnimationTrigger);
                FlowNode_GameObject.ActivateOutputLinks(this.<>f__this, 0x1f);
            Label_0557:
                this.$PC = -1;
            Label_055E:
                return 0;
            Label_0560:
                return 1;
                return flag;
            }

            [DebuggerHidden]
            public void Reset()
            {
                throw new NotSupportedException();
            }

            object IEnumerator<object>.Current
            {
                [DebuggerHidden]
                get
                {
                    return this.$current;
                }
            }

            object IEnumerator.Current
            {
                [DebuggerHidden]
                get
                {
                    return this.$current;
                }
            }
        }

        [CompilerGenerated]
        private sealed class <RecvHealAnimation>c__Iterator140 : IEnumerator, IDisposable, IEnumerator<object>
        {
            internal GameManager <gm>__0;
            internal TowerFloorParam <FloorParam>__1;
            internal List<TowerQuestResult.HealParanm> <heal_list>__2;
            internal bool <is_heal>__3;
            internal int <i>__4;
            internal Unit <unit>__5;
            internal TowerQuestResult.HealParanm <param>__6;
            internal int <i>__7;
            internal int $PC;
            internal object $current;
            internal TowerQuestResult <>f__this;

            public <RecvHealAnimation>c__Iterator140()
            {
                base..ctor();
                return;
            }

            [DebuggerHidden]
            public void Dispose()
            {
                this.$PC = -1;
                return;
            }

            public bool MoveNext()
            {
                uint num;
                bool flag;
                num = this.$PC;
                this.$PC = -1;
                switch (num)
                {
                    case 0:
                        goto Label_0021;

                    case 1:
                        goto Label_01EF;
                }
                goto Label_020C;
            Label_0021:
                this.<gm>__0 = MonoSingleton<GameManager>.Instance;
                this.<FloorParam>__1 = this.<gm>__0.FindTowerFloor(this.<>f__this.mQuestName);
                this.<heal_list>__2 = new List<TowerQuestResult.HealParanm>();
                this.<is_heal>__3 = 0;
                this.<i>__4 = 0;
                goto Label_014F;
            Label_0066:
                this.<unit>__5 = SceneBattle.Instance.Battle.Player[this.<i>__4];
                if (this.<unit>__5 != null)
                {
                    goto Label_0096;
                }
                goto Label_0141;
            Label_0096:
                if (this.<gm>__0.Player.FindUnitDataByUniqueID(this.<unit>__5.UnitData.UniqueID) != null)
                {
                    goto Label_00C0;
                }
                goto Label_0141;
            Label_00C0:
                if (this.<unit>__5.IsDead == null)
                {
                    goto Label_00D5;
                }
                goto Label_0141;
            Label_00D5:
                this.<param>__6 = new TowerQuestResult.HealParanm(this.<unit>__5, this.<FloorParam>__1, this.<>f__this.mHpBar[this.<i>__4], this.<>f__this.ExpGainRate, this.<>f__this.ExpGainTimeMax);
                this.<heal_list>__2.Add(this.<param>__6);
                if (this.<param>__6.hp_heal <= 0)
                {
                    goto Label_0141;
                }
                this.<is_heal>__3 = 1;
            Label_0141:
                this.<i>__4 += 1;
            Label_014F:
                if (this.<i>__4 < SceneBattle.Instance.Battle.Player.Count)
                {
                    goto Label_0066;
                }
                if (this.<is_heal>__3 == null)
                {
                    goto Label_01EF;
                }
                MonoSingleton<MySound>.Instance.PlaySEOneShot("SE_0507", 0f);
                goto Label_01EF;
            Label_0192:
                this.<i>__7 = 0;
                goto Label_01C2;
            Label_019E:
                this.<heal_list>__2[this.<i>__7].Update();
                this.<i>__7 += 1;
            Label_01C2:
                if (this.<i>__7 < this.<heal_list>__2.Count)
                {
                    goto Label_019E;
                }
                this.$current = new WaitForEndOfFrame();
                this.$PC = 1;
                goto Label_020E;
            Label_01EF:
                if (this.<>f__this.IsHealEnd(this.<heal_list>__2) == null)
                {
                    goto Label_0192;
                }
                this.$PC = -1;
            Label_020C:
                return 0;
            Label_020E:
                return 1;
                return flag;
            }

            [DebuggerHidden]
            public void Reset()
            {
                throw new NotSupportedException();
            }

            object IEnumerator<object>.Current
            {
                [DebuggerHidden]
                get
                {
                    return this.$current;
                }
            }

            object IEnumerator.Current
            {
                [DebuggerHidden]
                get
                {
                    return this.$current;
                }
            }
        }

        [CompilerGenerated]
        private sealed class <SetTowerResult>c__AnonStorey3AF
        {
            internal TowerRewardItem item;

            public <SetTowerResult>c__AnonStorey3AF()
            {
                base..ctor();
                return;
            }

            internal bool <>m__429(ArtifactData x)
            {
                return (x.ArtifactParam.iname == this.item.iname);
            }
        }

        [CompilerGenerated]
        private sealed class <StartTowerTreasureAnimation>c__Iterator143 : IEnumerator, IDisposable, IEnumerator<object>
        {
            internal int $PC;
            internal object $current;
            internal TowerQuestResult <>f__this;

            public <StartTowerTreasureAnimation>c__Iterator143()
            {
                base..ctor();
                return;
            }

            [DebuggerHidden]
            public void Dispose()
            {
                this.$PC = -1;
                return;
            }

            public bool MoveNext()
            {
                uint num;
                bool flag;
                num = this.$PC;
                this.$PC = -1;
                switch (num)
                {
                    case 0:
                        goto Label_0021;

                    case 1:
                        goto Label_0054;
                }
                goto Label_005B;
            Label_0021:
                this.$current = this.<>f__this.StartCoroutine(this.<>f__this.TowerTreasureAnimation(this.<>f__this.mTowerListItems));
                this.$PC = 1;
                goto Label_005D;
            Label_0054:
                this.$PC = -1;
            Label_005B:
                return 0;
            Label_005D:
                return 1;
                return flag;
            }

            [DebuggerHidden]
            public void Reset()
            {
                throw new NotSupportedException();
            }

            object IEnumerator<object>.Current
            {
                [DebuggerHidden]
                get
                {
                    return this.$current;
                }
            }

            object IEnumerator.Current
            {
                [DebuggerHidden]
                get
                {
                    return this.$current;
                }
            }
        }

        [CompilerGenerated]
        private sealed class <TowerTreasureAnimation>c__Iterator144 : IEnumerator, IDisposable, IEnumerator<object>
        {
            internal float <time>__0;
            internal int <currentItem>__1;
            internal List<GameObject> ListItems;
            internal int <i>__2;
            internal int $PC;
            internal object $current;
            internal List<GameObject> <$>ListItems;
            internal TowerQuestResult <>f__this;

            public <TowerTreasureAnimation>c__Iterator144()
            {
                base..ctor();
                return;
            }

            [DebuggerHidden]
            public void Dispose()
            {
                this.$PC = -1;
                return;
            }

            public bool MoveNext()
            {
                uint num;
                bool flag;
                num = this.$PC;
                this.$PC = -1;
                switch (num)
                {
                    case 0:
                        goto Label_0029;

                    case 1:
                        goto Label_011B;

                    case 2:
                        goto Label_0133;

                    case 3:
                        goto Label_01E8;
                }
                goto Label_0205;
            Label_0029:
                this.<time>__0 = 0f;
                this.<currentItem>__1 = 0;
                goto Label_01E8;
            Label_0040:
                this.<time>__0 += Time.get_deltaTime();
                goto Label_01BB;
            Label_0057:
                if (this.<time>__0 < this.<>f__this.Treasure_TriggerInterval)
                {
                    goto Label_01D1;
                }
                if (string.IsNullOrEmpty(this.<>f__this.Treasure_TurnOnTrigger) != null)
                {
                    goto Label_018B;
                }
                MonoSingleton<MySound>.Instance.PlaySEOneShot("SE_0508", 0f);
                GameUtility.SetAnimatorTrigger(this.ListItems[this.<currentItem>__1], this.<>f__this.Treasure_TurnOnTrigger);
                this.<i>__2 = 0;
                goto Label_00E8;
            Label_00C3:
                this.ListItems[this.<i>__2].SetActive(0);
                this.<i>__2 += 1;
            Label_00E8:
                if (this.<i>__2 < this.<currentItem>__1)
                {
                    goto Label_00C3;
                }
                this.$current = (float) this.<>f__this.TowerItem_TriggerInterval;
                this.$PC = 1;
                goto Label_0207;
            Label_011B:
                goto Label_0133;
            Label_0120:
                this.$current = null;
                this.$PC = 2;
                goto Label_0207;
            Label_0133:
                if (this.<>f__this.mContinueTowerItem == null)
                {
                    goto Label_0120;
                }
                if (this.<currentItem>__1 >= (this.ListItems.Count - 1))
                {
                    goto Label_0167;
                }
                this.<>f__this.mContinueTowerItem = 0;
            Label_0167:
                if (this.<currentItem>__1 != (this.ListItems.Count - 1))
                {
                    goto Label_018B;
                }
                this.<>f__this.mContinueTowerItemAnimation = 1;
            Label_018B:
                this.<time>__0 -= this.<>f__this.Treasure_TriggerInterval;
                this.<currentItem>__1 += 1;
                goto Label_01BB;
                goto Label_01D1;
            Label_01BB:
                if (this.<currentItem>__1 < this.ListItems.Count)
                {
                    goto Label_0057;
                }
            Label_01D1:
                this.$current = new WaitForEndOfFrame();
                this.$PC = 3;
                goto Label_0207;
            Label_01E8:
                if (this.<currentItem>__1 < this.ListItems.Count)
                {
                    goto Label_0040;
                }
                this.$PC = -1;
            Label_0205:
                return 0;
            Label_0207:
                return 1;
                return flag;
            }

            [DebuggerHidden]
            public void Reset()
            {
                throw new NotSupportedException();
            }

            object IEnumerator<object>.Current
            {
                [DebuggerHidden]
                get
                {
                    return this.$current;
                }
            }

            object IEnumerator.Current
            {
                [DebuggerHidden]
                get
                {
                    return this.$current;
                }
            }
        }

        [Flags]
        private enum eTowerResultFlags
        {
            Win = 1,
            Lose = 2,
            Clear = 4,
            ShowRank = 8,
            MissionFailure = 0x10,
            MissionComplete = 0x20
        }

        private class HealParanm
        {
            public Unit unit;
            public int hp;
            public int hp_heal;
            public float hp_gainRate;
            public int hpGained;
            public float hpAccumulator;
            public HpBarSlider hp_bar;

            public HealParanm(Unit unit, TowerFloorParam FloorParam, HpBarSlider hp_bar, float GainRate, float GainTimeMax)
            {
                float num;
                base..ctor();
                this.unit = unit;
                this.hp = unit.TowerStartHP;
                this.hp_heal = FloorParam.CalcHelaNum(this.hp);
                this.hp_gainRate = GainRate;
                num = ((float) this.hp_heal) / GainRate;
                if (num <= GainTimeMax)
                {
                    goto Label_0056;
                }
                this.hp_gainRate = ((float) this.hp_heal) / GainTimeMax;
            Label_0056:
                this.hp_bar = hp_bar;
                return;
            }

            public void Update()
            {
                int num;
                num = 0;
                this.hpAccumulator += this.hp_gainRate * Time.get_deltaTime();
                if (this.hpAccumulator < 1f)
                {
                    goto Label_00F6;
                }
                num = Mathf.FloorToInt(this.hpAccumulator);
                this.hpAccumulator = this.hpAccumulator % 1f;
                this.hpGained += num;
                if (this.hp_heal >= this.hpGained)
                {
                    goto Label_008A;
                }
                num = Math.Max(num - (this.hpGained - this.hp_heal), 0);
                this.hpGained = this.hp_heal;
            Label_008A:
                this.unit.Heal(num);
                this.hp_bar.slider.set_value((float) this.unit.CurrentStatus.param.hp);
                this.hp_bar.color.ChangeValue(this.unit.CurrentStatus.param.hp, this.unit.TowerStartHP);
            Label_00F6:
                return;
            }
        }
    }
}

