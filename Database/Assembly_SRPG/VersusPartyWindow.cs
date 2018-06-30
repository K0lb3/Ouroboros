namespace SRPG
{
    using GR;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;
    using UnityEngine;

    [Pin(8, "同一グリッド", 1, 8), Pin(7, "ユニット配置へ", 1, 7)]
    public class VersusPartyWindow : PartyWindow2
    {
        [CompilerGenerated]
        private static UIUtility.DialogResultEvent <>f__am$cache0;

        public VersusPartyWindow()
        {
            base..ctor();
            return;
        }

        [CompilerGenerated]
        private static void <PostForwardPressed>m__499(GameObject dialog)
        {
        }

        public void OnClickEdit()
        {
            base.SaveAndActivatePin(7);
            return;
        }

        protected override void OnItemSlotsChange()
        {
        }

        protected override void PostForwardPressed()
        {
            GameManager manager;
            PlayerData data;
            PartyData data2;
            long num;
            List<int> list;
            int num2;
            string str;
            <PostForwardPressed>c__AnonStorey3E9 storeye;
            data = MonoSingleton<GameManager>.Instance.Player;
            data2 = base.mCurrentParty.PartyData;
            list = new List<int>();
            num2 = 0;
            goto Label_00FE;
        Label_0028:
            if (((base.mCurrentParty.Units[num2] == null) ? 0L : base.mCurrentParty.Units[num2].UniqueID) == null)
            {
                goto Label_00F8;
            }
            storeye = new <PostForwardPressed>c__AnonStorey3E9();
            if (data2.PartyType != 10)
            {
                goto Label_007C;
            }
            str = PlayerPrefsUtility.RANKMATCH_ID_KEY;
            goto Label_0083;
        Label_007C:
            str = PlayerPrefsUtility.VERSUS_ID_KEY;
        Label_0083:
            storeye.idx = data.GetVersusPlacement(str + ((int) num2));
            if (list.FindIndex(new Predicate<int>(storeye.<>m__498)) == -1)
            {
                goto Label_00EA;
            }
            if (<>f__am$cache0 != null)
            {
                goto Label_00DB;
            }
            <>f__am$cache0 = new UIUtility.DialogResultEvent(VersusPartyWindow.<PostForwardPressed>m__499);
        Label_00DB:
            UIUtility.NegativeSystemMessage(null, LocalizedText.Get("sys.MULTI_VERSUS_SAME_POS"), <>f__am$cache0, null, 0, -1);
            return;
        Label_00EA:
            list.Add(storeye.idx);
        Label_00F8:
            num2 += 1;
        Label_00FE:
            if (num2 < data2.MAX_UNIT)
            {
                goto Label_0028;
            }
            base.PostForwardPressed();
            return;
        }

        protected override void SetItemSlot(int slotIndex, ItemData item)
        {
            base.mCurrentItems[slotIndex] = item;
            return;
        }

        private void Update()
        {
        }

        protected override int AvailableMainMemberSlots
        {
            get
            {
                return base.mCurrentParty.PartyData.MAX_UNIT;
            }
        }

        [CompilerGenerated]
        private sealed class <PostForwardPressed>c__AnonStorey3E9
        {
            internal int idx;

            public <PostForwardPressed>c__AnonStorey3E9()
            {
                base..ctor();
                return;
            }

            internal bool <>m__498(int d)
            {
                return (d == this.idx);
            }
        }
    }
}

