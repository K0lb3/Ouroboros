// Decompiled with JetBrains decompiler
// Type: UniWebView
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using SRPG;
using System;
using System.Collections;
using System.Diagnostics;
using UnityEngine;

public class UniWebView : MonoBehaviour
{
  [SerializeField]
  private UniWebViewEdgeInsets _insets;
  public string url;
  public bool loadOnStart;
  public bool autoShowWhenLoadComplete;
  private bool _backButtonEnable;
  private bool _bouncesEnable;
  private bool _zoomEnable;
  private string _currentGUID;
  private int _lastScreenHeight;
  private bool _immersiveMode;
  private Action _showTransitionAction;
  private Action _hideTransitionAction;
  public bool toolBarShow;

  public UniWebView()
  {
    base.\u002Ector();
  }

  public event UniWebView.LoadCompleteDelegate OnLoadComplete;

  public event UniWebView.LoadBeginDelegate OnLoadBegin;

  public event UniWebView.ReceivedMessageDelegate OnReceivedMessage;

  public event UniWebView.EvalJavaScriptFinishedDelegate OnEvalJavaScriptFinished;

  public event UniWebView.WebViewShouldCloseDelegate OnWebViewShouldClose;

  public event UniWebView.ReceivedKeyCodeDelegate OnReceivedKeyCode;

  public event UniWebView.InsetsForScreenOreitationDelegate InsetsForScreenOreitation;

  public UniWebViewEdgeInsets insets
  {
    get
    {
      return this._insets;
    }
    set
    {
      if (!(this._insets != value))
        return;
      this.ForceUpdateInsetsInternal(value);
    }
  }

  private void ForceUpdateInsetsInternal(UniWebViewEdgeInsets insets)
  {
    this._insets = insets;
    UniWebViewPlugin.ChangeInsets(((UnityEngine.Object) ((Component) this).get_gameObject()).get_name(), this.insets.top, this.insets.left, this.insets.bottom, this.insets.right);
  }

  public string currentUrl
  {
    get
    {
      return UniWebViewPlugin.GetCurrentUrl(((UnityEngine.Object) ((Component) this).get_gameObject()).get_name());
    }
  }

  public bool backButtonEnable
  {
    get
    {
      return this._backButtonEnable;
    }
    set
    {
      if (this._backButtonEnable == value)
        return;
      this._backButtonEnable = value;
      UniWebViewPlugin.SetBackButtonEnable(((UnityEngine.Object) ((Component) this).get_gameObject()).get_name(), this._backButtonEnable);
    }
  }

  public bool bouncesEnable
  {
    get
    {
      return this._bouncesEnable;
    }
    set
    {
      if (this._bouncesEnable == value)
        return;
      this._bouncesEnable = value;
      UniWebViewPlugin.SetBounces(((UnityEngine.Object) ((Component) this).get_gameObject()).get_name(), this._bouncesEnable);
    }
  }

  public bool zoomEnable
  {
    get
    {
      return this._zoomEnable;
    }
    set
    {
      if (this._zoomEnable == value)
        return;
      this._zoomEnable = value;
      UniWebViewPlugin.SetZoomEnable(((UnityEngine.Object) ((Component) this).get_gameObject()).get_name(), this._zoomEnable);
    }
  }

  public string userAgent
  {
    get
    {
      return UniWebViewPlugin.GetUserAgent(((UnityEngine.Object) ((Component) this).get_gameObject()).get_name());
    }
  }

  public float alpha
  {
    get
    {
      return UniWebViewPlugin.GetAlpha(((UnityEngine.Object) ((Component) this).get_gameObject()).get_name());
    }
    set
    {
      UniWebViewPlugin.SetAlpha(((UnityEngine.Object) ((Component) this).get_gameObject()).get_name(), Mathf.Clamp01(value));
    }
  }

