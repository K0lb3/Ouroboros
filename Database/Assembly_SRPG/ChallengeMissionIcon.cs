namespace SRPG
{
    using GR;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Runtime.CompilerServices;
    using UnityEngine;
    using UnityEngine.UI;

    [Pin(10, "進捗表示", 1, 10), Pin(0, "更新", 0, 0), Pin(1, "表示", 0, 1), Pin(2, "非表示", 0, 2)]
    public class ChallengeMissionIcon : MonoBehaviour, IFlowInterface
    {
        public GameObject Badge;

        public ChallengeMissionIcon()
        {
            base..ctor();
            return;
        }

        public void Activated(int pinID)
        {
            int num;
            num = pinID;
            switch (num)
            {
                case 0:
                    goto Label_0019;

                case 1:
                    goto Label_0024;

                case 2:
                    goto Label_0030;
            }
            goto Label_003C;
        Label_0019:
            this.Refresh();
            goto Label_003C;
        Label_0024:
            this.ShowImages(1);
            goto Label_003C;
        Label_0030:
            this.ShowImages(0);
        Label_003C:
            return;
        }

        private bool IsNotReceiveRewards(TrophyParam rootTrophy)
        {
            TrophyParam[] paramArray;
            TrophyParam param;
            TrophyParam[] paramArray2;
            int num;
            TrophyState state;
            paramArray2 = ChallengeMission.GetChildeTrophies(rootTrophy);
            num = 0;
            goto Label_003A;
        Label_0010:
            param = paramArray2[num];
            state = ChallengeMission.GetTrophyCounter(param);
            if (state.IsCompleted == null)
            {
                goto Label_0036;
            }
            if (state.IsEnded != null)
            {
                goto Label_0036;
            }
            return 1;
        Label_0036:
            num += 1;
        Label_003A:
            if (num < ((int) paramArray2.Length))
            {
                goto Label_0010;
            }
            return 0;
        }

        private void Refresh()
        {
            GameManager manager;
            bool flag;
            bool flag2;
            TrophyParam[] paramArray;
            TrophyParam param;
            TrophyParam[] paramArray2;
            int num;
            TrophyState state;
            if ((MonoSingleton<GameManager>.Instance != null) == null)
            {
                goto Label_0093;
            }
            flag = 0;
            flag2 = 1;
            paramArray2 = ChallengeMission.GetRootTrophiesSortedByPriority();
            num = 0;
            goto Label_005F;
        Label_0027:
            param = paramArray2[num];
            if (ChallengeMission.GetTrophyCounter(param).IsEnded != null)
            {
                goto Label_0059;
            }
            if (this.IsNotReceiveRewards(param) == null)
            {
                goto Label_0052;
            }
            flag = 1;
        Label_0052:
            flag2 = 0;
            goto Label_006A;
        Label_0059:
            num += 1;
        Label_005F:
            if (num < ((int) paramArray2.Length))
            {
                goto Label_0027;
            }
        Label_006A:
            this.Badge.SetActive(flag);
            base.get_gameObject().SetActive(flag2 == 0);
            if (flag2 != null)
            {
                goto Label_0093;
            }
            FlowNode_GameObject.ActivateOutputLinks(this, 10);
        Label_0093:
            return;
        }

        [DebuggerHidden]
        private IEnumerator SetAsLastSibling()
        {
            <SetAsLastSibling>c__IteratorEB reb;
            reb = new <SetAsLastSibling>c__IteratorEB();
            reb.<>f__this = this;
            return reb;
        }

        public void ShowImages(bool value)
        {
            Image image;
            Button button;
            Image image2;
            image = base.GetComponent<Image>();
            if ((image != null) == null)
            {
                goto Label_001A;
            }
            image.set_enabled(value);
        Label_001A:
            button = base.GetComponent<Button>();
            if ((button != null) == null)
            {
                goto Label_0034;
            }
            button.set_enabled(value);
        Label_0034:
            if ((this.Badge != null) == null)
            {
                goto Label_0064;
            }
            image2 = this.Badge.GetComponent<Image>();
            if ((image2 != null) == null)
            {
                goto Label_0064;
            }
            image2.set_enabled(value);
        Label_0064:
            return;
        }

        private void Start()
        {
        }

        [CompilerGenerated]
        private sealed class <SetAsLastSibling>c__IteratorEB : IEnumerator, IDisposable, IEnumerator<object>
        {
            internal RectTransform <rect>__0;
            internal int $PC;
            internal object $current;
            internal ChallengeMissionIcon <>f__this;

            public <SetAsLastSibling>c__IteratorEB()
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
                goto Label_0068;
            Label_0021:
                this.$current = null;
                this.$PC = 1;
                goto Label_006A;
            Label_0034:
                this.<rect>__0 = this.<>f__this.GetComponent<RectTransform>();
                if ((this.<rect>__0 != null) == null)
                {
                    goto Label_0061;
                }
                this.<rect>__0.SetAsLastSibling();
            Label_0061:
                this.$PC = -1;
            Label_0068:
                return 0;
            Label_006A:
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

