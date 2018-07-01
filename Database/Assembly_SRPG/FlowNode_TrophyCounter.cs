// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_TrophyCounter
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using GR;
using UnityEngine;

namespace SRPG
{
  [FlowNode.NodeType("Trophy/TrophyCounter", 32741)]
  [FlowNode.Pin(0, "RequestReviewURL", FlowNode.PinTypes.Input, 0)]
  [FlowNode.Pin(100, "output", FlowNode.PinTypes.Output, 100)]
  public class FlowNode_TrophyCounter : FlowNode
  {
    public string ReviewURL_Android;
    public string ReviewURL_iOS;
    public string ReviewURL_Generic;
    public string ReviewURL_Twitter;
    public string ReviewURL_Amazon;

    public override void OnActivate(int pinID)
    {
      Debug.Log((object) "<color=yellow> asking for review </color>");
      if (pinID != 0)
        return;
      string reviewUrlAndroid = this.ReviewURL_Android;
      if (!string.IsNullOrEmpty(reviewUrlAndroid))
        Application.OpenURL(reviewUrlAndroid);
      PlayerData player = MonoSingleton<GameManager>.Instance.Player;
      TrophyObjective[] trophiesOfType = MonoSingleton<GameManager>.Instance.GetTrophiesOfType(TrophyConditionTypes.review);
      for (int index = trophiesOfType.Length - 1; index >= 0; --index)
        player.AddTrophyCounter(trophiesOfType[index], 1);
      this.ActivateOutputLinks(100);
    }
  }
}
