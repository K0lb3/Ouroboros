// Decompiled with JetBrains decompiler
// Type: SRPG.WebHelpButton
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 85BFDF7F-5712-4D45-9CD6-3465C703DFDF
// Assembly location: S:\Desktop\Assembly-CSharp.dll

using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace SRPG
{
  public class WebHelpButton : SRPG_Button
  {
    public bool usegAuth = true;
    public GameObject Target;
    [StringIsResourcePath(typeof (GameObject))]
    public string PrefabPath;
    private IWebHelp mInterface;

    protected virtual void Start()
    {
      ((UIBehaviour) this).Start();
      if (Object.op_Inequality((Object) this.Target, (Object) null))
        this.mInterface = (IWebHelp) this.Target.GetComponentInChildren<IWebHelp>(true);
      // ISSUE: method pointer
      ((UnityEvent) this.get_onClick()).AddListener(new UnityAction((object) this, __methodptr(ShowWebHelp)));
    }

    protected virtual void OnEnable()
    {
      ((Selectable) this).OnEnable();
      this.Update();
    }

    private void Update()
    {
      if (this.mInterface == null)
        return;
      string url;
      string title;
      ((Selectable) this).set_interactable(this.mInterface.GetHelpURL(out url, out title));
    }

    private void ShowWebHelp()
    {
      string url;
      string title;
      if (!((Selectable) this).IsInteractable() || !this.mInterface.GetHelpURL(out url, out title))
        return;
      url = GameSettings.Instance.WebHelp_URLMode.ComposeURL(url);
      Application.OpenURL(url);
    }
  }
}
