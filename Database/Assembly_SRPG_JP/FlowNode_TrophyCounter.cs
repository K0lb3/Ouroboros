// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_TrophyCounter
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 85BFDF7F-5712-4D45-9CD6-3465C703DFDF
// Assembly location: S:\Desktop\Assembly-CSharp.dll

using GR;
using UnityEngine;

namespace SRPG
{
  [FlowNode.Pin(0, "RequestReviewURL", FlowNode.PinTypes.Input, 0)]
  [FlowNode.NodeType("Trophy/TrophyCounter", 32741)]
  [FlowNode.Pin(100, "output", FlowNode.PinTypes.Output, 100)]
  public class FlowNode_TrophyCounter : FlowNode
  {
    public string ReviewURL_Android;
    public string ReviewURL_iOS;
    public string ReviewURL_Generic;
    public string ReviewURL_Twitter;

    public override void OnActivate(int pinID)
    {
      if (pinID != 0)
        return;
      string reviewUrlGeneric = this.ReviewURL_Generic;
      if (!string.IsNullOrEmpty(reviewUrlGeneric))
        Application.OpenURL(reviewUrlGeneric);
      PlayerData player = MonoSingleton<GameManager>.Instance.Player;
      TrophyObjective[] trophiesOfType = MonoSingleton<GameManager>.Instance.GetTrophiesOfType(TrophyConditionTypes.review);
      for (int index = trophiesOfType.Length - 1; index >= 0; --index)
        player.AddTrophyCounter(trophiesOfType[index], 1);
      this.ActivateOutputLinks(100);
    }
  }
}
