// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_GetShopType
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 85BFDF7F-5712-4D45-9CD6-3465C703DFDF
// Assembly location: S:\Desktop\Assembly-CSharp.dll

namespace SRPG
{
  [FlowNode.Pin(100, "Normal", FlowNode.PinTypes.Output, 0)]
  [FlowNode.Pin(101, "Tabi", FlowNode.PinTypes.Output, 1)]
  [FlowNode.Pin(106, "Multi", FlowNode.PinTypes.Output, 6)]
  [FlowNode.Pin(102, "Kimagure", FlowNode.PinTypes.Output, 2)]
  [FlowNode.Pin(103, "Monozuki", FlowNode.PinTypes.Output, 3)]
  [FlowNode.Pin(107, "AwakePiece", FlowNode.PinTypes.Output, 7)]
  [FlowNode.Pin(108, "Artifact", FlowNode.PinTypes.Output, 8)]
  [FlowNode.Pin(109, "Limited", FlowNode.PinTypes.Output, 9)]
  [FlowNode.Pin(104, "Arena", FlowNode.PinTypes.Output, 4)]
  [FlowNode.Pin(1, "Test", FlowNode.PinTypes.Input, 10)]
  [FlowNode.Pin(105, "Tour", FlowNode.PinTypes.Output, 5)]
  [FlowNode.NodeType("System/GetShopType", 32741)]
  public class FlowNode_GetShopType : FlowNode
  {
    public override void OnActivate(int pinID)
    {
      if (pinID != 1)
        return;
      switch (GlobalVars.ShopType)
      {
        case EShopType.Tabi:
          pinID = 101;
          break;
        case EShopType.Kimagure:
          pinID = 102;
          break;
        case EShopType.Monozuki:
          pinID = 103;
          break;
        case EShopType.Tour:
          pinID = 105;
          break;
        case EShopType.Arena:
          pinID = 104;
          break;
        case EShopType.Multi:
          pinID = 106;
          break;
        case EShopType.AwakePiece:
          pinID = 107;
          break;
        case EShopType.Artifact:
          pinID = 108;
          break;
        case EShopType.Limited:
          pinID = 109;
          break;
        default:
          pinID = 100;
          break;
      }
      this.ActivateOutputLinks(pinID);
    }
  }
}
