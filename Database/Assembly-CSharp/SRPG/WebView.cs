// Decompiled with JetBrains decompiler
// Type: SRPG.WebView
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using System;
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
    private UniWebView uniWebView;

    public WebView()
    {
      base.\u002Ector();
    }

    public void BeginClose()
    {
      UnityEngine.Object.DestroyImmediate((UnityEngine.Object) ((Component) this).get_gameObject());
    }

    private void OnClickButton(GameObject obj)
    {
      if (UnityEngine.Object.op_Equality((UnityEngine.Object) obj, (UnityEngine.Object) ((Component) this.Btn_Close).get_gameObject()) && UnityEngine.Object.op_Inequality((UnityEngine.Object) this.Btn_Close, (UnityEngine.Object) null))
        this.OnClose(((Component) this).get_gameObject());
      this.BeginClose();
    }

    public void SetTitleText(string text)
    {
      if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) this.Text_Title, (UnityEngine.Object) null))
        return;
      this.Text_Title.set_text(text);
    }

    private void Awake()
    {
      if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) this.Btn_Close, (UnityEngine.Object) null))
        return;
      UIUtility.AddEventListener(((Component) this.Btn_Close).get_gameObject(), (UnityEvent) this.Btn_Close.get_onClick(), new UIUtility.EventListener(this.OnClickButton));
    }

    public void SetHeaderField(string key, string value)
    {
      this.uniWebView = (UniWebView) ((Component) this).get_gameObject().GetComponent<UniWebView>();
      if (UnityEngine.Object.op_Equality((UnityEngine.Object) this.uniWebView, (UnityEngine.Object) null))
        this.uniWebView = (UniWebView) ((Component) this).get_gameObject().AddComponent<UniWebView>();
      this.uniWebView.SetHeaderField(key, value);
    }

    public void OpenURL(string url)
    {
      if (GameUtility.Config_Language == "french")
        url += "?lang=fr";
      if (GameUtility.Config_Language == "german")
        url += "?lang=de";
      if (GameUtility.Config_Language == "spanish")
        url += "?lang=es";
      this.uniWebView = (UniWebView) ((Component) this).get_gameObject().GetComponent<UniWebView>();
      if (UnityEngine.Object.op_Equality((UnityEngine.Object) this.uniWebView, (UnityEngine.Object) null))
        this.uniWebView = (UniWebView) ((Component) this).get_gameObject().AddComponent<UniWebView>();
      this.uniWebView.OnLoadComplete += new UniWebView.LoadCompleteDelegate(this.OnLoadComplete);
      this.uniWebView.OnReceivedMessage += new UniWebView.ReceivedMessageDelegate(this.OnReceivedMessage);
      this.uniWebView.InsetsForScreenOreitation += new UniWebView.InsetsForScreenOreitationDelegate(this.InsetsForScreenOreitation);
      this.uniWebView.insets = new UniWebViewEdgeInsets(0, 0, 0, 0);
      this.uniWebView.SetShowSpinnerWhenLoading(true);
      this.uniWebView.url = url;
      this.uniWebView.Load();
    }

    private void OnLoadComplete(UniWebView webView, bool success, string errorMessage)
    {
      if (success)
        webView.Show(false, UniWebViewTransitionEdge.None, 0.4f, (Action) null);
      else
        Debug.Log((object) ("Something wrong in webview loading: " + errorMessage));
    }

    private void OnReceivedMessage(UniWebView webView, UniWebViewMessage message)
    {
      if (!(message.scheme == "uniwebview") || !string.Equals(message.path, "browser") || !(message.args.ContainsKey("protocol") | message.args.ContainsKey("url")))
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

    private UniWebViewEdgeInsets InsetsForScreenOreitation(UniWebView webView, UniWebViewOrientation orientation)
    {
      Vector3[] vector3Array = new Vector3[4];
      ((RectTransform) ((Component) this.ClientArea).GetComponent<RectTransform>()).GetWorldCorners(vector3Array);
      float num1 = (float) ScreenUtility.DefaultScreenWidth / (float) Screen.get_width();
      float num2 = (float) ScreenUtility.DefaultScreenHeight / (float) Screen.get_height();
      int aLeft = (int) (vector3Array[0].x * (double) num1);
      int aRight = (int) (((double) Screen.get_width() - vector3Array[2].x) * (double) num1);
      int aTop = (int) (((double) Screen.get_height() - vector3Array[1].y) * (double) num2);
      int aBottom = (int) (vector3Array[0].y * (double) num2);
      return new UniWebViewEdgeInsets(aTop, aLeft, aBottom, aRight);
    }
  }
}
