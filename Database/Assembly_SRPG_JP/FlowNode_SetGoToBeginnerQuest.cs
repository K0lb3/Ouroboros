// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_SetGoToBeginnerQuest
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 85BFDF7F-5712-4D45-9CD6-3465C703DFDF
// Assembly location: S:\Desktop\Assembly-CSharp.dll

namespace SRPG
{
  [FlowNode.Pin(1, "SetFalse", FlowNode.PinTypes.Input, 1)]
  [FlowNode.NodeType("System/SetGoToBeginnerQuest", 32741)]
  [FlowNode.Pin(0, "SetTrue", FlowNode.PinTypes.Input, 0)]
  [FlowNode.Pin(100, "Result", FlowNode.PinTypes.Output, 100)]
  public class FlowNode_SetGoToBeginnerQuest : FlowNode
  {
    private const int IN_SET_TRUE = 0;
    private const int IN_SET_FALSE = 1;
    private const int OUT_RESULT = 100;
    public string Name;

    public override void OnActivate(int pinID)
    {
      switch (pinID)
      {
        case 0:
          GlobalVars.RestoreBeginnerQuest = true;
          break;
        case 1:
          GlobalVars.RestoreBeginnerQuest = false;
          break;
      }
      this.ActivateOutputLinks(100);
    }
  }
}
