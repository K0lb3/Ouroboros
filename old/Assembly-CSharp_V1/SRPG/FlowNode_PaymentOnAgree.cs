// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_PaymentOnAgree
// Assembly: Assembly-CSharp, Version=1.2.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9BA76916-D0BD-4DB6-A90B-FE0BCC53E511
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using GR;

namespace SRPG
{
  [FlowNode.Pin(100, "Out", FlowNode.PinTypes.Output, 100)]
  [FlowNode.NodeType("Payment/OnAgree", 32741)]
  [FlowNode.Pin(0, "OnAgree", FlowNode.PinTypes.Input, 0)]
  public class FlowNode_PaymentOnAgree : FlowNode
  {
    public override void OnActivate(int pinID)
    {
      if (pinID != 0)
        return;
      MonoSingleton<PaymentManager>.Instance.OnAgree();
      this.ActivateOutputLinks(100);
    }
  }
}
