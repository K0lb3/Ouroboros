// Decompiled with JetBrains decompiler
// Type: FlowNode_OpenURL
// Assembly: Assembly-CSharp, Version=1.2.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9BA76916-D0BD-4DB6-A90B-FE0BCC53E511
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using SRPG;
using System;
using UnityEngine;

[FlowNode.Pin(2, "Output", FlowNode.PinTypes.Output, 2)]
[FlowNode.Pin(1, "Input", FlowNode.PinTypes.Input, 1)]
[FlowNode.NodeType("System/OpenURL", 58751)]
public class FlowNode_OpenURL : FlowNode
{
  [SerializeField]
  private FlowNode_OpenURL.URL_Mode URLMode = FlowNode_OpenURL.URL_Mode.NewsHost;
  [SerializeField]
  private string URL;

  public override void OnActivate(int pinID)
  {
    if (pinID != 1)
      return;
    this.Open();
  }

  private string BaseURL(FlowNode_OpenURL.URL_Mode urlMode)
  {
    string str = string.Empty;
    if (this.URLMode == FlowNode_OpenURL.URL_Mode.NewsHost)
      str = Network.NewsHost;
    else if (this.URLMode == FlowNode_OpenURL.URL_Mode.SiteHost)
      str = Network.SiteHost;
    else if (this.URLMode == FlowNode_OpenURL.URL_Mode.DLHost)
      str = Network.DLHost;
    else if (this.URLMode == FlowNode_OpenURL.URL_Mode.APIHost)
      str = Network.Host;
    return str;
  }

  private void Open()
  {
    if (this.URLMode == FlowNode_OpenURL.URL_Mode.Direct)
      Application.OpenURL(this.URL);
    else
      Application.OpenURL(new Uri(new Uri(this.BaseURL(this.URLMode)), this.URL).AbsoluteUri);
  }

  public enum URL_Mode
  {
    APIHost,
    DLHost,
    SiteHost,
    NewsHost,
    Direct,
  }
}
