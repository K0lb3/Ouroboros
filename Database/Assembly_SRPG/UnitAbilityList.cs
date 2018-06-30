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
    using UnityEngine.Events;
    using UnityEngine.EventSystems;
    using UnityEngine.UI;

    [Pin(100, "アビリティ詳細画面の表示", 1, 100)]
    public class UnitAbilityList : MonoBehaviour, IGameParameter, IFlowInterface
    {
        public RefreshTypes RefreshOnStart;
        private RefreshTypes mLastDisplayMode;
        private EAbilitySlot mLastDisplaySlot;
        public bool AutoRefresh;
        public AbilityEvent OnAbilitySelect;
        public AbilityEvent OnAbilityRankUp;
        public AbilityEvent OnRankUpBtnPress;
        public AbilityEvent OnAbilityRankUpExec;
        public AbilitySlotEvent OnSlotSelect;
        public UnitData Unit;
        [Description("アビリティ詳細を表示するアンカー位置 (0.0 - 1.0)")]
        public Vector2 TooltipAnchorPos;
        public GameObject AbilityTooltip;
        [Description("通常状態のアイテムとして使用する雛形")]
        public UnitAbilityListItemEvents Item_Normal;
        [Description("ロック状態のアイテムとして使用する雛形")]
        public UnitAbilityListItemEvents Item_Locked;
        [Description("使用不可スロットの雛形")]
        public UnitAbilityListItemEvents Item_NoSlot;
        [Description("アビリティが割り当てられていないスロットの雛形")]
        public UnitAbilityListItemEvents Item_Empty;
        [Description("スロットが違うアイテムの雛形")]
        public UnitAbilityListItemEvents Item_SlotMismatch;
        [Description("変更できないアビリティのスロットの雛形")]
        public UnitAbilityListItemEvents Item_Fixed;
        private List<UnitAbilityListItemEvents> mItems;
        [Description("固定アビリティを表示する")]
        public bool ShowFixedAbilities;
        [Description("マスターアビリティを表示する")]
        public bool ShowMasterAbilities;
        public bool ShowLockedJobAbilities;
        public ScrollRect ScrollParent;
        private float mDecelerationRate;

        public UnitAbilityList()
        {
            this.TooltipAnchorPos = new Vector2(1f, 0.5f);
            this.mItems = new List<UnitAbilityListItemEvents>();
            base..ctor();
            return;
        }

        private void _OnAbilityDetail(GameObject go)
        {
            AbilityData data;
            AbilityParam param;
            EventSystem system;
            GameSettings settings;
            GameObject obj2;
            bool flag;
            ListItemEvents events;
            GlobalVars.SelectedAbilityID.Set(string.Empty);
            GlobalVars.SelectedAbilityUniqueID.Set(0L);
            data = DataSource.FindDataOfClass<AbilityData>(go, null);
            if (data == null)
            {
                goto Label_0053;
            }
            GlobalVars.SelectedAbilityID.Set(data.Param.iname);
            GlobalVars.SelectedAbilityUniqueID.Set(data.UniqueID);
            goto Label_0077;
        Label_0053:
            param = DataSource.FindDataOfClass<AbilityParam>(go, null);
            GlobalVars.SelectedAbilityID.Set((param == null) ? null : param.iname);
        Label_0077:
            if (string.IsNullOrEmpty(GlobalVars.SelectedAbilityID) == null)
            {
                goto Label_008C;
            }
            return;
        Label_008C:
            system = EventSystem.get_current();
            if ((system != null) == null)
            {
                goto Label_00AC;
            }
            system.set_enabled(0);
            system.set_enabled(1);
        Label_00AC:
            settings = GameSettings.Instance;
            if (string.IsNullOrEmpty(settings.Dialog_AbilityDetail) != null)
            {
                goto Label_00E4;
            }
            obj2 = AssetManager.Load<GameObject>(settings.Dialog_AbilityDetail);
            if ((obj2 != null) == null)
            {
                goto Label_00E4;
            }
            Object.Instantiate<GameObject>(obj2);
        Label_00E4:
            flag = 0;
            if (go == null)
            {
                goto Label_010F;
            }
            events = go.GetComponent<ListItemEvents>();
            if (events == null)
            {
                goto Label_010F;
            }
            flag = events.IsEnableSkillChange;
        Label_010F:
            AbilityDetailWindow.IsEnableSkillChange = flag;
            AbilityDetailWindow.SetBindObject(this.Unit);
            FlowNode_GameObject.ActivateOutputLinks(this, 100);
            return;
        }

        private void _OnAbilityRankUp(GameObject go)
        {
            AbilityData data;
            data = DataSource.FindDataOfClass<AbilityData>(go, null);
            go = this.GetItemRoot(go);
            if (this.OnAbilityRankUp == null)
            {
                goto Label_0029;
            }
            this.OnAbilityRankUp(data, go);
        Label_0029:
            GameParameter.UpdateAll(go);
            return;
        }

        private void _OnAbilitySelect(GameObject go)
        {
            AbilityData data;
            data = DataSource.FindDataOfClass<AbilityData>(go, null);
            go = this.GetItemRoot(go);
            if (this.OnAbilitySelect == null)
            {
                goto Label_0029;
            }
            this.OnAbilitySelect(data, go);
        Label_0029:
            return;
        }

        private void _OnRankUpBtnPress(GameObject go)
        {
            AbilityData data;
            data = DataSource.FindDataOfClass<AbilityData>(go, null);
            go = this.GetItemRoot(go);
            if (this.OnRankUpBtnPress == null)
            {
                goto Label_0029;
            }
            this.OnRankUpBtnPress(data, go);
        Label_0029:
            GameParameter.UpdateAll(go);
            return;
        }

        private void _OnRankUpBtnUp(GameObject go)
        {
            AbilityData data;
            data = DataSource.FindDataOfClass<AbilityData>(go, null);
            go = this.GetItemRoot(go);
            if (this.OnAbilityRankUpExec == null)
            {
                goto Label_0029;
            }
            this.OnAbilityRankUpExec(data, go);
        Label_0029:
            GameParameter.UpdateAll(go);
            return;
        }

        private void _OnSlotSelect(GameObject go)
        {
            int num;
            num = 0;
            goto Label_003F;
        Label_0007:
            if ((this.mItems[num].get_gameObject() == go) == null)
            {
                goto Label_003B;
            }
            if (this.OnSlotSelect == null)
            {
                goto Label_003A;
            }
            this.OnSlotSelect(num);
        Label_003A:
            return;
        Label_003B:
            num += 1;
        Label_003F:
            if (num < this.mItems.Count)
            {
                goto Label_0007;
            }
            return;
        }

        [CompilerGenerated]
        private bool <DisplaySlotType>m__436(AbilityData p)
        {
            return (p.AbilityID == this.Unit.UnitParam.ability);
        }

        [CompilerGenerated]
        private bool <DisplaySlotType>m__437(string abil)
        {
            return (abil == this.Unit.UnitParam.ability);
        }

        public void Activated(int pinID)
        {
        }

        public void DisplayAll()
        {
            this.DisplaySlotType(-1, 0, 0);
            return;
        }

        public unsafe void DisplaySlots()
        {
            int num;
            UnitData data;
            AbilityData[] dataArray;
            List<AbilityData> list;
            Transform transform;
            int num2;
            AbilityData data2;
            UnitAbilityListItemEvents events;
            bool flag;
            UnitAbilityListItemEvents events2;
            AbilityDeriveList list2;
            string str;
            JobData data3;
            int num3;
            AbilityUnlockInfo info;
            List<AbilityDeriveParam> list3;
            AbilityParam param;
            this.mLastDisplayMode = 2;
            num = 0;
            goto Label_0028;
        Label_000E:
            Object.Destroy(this.mItems[num].get_gameObject());
            num += 1;
        Label_0028:
            if (num < this.mItems.Count)
            {
                goto Label_000E;
            }
            this.mItems.Clear();
            data = DataSource.FindDataOfClass<UnitData>(base.get_gameObject(), null);
            if (data == null)
            {
                goto Label_005E;
            }
            this.Unit = data;
        Label_005E:
            if (this.Unit != null)
            {
                goto Label_006A;
            }
            return;
        Label_006A:
            GlobalVars.SelectedUnitUniqueID.Set(this.Unit.UniqueID);
            list = new List<AbilityData>(this.Unit.CreateEquipAbilitys());
            if (this.Unit.MasterAbility == null)
            {
                goto Label_00B3;
            }
            list.Add(this.Unit.MasterAbility);
        Label_00B3:
            transform = base.get_transform();
            num2 = 0;
            goto Label_03A5;
        Label_00C3:
            data2 = list[num2];
            flag = 0;
            if (((data2 == null) || (data2.Param == null)) || (data2.IsDeriveBaseAbility == null))
            {
                goto Label_00F8;
            }
            data2 = data2.DerivedAbility;
        Label_00F8:
            if ((data2 == null) || (data2.Param == null))
            {
                goto Label_031F;
            }
            flag = data2.Param.is_fixed;
            events2 = null;
            if ((this.Unit.MasterAbility == null) || (this.Unit.MasterAbility != data2))
            {
                goto Label_014B;
            }
            events2 = this.Item_Fixed;
            goto Label_0165;
        Label_014B:
            events2 = (flag == null) ? this.Item_Normal : this.Item_Fixed;
        Label_0165:
            if (data2.IsDerivedAbility == null)
            {
                goto Label_0190;
            }
            if (data2.DeriveBaseAbility != this.Unit.MasterAbility)
            {
                goto Label_0190;
            }
            events2 = this.Item_Fixed;
        Label_0190:
            if ((events2 == null) == null)
            {
                goto Label_01A5;
            }
            events2 = this.Item_Normal;
        Label_01A5:
            events = Object.Instantiate<UnitAbilityListItemEvents>(events2);
            list2 = events.get_gameObject().GetComponent<AbilityDeriveList>();
            if ((list2 == null) == null)
            {
                goto Label_02AF;
            }
            DataSource.Bind<AbilityData>(events.get_gameObject(), data2);
            str = this.Unit.SearchAbilityReplacementSkill(data2.Param.iname);
            if (string.IsNullOrEmpty(str) != null)
            {
                goto Label_0214;
            }
            DataSource.Bind<AbilityParam>(events.get_gameObject(), MonoSingleton<GameManager>.Instance.GetAbilityParam(str));
        Label_0214:
            events.IsEnableSkillChange = 1;
            if (this.GetAbilitySource(data2.AbilityID, &data3, &num3) == null)
            {
                goto Label_025E;
            }
            info = new AbilityUnlockInfo();
            info.JobName = data3.Name;
            info.Rank = num3;
            DataSource.Bind<AbilityUnlockInfo>(events.get_gameObject(), info);
        Label_025E:
            events.OnOpenDetail = new ListItemEvents.ListItemEvent(this._OnAbilityDetail);
            events.OnRankUp = new ListItemEvents.ListItemEvent(this._OnAbilityRankUp);
            events.OnRankUpBtnPress = new ListItemEvents.ListItemEvent(this._OnRankUpBtnPress);
            events.OnRankUpBtnUp = new ListItemEvents.ListItemEvent(this._OnRankUpBtnUp);
            goto Label_031A;
        Label_02AF:
            if (data2.IsDerivedAbility == null)
            {
                goto Label_02F8;
            }
            list3 = new List<AbilityDeriveParam>();
            list3.Add(data2.DeriveParam);
            list2.SetupWithAbilityParam(data2.Param, list3);
            list2.AddDetailOpenEventListener(new UnityAction<GameObject>(this, this._OnAbilityDetail));
            goto Label_031A;
        Label_02F8:
            list2.SetupWithAbilityParam(data2.Param, null);
            list2.AddDetailOpenEventListener(new UnityAction<GameObject>(this, this._OnAbilityDetail));
        Label_031A:
            goto Label_0350;
        Label_031F:
            param = new AbilityParam();
            param.slot = JobData.ABILITY_SLOT_TYPES[num2];
            events = Object.Instantiate<UnitAbilityListItemEvents>(this.Item_Empty);
            DataSource.Bind<AbilityParam>(events.get_gameObject(), param);
        Label_0350:
            events.get_transform().SetParent(transform, 0);
            events.get_gameObject().SetActive(1);
            GameParameter.UpdateAll(events.get_gameObject());
            if (flag != null)
            {
                goto Label_0392;
            }
            events.OnSelect = new ListItemEvents.ListItemEvent(this._OnSlotSelect);
        Label_0392:
            this.mItems.Add(events);
            num2 += 1;
        Label_03A5:
            if (num2 < list.Count)
            {
                goto Label_00C3;
            }
            return;
        }

        public unsafe void DisplaySlotType(EAbilitySlot slotType, bool hideEquipped, bool showDerivedAbility)
        {
            int num;
            List<AbilityData> list;
            Transform transform;
            int num2;
            AbilityData data;
            bool flag;
            int num3;
            UnitAbilityListItemEvents events;
            AbilityDeriveList list2;
            JobData data2;
            int num4;
            AbilityUnlockInfo info;
            List<AbilityData> list3;
            int num5;
            AbilityData data3;
            UnitAbilityListItemEvents events2;
            JobData data4;
            int num6;
            AbilityUnlockInfo info2;
            List<string> list4;
            GameManager manager;
            int num7;
            RarityParam param;
            int num8;
            OString[] strArray;
            int num9;
            string str;
            AbilityParam param2;
            UnitAbilityListItemEvents events3;
            AbilityUnlockInfo info3;
            AbilityParam param3;
            UnitAbilityListItemEvents events4;
            this.mLastDisplayMode = 1;
            this.mLastDisplaySlot = slotType;
            num = 0;
            goto Label_002F;
        Label_0015:
            Object.Destroy(this.mItems[num].get_gameObject());
            num += 1;
        Label_002F:
            if (num < this.mItems.Count)
            {
                goto Label_0015;
            }
            this.mItems.Clear();
            if (this.Unit != null)
            {
                goto Label_0068;
            }
            this.Unit = DataSource.FindDataOfClass<UnitData>(base.get_gameObject(), null);
        Label_0068:
            if (this.Unit != null)
            {
                goto Label_0074;
            }
            return;
        Label_0074:
            list = null;
            if (showDerivedAbility == null)
            {
                goto Label_008E;
            }
            list = this.Unit.GetAllLearnedAbilities(1);
            goto Label_009B;
        Label_008E:
            list = this.Unit.GetAllLearnedAbilities(0);
        Label_009B:
            transform = base.get_transform();
            if ((this.Item_Normal != null) == null)
            {
                goto Label_04C6;
            }
            num2 = 0;
            goto Label_04BA;
        Label_00BA:
            data = list[num2];
            if (slotType == -1)
            {
                goto Label_00D7;
            }
            if (slotType != data.SlotType)
            {
                goto Label_04B6;
            }
        Label_00D7:
            if (this.ShowFixedAbilities != null)
            {
                goto Label_00F8;
            }
            if (data.Param.is_fixed == null)
            {
                goto Label_00F8;
            }
            goto Label_04B6;
        Label_00F8:
            if (this.ShowMasterAbilities != null)
            {
                goto Label_0146;
            }
            if ((this.Unit.UnitParam.ability == data.AbilityID) != null)
            {
                goto Label_04B6;
            }
            if (this.Unit.IsQuestClearUnlocked(data.Param.iname, 4) == null)
            {
                goto Label_0146;
            }
            goto Label_04B6;
        Label_0146:
            if (this.Unit.MapEffectAbility != data)
            {
                goto Label_015D;
            }
            goto Label_04B6;
        Label_015D:
            if (this.ShowMasterAbilities != null)
            {
                goto Label_0184;
            }
            if (this.Unit.TobiraMasterAbilitys.Contains(data) == null)
            {
                goto Label_0184;
            }
            goto Label_04B6;
        Label_0184:
            if (data.IsDerivedAbility == null)
            {
                goto Label_0256;
            }
            if (this.ShowFixedAbilities != null)
            {
                goto Label_01B6;
            }
            if (data.DeriveBaseAbility.Param.is_fixed == null)
            {
                goto Label_01B6;
            }
            goto Label_04B6;
        Label_01B6:
            if (this.ShowMasterAbilities != null)
            {
                goto Label_020E;
            }
            if ((this.Unit.UnitParam.ability == data.DeriveBaseAbility.AbilityID) != null)
            {
                goto Label_04B6;
            }
            if (this.Unit.IsQuestClearUnlocked(data.DeriveBaseAbility.Param.iname, 4) == null)
            {
                goto Label_020E;
            }
            goto Label_04B6;
        Label_020E:
            if (this.Unit.MapEffectAbility != data.DeriveBaseAbility)
            {
                goto Label_022A;
            }
            goto Label_04B6;
        Label_022A:
            if (this.ShowMasterAbilities != null)
            {
                goto Label_0256;
            }
            if (this.Unit.TobiraMasterAbilitys.Contains(data.DeriveBaseAbility) == null)
            {
                goto Label_0256;
            }
            goto Label_04B6;
        Label_0256:
            if (hideEquipped == null)
            {
                goto Label_02B9;
            }
            flag = 0;
            num3 = 0;
            goto Label_0294;
        Label_0267:
            if (this.Unit.CurrentJob.AbilitySlots[num3] != data.UniqueID)
            {
                goto Label_028E;
            }
            flag = 1;
            goto Label_02AD;
        Label_028E:
            num3 += 1;
        Label_0294:
            if (num3 < ((int) this.Unit.CurrentJob.AbilitySlots.Length))
            {
                goto Label_0267;
            }
        Label_02AD:
            if (flag == null)
            {
                goto Label_02B9;
            }
            goto Label_04B6;
        Label_02B9:
            events = Object.Instantiate<UnitAbilityListItemEvents>(this.Item_Normal);
            this.mItems.Add(events);
            events.get_transform().SetParent(transform, 0);
            events.get_gameObject().SetActive(1);
            list2 = events.get_gameObject().GetComponent<AbilityDeriveList>();
            if ((list2 == null) == null)
            {
                goto Label_03BD;
            }
            DataSource.Bind<AbilityData>(events.get_gameObject(), data);
            events.OnSelect = new ListItemEvents.ListItemEvent(this._OnAbilitySelect);
            events.OnRankUp = new ListItemEvents.ListItemEvent(this._OnAbilityRankUp);
            events.OnOpenDetail = new ListItemEvents.ListItemEvent(this._OnAbilityDetail);
            events.OnRankUpBtnPress = new ListItemEvents.ListItemEvent(this._OnRankUpBtnPress);
            events.OnRankUpBtnUp = new ListItemEvents.ListItemEvent(this._OnRankUpBtnUp);
            if (this.GetAbilitySource(data.AbilityID, &data2, &num4) == null)
            {
                goto Label_04B6;
            }
            info = new AbilityUnlockInfo();
            info.JobName = data2.Name;
            info.Rank = num4;
            DataSource.Bind<AbilityUnlockInfo>(events.get_gameObject(), info);
            goto Label_04B6;
        Label_03BD:
            if (data.IsDerivedAbility == null)
            {
                goto Label_044D;
            }
            list3 = new List<AbilityData>();
            list3.Add(data);
            list2.SetupWithAbilityData(data.DeriveBaseAbility, list3);
            list2.AddDetailOpenEventListener(new UnityAction<GameObject>(this, this._OnAbilityDetail));
            list2.AddSelectEventListener(new UnityAction<GameObject>(this, this._OnAbilitySelect));
            list2.AddRankUpEventListener(new UnityAction<GameObject>(this, this._OnAbilityRankUp));
            list2.AddRankUpBtnPressEventListener(new UnityAction<GameObject>(this, this._OnRankUpBtnPress));
            list2.AddRankUpBtnUpEventListener(new UnityAction<GameObject>(this, this._OnRankUpBtnUp));
            goto Label_04B6;
        Label_044D:
            list2.SetupWithAbilityData(data, null);
            list2.AddDetailOpenEventListener(new UnityAction<GameObject>(this, this._OnAbilityDetail));
            list2.AddSelectEventListener(new UnityAction<GameObject>(this, this._OnAbilitySelect));
            list2.AddRankUpEventListener(new UnityAction<GameObject>(this, this._OnAbilityRankUp));
            list2.AddRankUpBtnPressEventListener(new UnityAction<GameObject>(this, this._OnRankUpBtnPress));
            list2.AddRankUpBtnUpEventListener(new UnityAction<GameObject>(this, this._OnRankUpBtnUp));
        Label_04B6:
            num2 += 1;
        Label_04BA:
            if (num2 < list.Count)
            {
                goto Label_00BA;
            }
        Label_04C6:
            if (slotType == -1)
            {
                goto Label_068E;
            }
            if ((this.Item_SlotMismatch != null) == null)
            {
                goto Label_068E;
            }
            num5 = 0;
            goto Label_0681;
        Label_04E6:
            data3 = list[num5];
            if (slotType == data3.SlotType)
            {
                goto Label_067B;
            }
            if (this.ShowFixedAbilities != null)
            {
                goto Label_051E;
            }
            if (data3.Param.is_fixed == null)
            {
                goto Label_051E;
            }
            goto Label_067B;
        Label_051E:
            if (this.ShowMasterAbilities != null)
            {
                goto Label_056C;
            }
            if ((this.Unit.UnitParam.ability == data3.AbilityID) != null)
            {
                goto Label_067B;
            }
            if (this.Unit.IsQuestClearUnlocked(data3.Param.iname, 4) == null)
            {
                goto Label_056C;
            }
            goto Label_067B;
        Label_056C:
            if (this.Unit.MapEffectAbility != data3)
            {
                goto Label_0583;
            }
            goto Label_067B;
        Label_0583:
            if (this.ShowMasterAbilities != null)
            {
                goto Label_05AA;
            }
            if (this.Unit.TobiraMasterAbilitys.Contains(data3) == null)
            {
                goto Label_05AA;
            }
            goto Label_067B;
        Label_05AA:
            events2 = Object.Instantiate<UnitAbilityListItemEvents>(this.Item_SlotMismatch);
            this.mItems.Add(events2);
            DataSource.Bind<AbilityData>(events2.get_gameObject(), data3);
            events2.OnRankUp = new ListItemEvents.ListItemEvent(this._OnAbilityRankUp);
            events2.OnOpenDetail = new ListItemEvents.ListItemEvent(this._OnAbilityDetail);
            events2.OnRankUpBtnPress = new ListItemEvents.ListItemEvent(this._OnRankUpBtnPress);
            events2.OnRankUpBtnUp = new ListItemEvents.ListItemEvent(this._OnRankUpBtnUp);
            events2.get_transform().SetParent(transform, 0);
            events2.get_gameObject().SetActive(1);
            if (this.GetAbilitySource(data3.AbilityID, &data4, &num6) == null)
            {
                goto Label_067B;
            }
            info2 = new AbilityUnlockInfo();
            info2.JobName = data4.Name;
            info2.Rank = num6;
            DataSource.Bind<AbilityUnlockInfo>(events2.get_gameObject(), info2);
        Label_067B:
            num5 += 1;
        Label_0681:
            if (num5 < list.Count)
            {
                goto Label_04E6;
            }
        Label_068E:
            list4 = TobiraUtility.GetOverwrittenAbilitys(this.Unit);
            if ((this.Item_Locked != null) == null)
            {
                goto Label_09F6;
            }
            manager = MonoSingleton<GameManager>.GetInstanceDirect();
            num7 = 0;
            goto Label_08C9;
        Label_06BB:
            if (this.ShowLockedJobAbilities != null)
            {
                goto Label_06E4;
            }
            if (this.Unit.Jobs[num7].Rank > 0)
            {
                goto Label_06E4;
            }
            goto Label_08C3;
        Label_06E4:
            param = manager.GetRarityParam(this.Unit.UnitParam.raremax);
            num8 = this.Unit.Jobs[num7].Rank + 1;
            goto Label_08B7;
        Label_0719:
            strArray = this.Unit.Jobs[num7].Param.GetLearningAbilitys(num8);
            if (strArray != null)
            {
                goto Label_0741;
            }
            goto Label_08B1;
        Label_0741:
            if (param.UnitJobLvCap >= num8)
            {
                goto Label_0759;
            }
            goto Label_08B1;
        Label_0759:
            num9 = 0;
            goto Label_08A6;
        Label_0761:
            str = *(&(strArray[num9]));
            if (string.IsNullOrEmpty(str) == null)
            {
                goto Label_0787;
            }
            goto Label_08A0;
        Label_0787:
            param2 = manager.GetAbilityParam(str);
            if (this.ShowFixedAbilities != null)
            {
                goto Label_07AE;
            }
            if (param2.is_fixed == null)
            {
                goto Label_07AE;
            }
            goto Label_08A0;
        Label_07AE:
            if (list4.Contains(param2.iname) == null)
            {
                goto Label_07C6;
            }
            goto Label_08A0;
        Label_07C6:
            events3 = Object.Instantiate<UnitAbilityListItemEvents>(this.Item_Locked);
            this.mItems.Add(events3);
            DataSource.Bind<AbilityParam>(events3.get_gameObject(), param2);
            events3.OnSelect = new ListItemEvents.ListItemEvent(this._OnAbilitySelect);
            events3.OnRankUp = new ListItemEvents.ListItemEvent(this._OnAbilityRankUp);
            events3.OnOpenDetail = new ListItemEvents.ListItemEvent(this._OnAbilityDetail);
            events3.OnRankUpBtnPress = new ListItemEvents.ListItemEvent(this._OnRankUpBtnPress);
            events3.OnRankUpBtnUp = new ListItemEvents.ListItemEvent(this._OnRankUpBtnUp);
            events3.get_transform().SetParent(transform, 0);
            events3.get_gameObject().SetActive(1);
            info3 = new AbilityUnlockInfo();
            info3.JobName = this.Unit.Jobs[num7].Name;
            info3.Rank = num8;
            DataSource.Bind<AbilityUnlockInfo>(events3.get_gameObject(), info3);
        Label_08A0:
            num9 += 1;
        Label_08A6:
            if (num9 < ((int) strArray.Length))
            {
                goto Label_0761;
            }
        Label_08B1:
            num8 += 1;
        Label_08B7:
            if (num8 <= JobParam.MAX_JOB_RANK)
            {
                goto Label_0719;
            }
        Label_08C3:
            num7 += 1;
        Label_08C9:
            if (num7 < ((int) this.Unit.Jobs.Length))
            {
                goto Label_06BB;
            }
            if (this.ShowMasterAbilities == null)
            {
                goto Label_09F6;
            }
            if (string.IsNullOrEmpty(this.Unit.UnitParam.ability) != null)
            {
                goto Label_09F6;
            }
            if (this.Unit.LearnAbilitys.Find(new Predicate<AbilityData>(this.<DisplaySlotType>m__436)) != null)
            {
                goto Label_09F6;
            }
            if (list4.Exists(new Predicate<string>(this.<DisplaySlotType>m__437)) != null)
            {
                goto Label_09F6;
            }
            param3 = manager.GetAbilityParam(this.Unit.UnitParam.ability);
            events4 = Object.Instantiate<UnitAbilityListItemEvents>(this.Item_Locked);
            this.mItems.Add(events4);
            DataSource.Bind<AbilityParam>(events4.get_gameObject(), param3);
            events4.OnSelect = new ListItemEvents.ListItemEvent(this._OnAbilitySelect);
            events4.OnRankUp = new ListItemEvents.ListItemEvent(this._OnAbilityRankUp);
            events4.OnOpenDetail = new ListItemEvents.ListItemEvent(this._OnAbilityDetail);
            events4.OnRankUpBtnPress = new ListItemEvents.ListItemEvent(this._OnRankUpBtnPress);
            events4.OnRankUpBtnUp = new ListItemEvents.ListItemEvent(this._OnRankUpBtnUp);
            events4.get_transform().SetParent(transform, 0);
            events4.get_gameObject().SetActive(1);
        Label_09F6:
            this.ResetScrollPos();
            return;
        }

        private unsafe bool GetAbilitySource(string abilityID, out JobData job, out int rank)
        {
            int num;
            num = 0;
            goto Label_003E;
        Label_0007:
            *((int*) rank) = this.Unit.Jobs[num].Param.FindRankOfAbility(abilityID);
            if (*(((int*) rank)) == -1)
            {
                goto Label_003A;
            }
            *(job) = this.Unit.Jobs[num];
            return 1;
        Label_003A:
            num += 1;
        Label_003E:
            if (num < ((int) this.Unit.Jobs.Length))
            {
                goto Label_0007;
            }
            *(job) = null;
            *((int*) rank) = -1;
            return 0;
        }

        private GameObject GetItemRoot(GameObject go)
        {
            return go;
        }

        [DebuggerHidden]
        private IEnumerator RefreshScrollRect()
        {
            <RefreshScrollRect>c__Iterator148 iterator;
            iterator = new <RefreshScrollRect>c__Iterator148();
            iterator.<>f__this = this;
            return iterator;
        }

        public unsafe void ResetScrollPos()
        {
            RectTransform transform;
            Vector2 vector;
            if ((this.ScrollParent != null) == null)
            {
                goto Label_0032;
            }
            this.mDecelerationRate = this.ScrollParent.get_decelerationRate();
            this.ScrollParent.set_decelerationRate(0f);
        Label_0032:
            transform = base.get_transform() as RectTransform;
            transform.set_anchoredPosition(new Vector2(&transform.get_anchoredPosition().x, 0f));
            base.StartCoroutine(this.RefreshScrollRect());
            return;
        }

        private void Start()
        {
            if ((this.Item_Normal != null) == null)
            {
                goto Label_0022;
            }
            this.Item_Normal.get_gameObject().SetActive(0);
        Label_0022:
            if ((this.Item_Locked != null) == null)
            {
                goto Label_0044;
            }
            this.Item_Locked.get_gameObject().SetActive(0);
        Label_0044:
            if ((this.Item_NoSlot != null) == null)
            {
                goto Label_0066;
            }
            this.Item_NoSlot.get_gameObject().SetActive(0);
        Label_0066:
            if ((this.Item_Empty != null) == null)
            {
                goto Label_0088;
            }
            this.Item_Empty.get_gameObject().SetActive(0);
        Label_0088:
            if ((this.Item_SlotMismatch != null) == null)
            {
                goto Label_00AA;
            }
            this.Item_SlotMismatch.get_gameObject().SetActive(0);
        Label_00AA:
            if ((this.Item_Fixed != null) == null)
            {
                goto Label_00CC;
            }
            this.Item_Fixed.get_gameObject().SetActive(0);
        Label_00CC:
            if (this.RefreshOnStart != 1)
            {
                goto Label_00E3;
            }
            this.DisplayAll();
            goto Label_00F5;
        Label_00E3:
            if (this.RefreshOnStart != 2)
            {
                goto Label_00F5;
            }
            this.DisplaySlots();
        Label_00F5:
            return;
        }

        public void UpdateItem(AbilityData ability)
        {
            int num;
            AbilityData data;
            num = 0;
            goto Label_0040;
        Label_0007:
            if (DataSource.FindDataOfClass<AbilityData>(this.mItems[num].get_gameObject(), null) != ability)
            {
                goto Label_003C;
            }
            GameParameter.UpdateAll(this.mItems[num].get_gameObject());
        Label_003C:
            num += 1;
        Label_0040:
            if (num < this.mItems.Count)
            {
                goto Label_0007;
            }
            return;
        }

        public void UpdateValue()
        {
            if (this.AutoRefresh == null)
            {
                goto Label_003C;
            }
            if (this.mLastDisplayMode != 1)
            {
                goto Label_002A;
            }
            this.DisplaySlotType(this.mLastDisplaySlot, 0, 0);
            goto Label_003C;
        Label_002A:
            if (this.mLastDisplayMode != 2)
            {
                goto Label_003C;
            }
            this.DisplaySlots();
        Label_003C:
            return;
        }

        public bool IsEmpty
        {
            get
            {
                return (this.mItems.Count == 0);
            }
        }

        [CompilerGenerated]
        private sealed class <RefreshScrollRect>c__Iterator148 : IEnumerator, IDisposable, IEnumerator<object>
        {
            internal int $PC;
            internal object $current;
            internal UnitAbilityList <>f__this;

            public <RefreshScrollRect>c__Iterator148()
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
                        goto Label_0034;
                }
                goto Label_006C;
            Label_0021:
                this.$current = null;
                this.$PC = 1;
                goto Label_006E;
            Label_0034:
                if ((this.<>f__this.ScrollParent != null) == null)
                {
                    goto Label_0065;
                }
                this.<>f__this.ScrollParent.set_decelerationRate(this.<>f__this.mDecelerationRate);
            Label_0065:
                this.$PC = -1;
            Label_006C:
                return 0;
            Label_006E:
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

        public delegate void AbilityEvent(AbilityData ability, GameObject itemGO);

        public delegate void AbilitySlotEvent(int slotIndex);

        public enum RefreshTypes
        {
            None,
            DisplayAll,
            DisplaySlots
        }
    }
}

