// Decompiled with JetBrains decompiler
// Type: SRPG.ConceptCardListSortWindow
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 85BFDF7F-5712-4D45-9CD6-3465C703DFDF
// Assembly location: S:\Desktop\Assembly-CSharp.dll

using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace SRPG
{
  [FlowNode.Pin(1, "キャンセル", FlowNode.PinTypes.Input, 1)]
  [FlowNode.Pin(20, "キャンセル完了", FlowNode.PinTypes.Output, 20)]
  [FlowNode.Pin(10, "セーブ完了", FlowNode.PinTypes.Output, 10)]
  [FlowNode.Pin(0, "セーブ", FlowNode.PinTypes.Input, 0)]
  public class ConceptCardListSortWindow : MonoBehaviour, IFlowInterface
  {
    public const string SAVE_KEY = "CARD_FILTER";
    public const ConceptCardListSortWindow.Type CLEAR_TYPE = (ConceptCardListSortWindow.Type) 251658240;
    public const ConceptCardListSortWindow.Type CLEAR_ORDER_TYPE = (ConceptCardListSortWindow.Type) 16777215;
    public const ConceptCardListSortWindow.Type MASK_TYPE = (ConceptCardListSortWindow.Type) 16777215;
    public const ConceptCardListSortWindow.Type MASK_ORDER_TYPE = (ConceptCardListSortWindow.Type) 251658240;
    public const int PIN_SAVE = 0;
    public const int PIN_CANCEL = 1;
    public const int PIN_SAVE_END = 10;
    public const int PIN_CANCEL_END = 20;
    public const ConceptCardListSortWindow.Type DefaultType = ConceptCardListSortWindow.Type.TIME | ConceptCardListSortWindow.Type.ASCENDING;
    [SerializeField]
    private ConceptCardListSortWindow.ParentType parent_type;
    [SerializeField]
    private ConceptCardListSortWindow.Item[] SortToggles;
    [SerializeField]
    private ConceptCardListSortWindow.Item[] SortOrderToggles;
    [SerializeField]
    private ToggleGroup Group;
    [SerializeField]
    private ToggleGroup OrderGroup;
    private ConceptCardListSortWindow.Type CurrentType;
    private ConceptCardListSortWindow.Type FirstType;

    public ConceptCardListSortWindow()
    {
      base.\u002Ector();
    }

    public void Start()
    {
      if (this.SortToggles == null)
        return;
      for (int index = 0; index < this.SortToggles.Length; ++index)
      {
        // ISSUE: method pointer
        ((UnityEvent<bool>) this.SortToggles[index].toggle.onValueChanged).AddListener(new UnityAction<bool>((object) this, __methodptr(OnSelect)));
      }
      for (int index = 0; index < this.SortOrderToggles.Length; ++index)
      {
        // ISSUE: method pointer
        ((UnityEvent<bool>) this.SortOrderToggles[index].toggle.onValueChanged).AddListener(new UnityAction<bool>((object) this, __methodptr(OnSelectOrder)));
      }
      this.CurrentType = ConceptCardListSortWindow.LoadData();
      this.FirstType = this.CurrentType;
      for (int index = 0; index < this.SortToggles.Length; ++index)
        GameUtility.SetToggle(this.SortToggles[index].toggle, this.SortToggles[index].type == (this.CurrentType & (ConceptCardListSortWindow.Type) 16777215));
      for (int index = 0; index < this.SortOrderToggles.Length; ++index)
        GameUtility.SetToggle(this.SortOrderToggles[index].toggle, this.SortOrderToggles[index].type == (this.CurrentType & (ConceptCardListSortWindow.Type) 251658240));
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
          this.ResetType();
          FlowNode_GameObject.ActivateOutputLinks((Component) this, 20);
          break;
      }
    }

    public void OnSelect(bool is_on)
    {
      List<Toggle> list = this.Group.ActiveToggles().ToList<Toggle>();
      if (list.Count != 1)
        return;
      Toggle toggle = list[0];
      for (int index = 0; index < this.SortToggles.Length; ++index)
      {
        ConceptCardListSortWindow.Item sortToggle = this.SortToggles[index];
        if (UnityEngine.Object.op_Equality((UnityEngine.Object) toggle, (UnityEngine.Object) sortToggle.toggle))
          this.ChangeType(sortToggle.type);
      }
      this.SetType();
    }

    public void OnSelectOrder(bool is_on)
    {
      List<Toggle> list = this.OrderGroup.ActiveToggles().ToList<Toggle>();
      if (list.Count != 1)
        return;
      Toggle toggle = list[0];
      for (int index = 0; index < this.SortOrderToggles.Length; ++index)
      {
        ConceptCardListSortWindow.Item sortOrderToggle = this.SortOrderToggles[index];
        if (UnityEngine.Object.op_Equality((UnityEngine.Object) toggle, (UnityEngine.Object) sortOrderToggle.toggle))
          this.ChangeOrderType(sortOrderToggle.type);
      }
      this.SetType();
    }

    public static void Sort(ConceptCardListSortWindow.Type sort_type, ConceptCardListSortWindow.Type sort_order_type, List<ConceptCardData> card_list)
    {
      List<ConceptCardListSortWindow.SortData> sortDataList = new List<ConceptCardListSortWindow.SortData>();
      for (int index = 0; index < card_list.Count; ++index)
      {
        ConceptCardData card = card_list[index];
        if (card != null)
          sortDataList.Add(new ConceptCardListSortWindow.SortData(card, card.GetSortData(sort_type)));
      }
      sortDataList.Sort((Comparison<ConceptCardListSortWindow.SortData>) ((x, y) =>
      {
        if (x.sort_val != y.sort_val)
          return x.sort_val.CompareTo(y.sort_val);
        if (x.data.Param.type != y.data.Param.type)
          return y.data.Param.type.CompareTo((object) x.data.Param.type);
        if (x.data.Param.iname != y.data.Param.iname)
          return x.data.Param.iname.CompareTo(y.data.Param.iname);
        if ((int) x.data.Lv != (int) y.data.Lv)
          return (int) x.data.Lv.CompareTo((int) y.data.Lv);
        if ((int) x.data.Exp != (int) y.data.Exp)
          return (int) x.data.Exp.CompareTo((int) y.data.Exp);
        if ((long) x.data.UniqueID != (long) y.data.UniqueID)
          return (long) x.data.UniqueID.CompareTo((long) y.data.UniqueID);
        return 0;
      }));
      card_list.Clear();
      for (int index = 0; index < sortDataList.Count; ++index)
      {
        ConceptCardListSortWindow.SortData sortData = sortDataList[index];
        card_list.Add(sortData.data);
      }
      if (sort_order_type != ConceptCardListSortWindow.Type.DESCENDING)
        return;
      card_list.Reverse();
    }

    public void ResetType()
    {
      this.CurrentType = this.FirstType;
      this.SetType();
    }

    public void SetType()
    {
      switch (this.parent_type)
      {
        case ConceptCardListSortWindow.ParentType.Manager:
          ConceptCardManager instance1 = ConceptCardManager.Instance;
          if (UnityEngine.Object.op_Equality((UnityEngine.Object) instance1, (UnityEngine.Object) null))
            break;
          instance1.SortType = this.CurrentType & (ConceptCardListSortWindow.Type) 16777215;
          instance1.SortOrderType = this.CurrentType & (ConceptCardListSortWindow.Type) 251658240;
          break;
        case ConceptCardListSortWindow.ParentType.Equip:
          ConceptCardEquipWindow instance2 = ConceptCardEquipWindow.Instance;
          if (UnityEngine.Object.op_Equality((UnityEngine.Object) instance2, (UnityEngine.Object) null))
            break;
          instance2.SortType = this.CurrentType & (ConceptCardListSortWindow.Type) 16777215;
          instance2.SortOrderType = this.CurrentType & (ConceptCardListSortWindow.Type) 251658240;
          break;
      }
    }

    public void ChangeType(ConceptCardListSortWindow.Type type)
    {
      if ((type & (ConceptCardListSortWindow.Type) 251658240) > ConceptCardListSortWindow.Type.NONE)
        return;
      this.CurrentType &= (ConceptCardListSortWindow.Type) 251658240;
      this.CurrentType |= type;
    }

    public void ChangeOrderType(ConceptCardListSortWindow.Type type)
    {
      if ((type & (ConceptCardListSortWindow.Type) 16777215) > ConceptCardListSortWindow.Type.NONE)
        return;
      this.CurrentType &= (ConceptCardListSortWindow.Type) 16777215;
      this.CurrentType |= type;
    }

    public void Save()
    {
      if (string.IsNullOrEmpty("CARD_FILTER"))
        return;
      PlayerPrefsUtility.SetString("CARD_FILTER", ((int) this.CurrentType).ToString(), true);
    }

    public void Load()
    {
      this.CurrentType = ConceptCardListSortWindow.LoadData();
    }

    public static ConceptCardListSortWindow.Type LoadDataType()
    {
      return ConceptCardListSortWindow.LoadData() & (ConceptCardListSortWindow.Type) 16777215;
    }

    public static ConceptCardListSortWindow.Type LoadDataOrderType()
    {
      return ConceptCardListSortWindow.LoadData() & (ConceptCardListSortWindow.Type) 251658240;
    }

    private static ConceptCardListSortWindow.Type LoadData()
    {
      if (!PlayerPrefsUtility.HasKey("CARD_FILTER"))
        return ConceptCardListSortWindow.Type.TIME | ConceptCardListSortWindow.Type.ASCENDING;
      string s = PlayerPrefsUtility.GetString("CARD_FILTER", string.Empty);
      if (string.IsNullOrEmpty(s))
        return ConceptCardListSortWindow.Type.TIME | ConceptCardListSortWindow.Type.ASCENDING;
      int result = 0;
      if (!int.TryParse(s, out result))
        return ConceptCardListSortWindow.Type.TIME | ConceptCardListSortWindow.Type.ASCENDING;
      return (ConceptCardListSortWindow.Type) result;
    }

    public static string GetTypeString(ConceptCardListSortWindow.Type type)
    {
      ConceptCardListSortWindow.Type type1 = type;
      switch (type1)
      {
        case ConceptCardListSortWindow.Type.LEVEL:
          return "sys.SORT_LEVEL";
        case ConceptCardListSortWindow.Type.RARITY:
          return "sys.SORT_RARITY";
        case ConceptCardListSortWindow.Type.ATK:
          return "sys.SORT_ATK";
        case ConceptCardListSortWindow.Type.DEF:
          return "sys.SORT_DEF";
        default:
          if (type1 == ConceptCardListSortWindow.Type.MAG)
            return "sys.SORT_MAG";
          if (type1 == ConceptCardListSortWindow.Type.MND)
            return "sys.SORT_MND";
          if (type1 == ConceptCardListSortWindow.Type.SPD)
            return "sys.SORT_SPD";
          if (type1 == ConceptCardListSortWindow.Type.LUCK)
            return "sys.SORT_LUCK";
          if (type1 == ConceptCardListSortWindow.Type.HP)
            return "sys.SORT_HP";
          if (type1 == ConceptCardListSortWindow.Type.TIME)
            return "sys.SORT_TIME";
          if (type1 == ConceptCardListSortWindow.Type.TRUST)
            return "sys.SORT_TRUST";
          if (type1 == ConceptCardListSortWindow.Type.AWAKE)
            return "sys.SORT_AWAKE";
          return string.Empty;
      }
    }

    public enum Type
    {
      NONE = 0,
      LEVEL = 1,
      RARITY = 2,
      ATK = 4,
      DEF = 8,
      MAG = 16, // 0x00000010
      MND = 32, // 0x00000020
      SPD = 64, // 0x00000040
      LUCK = 128, // 0x00000080
      HP = 256, // 0x00000100
      TIME = 512, // 0x00000200
      TRUST = 1024, // 0x00000400
      AWAKE = 2048, // 0x00000800
      ASCENDING = 16777216, // 0x01000000
      DESCENDING = 33554432, // 0x02000000
    }

    public enum ParentType
    {
      Manager,
      Equip,
    }

    [Serializable]
    public class Item
    {
      public Toggle toggle;
      public ConceptCardListSortWindow.Type type;
    }

    public class SortData
    {
      public ConceptCardData data;
      public long sort_val;

      public SortData(ConceptCardData _data, long _sort_val)
      {
        this.data = _data;
        this.sort_val = _sort_val;
      }
    }
  }
}
