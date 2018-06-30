namespace SRPG
{
    using System;
    using System.Runtime.CompilerServices;
    using UnityEngine;
    using UnityEngine.UI;

    [Pin(100, "Level Up", 1, 100)]
    public class ExpPanel : MonoBehaviour, IFlowInterface
    {
        public Text Level;
        public Text LevelMax;
        public Text ValueCurrent;
        public Text ValueCurrentInLv;
        public Text ValueLeft;
        public Text ValueTotal;
        public Slider LevelSlider;
        public Slider ExpSlider;
        public float ExpAnimLength;
        private int mCurrentExp;
        private float mExpStart;
        private float mExpEnd;
        private float mExpAnimTime;
        private int mLevelCap;
        private CalcEvent mOnCalcExp;
        private CalcEvent mOnCalcLevel;
        public LevelChangeEvent OnLevelChange;
        public ExpPanelEvent OnFinish;
        [CompilerGenerated]
        private static LevelChangeEvent <>f__am$cache12;
        [CompilerGenerated]
        private static ExpPanelEvent <>f__am$cache13;

        public ExpPanel()
        {
            if (<>f__am$cache12 != null)
            {
                goto Label_0019;
            }
            <>f__am$cache12 = new LevelChangeEvent(ExpPanel.<OnLevelChange>m__315);
        Label_0019:
            this.OnLevelChange = <>f__am$cache12;
            if (<>f__am$cache13 != null)
            {
                goto Label_003C;
            }
            <>f__am$cache13 = new ExpPanelEvent(ExpPanel.<OnFinish>m__316);
        Label_003C:
            this.OnFinish = <>f__am$cache13;
            base..ctor();
            return;
        }

        [CompilerGenerated]
        private static void <OnFinish>m__316()
        {
        }

        [CompilerGenerated]
        private static void <OnLevelChange>m__315(int a, int b)
        {
        }

        public void Activated(int pinID)
        {
        }

        private void AnimateExp(float dt)
        {
            float num;
            bool flag;
            float num2;
            int num3;
            int num4;
            int num5;
            int num6;
            bool flag2;
            float num7;
            if (this.mOnCalcExp == null)
            {
                goto Label_0016;
            }
            if (this.mOnCalcLevel != null)
            {
                goto Label_0017;
            }
        Label_0016:
            return;
        Label_0017:
            num = Mathf.Lerp(this.mExpStart, this.mExpEnd, this.mExpAnimTime / this.ExpAnimLength);
            this.mExpAnimTime += dt;
            flag = (this.mExpAnimTime < this.ExpAnimLength) == 0;
            if (flag == null)
            {
                goto Label_0068;
            }
            num2 = this.mExpEnd;
            goto Label_008C;
        Label_0068:
            num2 = Mathf.Lerp(this.mExpStart, this.mExpEnd, Mathf.Clamp01(this.mExpAnimTime / this.ExpAnimLength));
        Label_008C:
            num3 = Mathf.FloorToInt(num);
            num4 = Mathf.FloorToInt(num2);
            num5 = Mathf.Min(this.mOnCalcLevel(num3), this.mLevelCap);
            num6 = Mathf.Min(this.mOnCalcLevel(num4), this.mLevelCap);
            flag2 = (num5 == num6) == 0;
            this.mCurrentExp = Mathf.FloorToInt(num2);
            this.SetValues(num2);
            if (flag2 == null)
            {
                goto Label_0113;
            }
            this.OnLevelChange(num5, num6);
            if (num5 >= num6)
            {
                goto Label_0113;
            }
            FlowNode_GameObject.ActivateOutputLinks(this, 100);
        Label_0113:
            if (flag == null)
            {
                goto Label_0143;
            }
            base.set_enabled(0);
            this.mExpStart = this.mExpEnd = (float) this.mCurrentExp;
            this.OnFinish();
        Label_0143:
            return;
        }

        public void AnimateTo(int newExp)
        {
            this.mExpStart = Mathf.Lerp(this.mExpStart, this.mExpEnd, Mathf.Clamp01(this.mExpAnimTime / this.ExpAnimLength));
            this.mExpEnd = (float) newExp;
            this.mExpAnimTime = 0f;
            base.set_enabled(1);
            return;
        }

        public void SetDelegate(CalcEvent expFromLv, CalcEvent lvFromExp)
        {
            this.mOnCalcExp = expFromLv;
            this.mOnCalcLevel = lvFromExp;
            return;
        }

        private unsafe void SetValues(float exp)
        {
            int num;
            int num2;
            int num3;
            int num4;
            int num5;
            int num6;
            int num7;
            num = Mathf.FloorToInt(exp);
            num2 = Mathf.Min(this.mOnCalcLevel(num), this.mLevelCap);
            num3 = this.mOnCalcExp(num2);
            num4 = this.mOnCalcExp(Mathf.Min(num2 + 1, this.mLevelCap));
            num = Mathf.Min(num, num4);
            if ((this.Level != null) == null)
            {
                goto Label_0071;
            }
            this.Level.set_text(&num2.ToString());
        Label_0071:
            if ((this.LevelSlider != null) == null)
            {
                goto Label_00BC;
            }
            this.LevelSlider.set_maxValue((float) this.mOnCalcExp(this.mLevelCap));
            this.LevelSlider.set_minValue(0f);
            this.LevelSlider.set_value((float) num);
        Label_00BC:
            if ((this.ExpSlider != null) == null)
            {
                goto Label_0135;
            }
            if (num2 < this.mLevelCap)
            {
                goto Label_010E;
            }
            this.ExpSlider.set_maxValue(1f);
            this.ExpSlider.set_minValue(0f);
            this.ExpSlider.set_value(1f);
            goto Label_0135;
        Label_010E:
            this.ExpSlider.set_maxValue((float) num4);
            this.ExpSlider.set_minValue((float) num3);
            this.ExpSlider.set_value((float) num);
        Label_0135:
            if ((this.ValueCurrent != null) == null)
            {
                goto Label_0158;
            }
            this.ValueCurrent.set_text(&num.ToString());
        Label_0158:
            if ((this.ValueLeft != null) == null)
            {
                goto Label_0180;
            }
            num5 = num4 - num;
            this.ValueLeft.set_text(&num5.ToString());
        Label_0180:
            if ((this.ValueCurrentInLv != null) == null)
            {
                goto Label_01A8;
            }
            num6 = num - num3;
            this.ValueCurrentInLv.set_text(&num6.ToString());
        Label_01A8:
            if ((this.ValueTotal != null) == null)
            {
                goto Label_01D0;
            }
            num7 = num4 - num3;
            this.ValueTotal.set_text(&num7.ToString());
        Label_01D0:
            return;
        }

        public void Stop()
        {
            this.mExpEnd = this.mExpStart;
            base.set_enabled(0);
            return;
        }

        private void Update()
        {
            if (this.mExpStart >= this.mExpEnd)
            {
                goto Label_001C;
            }
            this.AnimateExp(Time.get_unscaledDeltaTime());
        Label_001C:
            return;
        }

        public bool IsBusy
        {
            get
            {
                return ((this.mExpStart == this.mExpEnd) == 0);
            }
        }

        public int Exp
        {
            get
            {
                return this.mCurrentExp;
            }
            set
            {
                float num;
                this.mCurrentExp = value;
                this.mExpStart = this.mExpEnd = (float) this.mCurrentExp;
                this.SetValues((float) this.mCurrentExp);
                this.Stop();
                return;
            }
        }

        public int LevelCap
        {
            get
            {
                return this.mLevelCap;
            }
            set
            {
                this.mLevelCap = value;
                if ((this.LevelMax != null) == null)
                {
                    goto Label_002E;
                }
                this.LevelMax.set_text(&this.mLevelCap.ToString());
            Label_002E:
                return;
            }
        }

        public delegate int CalcEvent(int value);

        public delegate void ExpPanelEvent();

        public delegate void LevelChangeEvent(int levelOld, int levelNew);
    }
}

