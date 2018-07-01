// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_BattlePlayBGM
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 85BFDF7F-5712-4D45-9CD6-3465C703DFDF
// Assembly location: S:\Desktop\Assembly-CSharp.dll

using UnityEngine;

namespace SRPG
{
  [FlowNode.NodeType("Battle/PlayBGM", 32741)]
  [FlowNode.Pin(1, "停止", FlowNode.PinTypes.Input, 1)]
  [FlowNode.Pin(0, "再生", FlowNode.PinTypes.Input, 0)]
  public class FlowNode_BattlePlayBGM : FlowNode
  {
    public override void OnActivate(int pinID)
    {
      switch (pinID)
      {
        case 0:
          if (!Object.op_Inequality((Object) SceneBattle.Instance, (Object) null))
            break;
          SceneBattle.Instance.PlayBGM();
          break;
        case 1:
          if (!Object.op_Inequality((Object) SceneBattle.Instance, (Object) null))
            break;
          SceneBattle.Instance.StopBGM();
          break;
      }
    }
  }
}
