// Decompiled with JetBrains decompiler
// Type: SRPG.ConceptCardDescription
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 85BFDF7F-5712-4D45-9CD6-3465C703DFDF
// Assembly location: S:\Desktop\Assembly-CSharp.dll

using GR;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.UI;

namespace SRPG
{
  [FlowNode.Pin(2, "変更前のスキルを非表示", FlowNode.PinTypes.Input, 2)]
  [FlowNode.Pin(1, "変更前のスキルを表示", FlowNode.PinTypes.Input, 1)]
  public class ConceptCardDescription : MonoBehaviour, IFlowInterface
  {
    public const int INPUT_ENABLE_BASE_SKILL = 1;
    public const int INPUT_DIABLE_BASE_SKILL = 2;
    [SerializeField]
    private string PREFAB_PATH_BONUS_WINDOW;
    [SerializeField]
    public ConceptCardDetailLevel Level;
    [SerializeField]
    public ConceptCardDetailStatus Status;
    [SerializeField]
    public ConceptCardDetailSkin SkinPrefab;
    [SerializeField]
    private GameObject AbilityTemplate;
    [SerializeField]
    private GameObject mCardAbilityDescriptionPrefab;
    [SerializeField]
    private ConceptCardDetailGetUnit GetUnit;
    [SerializeField]
    private Selectable m_ShowBaseToggle;
    private GameObject mAbilityDetailParent;
    private GameObject mCardAbilityDescription;
    private List<ConceptCardDetailSkin> Skin;
    private List<AbilityDeriveList> m_AbilityDeriveList;
    private ConceptCardData mConceptCardData;
    private UnitData m_UnitData;
    [SerializeField]
    private Button OpenBonusButton;
    private GameObject mBonusWindow;
    private Canvas abilityCanvas;
    private Canvas bonusCanvas;
    private static ConceptCardDescription _instance;
    private bool mIsEnhance;
    private ConceptCardDescription.ConceptCardEnhanceInfo mEnhanceInfo;

    public ConceptCardDescription()
    {
      base.\u002Ector();
    }

    public static bool IsEnhance
    {
      get
      {
        if (Object.op_Inequality((Object) ConceptCardDescription._instance, (Object) null))
          return ConceptCardDescription._instance.mIsEnhance;
        return false;
      }
    }

    public static ConceptCardDescription.ConceptCardEnhanceInfo EnhanceInfo
    {
      get
      {
        if (Object.op_Inequality((Object) ConceptCardDescription._instance, (Object) null) && ConceptCardDescription.IsEnhance)
          return ConceptCardDescription._instance.mEnhanceInfo;
        return (ConceptCardDescription.ConceptCardEnhanceInfo) null;
      }
    }

    private void Awake()
    {
      ConceptCardDescription._instance = this;
    }

    public void Activated(int pinID)
    {
      switch (pinID)
      {
        case 1:
          this.SwitchBaseSkillEnable(true);
          break;
        case 2:
          this.SwitchBaseSkillEnable(false);
          break;
      }
    }

    private void SwitchBaseSkillEnable(bool enable)
    {
      using (List<AbilityDeriveList>.Enumerator enumerator = this.m_AbilityDeriveList.GetEnumerator())
      {
        while (enumerator.MoveNext())
          enumerator.Current.SwitchBaseAbilityEnable(enable);
      }
    }

    public void SetConceptCardData(ConceptCardData data, GameObject ability_detail_parent, bool bEnhance, bool is_first_get_unit = false, UnitData unitData = null)
    {
      this.mConceptCardData = data;
      if (this.mConceptCardData == null)
        return;
      this.m_UnitData = unitData;
      this.mIsEnhance = bEnhance;
      int mixTotalExp;
      int mixTrustExp;
      int mixTotalAwakeLv;
      ConceptCardManager.CalcTotalExpTrust(out mixTotalExp, out mixTrustExp, out mixTotalAwakeLv);
      this.mEnhanceInfo = new ConceptCardDescription.ConceptCardEnhanceInfo(data, mixTotalExp, mixTrustExp, mixTotalAwakeLv);
      this.CreatePrefab(ability_detail_parent);
      this.SetParam((ConceptCardDetailBase) this.Level, this.mConceptCardData, mixTotalExp, mixTrustExp, mixTotalAwakeLv);
      this.SetParam((ConceptCardDetailBase) this.Status, this.mConceptCardData, mixTotalExp, mixTrustExp, mixTotalAwakeLv);
      if (this.Skin != null)
      {
        for (int index = 0; index < this.Skin.Count; ++index)
          this.SetParam((ConceptCardDetailBase) this.Skin[index], this.mConceptCardData);
      }
      if (Object.op_Inequality((Object) this.GetUnit, (Object) null))
      {
        if (!is_first_get_unit)
          ((Component) this.GetUnit).get_gameObject().SetActive(false);
        this.SetParam((ConceptCardDetailBase) this.GetUnit, this.mConceptCardData);
      }
      if (Object.op_Inequality((Object) this.OpenBonusButton, (Object) null))
      {
        bool flag = this.mConceptCardData.IsEnableAwake;
        if (!this.mConceptCardData.Param.IsExistAddCardSkillBuffAwake() && !this.mConceptCardData.Param.IsExistAddCardSkillBuffLvMax())
          flag = false;
        ((Selectable) this.OpenBonusButton).set_interactable(flag);
      }
      this.Refresh();
    }

