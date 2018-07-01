// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_SelectServer
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 85BFDF7F-5712-4D45-9CD6-3465C703DFDF
// Assembly location: S:\Desktop\Assembly-CSharp.dll

using UnityEngine;

namespace SRPG
{
  [FlowNode.NodeType("System/SelectServer", 32741)]
  [FlowNode.Pin(1, "開発用", FlowNode.PinTypes.Input, 1)]
  [FlowNode.Pin(2, "Output", FlowNode.PinTypes.Output, 2)]
  [FlowNode.Pin(0, "安定版", FlowNode.PinTypes.Input, 0)]
  public class FlowNode_SelectServer : FlowNode
  {
    public override void OnActivate(int pinID)
    {
      string host = "http://localhost:5000/";
      if (pinID == 1)
        Network.SetDefaultHostConfigured(host);
      ((Behaviour) this).set_enabled(false);
      this.ActivateOutputLinks(2);
    }
  }
}
