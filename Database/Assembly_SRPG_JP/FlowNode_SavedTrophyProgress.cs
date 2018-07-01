// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_SavedTrophyProgress
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 85BFDF7F-5712-4D45-9CD6-3465C703DFDF
// Assembly location: S:\Desktop\Assembly-CSharp.dll

using GR;

namespace SRPG
{
  [FlowNode.Pin(2, "UpdateByGameManager", FlowNode.PinTypes.Input, 2)]
  [FlowNode.Pin(1, "LoadTrophyProgress", FlowNode.PinTypes.Input, 1)]
  [FlowNode.Pin(0, "Success", FlowNode.PinTypes.Output, 0)]
  [FlowNode.NodeType("System/SavedTrophyProgress", 32741)]
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
