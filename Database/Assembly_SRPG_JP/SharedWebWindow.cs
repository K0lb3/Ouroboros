// Decompiled with JetBrains decompiler
// Type: SRPG.SharedWebWindow
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 85BFDF7F-5712-4D45-9CD6-3465C703DFDF
// Assembly location: S:\Desktop\Assembly-CSharp.dll

using Gsc.App.NetworkHelper;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace SRPG
{
  public class SharedWebWindow : MonoBehaviour
  {
    [SerializeField]
    private WebView Target;
    [SerializeField]
    private GameObject Caution;
    [SerializeField]
    private bool usegAuth;

    public SharedWebWindow()
    {
      base.\u002Ector();
    }

    private void Awake()
    {
      if (UnityEngine.Object.op_Equality((UnityEngine.Object) this.Target, (UnityEngine.Object) null))
      {
        Transform child = ((Component) this).get_transform().FindChild("window");
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) child, (UnityEngine.Object) null))
        {
          WebView component = (WebView) ((Component) child).GetComponent<WebView>();
          if (UnityEngine.Object.op_Inequality((UnityEngine.Object) component, (UnityEngine.Object) null))
            this.Target = component;
        }
      }
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.Caution, (UnityEngine.Object) null))
      {
        this.Caution.SetActive(false);
      }
      else
      {
        if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) this.Target, (UnityEngine.Object) null))
          return;
        Transform child = ((Component) this.Target).get_transform().FindChild("caution");
        if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) child, (UnityEngine.Object) null))
          return;
        this.Caution = ((Component) child).get_gameObject();
        this.Caution.SetActive(false);
      }
    }

    private void Start()
    {
      if (UnityEngine.Object.op_Equality((UnityEngine.Object) this.Target, (UnityEngine.Object) null))
        return;
      string text = FlowNode_Variable.Get("SHARED_WEBWINDOW_TITLE");
      if (!string.IsNullOrEmpty(text))
        this.Target.SetTitleText(text);
      string url = FlowNode_Variable.Get("SHARED_WEBWINDOW_URL");
      if (!string.IsNullOrEmpty(url))
      {
        if (this.usegAuth)
        {
          Dictionary<string, string> dictionary = new Dictionary<string, string>();
          GsccBridge.SetWebViewHeaders(new Action<string, string>(dictionary.Add));
          using (Dictionary<string, string>.Enumerator enumerator = dictionary.GetEnumerator())
          {
            while (enumerator.MoveNext())
            {
              KeyValuePair<string, string> current = enumerator.Current;
              this.Target.SetHeaderField(current.Key, current.Value);
            }
          }
        }
        this.Target.OpenURL(url);
        FlowNode_Variable.Set("SHARED_WEBWINDOW_URL", string.Empty);
      }
      else
        this.Caution.SetActive(true);
    }
  }
}
