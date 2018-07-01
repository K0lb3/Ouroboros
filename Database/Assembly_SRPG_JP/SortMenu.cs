// Decompiled with JetBrains decompiler
// Type: SRPG.SortMenu
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 85BFDF7F-5712-4D45-9CD6-3465C703DFDF
// Assembly location: S:\Desktop\Assembly-CSharp.dll

using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace SRPG
{
  [FlowNode.Pin(2, "Restore State", FlowNode.PinTypes.Input, 2)]
  [FlowNode.Pin(1, "Open", FlowNode.PinTypes.Output, 1)]
  public class SortMenu : MonoBehaviour, IFlowInterface
  {
    public bool LocalizeCaption;
    public string DefaultCaption;
    public Toggle Ascending;
    public Toggle Descending;
    public SortMenu.SortMenuEvent OnAccept;
    public SortMenu.SortMenuItem[] Items;
    public SortMenu.SortMenuItem[] Filters;
    public Button ToggleFiltersOn;
    public Button ToggleFiltersOff;
    private bool mSelectedAscending;
    public bool UseFilterCaption;

    public SortMenu()
    {
      base.\u002Ector();
    }

    public void Activated(int pinID)
    {
      if (pinID != 2)
        return;
      this.RestoreState();
    }

    public void Reset()
    {
      for (int index = 0; index < this.Items.Length; ++index)
      {
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.Items[index].Toggle, (UnityEngine.Object) null))
          GameUtility.SetToggle(this.Items[index].Toggle, false);
      }
    }

    private void Start()
    {
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.ToggleFiltersOff, (UnityEngine.Object) null))
      {
        // ISSUE: method pointer
        ((UnityEvent) this.ToggleFiltersOff.get_onClick()).AddListener(new UnityAction((object) this, __methodptr(SetAllFiltersOff)));
      }
      if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) this.ToggleFiltersOn, (UnityEngine.Object) null))
        return;
      // ISSUE: method pointer
      ((UnityEvent) this.ToggleFiltersOn.get_onClick()).AddListener(new UnityAction((object) this, __methodptr(SetAllFiltersOn)));
    }

    public void SetAllFiltersOn()
    {
      for (int index = 0; index < this.Filters.Length; ++index)
      {
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.Filters[index].Toggle, (UnityEngine.Object) null))
          GameUtility.SetToggle(this.Filters[index].Toggle, true);
      }
    }

    public void SetAllFiltersOff()
    {
      for (int index = 0; index < this.Filters.Length; ++index)
      {
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.Filters[index].Toggle, (UnityEngine.Object) null))
          GameUtility.SetToggle(this.Filters[index].Toggle, false);
      }
    }

    public void Open()
    {
      this.RestoreState();
      FlowNode_GameObject.ActivateOutputLinks((Component) this, 1);
    }

    public void SaveState()
    {
      this.mSelectedAscending = this.IsAscending;
      for (int index = 0; index < this.Items.Length; ++index)
      {
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.Items[index].Toggle, (UnityEngine.Object) null))
          this.Items[index].LastState = this.Items[index].Toggle.get_isOn();
      }
      for (int index = 0; index < this.Filters.Length; ++index)
      {
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.Filters[index].Toggle, (UnityEngine.Object) null))
          this.Filters[index].LastState = this.Filters[index].Toggle.get_isOn();
      }
    }

    public void RestoreState()
    {
      this.IsAscending = this.mSelectedAscending;
      for (int index = 0; index < this.Items.Length; ++index)
      {
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.Items[index].Toggle, (UnityEngine.Object) null))
          GameUtility.SetToggle(this.Items[index].Toggle, this.Items[index].LastState);
      }
      for (int index = 0; index < this.Filters.Length; ++index)
      {
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.Filters[index].Toggle, (UnityEngine.Object) null))
          GameUtility.SetToggle(this.Filters[index].Toggle, this.Filters[index].LastState);
      }
    }

    public void Accept()
    {
      this.SaveState();
      if (this.OnAccept == null)
        return;
      this.OnAccept(this);
    }

    public string CurrentCaption
    {
      get
      {
        for (int index = 0; index < this.Items.Length; ++index)
        {
          if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.Items[index].Toggle, (UnityEngine.Object) null) && this.Items[index].Toggle.get_isOn())
          {
            if (this.LocalizeCaption)
              return LocalizedText.Get(this.Items[index].Caption);
            return this.Items[index].Caption;
          }
        }
        if (this.UseFilterCaption)
        {
          for (int index = 0; index < this.Filters.Length; ++index)
          {
            if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.Filters[index].Toggle, (UnityEngine.Object) null) && this.Filters[index].Toggle.get_isOn())
            {
              if (this.LocalizeCaption)
                return LocalizedText.Get(this.Filters[index].Caption);
              return this.Filters[index].Caption;
            }
          }
        }
        if (this.LocalizeCaption)
          return LocalizedText.Get(this.DefaultCaption);
        return this.DefaultCaption;
      }
    }

    public string SortMethod
    {
      get
      {
        for (int index = 0; index < this.Items.Length; ++index)
        {
          if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.Items[index].Toggle, (UnityEngine.Object) null) && this.Items[index].Toggle.get_isOn())
            return this.Items[index].Method;
        }
        return (string) null;
      }
      set
      {
        for (int index = 0; index < this.Items.Length; ++index)
        {
          if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.Items[index].Toggle, (UnityEngine.Object) null))
            GameUtility.SetToggle(this.Items[index].Toggle, this.Items[index].Method == value);
        }
      }
    }

    public bool IsAscending
    {
      get
      {
        return !this.IsDescending;
      }
      set
      {
        this.IsDescending = !value;
      }
    }

    public bool IsDescending
    {
      get
      {
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.Ascending, (UnityEngine.Object) null))
          return !this.Ascending.get_isOn();
        return false;
      }
      set
      {
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.Ascending, (UnityEngine.Object) null))
          GameUtility.SetToggle(this.Ascending, !value);
        if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) this.Descending, (UnityEngine.Object) null))
          return;
        GameUtility.SetToggle(this.Descending, value);
      }
    }

    public bool Contains(string method)
    {
      for (int index = 0; index < this.Items.Length; ++index)
      {
        if (this.Items[index].Method == method)
          return true;
      }
      return false;
    }

    public string[] GetFilters(bool invert = false)
    {
      List<string> stringList = new List<string>();
      if (invert)
      {
        for (int index = 0; index < this.Filters.Length; ++index)
        {
          if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.Filters[index].Toggle, (UnityEngine.Object) null) && !this.Filters[index].Toggle.get_isOn())
            stringList.Add(this.Filters[index].Method);
        }
        if (stringList.Count == 0)
          return (string[]) null;
      }
      else
      {
        for (int index = 0; index < this.Filters.Length; ++index)
        {
          if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.Filters[index].Toggle, (UnityEngine.Object) null) && this.Filters[index].Toggle.get_isOn())
            stringList.Add(this.Filters[index].Method);
        }
        if (this.Filters.Length == stringList.Count)
          return (string[]) null;
      }
      return stringList.ToArray();
    }

    public void SetFilters(string[] filters, bool invert = false)
    {
      if (filters == null || filters.Length == 0)
      {
        if (invert)
          this.SetAllFiltersOn();
        else
          this.SetAllFiltersOff();
      }
      else
      {
        for (int index = 0; index < this.Filters.Length; ++index)
        {
          if (!string.IsNullOrEmpty(this.Filters[index].Method) && UnityEngine.Object.op_Inequality((UnityEngine.Object) this.Filters[index].Toggle, (UnityEngine.Object) null))
          {
            bool flag = Array.IndexOf<string>(filters, this.Filters[index].Method) >= 0;
            if (invert)
              flag = !flag;
            GameUtility.SetToggle(this.Filters[index].Toggle, flag);
          }
        }
      }
    }

    [Serializable]
    public struct SortMenuItem
    {
      public string Method;
      public Toggle Toggle;
      public string Caption;
      [NonSerialized]
      public bool LastState;
    }

    public delegate void SortMenuEvent(SortMenu menu);
  }
}
