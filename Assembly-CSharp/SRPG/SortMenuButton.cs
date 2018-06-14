// Decompiled with JetBrains decompiler
// Type: SRPG.SortMenuButton
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace SRPG
{
  public class SortMenuButton : SRPG_Button
  {
    public bool CreateMenuInstance = true;
    public GameObject Target;
    public SortMenu Menu;
    public Text Caption;
    private SortMenu mMenu;
    private GameObject mMenuObject;
    public string MenuID;
    public string FilterActive;

    public void OpenSortMenu()
    {
      this.mMenu.Open();
    }

    private void OnSortChange(SortMenu menu)
    {
      string sortMethod = menu.SortMethod;
      if (!string.IsNullOrEmpty(this.MenuID))
      {
        PlayerPrefsUtility.SetString(this.MenuID, sortMethod, false);
        PlayerPrefsUtility.SetInt(this.MenuID + "#", !this.mMenu.IsAscending ? 0 : 1, false);
        string[] filters = this.mMenu.GetFilters(true);
        PlayerPrefsUtility.SetString(this.MenuID + "&", filters == null ? string.Empty : string.Join("|", filters), false);
        PlayerPrefsUtility.Save();
      }
      if (Object.op_Inequality((Object) this.Caption, (Object) null))
        this.Caption.set_text(this.mMenu.CurrentCaption);
      this.UpdateTarget(sortMethod, menu.IsAscending);
    }

    protected virtual void Awake()
    {
      ((Selectable) this).Awake();
      if (!Application.get_isPlaying())
        return;
      if (Object.op_Inequality((Object) this.Menu, (Object) null))
      {
        if (this.CreateMenuInstance)
        {
          this.mMenu = (SortMenu) Object.Instantiate<SortMenu>((M0) this.Menu);
          this.mMenu.OnAccept = new SortMenu.SortMenuEvent(this.OnSortChange);
        }
        else
        {
          this.mMenu = this.Menu;
          this.mMenu.OnAccept = new SortMenu.SortMenuEvent(this.OnSortChange);
        }
      }
      // ISSUE: method pointer
      ((UnityEvent) this.get_onClick()).AddListener(new UnityAction((object) this, __methodptr(OpenSortMenu)));
    }

    protected virtual void Start()
    {
      ((UIBehaviour) this).Start();
      if (!Application.get_isPlaying() || !Object.op_Inequality((Object) this.mMenu, (Object) null))
        return;
      string method = (string) null;
      if (!string.IsNullOrEmpty(this.MenuID))
      {
        string str = !PlayerPrefsUtility.HasKey(this.MenuID) ? this.mMenu.SortMethod : PlayerPrefsUtility.GetString(this.MenuID, string.Empty);
        method = PlayerPrefsUtility.GetString(this.MenuID, string.Empty);
        this.mMenu.IsAscending = PlayerPrefsUtility.GetInt(this.MenuID + "#", 0) != 0;
        if (this.mMenu.Contains(method))
          this.mMenu.SortMethod = method;
        string key = this.MenuID + "&";
        if (PlayerPrefsUtility.HasKey(key))
          this.mMenu.SetFilters(PlayerPrefsUtility.GetString(key, string.Empty).Split('|'), true);
      }
      this.mMenu.SaveState();
      if (Object.op_Inequality((Object) this.Caption, (Object) null))
        this.Caption.set_text(this.mMenu.CurrentCaption);
      this.UpdateTarget(method, this.mMenu.IsAscending);
    }

    protected virtual void UpdateFilterState(bool active)
    {
      if (string.IsNullOrEmpty(this.FilterActive))
        return;
      Animator component = (Animator) ((Component) this).GetComponent<Animator>();
      if (!Object.op_Inequality((Object) component, (Object) null))
        return;
      component.SetBool(this.FilterActive, active);
    }

    protected virtual void OnEnable()
    {
      ((Selectable) this).OnEnable();
      if (!Object.op_Inequality((Object) this.mMenu, (Object) null))
        return;
      this.UpdateFilterState(this.mMenu.GetFilters(false) != null);
    }

    private void UpdateTarget(string method, bool ascending)
    {
      if (Object.op_Equality((Object) this.mMenu, (Object) null))
        return;
      string[] filters = this.mMenu.GetFilters(false);
      this.UpdateFilterState(filters != null);
      if (!Object.op_Inequality((Object) this.Target, (Object) null))
        return;
      ISortableList component = (ISortableList) this.Target.GetComponent<ISortableList>();
      if (component == null)
        return;
      component.SetSortMethod(method, ascending, filters);
    }

    protected virtual void OnDestroy()
    {
      if (Object.op_Inequality((Object) this.mMenu, (Object) null))
      {
        Object.Destroy((Object) ((Component) this.mMenu).get_gameObject());
        this.mMenu = (SortMenu) null;
      }
      ((UIBehaviour) this).OnDestroy();
    }

    public void ForceReloadFilter()
    {
      if (!Application.get_isPlaying() || !Object.op_Inequality((Object) this.mMenu, (Object) null))
        return;
      string str = (string) null;
      if (!string.IsNullOrEmpty(this.MenuID))
      {
        str = !PlayerPrefsUtility.HasKey(this.MenuID) ? this.mMenu.SortMethod : PlayerPrefsUtility.GetString(this.MenuID, string.Empty);
        string method = PlayerPrefsUtility.GetString(this.MenuID, string.Empty);
        this.mMenu.IsAscending = PlayerPrefsUtility.GetInt(this.MenuID + "#", 0) != 0;
        if (this.mMenu.Contains(method))
          this.mMenu.SortMethod = method;
        string key = this.MenuID + "&";
        if (PlayerPrefsUtility.HasKey(key))
          this.mMenu.SetFilters(PlayerPrefsUtility.GetString(key, string.Empty).Split('|'), true);
      }
      this.mMenu.SaveState();
      if (!Object.op_Inequality((Object) this.Caption, (Object) null))
        return;
      this.Caption.set_text(this.mMenu.CurrentCaption);
    }
  }
}