  public bool immersiveMode
  {
    get
    {
      return this._immersiveMode;
    }
    set
    {
      this._immersiveMode = value;
      UniWebViewPlugin.SetImmersiveModeEnabled(((UnityEngine.Object) ((Component) this).get_gameObject()).get_name(), this._immersiveMode);
    }
  }

  public static void SetUserAgent(string value)
  {
    UniWebViewPlugin.SetUserAgent(value);
  }

  public static void ResetUserAgent()
  {
    UniWebViewPlugin.SetUserAgent((string) null);
  }

  public void Load()
  {
    UniWebViewPlugin.Load(((UnityEngine.Object) ((Component) this).get_gameObject()).get_name(), !string.IsNullOrEmpty(this.url) ? this.url.Trim() : "about:blank");
  }

  public void Load(string aUrl)
  {
    this.url = aUrl;
    this.Load();
  }

  public void LoadHTMLString(string htmlString, string baseUrl)
  {
    UniWebViewPlugin.LoadHTMLString(((UnityEngine.Object) ((Component) this).get_gameObject()).get_name(), htmlString, baseUrl);
  }

  public void Reload()
  {
    UniWebViewPlugin.Reload(((UnityEngine.Object) ((Component) this).get_gameObject()).get_name());
  }

  public void Stop()
  {
    UniWebViewPlugin.Stop(((UnityEngine.Object) ((Component) this).get_gameObject()).get_name());
  }

  public void Show(bool fade = false, UniWebViewTransitionEdge direction = UniWebViewTransitionEdge.None, float duration = 0.4f, Action finishAction = null)
  {
    this._lastScreenHeight = UniWebViewHelper.screenHeight;
    this.ResizeInternal();
    UniWebViewPlugin.Show(((UnityEngine.Object) ((Component) this).get_gameObject()).get_name(), fade, (int) direction, duration);
    this._showTransitionAction = finishAction;
    if (!this.toolBarShow)
      return;
    this.ShowToolBar(true);
  }

  public void Hide(bool fade = false, UniWebViewTransitionEdge direction = UniWebViewTransitionEdge.None, float duration = 0.4f, Action finishAction = null)
  {
    UniWebViewPlugin.Hide(((UnityEngine.Object) ((Component) this).get_gameObject()).get_name(), fade, (int) direction, duration);
    this._hideTransitionAction = finishAction;
  }

  public void EvaluatingJavaScript(string javaScript)
  {
    UniWebViewPlugin.EvaluatingJavaScript(((UnityEngine.Object) ((Component) this).get_gameObject()).get_name(), javaScript);
  }

  public void AddJavaScript(string javaScript)
  {
    UniWebViewPlugin.AddJavaScript(((UnityEngine.Object) ((Component) this).get_gameObject()).get_name(), javaScript);
  }

  public void CleanCache()
  {
    UniWebViewPlugin.CleanCache(((UnityEngine.Object) ((Component) this).get_gameObject()).get_name());
  }

  public void CleanCookie(string key = null)
  {
    UniWebViewPlugin.CleanCookie(((UnityEngine.Object) ((Component) this).get_gameObject()).get_name(), key);
  }

  [Obsolete("SetTransparentBackground is deprecated, please use SetBackgroundColor instead.")]
  public void SetTransparentBackground(bool transparent = true)
  {
    UniWebViewPlugin.TransparentBackground(((UnityEngine.Object) ((Component) this).get_gameObject()).get_name(), transparent);
  }

  public void SetBackgroundColor(Color color)
  {
    UniWebViewPlugin.SetBackgroundColor(((UnityEngine.Object) ((Component) this).get_gameObject()).get_name(), (float) color.r, (float) color.g, (float) color.b, (float) color.a);
  }

  public void ShowToolBar(bool animate)
  {
  }

  public void HideToolBar(bool animate)
  {
  }

  public void SetShowSpinnerWhenLoading(bool show)
  {
    UniWebViewPlugin.SetSpinnerShowWhenLoading(((UnityEngine.Object) ((Component) this).get_gameObject()).get_name(), show);
  }

