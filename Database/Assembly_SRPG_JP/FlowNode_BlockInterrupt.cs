// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_BlockInterrupt
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 85BFDF7F-5712-4D45-9CD6-3465C703DFDF
// Assembly location: S:\Desktop\Assembly-CSharp.dll

namespace SRPG
{
  [FlowNode.NodeType("BlockInterrupt", 32741)]
  [FlowNode.Pin(20, "OnCreate", FlowNode.PinTypes.Output, 0)]
  [FlowNode.Pin(10, "OnDestroy", FlowNode.PinTypes.Output, 0)]
  [FlowNode.Pin(5, "Create", FlowNode.PinTypes.Input, 0)]
  [FlowNode.Pin(0, "Destroy", FlowNode.PinTypes.Input, 0)]
  public class FlowNode_BlockInterrupt : FlowNode
  {
    public BlockInterrupt.EType Type;
    private BlockInterrupt mBlock;

    protected override void OnDestroy()
    {
      if (this.mBlock != null)
      {
        this.mBlock.Destroy();
        this.mBlock = (BlockInterrupt) null;
      }
      base.OnDestroy();
    }

    public override void OnActivate(int pinID)
    {
      switch (pinID)
      {
        case 0:
          if (this.mBlock != null)
          {
            this.mBlock.Destroy();
            this.mBlock = (BlockInterrupt) null;
          }
          this.ActivateOutputLinks(10);
          break;
        case 5:
          if (this.mBlock == null)
            this.mBlock = BlockInterrupt.Create(this.Type);
          this.ActivateOutputLinks(20);
          break;
      }
    }
  }
}
