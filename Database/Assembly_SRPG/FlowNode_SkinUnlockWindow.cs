namespace SRPG
{
    using GR;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Runtime.CompilerServices;

    [NodeType("UI/SkinUnlockWindow", 0x7fe5), Pin(1, "Closed", 1, 1), Pin(10, "Open", 0, 0)]
    public class FlowNode_SkinUnlockWindow : FlowNode
    {
        public FlowNode_SkinUnlockWindow()
        {
            base..ctor();
            return;
        }

        public override void OnActivate(int pinID)
        {
            GameManager manager;
            List<ItemParam> list;
            ItemData[] dataArray;
            int num;
            if (pinID != 10)
            {
                goto Label_007B;
            }
            manager = MonoSingleton<GameManager>.Instance;
            list = new List<ItemParam>();
            dataArray = manager.Player.Items.ToArray();
            num = 0;
            goto Label_004B;
        Label_002C:
            if (dataArray[num].IsNewSkin == null)
            {
                goto Label_0047;
            }
            list.Add(dataArray[num].Param);
        Label_0047:
            num += 1;
        Label_004B:
            if (num < ((int) dataArray.Length))
            {
                goto Label_002C;
            }
            if (list.Count < 1)
            {
                goto Label_0073;
            }
            base.StartCoroutine(this.OnOpenAsync(list));
            goto Label_007B;
        Label_0073:
            base.ActivateOutputLinks(1);
        Label_007B:
            return;
        }

        [DebuggerHidden]
        private IEnumerator OnOpenAsync(List<ItemParam> showItems)
        {
            <OnOpenAsync>c__IteratorCA rca;
            rca = new <OnOpenAsync>c__IteratorCA();
            rca.showItems = showItems;
            rca.<$>showItems = showItems;
            rca.<>f__this = this;
            return rca;
        }

        [CompilerGenerated]
        private sealed class <OnOpenAsync>c__IteratorCA : IEnumerator, IDisposable, IEnumerator<object>
        {
            internal List<ItemParam> showItems;
            internal int $PC;
            internal object $current;
            internal List<ItemParam> <$>showItems;
            internal FlowNode_SkinUnlockWindow <>f__this;

            public <OnOpenAsync>c__IteratorCA()
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
                        goto Label_0048;
                }
                goto Label_005C;
            Label_0021:
                this.$current = MonoSingleton<GameManager>.Instance.SkinUnlockPopup(this.showItems.ToArray());
                this.$PC = 1;
                goto Label_005E;
            Label_0048:
                this.<>f__this.ActivateOutputLinks(1);
                this.$PC = -1;
            Label_005C:
                return 0;
            Label_005E:
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
    }
}