    private void Refresh()
    {
      if (this.mConceptCardData == null)
        return;
      this.Refresh((ConceptCardDetailBase) this.Level);
      this.Refresh((ConceptCardDetailBase) this.Status);
      if (this.Skin != null)
      {
        for (int index = 0; index < this.Skin.Count; ++index)
          this.Refresh((ConceptCardDetailBase) this.Skin[index]);
      }
      if (Object.op_Inequality((Object) this.GetUnit, (Object) null))
        this.Refresh((ConceptCardDetailBase) this.GetUnit);
      foreach (Scrollbar componentsInChild in (Scrollbar[]) ((Component) this).GetComponentsInChildren<Scrollbar>())
        componentsInChild.set_value(1f);
    }

    public void Refresh(ConceptCardDetailBase concept)
    {
      if (!Object.op_Inequality((Object) concept, (Object) null))
        return;
      concept.Refresh();
    }

    public void SetParam(ConceptCardDetailBase concept, ConceptCardData data)
    {
      if (!Object.op_Inequality((Object) concept, (Object) null))
        return;
      concept.SetParam(data);
    }

    public void SetParam(ConceptCardDetailBase concept, ConceptCardData data, int addExp, int addTrust, int addAwakeLv)
    {
      if (!Object.op_Inequality((Object) concept, (Object) null))
        return;
      concept.SetParam(data, addExp, addTrust, addAwakeLv);
    }

    public void CreatePrefab(GameObject ability_detail_parent)
    {
      this.CreateSkinPrefab();
      this.CreateSkillPrefab(ability_detail_parent);
    }

    public void CreateSkinPrefab()
    {
      List<ConceptCardEquipEffect> equipEffects = this.mConceptCardData.EquipEffects;
      int index1 = 0;
      if (equipEffects != null)
      {
        for (int index2 = 0; index2 < equipEffects.Count; ++index2)
        {
          ConceptCardEquipEffect effect = equipEffects[index2];
          if (!string.IsNullOrEmpty(effect.Skin))
          {
            ConceptCardDetailSkin conceptCardDetailSkin;
            if (this.Skin.Count <= index1)
            {
              conceptCardDetailSkin = (ConceptCardDetailSkin) Object.Instantiate<ConceptCardDetailSkin>((M0) this.SkinPrefab);
              this.Skin.Add(conceptCardDetailSkin);
            }
            else
              conceptCardDetailSkin = this.Skin[index1];
            ((Component) conceptCardDetailSkin).get_gameObject().SetActive(true);
            ((Component) conceptCardDetailSkin).get_transform().SetParent(((Component) this.SkinPrefab).get_transform().get_parent(), false);
            conceptCardDetailSkin.SetEquipEffect(effect);
            ++index1;
          }
        }
      }
      for (int index2 = index1; index2 < this.Skin.Count; ++index2)
        ((Component) this.Skin[index2]).get_gameObject().SetActive(false);
    }

    public void CreateSkillPrefab(GameObject ability_detail_parent)
    {
      List<ConceptCardSkillDatailData> abilityDatailData = this.mConceptCardData.GetAbilityDatailData();
      this.mAbilityDetailParent = ability_detail_parent;
      if (Object.op_Inequality((Object) this.m_ShowBaseToggle, (Object) null))
        this.m_ShowBaseToggle.set_interactable(false);
      for (int index = 0; index < this.m_AbilityDeriveList.Count; ++index)
        ((Component) this.m_AbilityDeriveList[index]).get_gameObject().SetActive(false);
      for (int index = 0; index < abilityDatailData.Count; ++index)
      {
        ConceptCardSkillDatailData conceptCardSkillDatailData = abilityDatailData[index];
        AbilityDeriveList abilityDeriveList;
        if (index < this.m_AbilityDeriveList.Count)
        {
          abilityDeriveList = this.m_AbilityDeriveList[index];
        }
        else
        {
          abilityDeriveList = this.CreateAbilityListItem();
          this.m_AbilityDeriveList.Add(abilityDeriveList);
        }
        abilityDeriveList.SetupWithConceptCard(conceptCardSkillDatailData, this.m_UnitData, new ConceptCardDetailAbility.ClickDetail(this.OnClickDetail));
        if (Object.op_Inequality((Object) this.m_ShowBaseToggle, (Object) null))
        {
          Selectable showBaseToggle = this.m_ShowBaseToggle;
          showBaseToggle.set_interactable(showBaseToggle.get_interactable() | abilityDeriveList.HasDerive);
        }
        GameUtility.SetGameObjectActive((Component) abilityDeriveList, true);
      }
      GameUtility.SetGameObjectActive(this.AbilityTemplate, false);
      this.AbilityTemplate.get_gameObject().SetActive(false);
    }

