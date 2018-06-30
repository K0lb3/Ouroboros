namespace SRPG
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Runtime.CompilerServices;
    using UnityEngine;
    using UnityEngine.UI;

    public class DamageDsgPopup : MonoBehaviour
    {
        public Sprite[] DigitSprites;
        public Sprite BillionSprite;
        public Sprite TrillionSprite;
        public GameObject GoDispDigit;
        public float Spacing;
        public float SpacingUnit;
        public float DispTime;
        public float FadeTime;
        public float DelayTime;
        [SerializeField]
        private int mValue;
        [SerializeField]
        private Color mDigitColor;
        [SerializeField]
        private eDamageDispType mDamageDispType;
        private List<Digit> mDigitLists;
        private Digit mNumUnit;
        private float mPassedTime;
        private float mPassedFadeTime;
        private bool mIsInitialized;

        public DamageDsgPopup()
        {
            this.Spacing = 48f;
            this.SpacingUnit = 90f;
            this.DispTime = 1f;
            this.FadeTime = 0.3f;
            this.DelayTime = 0.03f;
            this.mDigitColor = new Color(1f, 1f, 1f, 1f);
            this.mDigitLists = new List<Digit>();
            base..ctor();
            return;
        }

        public unsafe void Setup(int value, Color color, eDamageDispType damage_disp_type)
        {
            int num;
            int num2;
            float num3;
            Sprite sprite;
            Digit digit;
            GameObject obj2;
            List<Digit> list;
            int num4;
            GameObject obj3;
            Sprite sprite2;
            Digit digit2;
            int num5;
            Digit digit3;
            List<Digit>.Enumerator enumerator;
            Animator animator;
            eDamageDispType type;
            Rect rect;
            Rect rect2;
            Rect rect3;
            Rect rect4;
            if (this.GoDispDigit != null)
            {
                goto Label_0011;
            }
            return;
        Label_0011:
            this.GoDispDigit.SetActive(0);
            this.mValue = value;
            this.mDigitColor = color;
            this.mDamageDispType = damage_disp_type;
            num = 1;
            num2 = this.mValue;
            goto Label_0049;
        Label_0040:
            num += 1;
            num2 /= 10;
        Label_0049:
            if (num2 >= 10)
            {
                goto Label_0040;
            }
            this.mDigitLists.Clear();
            num3 = ((float) num) * this.Spacing;
            sprite = null;
            type = this.mDamageDispType;
            if (type == 1)
            {
                goto Label_0085;
            }
            if (type == 2)
            {
                goto Label_0091;
            }
            goto Label_009D;
        Label_0085:
            sprite = this.BillionSprite;
            goto Label_009D;
        Label_0091:
            sprite = this.TrillionSprite;
        Label_009D:
            digit = null;
            if ((sprite != null) == null)
            {
                goto Label_0180;
            }
            obj2 = Object.Instantiate<GameObject>(this.GoDispDigit);
            if (obj2 == null)
            {
                goto Label_0180;
            }
            digit = new Digit();
            digit.mPosition = new Vector2(num3 * 0.5f, 0f);
            digit.mTransform = obj2.get_transform() as RectTransform;
            digit.mTransform.SetParent(base.get_transform(), 0);
            digit.mTransform.set_sizeDelta(new Vector2(&sprite.get_textureRect().get_width(), &sprite.get_textureRect().get_height()));
            digit.mTransform.set_anchoredPosition(digit.mPosition);
            digit.mImage = obj2.GetComponent<Image>();
            if (digit.mImage == null)
            {
                goto Label_0178;
            }
            digit.mImage.set_sprite(sprite);
        Label_0178:
            obj2.SetActive(1);
        Label_0180:
            list = new List<Digit>();
            num2 = this.mValue;
            num4 = 0;
            goto Label_02BE;
        Label_0196:
            obj3 = Object.Instantiate<GameObject>(this.GoDispDigit);
            if (obj3 == null)
            {
                goto Label_02B3;
            }
            sprite2 = this.DigitSprites[num2 % 10];
            digit2 = new Digit();
            digit2.mPosition = new Vector2(((num3 * 0.5f) - (this.SpacingUnit * 0.5f)) - (this.Spacing * (((float) num4) + 0.5f)), 0f);
            digit2.mTransform = obj3.get_transform() as RectTransform;
            digit2.mTransform.SetParent(base.get_transform(), 0);
            digit2.mTransform.set_sizeDelta(new Vector2(&sprite2.get_textureRect().get_width(), &sprite2.get_textureRect().get_height()));
            digit2.mTransform.set_anchoredPosition(digit2.mPosition);
            digit2.mImage = obj3.GetComponent<Image>();
            if (digit2.mImage == null)
            {
                goto Label_02A2;
            }
            digit2.mImage.set_sprite(sprite2);
            digit2.mImage.set_color(this.mDigitColor);
        Label_02A2:
            list.Add(digit2);
            obj3.SetActive(1);
        Label_02B3:
            num2 /= 10;
            num4 += 1;
        Label_02BE:
            if (num4 < num)
            {
                goto Label_0196;
            }
            num5 = list.Count - 1;
            goto Label_02F0;
        Label_02D6:
            this.mDigitLists.Add(list[num5]);
            num5 -= 1;
        Label_02F0:
            if (num5 >= 0)
            {
                goto Label_02D6;
            }
            if (digit == null)
            {
                goto Label_030C;
            }
            this.mDigitLists.Add(digit);
        Label_030C:
            this.mPassedTime = 0f;
            this.mPassedFadeTime = 0f;
            this.mIsInitialized = 1;
            if (this.mDigitLists.Count == null)
            {
                goto Label_03A6;
            }
            enumerator = this.mDigitLists.GetEnumerator();
        Label_0346:
            try
            {
                goto Label_037B;
            Label_034B:
                digit3 = &enumerator.Current;
                animator = digit3.mImage.get_gameObject().GetComponent<Animator>();
                if (animator == null)
                {
                    goto Label_037B;
                }
                animator.set_enabled(0);
            Label_037B:
                if (&enumerator.MoveNext() != null)
                {
                    goto Label_034B;
                }
                goto Label_0399;
            }
            finally
            {
            Label_038C:
                ((List<Digit>.Enumerator) enumerator).Dispose();
            }
        Label_0399:
            base.StartCoroutine(this.startDelayAnm());
        Label_03A6:
            return;
        }

        [DebuggerHidden]
        private IEnumerator startDelayAnm()
        {
            <startDelayAnm>c__Iterator2C iteratorc;
            iteratorc = new <startDelayAnm>c__Iterator2C();
            iteratorc.<>f__this = this;
            return iteratorc;
        }

        private unsafe void Update()
        {
            float num;
            int num2;
            Color color;
            if (this.mIsInitialized != null)
            {
                goto Label_000C;
            }
            return;
        Label_000C:
            this.mPassedTime += Time.get_deltaTime();
            if (this.mPassedTime < this.DispTime)
            {
                goto Label_00B4;
            }
            this.mPassedFadeTime += Time.get_deltaTime();
            if (this.mPassedFadeTime < this.FadeTime)
            {
                goto Label_005E;
            }
            Object.Destroy(base.get_gameObject());
            return;
        Label_005E:
            num = 1f - (this.mPassedFadeTime / this.FadeTime);
            num2 = 0;
            goto Label_00A3;
        Label_0079:
            color = this.mDigitColor;
            &color.a = num;
            this.mDigitLists[num2].mImage.set_color(color);
            num2 += 1;
        Label_00A3:
            if (num2 < this.mDigitLists.Count)
            {
                goto Label_0079;
            }
        Label_00B4:
            return;
        }

        [CompilerGenerated]
        private sealed class <startDelayAnm>c__Iterator2C : IEnumerator, IDisposable, IEnumerator<object>
        {
            internal List<DamageDsgPopup.Digit>.Enumerator <$s_216>__0;
            internal DamageDsgPopup.Digit <digit>__1;
            internal Animator <anm>__2;
            internal int $PC;
            internal object $current;
            internal DamageDsgPopup <>f__this;

            public <startDelayAnm>c__Iterator2C()
            {
                base..ctor();
                return;
            }

            [DebuggerHidden]
            public void Dispose()
            {
                uint num;
                num = this.$PC;
                this.$PC = -1;
                switch (num)
                {
                    case 0:
                        goto Label_0037;

                    case 1:
                        goto Label_0021;
                }
                goto Label_0037;
            Label_0021:
                try
                {
                    goto Label_0037;
                }
                finally
                {
                Label_0026:
                    ((List<DamageDsgPopup.Digit>.Enumerator) this.<$s_216>__0).Dispose();
                }
            Label_0037:
                return;
            }

            public unsafe bool MoveNext()
            {
                uint num;
                bool flag;
                bool flag2;
                num = this.$PC;
                this.$PC = -1;
                flag = 0;
                switch (num)
                {
                    case 0:
                        goto Label_0023;

                    case 1:
                        goto Label_003C;
                }
                goto Label_00EA;
            Label_0023:
                this.<$s_216>__0 = this.<>f__this.mDigitLists.GetEnumerator();
                num = -3;
            Label_003C:
                try
                {
                    switch ((num - 1))
                    {
                        case 0:
                            goto Label_00B9;
                    }
                    goto Label_00B9;
                Label_004D:
                    this.<digit>__1 = &this.<$s_216>__0.Current;
                    this.<anm>__2 = this.<digit>__1.mImage.get_gameObject().GetComponent<Animator>();
                    if (this.<anm>__2 == null)
                    {
                        goto Label_00B9;
                    }
                    this.<anm>__2.set_enabled(1);
                    this.$current = new WaitForSeconds(this.<>f__this.DelayTime);
                    this.$PC = 1;
                    flag = 1;
                    goto Label_00EC;
                Label_00B9:
                    if (&this.<$s_216>__0.MoveNext() != null)
                    {
                        goto Label_004D;
                    }
                    goto Label_00E3;
                }
                finally
                {
                Label_00CE:
                    if (flag == null)
                    {
                        goto Label_00D2;
                    }
                Label_00D2:
                    ((List<DamageDsgPopup.Digit>.Enumerator) this.<$s_216>__0).Dispose();
                }
            Label_00E3:
                this.$PC = -1;
            Label_00EA:
                return 0;
            Label_00EC:
                return 1;
                return flag2;
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

        private class Digit
        {
            public RectTransform mTransform;
            public Image mImage;
            public Vector2 mPosition;

            public Digit()
            {
                base..ctor();
                return;
            }
        }
    }
}

