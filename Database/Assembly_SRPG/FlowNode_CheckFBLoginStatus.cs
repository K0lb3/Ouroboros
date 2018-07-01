// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_CheckFBLoginStatus
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using GR;

namespace SRPG
{
  [FlowNode.NodeType("System/CheckFBLoginStatus", 32741)]
  [FlowNode.Pin(2, "Complete", FlowNode.PinTypes.Output, 1)]
  [FlowNode.Pin(1, "Start", FlowNode.PinTypes.Input, 0)]
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
