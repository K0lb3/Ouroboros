// Decompiled with JetBrains decompiler
// Type: SRPG.NewsWindow
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 85BFDF7F-5712-4D45-9CD6-3465C703DFDF
// Assembly location: S:\Desktop\Assembly-CSharp.dll

using GR;
using UnityEngine;
using UnityEngine.UI;

namespace SRPG
{
  public class NewsWindow : MonoBehaviour
  {
    public RectTransform WebViewContainer;
    public bool usegAuth;
    public SerializeValueBehaviour ValueList;
    private string[] allow_change_scenes;
    public Button CloseButton;
    public int testCounter;

    public NewsWindow()
    {
      base.\u002Ector();
    }

    private void Start()
    {
      Debug.Log((object) "[NewsWindow]Start");
      if (!MonoSingleton<DebugManager>.Instance.IsWebViewEnable())
      {
        if (Object.op_Inequality((Object) this.CloseButton, (Object) null))
          ((Selectable) this.CloseButton).set_interactable(true);
        Debug.Log((object) "[NewsWindow]Not WebView Enable");
      }
      else
      {
        Debug.Log((object) "[NewsWindow]WebView Enable");
        if (!Object.op_Inequality((Object) this.CloseButton, (Object) null))
          return;
        ((Selectable) this.CloseButton).set_interactable(true);
      }
    }

    private void StartSceneChange(string new_scene)
    {
      foreach (string allowChangeScene in this.allow_change_scenes)
      {
        if (allowChangeScene == new_scene)
        {
          GameObject gameObject = GameObject.Find("Config_Home(Clone)");
          if (Object.op_Inequality((Object) gameObject, (Object) null))
            Object.Destroy((Object) gameObject);
          Object.Destroy((Object) ((Component) this).get_gameObject());
          GlobalEvent.Invoke(new_scene, (object) this);
          break;
        }
      }
    }
  }
}
