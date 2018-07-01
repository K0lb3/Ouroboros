// Decompiled with JetBrains decompiler
// Type: SRPG.ConceptCardDetailLevel
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 85BFDF7F-5712-4D45-9CD6-3465C703DFDF
// Assembly location: S:\Desktop\Assembly-CSharp.dll

using GR;
using System;
using System.Collections;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.UI;

namespace SRPG
{
  public class ConceptCardDetailLevel : ConceptCardDetailBase
  {
    private readonly string ANIM_NAME_LV_TEXT_STYLE_DEFAULT = "default";
    private readonly string ANIM_NAME_LV_TEXT_STYLE_ENHANCE = "enhance";
    [SerializeField]
    private float mMixEffectAnimTime = 1f;
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
    private int mExpStart;
    private int mExpEnd;
    private int mTrustStart;
    private int mTrustEnd;
    private ConceptCardDetailLevel.EffectCallBack mCallback;
    private int mAddExp;
    private int mAddTrust;
    private int mAddAwakeLv;
    private bool mEnhance;

    public override void SetParam(ConceptCardData card_data, int addExp, int addTrust, int addAwakeLv)
    {
      this.mConceptCardData = card_data;
      this.mAddExp = addExp;
      this.mAddTrust = addTrust;
      this.mAddAwakeLv = addAwakeLv;
      this.mEnhance = ConceptCardDescription.IsEnhance;
    }

    public override void Refresh()
    {
      if (this.mConceptCardData == null)
        return;
      this.RefreshParam((int) this.mConceptCardData.Rarity, (int) this.mConceptCardData.Exp, (int) this.mConceptCardData.Trust, (int) this.mConceptCardData.CurrentLvCap, this.mEnhance);
      this.RefreshIcon();
      this.RefreshFrame();
      this.RefreshRewardAmountCount();
    }

    public void RefreshIcon()
    {
      if (UnityEngine.Object.op_Equality((UnityEngine.Object) this.mTrustMasterRewardBase, (UnityEngine.Object) null))
        return;
      ConceptCardTrustRewardItemParam reward = this.mConceptCardData.GetReward();
      if (reward != null)
      {
        bool is_on = reward.reward_type == eRewardType.ConceptCard;
        this.SwitchObject(is_on, ((Component) this.mTrustMasterRewardCardIcon).get_gameObject(), this.mTrustMasterRewardItemIconObject);
        this.mTrustMasterRewardBase.get_gameObject().SetActive(true);
        if (is_on)
        {
          ConceptCardData cardDataForDisplay = ConceptCardData.CreateConceptCardDataForDisplay(reward.iname);
          if (cardDataForDisplay == null)
            return;
          this.mTrustMasterRewardCardIcon.Setup(cardDataForDisplay);
        }
        else
          this.LoadImage(reward.GetIconPath(), this.mTrustMasterRewardIcon);
      }
      else
        this.mTrustMasterRewardBase.get_gameObject().SetActive(false);
    }

    public void RefreshFrame()
    {
      ConceptCardTrustRewardItemParam reward = this.mConceptCardData.GetReward();
      if (reward == null)
        return;
      this.SetSprite(this.mTrustMasuterRewardFrame, reward.GetFrameSprite());
    }

    private void RefreshRewardAmountCount()
    {
      if (UnityEngine.Object.op_Equality((UnityEngine.Object) this.mTrustMasterRewardAmountCountParent, (UnityEngine.Object) null) || UnityEngine.Object.op_Equality((UnityEngine.Object) this.mTrustMasterRewardAmountCount, (UnityEngine.Object) null))
        return;
      this.mTrustMasterRewardAmountCountParent.SetActive(false);
      ConceptCardTrustRewardItemParam reward = this.mConceptCardData.GetReward();
      if (reward == null || reward.reward_num <= 1)
        return;
      this.mTrustMasterRewardAmountCountParent.get_gameObject().SetActive(true);
      this.mTrustMasterRewardAmountCount.set_text(reward.reward_num.ToString());
    }

