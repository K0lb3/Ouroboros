namespace SRPG
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Runtime.CompilerServices;
    using UnityEngine;
    using UnityEngine.EventSystems;

    public class CondDescTooltip : MonoBehaviour
    {
        public eDispType DispType;
        public Tooltip PrefabTooltip;
        public ImageArray ImageCond;
        public float PosLeftOffset;
        public float PosRightOffset;
        public float PosUpOffset;
        private static Tooltip mTooltip;
        private CanvasGroup[] mParentCgs;

        static CondDescTooltip()
        {
        }

        public CondDescTooltip()
        {
            this.PosLeftOffset = 50f;
            this.PosRightOffset = 50f;
            this.PosUpOffset = 50f;
            base..ctor();
            return;
        }

        [DebuggerHidden]
        private IEnumerator ResetPosiotion()
        {
            <ResetPosiotion>c__Iterator5F iteratorf;
            iteratorf = new <ResetPosiotion>c__Iterator5F();
            iteratorf.<>f__this = this;
            return iteratorf;
        }

        private unsafe void ShowTooltip(PointerEventData event_data)
        {
            GameObject obj2;
            CanvasGroup[] groupArray;
            CanvasGroup group;
            CanvasGroup[] groupArray2;
            int num;
            int num2;
            string str;
            string str2;
            RaycastResult result;
            if (this.PrefabTooltip != null)
            {
                goto Label_0011;
            }
            return;
        Label_0011:
            if (mTooltip == null)
            {
                goto Label_0021;
            }
            return;
        Label_0021:
            if ((&event_data.get_pointerCurrentRaycast().get_gameObject() == null) == null)
            {
                goto Label_003E;
            }
            return;
        Label_003E:
            groupArray = this.ParentCgs;
            if (groupArray == null)
            {
                goto Label_007B;
            }
            groupArray2 = groupArray;
            num = 0;
            goto Label_0071;
        Label_0055:
            group = groupArray2[num];
            if (group.get_alpha() > 0f)
            {
                goto Label_006B;
            }
            return;
        Label_006B:
            num += 1;
        Label_0071:
            if (num < ((int) groupArray2.Length))
            {
                goto Label_0055;
            }
        Label_007B:
            if ((mTooltip == null) == null)
            {
                goto Label_00A0;
            }
            mTooltip = Object.Instantiate<Tooltip>(this.PrefabTooltip);
            goto Label_00AA;
        Label_00A0:
            mTooltip.ResetPosition();
        Label_00AA:
            if ((mTooltip.TooltipText != null) == null)
            {
                goto Label_016E;
            }
            if (this.ImageCond == null)
            {
                goto Label_016E;
            }
            num2 = this.ImageCond.ImageIndex;
            str = string.Empty;
            str2 = string.Empty;
            if (num2 < 0)
            {
                goto Label_010F;
            }
            if (num2 >= ((int) Unit.StrNameUnitConds.Length))
            {
                goto Label_010F;
            }
            str = LocalizedText.Get(Unit.StrNameUnitConds[num2]);
        Label_010F:
            if (num2 < 0)
            {
                goto Label_0134;
            }
            if (num2 >= ((int) Unit.StrDescUnitConds.Length))
            {
                goto Label_0134;
            }
            str2 = LocalizedText.Get(Unit.StrDescUnitConds[num2]);
        Label_0134:
            mTooltip.TooltipText.set_text(string.Format(LocalizedText.Get("quest.BUD_COND_DESC"), str, str2));
            mTooltip.EnableDisp = 0;
            base.StartCoroutine(this.ResetPosiotion());
        Label_016E:
            return;
        }

        private void Start()
        {
            UIEventListener listener;
            listener = SRPG_Extensions.RequireComponent<UIEventListener>(this);
            if (listener.onMove != null)
            {
                goto Label_0029;
            }
            listener.onPointerEnter = new UIEventListener.PointerEvent(this.ShowTooltip);
            goto Label_004B;
        Label_0029:
            listener.onPointerEnter = (UIEventListener.PointerEvent) Delegate.Combine(listener.onPointerEnter, new UIEventListener.PointerEvent(this.ShowTooltip));
        Label_004B:
            if (this.ImageCond != null)
            {
                goto Label_0067;
            }
            this.ImageCond = base.GetComponent<ImageArray>();
        Label_0067:
            return;
        }

        private CanvasGroup[] ParentCgs
        {
            get
            {
                if (this.mParentCgs != null)
                {
                    goto Label_0017;
                }
                this.mParentCgs = base.GetComponentsInParent<CanvasGroup>();
            Label_0017:
                return this.mParentCgs;
            }
        }

        [CompilerGenerated]
        private sealed class <ResetPosiotion>c__Iterator5F : IEnumerator, IDisposable, IEnumerator<object>
        {
            internal Vector2 <size>__0;
            internal Vector2 <icon_size>__1;
            internal RectTransform <rc>__2;
            internal int $PC;
            internal object $current;
            internal CondDescTooltip <>f__this;

            public <ResetPosiotion>c__Iterator5F()
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

            public unsafe bool MoveNext()
            {
                uint num;
                CondDescTooltip.eDispType type;
                bool flag;
                num = this.$PC;
                this.$PC = -1;
                switch (num)
                {
                    case 0:
                        goto Label_0021;

                    case 1:
                        goto Label_0038;
                }
                goto Label_0165;
            Label_0021:
                this.$current = new WaitForEndOfFrame();
                this.$PC = 1;
                goto Label_0167;
            Label_0038:
                this.<size>__0 = CondDescTooltip.mTooltip.BodySize;
                this.<icon_size>__1 = Vector2.get_zero();
                this.<rc>__2 = this.<>f__this.GetComponent<RectTransform>();
                if (this.<rc>__2 == null)
                {
                    goto Label_0085;
                }
                this.<icon_size>__1 = this.<rc>__2.get_sizeDelta();
            Label_0085:
                switch (this.<>f__this.DispType)
                {
                    case 0:
                        goto Label_00A8;

                    case 1:
                        goto Label_00DA;

                    case 2:
                        goto Label_00FF;
                }
                goto Label_0149;
            Label_00A8:
                Tooltip.SetTooltipPosition(this.<rc>__2, new Vector2(-(&this.<size>__0.x + this.<>f__this.PosLeftOffset), 0f));
                goto Label_0149;
            Label_00DA:
                Tooltip.SetTooltipPosition(this.<rc>__2, new Vector2(this.<>f__this.PosRightOffset, 0f));
                goto Label_0149;
            Label_00FF:
                Tooltip.SetTooltipPosition(this.<rc>__2, new Vector2(-((&this.<size>__0.x - &this.<icon_size>__1.x) / 2f), &this.<size>__0.y + this.<>f__this.PosUpOffset));
            Label_0149:
                CondDescTooltip.mTooltip.ResetPosition();
                CondDescTooltip.mTooltip.EnableDisp = 1;
                this.$PC = -1;
            Label_0165:
                return 0;
            Label_0167:
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

        public enum eDispType
        {
            LEFT,
            RIGHT,
            UP
        }
    }
}