  public void SetSpinnerLabelText(string text)
  {
    UniWebViewPlugin.SetSpinnerText(((UnityEngine.Object) ((Component) this).get_gameObject()).get_name(), text);
  }

  public void SetUseWideViewPort(bool use)
  {
    UniWebViewPlugin.SetUseWideViewPort(((UnityEngine.Object) ((Component) this).get_gameObject()).get_name(), use);
  }

  public bool CanGoBack()
  {
    return UniWebViewPlugin.CanGoBack(((UnityEngine.Object) ((Component) this).get_gameObject()).get_name());
  }

  public bool CanGoForward()
  {
    return UniWebViewPlugin.CanGoForward(((UnityEngine.Object) ((Component) this).get_gameObject()).get_name());
  }

  public void GoBack()
  {
    UniWebViewPlugin.GoBack(((UnityEngine.Object) ((Component) this).get_gameObject()).get_name());
  }

  public void GoForward()
  {
    UniWebViewPlugin.GoForward(((UnityEngine.Object) ((Component) this).get_gameObject()).get_name());
  }

  public void AddPermissionRequestTrustSite(string url)
  {
    UniWebViewPlugin.AddPermissionRequestTrustSite(((UnityEngine.Object) ((Component) this).get_gameObject()).get_name(), url);
  }

  public void AddUrlScheme(string scheme)
  {
    UniWebViewPlugin.AddUrlScheme(((UnityEngine.Object) ((Component) this).get_gameObject()).get_name(), scheme);
  }

  public void RemoveUrlScheme(string scheme)
  {
    UniWebViewPlugin.RemoveUrlScheme(((UnityEngine.Object) ((Component) this).get_gameObject()).get_name(), scheme);
  }

  public void SetHeaderField(string key, string value)
  {
    UniWebViewPlugin.SetHeaderField(((UnityEngine.Object) ((Component) this).get_gameObject()).get_name(), key, value);
  }

  private bool OrientationChanged()
  {
    int screenHeight = UniWebViewHelper.screenHeight;
    if (this._lastScreenHeight == screenHeight)
      return false;
    this._lastScreenHeight = screenHeight;
    return true;
  }

  private void ResizeInternal()
  {
    int screenHeight = UniWebViewHelper.screenHeight;
    int screenWidth = UniWebViewHelper.screenWidth;
    UniWebViewEdgeInsets insets = this.insets;
    if (this.InsetsForScreenOreitation != null)
      insets = this.InsetsForScreenOreitation(this, screenHeight < screenWidth ? UniWebViewOrientation.LandScape : UniWebViewOrientation.Portrait);
    this.ForceUpdateInsetsInternal(insets);
  }

  private void LoadComplete(string message)
  {
    bool flag1 = string.Equals(message, string.Empty);
    bool flag2 = this.OnLoadComplete != null;
    if (flag1)
    {
      if (flag2)
        this.OnLoadComplete(this, true, (string) null);
      if (!this.autoShowWhenLoadComplete)
        return;
      this.Show(false, UniWebViewTransitionEdge.None, 0.4f, (Action) null);
    }
    else
    {
      Debug.LogWarning((object) ("Web page load failed: " + ((UnityEngine.Object) ((Component) this).get_gameObject()).get_name() + "; url: " + this.url + "; error:" + message));
      if (!flag2)
        return;
      this.OnLoadComplete(this, false, message);
    }
  }

  private void LoadBegin(string url)
  {
    if (this.OnLoadBegin == null)
      return;
    this.OnLoadBegin(this, url);
  }

  private void ReceivedMessage(string rawMessage)
  {
    UniWebViewMessage message = new UniWebViewMessage(rawMessage);
    if (this.OnReceivedMessage == null)
      return;
    this.OnReceivedMessage(this, message);
  }

  private void WebViewDone(string message)
  {
    BackHandler.Invoke();
  }

