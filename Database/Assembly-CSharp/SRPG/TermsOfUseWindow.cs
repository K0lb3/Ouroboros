// Decompiled with JetBrains decompiler
// Type: SRPG.TermsOfUseWindow
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using GR;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace SRPG
{
  public class TermsOfUseWindow : MonoBehaviour
  {
    public TermsOfUseWindow.URLType URL_Type;
    public RectTransform WebViewContainer;
    private UniWebView mWebView;
    public Button CloseButton;

    public TermsOfUseWindow()
    {
      base.\u002Ector();
    }

    private void Start()
    {
      Debug.Log((object) "[WebviewWindow]Start");
      if (!MonoSingleton<DebugManager>.Instance.IsWebViewEnable())
      {
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.CloseButton, (UnityEngine.Object) null))
          ((Selectable) this.CloseButton).set_interactable(true);
        Debug.Log((object) "[WebviewWindow]WebView not Enabled");
      }
      else
      {
        string str = string.Empty;
        switch (this.URL_Type)
        {
          case TermsOfUseWindow.URLType.TermsOfUse:
            str = "http://gumi.sg/terms/";
            break;
          case TermsOfUseWindow.URLType.PrivacyPolicy:
            str = "http://gumi.sg/privacy-policy/";
            break;
          case TermsOfUseWindow.URLType.HelpCenter:
            string configLanguage = GameUtility.Config_Language;
            if (configLanguage != null)
            {
              // ISSUE: reference to a compiler-generated field
              if (TermsOfUseWindow.\u003C\u003Ef__switch\u0024mapA == null)
              {
                // ISSUE: reference to a compiler-generated field
                TermsOfUseWindow.\u003C\u003Ef__switch\u0024mapA = new Dictionary<string, int>(3)
                {
                  {
                    "french",
                    0
                  },
                  {
                    "german",
                    1
                  },
                  {
                    "spanish",
                    2
                  }
                };
              }
              int num;
              // ISSUE: reference to a compiler-generated field
              if (TermsOfUseWindow.\u003C\u003Ef__switch\u0024mapA.TryGetValue(configLanguage, out num))
              {
                switch (num)
                {
                  case 0:
                    str = "https://alchemistww.zendesk.com/hc/fr";
                    goto label_17;
                  case 1:
                    str = "https://alchemistww.zendesk.com/hc/de";
                    goto label_17;
                  case 2:
                    str = "https://alchemistww.zendesk.com/hc/es";
                    goto label_17;
                }
              }
            }
            str = "https://alchemistww.zendesk.com/hc/en-us";
            break;
        }
label_17:
        if (GameUtility.Config_Language == "french")
          str += "?lang=fr";
        if (GameUtility.Config_Language == "german")
          str += "?lang=de";
        if (GameUtility.Config_Language == "spanish")
          str += "?lang=es";
        Debug.Log((object) ("[WebviewWindow]WebView opening " + str));
        this.mWebView = (UniWebView) ((Component) this).GetComponent<UniWebView>();
        if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) this.WebViewContainer, (UnityEngine.Object) null) || !UnityEngine.Object.op_Equality((UnityEngine.Object) this.mWebView, (UnityEngine.Object) null))
          return;
        this.WebViewContainer.get_rect();
        this.mWebView = (UniWebView) new GameObject("UniWebView").AddComponent<UniWebView>();
        this.mWebView.OnLoadComplete += new UniWebView.LoadCompleteDelegate(this.OnLoadComplete);
        this.mWebView.InsetsForScreenOreitation += new UniWebView.InsetsForScreenOreitationDelegate(this.InsetsForScreenOreitation);
        ((Component) this.mWebView).get_transform().SetParent((Transform) this.WebViewContainer, false);
        this.mWebView.insets = new UniWebViewEdgeInsets(0, 0, 0, 0);
        this.mWebView.url = str;
        this.mWebView.Load();
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
      if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) this.CloseButton, (UnityEngine.Object) null))
        return;
      ((Selectable) this.CloseButton).set_interactable(true);
    }

    public enum URLType
    {
      TermsOfUse,
      PrivacyPolicy,
      HelpCenter,
    }
  }
}
