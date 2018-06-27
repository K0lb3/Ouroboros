// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_SelectServer
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using UnityEngine;

namespace SRPG
{
  [FlowNode.Pin(1, "開発用", FlowNode.PinTypes.Input, 1)]
  [FlowNode.Pin(2, "Output", FlowNode.PinTypes.Output, 2)]
  [FlowNode.Pin(0, "安定版", FlowNode.PinTypes.Input, 0)]
  [FlowNode.NodeType("System/SelectServer", 32741)]
  public class FlowNode_SelectServer : FlowNode
  {
    public override void OnActivate(int pinID)
    {
      string host = "http://dev01-app.alcww.gumi.sg/";
      if (pinID == 1)
        Network.SetDefaultHostConfigured(host);
      ((Behaviour) this).set_enabled(false);
      this.ActivateOutputLinks(2);
    }
  }
}
