namespace SRPG
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Runtime.CompilerServices;
    using UnityEngine;
    using UnityEngine.EventSystems;

    public class WeatherTooltip : MonoBehaviour
    {
        public eDispType DispType;
        public Tooltip PrefabTooltip;
        public float PosLeftOffset;
        public float PosRightOffset;
        public float PosUpOffset;
        private static Tooltip mTooltip;
        private CanvasGroup[] mParentCgs;

        static WeatherTooltip()
        {
        }

        public WeatherTooltip()
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
            <ResetPosiotion>c__Iterator2B iteratorb;
            iteratorb = new <ResetPosiotion>c__Iterator2B();
            iteratorb.<>f__this = this;
            return iteratorb;
        }

        private unsafe void ShowTooltip(PointerEventData event_data)
        {
            GameObject obj2;
            CanvasGroup[] groupArray;
            CanvasGroup group;
            CanvasGroup[] groupArray2;
            int num;
            WeatherParam param;
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
            param = DataSource.FindDataOfClass<WeatherParam>(base.get_gameObject(), null);
            if ((mTooltip.TooltipText != null) == null)
            {
                goto Label_0118;
            }
            if (param == null)
            {
                goto Label_0118;
            }
            mTooltip.TooltipText.set_text(string.Format(LocalizedText.Get("quest.WEATHER_DESC"), param.Name, param.Expr));
            mTooltip.EnableDisp = 0;
            base.StartCoroutine(this.ResetPosiotion());
        Label_0118:
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
        private sealed class <ResetPosiotion>c__Iterator2B : IEnumerator, IDisposable, IEnumerator<object>
        {
            internal Vector2 <size>__0;
            internal Vector2 <icon_size>__1;
            internal RectTransform <rc>__2;
            internal int $PC;
            internal object $current;
            internal WeatherTooltip <>f__this;

            public <ResetPosiotion>c__Iterator2B()
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
                WeatherTooltip.eDispType type;
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
                goto Label_0171;
            Label_0021:
                this.$current = new WaitForEndOfFrame();
                this.$PC = 1;
                goto Label_0173;
            Label_0038:
                this.<size>__0 = WeatherTooltip.mTooltip.BodySize;
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
                        goto Label_00E0;

                    case 2:
                        goto Label_010B;
                }
                goto Label_0155;
            Label_00A8:
                Tooltip.SetTooltipPosition(this.<rc>__2, new Vector2(-(&this.<size>__0.x + this.<>f__this.PosLeftOffset), this.<>f__this.PosUpOffset));
                goto Label_0155;
            Label_00E0:
                Tooltip.SetTooltipPosition(this.<rc>__2, new Vector2(this.<>f__this.PosRightOffset, this.<>f__this.PosUpOffset));
                goto Label_0155;
            Label_010B:
                Tooltip.SetTooltipPosition(this.<rc>__2, new Vector2(-((&this.<size>__0.x - &this.<icon_size>__1.x) / 2f), &this.<size>__0.y + this.<>f__this.PosUpOffset));
            Label_0155:
                WeatherTooltip.mTooltip.ResetPosition();
                WeatherTooltip.mTooltip.EnableDisp = 1;
                this.$PC = -1;
            Label_0171:
                return 0;
            Label_0173:
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

