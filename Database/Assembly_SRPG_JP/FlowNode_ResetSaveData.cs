// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_ResetSaveData
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 85BFDF7F-5712-4D45-9CD6-3465C703DFDF
// Assembly location: S:\Desktop\Assembly-CSharp.dll

using GR;

namespace SRPG
{
  [FlowNode.NodeType("SRPG/セーブデータリセット", 32741)]
  [FlowNode.Pin(0, "Reset", FlowNode.PinTypes.Input, 0)]
  [FlowNode.Pin(1, "Out", FlowNode.PinTypes.Output, 1)]
  public class FlowNode_ResetSaveData : FlowNode
  {
    public override void OnActivate(int pinID)
    {
      if (pinID != 0)
        return;
      MonoSingleton<GameManager>.Instance.Player.InitPlayerPrefs();
      MonoSingleton<GameManager>.Instance.Player.LoadPlayerPrefs();
      this.ActivateOutputLinks(1);
    }
  }
}
