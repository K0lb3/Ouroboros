// Decompiled with JetBrains decompiler
// Type: FlowNode_OpenURL
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
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
  [HideInInspector]
  [SerializeField]
  private string URL;
  [HideInInspector]
  [SerializeField]
  private bool UseVariableURL;
  [SerializeField]
  [HideInInspector]
  private bool ResetVariableValue;
  [HideInInspector]
  [SerializeField]
  private string VariableName;

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
    string url = this.URL;
    if (this.UseVariableURL)
      url = FlowNode_Variable.Get(this.VariableName);
    if (this.URLMode == FlowNode_OpenURL.URL_Mode.Direct)
      Application.OpenURL(url);
    else
      Application.OpenURL(new Uri(new Uri(this.BaseURL(this.URLMode)), url).AbsoluteUri);
    if (!this.UseVariableURL || !this.ResetVariableValue)
      return;
    FlowNode_Variable.Set(this.VariableName, string.Empty);
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
