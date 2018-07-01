// Decompiled with JetBrains decompiler
// Type: SRPG.WebView
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 85BFDF7F-5712-4D45-9CD6-3465C703DFDF
// Assembly location: S:\Desktop\Assembly-CSharp.dll

using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace SRPG
{
  public class WebView : MonoBehaviour
  {
    public Text Text_Title;
    public Button Btn_Close;
    public RawImage ClientArea;
    public UIUtility.DialogResultEvent OnClose;

    public WebView()
    {
      base.\u002Ector();
    }

    public void BeginClose()
    {
      Object.DestroyImmediate((Object) ((Component) this).get_gameObject());
    }

    private void OnClickButton(GameObject obj)
    {
      if (Object.op_Equality((Object) obj, (Object) ((Component) this.Btn_Close).get_gameObject()) && Object.op_Inequality((Object) this.Btn_Close, (Object) null))
        this.OnClose(((Component) this).get_gameObject());
      this.BeginClose();
    }

    public void SetTitleText(string text)
    {
      if (!Object.op_Inequality((Object) this.Text_Title, (Object) null))
        return;
      this.Text_Title.set_text(text);
    }

    private void Awake()
    {
      if (!Object.op_Inequality((Object) this.Btn_Close, (Object) null))
        return;
      UIUtility.AddEventListener(((Component) this.Btn_Close).get_gameObject(), (UnityEvent) this.Btn_Close.get_onClick(), new UIUtility.EventListener(this.OnClickButton));
    }

    public void SetHeaderField(string key, string value)
    {
    }

    public void OpenURL(string url)
    {
    }
  }
}
