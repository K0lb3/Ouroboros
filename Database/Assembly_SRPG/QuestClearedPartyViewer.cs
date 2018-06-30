namespace SRPG
{
    using GR;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;
    using UnityEngine;
    using UnityEngine.UI;

    public class QuestClearedPartyViewer : MonoBehaviour
    {
        [SerializeField]
        private GenericSlot UnitSlotTemplate;
        [SerializeField]
        private GenericSlot NpcSlotTemplate;
        [SerializeField]
        private Transform MainMemberHolder;
        [SerializeField]
        private Transform SubMemberHolder;
        [SerializeField]
        private GameObject[] ConditionItems;
        [SerializeField]
        private GameObject[] ConditionStars;
        [SerializeField]
        private GenericSlot[] ItemSlots;
        [SerializeField]
        private Text TotalAtk;
        [SerializeField]
        private GenericSlot LeaderSkill;
        [SerializeField]
        private GenericSlot SupportSkill;
        [SerializeField]
        private GameObject[] EnableUploadObjects;
        private QuestParam mCurrentQuest;
        private UnitData[] mCurrentParty;
        private SupportData mCurrentSupport;
        private List<UnitData> mGuestUnits;
        private GenericSlot[] UnitSlots;
        private GenericSlot[] SubUnitSlots;
        private GenericSlot GuestUnitSlot;
        private GenericSlot FriendSlot;
        private List<PartySlotData> mSlotData;
        private bool mIsHeloOnly;
        private UnitData[] mUserSelectionParty;
        private int[] mUserSelectionAchievement;
        private ItemData[] mUsedItems;
        private bool mIsUserOwnUnits;
        [CompilerGenerated]
        private static Predicate<SupportData> <>f__am$cache19;
        [CompilerGenerated]
        private static Predicate<SupportData> <>f__am$cache1A;
        [CompilerGenerated]
        private static Predicate<SupportData> <>f__am$cache1B;

        public QuestClearedPartyViewer()
        {
            this.mSlotData = new List<PartySlotData>();
            base..ctor();
            return;
        }

        [CompilerGenerated]
        private static bool <ReflectionUnitSlot>m__3C9(SupportData f)
        {
            return (f.FUID == GlobalVars.SelectedFriendID);
        }

        [CompilerGenerated]
        private static bool <Start>m__3C8(SupportData f)
        {
            return (f.FUID == GlobalVars.SelectedFriendID);
        }

        [CompilerGenerated]
        private static bool <UpdateLeaderSkills>m__3CA(SupportData f)
        {
            return (f.FUID == GlobalVars.SelectedFriendID);
        }

        private unsafe void CalcTotalAttack()
        {
            int num;
            int num2;
            UnitData data;
            UnitData data2;
            List<UnitData>.Enumerator enumerator;
            num = 0;
            num2 = 0;
            goto Label_004C;
        Label_0009:
            data = this.mCurrentParty[num2];
            if (data == null)
            {
                goto Label_0048;
            }
            num += data.Status.param.atk;
            num += data.Status.param.mag;
        Label_0048:
            num2 += 1;
        Label_004C:
            if (num2 < ((int) this.mCurrentParty.Length))
            {
                goto Label_0009;
            }
            if (this.mCurrentSupport == null)
            {
                goto Label_00B9;
            }
            if (this.mCurrentSupport.Unit == null)
            {
                goto Label_00B9;
            }
            num += this.mCurrentSupport.Unit.Status.param.atk;
            num += this.mCurrentSupport.Unit.Status.param.mag;
        Label_00B9:
            if (this.mGuestUnits == null)
            {
                goto Label_012C;
            }
            enumerator = this.mGuestUnits.GetEnumerator();
        Label_00D1:
            try
            {
                goto Label_010E;
            Label_00D6:
                data2 = &enumerator.Current;
                num += data2.Status.param.atk;
                num += data2.Status.param.mag;
            Label_010E:
                if (&enumerator.MoveNext() != null)
                {
                    goto Label_00D6;
                }
                goto Label_012C;
            }
            finally
            {
            Label_011F:
                ((List<UnitData>.Enumerator) enumerator).Dispose();
            }
        Label_012C:
            if ((this.TotalAtk != null) == null)
            {
                goto Label_014F;
            }
            this.TotalAtk.set_text(&num.ToString());
        Label_014F:
            return;
        }

        private GenericSlot CreateSlotObject(PartySlotData slotData, GenericSlot template, Transform parent)
        {
            GenericSlot slot;
            slot = Object.Instantiate<GameObject>(template.get_gameObject()).GetComponent<GenericSlot>();
            slot.get_transform().SetParent(parent, 0);
            slot.get_gameObject().SetActive(1);
            slot.SetSlotData<PartySlotData>(slotData);
            return slot;
        }

        private unsafe void CreateSlots()
        {
            List<PartySlotData> list;
            List<PartySlotData> list2;
            PartySlotData data;
            List<GenericSlot> list3;
            PartySlotData data2;
            List<PartySlotData>.Enumerator enumerator;
            GenericSlot slot;
            PartySlotData data3;
            List<PartySlotData>.Enumerator enumerator2;
            GenericSlot slot2;
            TowerFloorParam param;
            this.DestroyPartySlotObjects();
            list = new List<PartySlotData>();
            list2 = new List<PartySlotData>();
            data = null;
            PartyUtility.CreatePartySlotData(this.mCurrentQuest, &list, &list2, &data);
            list3 = new List<GenericSlot>();
            if ((this.MainMemberHolder != null) == null)
            {
                goto Label_00EE;
            }
            if (list.Count <= 0)
            {
                goto Label_00EE;
            }
            enumerator = list.GetEnumerator();
        Label_0050:
            try
            {
                goto Label_00B1;
            Label_0055:
                data2 = &enumerator.Current;
                if (data2.Type == 4)
                {
                    goto Label_0078;
                }
                if (data2.Type != 5)
                {
                    goto Label_0093;
                }
            Label_0078:
                slot = this.CreateSlotObject(data2, this.NpcSlotTemplate, this.MainMemberHolder);
                goto Label_00A9;
            Label_0093:
                slot = this.CreateSlotObject(data2, this.UnitSlotTemplate, this.MainMemberHolder);
            Label_00A9:
                list3.Add(slot);
            Label_00B1:
                if (&enumerator.MoveNext() != null)
                {
                    goto Label_0055;
                }
                goto Label_00CF;
            }
            finally
            {
            Label_00C2:
                ((List<PartySlotData>.Enumerator) enumerator).Dispose();
            }
        Label_00CF:
            if (data == null)
            {
                goto Label_00EE;
            }
            this.FriendSlot = this.CreateSlotObject(data, this.UnitSlotTemplate, this.MainMemberHolder);
        Label_00EE:
            if ((this.SubMemberHolder != null) == null)
            {
                goto Label_0192;
            }
            if (list2.Count <= 0)
            {
                goto Label_0192;
            }
            enumerator2 = list2.GetEnumerator();
        Label_0113:
            try
            {
                goto Label_0174;
            Label_0118:
                data3 = &enumerator2.Current;
                if (data3.Type == 4)
                {
                    goto Label_013B;
                }
                if (data3.Type != 5)
                {
                    goto Label_0156;
                }
            Label_013B:
                slot2 = this.CreateSlotObject(data3, this.NpcSlotTemplate, this.SubMemberHolder);
                goto Label_016C;
            Label_0156:
                slot2 = this.CreateSlotObject(data3, this.UnitSlotTemplate, this.SubMemberHolder);
            Label_016C:
                list3.Add(slot2);
            Label_0174:
                if (&enumerator2.MoveNext() != null)
                {
                    goto Label_0118;
                }
                goto Label_0192;
            }
            finally
            {
            Label_0185:
                ((List<PartySlotData>.Enumerator) enumerator2).Dispose();
            }
        Label_0192:
            this.mSlotData.AddRange(list);
            this.mSlotData.AddRange(list2);
            this.UnitSlots = list3.ToArray();
            if ((this.FriendSlot != null) == null)
            {
                goto Label_022F;
            }
            if (this.mCurrentQuest == null)
            {
                goto Label_022F;
            }
            if (this.mCurrentQuest.type != 7)
            {
                goto Label_022F;
            }
            param = MonoSingleton<GameManager>.Instance.FindTowerFloor(this.mCurrentQuest.iname);
            if (param == null)
            {
                goto Label_022F;
            }
            this.FriendSlot.get_gameObject().SetActive(param.can_help);
            this.SupportSkill.get_gameObject().SetActive(param.can_help);
        Label_022F:
            this.mGuestUnits = new List<UnitData>();
            PartyUtility.MergePartySlotWithPartyUnits(this.mCurrentQuest, this.mSlotData, this.mCurrentParty, this.mGuestUnits, this.mIsUserOwnUnits);
            this.ReflectionUnitSlot();
            return;
        }

        private void DestroyPartySlotObjects()
        {
            if (this.UnitSlots == null)
            {
                goto Label_0016;
            }
            GameUtility.DestroyGameObjects<GenericSlot>(this.UnitSlots);
        Label_0016:
            GameUtility.DestroyGameObject(this.FriendSlot);
            this.mSlotData.Clear();
            return;
        }

        private void LoadConditions()
        {
            if (this.ConditionItems != null)
            {
                goto Label_000C;
            }
            return;
        Label_000C:
            if (this.ConditionStars != null)
            {
                goto Label_0018;
            }
            return;
        Label_0018:
            if (this.mIsUserOwnUnits == null)
            {
                goto Label_002E;
            }
            this.LoadConditionStarsFromBattle();
            goto Label_0034;
        Label_002E:
            this.LoadConditionStarsFromUserSelection();
        Label_0034:
            if (this.mCurrentQuest.type != 7)
            {
                goto Label_0061;
            }
            if (((int) this.ConditionItems.Length) < 3)
            {
                goto Label_0061;
            }
            this.ConditionItems[2].SetActive(0);
        Label_0061:
            return;
        }

        private void LoadConditionStarsFromBattle()
        {
            SceneBattle battle;
            int num;
            bool flag;
            BattleCore.Record record;
            int num2;
            battle = SceneBattle.Instance;
            num = 0;
            goto Label_009D;
        Label_000D:
            flag = 0;
            if (num != null)
            {
                goto Label_0029;
            }
            flag = battle.Battle.PlayByManually == 0;
            goto Label_008B;
        Label_0029:
            if (num != 1)
            {
                goto Label_0041;
            }
            flag = battle.Battle.IsAllAlive();
            goto Label_008B;
        Label_0041:
            if (num != 2)
            {
                goto Label_008B;
            }
            record = battle.Battle.GetQuestRecord();
            flag = 1;
            num2 = 0;
            goto Label_007E;
        Label_005E:
            if ((record.allBonusFlags & (1 << (num2 & 0x1f))) != null)
            {
                goto Label_0078;
            }
            flag = 0;
            goto Label_008B;
        Label_0078:
            num2 += 1;
        Label_007E:
            if (num2 < record.bonusCount)
            {
                goto Label_005E;
            }
        Label_008B:
            this.ConditionStars[num].SetActive(flag);
            num += 1;
        Label_009D:
            if (num < ((int) this.ConditionStars.Length))
            {
                goto Label_000D;
            }
            return;
        }

        private void LoadConditionStarsFromUserSelection()
        {
            int num;
            bool flag;
            num = 0;
            goto Label_0028;
        Label_0007:
            flag = (this.mUserSelectionAchievement[num] == 0) == 0;
            this.ConditionStars[num].SetActive(flag);
            num += 1;
        Label_0028:
            if (num >= ((int) this.ConditionStars.Length))
            {
                goto Label_0044;
            }
            if (num < ((int) this.mUserSelectionAchievement.Length))
            {
                goto Label_0007;
            }
        Label_0044:
            return;
        }

        private unsafe void LoadInventory()
        {
            int num;
            KeyValuePair<OString, OInt> pair;
            Dictionary<OString, OInt>.Enumerator enumerator;
            string str;
            int num2;
            ItemData data;
            int num3;
            if (this.mIsUserOwnUnits == null)
            {
                goto Label_00AB;
            }
            num = 0;
            enumerator = SceneBattle.Instance.Battle.GetQuestRecord().used_items.GetEnumerator();
        Label_0027:
            try
            {
                goto Label_0089;
            Label_002C:
                pair = &enumerator.Current;
                if (num < ((int) this.ItemSlots.Length))
                {
                    goto Label_0047;
                }
                goto Label_0095;
            Label_0047:
                str = &pair.Key;
                num2 = &pair.Value;
                data = new ItemData();
                data.Setup(0L, str, num2);
                this.ItemSlots[num].SetSlotData<ItemData>(data);
                num += 1;
            Label_0089:
                if (&enumerator.MoveNext() != null)
                {
                    goto Label_002C;
                }
            Label_0095:
                goto Label_00A6;
            }
            finally
            {
            Label_009A:
                ((Dictionary<OString, OInt>.Enumerator) enumerator).Dispose();
            }
        Label_00A6:
            goto Label_00F9;
        Label_00AB:
            if (this.mUsedItems == null)
            {
                goto Label_00F9;
            }
            num3 = 0;
            goto Label_00DB;
        Label_00BE:
            this.ItemSlots[num3].SetSlotData<ItemData>(this.mUsedItems[num3]);
            num3 += 1;
        Label_00DB:
            if (num3 >= ((int) this.ItemSlots.Length))
            {
                goto Label_00F9;
            }
            if (num3 < ((int) this.mUsedItems.Length))
            {
                goto Label_00BE;
            }
        Label_00F9:
            return;
        }

        private void LoadParty()
        {
            PlayerData data;
            PartyWindow2.EditPartyTypes types;
            PlayerPartyTypes types2;
            PartyData data2;
            string str;
            int num;
            long num2;
            int num3;
            this.mCurrentQuest = MonoSingleton<GameManager>.Instance.FindQuest(GlobalVars.SelectedQuestID);
            this.mIsHeloOnly = PartyUtility.IsSoloStoryParty(this.mCurrentQuest);
            data = MonoSingleton<GameManager>.Instance.Player;
            types = PartyUtility.GetEditPartyTypes(this.mCurrentQuest);
            if (types != null)
            {
                goto Label_0045;
            }
            types = 1;
        Label_0045:
            types2 = SRPG_Extensions.ToPlayerPartyType(types);
            data2 = MonoSingleton<GameManager>.Instance.Player.FindPartyOfType(types2);
            this.mCurrentParty = new UnitData[data2.MAX_UNIT];
            str = this.mCurrentQuest.units.Get(0);
            if (this.mIsUserOwnUnits == null)
            {
                goto Label_00D0;
            }
            num = 0;
            goto Label_00BE;
        Label_0094:
            num2 = data2.GetUnitUniqueID(num);
            if (num2 <= 0L)
            {
                goto Label_00B8;
            }
            this.mCurrentParty[num] = data.FindUnitDataByUniqueID(num2);
        Label_00B8:
            num += 1;
        Label_00BE:
            if (num < data2.MAX_UNIT)
            {
                goto Label_0094;
            }
            goto Label_0156;
        Label_00D0:
            num3 = 0;
            goto Label_013A;
        Label_00D8:
            if (this.mUserSelectionParty[num3] != null)
            {
                goto Label_00F0;
            }
            this.mCurrentParty[num3] = null;
        Label_00F0:
            if (this.mUserSelectionParty[num3] == null)
            {
                goto Label_0122;
            }
            if ((this.mUserSelectionParty[num3].UnitParam.iname == str) == null)
            {
                goto Label_0122;
            }
            goto Label_0134;
        Label_0122:
            this.mCurrentParty[num3] = this.mUserSelectionParty[num3];
        Label_0134:
            num3 += 1;
        Label_013A:
            if (num3 >= data2.MAX_UNIT)
            {
                goto Label_0156;
            }
            if (num3 < ((int) this.mUserSelectionParty.Length))
            {
                goto Label_00D8;
            }
        Label_0156:
            return;
        }

        private void OnDestroy()
        {
            GlobalVars.UserSelectionPartyDataInfo = null;
            return;
        }

        private void ReflectionUnitSlot()
        {
            int num;
            int num2;
            PartySlotData data;
            UnitParam param;
            SupportData data2;
            num = 0;
            num2 = 0;
            goto Label_00E0;
        Label_0009:
            if ((this.UnitSlots[num2] != null) == null)
            {
                goto Label_00DC;
            }
            this.UnitSlots[num2].SetSlotData<QuestParam>(this.mCurrentQuest);
            data = this.mSlotData[num2];
            if (data.Type != 3)
            {
                goto Label_0086;
            }
            if ((this.mGuestUnits == null) || (num >= this.mGuestUnits.Count))
            {
                goto Label_007D;
            }
            this.UnitSlots[num2].SetSlotData<UnitData>(this.mGuestUnits[num]);
        Label_007D:
            num += 1;
            goto Label_00DC;
        Label_0086:
            if ((data.Type != 4) && (data.Type != 5))
            {
                goto Label_00C7;
            }
            param = MonoSingleton<GameManager>.Instance.MasterParam.GetUnitParam(data.UnitName);
            this.UnitSlots[num2].SetSlotData<UnitParam>(param);
            goto Label_00DC;
        Label_00C7:
            this.UnitSlots[num2].SetSlotData<UnitData>(this.mCurrentParty[num2]);
        Label_00DC:
            num2 += 1;
        Label_00E0:
            if (((num2 < ((int) this.UnitSlots.Length)) && (num2 < ((int) this.mCurrentParty.Length))) && (num2 < this.mSlotData.Count))
            {
                goto Label_0009;
            }
            if ((this.FriendSlot != null) == null)
            {
                goto Label_01E6;
            }
            this.FriendSlot.SetSlotData<QuestParam>(this.mCurrentQuest);
            this.FriendSlot.SetSlotData<SupportData>(this.mCurrentSupport);
            if (this.mIsUserOwnUnits == null)
            {
                goto Label_01C5;
            }
            if (<>f__am$cache1A != null)
            {
                goto Label_0172;
            }
            <>f__am$cache1A = new Predicate<SupportData>(QuestClearedPartyViewer.<ReflectionUnitSlot>m__3C9);
        Label_0172:
            data2 = MonoSingleton<GameManager>.Instance.Player.Supports.Find(<>f__am$cache1A);
            if (data2 != null)
            {
                goto Label_0191;
            }
            data2 = GlobalVars.SelectedSupport.Get();
        Label_0191:
            this.mCurrentSupport = data2;
            this.FriendSlot.SetSlotData<UnitData>((this.mCurrentSupport != null) ? this.mCurrentSupport.Unit : null);
            goto Label_01E6;
        Label_01C5:
            if (this.mCurrentSupport == null)
            {
                goto Label_01E6;
            }
            this.FriendSlot.SetSlotData<UnitData>(this.mCurrentSupport.Unit);
        Label_01E6:
            return;
        }

        private void Start()
        {
            GameObject obj2;
            GameObject[] objArray;
            int num;
            this.mIsUserOwnUnits = GlobalVars.UserSelectionPartyDataInfo == null;
            if (this.mIsUserOwnUnits == null)
            {
                goto Label_0070;
            }
            if (<>f__am$cache19 != null)
            {
                goto Label_0041;
            }
            <>f__am$cache19 = new Predicate<SupportData>(QuestClearedPartyViewer.<Start>m__3C8);
        Label_0041:
            this.mCurrentSupport = MonoSingleton<GameManager>.Instance.Player.Supports.Find(<>f__am$cache19);
            if (this.mCurrentSupport != null)
            {
                goto Label_00B0;
            }
            this.mCurrentSupport = GlobalVars.SelectedSupport.Get();
            goto Label_00B0;
        Label_0070:
            this.mUserSelectionParty = GlobalVars.UserSelectionPartyDataInfo.unitData;
            this.mCurrentSupport = GlobalVars.UserSelectionPartyDataInfo.supportData;
            this.mUserSelectionAchievement = GlobalVars.UserSelectionPartyDataInfo.achievements;
            this.mUsedItems = GlobalVars.UserSelectionPartyDataInfo.usedItems;
        Label_00B0:
            objArray = this.EnableUploadObjects;
            num = 0;
            goto Label_00D2;
        Label_00BE:
            obj2 = objArray[num];
            obj2.SetActive(this.mIsUserOwnUnits);
            num += 1;
        Label_00D2:
            if (num < ((int) objArray.Length))
            {
                goto Label_00BE;
            }
            GameUtility.SetGameObjectActive(this.UnitSlotTemplate, 0);
            GameUtility.SetGameObjectActive(this.NpcSlotTemplate, 0);
            this.LoadParty();
            this.CreateSlots();
            this.UpdateLeaderSkills();
            this.CalcTotalAttack();
            this.LoadInventory();
            this.LoadConditions();
            return;
        }

        private void UpdateLeaderSkills()
        {
            SkillParam param;
            UnitParam param2;
            string str;
            SupportData data;
            SkillParam param3;
            if ((this.LeaderSkill != null) == null)
            {
                goto Label_0190;
            }
            param = null;
            if (this.mIsHeloOnly == null)
            {
                goto Label_006C;
            }
            if (this.mGuestUnits == null)
            {
                goto Label_0184;
            }
            if (this.mGuestUnits.Count <= 0)
            {
                goto Label_0184;
            }
            if (this.mGuestUnits[0].LeaderSkill == null)
            {
                goto Label_0184;
            }
            param = this.mGuestUnits[0].LeaderSkill.SkillParam;
            goto Label_0184;
        Label_006C:
            if (this.mCurrentParty[0] == null)
            {
                goto Label_00A3;
            }
            if (this.mCurrentParty[0].LeaderSkill == null)
            {
                goto Label_0184;
            }
            param = this.mCurrentParty[0].LeaderSkill.SkillParam;
            goto Label_0184;
        Label_00A3:
            if (this.mSlotData[0].Type == 4)
            {
                goto Label_00D1;
            }
            if (this.mSlotData[0].Type != 5)
            {
                goto Label_013B;
            }
        Label_00D1:
            param2 = MonoSingleton<GameManager>.Instance.MasterParam.GetUnitParam(this.mSlotData[0].UnitName);
            if (param2 == null)
            {
                goto Label_0184;
            }
            if (param2.leader_skills == null)
            {
                goto Label_0184;
            }
            if (((int) param2.leader_skills.Length) < 4)
            {
                goto Label_0184;
            }
            str = param2.leader_skills[4];
            if (string.IsNullOrEmpty(str) != null)
            {
                goto Label_0184;
            }
            param = MonoSingleton<GameManager>.Instance.MasterParam.GetSkillParam(str);
            goto Label_0184;
        Label_013B:
            if (this.mGuestUnits == null)
            {
                goto Label_0184;
            }
            if (this.mGuestUnits.Count <= 0)
            {
                goto Label_0184;
            }
            if (this.mGuestUnits[0].LeaderSkill == null)
            {
                goto Label_0184;
            }
            param = this.mGuestUnits[0].LeaderSkill.SkillParam;
        Label_0184:
            this.LeaderSkill.SetSlotData<SkillParam>(param);
        Label_0190:
            if ((this.SupportSkill != null) == null)
            {
                goto Label_023E;
            }
            if (this.mIsUserOwnUnits == null)
            {
                goto Label_01F4;
            }
            if (<>f__am$cache1B != null)
            {
                goto Label_01D3;
            }
            <>f__am$cache1B = new Predicate<SupportData>(QuestClearedPartyViewer.<UpdateLeaderSkills>m__3CA);
        Label_01D3:
            data = MonoSingleton<GameManager>.Instance.Player.Supports.Find(<>f__am$cache1B);
            if (data != null)
            {
                goto Label_01FB;
            }
            data = GlobalVars.SelectedSupport.Get();
            goto Label_01FB;
        Label_01F4:
            data = this.mCurrentSupport;
        Label_01FB:
            param3 = null;
            if (data == null)
            {
                goto Label_0231;
            }
            if (data.Unit.LeaderSkill == null)
            {
                goto Label_0231;
            }
            if (data.IsFriend() == null)
            {
                goto Label_0231;
            }
            param3 = data.Unit.LeaderSkill.SkillParam;
        Label_0231:
            this.SupportSkill.SetSlotData<SkillParam>(param3);
        Label_023E:
            return;
        }
    }
}

