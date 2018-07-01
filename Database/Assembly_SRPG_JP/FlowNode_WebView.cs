// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_WebView
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 85BFDF7F-5712-4D45-9CD6-3465C703DFDF
// Assembly location: S:\Desktop\Assembly-CSharp.dll

using GR;
using Gsc.App.NetworkHelper;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace SRPG
{
  [FlowNode.NodeType("System/WebView", 32741)]
  [AddComponentMenu("")]
  [FlowNode.Pin(102, "Preload", FlowNode.PinTypes.Input, 2)]
  [FlowNode.Pin(101, "Destroy", FlowNode.PinTypes.Input, 1)]
  [FlowNode.Pin(1, "Created", FlowNode.PinTypes.Output, 10)]
  [FlowNode.Pin(2, "Destroyed", FlowNode.PinTypes.Output, 11)]
  [FlowNode.Pin(100, "Create", FlowNode.PinTypes.Input, 0)]
  public class FlowNode_WebView : FlowNode
  {
    public bool usegAuth = true;
    private const int PIN_ID_CREATE = 100;
    private const int PIN_ID_DESTROY = 101;
    private const int PIN_ID_PRELOAD = 102;
    private const int PIN_ID_CREATED = 1;
    private const int PIN_ID_DESTROYED = 2;
    private const string PREFAB_PATH = "UI/WebView";
    public string Title;
    public string URL;
    public bool useVariable;
    private WebView webView;
    public FlowNode_WebView.URL_Mode URLMode;

    private void Create()
    {
      GameObject gameObject = AssetManager.Load<GameObject>("UI/WebView");
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) gameObject, (UnityEngine.Object) null))
      {
        this.webView = (WebView) ((GameObject) UnityEngine.Object.Instantiate<GameObject>((M0) gameObject)).GetComponent<WebView>();
        if (this.useVariable)
        {
          string str1 = FlowNode_Variable.Get(this.URL);
          if (!string.IsNullOrEmpty(str1))
            this.URL = str1;
          string str2 = FlowNode_Variable.Get(this.Title);
          if (!string.IsNullOrEmpty(str2))
            this.Title = str2;
        }
        this.webView.OnClose = new UIUtility.DialogResultEvent(this.OnClose);
        this.webView.Text_Title.set_text(LocalizedText.Get(this.Title));
        if (this.usegAuth)
        {
          Dictionary<string, string> dictionary = new Dictionary<string, string>();
          GsccBridge.SetWebViewHeaders(new Action<string, string>(dictionary.Add));
          using (Dictionary<string, string>.Enumerator enumerator = dictionary.GetEnumerator())
          {
            while (enumerator.MoveNext())
            {
              KeyValuePair<string, string> current = enumerator.Current;
              this.webView.SetHeaderField(current.Key, current.Value);
            }
          }
        }
        if (this.URL.StartsWith("http://") || this.URL.StartsWith("https://"))
        {
          MonoSingleton<GameManager>.Instance.Player.UpdateViewNewsTrophy(this.URL);
          this.webView.OpenURL(this.URL);
        }
        else if (this.URLMode == FlowNode_WebView.URL_Mode.APIHost)
          this.webView.OpenURL(Network.Host + this.URL);
        else if (this.URLMode == FlowNode_WebView.URL_Mode.SiteHost)
        {
          this.webView.OpenURL(Network.SiteHost + this.URL);
        }
        else
        {
          if (this.URLMode != FlowNode_WebView.URL_Mode.NewsHost)
            return;
          MonoSingleton<GameManager>.Instance.Player.UpdateViewNewsTrophy(Network.NewsHost + this.URL);
          this.webView.OpenURL(Network.NewsHost + this.URL);
        }
      }
      else
        Debug.Log((object) "Prefab not Found");
    }

    public void OnClose(GameObject go)
    {
      this.ActivateOutputLinks(2);
    }

    public override void OnActivate(int pinID)
    {
      switch (pinID)
      {
        case 100:
          this.Create();
          this.ActivateOutputLinks(1);
          break;
        case 101:
          if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.webView, (UnityEngine.Object) null))
            this.webView.BeginClose();
          this.ActivateOutputLinks(2);
          break;
      }
    }

    public enum URL_Mode
    {
      APIHost,
      SiteHost,
      NewsHost,
    }
  }
}
