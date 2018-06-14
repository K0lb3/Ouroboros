// Decompiled with JetBrains decompiler
// Type: SRPG.UnitPickerSort
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace SRPG
{
  [FlowNode.Pin(10, "Open", FlowNode.PinTypes.Output, 10)]
  public class UnitPickerSort : MonoBehaviour, IFlowInterface
  {
    public string MenuID;
    public bool LocalizeCaption;
    public string DefaultCaption;
    public bool UseFilterCaption;
    public bool mSelectedAscending;
    public Toggle Ascending;
    public Toggle Descending;
    public SortMenu.SortMenuItem[] Items;
    public SRPG_Button DecideButton;
    public UnitPickerSort.SortEvent OnAccept;

    public UnitPickerSort()
    {
      base.\u002Ector();
    }

    public void Activated(int pinID)
    {
    }

    public void Open()
    {
      this.RestoreState();
      FlowNode_GameObject.ActivateOutputLinks((Component) this, 10);
    }

    private void Start()
    {
      if (!Object.op_Inequality((Object) this.DecideButton, (Object) null))
        return;
      // ISSUE: method pointer
      ((UnityEvent) this.DecideButton.get_onClick()).AddListener(new UnityAction((object) this, __methodptr(Accept)));
    }

    private void Accept()
    {
      this.SaveState();
      PlayerPrefsUtility.SetString(this.MenuID, this.SortMethod, false);
      PlayerPrefsUtility.SetInt(this.MenuID + "#", !this.IsAscending ? 0 : 1, false);
      PlayerPrefsUtility.Save();
      if (this.OnAccept == null)
        return;
      this.OnAccept(this.SortMethod, this.IsAscending);
    }

    public void SetUp(string method)
    {
      if (PlayerPrefsUtility.HasKey(this.MenuID))
        this.SortMethod = PlayerPrefsUtility.GetString(this.MenuID, "TIME");
      if (PlayerPrefsUtility.HasKey(this.MenuID + "#"))
        this.IsAscending = PlayerPrefsUtility.GetInt(this.MenuID + "#", 0) != 0;
      this.SaveState();
    }

    public void SetUp(string method, bool ascending = false)
    {
      this.SortMethod = string.IsNullOrEmpty(method) ? "TIME" : method;
      this.IsAscending = ascending;
      this.SaveState();
    }

    public void SaveState()
    {
      this.mSelectedAscending = this.IsAscending;
      for (int index = 0; index < this.Items.Length; ++index)
      {
        if (Object.op_Inequality((Object) this.Items[index].Toggle, (Object) null))
          this.Items[index].LastState = this.Items[index].Toggle.get_isOn();
      }
    }

    public void RestoreState()
    {
      this.IsAscending = this.mSelectedAscending;
      for (int index = 0; index < this.Items.Length; ++index)
      {
        if (Object.op_Inequality((Object) this.Items[index].Toggle, (Object) null))
          GameUtility.SetToggle(this.Items[index].Toggle, this.Items[index].LastState);
      }
    }

    public string CurrentCaption
    {
      get
      {
        for (int index = 0; index < this.Items.Length; ++index)
        {
          if (Object.op_Inequality((Object) this.Items[index].Toggle, (Object) null) && this.Items[index].Toggle.get_isOn())
          {
            if (this.LocalizeCaption)
              return LocalizedText.Get(this.Items[index].Caption);
            return this.Items[index].Caption;
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
        if (this.Items != null && this.Items.Length > 0)
        {
          for (int index = 0; index < this.Items.Length; ++index)
          {
            if (Object.op_Inequality((Object) this.Items[index].Toggle, (Object) null) && this.Items[index].Toggle.get_isOn())
              return this.Items[index].Method;
          }
        }
        return (string) null;
      }
      set
      {
        if (this.Items == null || this.Items.Length <= 0)
          return;
        for (int index = 0; index < this.Items.Length; ++index)
        {
          if (Object.op_Inequality((Object) this.Items[index].Toggle, (Object) null))
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
        if (Object.op_Inequality((Object) this.Ascending, (Object) null))
          return !this.Ascending.get_isOn();
        return false;
      }
      set
      {
        if (Object.op_Inequality((Object) this.Ascending, (Object) null))
          GameUtility.SetToggle(this.Ascending, !value);
        if (!Object.op_Inequality((Object) this.Descending, (Object) null))
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

    public string GetSortMethod()
    {
      string empty = string.Empty;
      string lower = this.SortMethod.ToLower();
      return ((int) char.ToUpper(lower[0])).ToString() + lower.Substring(1);
    }

    public delegate void SortEvent(string method, bool ascending);
  }
}