    private AbilityDeriveList CreateAbilityListItem()
    {
      GameObject gameObject = (GameObject) Object.Instantiate<GameObject>((M0) this.AbilityTemplate);
      gameObject.get_gameObject().SetActive(true);
      gameObject.get_transform().SetParent(this.AbilityTemplate.get_transform().get_parent(), false);
      return (AbilityDeriveList) gameObject.GetComponent<AbilityDeriveList>();
    }

    public void OnClickOpenBonusButton()
    {
      if (this.mConceptCardData == null)
        return;
      this.StartCoroutine(this.OpenBonusWindow(this.mConceptCardData));
    }

    [DebuggerHidden]
    private IEnumerator OpenBonusWindow(ConceptCardData concept_card)
    {
      // ISSUE: object of a compiler-generated type is created
      return (IEnumerator) new ConceptCardDescription.\u003COpenBonusWindow\u003Ec__IteratorF4()
      {
        concept_card = concept_card,
        \u003C\u0024\u003Econcept_card = concept_card,
        \u003C\u003Ef__this = this
      };
    }

    private void OnDestoryBonusWindow(GameObject obj)
    {
      if (Object.op_Equality((Object) this.bonusCanvas, (Object) null))
        return;
      Object.Destroy((Object) ((Component) this.bonusCanvas).get_gameObject());
      this.bonusCanvas = (Canvas) null;
    }

    public void OnClickDetail(ConceptCardSkillDatailData data)
    {
      if (Object.op_Equality((Object) this.mAbilityDetailParent, (Object) null) || Object.op_Inequality((Object) this.mCardAbilityDescription, (Object) null))
        return;
      this.mCardAbilityDescription = (GameObject) Object.Instantiate<GameObject>((M0) this.mCardAbilityDescriptionPrefab);
      DataSource.Bind<ConceptCardSkillDatailData>(this.mCardAbilityDescription, data);
      this.abilityCanvas = UIUtility.PushCanvas(false, -1);
      this.mCardAbilityDescription.get_transform().SetParent(((Component) this.abilityCanvas).get_transform(), false);
      ((DestroyEventListener) this.mCardAbilityDescription.AddComponent<DestroyEventListener>()).Listeners += new DestroyEventListener.DestroyEvent(this.OnDestroyAbilityDescription);
    }

    private void OnDestroyAbilityDescription(GameObject go)
    {
      if (!Object.op_Inequality((Object) this.abilityCanvas, (Object) null))
        return;
      Object.Destroy((Object) ((Component) this.abilityCanvas).get_gameObject());
      this.abilityCanvas = (Canvas) null;
    }

    private void OnDestroy()
    {
      if (!Object.op_Inequality((Object) this.abilityCanvas, (Object) null))
        return;
      Object.Destroy((Object) ((Component) this.abilityCanvas).get_gameObject());
      this.abilityCanvas = (Canvas) null;
    }

    public class ConceptCardEnhanceInfo
    {
      private ConceptCardData target;
      private int add_exp;
      private int add_trust;
      private int add_awake_lv;
      private int predict_lv;
      private int predict_awake;

      public ConceptCardEnhanceInfo(ConceptCardData _concept_card, int _add_exp, int _add_trust, int _add_awake_lv)
      {
        this.target = _concept_card;
        this.add_exp = _add_exp;
        this.add_trust = _add_trust;
        this.add_awake_lv = _add_awake_lv;
        this.predict_lv = MonoSingleton<GameManager>.Instance.MasterParam.CalcConceptCardLevel((int) this.target.Rarity, (int) this.target.Exp + this.add_exp, Mathf.Min((int) this.target.LvCap, (int) this.target.CurrentLvCap + this.add_awake_lv));
        this.predict_awake = Mathf.Min(this.target.AwakeCountCap, (int) this.target.AwakeCount + this.add_awake_lv / (int) MonoSingleton<GameManager>.Instance.MasterParam.FixParam.CardAwakeUnlockLevelCap);
      }

      public ConceptCardData Target
      {
        get
        {
          return this.target;
        }
      }

      public int addExp
      {
        get
        {
          return this.add_exp;
        }
      }

      public int addTrust
      {
        get
        {
          return this.add_trust;
        }
      }

      public int addAwakeLv
      {
        get
        {
          return this.add_awake_lv;
        }
      }

      public int predictLv
      {
        get
        {
          return this.predict_lv;
        }
      }

      public int predictAwake
      {
        get
        {
          return this.predict_awake;
        }
      }

      public void Clear()
      {
        this.target = (ConceptCardData) null;
        this.add_exp = 0;
        this.add_trust = 0;
        this.add_awake_lv = 0;
        this.predict_lv = 0;
      }
    }
  }
}
