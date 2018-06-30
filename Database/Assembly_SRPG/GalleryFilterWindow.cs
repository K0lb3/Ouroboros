namespace SRPG
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using UnityEngine;
    using UnityEngine.Events;
    using UnityEngine.UI;

    [Pin(2, "Enable All Toggle", 0, 2), Pin(3, "Disable All Toggle", 0, 3), Pin(100, "Close", 1, 100), Pin(1, "Save Setting", 0, 1)]
    public class GalleryFilterWindow : MonoBehaviour, IFlowInterface
    {
        private const int SAVE_SETTING = 1;
        private const int ENABLE_ALL_TOGGLE = 2;
        private const int DISABLE_ALL_TOGGLE = 3;
        private const int OUTPUT_CLOSE = 100;
        [SerializeField]
        private Toggle[] mToggles;
        private GalleryItemListWindow.Settings mSettings;
        private List<int> mRareFiltters;
        [CompilerGenerated]
        private static Func<int, int> <>f__am$cache3;

        public GalleryFilterWindow()
        {
            this.mToggles = new Toggle[0];
            base..ctor();
            return;
        }

        [CompilerGenerated]
        private static int <Awake>m__33B(int x)
        {
            return x;
        }

        public void Activated(int pinID)
        {
            Toggle toggle;
            Toggle[] toggleArray;
            int num;
            Toggle toggle2;
            Toggle[] toggleArray2;
            int num2;
            int num3;
            num3 = pinID;
            switch ((num3 - 1))
            {
                case 0:
                    goto Label_001D;

                case 1:
                    goto Label_003C;

                case 2:
                    goto Label_006E;
            }
            goto Label_00A8;
        Label_001D:
            this.mSettings.rareFilters = this.mRareFiltters.ToArray();
            FlowNode_GameObject.ActivateOutputLinks(this, 100);
            return;
        Label_003C:
            if (this.mToggles == null)
            {
                goto Label_006D;
            }
            toggleArray = this.mToggles;
            num = 0;
            goto Label_0064;
        Label_0055:
            toggle = toggleArray[num];
            toggle.set_isOn(1);
            num += 1;
        Label_0064:
            if (num < ((int) toggleArray.Length))
            {
                goto Label_0055;
            }
        Label_006D:
            return;
        Label_006E:
            if (this.mToggles == null)
            {
                goto Label_00A7;
            }
            toggleArray2 = this.mToggles;
            num2 = 0;
            goto Label_009C;
        Label_0089:
            toggle2 = toggleArray2[num2];
            toggle2.set_isOn(0);
            num2 += 1;
        Label_009C:
            if (num2 < ((int) toggleArray2.Length))
            {
                goto Label_0089;
            }
        Label_00A7:
            return;
        Label_00A8:
            return;
        }

        private unsafe void Awake()
        {
            SerializeValueList list;
            Toggle toggle;
            Toggle[] toggleArray;
            int num;
            int num2;
            List<int>.Enumerator enumerator;
            int num3;
            <Awake>c__AnonStorey348 storey;
            list = FlowNode_ButtonEvent.currentValue as SerializeValueList;
            if (list != null)
            {
                goto Label_0012;
            }
            return;
        Label_0012:
            this.mSettings = list.GetObject("settings") as GalleryItemListWindow.Settings;
            if (this.mSettings != null)
            {
                goto Label_0034;
            }
            return;
        Label_0034:
            if (<>f__am$cache3 != null)
            {
                goto Label_0058;
            }
            <>f__am$cache3 = new Func<int, int>(GalleryFilterWindow.<Awake>m__33B);
        Label_0058:
            this.mRareFiltters = Enumerable.ToList<int>(Enumerable.OrderBy<int, int>(this.mSettings.rareFilters, <>f__am$cache3));
            toggleArray = this.mToggles;
            num = 0;
            goto Label_0089;
        Label_007A:
            toggle = toggleArray[num];
            toggle.set_isOn(0);
            num += 1;
        Label_0089:
            if (num < ((int) toggleArray.Length))
            {
                goto Label_007A;
            }
            if (this.mToggles == null)
            {
                goto Label_010A;
            }
            if (((int) this.mToggles.Length) < 0)
            {
                goto Label_010A;
            }
            enumerator = this.mRareFiltters.GetEnumerator();
        Label_00B8:
            try
            {
                goto Label_00EC;
            Label_00BD:
                num2 = &enumerator.Current;
                if (num2 < 0)
                {
                    goto Label_00EC;
                }
                if (num2 >= ((int) this.mToggles.Length))
                {
                    goto Label_00EC;
                }
                this.mToggles[num2].set_isOn(1);
            Label_00EC:
                if (&enumerator.MoveNext() != null)
                {
                    goto Label_00BD;
                }
                goto Label_010A;
            }
            finally
            {
            Label_00FD:
                ((List<int>.Enumerator) enumerator).Dispose();
            }
        Label_010A:
            if (this.mToggles == null)
            {
                goto Label_0178;
            }
            if (((int) this.mToggles.Length) < 0)
            {
                goto Label_0178;
            }
            num3 = 0;
            goto Label_0169;
        Label_012B:
            storey = new <Awake>c__AnonStorey348();
            storey.<>f__this = this;
            storey.index = num3;
            this.mToggles[num3].onValueChanged.AddListener(new UnityAction<bool>(storey, this.<>m__33C));
            num3 += 1;
        Label_0169:
            if (num3 < ((int) this.mToggles.Length))
            {
                goto Label_012B;
            }
        Label_0178:
            return;
        }

        [CompilerGenerated]
        private sealed class <Awake>c__AnonStorey348
        {
            internal int index;
            internal GalleryFilterWindow <>f__this;
            private static Func<int, int> <>f__am$cache2;

            public <Awake>c__AnonStorey348()
            {
                base..ctor();
                return;
            }

            internal void <>m__33C(bool isOn)
            {
                if (isOn == null)
                {
                    goto Label_0063;
                }
                this.<>f__this.mRareFiltters.Add(this.index);
                if (<>f__am$cache2 != null)
                {
                    goto Label_004A;
                }
                <>f__am$cache2 = new Func<int, int>(GalleryFilterWindow.<Awake>c__AnonStorey348.<>m__33D);
            Label_004A:
                this.<>f__this.mRareFiltters = Enumerable.ToList<int>(Enumerable.OrderBy<int, int>(Enumerable.Distinct<int>(this.<>f__this.mRareFiltters), <>f__am$cache2));
                goto Label_007A;
            Label_0063:
                this.<>f__this.mRareFiltters.Remove(this.index);
            Label_007A:
                return;
            }

            private static int <>m__33D(int x)
            {
                return x;
            }
        }
    }
}

