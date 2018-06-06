// Decompiled with JetBrains decompiler
// Type: SRPG.NewsWindow
// Assembly: Assembly-CSharp, Version=1.2.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9BA76916-D0BD-4DB6-A90B-FE0BCC53E511
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using GR;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace SRPG
{
  public class NewsWindow : MonoBehaviour
  {
    public RectTransform WebViewContainer;
    private UniWebView mWebView;
    private string uriWhatsnew;
    private string uriWhatsnewUrgency;
    private string siteHost;
    private readonly string offical_url;
    private readonly string serialcode_url;
    private readonly string invitation_url;
    private string[] allow_change_scenes;
    public Button CloseButton;
    public int testCounter;

    public NewsWindow()
    {
      base.\u002Ector();
    }

    private void Start()
    {
      this.siteHost = Network.SiteHost;
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
        string str = Network.NewsHost + this.uriWhatsnew;
        if (NewsUtility.getNewsTypes() == NewsUtility.NewsTypes.Urgency)
        {
          str = Network.NewsHost + this.uriWhatsnewUrgency;
          NewsUtility.clearNewsType();
        }
        if (FlowNode_Variable.Get("WEBVIEW_ACCESS_TYPE") != null)
        {
          if (FlowNode_Variable.Get("WEBVIEW_ACCESS_TYPE") == "1")
          {
            FlowNode_Variable.Set("WEBVIEW_ACCESS_TYPE", "0");
            str = this.siteHost + this.serialcode_url + "?fuid=" + MonoSingleton<GameManager>.Instance.Player.FUID;
          }
          else
          {
            if (FlowNode_Variable.Get("WEBVIEW_ACCESS_TYPE") == "2")
            {
              FlowNode_Variable.Set("WEBVIEW_ACCESS_TYPE", "0");
              Application.OpenURL(this.offical_url);
              return;
            }
            if (FlowNode_Variable.Get("WEBVIEW_ACCESS_TYPE") == "3")
            {
              FlowNode_Variable.Set("WEBVIEW_ACCESS_TYPE", "0");
              str = this.siteHost + this.invitation_url + "?fuid=" + MonoSingleton<GameManager>.Instance.Player.FUID;
            }
          }
        }
        this.mWebView = (UniWebView) ((Component) this).GetComponent<UniWebView>();
        if (Object.op_Inequality((Object) this.WebViewContainer, (Object) null) && Object.op_Equality((Object) this.mWebView, (Object) null))
        {
          this.WebViewContainer.get_rect();
          this.mWebView = (UniWebView) new GameObject("UniWebView").AddComponent<UniWebView>();
          this.mWebView.OnLoadComplete += new UniWebView.LoadCompleteDelegate(this.OnLoadComplete);
          this.mWebView.OnReceivedMessage += new UniWebView.ReceivedMessageDelegate(this.OnReceivedMessage);
          this.mWebView.InsetsForScreenOreitation += new UniWebView.InsetsForScreenOreitationDelegate(this.InsetsForScreenOreitation);
          ((Component) this.mWebView).get_transform().SetParent((Transform) this.WebViewContainer, false);
          this.mWebView.insets = new UniWebViewEdgeInsets(0, 0, 0, 0);
          if (GameUtility.Config_Language == "french")
            str += "?lang=fr";
          if (GameUtility.Config_Language == "german")
            str += "?lang=de";
          if (GameUtility.Config_Language == "spanish")
            str += "?lang=es";
          this.mWebView.url = str;
          this.mWebView.Load();
        }
        if (!Object.op_Inequality((Object) this.CloseButton, (Object) null))
          return;
        ((Selectable) this.CloseButton).set_interactable(false);
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

    private UniWebViewEdgeInsets InsetsForScreenOreitation(UniWebView webView, UniWebViewOrientation orientation)
    {
      Vector3[] vector3Array = new Vector3[4];
      this.WebViewContainer.GetWorldCorners(vector3Array);
      float num1 = (float) ScreenUtility.DefaultScreenWidth / (float) Screen.get_width();
      float num2 = (float) ScreenUtility.DefaultScreenHeight / (float) Screen.get_height();
      int aLeft = (int) (vector3Array[0].x * (double) num1);
      int aRight = (int) (((double) Screen.get_width() - vector3Array[2].x) * (double) num1);
      int aTop = (int) (((double) Screen.get_height() - vector3Array[1].y) * (double) num2);
      int aBottom = (int) (vector3Array[0].y * (double) num2);
      return new UniWebViewEdgeInsets(aTop, aLeft, aBottom, aRight);
    }

    private void OnLoadComplete(UniWebView webView, bool success, string errorMessage)
    {
      if (success)
        this.mWebView.Show(false, UniWebViewTransitionEdge.None, 0.4f, (Action) null);
      else
        Debug.LogError((object) ("Something wrong in webview loading: " + errorMessage));
      if (!Object.op_Inequality((Object) this.CloseButton, (Object) null))
        return;
      ((Selectable) this.CloseButton).set_interactable(true);
    }

    private void OnReceivedMessage(UniWebView webView, UniWebViewMessage message)
    {
      if (string.Equals(message.path, "scene"))
      {
        this.StartSceneChange(message.args["name"]);
      }
      else
      {
        if (!string.Equals(message.path, "browser") || !(message.args.ContainsKey("protocol") | message.args.ContainsKey("url")))
          return;
        string str1 = message.rawMessage.Substring("uniwebview://".Length);
        string str2 = message.args["protocol"];
        int num = str1.IndexOf("url=");
        string str3 = str1.Substring(num + "url=".Length);
        string uriString = str2 + "://" + str3;
        Uri result;
        if (Uri.TryCreate(uriString, UriKind.RelativeOrAbsolute, out result))
          Application.OpenURL(uriString);
        else
          Debug.Log((object) ("This is not valid as URL: " + str1));
      }
    }
  }
}
