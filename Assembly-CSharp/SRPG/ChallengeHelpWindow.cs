// Decompiled with JetBrains decompiler
// Type: SRPG.ChallengeHelpWindow
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using GR;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace SRPG
{
  public class ChallengeHelpWindow : MonoBehaviour
  {
    public RectTransform WebViewContainer;
    private UniWebView mWebView;
    private readonly string url;
    public Button CloseButton;

    public ChallengeHelpWindow()
    {
      base.\u002Ector();
    }

    private void Start()
    {
      if (!MonoSingleton<DebugManager>.Instance.IsWebViewEnable())
      {
        if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) this.CloseButton, (UnityEngine.Object) null))
          return;
        ((Selectable) this.CloseButton).set_interactable(true);
      }
      else
      {
        this.mWebView = (UniWebView) ((Component) this).GetComponent<UniWebView>();
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.WebViewContainer, (UnityEngine.Object) null) && UnityEngine.Object.op_Equality((UnityEngine.Object) this.mWebView, (UnityEngine.Object) null))
        {
          this.WebViewContainer.get_rect();
          this.mWebView = (UniWebView) new GameObject("UniWebView").AddComponent<UniWebView>();
          this.mWebView.OnLoadComplete += new UniWebView.LoadCompleteDelegate(this.OnLoadComplete);
          this.mWebView.OnReceivedMessage += new UniWebView.ReceivedMessageDelegate(this.OnReceivedMessage);
          this.mWebView.InsetsForScreenOreitation += new UniWebView.InsetsForScreenOreitationDelegate(this.InsetsForScreenOreitation);
          ((Component) this.mWebView).get_transform().SetParent((Transform) this.WebViewContainer, false);
          this.mWebView.insets = new UniWebViewEdgeInsets(0, 0, 0, 0);
          this.mWebView.url = this.url;
          this.mWebView.Load();
        }
        if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) this.CloseButton, (UnityEngine.Object) null))
          return;
        ((Selectable) this.CloseButton).set_interactable(false);
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

    private void OnReceivedMessage(UniWebView webView, UniWebViewMessage message)
    {
    }
  }
}
