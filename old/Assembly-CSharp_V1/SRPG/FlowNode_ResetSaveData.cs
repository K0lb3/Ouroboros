// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_ResetSaveData
// Assembly: Assembly-CSharp, Version=1.2.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9BA76916-D0BD-4DB6-A90B-FE0BCC53E511
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using GR;

namespace SRPG
{
  [FlowNode.Pin(1, "Out", FlowNode.PinTypes.Output, 1)]
  [FlowNode.NodeType("SRPG/セーブデータリセット", 32741)]
  [FlowNode.Pin(0, "Reset", FlowNode.PinTypes.Input, 0)]
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
