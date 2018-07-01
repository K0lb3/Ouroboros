// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_PaymentOnAgree
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 85BFDF7F-5712-4D45-9CD6-3465C703DFDF
// Assembly location: S:\Desktop\Assembly-CSharp.dll

using GR;

namespace SRPG
{
  [FlowNode.NodeType("Payment/OnAgree", 32741)]
  [FlowNode.Pin(100, "Out", FlowNode.PinTypes.Output, 100)]
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
