// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_SavedTrophyProgress
// Assembly: Assembly-CSharp, Version=1.2.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9BA76916-D0BD-4DB6-A90B-FE0BCC53E511
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using GR;

namespace SRPG
{
  [FlowNode.Pin(2, "UpdateByGameManager", FlowNode.PinTypes.Input, 2)]
  [FlowNode.NodeType("System/SavedTrophyProgress", 32741)]
  [FlowNode.Pin(0, "Success", FlowNode.PinTypes.Output, 0)]
  [FlowNode.Pin(1, "LoadTrophyProgress", FlowNode.PinTypes.Input, 1)]
  public class FlowNode_SavedTrophyProgress : FlowNode
  {
    public override void OnActivate(int pinID)
    {
      switch (pinID)
      {
        case 1:
          MonoSingleton<GameManager>.Instance.LoadUpdateTrophyList();
          break;
        case 2:
          MonoSingleton<GameManager>.Instance.update_trophy_lock.LockClear();
          break;
      }
      this.ActivateOutputLinks(0);
    }
  }
}
