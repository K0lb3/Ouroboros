namespace SRPG
{
    using System;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.UI;

    public class ShopTelop : MonoBehaviour
    {
        public Text BodyText;
        public float FadeInSec;
        public float FadeInInterval;
        public float FadeOutSec;
        public float FadeOutInterval;
        public GameObject WindowAnimator;
        public string WindowOpenProperty;
        public string WindowOpeningState;
        public string WindowCloseProperty;
        public string WindowClosingState;
        public string WindowClosedState;
        public string WindowLoopState;
        public CanvasGroup WindowAlphaCanvasGroup;
        private List<TextChar> chList;
        private float mPastSec;
        private EState mState;
        private string mNextText;
        private bool mNextTextUpdated;
        private bool mFadeOut;

        public ShopTelop()
        {
            this.FadeInSec = 1f;
            this.FadeInInterval = 0.05f;
            this.FadeOutSec = 0.1f;
            this.FadeOutInterval = 0.005f;
            this.WindowOpenProperty = "close:false";
            this.WindowOpeningState = "open";
            this.WindowCloseProperty = "close:true";
            this.WindowClosingState = "close";
            this.WindowLoopState = "loop";
            this.chList = new List<TextChar>();
            base..ctor();
            return;
        }

        private void Awake()
        {
            base.get_gameObject().SetActive(0);
            return;
        }

        private bool IsCanvasGroupAlphaZero()
        {
            if ((this.WindowAlphaCanvasGroup == null) == null)
            {
                goto Label_0013;
            }
            return 0;
        Label_0013:
            return ((this.WindowAlphaCanvasGroup.get_alpha() > 0f) == 0);
        }

        private unsafe bool IsWindowState(string state)
        {
            Animator animator;
            AnimatorStateInfo info;
            if (string.IsNullOrEmpty(state) == null)
            {
                goto Label_000D;
            }
            return 0;
        Label_000D:
            animator = ((this.WindowAnimator == null) == null) ? this.WindowAnimator.GetComponent<Animator>() : null;
            if ((animator == null) == null)
            {
                goto Label_003E;
            }
            return 1;
        Label_003E:
            return &animator.GetCurrentAnimatorStateInfo(0).IsName(state);
        }

        public void SetText(string text)
        {
            this.mNextText = text;
            this.mNextTextUpdated = 1;
            if (string.IsNullOrEmpty(this.mNextText) != null)
            {
                goto Label_002A;
            }
            base.get_gameObject().SetActive(1);
        Label_002A:
            return;
        }

        private void StartTextAnim()
        {
            int num;
            char ch;
            string str;
            int num2;
            TextChar ch2;
            this.chList.Clear();
            if (this.mNextText == null)
            {
                goto Label_0072;
            }
            num = 0;
            str = this.mNextText;
            num2 = 0;
            goto Label_0066;
        Label_0026:
            ch = str[num2];
            ch2 = new TextChar();
            ch2.index = num++;
            ch2.ch = ch;
            ch2.alpha = 0f;
            this.chList.Add(ch2);
            num2 += 1;
        Label_0066:
            if (num2 < str.Length)
            {
                goto Label_0026;
            }
        Label_0072:
            this.mPastSec = 0f;
            this.mFadeOut = 0;
            this.mNextText = null;
            this.mNextTextUpdated = 0;
            return;
        }

        private void StartWindowAnim(string property)
        {
            char[] chArray1;
            Animator animator;
            string[] strArray;
            bool flag;
            animator = ((this.WindowAnimator == null) == null) ? this.WindowAnimator.GetComponent<Animator>() : null;
            if ((animator == null) == null)
            {
                goto Label_0030;
            }
            return;
        Label_0030:
            chArray1 = new char[] { 0x3a };
            strArray = property.Split(chArray1);
            flag = 1;
            if (((int) strArray.Length) <= 1)
            {
                goto Label_005B;
            }
            flag = strArray[1].Equals("true");
        Label_005B:
            animator.SetBool(strArray[0], flag);
            return;
        }

        private void Update()
        {
            if ((this.BodyText == null) == null)
            {
                goto Label_0012;
            }
            return;
        Label_0012:
            if (this.mState != null)
            {
                goto Label_0046;
            }
            this.BodyText.set_text(string.Empty);
            if (this.UpdateWindowState() != null)
            {
                goto Label_0039;
            }
            return;
        Label_0039:
            this.StartTextAnim();
            this.mState = 1;
        Label_0046:
            if (this.mNextTextUpdated != null)
            {
                goto Label_0058;
            }
            this.UpdateTextAnim();
            return;
        Label_0058:
            if (this.UpdateTextOut() != null)
            {
                goto Label_0064;
            }
            return;
        Label_0064:
            if (string.IsNullOrEmpty(this.mNextText) != null)
            {
                goto Label_007B;
            }
            this.StartTextAnim();
            return;
        Label_007B:
            this.StartWindowAnim(this.WindowCloseProperty);
            this.mState = 0;
            return;
        }

        private unsafe void UpdateTextAnim()
        {
            TextChar ch;
            List<TextChar>.Enumerator enumerator;
            float num;
            float num2;
            this.mPastSec += Time.get_deltaTime();
            enumerator = this.chList.GetEnumerator();
        Label_001E:
            try
            {
                goto Label_00A0;
            Label_0023:
                ch = &enumerator.Current;
                num = ((float) ch.index) * this.FadeInInterval;
                num2 = num + this.FadeInSec;
                if (this.mPastSec > num)
                {
                    goto Label_005F;
                }
                ch.alpha = 0f;
                goto Label_00A0;
            Label_005F:
                if (num2 <= this.mPastSec)
                {
                    goto Label_007B;
                }
                if (this.FadeInSec > 0f)
                {
                    goto Label_008B;
                }
            Label_007B:
                ch.alpha = 1f;
                goto Label_00A0;
            Label_008B:
                ch.alpha = (this.mPastSec - num) / this.FadeInSec;
            Label_00A0:
                if (&enumerator.MoveNext() != null)
                {
                    goto Label_0023;
                }
                goto Label_00BD;
            }
            finally
            {
            Label_00B1:
                ((List<TextChar>.Enumerator) enumerator).Dispose();
            }
        Label_00BD:
            this.UpdateTextString();
            return;
        }

        private unsafe bool UpdateTextOut()
        {
            bool flag;
            TextChar ch;
            List<TextChar>.Enumerator enumerator;
            float num;
            float num2;
            float num3;
            if (this.mFadeOut != null)
            {
                goto Label_001D;
            }
            this.mFadeOut = 1;
            this.mPastSec = 0f;
        Label_001D:
            this.mPastSec += Time.get_deltaTime();
            flag = 1;
            enumerator = this.chList.GetEnumerator();
        Label_003D:
            try
            {
                goto Label_00DB;
            Label_0042:
                ch = &enumerator.Current;
                num = ((float) ch.index) * this.FadeOutInterval;
                num2 = num + this.FadeOutSec;
                num3 = 1f;
                if (this.mPastSec > num)
                {
                    goto Label_0082;
                }
                num3 = 1f;
                goto Label_00BD;
            Label_0082:
                if (num2 <= this.mPastSec)
                {
                    goto Label_009F;
                }
                if (this.FadeOutSec > 0f)
                {
                    goto Label_00AB;
                }
            Label_009F:
                num3 = 0f;
                goto Label_00BD;
            Label_00AB:
                num3 = (num2 - this.mPastSec) / this.FadeOutSec;
            Label_00BD:
                ch.alpha *= num3;
                flag &= (num3 > 0f) == 0;
            Label_00DB:
                if (&enumerator.MoveNext() != null)
                {
                    goto Label_0042;
                }
                goto Label_00F8;
            }
            finally
            {
            Label_00EC:
                ((List<TextChar>.Enumerator) enumerator).Dispose();
            }
        Label_00F8:
            this.UpdateTextString();
            return flag;
        }

        private unsafe void UpdateTextString()
        {
            object[] objArray1;
            Color color;
            string str;
            TextChar ch;
            List<TextChar>.Enumerator enumerator;
            char ch2;
            byte num;
            byte num2;
            byte num3;
            byte num4;
            string str2;
            if ((this.BodyText == null) == null)
            {
                goto Label_0012;
            }
            return;
        Label_0012:
            color = this.BodyText.get_color();
            str = string.Empty;
            enumerator = this.chList.GetEnumerator();
        Label_0030:
            try
            {
                goto Label_00D3;
            Label_0035:
                ch = &enumerator.Current;
                ch2 = ch.ch;
                num = (byte) (255f * ch.alpha);
                num2 = (byte) (255f * &color.r);
                num3 = (byte) (255f * &color.g);
                num4 = (byte) (255f * &color.b);
                objArray1 = new object[] { (byte) num2, (byte) num3, (byte) num4, (byte) num };
                str2 = string.Format("<color=#{0:X2}{1:X2}{2:X2}{3:X2}>", objArray1);
                str = str + str2 + &ch2.ToString() + "</color>";
            Label_00D3:
                if (&enumerator.MoveNext() != null)
                {
                    goto Label_0035;
                }
                goto Label_00F0;
            }
            finally
            {
            Label_00E4:
                ((List<TextChar>.Enumerator) enumerator).Dispose();
            }
        Label_00F0:
            this.BodyText.set_text(str);
            return;
        }

        private bool UpdateWindowState()
        {
            if (string.IsNullOrEmpty(this.mNextText) == null)
            {
                goto Label_006D;
            }
            if (this.IsWindowState(this.WindowClosedState) != null)
            {
                goto Label_002C;
            }
            if (this.IsCanvasGroupAlphaZero() == null)
            {
                goto Label_003D;
            }
        Label_002C:
            base.get_gameObject().SetActive(0);
            goto Label_006B;
        Label_003D:
            if (this.IsWindowState(this.WindowOpeningState) != null)
            {
                goto Label_005F;
            }
            if (this.IsWindowState(this.WindowLoopState) == null)
            {
                goto Label_006B;
            }
        Label_005F:
            this.StartWindowAnim(this.WindowCloseProperty);
        Label_006B:
            return 0;
        Label_006D:
            if (this.IsWindowState(this.WindowClosingState) != null)
            {
                goto Label_008F;
            }
            if (this.IsWindowState(this.WindowClosedState) == null)
            {
                goto Label_009B;
            }
        Label_008F:
            this.StartWindowAnim(this.WindowOpenProperty);
        Label_009B:
            return this.IsWindowState(this.WindowLoopState);
        }

        private enum EState
        {
            NOP,
            ACTIVE
        }

        private class TextChar
        {
            public int index;
            public char ch;
            public float alpha;

            public TextChar()
            {
                base..ctor();
                return;
            }
        }
    }
}

