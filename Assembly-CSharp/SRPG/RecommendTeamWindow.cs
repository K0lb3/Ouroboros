// Decompiled with JetBrains decompiler
// Type: SRPG.RecommendTeamWindow
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using System;
using UnityEngine;

namespace SRPG
{
  public class RecommendTeamWindow : MonoBehaviour
  {
    [SerializeField]
    private ScrollablePulldown TypePullDown;
    [SerializeField]
    private ElementDropdown ElemmentPullDown;
    private readonly RecommendTeamWindow.TypeAndStr[] items;
    private readonly RecommendTeamWindow.ElemAndStr[] elements;
    private int currentTypeIndex;
    private int currentElemmentIndex;

    public RecommendTeamWindow()
    {
      base.\u002Ector();
    }

    private void Awake()
    {
      this.TypePullDown.OnSelectionChangeDelegate = new ScrollablePulldownBase.SelectItemEvent(this.OnTypeItemSelect);
      this.ElemmentPullDown.OnSelectionChangeDelegate = new Pulldown.SelectItemEvent(this.OnElemmentItemSelect);
      if (GlobalVars.RecommendTeamSettingValue != null)
      {
        this.currentTypeIndex = PartyUtility.RecommendTypeToComparatorOrder(GlobalVars.RecommendTeamSettingValue.recommendedType);
        this.currentElemmentIndex = (int) GlobalVars.RecommendTeamSettingValue.recommendedElement;
      }
      else
      {
        this.currentTypeIndex = 0;
        this.currentElemmentIndex = 0;
      }
      this.Refresh();
    }

    private void OnTypeItemSelect(int value)
    {
      if (value < 0 || value >= this.items.Length || value == this.currentTypeIndex)
        return;
      this.currentTypeIndex = value;
    }

    private void OnElemmentItemSelect(int value)
    {
      if (value == this.currentElemmentIndex)
        return;
      this.currentElemmentIndex = value;
    }

    private void Refresh()
    {
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.TypePullDown, (UnityEngine.Object) null))
      {
        this.TypePullDown.ClearItems();
        for (int index = 0; index < this.items.Length; ++index)
          this.TypePullDown.AddItem(LocalizedText.Get(this.items[index].title), index);
        this.TypePullDown.Selection = this.currentTypeIndex;
        ((Component) this.TypePullDown).get_gameObject().SetActive(true);
      }
      if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) this.ElemmentPullDown, (UnityEngine.Object) null))
        return;
      this.ElemmentPullDown.ClearItems();
      GameSettings instance = GameSettings.Instance;
      for (int index = 0; index < this.elements.Length; ++index)
      {
        Sprite sprite = (Sprite) null;
        if (index < instance.Elements_IconSmall.Length && index != 0)
          sprite = instance.Elements_IconSmall[(int) this.elements[index].element];
        this.ElemmentPullDown.AddItem(LocalizedText.Get(this.elements[index].title), sprite, index);
      }
      this.ElemmentPullDown.Selection = this.currentElemmentIndex;
      ((Component) this.ElemmentPullDown).get_gameObject().SetActive(true);
    }

    public void SaveSettings()
    {
      GlobalVars.RecommendType type = this.items[this.currentTypeIndex].type;
      EElement element = this.elements[this.currentElemmentIndex].element;
      if (Enum.IsDefined(typeof (GlobalVars.RecommendType), (object) type) && Enum.IsDefined(typeof (EElement), (object) element))
        GlobalVars.RecommendTeamSettingValue = new GlobalVars.RecommendTeamSetting(type, element);
      else
        GlobalVars.RecommendTeamSettingValue = (GlobalVars.RecommendTeamSetting) null;
    }

    public void Cancel()
    {
      GlobalVars.RecommendTeamSettingValue = (GlobalVars.RecommendTeamSetting) null;
    }

    private struct TypeAndStr
    {
      public readonly GlobalVars.RecommendType type;
      public readonly string title;

      public TypeAndStr(GlobalVars.RecommendType type, string title)
      {
        this.type = type;
        this.title = title;
      }
    }

    private struct ElemAndStr
    {
      public readonly EElement element;
      public readonly string title;

      public ElemAndStr(EElement element, string title)
      {
        this.element = element;
        this.title = title;
      }
    }
  }
}
