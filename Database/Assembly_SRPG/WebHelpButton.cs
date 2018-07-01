// Decompiled with JetBrains decompiler
// Type: SRPG.WebHelpButton
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace SRPG
{
  public class WebHelpButton : SRPG_Button
  {
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
      string name = this.PrefabPath;
      if (string.IsNullOrEmpty(name))
        name = GameSettings.Instance.WebHelp_PrefabPath;
      if (string.IsNullOrEmpty(name))
        return;
      GameObject gameObject = AssetManager.Load<GameObject>(name);
      if (Object.op_Equality((Object) gameObject, (Object) null))
        return;
      WebView component = (WebView) ((GameObject) Object.Instantiate<GameObject>((M0) gameObject)).GetComponent<WebView>();
      if (!Object.op_Inequality((Object) component, (Object) null))
        return;
      if (!string.IsNullOrEmpty(title))
        component.SetTitleText(title);
      url = GameSettings.Instance.WebHelp_URLMode.ComposeURL(url);
      component.OnClose = (UIUtility.DialogResultEvent) (g => {});
      component.OpenURL(url);
    }
  }
}
