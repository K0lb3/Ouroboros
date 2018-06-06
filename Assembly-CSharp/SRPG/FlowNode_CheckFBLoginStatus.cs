// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_CheckFBLoginStatus
// Assembly: Assembly-CSharp, Version=1.2.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9BA76916-D0BD-4DB6-A90B-FE0BCC53E511
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using GR;

namespace SRPG
{
  [FlowNode.Pin(1, "Start", FlowNode.PinTypes.Input, 0)]
  [FlowNode.Pin(2, "Complete", FlowNode.PinTypes.Output, 1)]
  [FlowNode.NodeType("System/CheckFBLoginStatus", 32741)]
  public class FlowNode_CheckFBLoginStatus : FlowNode
  {
    public override void OnActivate(int pinID)
    {
      if (pinID != 1)
        return;
      MonoSingleton<GameManager>.Instance.Player.OnFacebookLogin();
      this.ActivateOutputLinks(2);
    }
  }
}
