// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_Notify
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using GR;
using UnityEngine;

namespace SRPG
{
  [FlowNode.Pin(10, "output", FlowNode.PinTypes.Output, 10)]
  [FlowNode.Pin(1, "Start", FlowNode.PinTypes.Input, 0)]
  [FlowNode.NodeType("System/Notify", 32741)]
  public class FlowNode_Notify : FlowNode
  {
    public GameObject NotifyListTemplate;

    public override void OnActivate(int pinID)
    {
      if (pinID == 1)
        MonoSingleton<GameManager>.Instance.InitNotifyList(this.NotifyListTemplate);
      this.ActivateOutputLinks(10);
    }
  }
}