    public void RefreshParam(int rarity, int baseExp, int baseTrust, int lvCap, bool enhance)
    {
      int lv1;
      int nextExp1;
      int expTbl1;
      ConceptCardUtility.GetExpParameter(rarity, baseExp, (int) this.mConceptCardData.CurrentLvCap, out lv1, out nextExp1, out expTbl1);
      bool flag1 = enhance && this.mAddExp != 0 && nextExp1 != 0;
      bool flag2 = enhance && this.mAddTrust != 0 && baseTrust != (int) MonoSingleton<GameManager>.Instance.MasterParam.FixParam.CardTrustMax;
      bool flag3 = enhance && this.mAddAwakeLv != 0;
      this.SetText(this.mCardLvText, lv1.ToString());
      this.SetText(this.mCardLvCapText, lvCap.ToString());
      this.SetText(this.mCardNextExpText, nextExp1.ToString());
      ConceptCardManager.SubstituteTrustFormat(this.mConceptCardData, this.mCardTrustText, baseTrust, false);
      this.mCardLvSlider.set_value((float) (1.0 - (double) nextExp1 / (double) expTbl1));
      this.mCardPredictLvArrow.SetActive(false);
      this.mCardPredictLvSlash.SetActive(false);
      ((Component) this.mCardPredictLvWhiteText).get_gameObject().SetActive(false);
      ((Component) this.mCardPredictLvGreenText).get_gameObject().SetActive(false);
      ((Component) this.mCardPredictLvCapWhiteText).get_gameObject().SetActive(false);
      ((Component) this.mCardPredictLvCapGreenText).get_gameObject().SetActive(false);
      this.mCardLvAnimator.Play(this.ANIM_NAME_LV_TEXT_STYLE_DEFAULT);
      if (flag1 || flag3)
      {
        int lv2;
        int nextExp2;
        int expTbl2;
        ConceptCardUtility.GetExpParameter(rarity, baseExp + this.mAddExp, lvCap + this.mAddAwakeLv, out lv2, out nextExp2, out expTbl2);
        this.SetText(this.mCardNextPredictExpText, nextExp2.ToString());
        this.mCardPredictLvSlider.set_value(lv1 >= lv2 ? (float) (1.0 - (double) nextExp2 / (double) expTbl2) : 1f);
        this.mCardPredictLvArrow.SetActive(true);
        this.mCardPredictLvSlash.SetActive(true);
        this.mCardLvAnimator.Play(this.ANIM_NAME_LV_TEXT_STYLE_ENHANCE);
        if (lv1 < lv2)
        {
          ((Component) this.mCardPredictLvGreenText).get_gameObject().SetActive(true);
          this.SetText(this.mCardPredictLvGreenText, lv2.ToString());
        }
        else
        {
          ((Component) this.mCardPredictLvWhiteText).get_gameObject().SetActive(true);
          this.SetText(this.mCardPredictLvWhiteText, lv2.ToString());
        }
        if (flag3)
        {
          ((Component) this.mCardPredictLvCapGreenText).get_gameObject().SetActive(true);
          this.SetText(this.mCardPredictLvCapGreenText, (lvCap + this.mAddAwakeLv).ToString());
        }
        else
        {
          ((Component) this.mCardPredictLvCapWhiteText).get_gameObject().SetActive(true);
          this.SetText(this.mCardPredictLvCapWhiteText, (lvCap + this.mAddAwakeLv).ToString());
        }
      }
      if (flag2)
        this.SetText(this.mCardPredictTrustText, ConceptCardManager.ParseTrustFormat(baseTrust + this.mAddTrust));
      this.RefreshAwakeIcons(enhance);
      ((Component) this.mCardNextExpText).get_gameObject().SetActive(!flag1);
      ((Component) this.mCardNextPredictExpText).get_gameObject().SetActive(flag1);
      ((Component) this.mCardPredictLvSlider).get_gameObject().SetActive(flag1);
      ((Component) this.mCardTrustText).get_gameObject().SetActive(!flag2);
      ((Component) this.mCardPredictTrustText).get_gameObject().SetActive(flag2);
    }

    private void GetExpParameter(int rarity, int exp, int lvcap, out int lv, out int nextExp, out int expTbl)
    {
      lv = this.Master.CalcConceptCardLevel(rarity, exp, lvcap);
      int conceptCardLevelExp = this.Master.GetConceptCardLevelExp(rarity, lv);
      if (lv < lvcap)
      {
        expTbl = this.Master.GetConceptCardNextExp(rarity, lv + 1);
        nextExp = expTbl - (exp - conceptCardLevelExp);
      }
      else
      {
        expTbl = 1;
        nextExp = 0;
      }
    }

