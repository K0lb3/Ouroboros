// Decompiled with JetBrains decompiler
// Type: SRPG.ConceptCardLevelUpWindow
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 85BFDF7F-5712-4D45-9CD6-3465C703DFDF
// Assembly location: S:\Desktop\Assembly-CSharp.dll

using GR;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace SRPG
{
  [FlowNode.Pin(11, "強化素材画面のタブを押した", FlowNode.PinTypes.Output, 11)]
  [FlowNode.Pin(1, "強化素材画面のタブを押した", FlowNode.PinTypes.Input, 1)]
  [FlowNode.Pin(0, "一括強化ボタンを押した", FlowNode.PinTypes.Input, 0)]
  [FlowNode.Pin(12, "錬金素材画面のタブを押した", FlowNode.PinTypes.Output, 12)]
  [FlowNode.Pin(2, "錬金素材画面のタブを押した", FlowNode.PinTypes.Input, 2)]
  [FlowNode.Pin(10, "一括強化のための選択素材の設定完了", FlowNode.PinTypes.Output, 10)]
  [FlowNode.Pin(4, "MAXボタンを押した", FlowNode.PinTypes.Input, 4)]
  [FlowNode.Pin(3, "選択クリアボタンを押した", FlowNode.PinTypes.Input, 3)]
  public class ConceptCardLevelUpWindow : ConceptCardDetailBase, IFlowInterface
  {
    private Dictionary<string, int> mSelectExpMaterials = new Dictionary<string, int>();
    private List<ConceptCardMaterialData> mCacheMaxCardExpMaterialList = new List<ConceptCardMaterialData>();
    private List<ConceptCardLevelUpListItem> mCCExpListItem = new List<ConceptCardLevelUpListItem>();
    private Dictionary<string, int> mSelectTrustMaterials = new Dictionary<string, int>();
    private List<ConceptCardMaterialData> mCacheMaxCardTrustMaterialList = new List<ConceptCardMaterialData>();
    private List<ConceptCardLevelUpListItem> mCCTrustListItem = new List<ConceptCardLevelUpListItem>();
    public const int PIN_INPUT_PUSH_ENHANCE_BUTTON = 0;
    public const int PIN_INPUT_PUSH_ENHANCE_TOGGLE_BUTTON = 1;
    public const int PIN_INPUT_PUSH_TRUST_TOGGLE_BUTTON = 2;
    public const int PIN_INPUT_PUSH_CLEAR_BUTTON = 3;
    public const int PIN_INPUT_PUSH_MAX_BUTTON = 4;
    public const int PIN_OUTPUT_PUSH_ENHANCE_BUTTON = 10;
    public const int PIN_OUTPUT_PUSH_ENHANCE_TAB = 11;
    public const int PIN_OUTPUT_PUSH_TRUST_TAB = 12;
    [SerializeField]
    private GameObject SelectedCardIcon;
    [SerializeField]
    private RectTransform ListParent;
    [SerializeField]
    private GameObject ListItemTemplate;
    [SerializeField]
    private Text CurrentLevel;
    [SerializeField]
    private Text FinishedLevel;
    [SerializeField]
    private Text MaxLevel;
    [SerializeField]
    private Text NextExp;
    [SerializeField]
    private Slider CardLvSlider;
    [SerializeField]
    private Text GetAllExp;
    [SerializeField]
    private Button DecideBtn;
    [SerializeField]
    private Button MaxBtn;
    [SerializeField]
    private SliderAnimator AddLevelGauge;
    [SerializeField]
    private GameObject MainLevelup;
    [SerializeField]
    private GameObject MainTrust;
    [SerializeField]
    private Toggle TabEnhanceToggle;
    [SerializeField]
    private Toggle TabTrustToggle;
    [SerializeField]
    private RawImage TrustMasterRewardIcon;
    [SerializeField]
    private Image TrustMasterRewardFrame;
    [SerializeField]
    private GameObject TrustMasterRewardItemIconObject;
    [SerializeField]
    private ConceptCardIcon TrustMasterRewardCardIcon;
    [SerializeField]
    private Text TrustValueTxt;
    [SerializeField]
    private Text TrustPredictValueTxt;
    private int mLv;
    private int mExp;
    private int mTrust;
    private ConceptCardLevelUpWindow.TabState mTabState;

    private List<ConceptCardMaterialData> ConceptCardExpMaterials
    {
      get
      {
        return MonoSingleton<GameManager>.Instance.Player.ConceptCardExpMaterials;
      }
    }

    private List<ConceptCardMaterialData> ConceptCardTrustMaterials
    {
      get
      {
        return MonoSingleton<GameManager>.Instance.Player.ConceptCardTrustMaterials;
      }
    }

    private void Start()
    {
      ConceptCardManager instance1 = ConceptCardManager.Instance;
      if (UnityEngine.Object.op_Equality((UnityEngine.Object) instance1, (UnityEngine.Object) null))
        return;
      this.mConceptCardData = instance1.SelectedConceptCardData;
      if (this.mConceptCardData == null)
        return;
      this.mExp = (int) this.mConceptCardData.Exp;
      this.mLv = (int) this.mConceptCardData.Lv;
      this.mTrust = (int) this.mConceptCardData.Trust;
      if (UnityEngine.Object.op_Equality((UnityEngine.Object) this.SelectedCardIcon, (UnityEngine.Object) null))
        return;
      ConceptCardIcon component = (ConceptCardIcon) this.SelectedCardIcon.GetComponent<ConceptCardIcon>();
      if (UnityEngine.Object.op_Equality((UnityEngine.Object) component, (UnityEngine.Object) null) || UnityEngine.Object.op_Equality((UnityEngine.Object) this.ListItemTemplate, (UnityEngine.Object) null) || UnityEngine.Object.op_Equality((UnityEngine.Object) this.ListItemTemplate.GetComponent<ConceptCardLevelUpListItem>(), (UnityEngine.Object) null) || (this.ConceptCardExpMaterials == null || this.ConceptCardExpMaterials.Count == 0) && (this.ConceptCardTrustMaterials == null || this.ConceptCardTrustMaterials.Count == 0))
        return;
      component.Setup(this.mConceptCardData);
      this.InitSelectedCardData();
      this.InitListItem();
      this.InitSetTab();
      if (this.mTabState == ConceptCardLevelUpWindow.TabState.Enhance)
        this.SetTabEnhance();
      else
        this.SetTabTrust();
      this.InitTrust();
      this.InitWindowButton();
      ConceptCardManager instance2 = ConceptCardManager.Instance;
      if (UnityEngine.Object.op_Equality((UnityEngine.Object) instance2, (UnityEngine.Object) null))
        return;
      instance2.BulkSelectedMaterialList.Clear();
    }

    private void InitSelectedCardData()
    {
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.CurrentLevel, (UnityEngine.Object) null))
        this.CurrentLevel.set_text(this.mConceptCardData.Lv.ToString());
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.FinishedLevel, (UnityEngine.Object) null) && UnityEngine.Object.op_Inequality((UnityEngine.Object) this.CurrentLevel, (UnityEngine.Object) null))
        this.FinishedLevel.set_text(this.CurrentLevel.get_text());
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.MaxLevel, (UnityEngine.Object) null))
        this.MaxLevel.set_text("/" + this.mConceptCardData.CurrentLvCap.ToString());
      int lv;
      int nextExp;
      int expTbl;
      ConceptCardUtility.GetExpParameter((int) this.mConceptCardData.Rarity, (int) this.mConceptCardData.Exp, (int) this.mConceptCardData.CurrentLvCap, out lv, out nextExp, out expTbl);
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.NextExp, (UnityEngine.Object) null))
        this.NextExp.set_text(nextExp.ToString());
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.CardLvSlider, (UnityEngine.Object) null))
        this.CardLvSlider.set_value((float) (1.0 - (double) nextExp / (double) expTbl));
      if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) this.GetAllExp, (UnityEngine.Object) null))
        return;
      this.GetAllExp.set_text("0");
    }

    private void InitListItem()
    {
      List<string> stringList = new List<string>((IEnumerable<string>) PlayerPrefsUtility.GetString(PlayerPrefsUtility.CONCEPT_CARD_LEVELUP_EXPITEM_CHECKS, string.Empty).Split('|'));
      this.ListItemTemplate.SetActive(false);
      List<ConceptCardMaterialData> cardMaterialDataList = new List<ConceptCardMaterialData>();
      cardMaterialDataList.AddRange((IEnumerable<ConceptCardMaterialData>) this.ConceptCardExpMaterials);
      cardMaterialDataList.AddRange((IEnumerable<ConceptCardMaterialData>) this.ConceptCardTrustMaterials);
      using (List<ConceptCardMaterialData>.Enumerator enumerator = cardMaterialDataList.GetEnumerator())
      {
        while (enumerator.MoveNext())
        {
          ConceptCardMaterialData current = enumerator.Current;
          if ((int) current.Num != 0)
          {
            GameObject gameObject = (GameObject) UnityEngine.Object.Instantiate<GameObject>((M0) this.ListItemTemplate);
            gameObject.SetActive(true);
            gameObject.get_transform().SetParent((Transform) this.ListParent, false);
            ConceptCardLevelUpListItem component = (ConceptCardLevelUpListItem) gameObject.GetComponent<ConceptCardLevelUpListItem>();
            component.OnSelect = new ConceptCardLevelUpListItem.SelectExpItem(this.RefreshParamSelectItems);
            component.ChangeUseMax = new ConceptCardLevelUpListItem.ChangeToggleEvent(this.RefreshUseMaxItems);
            component.OnCheck = new ConceptCardLevelUpListItem.CheckSliderValue(this.OnCheck);
            if (stringList != null && stringList.Count > 0)
              component.SetUseMax(stringList.IndexOf(current.Param.iname) != -1);
            component.AddConceptCardData(current);
            if (current.Param.type == eCardType.Enhance_exp)
            {
              this.mCCExpListItem.Add(component);
            }
            else
            {
              component.SetExpObject(false);
              this.mCCTrustListItem.Add(component);
            }
            if (component.IsUseMax())
            {
              if (current.Param.type == eCardType.Enhance_exp)
                this.mCacheMaxCardExpMaterialList.Add(current);
              else
                this.mCacheMaxCardTrustMaterialList.Add(current);
            }
          }
        }
      }
      if (this.mCacheMaxCardExpMaterialList != null && this.mCacheMaxCardExpMaterialList.Count > 0)
        this.mCacheMaxCardExpMaterialList.Sort((Comparison<ConceptCardMaterialData>) ((a, b) => b.Param.en_exp - a.Param.en_exp));
      if (this.mCacheMaxCardTrustMaterialList == null || this.mCacheMaxCardTrustMaterialList.Count <= 0)
        return;
      this.mCacheMaxCardTrustMaterialList.Sort((Comparison<ConceptCardMaterialData>) ((a, b) => b.Param.en_exp - a.Param.en_exp));
    }

    private void InitSetTab()
    {
      if ((int) this.mConceptCardData.Lv == (int) this.mConceptCardData.CurrentLvCap || !MonoSingleton<GameManager>.Instance.Player.IsHaveConceptCardExpMaterial())
        ((Selectable) this.TabEnhanceToggle).set_interactable(false);
      if (this.mConceptCardData.GetReward() == null || this.mConceptCardData.GetTrustToLevelMax() == 0 || !MonoSingleton<GameManager>.Instance.Player.IsHaveConceptCardTrustMaterial())
        ((Selectable) this.TabTrustToggle).set_interactable(false);
      if (!((Selectable) this.TabEnhanceToggle).get_interactable())
        this.mTabState = ConceptCardLevelUpWindow.TabState.Trust;
      else if (!((Selectable) this.TabTrustToggle).get_interactable())
        this.mTabState = ConceptCardLevelUpWindow.TabState.Enhance;
      else
        this.mTabState = ConceptCardLevelUpWindow.LoadTabState();
    }

    private void InitTrust()
    {
      ConceptCardTrustRewardItemParam reward = this.mConceptCardData.GetReward();
      if (reward == null)
        return;
      bool is_on = reward.reward_type == eRewardType.ConceptCard;
      this.SwitchObject(is_on, ((Component) this.TrustMasterRewardCardIcon).get_gameObject(), this.TrustMasterRewardItemIconObject);
      if (is_on)
      {
        ConceptCardData cardDataForDisplay = ConceptCardData.CreateConceptCardDataForDisplay(reward.iname);
        if (cardDataForDisplay != null)
          this.TrustMasterRewardCardIcon.Setup(cardDataForDisplay);
      }
      else
        this.LoadImage(reward.GetIconPath(), this.TrustMasterRewardIcon);
      this.SetSprite(this.TrustMasterRewardFrame, reward.GetFrameSprite());
      ConceptCardManager.SubstituteTrustFormat(this.mConceptCardData, this.TrustValueTxt, (int) this.mConceptCardData.Trust, false);
      ConceptCardManager.SubstituteTrustFormat(this.mConceptCardData, this.TrustPredictValueTxt, (int) this.mConceptCardData.Trust, false);
    }

    private void InitWindowButton()
    {
      int num = 0;
      if (this.mTabState == ConceptCardLevelUpWindow.TabState.Enhance)
      {
        ((Selectable) this.MaxBtn).set_interactable(this.mCacheMaxCardExpMaterialList != null && this.mCacheMaxCardExpMaterialList.Count > 0);
        ((Selectable) this.DecideBtn).set_interactable(false);
        if (this.mSelectExpMaterials == null)
          return;
        using (Dictionary<string, int>.Enumerator enumerator = this.mSelectExpMaterials.GetEnumerator())
        {
          while (enumerator.MoveNext())
          {
            KeyValuePair<string, int> current = enumerator.Current;
            num += current.Value;
          }
        }
        if (num <= 0)
          return;
        ((Selectable) this.DecideBtn).set_interactable(true);
      }
      else
      {
        if (this.mTabState != ConceptCardLevelUpWindow.TabState.Trust)
          return;
        ((Selectable) this.MaxBtn).set_interactable(this.mCacheMaxCardTrustMaterialList != null && this.mCacheMaxCardTrustMaterialList.Count > 0);
        ((Selectable) this.DecideBtn).set_interactable(false);
        if (this.mSelectTrustMaterials == null)
          return;
        using (Dictionary<string, int>.Enumerator enumerator = this.mSelectTrustMaterials.GetEnumerator())
        {
          while (enumerator.MoveNext())
          {
            KeyValuePair<string, int> current = enumerator.Current;
            num += current.Value;
          }
        }
        if (num <= 0)
          return;
        ((Selectable) this.DecideBtn).set_interactable(true);
      }
    }

    public void Activated(int pinID)
    {
      switch (pinID)
      {
        case 0:
          this.SetSelectMaterials();
          FlowNode_GameObject.ActivateOutputLinks((Component) this, 10);
          break;
        case 1:
          this.SetTabEnhance();
          FlowNode_GameObject.ActivateOutputLinks((Component) this, 11);
          break;
        case 2:
          this.SetTabTrust();
          FlowNode_GameObject.ActivateOutputLinks((Component) this, 12);
          break;
        case 3:
          this.OnClear();
          break;
        case 4:
          this.OnMax();
          break;
      }
    }

    private void OnClear()
    {
      Dictionary<string, int> dictionary;
      List<ConceptCardLevelUpListItem> cardLevelUpListItemList;
      if (this.mTabState == ConceptCardLevelUpWindow.TabState.Enhance)
      {
        dictionary = this.mSelectExpMaterials;
        cardLevelUpListItemList = this.mCCExpListItem;
      }
      else
      {
        if (this.mTabState != ConceptCardLevelUpWindow.TabState.Trust)
          return;
        dictionary = this.mSelectTrustMaterials;
        cardLevelUpListItemList = this.mCCTrustListItem;
      }
      if (dictionary.Count <= 0)
        return;
      for (int index = 0; index < cardLevelUpListItemList.Count; ++index)
      {
        ConceptCardLevelUpListItem component = (ConceptCardLevelUpListItem) ((Component) cardLevelUpListItemList[index]).GetComponent<ConceptCardLevelUpListItem>();
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) component, (UnityEngine.Object) null))
          component.Reset();
      }
      dictionary.Clear();
      if (this.mTabState == ConceptCardLevelUpWindow.TabState.Enhance)
        this.RefreshFinishedExpStatus();
      else
        this.RefreshFinishedTrustStatus();
    }

    private void OnMax()
    {
      List<ConceptCardMaterialData> cardMaterialDataList;
      List<ConceptCardLevelUpListItem> cardLevelUpListItemList;
      if (this.mTabState == ConceptCardLevelUpWindow.TabState.Enhance)
      {
        cardMaterialDataList = this.mCacheMaxCardExpMaterialList;
        cardLevelUpListItemList = this.mCCExpListItem;
      }
      else
      {
        if (this.mTabState != ConceptCardLevelUpWindow.TabState.Trust)
          return;
        cardMaterialDataList = this.mCacheMaxCardTrustMaterialList;
        cardLevelUpListItemList = this.mCCTrustListItem;
      }
      if (cardMaterialDataList == null || cardMaterialDataList.Count < 0)
        return;
      for (int index = 0; index < cardLevelUpListItemList.Count; ++index)
      {
        ConceptCardLevelUpListItem component = (ConceptCardLevelUpListItem) ((Component) cardLevelUpListItemList[index]).GetComponent<ConceptCardLevelUpListItem>();
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) component, (UnityEngine.Object) null))
          component.Reset();
      }
      if (this.mTabState == ConceptCardLevelUpWindow.TabState.Enhance)
        this.CalcCanCardLevelUpMax();
      else
        this.CalcCanCardTrustUpMax();
    }

    private int OnCheck(string iname, int num)
    {
      if (string.IsNullOrEmpty(iname) || num == 0)
        return -1;
      Dictionary<string, int> dictionary;
      if (this.mTabState == ConceptCardLevelUpWindow.TabState.Enhance)
      {
        dictionary = this.mSelectExpMaterials;
      }
      else
      {
        if (this.mTabState != ConceptCardLevelUpWindow.TabState.Trust)
          return -1;
        dictionary = this.mSelectTrustMaterials;
      }
      if (dictionary.ContainsKey(iname) && dictionary[iname] > num)
        return -1;
      ConceptCardParam conceptCardParam1 = this.Master.GetConceptCardParam(iname);
      if (conceptCardParam1 == null || MonoSingleton<GameManager>.Instance.Player.GetConceptCardMaterialNum(iname) == 0)
        return -1;
      if (this.mTabState == ConceptCardLevelUpWindow.TabState.Enhance)
      {
        long expToLevelMax = (long) this.mConceptCardData.GetExpToLevelMax();
        long num1 = 0;
        using (Dictionary<string, int>.KeyCollection.Enumerator enumerator = dictionary.Keys.GetEnumerator())
        {
          while (enumerator.MoveNext())
          {
            string current = enumerator.Current;
            if (!(current == iname))
            {
              ConceptCardParam conceptCardParam2 = this.Master.GetConceptCardParam(current);
              if (conceptCardParam2 != null)
                num1 += (long) (conceptCardParam2.en_exp * dictionary[current]);
            }
          }
        }
        long num2 = expToLevelMax - num1;
        long num3 = (long) (conceptCardParam1.en_exp * num);
        if (num2 < num3)
          return Mathf.CeilToInt((float) num2 / (float) conceptCardParam1.en_exp);
      }
      else
      {
        int num1 = (int) this.Master.FixParam.CardTrustMax - (int) this.mConceptCardData.Trust;
        int num2 = 0;
        using (Dictionary<string, int>.KeyCollection.Enumerator enumerator = dictionary.Keys.GetEnumerator())
        {
          while (enumerator.MoveNext())
          {
            string current = enumerator.Current;
            if (!(current == iname))
            {
              ConceptCardParam conceptCardParam2 = this.Master.GetConceptCardParam(current);
              if (conceptCardParam2 != null)
                num2 += conceptCardParam2.en_trust * dictionary[current];
            }
          }
        }
        int num3 = num1 - num2;
        long num4 = (long) (conceptCardParam1.en_trust * num);
        if ((long) num3 < num4)
          return Mathf.CeilToInt((float) num3 / (float) conceptCardParam1.en_trust);
      }
      return num;
    }

    private void RefreshParamSelectItems(string iname, int value)
    {
      if (string.IsNullOrEmpty(iname) || MonoSingleton<GameManager>.Instance.Player.GetConceptCardMaterialNum(iname) == 0)
        return;
      Dictionary<string, int> dictionary;
      if (this.mTabState == ConceptCardLevelUpWindow.TabState.Enhance)
      {
        dictionary = this.mSelectExpMaterials;
      }
      else
      {
        if (this.mTabState != ConceptCardLevelUpWindow.TabState.Trust)
          return;
        dictionary = this.mSelectTrustMaterials;
      }
      if (!dictionary.ContainsKey(iname))
        dictionary.Add(iname, value);
      else
        dictionary[iname] = value;
      if (this.mTabState == ConceptCardLevelUpWindow.TabState.Enhance)
        this.RefreshFinishedExpStatus();
      else
        this.RefreshFinishedTrustStatus();
    }

    private void RefreshFinishedExpStatus()
    {
      if (this.mSelectExpMaterials == null || this.mSelectExpMaterials.Count <= 0)
        return;
      int num1 = 0;
      using (Dictionary<string, int>.KeyCollection.Enumerator enumerator = this.mSelectExpMaterials.Keys.GetEnumerator())
      {
        while (enumerator.MoveNext())
        {
          string current = enumerator.Current;
          ConceptCardParam conceptCardParam = this.Master.GetConceptCardParam(current);
          int conceptCardMaterialNum = MonoSingleton<GameManager>.Instance.Player.GetConceptCardMaterialNum(current);
          if (conceptCardParam != null)
          {
            int selectExpMaterial = this.mSelectExpMaterials[current];
            if (selectExpMaterial != 0 && selectExpMaterial <= conceptCardMaterialNum)
            {
              int num2 = conceptCardParam.en_exp * selectExpMaterial;
              num1 += num2;
            }
          }
        }
      }
      int conceptCardLevelExp1 = this.Master.GetConceptCardLevelExp((int) this.mConceptCardData.Rarity, (int) this.mConceptCardData.CurrentLvCap);
      this.mExp = Math.Min((int) this.mConceptCardData.Exp + num1, conceptCardLevelExp1);
      this.mLv = this.Master.CalcConceptCardLevel((int) this.mConceptCardData.Rarity, (int) this.mConceptCardData.Exp + num1, (int) this.mConceptCardData.CurrentLvCap);
      using (List<ConceptCardLevelUpListItem>.Enumerator enumerator = this.mCCExpListItem.GetEnumerator())
      {
        while (enumerator.MoveNext())
          enumerator.Current.SetInputLock(this.mExp < conceptCardLevelExp1);
      }
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.FinishedLevel, (UnityEngine.Object) null))
      {
        this.FinishedLevel.set_text(this.mLv.ToString());
        if (this.mLv >= (int) this.mConceptCardData.CurrentLvCap)
          ((Graphic) this.FinishedLevel).set_color(Color.get_red());
        else if (this.mLv > (int) this.mConceptCardData.Lv)
          ((Graphic) this.FinishedLevel).set_color(Color.get_green());
        else
          ((Graphic) this.FinishedLevel).set_color(Color.get_white());
      }
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.AddLevelGauge, (UnityEngine.Object) null))
      {
        if (this.mExp == (int) this.mConceptCardData.Exp || num1 == 0)
          this.AddLevelGauge.AnimateValue(0.0f, 0.0f);
        else
          this.AddLevelGauge.AnimateValue(Mathf.Min(1f, Mathf.Clamp01((float) (this.mExp - this.Master.GetConceptCardLevelExp((int) this.mConceptCardData.Rarity, (int) this.mConceptCardData.Lv)) / (float) this.Master.GetConceptCardNextExp((int) this.mConceptCardData.Rarity, (int) this.mConceptCardData.Lv))), 0.0f);
      }
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.NextExp, (UnityEngine.Object) null))
      {
        int num2 = 0;
        if (this.mExp < conceptCardLevelExp1)
        {
          int conceptCardLevelExp2 = this.Master.GetConceptCardLevelExp((int) this.mConceptCardData.Rarity, this.mLv);
          int conceptCardNextExp = this.Master.GetConceptCardNextExp((int) this.mConceptCardData.Rarity, this.mLv);
          if (this.mExp >= conceptCardLevelExp2)
            conceptCardNextExp = this.Master.GetConceptCardNextExp((int) this.mConceptCardData.Rarity, Math.Min((int) this.mConceptCardData.CurrentLvCap, this.mLv + 1));
          int num3 = this.mExp - conceptCardLevelExp2;
          num2 = Math.Max(0, conceptCardNextExp <= num3 ? 0 : conceptCardNextExp - num3);
        }
        this.NextExp.set_text(num2.ToString());
      }
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.GetAllExp, (UnityEngine.Object) null))
        this.GetAllExp.set_text(num1.ToString());
      ((Selectable) this.DecideBtn).set_interactable(num1 > 0);
    }

    private void CalcCanCardLevelUpMax()
    {
      if (this.mCacheMaxCardExpMaterialList == null)
        return;
      long num1 = 0;
      using (List<ConceptCardMaterialData>.Enumerator enumerator = this.mCacheMaxCardExpMaterialList.GetEnumerator())
      {
        while (enumerator.MoveNext())
        {
          ConceptCardMaterialData current = enumerator.Current;
          int num2 = current.Param.en_exp * MonoSingleton<GameManager>.Instance.Player.GetConceptCardMaterialNum(current.Param.iname);
          num1 += (long) num2;
        }
      }
      if (num1 < 0L)
        num1 = long.MaxValue;
      long num3 = (long) Mathf.Min((float) this.mConceptCardData.GetExpToLevelMax(), (float) num1);
      this.mSelectExpMaterials.Clear();
      int index1 = 0;
      for (int index2 = 0; index2 < this.mCacheMaxCardExpMaterialList.Count; ++index2)
      {
        if (num3 <= 0L)
        {
          num3 = 0L;
          break;
        }
        ConceptCardMaterialData maxCardExpMaterial1 = this.mCacheMaxCardExpMaterialList[index2];
        int conceptCardMaterialNum1 = MonoSingleton<GameManager>.Instance.Player.GetConceptCardMaterialNum(maxCardExpMaterial1.Param.iname);
        if (maxCardExpMaterial1 != null || conceptCardMaterialNum1 > 0)
        {
          ConceptCardMaterialData maxCardExpMaterial2 = this.mCacheMaxCardExpMaterialList[index1];
          int conceptCardMaterialNum2 = MonoSingleton<GameManager>.Instance.Player.GetConceptCardMaterialNum(maxCardExpMaterial2.Param.iname);
          if (index2 == index1 || maxCardExpMaterial2 != null || conceptCardMaterialNum2 > 0)
          {
            if ((long) maxCardExpMaterial1.Param.en_exp > num3)
            {
              index1 = index2;
            }
            else
            {
              int num2 = (int) (num3 / (long) maxCardExpMaterial1.Param.en_exp);
              int num4 = Mathf.Min(conceptCardMaterialNum1, num2);
              int num5 = maxCardExpMaterial1.Param.en_exp * num4;
              int enExp = maxCardExpMaterial2.Param.en_exp;
              if ((long) Mathf.Abs((float) (num3 - (long) num5)) > (long) Mathf.Abs((float) (num3 - (long) enExp)))
              {
                if (this.mSelectExpMaterials.ContainsKey(maxCardExpMaterial2.Param.iname))
                {
                  if (conceptCardMaterialNum2 - this.mSelectExpMaterials[maxCardExpMaterial2.Param.iname] > 0)
                  {
                    Dictionary<string, int> selectExpMaterials;
                    string iname;
                    (selectExpMaterials = this.mSelectExpMaterials)[iname = maxCardExpMaterial2.Param.iname] = selectExpMaterials[iname] + 1;
                    num3 = 0L;
                    break;
                  }
                }
                else
                {
                  this.mSelectExpMaterials.Add(maxCardExpMaterial2.Param.iname, 1);
                  num3 = 0L;
                  break;
                }
              }
              num3 -= (long) num5;
              this.mSelectExpMaterials.Add(maxCardExpMaterial1.Param.iname, num4);
              index1 = index2;
            }
          }
        }
      }
      if (num3 > 0L)
      {
        ConceptCardMaterialData maxCardExpMaterial = this.mCacheMaxCardExpMaterialList[index1];
        int conceptCardMaterialNum = MonoSingleton<GameManager>.Instance.Player.GetConceptCardMaterialNum(maxCardExpMaterial.Param.iname);
        if (maxCardExpMaterial != null && conceptCardMaterialNum > 0)
        {
          if (this.mSelectExpMaterials.ContainsKey(maxCardExpMaterial.Param.iname))
          {
            if (conceptCardMaterialNum - this.mSelectExpMaterials[maxCardExpMaterial.Param.iname] > 0)
            {
              Dictionary<string, int> selectExpMaterials;
              string iname;
              (selectExpMaterials = this.mSelectExpMaterials)[iname = maxCardExpMaterial.Param.iname] = selectExpMaterials[iname] + 1;
            }
          }
          else
            this.mSelectExpMaterials.Add(maxCardExpMaterial.Param.iname, 1);
        }
      }
      if (this.mSelectExpMaterials.Count > 0)
      {
        for (int index2 = 0; index2 < this.mCCExpListItem.Count; ++index2)
        {
          if (this.mSelectExpMaterials.ContainsKey(this.mCCExpListItem[index2].GetConceptCardIName()))
            this.mCCExpListItem[index2].SetUseParamItemSliderValue(this.mSelectExpMaterials[this.mCCExpListItem[index2].GetConceptCardIName()]);
        }
      }
      this.RefreshFinishedExpStatus();
    }

    private void RefreshFinishedTrustStatus()
    {
      if (this.mSelectTrustMaterials == null || this.mSelectTrustMaterials.Count <= 0)
        return;
      int num1 = 0;
      using (Dictionary<string, int>.KeyCollection.Enumerator enumerator = this.mSelectTrustMaterials.Keys.GetEnumerator())
      {
        while (enumerator.MoveNext())
        {
          string current = enumerator.Current;
          ConceptCardParam conceptCardParam = this.Master.GetConceptCardParam(current);
          int conceptCardMaterialNum = MonoSingleton<GameManager>.Instance.Player.GetConceptCardMaterialNum(current);
          if (conceptCardParam != null)
          {
            int selectTrustMaterial = this.mSelectTrustMaterials[current];
            if (selectTrustMaterial != 0 && selectTrustMaterial <= conceptCardMaterialNum)
            {
              int num2 = conceptCardParam.en_trust * selectTrustMaterial;
              num1 += num2;
            }
          }
        }
      }
      int cardTrustMax = (int) this.Master.FixParam.CardTrustMax;
      this.mTrust = Math.Min((int) this.mConceptCardData.Trust + num1, cardTrustMax);
      using (List<ConceptCardLevelUpListItem>.Enumerator enumerator = this.mCCTrustListItem.GetEnumerator())
      {
        while (enumerator.MoveNext())
          enumerator.Current.SetInputLock(this.mTrust < cardTrustMax);
      }
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.TrustPredictValueTxt, (UnityEngine.Object) null))
      {
        ConceptCardManager.SubstituteTrustFormat(this.mConceptCardData, this.TrustPredictValueTxt, this.mTrust, false);
        if (this.mTrust >= cardTrustMax)
          ((Graphic) this.TrustPredictValueTxt).set_color(Color.get_red());
        else if (this.mTrust > (int) this.mConceptCardData.Trust)
          ((Graphic) this.TrustPredictValueTxt).set_color(Color.get_green());
        else
          ((Graphic) this.TrustPredictValueTxt).set_color(Color.get_white());
      }
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.AddLevelGauge, (UnityEngine.Object) null))
      {
        if (this.mTrust == (int) this.mConceptCardData.Trust || num1 == 0)
          this.AddLevelGauge.AnimateValue(0.0f, 0.0f);
        else
          this.AddLevelGauge.AnimateValue(Mathf.Min(1f, Mathf.Clamp01((float) (int) this.mConceptCardData.Trust / (float) ((int) this.Master.FixParam.CardTrustMax - (int) this.mConceptCardData.Trust))), 0.0f);
      }
      ((Selectable) this.DecideBtn).set_interactable(num1 > 0);
    }

    private void CalcCanCardTrustUpMax()
    {
      if (this.mCacheMaxCardTrustMaterialList == null)
        return;
      long num1 = 0;
      using (List<ConceptCardMaterialData>.Enumerator enumerator = this.mCacheMaxCardTrustMaterialList.GetEnumerator())
      {
        while (enumerator.MoveNext())
        {
          ConceptCardMaterialData current = enumerator.Current;
          int num2 = current.Param.en_trust * MonoSingleton<GameManager>.Instance.Player.GetConceptCardMaterialNum(current.Param.iname);
          num1 += (long) num2;
        }
      }
      long num3 = (long) Mathf.Min((float) ((int) this.Master.FixParam.CardTrustMax - this.mTrust), (float) num1);
      this.mSelectTrustMaterials.Clear();
      int index1 = 0;
      for (int index2 = 0; index2 < this.mCacheMaxCardTrustMaterialList.Count; ++index2)
      {
        if (num3 <= 0L)
        {
          num3 = 0L;
          break;
        }
        ConceptCardMaterialData cardTrustMaterial1 = this.mCacheMaxCardTrustMaterialList[index2];
        int conceptCardMaterialNum1 = MonoSingleton<GameManager>.Instance.Player.GetConceptCardMaterialNum(cardTrustMaterial1.Param.iname);
        if (cardTrustMaterial1 != null || conceptCardMaterialNum1 > 0)
        {
          ConceptCardMaterialData cardTrustMaterial2 = this.mCacheMaxCardTrustMaterialList[index1];
          int conceptCardMaterialNum2 = MonoSingleton<GameManager>.Instance.Player.GetConceptCardMaterialNum(cardTrustMaterial2.Param.iname);
          if (index2 == index1 || cardTrustMaterial2 != null || conceptCardMaterialNum2 > 0)
          {
            if ((long) cardTrustMaterial1.Param.en_trust > num3)
            {
              index1 = index2;
            }
            else
            {
              int num2 = (int) (num3 / (long) cardTrustMaterial1.Param.en_trust);
              int num4 = Mathf.Min(conceptCardMaterialNum1, num2);
              int num5 = cardTrustMaterial1.Param.en_trust * num4;
              int enTrust = cardTrustMaterial2.Param.en_trust;
              if ((long) Mathf.Abs((float) (num3 - (long) num5)) > (long) Mathf.Abs((float) (num3 - (long) enTrust)))
              {
                if (this.mSelectTrustMaterials.ContainsKey(cardTrustMaterial2.Param.iname))
                {
                  if (conceptCardMaterialNum2 - this.mSelectTrustMaterials[cardTrustMaterial2.Param.iname] > 0)
                  {
                    Dictionary<string, int> selectTrustMaterials;
                    string iname;
                    (selectTrustMaterials = this.mSelectTrustMaterials)[iname = cardTrustMaterial2.Param.iname] = selectTrustMaterials[iname] + 1;
                    num3 = 0L;
                    break;
                  }
                }
                else
                {
                  this.mSelectTrustMaterials.Add(cardTrustMaterial2.Param.iname, 1);
                  num3 = 0L;
                  break;
                }
              }
              num3 -= (long) num5;
              this.mSelectTrustMaterials.Add(cardTrustMaterial1.Param.iname, num4);
              index1 = index2;
            }
          }
        }
      }
      if (num3 > 0L)
      {
        ConceptCardMaterialData cardTrustMaterial = this.mCacheMaxCardTrustMaterialList[index1];
        int conceptCardMaterialNum = MonoSingleton<GameManager>.Instance.Player.GetConceptCardMaterialNum(cardTrustMaterial.Param.iname);
        if (cardTrustMaterial != null && conceptCardMaterialNum > 0)
        {
          if (this.mSelectTrustMaterials.ContainsKey(cardTrustMaterial.Param.iname))
          {
            if (conceptCardMaterialNum - this.mSelectTrustMaterials[cardTrustMaterial.Param.iname] > 0)
            {
              Dictionary<string, int> selectTrustMaterials;
              string iname;
              (selectTrustMaterials = this.mSelectTrustMaterials)[iname = cardTrustMaterial.Param.iname] = selectTrustMaterials[iname] + 1;
            }
          }
          else
            this.mSelectTrustMaterials.Add(cardTrustMaterial.Param.iname, 1);
        }
      }
      if (this.mSelectTrustMaterials.Count > 0)
      {
        for (int index2 = 0; index2 < this.mCCTrustListItem.Count; ++index2)
        {
          if (this.mSelectTrustMaterials.ContainsKey(this.mCCTrustListItem[index2].GetConceptCardIName()))
            this.mCCTrustListItem[index2].SetUseParamItemSliderValue(this.mSelectTrustMaterials[this.mCCTrustListItem[index2].GetConceptCardIName()]);
        }
      }
      this.RefreshFinishedTrustStatus();
    }

    private void RefreshUseMaxItems(string iname, bool is_on)
    {
      if (string.IsNullOrEmpty(iname))
        return;
      List<ConceptCardMaterialData> cardMaterialDataList;
      ConceptCardMaterialData cardMaterialData;
      if (this.mTabState == ConceptCardLevelUpWindow.TabState.Enhance)
      {
        cardMaterialDataList = this.mCacheMaxCardExpMaterialList;
        cardMaterialData = this.ConceptCardExpMaterials.Find((Predicate<ConceptCardMaterialData>) (p => p.Param.iname == iname));
      }
      else
      {
        if (this.mTabState != ConceptCardLevelUpWindow.TabState.Trust)
          return;
        cardMaterialDataList = this.mCacheMaxCardTrustMaterialList;
        cardMaterialData = this.ConceptCardTrustMaterials.Find((Predicate<ConceptCardMaterialData>) (p => p.Param.iname == iname));
      }
      if (cardMaterialData == null)
        return;
      if (!is_on)
      {
        if (cardMaterialDataList.FindIndex((Predicate<ConceptCardMaterialData>) (p => p.Param.iname == iname)) != -1)
          cardMaterialDataList.RemoveAt(cardMaterialDataList.FindIndex((Predicate<ConceptCardMaterialData>) (p => p.Param.iname == iname)));
      }
      else if (cardMaterialDataList.Find((Predicate<ConceptCardMaterialData>) (p => p.Param.iname == iname)) == null)
        cardMaterialDataList.Add(cardMaterialData);
      if (this.mTabState == ConceptCardLevelUpWindow.TabState.Enhance)
        cardMaterialDataList.Sort((Comparison<ConceptCardMaterialData>) ((a, b) => b.Param.en_exp - a.Param.en_exp));
      else
        cardMaterialDataList.Sort((Comparison<ConceptCardMaterialData>) ((a, b) => b.Param.en_trust - a.Param.en_trust));
      this.SaveSelectUseMax();
      ((Selectable) this.MaxBtn).set_interactable(cardMaterialDataList != null && cardMaterialDataList.Count > 0);
    }

    private void SavePage()
    {
      PlayerPrefsUtility.SetString(PlayerPrefsUtility.CONCEPT_CARD_LEVELUP_PAGE_CHECKS, ((int) this.mTabState).ToString(), true);
    }

    private void SaveSelectUseMax()
    {
      List<string> stringList = new List<string>();
      for (int index = 0; index < this.mCacheMaxCardExpMaterialList.Count; ++index)
        stringList.Add(this.mCacheMaxCardExpMaterialList[index].Param.iname);
      for (int index = 0; index < this.mCacheMaxCardTrustMaterialList.Count; ++index)
        stringList.Add(this.mCacheMaxCardTrustMaterialList[index].Param.iname);
      string str = stringList.Count <= 0 ? string.Empty : string.Join("|", stringList.ToArray());
      PlayerPrefsUtility.SetString(PlayerPrefsUtility.CONCEPT_CARD_LEVELUP_EXPITEM_CHECKS, str, true);
    }

    private static ConceptCardLevelUpWindow.TabState LoadTabState()
    {
      if (!PlayerPrefsUtility.HasKey(PlayerPrefsUtility.CONCEPT_CARD_LEVELUP_PAGE_CHECKS))
        return ConceptCardLevelUpWindow.TabState.Enhance;
      string s = PlayerPrefsUtility.GetString(PlayerPrefsUtility.CONCEPT_CARD_LEVELUP_PAGE_CHECKS, string.Empty);
      if (string.IsNullOrEmpty(s))
        return ConceptCardLevelUpWindow.TabState.Enhance;
      int result = 0;
      if (!int.TryParse(s, out result))
        return ConceptCardLevelUpWindow.TabState.Enhance;
      return (ConceptCardLevelUpWindow.TabState) result;
    }

    private void SetSelectMaterials()
    {
      ConceptCardManager instance = ConceptCardManager.Instance;
      if (UnityEngine.Object.op_Equality((UnityEngine.Object) instance, (UnityEngine.Object) null))
        return;
      List<SelecteConceptCardMaterial> conceptCardMaterialList = new List<SelecteConceptCardMaterial>();
      Dictionary<string, int> dictionary;
      List<ConceptCardLevelUpListItem> cardLevelUpListItemList;
      List<ConceptCardMaterialData> cardMaterialDataList;
      if (this.mTabState == ConceptCardLevelUpWindow.TabState.Enhance)
      {
        dictionary = this.mSelectExpMaterials;
        cardLevelUpListItemList = this.mCCExpListItem;
        cardMaterialDataList = this.ConceptCardExpMaterials;
      }
      else
      {
        if (this.mTabState != ConceptCardLevelUpWindow.TabState.Trust)
          return;
        dictionary = this.mSelectTrustMaterials;
        cardLevelUpListItemList = this.mCCTrustListItem;
        cardMaterialDataList = this.ConceptCardTrustMaterials;
      }
      using (Dictionary<string, int>.KeyCollection.Enumerator enumerator1 = dictionary.Keys.GetEnumerator())
      {
        while (enumerator1.MoveNext())
        {
          string key = enumerator1.Current;
          if (this.Master.GetConceptCardParam(key) != null)
          {
            int num1 = dictionary[key];
            if (num1 <= MonoSingleton<GameManager>.Instance.Player.GetConceptCardMaterialNum(key) && num1 > 0)
            {
              int num2 = 0;
              using (List<ConceptCardMaterialData>.Enumerator enumerator2 = cardMaterialDataList.GetEnumerator())
              {
                while (enumerator2.MoveNext())
                {
                  if (enumerator2.Current.Param.iname == key)
                  {
                    ConceptCardLevelUpListItem cardLevelUpListItem = cardLevelUpListItemList.Find((Predicate<ConceptCardLevelUpListItem>) (ccd => ccd.GetConceptCardIName() == key));
                    if (!UnityEngine.Object.op_Equality((UnityEngine.Object) cardLevelUpListItem, (UnityEngine.Object) null))
                    {
                      conceptCardMaterialList.Add(new SelecteConceptCardMaterial()
                      {
                        mUniqueID = MonoSingleton<GameManager>.Instance.Player.GetConceptCardMaterialUniqueID(key),
                        mSelectedData = cardLevelUpListItem.GetConceptCardData(),
                        mSelectNum = num1
                      });
                      ++num2;
                      if (num2 == num1)
                        break;
                    }
                  }
                }
              }
            }
          }
        }
      }
      instance.BulkSelectedMaterialList = conceptCardMaterialList;
    }

    private void SetTabEnhance()
    {
      this.mTabState = ConceptCardLevelUpWindow.TabState.Enhance;
      if (UnityEngine.Object.op_Equality((UnityEngine.Object) this.MainLevelup, (UnityEngine.Object) null) || UnityEngine.Object.op_Equality((UnityEngine.Object) this.MainTrust, (UnityEngine.Object) null))
        return;
      this.MainLevelup.SetActive(true);
      this.MainTrust.SetActive(false);
      using (List<ConceptCardLevelUpListItem>.Enumerator enumerator = this.mCCExpListItem.GetEnumerator())
      {
        while (enumerator.MoveNext())
          ((Component) enumerator.Current).get_gameObject().SetActive(true);
      }
      using (List<ConceptCardLevelUpListItem>.Enumerator enumerator = this.mCCTrustListItem.GetEnumerator())
      {
        while (enumerator.MoveNext())
          ((Component) enumerator.Current).get_gameObject().SetActive(false);
      }
      this.TabEnhanceToggle.set_isOn(true);
      this.TabTrustToggle.set_isOn(false);
      this.InitWindowButton();
      this.SavePage();
    }

    private void SetTabTrust()
    {
      this.mTabState = ConceptCardLevelUpWindow.TabState.Trust;
      if (UnityEngine.Object.op_Equality((UnityEngine.Object) this.MainLevelup, (UnityEngine.Object) null) || UnityEngine.Object.op_Equality((UnityEngine.Object) this.MainTrust, (UnityEngine.Object) null))
        return;
      this.MainLevelup.SetActive(false);
      this.MainTrust.SetActive(true);
      using (List<ConceptCardLevelUpListItem>.Enumerator enumerator = this.mCCExpListItem.GetEnumerator())
      {
        while (enumerator.MoveNext())
          ((Component) enumerator.Current).get_gameObject().SetActive(false);
      }
      using (List<ConceptCardLevelUpListItem>.Enumerator enumerator = this.mCCTrustListItem.GetEnumerator())
      {
        while (enumerator.MoveNext())
          ((Component) enumerator.Current).get_gameObject().SetActive(true);
      }
      this.TabEnhanceToggle.set_isOn(false);
      this.TabTrustToggle.set_isOn(true);
      this.InitWindowButton();
      this.SavePage();
    }

    private enum TabState
    {
      Enhance,
      Trust,
    }
  }
}
