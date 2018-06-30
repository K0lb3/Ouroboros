namespace SRPG
{
    using GR;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using UnityEngine;
    using UnityEngine.UI;

    public class RecentPartyPanel : MonoBehaviour
    {
        [SerializeField]
        private GenericSlot UnitSlotTemplate;
        [SerializeField]
        private GenericSlot NpcSlotTemplate;
        [SerializeField]
        private Transform MembersHolder;
        [SerializeField]
        private GameObject[] ConditionItems;
        [SerializeField]
        private GameObject[] ConditionStars;
        [SerializeField]
        private Text UserName;
        [SerializeField]
        private Text Level;
        [SerializeField]
        private Text ClearDate;
        private QuestParam mCurrentQuest;
        private UnitData[] mCurrentParty;
        private SupportData mCurrentSupport;
        private List<UnitData> mGuestUnits;
        private GenericSlot[] UnitSlots;
        private GenericSlot[] SubUnitSlots;
        private GenericSlot GuestUnitSlot;
        private GenericSlot FriendSlot;
        private List<PartySlotData> mSlotData;
        private List<SRPG_Button> allUnitButtons;
        [CompilerGenerated]
        private static Func<GenericSlot, SRPG_Button> <>f__am$cache12;
        [CompilerGenerated]
        private static Func<SRPG_Button, bool> <>f__am$cache13;

        public RecentPartyPanel()
        {
            this.mGuestUnits = new List<UnitData>();
            this.mSlotData = new List<PartySlotData>();
            this.allUnitButtons = new List<SRPG_Button>();
            base..ctor();
            return;
        }

        [CompilerGenerated]
        private static SRPG_Button <CreateSlots>m__3EE(GenericSlot slot)
        {
            return slot.GetComponent<SRPG_Button>();
        }

        [CompilerGenerated]
        private static bool <CreateSlots>m__3EF(SRPG_Button button)
        {
            return (button != null);
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
            if ((this.MembersHolder != null) == null)
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
                slot = this.CreateSlotObject(data2, this.NpcSlotTemplate, this.MembersHolder);
                goto Label_00A9;
            Label_0093:
                slot = this.CreateSlotObject(data2, this.UnitSlotTemplate, this.MembersHolder);
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
            this.FriendSlot = this.CreateSlotObject(data, this.UnitSlotTemplate, this.MembersHolder);
        Label_00EE:
            if ((this.MembersHolder != null) == null)
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
                slot2 = this.CreateSlotObject(data3, this.NpcSlotTemplate, this.MembersHolder);
                goto Label_016C;
            Label_0156:
                slot2 = this.CreateSlotObject(data3, this.UnitSlotTemplate, this.MembersHolder);
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
                goto Label_0218;
            }
            if (this.mCurrentQuest == null)
            {
                goto Label_0218;
            }
            if (this.mCurrentQuest.type != 7)
            {
                goto Label_0218;
            }
            param = MonoSingleton<GameManager>.Instance.FindTowerFloor(this.mCurrentQuest.iname);
            if (param == null)
            {
                goto Label_0218;
            }
            this.FriendSlot.get_gameObject().SetActive(param.can_help);
        Label_0218:
            PartyUtility.MergePartySlotWithPartyUnits(this.mCurrentQuest, this.mSlotData, this.mCurrentParty, this.mGuestUnits, 0);
            this.ReflectionUnitSlot();
            if (<>f__am$cache12 != null)
            {
                goto Label_025B;
            }
            <>f__am$cache12 = new Func<GenericSlot, SRPG_Button>(RecentPartyPanel.<CreateSlots>m__3EE);
        Label_025B:
            if (<>f__am$cache13 != null)
            {
                goto Label_027D;
            }
            <>f__am$cache13 = new Func<SRPG_Button, bool>(RecentPartyPanel.<CreateSlots>m__3EF);
        Label_027D:
            this.allUnitButtons = Enumerable.ToList<SRPG_Button>(Enumerable.Where<SRPG_Button>(Enumerable.Select<GenericSlot, SRPG_Button>(this.UnitSlots, <>f__am$cache12), <>f__am$cache13));
            this.allUnitButtons.Add(this.FriendSlot.GetComponent<SRPG_Button>());
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

        private void ReflectionUnitSlot()
        {
            int num;
            int num2;
            PartySlotData data;
            UnitParam param;
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
            if (this.mGuestUnits == null)
            {
                goto Label_007D;
            }
            if (num >= this.mGuestUnits.Count)
            {
                goto Label_007D;
            }
            this.UnitSlots[num2].SetSlotData<UnitData>(this.mGuestUnits[num]);
        Label_007D:
            num += 1;
            goto Label_00DC;
        Label_0086:
            if (data.Type == 4)
            {
                goto Label_009E;
            }
            if (data.Type != 5)
            {
                goto Label_00C7;
            }
        Label_009E:
            param = MonoSingleton<GameManager>.Instance.MasterParam.GetUnitParam(data.UnitName);
            this.UnitSlots[num2].SetSlotData<UnitParam>(param);
            goto Label_00DC;
        Label_00C7:
            this.UnitSlots[num2].SetSlotData<UnitData>(this.mCurrentParty[num2]);
        Label_00DC:
            num2 += 1;
        Label_00E0:
            if (num2 >= ((int) this.UnitSlots.Length))
            {
                goto Label_010D;
            }
            if (num2 >= ((int) this.mCurrentParty.Length))
            {
                goto Label_010D;
            }
            if (num2 < this.mSlotData.Count)
            {
                goto Label_0009;
            }
        Label_010D:
            if ((this.FriendSlot != null) == null)
            {
                goto Label_0161;
            }
            this.FriendSlot.SetSlotData<QuestParam>(this.mCurrentQuest);
            this.FriendSlot.SetSlotData<SupportData>(this.mCurrentSupport);
            if (this.mCurrentSupport == null)
            {
                goto Label_0161;
            }
            this.FriendSlot.SetSlotData<UnitData>(this.mCurrentSupport.Unit);
        Label_0161:
            return;
        }

        public void SetClearDate(string value)
        {
            if ((this.ClearDate != null) == null)
            {
                goto Label_001D;
            }
            this.ClearDate.set_text(value);
        Label_001D:
            return;
        }

        public void SetConditionItemActive(int index, bool active)
        {
            if (index >= ((int) this.ConditionItems.Length))
            {
                goto Label_001C;
            }
            this.ConditionItems[index].SetActive(active);
        Label_001C:
            return;
        }

        public void SetConditionStarActive(int index, bool active)
        {
            if (index >= ((int) this.ConditionStars.Length))
            {
                goto Label_001C;
            }
            this.ConditionStars[index].SetActive(active);
        Label_001C:
            return;
        }

        public void SetPartyInfo(UnitData[] party, SupportData supportData, QuestParam questParam)
        {
            this.mCurrentQuest = MonoSingleton<GameManager>.Instance.FindQuest(GlobalVars.SelectedQuestID);
            this.mCurrentParty = party;
            this.mCurrentSupport = supportData;
            this.CreateSlots();
            return;
        }

        public unsafe void SetUnitIconPressedCallback(SRPG_Button.ButtonClickEvent callback)
        {
            SRPG_Button button;
            List<SRPG_Button>.Enumerator enumerator;
            if (this.allUnitButtons == null)
            {
                goto Label_0048;
            }
            enumerator = this.allUnitButtons.GetEnumerator();
        Label_0017:
            try
            {
                goto Label_002B;
            Label_001C:
                button = &enumerator.Current;
                button.AddListener(callback);
            Label_002B:
                if (&enumerator.MoveNext() != null)
                {
                    goto Label_001C;
                }
                goto Label_0048;
            }
            finally
            {
            Label_003C:
                ((List<SRPG_Button>.Enumerator) enumerator).Dispose();
            }
        Label_0048:
            return;
        }

        public void SetUserName(string value)
        {
            if ((this.UserName != null) == null)
            {
                goto Label_001D;
            }
            this.UserName.set_text(value);
        Label_001D:
            return;
        }

        public void SetUserRank(string value)
        {
            if ((this.Level != null) == null)
            {
                goto Label_001D;
            }
            this.Level.set_text(value);
        Label_001D:
            return;
        }

        private void Start()
        {
            GameUtility.SetGameObjectActive(this.UnitSlotTemplate, 0);
            GameUtility.SetGameObjectActive(this.NpcSlotTemplate, 0);
            return;
        }
    }
}

