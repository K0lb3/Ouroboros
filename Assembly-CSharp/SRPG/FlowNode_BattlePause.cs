// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_BattlePause
// Assembly: Assembly-CSharp, Version=1.2.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9BA76916-D0BD-4DB6-A90B-FE0BCC53E511
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using UnityEngine;

namespace SRPG
{
  [FlowNode.Pin(100, "Out", FlowNode.PinTypes.Output, 100)]
  [FlowNode.Pin(0, "一時停止", FlowNode.PinTypes.Input, 0)]
  [FlowNode.NodeType("Battle/Pause", 32741)]
  [FlowNode.Pin(1, "再開", FlowNode.PinTypes.Input, 1)]
  public class FlowNode_BattlePause : FlowNode
  {
    private bool IsPauseAllowed
    {
      get
      {
        return GameUtility.GetCurrentScene() != GameUtility.EScene.BATTLE_MULTI;
      }
    }

    public override void OnActivate(int pinID)
    {
      if (!this.IsPauseAllowed || Object.op_Equality((Object) SceneBattle.Instance, (Object) null))
        this.ActivateOutputLinks(100);
      else if (pinID == 0)
      {
        SceneBattle.Instance.Pause(true);
        this.ActivateOutputLinks(100);
      }
      else
      {
        if (pinID != 1)
          return;
        SceneBattle.Instance.Pause(false);
        this.ActivateOutputLinks(100);
      }
    }
  }
}
