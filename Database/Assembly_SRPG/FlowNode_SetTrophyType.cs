// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_SetTrophyType
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using UnityEngine;

namespace SRPG
{
  [FlowNode.Pin(101, "Record", FlowNode.PinTypes.Input, 1)]
  [FlowNode.Pin(1, "Success", FlowNode.PinTypes.Output, 10)]
  [FlowNode.NodeType("System/SetTrophyType", 32741)]
  [FlowNode.Pin(100, "Daily", FlowNode.PinTypes.Input, 0)]
  public class FlowNode_SetTrophyType : FlowNode
  {
    public override void OnActivate(int pinID)
    {
      switch (pinID)
      {
        case 100:
          GlobalVars.SelectedTrophyType = TrophyType.Daily;
          break;
        case 101:
          GlobalVars.SelectedTrophyType = TrophyType.Record;
          break;
      }
      ((Behaviour) this).set_enabled(false);
      this.ActivateOutputLinks(1);
    }
  }
}
