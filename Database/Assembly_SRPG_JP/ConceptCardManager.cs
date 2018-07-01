// Decompiled with JetBrains decompiler
// Type: SRPG.ConceptCardManager
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 85BFDF7F-5712-4D45-9CD6-3465C703DFDF
// Assembly location: S:\Desktop\Assembly-CSharp.dll

using GR;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace SRPG
{
  [FlowNode.Pin(14, "VisionMaster再生", FlowNode.PinTypes.Input, 14)]
  [FlowNode.Pin(13, "グループスキル強化再生", FlowNode.PinTypes.Input, 13)]
  [FlowNode.Pin(111, "トラストマスター再生後", FlowNode.PinTypes.Output, 111)]
  [FlowNode.Pin(11, "トラストマスター再生", FlowNode.PinTypes.Input, 11)]
  [FlowNode.Pin(112, "限界突破アニメ再生後", FlowNode.PinTypes.Output, 112)]
  [FlowNode.Pin(12, "限界突破アニメ再生", FlowNode.PinTypes.Input, 12)]
  [FlowNode.Pin(110, "強化アニメ再生後", FlowNode.PinTypes.Output, 110)]
  [FlowNode.Pin(10, "強化アニメ再生", FlowNode.PinTypes.Input, 10)]
  [FlowNode.Pin(1, "選択素材等クリア", FlowNode.PinTypes.Input, 1)]
  [FlowNode.Pin(0, "初期化", FlowNode.PinTypes.Input, 0)]
  [FlowNode.Pin(200, "TIPS表示", FlowNode.PinTypes.Output, 200)]
  [FlowNode.Pin(114, "VisionMaster再生後", FlowNode.PinTypes.Output, 114)]
  [FlowNode.Pin(113, "グループスキル強化再生後", FlowNode.PinTypes.Output, 113)]
  public class ConceptCardManager : MonoBehaviour, IFlowInterface
  {
    public const int PIN_INIT = 0;
    public const int PIN_CLEAR_MAT = 1;
    public const int PIN_SELL = 3;
    public const int PIN_ENHANCE_ANIM = 10;
    public const int PIN_TRUSTMASTER_ANIM = 11;
    public const int PIN_AWAKE_ANIM = 12;
    public const int PIN_GROUPSKILL_POWERUP_ANIM = 13;
    public const int PIN_GROUPSKILL_MAX_POWERUP_ANIM = 14;
    public const int PIN_ENHANCE_ANIM_OUTPUT = 110;
    public const int PIN_TRUSTMASTER_ANIM_OUTPUT = 111;
    public const int PIN_AWAKE_ANIM_OUTPUT = 112;
    public const int PIN_GROUPSKILL_POWERUP_ANIM_OUTPUT = 113;
    public const int PIN_GROUPSKILL_MAX_POWERUP_ANIM_OUTPUT = 114;
    public const int PIN_TIPS_EQUIPMENT_OUTPUT = 200;
    private static ConceptCardManager _instance;
    [SerializeField]
    private GameObject mConceptCardBranceList;
    [SerializeField]
    private GameObject mConceptCardEnhanceList;
    [SerializeField]
    private GameObject mConceptCardSellList;
    [SerializeField]
    private GameObject mConceptCardDetail;
    [SerializeField]
    private GameObject mConceptCardCheck;
    [Space(10f)]
    private ConceptCardDetailLevel mLevelObject;
    [HideInInspector]
    public ConceptCardListFilterWindow.Type FilterType;
    [HideInInspector]
    public bool ToggleSameSelectCard;
    [HideInInspector]
    public ConceptCardListSortWindow.Type SortType;
    [HideInInspector]
    public ConceptCardListSortWindow.Type SortOrderType;
    private OLong mSelectedUniqueID;
    private MultiConceptCard mSelectedMaterials;
    [HideInInspector]
    public int CostConceptCardRare;
    private List<SelecteConceptCardMaterial> mBulkSelectedMaterialList;
    private ConceptCardData mSelectedConceptCardMaterial;

    public ConceptCardManager()
    {
      base.\u002Ector();
    }

    public static ConceptCardManager Instance
    {
      get
      {
        return ConceptCardManager._instance;
      }
    }

    private void Awake()
    {
      ConceptCardManager._instance = this;
      this.LoadSortFilterData();
    }

    private void OnDestroy()
    {
      ConceptCardManager._instance = (ConceptCardManager) null;
    }

    public bool IsBranceListActive
    {
      get
      {
        return this.mConceptCardBranceList.GetActive();
      }
    }

    public bool IsEnhanceListActive
    {
      get
      {
        return this.mConceptCardEnhanceList.GetActive();
      }
    }

    public bool IsSellListActive
    {
      get
      {
        return this.mConceptCardSellList.GetActive();
      }
    }

    public bool IsDetailActive
    {
      get
      {
        return this.mConceptCardDetail.GetActive();
      }
    }

    public static string ParseTrustFormat(int trust)
    {
      int cardTrustMax = (int) MonoSingleton<GameManager>.Instance.MasterParam.FixParam.CardTrustMax;
      return ((float) (Mathf.Min(trust, cardTrustMax) / 10 * 10) / 100f).ToString("F1");
    }

    public static void SubstituteTrustFormat(ConceptCardData card, Text txt, int trust, bool notChangeColor = false)
    {
      if (UnityEngine.Object.op_Equality((UnityEngine.Object) txt, (UnityEngine.Object) null) || card == null)
        return;
      string trustFormat = ConceptCardManager.ParseTrustFormat(trust);
      txt.set_text(trustFormat);
      if (notChangeColor)
        return;
      if (trust >= (int) MonoSingleton<GameManager>.Instance.MasterParam.FixParam.CardTrustMax && card.GetReward() != null)
        ((Graphic) txt).set_color(Color.get_red());
      else
        ((Graphic) txt).set_color(Color.get_white());
    }

    public static void CalcTotalExpTrust(ConceptCardData selectedCard, MultiConceptCard materials, out int mixTotalExp, out int mixTrustExp, out int mixTotalAwakeLv)
    {
      int num = 0;
      mixTotalExp = 0;
      mixTrustExp = 0;
      mixTotalAwakeLv = 0;
      using (List<ConceptCardData>.Enumerator enumerator = materials.GetList().GetEnumerator())
      {
        while (enumerator.MoveNext())
        {
          ConceptCardData current = enumerator.Current;
          mixTotalExp += current.MixExp;
          mixTrustExp += current.Param.en_trust;
          if (selectedCard != null && selectedCard.Param.iname == current.Param.iname)
            mixTrustExp += (int) MonoSingleton<GameManager>.Instance.MasterParam.FixParam.CardTrustPileUp;
          if (selectedCard != null && selectedCard.Param.iname == current.Param.iname && (int) selectedCard.AwakeCount + num < selectedCard.AwakeCountCap)
            ++num;
        }
      }
      mixTotalAwakeLv = (int) MonoSingleton<GameManager>.Instance.MasterParam.FixParam.CardAwakeUnlockLevelCap * num;
    }

    public static void CalcTotalExpTrustMaterialData(out int mixTotalExp, out int mixTrustExp)
    {
      mixTotalExp = 0;
      mixTrustExp = 0;
      ConceptCardManager instance = ConceptCardManager.Instance;
      if (UnityEngine.Object.op_Equality((UnityEngine.Object) instance, (UnityEngine.Object) null) || instance.BulkSelectedMaterialList.Count == 0)
        return;
      using (List<SelecteConceptCardMaterial>.Enumerator enumerator = instance.BulkSelectedMaterialList.GetEnumerator())
      {
        while (enumerator.MoveNext())
        {
          SelecteConceptCardMaterial current = enumerator.Current;
          mixTotalExp += current.mSelectedData.MixExp * current.mSelectNum;
          mixTrustExp += current.mSelectedData.Param.en_trust * current.mSelectNum;
        }
      }
    }

    public static void CalcTotalExpTrust(out int mixTotalExp, out int mixTrustExp, out int mixTotalAwakeLv)
    {
      if (UnityEngine.Object.op_Equality((UnityEngine.Object) ConceptCardManager.Instance, (UnityEngine.Object) null))
      {
        mixTotalExp = 0;
        mixTrustExp = 0;
        mixTotalAwakeLv = 0;
      }
      else
        ConceptCardManager.CalcTotalExpTrust(ConceptCardManager.Instance.SelectedConceptCardData, ConceptCardManager.Instance.SelectedMaterials, out mixTotalExp, out mixTrustExp, out mixTotalAwakeLv);
    }

    public static void GalcTotalSellZeny(MultiConceptCard materials, out int totalSellZeny)
    {
      totalSellZeny = 0;
      using (List<ConceptCardData>.Enumerator enumerator = materials.GetList().GetEnumerator())
      {
        while (enumerator.MoveNext())
        {
          ConceptCardData current = enumerator.Current;
          totalSellZeny += current.SellGold;
        }
      }
    }

    public static void GalcTotalMixZeny(MultiConceptCard materials, out int totalMixZeny)
    {
      totalMixZeny = 0;
      using (List<ConceptCardData>.Enumerator enumerator = materials.GetList().GetEnumerator())
      {
        while (enumerator.MoveNext())
        {
          ConceptCardData current = enumerator.Current;
          totalMixZeny += current.Param.en_cost;
        }
      }
    }

    public static void GalcTotalMixZenyMaterialData(out int totalMixZeny)
    {
      totalMixZeny = 0;
      ConceptCardManager instance = ConceptCardManager.Instance;
      if (UnityEngine.Object.op_Equality((UnityEngine.Object) instance, (UnityEngine.Object) null) || instance.BulkSelectedMaterialList.Count == 0)
        return;
      using (List<SelecteConceptCardMaterial>.Enumerator enumerator = instance.BulkSelectedMaterialList.GetEnumerator())
      {
        while (enumerator.MoveNext())
        {
          SelecteConceptCardMaterial current = enumerator.Current;
          totalMixZeny += current.mSelectedData.Param.en_cost * current.mSelectNum;
        }
      }
    }

    public static string GetWarningTextByMaterials(MultiConceptCard materials)
    {
      string empty = string.Empty;
      bool flag = false;
      using (List<ConceptCardData>.Enumerator enumerator = materials.GetList().GetEnumerator())
      {
        while (enumerator.MoveNext())
        {
          if ((int) enumerator.Current.Rarity >= 3)
            flag = true;
        }
      }
      if (flag)
        empty = LocalizedText.Get("sys.CONCEPT_CARD_WARNING_HIGH_RARITY");
      return empty;
    }

    public void Activated(int pinID)
    {
      switch (pinID)
      {
        case 0:
          this.Init();
          break;
        case 1:
          this.ClearMaterials();
          break;
        case 10:
          this.mLevelObject.StartLevelupAnimation(new ConceptCardDetailLevel.EffectCallBack(this.EnhanceAnimCallBack));
          break;
        case 11:
          this.mLevelObject.StartTrustMasterAnimation(new ConceptCardDetailLevel.EffectCallBack(this.TrustMasterAnimCallBack));
          break;
        case 12:
          this.mLevelObject.StartAwakeAnimation(new ConceptCardDetailLevel.EffectCallBack(this.AwakeAnimCallBack));
          break;
        case 13:
          this.mLevelObject.StartGroupSkillPowerUpAnimation(new ConceptCardDetailLevel.EffectCallBack(this.GroupSkillPowerUpAnimCallBack));
          break;
        case 14:
          this.mLevelObject.StartGroupSkillMaxPowerUpAnimation(new ConceptCardDetailLevel.EffectCallBack(this.GroupSkillMaxPowerUpAnimCallBack));
          break;
      }
    }

    private void EnhanceAnimCallBack()
    {
      FlowNode_GameObject.ActivateOutputLinks((Component) this, 110);
    }

    private void AwakeAnimCallBack()
    {
      FlowNode_GameObject.ActivateOutputLinks((Component) this, 112);
    }

    private void GroupSkillPowerUpAnimCallBack()
    {
      FlowNode_GameObject.ActivateOutputLinks((Component) this, 113);
    }

    private void GroupSkillMaxPowerUpAnimCallBack()
    {
      FlowNode_GameObject.ActivateOutputLinks((Component) this, 114);
    }

    private void TrustMasterAnimCallBack()
    {
      FlowNode_GameObject.ActivateOutputLinks((Component) this, 111);
    }

    public ConceptCardData SelectedConceptCardData
    {
      set
      {
        this.mSelectedUniqueID = value.UniqueID;
      }
      get
      {
        return MonoSingleton<GameManager>.Instance.Player.ConceptCards.Find((Predicate<ConceptCardData>) (ccd => (long) ccd.UniqueID == (long) this.mSelectedUniqueID));
      }
    }

    public ConceptCardData SelectedConceptCardMaterialData
    {
      set
      {
        this.mSelectedConceptCardMaterial = value;
      }
      get
      {
        return this.mSelectedConceptCardMaterial;
      }
    }

    public bool IsEqualsSelectedConceptCardData(ConceptCardData ccd)
    {
      if (ccd == null)
        return false;
      ConceptCardData selectedConceptCardData = this.SelectedConceptCardData;
      if (selectedConceptCardData == null)
        return false;
      return (long) ccd.UniqueID == (long) selectedConceptCardData.UniqueID;
    }

    public MultiConceptCard SelectedMaterials
    {
      set
      {
        this.mSelectedMaterials = value;
      }
      get
      {
        return this.mSelectedMaterials;
      }
    }

    private void ClearMaterials()
    {
      this.mSelectedMaterials.Clear();
    }

    public List<SelecteConceptCardMaterial> BulkSelectedMaterialList
    {
      set
      {
        this.mBulkSelectedMaterialList = value;
      }
      get
      {
        return this.mBulkSelectedMaterialList;
      }
    }

    private void Init()
    {
      this.CallConceptCardInit(this.mConceptCardBranceList);
      this.CallConceptCardInit(this.mConceptCardEnhanceList);
      this.CallConceptCardInit(this.mConceptCardSellList);
      this.CallConceptCardInit(this.mConceptCardDetail);
      ConceptCardDetail component = (ConceptCardDetail) this.mConceptCardDetail.GetComponent<ConceptCardDetail>();
      component.Init();
      this.mLevelObject = component.Description.Level;
      this.CallConceptCardInit(this.mConceptCardCheck);
      MonoSingleton<GameManager>.Instance.Player.UpdateConceptCardTrophyAll();
      if (!MonoSingleton<GameManager>.Instance.Player.ConceptCards.Any<ConceptCardData>((Func<ConceptCardData, bool>) (card => card.Param.type == eCardType.Equipment)))
        return;
      FlowNode_GameObject.ActivateOutputLinks((Component) this, 200);
    }

    private void CallConceptCardInit(GameObject obj)
    {
      if (UnityEngine.Object.op_Equality((UnityEngine.Object) obj, (UnityEngine.Object) null))
        return;
      ConceptCardList conceptCardList = !UnityEngine.Object.op_Inequality((UnityEngine.Object) obj, (UnityEngine.Object) null) ? (ConceptCardList) null : (ConceptCardList) obj.GetComponent<ConceptCardList>();
      if (UnityEngine.Object.op_Equality((UnityEngine.Object) conceptCardList, (UnityEngine.Object) null))
        return;
      conceptCardList.Init();
    }

    public void GetTotalExp(out int mixTotalExp, out int mixTrustExp)
    {
      mixTotalExp = 0;
      mixTrustExp = 0;
      using (List<ConceptCardData>.Enumerator enumerator = this.SelectedMaterials.GetList().GetEnumerator())
      {
        while (enumerator.MoveNext())
        {
          ConceptCardData current = enumerator.Current;
          mixTotalExp += current.MixExp;
          mixTrustExp += current.Param.en_trust;
        }
      }
    }

    public void SetupLevelupAnimation()
    {
      int mixTotalExp;
      int mixTrustExp;
      int mixTotalAwakeLv;
      ConceptCardManager.CalcTotalExpTrust(this.SelectedConceptCardData, this.SelectedMaterials, out mixTotalExp, out mixTrustExp, out mixTotalAwakeLv);
      this.mLevelObject.SetupLevelupAnimation(mixTotalExp, mixTrustExp);
    }

    public void SetupBulkLevelupAnimation()
    {
      int mixTotalExp;
      int mixTrustExp;
      ConceptCardManager.CalcTotalExpTrustMaterialData(out mixTotalExp, out mixTrustExp);
      this.mLevelObject.SetupLevelupAnimation(mixTotalExp, mixTrustExp);
    }

    public void LoadSortFilterData()
    {
      this.FilterType = ConceptCardListFilterWindow.LoadData();
      this.SortType = ConceptCardListSortWindow.LoadDataType();
      this.SortOrderType = ConceptCardListSortWindow.LoadDataOrderType();
    }
  }
}
