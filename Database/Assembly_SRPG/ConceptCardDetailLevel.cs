namespace SRPG
{
    using GR;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;
    using UnityEngine;
    using UnityEngine.UI;

    public class ConceptCardDetailLevel : ConceptCardDetailBase
    {
        private readonly string ANIM_NAME_LV_TEXT_STYLE_DEFAULT;
        private readonly string ANIM_NAME_LV_TEXT_STYLE_ENHANCE;
        [SerializeField]
        private Animator mCardLvAnimator;
        [SerializeField]
        private Text mCardLvCapText;
        [SerializeField]
        private Text mCardLvText;
        [SerializeField]
        private Text mCardNextExpText;
        [SerializeField]
        private Text mCardTrustText;
        [SerializeField]
        private Text mCardPredictLvWhiteText;
        [SerializeField]
        private Text mCardPredictLvGreenText;
        [SerializeField]
        private Text mCardPredictLvCapWhiteText;
        [SerializeField]
        private Text mCardPredictLvCapGreenText;
        [SerializeField]
        private GameObject mCardPredictLvSlash;
        [SerializeField]
        private Text mCardNextPredictExpText;
        [SerializeField]
        private Text mCardPredictTrustText;
        [SerializeField]
        private GameObject mCardPredictLvArrow;
        [SerializeField]
        private Slider mCardLvSlider;
        [SerializeField]
        private Slider mCardPredictLvSlider;
        [SerializeField]
        private GameObject mTrustMasterRewardBase;
        [SerializeField]
        private RawImage mTrustMasterRewardIcon;
        [SerializeField]
        private Image mTrustMasuterRewardFrame;
        [SerializeField]
        private GameObject mTrustMasterRewardItemIconObject;
        [SerializeField]
        private ConceptCardIcon mTrustMasterRewardCardIcon;
        [SerializeField]
        private GameObject mTrustMasterRewardAmountCountParent;
        [SerializeField]
        private Text mTrustMasterRewardAmountCount;
        [SerializeField]
        private Animator mTrustUpAnimator;
        [SerializeField]
        private GameObject mAwakeCountIconsParent;
        [SerializeField]
        private float mMixEffectAnimTime;
        private int mExpStart;
        private int mExpEnd;
        private int mTrustStart;
        private int mTrustEnd;
        private EffectCallBack mCallback;
        private int mAddExp;
        private int mAddTrust;
        private int mAddAwakeLv;
        private bool mEnhance;

        public ConceptCardDetailLevel()
        {
            this.ANIM_NAME_LV_TEXT_STYLE_DEFAULT = "default";
            this.ANIM_NAME_LV_TEXT_STYLE_ENHANCE = "enhance";
            this.mMixEffectAnimTime = 1f;
            base..ctor();
            return;
        }

        [DebuggerHidden]
        private IEnumerator AwakeUpdate(bool is_awake)
        {
            <AwakeUpdate>c__IteratorF7 rf;
            rf = new <AwakeUpdate>c__IteratorF7();
            rf.is_awake = is_awake;
            rf.<$>is_awake = is_awake;
            rf.<>f__this = this;
            return rf;
        }

        private unsafe void GetExpParameter(int rarity, int exp, int lvcap, out int lv, out int nextExp, out int expTbl)
        {
            int num;
            *((int*) lv) = base.Master.CalcConceptCardLevel(rarity, exp, lvcap);
            num = base.Master.GetConceptCardLevelExp(rarity, *((int*) lv));
            if (*(((int*) lv)) >= lvcap)
            {
                goto Label_004D;
            }
            *((int*) expTbl) = base.Master.GetConceptCardNextExp(rarity, *(((int*) lv)) + 1);
            *((int*) nextExp) = *(((int*) expTbl)) - (exp - num);
            goto Label_0055;
        Label_004D:
            *((int*) expTbl) = 1;
            *((int*) nextExp) = 0;
        Label_0055:
            return;
        }

        [DebuggerHidden]
        private IEnumerator GroupSkillMaxPowerUpUpdate(ConceptCardData cardData, bool bGroupSkillMaxPowerUp, ConceptCardEffectRoutine.EffectAltData altData)
        {
            <GroupSkillMaxPowerUpUpdate>c__IteratorFA rfa;
            rfa = new <GroupSkillMaxPowerUpUpdate>c__IteratorFA();
            rfa.bGroupSkillMaxPowerUp = bGroupSkillMaxPowerUp;
            rfa.cardData = cardData;
            rfa.altData = altData;
            rfa.<$>bGroupSkillMaxPowerUp = bGroupSkillMaxPowerUp;
            rfa.<$>cardData = cardData;
            rfa.<$>altData = altData;
            rfa.<>f__this = this;
            return rfa;
        }

        [DebuggerHidden]
        private IEnumerator GroupSkillPowerUpUpdate(ConceptCardData cardData, bool bGroupSkillPowerUp, ConceptCardEffectRoutine.EffectAltData altData)
        {
            <GroupSkillPowerUpUpdate>c__IteratorF9 rf;
            rf = new <GroupSkillPowerUpUpdate>c__IteratorF9();
            rf.bGroupSkillPowerUp = bGroupSkillPowerUp;
            rf.cardData = cardData;
            rf.altData = altData;
            rf.<$>bGroupSkillPowerUp = bGroupSkillPowerUp;
            rf.<$>cardData = cardData;
            rf.<$>altData = altData;
            rf.<>f__this = this;
            return rf;
        }

        [DebuggerHidden]
        private IEnumerator LevelupUpdate(ConceptCardData cardData, bool bLevelUpEffect, bool bTrustUpEffect)
        {
            <LevelupUpdate>c__IteratorF6 rf;
            rf = new <LevelupUpdate>c__IteratorF6();
            rf.cardData = cardData;
            rf.bTrustUpEffect = bTrustUpEffect;
            rf.bLevelUpEffect = bLevelUpEffect;
            rf.<$>cardData = cardData;
            rf.<$>bTrustUpEffect = bTrustUpEffect;
            rf.<$>bLevelUpEffect = bLevelUpEffect;
            rf.<>f__this = this;
            return rf;
        }

        public override void Refresh()
        {
            if (base.mConceptCardData != null)
            {
                goto Label_000C;
            }
            return;
        Label_000C:
            this.RefreshParam(base.mConceptCardData.Rarity, base.mConceptCardData.Exp, base.mConceptCardData.Trust, base.mConceptCardData.CurrentLvCap, this.mEnhance);
            this.RefreshIcon();
            this.RefreshFrame();
            this.RefreshRewardAmountCount();
            return;
        }

        private void RefreshAwakeIcons(bool is_enhance)
        {
            int num;
            int num2;
            Transform transform;
            IEnumerator enumerator;
            Transform transform2;
            IEnumerator enumerator2;
            string str;
            Transform transform3;
            IDisposable disposable;
            IDisposable disposable2;
            this.mAwakeCountIconsParent.SetActive(base.mConceptCardData.IsEnableAwake);
            num = 0;
            if (is_enhance == null)
            {
                goto Label_003F;
            }
            num = this.mAddAwakeLv / MonoSingleton<GameManager>.Instance.MasterParam.FixParam.CardAwakeUnlockLevelCap;
        Label_003F:
            num2 = 0;
            enumerator = this.mAwakeCountIconsParent.get_transform().GetEnumerator();
        Label_0052:
            try
            {
                goto Label_0122;
            Label_0057:
                transform = (Transform) enumerator.Current;
                enumerator2 = transform.GetEnumerator();
            Label_006B:
                try
                {
                    goto Label_008B;
                Label_0070:
                    transform2 = (Transform) enumerator2.Current;
                    transform2.get_gameObject().SetActive(0);
                Label_008B:
                    if (enumerator2.MoveNext() != null)
                    {
                        goto Label_0070;
                    }
                    goto Label_00B2;
                }
                finally
                {
                Label_009C:
                    disposable = enumerator2 as IDisposable;
                    if (disposable != null)
                    {
                        goto Label_00AA;
                    }
                Label_00AA:
                    disposable.Dispose();
                }
            Label_00B2:
                str = "off";
                if (num2 >= base.mConceptCardData.AwakeCount)
                {
                    goto Label_00DB;
                }
                str = "on";
                goto Label_00FA;
            Label_00DB:
                if (num2 >= (base.mConceptCardData.AwakeCount + num))
                {
                    goto Label_00FA;
                }
                str = "up";
            Label_00FA:
                transform3 = transform.Find(str);
                if ((transform3 != null) == null)
                {
                    goto Label_011E;
                }
                transform3.get_gameObject().SetActive(1);
            Label_011E:
                num2 += 1;
            Label_0122:
                if (enumerator.MoveNext() != null)
                {
                    goto Label_0057;
                }
                goto Label_0147;
            }
            finally
            {
            Label_0132:
                disposable2 = enumerator as IDisposable;
                if (disposable2 != null)
                {
                    goto Label_013F;
                }
            Label_013F:
                disposable2.Dispose();
            }
        Label_0147:
            return;
        }

        public void RefreshFrame()
        {
            ConceptCardTrustRewardItemParam param;
            param = base.mConceptCardData.GetReward();
            if (param == null)
            {
                goto Label_0024;
            }
            base.SetSprite(this.mTrustMasuterRewardFrame, param.GetFrameSprite());
        Label_0024:
            return;
        }

        public void RefreshIcon()
        {
            ConceptCardTrustRewardItemParam param;
            bool flag;
            ConceptCardData data;
            string str;
            if ((this.mTrustMasterRewardBase == null) == null)
            {
                goto Label_0012;
            }
            return;
        Label_0012:
            param = base.mConceptCardData.GetReward();
            if (param == null)
            {
                goto Label_0099;
            }
            flag = param.reward_type == 5;
            base.SwitchObject(flag, this.mTrustMasterRewardCardIcon.get_gameObject(), this.mTrustMasterRewardItemIconObject);
            this.mTrustMasterRewardBase.get_gameObject().SetActive(1);
            if (flag == null)
            {
                goto Label_0080;
            }
            data = ConceptCardData.CreateConceptCardDataForDisplay(param.iname);
            if (data == null)
            {
                goto Label_00AA;
            }
            this.mTrustMasterRewardCardIcon.Setup(data);
            goto Label_0094;
        Label_0080:
            str = param.GetIconPath();
            base.LoadImage(str, this.mTrustMasterRewardIcon);
        Label_0094:
            goto Label_00AA;
        Label_0099:
            this.mTrustMasterRewardBase.get_gameObject().SetActive(0);
        Label_00AA:
            return;
        }

        public unsafe void RefreshParam(int rarity, int baseExp, int baseTrust, int lvCap, bool enhance)
        {
            int num;
            int num2;
            int num3;
            bool flag;
            bool flag2;
            bool flag3;
            int num4;
            int num5;
            int num6;
            float num7;
            int num8;
            int num9;
            int num10;
            ConceptCardUtility.GetExpParameter(rarity, baseExp, base.mConceptCardData.CurrentLvCap, &num, &num2, &num3);
            flag = ((enhance == null) || (this.mAddExp == null)) ? 0 : ((num2 == 0) == 0);
            flag2 = ((enhance == null) || (this.mAddTrust == null)) ? 0 : ((baseTrust == MonoSingleton<GameManager>.Instance.MasterParam.FixParam.CardTrustMax) == 0);
            flag3 = (enhance == null) ? 0 : ((this.mAddAwakeLv == 0) == 0);
            base.SetText(this.mCardLvText, &num.ToString());
            base.SetText(this.mCardLvCapText, &lvCap.ToString());
            base.SetText(this.mCardNextExpText, &num2.ToString());
            ConceptCardManager.SubstituteTrustFormat(base.mConceptCardData, this.mCardTrustText, baseTrust, 0);
            this.mCardLvSlider.set_value(1f - (((float) num2) / ((float) num3)));
            this.mCardPredictLvArrow.SetActive(0);
            this.mCardPredictLvSlash.SetActive(0);
            this.mCardPredictLvWhiteText.get_gameObject().SetActive(0);
            this.mCardPredictLvGreenText.get_gameObject().SetActive(0);
            this.mCardPredictLvCapWhiteText.get_gameObject().SetActive(0);
            this.mCardPredictLvCapGreenText.get_gameObject().SetActive(0);
            this.mCardLvAnimator.Play(this.ANIM_NAME_LV_TEXT_STYLE_DEFAULT);
            if ((flag == null) && (flag3 == null))
            {
                goto Label_02AA;
            }
            ConceptCardUtility.GetExpParameter(rarity, baseExp + this.mAddExp, lvCap + this.mAddAwakeLv, &num4, &num5, &num6);
            base.SetText(this.mCardNextPredictExpText, &num5.ToString());
            num7 = (num >= num4) ? (1f - (((float) num5) / ((float) num6))) : 1f;
            this.mCardPredictLvSlider.set_value(num7);
            this.mCardPredictLvArrow.SetActive(1);
            this.mCardPredictLvSlash.SetActive(1);
            this.mCardLvAnimator.Play(this.ANIM_NAME_LV_TEXT_STYLE_ENHANCE);
            if (num >= num4)
            {
                goto Label_021C;
            }
            this.mCardPredictLvGreenText.get_gameObject().SetActive(1);
            base.SetText(this.mCardPredictLvGreenText, &num4.ToString());
            goto Label_0240;
        Label_021C:
            this.mCardPredictLvWhiteText.get_gameObject().SetActive(1);
            base.SetText(this.mCardPredictLvWhiteText, &num4.ToString());
        Label_0240:
            if (flag3 == null)
            {
                goto Label_027B;
            }
            this.mCardPredictLvCapGreenText.get_gameObject().SetActive(1);
            num9 = lvCap + this.mAddAwakeLv;
            base.SetText(this.mCardPredictLvCapGreenText, &num9.ToString());
            goto Label_02AA;
        Label_027B:
            this.mCardPredictLvCapWhiteText.get_gameObject().SetActive(1);
            num10 = lvCap + this.mAddAwakeLv;
            base.SetText(this.mCardPredictLvCapWhiteText, &num10.ToString());
        Label_02AA:
            if (flag2 == null)
            {
                goto Label_02CE;
            }
            num8 = baseTrust + this.mAddTrust;
            base.SetText(this.mCardPredictTrustText, ConceptCardManager.ParseTrustFormat(num8));
        Label_02CE:
            this.RefreshAwakeIcons(enhance);
            this.mCardNextExpText.get_gameObject().SetActive(flag == 0);
            this.mCardNextPredictExpText.get_gameObject().SetActive(flag);
            this.mCardPredictLvSlider.get_gameObject().SetActive(flag);
            this.mCardTrustText.get_gameObject().SetActive(flag2 == 0);
            this.mCardPredictTrustText.get_gameObject().SetActive(flag2);
            return;
        }

        private unsafe void RefreshRewardAmountCount()
        {
            ConceptCardTrustRewardItemParam param;
            if ((this.mTrustMasterRewardAmountCountParent == null) == null)
            {
                goto Label_0012;
            }
            return;
        Label_0012:
            if ((this.mTrustMasterRewardAmountCount == null) == null)
            {
                goto Label_0024;
            }
            return;
        Label_0024:
            this.mTrustMasterRewardAmountCountParent.SetActive(0);
            param = base.mConceptCardData.GetReward();
            if (param == null)
            {
                goto Label_0075;
            }
            if (param.reward_num <= 1)
            {
                goto Label_0075;
            }
            this.mTrustMasterRewardAmountCountParent.get_gameObject().SetActive(1);
            this.mTrustMasterRewardAmountCount.set_text(&param.reward_num.ToString());
        Label_0075:
            return;
        }

        public override void SetParam(ConceptCardData card_data, int addExp, int addTrust, int addAwakeLv)
        {
            base.mConceptCardData = card_data;
            this.mAddExp = addExp;
            this.mAddTrust = addTrust;
            this.mAddAwakeLv = addAwakeLv;
            this.mEnhance = ConceptCardDescription.IsEnhance;
            return;
        }

        public void SetupLevelupAnimation(int addExp, int addTrust)
        {
            int num;
            this.mExpStart = base.mConceptCardData.Exp;
            this.mExpEnd = this.mExpStart + addExp;
            this.mTrustStart = base.mConceptCardData.Trust;
            this.mTrustEnd = this.mTrustStart + addTrust;
            num = MonoSingleton<GameManager>.Instance.MasterParam.FixParam.CardTrustMax;
            this.mTrustStart = Mathf.Min(this.mTrustStart, num);
            this.mTrustEnd = Mathf.Min(this.mTrustEnd, num);
            return;
        }

        public void StartAwakeAnimation(EffectCallBack cb)
        {
            bool flag;
            this.mCallback = cb;
            flag = this.mAddAwakeLv > 0;
            base.StartCoroutine(this.AwakeUpdate(flag));
            return;
        }

        public void StartGroupSkillMaxPowerUpAnimation(EffectCallBack cb)
        {
            int num;
            int num2;
            int num3;
            bool flag;
            ConceptCardEffectRoutine.EffectAltData data;
            this.mCallback = cb;
            num = base.Master.CalcConceptCardLevel(base.mConceptCardData.Rarity, this.mExpStart, base.mConceptCardData.CurrentLvCap);
            num2 = base.Master.CalcConceptCardLevel(base.mConceptCardData.Rarity, this.mExpEnd, base.mConceptCardData.CurrentLvCap);
            num3 = this.mAddAwakeLv / MonoSingleton<GameManager>.Instance.MasterParam.FixParam.CardAwakeUnlockLevelCap;
            flag = ((num == num2) && (num3 <= 0)) ? 0 : ((base.mConceptCardData.AwakeCount != base.mConceptCardData.AwakeCountCap) ? 0 : (base.mConceptCardData.Lv == base.mConceptCardData.LvCap));
            data = new ConceptCardEffectRoutine.EffectAltData();
            if (this.mAddAwakeLv <= 0)
            {
                goto Label_0129;
            }
            data.prevAwakeCount = base.mConceptCardData.AwakeCount;
            data.prevLevel = base.mConceptCardData.Lv;
            goto Label_014A;
        Label_0129:
            data.prevAwakeCount = base.mConceptCardData.AwakeCount - num3;
            data.prevLevel = num;
        Label_014A:
            base.StartCoroutine(this.GroupSkillMaxPowerUpUpdate(base.mConceptCardData, flag, data));
            return;
        }

        public void StartGroupSkillPowerUpAnimation(EffectCallBack cb)
        {
            int num;
            bool flag;
            ConceptCardEffectRoutine.EffectAltData data;
            int num2;
            this.mCallback = cb;
            num = base.Master.CalcConceptCardLevel(base.mConceptCardData.Rarity, this.mExpStart, base.mConceptCardData.CurrentLvCap);
            flag = this.mAddAwakeLv > 0;
            data = new ConceptCardEffectRoutine.EffectAltData();
            num2 = this.mAddAwakeLv / MonoSingleton<GameManager>.Instance.MasterParam.FixParam.CardAwakeUnlockLevelCap;
            data.prevAwakeCount = base.mConceptCardData.AwakeCount - num2;
            data.prevLevel = num;
            base.StartCoroutine(this.GroupSkillPowerUpUpdate(base.mConceptCardData, flag, data));
            return;
        }

        public void StartLevelupAnimation(EffectCallBack cb)
        {
            int num;
            int num2;
            bool flag;
            bool flag2;
            int num3;
            this.mCallback = cb;
            this.mAddExp = 0;
            this.mAddTrust = 0;
            this.mEnhance = 0;
            num = base.Master.CalcConceptCardLevel(base.mConceptCardData.Rarity, this.mExpStart, base.mConceptCardData.CurrentLvCap);
            num2 = base.Master.CalcConceptCardLevel(base.mConceptCardData.Rarity, this.mExpEnd, base.mConceptCardData.CurrentLvCap);
            flag = num < num2;
            flag2 = this.mTrustStart < this.mTrustEnd;
            num3 = MonoSingleton<GameManager>.Instance.MasterParam.GetConceptCardLevelExp(base.mConceptCardData.Rarity, base.mConceptCardData.CurrentLvCap);
            this.mExpEnd = Mathf.Min(num3, this.mExpEnd);
            base.StartCoroutine(this.LevelupUpdate(base.mConceptCardData, flag, flag2));
            return;
        }

        public void StartTrustMasterAnimation(EffectCallBack cb)
        {
            bool flag;
            this.mCallback = cb;
            flag = ((base.mConceptCardData.GetReward() == null) || (this.mTrustStart >= this.mTrustEnd)) ? 0 : ((this.mTrustEnd < MonoSingleton<GameManager>.Instance.MasterParam.FixParam.CardTrustMax) == 0);
            base.StartCoroutine(this.TrustMasterUpdate(base.mConceptCardData, flag));
            return;
        }

        [DebuggerHidden]
        private IEnumerator TrustMasterUpdate(ConceptCardData cardData, bool bTrustMasterEffect)
        {
            <TrustMasterUpdate>c__IteratorF8 rf;
            rf = new <TrustMasterUpdate>c__IteratorF8();
            rf.bTrustMasterEffect = bTrustMasterEffect;
            rf.cardData = cardData;
            rf.<$>bTrustMasterEffect = bTrustMasterEffect;
            rf.<$>cardData = cardData;
            rf.<>f__this = this;
            return rf;
        }

        [CompilerGenerated]
        private sealed class <AwakeUpdate>c__IteratorF7 : IEnumerator, IDisposable, IEnumerator<object>
        {
            internal bool is_awake;
            internal ConceptCardEffectRoutine <routine>__0;
            internal Canvas <overlayCanvas>__1;
            internal int $PC;
            internal object $current;
            internal bool <$>is_awake;
            internal ConceptCardDetailLevel <>f__this;

            public <AwakeUpdate>c__IteratorF7()
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
                        goto Label_0067;
                }
                goto Label_008E;
            Label_0021:
                if (this.is_awake == null)
                {
                    goto Label_0077;
                }
                this.<routine>__0 = new ConceptCardEffectRoutine();
                this.<overlayCanvas>__1 = UIUtility.PushCanvas(0, -1);
                this.$current = this.<routine>__0.PlayAwake(this.<overlayCanvas>__1);
                this.$PC = 1;
                goto Label_0090;
            Label_0067:
                Object.Destroy(this.<overlayCanvas>__1.get_gameObject());
            Label_0077:
                this.<>f__this.mCallback();
                this.$PC = -1;
            Label_008E:
                return 0;
            Label_0090:
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

        [CompilerGenerated]
        private sealed class <GroupSkillMaxPowerUpUpdate>c__IteratorFA : IEnumerator, IDisposable, IEnumerator<object>
        {
            internal bool bGroupSkillMaxPowerUp;
            internal ConceptCardEffectRoutine <routine>__0;
            internal Canvas <overlayCanvas>__1;
            internal ConceptCardData cardData;
            internal ConceptCardEffectRoutine.EffectAltData altData;
            internal int $PC;
            internal object $current;
            internal bool <$>bGroupSkillMaxPowerUp;
            internal ConceptCardData <$>cardData;
            internal ConceptCardEffectRoutine.EffectAltData <$>altData;
            internal ConceptCardDetailLevel <>f__this;

            public <GroupSkillMaxPowerUpUpdate>c__IteratorFA()
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
                        goto Label_0073;
                }
                goto Label_009A;
            Label_0021:
                if (this.bGroupSkillMaxPowerUp == null)
                {
                    goto Label_0083;
                }
                this.<routine>__0 = new ConceptCardEffectRoutine();
                this.<overlayCanvas>__1 = UIUtility.PushCanvas(0, -1);
                this.$current = this.<routine>__0.PlayGroupSkilMaxPowerUp(this.<overlayCanvas>__1, this.cardData, this.altData);
                this.$PC = 1;
                goto Label_009C;
            Label_0073:
                Object.Destroy(this.<overlayCanvas>__1.get_gameObject());
            Label_0083:
                this.<>f__this.mCallback();
                this.$PC = -1;
            Label_009A:
                return 0;
            Label_009C:
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

        [CompilerGenerated]
        private sealed class <GroupSkillPowerUpUpdate>c__IteratorF9 : IEnumerator, IDisposable, IEnumerator<object>
        {
            internal bool bGroupSkillPowerUp;
            internal ConceptCardEffectRoutine <routine>__0;
            internal Canvas <overlayCanvas>__1;
            internal ConceptCardData cardData;
            internal ConceptCardEffectRoutine.EffectAltData altData;
            internal int $PC;
            internal object $current;
            internal bool <$>bGroupSkillPowerUp;
            internal ConceptCardData <$>cardData;
            internal ConceptCardEffectRoutine.EffectAltData <$>altData;
            internal ConceptCardDetailLevel <>f__this;

            public <GroupSkillPowerUpUpdate>c__IteratorF9()
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
                        goto Label_0073;
                }
                goto Label_009A;
            Label_0021:
                if (this.bGroupSkillPowerUp == null)
                {
                    goto Label_0083;
                }
                this.<routine>__0 = new ConceptCardEffectRoutine();
                this.<overlayCanvas>__1 = UIUtility.PushCanvas(0, -1);
                this.$current = this.<routine>__0.PlayGroupSkilPowerUp(this.<overlayCanvas>__1, this.cardData, this.altData);
                this.$PC = 1;
                goto Label_009C;
            Label_0073:
                Object.Destroy(this.<overlayCanvas>__1.get_gameObject());
            Label_0083:
                this.<>f__this.mCallback();
                this.$PC = -1;
            Label_009A:
                return 0;
            Label_009C:
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

        [CompilerGenerated]
        private sealed class <LevelupUpdate>c__IteratorF6 : IEnumerator, IDisposable, IEnumerator<object>
        {
            internal float <ExpAnimSpan>__0;
            internal float <mExpAnimTime>__1;
            internal int <mExpCurr>__2;
            internal int <mTrustCurr>__3;
            internal ConceptCardData cardData;
            internal bool bTrustUpEffect;
            internal bool bLevelUpEffect;
            internal ConceptCardEffectRoutine <routine>__4;
            internal Canvas <overlayCanvas>__5;
            internal int $PC;
            internal object $current;
            internal ConceptCardData <$>cardData;
            internal bool <$>bTrustUpEffect;
            internal bool <$>bLevelUpEffect;
            internal ConceptCardDetailLevel <>f__this;

            public <LevelupUpdate>c__IteratorF6()
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
                        goto Label_0025;

                    case 1:
                        goto Label_0194;

                    case 2:
                        goto Label_0243;
                }
                goto Label_026A;
            Label_0025:
                this.<ExpAnimSpan>__0 = this.<>f__this.mMixEffectAnimTime;
                this.<mExpAnimTime>__1 = 0f;
                this.<mExpCurr>__2 = this.<>f__this.mExpStart;
                this.<mTrustCurr>__3 = this.<>f__this.mTrustStart;
                FlowNode_TriggerLocalEvent.TriggerLocalEvent(this.<>f__this, "PLAY_SE_GETEXP");
                goto Label_0194;
            Label_0078:
                this.<mExpAnimTime>__1 += Time.get_deltaTime();
                this.<mExpCurr>__2 = (int) Mathf.Lerp((float) this.<>f__this.mExpStart, (float) this.<>f__this.mExpEnd, Mathf.Clamp01(this.<mExpAnimTime>__1 / this.<ExpAnimSpan>__0));
                if (this.<mExpCurr>__2 <= this.<>f__this.mExpEnd)
                {
                    goto Label_00E7;
                }
                this.<mExpCurr>__2 = this.<>f__this.mExpEnd;
            Label_00E7:
                this.<mTrustCurr>__3 = (int) Mathf.Lerp((float) this.<>f__this.mTrustStart, (float) this.<>f__this.mTrustEnd, Mathf.Clamp01(this.<mExpAnimTime>__1 / this.<ExpAnimSpan>__0));
                if (this.<mTrustCurr>__3 <= this.<>f__this.mTrustEnd)
                {
                    goto Label_0144;
                }
                this.<mTrustCurr>__3 = this.<>f__this.mTrustEnd;
            Label_0144:
                this.<>f__this.RefreshParam(this.cardData.Rarity, this.<mExpCurr>__2, this.<mTrustCurr>__3, this.<>f__this.mConceptCardData.CurrentLvCap, 0);
                this.$current = null;
                this.$PC = 1;
                goto Label_026C;
            Label_0194:
                if (this.<mExpAnimTime>__1 < this.<ExpAnimSpan>__0)
                {
                    goto Label_0078;
                }
                this.<mExpCurr>__2 = this.<>f__this.mExpEnd;
                this.<mTrustCurr>__3 = this.<>f__this.mTrustEnd;
                if (this.bTrustUpEffect == null)
                {
                    goto Label_01FD;
                }
                if ((this.<>f__this.mTrustUpAnimator != null) == null)
                {
                    goto Label_01FD;
                }
                this.<>f__this.mTrustUpAnimator.Play("up");
            Label_01FD:
                if (this.bLevelUpEffect == null)
                {
                    goto Label_0253;
                }
                this.<routine>__4 = new ConceptCardEffectRoutine();
                this.<overlayCanvas>__5 = UIUtility.PushCanvas(0, -1);
                this.$current = this.<routine>__4.PlayLevelup(this.<overlayCanvas>__5);
                this.$PC = 2;
                goto Label_026C;
            Label_0243:
                Object.Destroy(this.<overlayCanvas>__5.get_gameObject());
            Label_0253:
                this.<>f__this.mCallback();
                this.$PC = -1;
            Label_026A:
                return 0;
            Label_026C:
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

        [CompilerGenerated]
        private sealed class <TrustMasterUpdate>c__IteratorF8 : IEnumerator, IDisposable, IEnumerator<object>
        {
            internal bool bTrustMasterEffect;
            internal ConceptCardEffectRoutine <routine>__0;
            internal Canvas <overlayCanvas>__1;
            internal ConceptCardData cardData;
            internal int $PC;
            internal object $current;
            internal bool <$>bTrustMasterEffect;
            internal ConceptCardData <$>cardData;
            internal ConceptCardDetailLevel <>f__this;

            public <TrustMasterUpdate>c__IteratorF8()
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
                        goto Label_0025;

                    case 1:
                        goto Label_0071;

                    case 2:
                        goto Label_009A;
                }
                goto Label_00C1;
            Label_0025:
                if (this.bTrustMasterEffect == null)
                {
                    goto Label_00AA;
                }
                this.<routine>__0 = new ConceptCardEffectRoutine();
                this.<overlayCanvas>__1 = UIUtility.PushCanvas(0, -1);
                this.$current = this.<routine>__0.PlayTrustMaster(this.<overlayCanvas>__1, this.cardData);
                this.$PC = 1;
                goto Label_00C3;
            Label_0071:
                this.$current = this.<routine>__0.PlayTrustMasterReward(this.<overlayCanvas>__1, this.cardData);
                this.$PC = 2;
                goto Label_00C3;
            Label_009A:
                Object.Destroy(this.<overlayCanvas>__1.get_gameObject());
            Label_00AA:
                this.<>f__this.mCallback();
                this.$PC = -1;
            Label_00C1:
                return 0;
            Label_00C3:
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

        public delegate void EffectCallBack();
    }
}

