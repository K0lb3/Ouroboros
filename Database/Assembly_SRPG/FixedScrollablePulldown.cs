namespace SRPG
{
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;
    using UnityEngine;

    public class FixedScrollablePulldown : ScrollablePulldownBase
    {
        public FixedScrollablePulldown()
        {
            base..ctor();
            return;
        }

        public unsafe void ResetAllItems()
        {
            PulldownItem item;
            List<PulldownItem>.Enumerator enumerator;
            enumerator = base.Items.GetEnumerator();
        Label_000C:
            try
            {
                goto Label_0030;
            Label_0011:
                item = &enumerator.Current;
                GameUtility.RemoveComponent<SRPG_Button>(item.get_gameObject());
                item.get_gameObject().SetActive(0);
            Label_0030:
                if (&enumerator.MoveNext() != null)
                {
                    goto Label_0011;
                }
                goto Label_004D;
            }
            finally
            {
            Label_0041:
                ((List<PulldownItem>.Enumerator) enumerator).Dispose();
            }
        Label_004D:
            base.ResetAllStatus();
            return;
        }

        public PulldownItem SetItem(string label, int index, int value)
        {
            PulldownItem item;
            GameObject obj2;
            SRPG_Button button;
            <SetItem>c__AnonStorey33A storeya;
            storeya = new <SetItem>c__AnonStorey33A();
            storeya.value = value;
            storeya.<>f__this = this;
            if (index < 0)
            {
                goto Label_002C;
            }
            if (index < base.Items.Count)
            {
                goto Label_002E;
            }
        Label_002C:
            return null;
        Label_002E:
            item = base.Items[index];
            if ((item.Text != null) == null)
            {
                goto Label_0058;
            }
            item.Text.set_text(label);
        Label_0058:
            item.Value = storeya.value;
            obj2 = item.get_gameObject();
            GameUtility.RequireComponent<SRPG_Button>(obj2).AddListener(new SRPG_Button.ButtonClickEvent(storeya.<>m__317));
            obj2.get_transform().SetParent(base.ItemHolder, 0);
            obj2.SetActive(1);
            return item;
        }

        [CompilerGenerated]
        private sealed class <SetItem>c__AnonStorey33A
        {
            internal int value;
            internal FixedScrollablePulldown <>f__this;

            public <SetItem>c__AnonStorey33A()
            {
                base..ctor();
                return;
            }

            internal void <>m__317(SRPG_Button g)
            {
                this.<>f__this.Selection = this.value;
                this.<>f__this.ClosePulldown(0);
                this.<>f__this.TriggerItemChange();
                return;
            }
        }
    }
}

