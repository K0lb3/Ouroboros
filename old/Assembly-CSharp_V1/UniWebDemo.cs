// Decompiled with JetBrains decompiler
// Type: UniWebDemo
// Assembly: Assembly-CSharp, Version=1.2.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9BA76916-D0BD-4DB6-A90B-FE0BCC53E511
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using System;
using UnityEngine;

public class UniWebDemo : MonoBehaviour
{
  public GameObject cubePrefab;
  public TextMesh tipTextMesh;
  private UniWebView _webView;
  private string _errorMessage;
  private GameObject _cube;
  private Vector3 _moveVector;

  public UniWebDemo()
  {
    base.\u002Ector();
  }

  private void Start()
  {
    this._cube = (GameObject) Object.Instantiate<GameObject>((M0) this.cubePrefab);
    ((UniWebViewCube) this._cube.GetComponent<UniWebViewCube>()).webViewDemo = this;
    this._moveVector = Vector3.get_zero();
  }

  private void Update()
  {
    if (!Object.op_Inequality((Object) this._cube, (Object) null))
      return;
    if (this._cube.get_transform().get_position().y < -5.0)
    {
      Object.Destroy((Object) this._cube);
      this._cube = (GameObject) null;
    }
    else
      this._cube.get_transform().Translate(Vector3.op_Multiply(this._moveVector, Time.get_deltaTime()));
  }

  private void OnGUI()
  {
    if (GUI.Button(new Rect(0.0f, (float) (Screen.get_height() - 150), 150f, 80f), "Open"))
    {
      this._webView = (UniWebView) ((Component) this).GetComponent<UniWebView>();
      if (Object.op_Equality((Object) this._webView, (Object) null))
      {
        this._webView = (UniWebView) ((Component) this).get_gameObject().AddComponent<UniWebView>();
        this._webView.OnReceivedMessage += new UniWebView.ReceivedMessageDelegate(this.OnReceivedMessage);
        this._webView.OnLoadComplete += new UniWebView.LoadCompleteDelegate(this.OnLoadComplete);
        this._webView.OnWebViewShouldClose += new UniWebView.WebViewShouldCloseDelegate(this.OnWebViewShouldClose);
        this._webView.OnEvalJavaScriptFinished += new UniWebView.EvalJavaScriptFinishedDelegate(this.OnEvalJavaScriptFinished);
        this._webView.InsetsForScreenOreitation += new UniWebView.InsetsForScreenOreitationDelegate(this.InsetsForScreenOreitation);
      }
      this._webView.url = "http://uniwebview.onevcat.com/demo/index1-1.html";
      this._webView.Load();
      this._errorMessage = (string) null;
    }
    if (Object.op_Inequality((Object) this._webView, (Object) null) && GUI.Button(new Rect(150f, (float) (Screen.get_height() - 150), 150f, 80f), "Back"))
      this._webView.GoBack();
    if (Object.op_Inequality((Object) this._webView, (Object) null) && GUI.Button(new Rect(300f, (float) (Screen.get_height() - 150), 150f, 80f), "ToolBar"))
    {
      if (this._webView.toolBarShow)
        this._webView.HideToolBar(true);
      else
        this._webView.ShowToolBar(true);
    }
    if (this._errorMessage == null)
      return;
    GUI.Label(new Rect(0.0f, 0.0f, (float) Screen.get_width(), 80f), this._errorMessage);
  }

  private void OnLoadComplete(UniWebView webView, bool success, string errorMessage)
  {
    if (success)
    {
      webView.Show(false, UniWebViewTransitionEdge.None, 0.4f, (Action) null);
    }
    else
    {
      Debug.Log((object) ("Something wrong in webview loading: " + errorMessage));
      this._errorMessage = errorMessage;
    }
  }

  private void OnReceivedMessage(UniWebView webView, UniWebViewMessage message)
  {
    Debug.Log((object) "Received a message from native");
    Debug.Log((object) message.rawMessage);
    if (string.Equals(message.path, "move"))
    {
      Vector3 vector3 = Vector3.get_zero();
      if (string.Equals(message.args["direction"], "up"))
      {
        // ISSUE: explicit reference operation
        ((Vector3) @vector3).\u002Ector(0.0f, 0.0f, 1f);
      }
      else if (string.Equals(message.args["direction"], "down"))
      {
        // ISSUE: explicit reference operation
        ((Vector3) @vector3).\u002Ector(0.0f, 0.0f, -1f);
      }
      else if (string.Equals(message.args["direction"], "left"))
      {
        // ISSUE: explicit reference operation
        ((Vector3) @vector3).\u002Ector(-1f, 0.0f, 0.0f);
      }
      else if (string.Equals(message.args["direction"], "right"))
      {
        // ISSUE: explicit reference operation
        ((Vector3) @vector3).\u002Ector(1f, 0.0f, 0.0f);
      }
      int result = 0;
      if (int.TryParse(message.args["distance"], out result))
        vector3 = Vector3.op_Multiply(vector3, (float) result);
      this._moveVector = vector3;
    }
    else if (string.Equals(message.path, "add"))
    {
      if (Object.op_Inequality((Object) this._cube, (Object) null))
        Object.Destroy((Object) this._cube);
      this._cube = (GameObject) Object.Instantiate<GameObject>((M0) this.cubePrefab);
      ((UniWebViewCube) this._cube.GetComponent<UniWebViewCube>()).webViewDemo = this;
      this._moveVector = Vector3.get_zero();
    }
    else
    {
      if (!string.Equals(message.path, "close"))
        return;
      webView.Hide(false, UniWebViewTransitionEdge.None, 0.4f, (Action) null);
      Object.Destroy((Object) webView);
      webView.OnReceivedMessage -= new UniWebView.ReceivedMessageDelegate(this.OnReceivedMessage);
      webView.OnLoadComplete -= new UniWebView.LoadCompleteDelegate(this.OnLoadComplete);
      webView.OnWebViewShouldClose -= new UniWebView.WebViewShouldCloseDelegate(this.OnWebViewShouldClose);
      webView.OnEvalJavaScriptFinished -= new UniWebView.EvalJavaScriptFinishedDelegate(this.OnEvalJavaScriptFinished);
      webView.InsetsForScreenOreitation -= new UniWebView.InsetsForScreenOreitationDelegate(this.InsetsForScreenOreitation);
      this._webView = (UniWebView) null;
    }
  }

  public void ShowAlertInWebview(float time, bool first)
  {
    this._moveVector = Vector3.get_zero();
    if (!first)
      return;
    this._webView.EvaluatingJavaScript("sample(" + (object) time + ")");
  }

  private void OnEvalJavaScriptFinished(UniWebView webView, string result)
  {
    Debug.Log((object) ("js result: " + result));
    this.tipTextMesh.set_text("<color=#000000>" + result + "</color>");
  }

  private bool OnWebViewShouldClose(UniWebView webView)
  {
    if (!Object.op_Equality((Object) webView, (Object) this._webView))
      return false;
    this._webView = (UniWebView) null;
    return true;
  }

  private UniWebViewEdgeInsets InsetsForScreenOreitation(UniWebView webView, UniWebViewOrientation orientation)
  {
    int aBottom = (int) ((double) UniWebViewHelper.screenHeight * 0.5);
    if (orientation == UniWebViewOrientation.Portrait)
      return new UniWebViewEdgeInsets(5, 5, aBottom, 5);
    return new UniWebViewEdgeInsets(5, 5, aBottom, 5);
  }
}