    private void RefreshAwakeIcons(bool is_enhance)
    {
      this.mAwakeCountIconsParent.SetActive(this.mConceptCardData.IsEnableAwake);
      int num1 = 0;
      if (is_enhance)
        num1 = this.mAddAwakeLv / (int) MonoSingleton<GameManager>.Instance.MasterParam.FixParam.CardAwakeUnlockLevelCap;
      int num2 = 0;
      IEnumerator enumerator1 = this.mAwakeCountIconsParent.get_transform().GetEnumerator();
      try
      {
        while (enumerator1.MoveNext())
        {
          Transform current = (Transform) enumerator1.Current;
          IEnumerator enumerator2 = current.GetEnumerator();
          try
          {
            while (enumerator2.MoveNext())
              ((Component) enumerator2.Current).get_gameObject().SetActive(false);
          }
          finally
          {
            IDisposable disposable = enumerator2 as IDisposable;
            if (disposable != null)
              disposable.Dispose();
          }
          string str = "off";
          if (num2 < (int) this.mConceptCardData.AwakeCount)
            str = "on";
          else if (num2 < (int) this.mConceptCardData.AwakeCount + num1)
            str = "up";
          Transform transform = current.Find(str);
          if (UnityEngine.Object.op_Inequality((UnityEngine.Object) transform, (UnityEngine.Object) null))
            ((Component) transform).get_gameObject().SetActive(true);
          ++num2;
        }
      }
      finally
      {
        IDisposable disposable = enumerator1 as IDisposable;
        if (disposable != null)
          disposable.Dispose();
      }
    }

    public void SetupLevelupAnimation(int addExp, int addTrust)
    {
      this.mExpStart = (int) this.mConceptCardData.Exp;
      this.mExpEnd = this.mExpStart + addExp;
      this.mTrustStart = (int) this.mConceptCardData.Trust;
      this.mTrustEnd = this.mTrustStart + addTrust;
      int cardTrustMax = (int) MonoSingleton<GameManager>.Instance.MasterParam.FixParam.CardTrustMax;
      this.mTrustStart = Mathf.Min(this.mTrustStart, cardTrustMax);
      this.mTrustEnd = Mathf.Min(this.mTrustEnd, cardTrustMax);
    }

    public void StartLevelupAnimation(ConceptCardDetailLevel.EffectCallBack cb)
    {
      this.mCallback = cb;
      this.mAddExp = 0;
      this.mAddTrust = 0;
      this.mEnhance = false;
      bool bLevelUpEffect = this.Master.CalcConceptCardLevel((int) this.mConceptCardData.Rarity, this.mExpStart, (int) this.mConceptCardData.CurrentLvCap) < this.Master.CalcConceptCardLevel((int) this.mConceptCardData.Rarity, this.mExpEnd, (int) this.mConceptCardData.CurrentLvCap);
      bool bTrustUpEffect = this.mTrustStart < this.mTrustEnd;
      this.mExpEnd = Mathf.Min(MonoSingleton<GameManager>.Instance.MasterParam.GetConceptCardLevelExp((int) this.mConceptCardData.Rarity, (int) this.mConceptCardData.CurrentLvCap), this.mExpEnd);
      this.StartCoroutine(this.LevelupUpdate(this.mConceptCardData, bLevelUpEffect, bTrustUpEffect));
    }

    public void StartAwakeAnimation(ConceptCardDetailLevel.EffectCallBack cb)
    {
      this.mCallback = cb;
      this.StartCoroutine(this.AwakeUpdate(this.mAddAwakeLv > 0));
    }

    public void StartTrustMasterAnimation(ConceptCardDetailLevel.EffectCallBack cb)
    {
      this.mCallback = cb;
      this.StartCoroutine(this.TrustMasterUpdate(this.mConceptCardData, this.mConceptCardData.GetReward() != null && this.mTrustStart < this.mTrustEnd && this.mTrustEnd >= (int) MonoSingleton<GameManager>.Instance.MasterParam.FixParam.CardTrustMax));
    }

    public void StartGroupSkillPowerUpAnimation(ConceptCardDetailLevel.EffectCallBack cb)
    {
      this.mCallback = cb;
      int num1 = this.Master.CalcConceptCardLevel((int) this.mConceptCardData.Rarity, this.mExpStart, (int) this.mConceptCardData.CurrentLvCap);
      bool bGroupSkillPowerUp = this.mAddAwakeLv > 0;
      ConceptCardEffectRoutine.EffectAltData altData = new ConceptCardEffectRoutine.EffectAltData();
      int num2 = this.mAddAwakeLv / (int) MonoSingleton<GameManager>.Instance.MasterParam.FixParam.CardAwakeUnlockLevelCap;
      altData.prevAwakeCount = (int) this.mConceptCardData.AwakeCount - num2;
      altData.prevLevel = num1;
      this.StartCoroutine(this.GroupSkillPowerUpUpdate(this.mConceptCardData, bGroupSkillPowerUp, altData));
    }