  private void WebViewKeyDown(string message)
  {
    int int32 = Convert.ToInt32(message);
    if (this.OnReceivedKeyCode == null)
      return;
    this.OnReceivedKeyCode(this, int32);
  }

  private void EvalJavaScriptFinished(string result)
  {
    if (this.OnEvalJavaScriptFinished == null)
      return;
    this.OnEvalJavaScriptFinished(this, result);
  }

  private void AnimationFinished(string identifier)
  {
  }

  private void ShowTransitionFinished(string message)
  {
    if (this._showTransitionAction == null)
      return;
    this._showTransitionAction();
    this._showTransitionAction = (Action) null;
  }

  private void HideTransitionFinished(string message)
  {
    if (this._hideTransitionAction == null)
      return;
    this._hideTransitionAction();
    this._hideTransitionAction = (Action) null;
  }

  [DebuggerHidden]
  private IEnumerator LoadFromJarPackage(string jarFilePath)
  {
    // ISSUE: object of a compiler-generated type is created
    return (IEnumerator) new UniWebView.\u003CLoadFromJarPackage\u003Ec__Iterator145()
    {
      jarFilePath = jarFilePath,
      \u003C\u0024\u003EjarFilePath = jarFilePath,
      \u003C\u003Ef__this = this
    };
  }

  private void Awake()
  {
    this._currentGUID = Guid.NewGuid().ToString();
    ((UnityEngine.Object) ((Component) this).get_gameObject()).set_name(((UnityEngine.Object) ((Component) this).get_gameObject()).get_name() + this._currentGUID);
    UniWebViewPlugin.Init(((UnityEngine.Object) ((Component) this).get_gameObject()).get_name(), this.insets.top, this.insets.left, this.insets.bottom, this.insets.right);
    this._lastScreenHeight = UniWebViewHelper.screenHeight;
  }

  private void Start()
  {
    if (!this.loadOnStart)
      return;
    this.Load();
  }

  private void OnDestroy()
  {
    this.RemoveAllListeners();
    UniWebViewPlugin.Destroy(((UnityEngine.Object) ((Component) this).get_gameObject()).get_name());
    ((UnityEngine.Object) ((Component) this).get_gameObject()).set_name(((UnityEngine.Object) ((Component) this).get_gameObject()).get_name().Replace(this._currentGUID, string.Empty));
  }

  private void RemoveAllListeners()
  {
    this.OnLoadBegin = (UniWebView.LoadBeginDelegate) null;
    this.OnLoadComplete = (UniWebView.LoadCompleteDelegate) null;
    this.OnReceivedMessage = (UniWebView.ReceivedMessageDelegate) null;
    this.OnReceivedKeyCode = (UniWebView.ReceivedKeyCodeDelegate) null;
    this.OnEvalJavaScriptFinished = (UniWebView.EvalJavaScriptFinishedDelegate) null;
    this.OnWebViewShouldClose = (UniWebView.WebViewShouldCloseDelegate) null;
    this.InsetsForScreenOreitation = (UniWebView.InsetsForScreenOreitationDelegate) null;
  }

  private void Update()
  {
    if (!this.OrientationChanged())
      return;
    this.ResizeInternal();
  }

  public delegate void LoadCompleteDelegate(UniWebView webView, bool success, string errorMessage);

  public delegate void LoadBeginDelegate(UniWebView webView, string loadingUrl);

  public delegate void ReceivedMessageDelegate(UniWebView webView, UniWebViewMessage message);

  public delegate void EvalJavaScriptFinishedDelegate(UniWebView webView, string result);

  public delegate bool WebViewShouldCloseDelegate(UniWebView webView);

  public delegate void ReceivedKeyCodeDelegate(UniWebView webView, int keyCode);

  public delegate UniWebViewEdgeInsets InsetsForScreenOreitationDelegate(UniWebView webView, UniWebViewOrientation orientation);
}
