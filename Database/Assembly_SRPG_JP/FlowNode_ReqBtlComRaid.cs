// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_ReqBtlComRaid
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 85BFDF7F-5712-4D45-9CD6-3465C703DFDF
// Assembly location: S:\Desktop\Assembly-CSharp.dll

namespace SRPG
{
  [FlowNode.Pin(100, "Request", FlowNode.PinTypes.Input, 0)]
  [FlowNode.Pin(2, "Reset to Title", FlowNode.PinTypes.Output, 11)]
  [FlowNode.NodeType("System/ReqBtlComRaid", 32741)]
  [FlowNode.Pin(1, "Success", FlowNode.PinTypes.Output, 10)]
  public class FlowNode_ReqBtlComRaid : FlowNode_Network
  {
    public override void OnSuccess(WWWResult www)
    {
    }
  }
}
