// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_FgGWebView
// Assembly: Assembly-CSharp, Version=1.2.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9BA76916-D0BD-4DB6-A90B-FE0BCC53E511
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using GR;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace SRPG
{
  [FlowNode.NodeType("FgGID/FgGWebView", 32741)]
  [FlowNode.Pin(1, "Enable", FlowNode.PinTypes.Input, 0)]
  [FlowNode.Pin(2, "Disable", FlowNode.PinTypes.Input, 1)]
  [FlowNode.Pin(3, "Finished", FlowNode.PinTypes.Output, 100)]
  public class FlowNode_FgGWebView : FlowNode
  {
    private const int PIN_ID_ENABLE = 1;
    private const int PIN_ID_DISABLE = 2;
    private const int PIN_ID_FINISHED = 3;
    [FlowNode.ShowInInfo]
    [FlowNode.DropTarget(typeof (GameObject), true)]
    public GameObject Target;
    public string URL;
    public RawImage mClientArea;
    private UniWebView uniWebView;

    public override void OnActivate(int pinID)
    {
      switch (pinID)
      {
        case 1:
          if (!Object.op_Inequality((Object) this.Target, (Object) null))
            break;
          ((Behaviour) this.mClientArea).set_enabled(true);
          this.OpenURL();
          break;
        case 2:
          if (!Object.op_Inequality((Object) this.Target, (Object) null))
            break;
          ((Behaviour) this.mClientArea).set_enabled(false);
          break;
      }
    }

    private void OpenURL()
    {
      this.uniWebView = (UniWebView) this.Target.GetComponent<UniWebView>();
      if (Object.op_Equality((Object) this.uniWebView, (Object) null))
        this.uniWebView = (UniWebView) this.Target.AddComponent<UniWebView>();
      this.uniWebView.OnLoadComplete += new UniWebView.LoadCompleteDelegate(this.OnLoadComplete);
      this.uniWebView.OnReceivedMessage += new UniWebView.ReceivedMessageDelegate(this.OnReceivedMessage);
      this.uniWebView.InsetsForScreenOreitation += new UniWebView.InsetsForScreenOreitationDelegate(this.InsetsForScreenOreitation);
      this.uniWebView.insets = new UniWebViewEdgeInsets(0, 0, 0, 0);
      this.uniWebView.SetHeaderField("Authorization", "gumi " + Network.SessionID);
      this.uniWebView.SetShowSpinnerWhenLoading(true);
      this.uniWebView.url = MonoSingleton<GameManager>.Instance.FgGAuthHost;
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
      if (!(message.scheme == "uniwebview"))
        return;
      if (string.Equals(message.path, "browser"))
      {
        if (!(message.args.ContainsKey("protocol") | message.args.ContainsKey("url")))
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
      else
      {
        if (!string.Equals(message.path, "click"))
          return;
        if (string.Equals(message.args["id"], "close"))
        {
          this.ActivateOutputLinks(3);
          Object.Destroy((Object) this.uniWebView);
          this.uniWebView = (UniWebView) null;
          ((Behaviour) this.mClientArea).set_enabled(false);
        }
        else if (string.Equals(message.args["id"], "login"))
          ;
      }
    }

    private UniWebViewEdgeInsets InsetsForScreenOreitation(UniWebView webView, UniWebViewOrientation orientation)
    {
      Vector3[] vector3Array = new Vector3[4];
      ((RectTransform) ((Component) this.mClientArea).GetComponent<RectTransform>()).GetWorldCorners(vector3Array);
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