    public void StartGroupSkillMaxPowerUpAnimation(ConceptCardDetailLevel.EffectCallBack cb)
    {
      this.mCallback = cb;
      int num1 = this.Master.CalcConceptCardLevel((int) this.mConceptCardData.Rarity, this.mExpStart, (int) this.mConceptCardData.CurrentLvCap);
      int num2 = this.Master.CalcConceptCardLevel((int) this.mConceptCardData.Rarity, this.mExpEnd, (int) this.mConceptCardData.CurrentLvCap);
      int num3 = this.mAddAwakeLv / (int) MonoSingleton<GameManager>.Instance.MasterParam.FixParam.CardAwakeUnlockLevelCap;
      bool bGroupSkillMaxPowerUp = (num1 != num2 || num3 > 0) && ((int) this.mConceptCardData.AwakeCount == this.mConceptCardData.AwakeCountCap && (int) this.mConceptCardData.Lv == (int) this.mConceptCardData.LvCap);
      ConceptCardEffectRoutine.EffectAltData altData = new ConceptCardEffectRoutine.EffectAltData();
      if (this.mAddAwakeLv > 0)
      {
        altData.prevAwakeCount = (int) this.mConceptCardData.AwakeCount;
        altData.prevLevel = (int) this.mConceptCardData.Lv;
      }
      else
      {
        altData.prevAwakeCount = (int) this.mConceptCardData.AwakeCount - num3;
        altData.prevLevel = num1;
      }
      this.StartCoroutine(this.GroupSkillMaxPowerUpUpdate(this.mConceptCardData, bGroupSkillMaxPowerUp, altData));
    }

    [DebuggerHidden]
    private IEnumerator LevelupUpdate(ConceptCardData cardData, bool bLevelUpEffect, bool bTrustUpEffect)
    {
      // ISSUE: object of a compiler-generated type is created
      return (IEnumerator) new ConceptCardDetailLevel.\u003CLevelupUpdate\u003Ec__IteratorF6()
      {
        cardData = cardData,
        bTrustUpEffect = bTrustUpEffect,
        bLevelUpEffect = bLevelUpEffect,
        \u003C\u0024\u003EcardData = cardData,
        \u003C\u0024\u003EbTrustUpEffect = bTrustUpEffect,
        \u003C\u0024\u003EbLevelUpEffect = bLevelUpEffect,
        \u003C\u003Ef__this = this
      };
    }

    [DebuggerHidden]
    private IEnumerator AwakeUpdate(bool is_awake)
    {
      // ISSUE: object of a compiler-generated type is created
      return (IEnumerator) new ConceptCardDetailLevel.\u003CAwakeUpdate\u003Ec__IteratorF7()
      {
        is_awake = is_awake,
        \u003C\u0024\u003Eis_awake = is_awake,
        \u003C\u003Ef__this = this
      };
    }

    [DebuggerHidden]
    private IEnumerator TrustMasterUpdate(ConceptCardData cardData, bool bTrustMasterEffect)
    {
      // ISSUE: object of a compiler-generated type is created
      return (IEnumerator) new ConceptCardDetailLevel.\u003CTrustMasterUpdate\u003Ec__IteratorF8()
      {
        bTrustMasterEffect = bTrustMasterEffect,
        cardData = cardData,
        \u003C\u0024\u003EbTrustMasterEffect = bTrustMasterEffect,
        \u003C\u0024\u003EcardData = cardData,
        \u003C\u003Ef__this = this
      };
    }

    [DebuggerHidden]
    private IEnumerator GroupSkillPowerUpUpdate(ConceptCardData cardData, bool bGroupSkillPowerUp, ConceptCardEffectRoutine.EffectAltData altData)
    {
      // ISSUE: object of a compiler-generated type is created
      return (IEnumerator) new ConceptCardDetailLevel.\u003CGroupSkillPowerUpUpdate\u003Ec__IteratorF9()
      {
        bGroupSkillPowerUp = bGroupSkillPowerUp,
        cardData = cardData,
        altData = altData,
        \u003C\u0024\u003EbGroupSkillPowerUp = bGroupSkillPowerUp,
        \u003C\u0024\u003EcardData = cardData,
        \u003C\u0024\u003EaltData = altData,
        \u003C\u003Ef__this = this
      };
    }

    [DebuggerHidden]
    private IEnumerator GroupSkillMaxPowerUpUpdate(ConceptCardData cardData, bool bGroupSkillMaxPowerUp, ConceptCardEffectRoutine.EffectAltData altData)
    {
      // ISSUE: object of a compiler-generated type is created
      return (IEnumerator) new ConceptCardDetailLevel.\u003CGroupSkillMaxPowerUpUpdate\u003Ec__IteratorFA()
      {
        bGroupSkillMaxPowerUp = bGroupSkillMaxPowerUp,
        cardData = cardData,
        altData = altData,
        \u003C\u0024\u003EbGroupSkillMaxPowerUp = bGroupSkillMaxPowerUp,
        \u003C\u0024\u003EcardData = cardData,
        \u003C\u0024\u003EaltData = altData,
        \u003C\u003Ef__this = this
      };
    }

    public delegate void EffectCallBack();
  }
}
