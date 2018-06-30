namespace SRPG
{
    using GR;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;
    using UnityEngine;

    public class TowerPartyWindow : PartyWindow2
    {
        [CompilerGenerated]
        private static UIUtility.DialogResultEvent <>f__am$cache0;
        [CompilerGenerated]
        private static UIUtility.DialogResultEvent <>f__am$cache1;
        [CompilerGenerated]
        private static UIUtility.DialogResultEvent <>f__am$cache2;

        public TowerPartyWindow()
        {
            base..ctor();
            return;
        }

        [CompilerGenerated]
        private static void <CheckMember>m__424(GameObject dialog)
        {
        }

        [CompilerGenerated]
        private static void <CheckMember>m__425(GameObject dialog)
        {
        }

        [CompilerGenerated]
        private static void <CheckMember>m__426(GameObject dialog)
        {
        }

        protected override unsafe bool CheckMember(int numMainUnits)
        {
            string str;
            if (numMainUnits > 0)
            {
                goto Label_003A;
            }
            if (<>f__am$cache0 != null)
            {
                goto Label_002A;
            }
            <>f__am$cache0 = new UIUtility.DialogResultEvent(TowerPartyWindow.<CheckMember>m__424);
        Label_002A:
            UIUtility.NegativeSystemMessage(null, LocalizedText.Get("sys.PARTYEDITOR_CANTSTART"), <>f__am$cache0, null, 0, -1);
            return 0;
        Label_003A:
            if (base.mCurrentParty.Units[0] != null)
            {
                goto Label_007F;
            }
            if (<>f__am$cache1 != null)
            {
                goto Label_006F;
            }
            <>f__am$cache1 = new UIUtility.DialogResultEvent(TowerPartyWindow.<CheckMember>m__425);
        Label_006F:
            UIUtility.NegativeSystemMessage(null, LocalizedText.Get("sys.LEADERNOTSET"), <>f__am$cache1, null, 0, -1);
            return 0;
        Label_007F:
            str = string.Empty;
            if (base.mCurrentQuest.IsEntryQuestCondition(base.mCurrentParty.Units, &str) != null)
            {
                goto Label_00D1;
            }
            if (<>f__am$cache2 != null)
            {
                goto Label_00C1;
            }
            <>f__am$cache2 = new UIUtility.DialogResultEvent(TowerPartyWindow.<CheckMember>m__426);
        Label_00C1:
            UIUtility.NegativeSystemMessage(null, LocalizedText.Get(str), <>f__am$cache2, null, 0, -1);
            return 0;
        Label_00D1:
            return 1;
        }

        protected override void RegistPartyMember(List<UnitData> allUnits, bool heroesAvailable, bool selectedSlotIsEmpty, int numMainMembers)
        {
            int num;
            int num2;
            num = 0;
            goto Label_00F1;
        Label_0007:
            if (heroesAvailable != null)
            {
                goto Label_0028;
            }
            if (allUnits[num].UnitParam.IsHero() == null)
            {
                goto Label_0028;
            }
            goto Label_00ED;
        Label_0028:
            if (base.mCurrentParty.PartyData.SUBMEMBER_START > base.mSelectedSlotIndex)
            {
                goto Label_008A;
            }
            if (base.mSelectedSlotIndex > base.mCurrentParty.PartyData.SUBMEMBER_END)
            {
                goto Label_008A;
            }
            if (allUnits[num] != base.mCurrentParty.Units[0])
            {
                goto Label_008A;
            }
            if (selectedSlotIsEmpty == null)
            {
                goto Label_008A;
            }
            if (numMainMembers > 1)
            {
                goto Label_008A;
            }
            goto Label_00ED;
        Label_008A:
            if (base.mCurrentQuest == null)
            {
                goto Label_00CC;
            }
            if (base.mCurrentQuest.type != 7)
            {
                goto Label_00CC;
            }
            if (allUnits[num].Lv >= base.mCurrentQuest.EntryCondition.ulvmin)
            {
                goto Label_00CC;
            }
            goto Label_00ED;
        Label_00CC:
            num2 = base.mOwnUnits.IndexOf(allUnits[num]);
            base.UnitList.AddItem(num2 + 1);
        Label_00ED:
            num += 1;
        Label_00F1:
            if (num < allUnits.Count)
            {
                goto Label_0007;
            }
            return;
        }

        protected override int AvailableMainMemberSlots
        {
            get
            {
                TowerFloorParam param;
                if (base.mCurrentPartyType != 7)
                {
                    goto Label_0035;
                }
                param = MonoSingleton<GameManager>.Instance.FindTowerFloor(base.mCurrentQuest.iname);
                if (param == null)
                {
                    goto Label_0035;
                }
                if (param.can_help != null)
                {
                    goto Label_0035;
                }
                return 5;
            Label_0035:
                return base.AvailableMainMemberSlots;
            }
        }
    }
}

