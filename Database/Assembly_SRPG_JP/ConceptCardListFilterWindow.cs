// Decompiled with JetBrains decompiler
// Type: SRPG.ConceptCardListFilterWindow
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 85BFDF7F-5712-4D45-9CD6-3465C703DFDF
// Assembly location: S:\Desktop\Assembly-CSharp.dll

using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace SRPG
{
  [FlowNode.Pin(0, "Save", FlowNode.PinTypes.Input, 0)]
  [FlowNode.Pin(11, "キャンセル完了", FlowNode.PinTypes.Output, 11)]
  [FlowNode.Pin(3, "キャンセル", FlowNode.PinTypes.Input, 3)]
  [FlowNode.Pin(10, "Save完了", FlowNode.PinTypes.Output, 10)]
  [FlowNode.Pin(2, "全選択解除", FlowNode.PinTypes.Input, 2)]
  [FlowNode.Pin(1, "全選択", FlowNode.PinTypes.Input, 1)]
  public class ConceptCardListFilterWindow : MonoBehaviour, IFlowInterface
  {
    private const string SAVE_KEY = "CARD_FILTERT";
    private const string SAVE_CARDTYPE_KEY = "CARD_CARDTYPE_FILTERT";
    public const int PIN_SAVE = 0;
    public const int PIN_ALL_SELECT = 1;
    public const int PIN_ALL_CLEAR_SELECT = 2;
    public const int PIN_CANCEL = 3;
    public const int PIN_SAVE_END = 10;
    public const int PIN_CANCEL_END = 11;
    public const ConceptCardListFilterWindow.Type MASK_RARITY = ConceptCardListFilterWindow.Type.RARITY_1 | ConceptCardListFilterWindow.Type.RARITY_2 | ConceptCardListFilterWindow.Type.RARITY_3 | ConceptCardListFilterWindow.Type.RARITY_4 | ConceptCardListFilterWindow.Type.RARITY_5;
    public ConceptCardListFilterWindow.Item[] FilterToggles;
    public ConceptCardListFilterWindow.Type CurrentType;
    public ConceptCardListFilterWindow.Type FirstType;

    public ConceptCardListFilterWindow()
    {
      base.\u002Ector();
    }

    public void Start()
    {
      for (int index = 0; index < this.FilterToggles.Length; ++index)
      {
        // ISSUE: method pointer
        ((UnityEvent<bool>) this.FilterToggles[index].toggle.onValueChanged).AddListener(new UnityAction<bool>((object) this, __methodptr(OnSelect)));
      }
      this.Load();
    }

    public void Activated(int pinID)
    {
      switch (pinID)
      {
        case 0:
          ConceptCardManager instance = ConceptCardManager.Instance;
          if (UnityEngine.Object.op_Inequality((UnityEngine.Object) instance, (UnityEngine.Object) null) && instance.IsEnhanceListActive)
            instance.ToggleSameSelectCard = false;
          this.Save();
          FlowNode_GameObject.ActivateOutputLinks((Component) this, 10);
          break;
        case 1:
          this.OnSelectAll(true);
          break;
        case 2:
          this.OnSelectAll(false);
          break;
        case 3:
          this.ResetType();
          FlowNode_GameObject.ActivateOutputLinks((Component) this, 11);
          break;
      }
    }

    public void OnSelect(bool is_on)
    {
      this.CurrentType = ConceptCardListFilterWindow.Type.NONE;
      for (int index = 0; index < this.FilterToggles.Length; ++index)
      {
        ConceptCardListFilterWindow.Item filterToggle = this.FilterToggles[index];
        if (filterToggle.toggle.get_isOn())
          this.CurrentType |= filterToggle.type;
      }
      this.SetType();
    }

    public void OnSelectAll(bool is_on)
    {
      this.CurrentType = ConceptCardListFilterWindow.Type.NONE;
      for (int index = 0; index < this.FilterToggles.Length; ++index)
      {
        ConceptCardListFilterWindow.Item filterToggle = this.FilterToggles[index];
        GameUtility.SetToggle(filterToggle.toggle, is_on);
        if (is_on)
          this.CurrentType |= filterToggle.type;
      }
      this.SetType();
    }

    public void OnSelectType()
    {
      for (int index = 0; index < this.FilterToggles.Length; ++index)
      {
        ConceptCardListFilterWindow.Item filterToggle = this.FilterToggles[index];
        GameUtility.SetToggle(filterToggle.toggle, (this.CurrentType & filterToggle.type) > ConceptCardListFilterWindow.Type.NONE);
      }
      this.SetType();
    }

    public void ResetType()
    {
      this.CurrentType = this.FirstType;
      this.SetType();
    }

    public void SetType()
    {
      ConceptCardManager instance = ConceptCardManager.Instance;
      if (UnityEngine.Object.op_Equality((UnityEngine.Object) instance, (UnityEngine.Object) null))
        return;
      instance.FilterType = this.CurrentType;
    }

    public void Save()
    {
      PlayerPrefsUtility.SetString("CARD_FILTERT", ((int) this.CurrentType).ToString(), true);
    }

    public void Load()
    {
      this.CurrentType = ConceptCardListFilterWindow.LoadData();
      this.FirstType = this.CurrentType;
      this.OnSelectType();
    }

    public static ConceptCardListFilterWindow.Type LoadData()
    {
      if (!PlayerPrefsUtility.HasKey("CARD_FILTERT"))
        return ConceptCardListFilterWindow.Type.RARITY_1 | ConceptCardListFilterWindow.Type.RARITY_2 | ConceptCardListFilterWindow.Type.RARITY_3 | ConceptCardListFilterWindow.Type.RARITY_4 | ConceptCardListFilterWindow.Type.RARITY_5;
      string s = PlayerPrefsUtility.GetString("CARD_FILTERT", string.Empty);
      if (string.IsNullOrEmpty(s))
        return ConceptCardListFilterWindow.Type.RARITY_1 | ConceptCardListFilterWindow.Type.RARITY_2 | ConceptCardListFilterWindow.Type.RARITY_3 | ConceptCardListFilterWindow.Type.RARITY_4 | ConceptCardListFilterWindow.Type.RARITY_5;
      int result = 0;
      if (!int.TryParse(s, out result))
        return ConceptCardListFilterWindow.Type.RARITY_1 | ConceptCardListFilterWindow.Type.RARITY_2 | ConceptCardListFilterWindow.Type.RARITY_3 | ConceptCardListFilterWindow.Type.RARITY_4 | ConceptCardListFilterWindow.Type.RARITY_5;
      return (ConceptCardListFilterWindow.Type) result;
    }

    public enum Type
    {
      NONE = 0,
      RARITY_1 = 1,
      RARITY_2 = 2,
      RARITY_3 = 4,
      RARITY_4 = 8,
      RARITY_5 = 16, // 0x00000010
    }

    [Serializable]
    public class Item
    {
      public Toggle toggle;
      public ConceptCardListFilterWindow.Type type;
    }

    [Serializable]
    public class FilterItem_CardType
    {
      public Toggle toggle;
    }
  }
}
