// Decompiled with JetBrains decompiler
// Type: SRPG.RankMatchMatchingInfo
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 85BFDF7F-5712-4D45-9CD6-3465C703DFDF
// Assembly location: S:\Desktop\Assembly-CSharp.dll

using GR;
using UnityEngine;

namespace SRPG
{
  [FlowNode.Pin(1, "Matching", FlowNode.PinTypes.Input, 1)]
  public class RankMatchMatchingInfo : MonoBehaviour, IFlowInterface
  {
    public RankMatchMatchingInfo()
    {
      base.\u002Ector();
    }

    public void Start()
    {
      MonoSingleton<GameManager>.Instance.AudienceMode = false;
    }

    public void Activated(int pinID)
    {
      if (pinID != 1)
        return;
      ProgressWindow.OpenRankMatchLoadScreen();
    }
  }
}
