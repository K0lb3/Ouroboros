namespace SRPG
{
    using System;
    using System.Runtime.CompilerServices;
    using UnityEngine;

    public class ScrollablePulldown : ScrollablePulldownBase
    {
        [SerializeField]
        private GameObject PulldownItemTemplate;

        public ScrollablePulldown()
        {
            base..ctor();
            return;
        }

        public PulldownItem AddItem(string label, int value)
        {
            GameObject obj2;
            SRPG_Button button;
            PulldownItem item;
            <AddItem>c__AnonStorey3A0 storeya;
            storeya = new <AddItem>c__AnonStorey3A0();
            storeya.value = value;
            storeya.<>f__this = this;
            if ((this.PulldownItemTemplate == null) == null)
            {
                goto Label_0027;
            }
            return null;
        Label_0027:
            obj2 = Object.Instantiate<GameObject>(this.PulldownItemTemplate);
            obj2.GetComponent<SRPG_Button>().AddListener(new SRPG_Button.ButtonClickEvent(storeya.<>m__402));
            item = obj2.GetComponent<PulldownItem>();
            if ((item.Text != null) == null)
            {
                goto Label_0070;
            }
            item.Text.set_text(label);
        Label_0070:
            item.Value = storeya.value;
            base.Items.Add(item);
            obj2.get_transform().SetParent(base.ItemHolder, 0);
            obj2.SetActive(1);
            base.ScrollRect.set_verticalNormalizedPosition(1f);
            base.ScrollRect.set_horizontalNormalizedPosition(1f);
            return item;
        }

        public void ClearItems()
        {
            int num;
            num = 0;
            goto Label_0021;
        Label_0007:
            Object.Destroy(base.Items[num].get_gameObject());
            num += 1;
        Label_0021:
            if (num < base.Items.Count)
            {
                goto Label_0007;
            }
            base.Items.Clear();
            base.ResetAllStatus();
            return;
        }

        protected override void OnDestroy()
        {
            this.ClearItems();
            base.OnDestroy();
            return;
        }

        protected override void Start()
        {
            base.Start();
            if ((this.PulldownItemTemplate != null) == null)
            {
                goto Label_0028;
            }
            this.PulldownItemTemplate.get_gameObject().SetActive(0);
        Label_0028:
            return;
        }

        [CompilerGenerated]
        private sealed class <AddItem>c__AnonStorey3A0
        {
            internal int value;
            internal ScrollablePulldown <>f__this;

            public <AddItem>c__AnonStorey3A0()
            {
                base..ctor();
                return;
            }

            internal void <>m__402(SRPG_Button g)
            {
                this.<>f__this.Selection = this.value;
                this.<>f__this.ClosePulldown(0);
                this.<>f__this.TriggerItemChange();
                return;
            }
        }
    }
}

