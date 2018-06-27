// Decompiled with JetBrains decompiler
// Type: FlowNode_MultiPlayDecideLINE
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using GR;
using UnityEngine;

[FlowNode.Pin(100, "Decided", FlowNode.PinTypes.Output, 0)]
[FlowNode.Pin(101, "Canceled", FlowNode.PinTypes.Output, 0)]
[AddComponentMenu("")]
[FlowNode.NodeType("Multi/MultiPlayDecideLINE", 58751)]
[FlowNode.Pin(0, "Decide", FlowNode.PinTypes.Input, 0)]
[FlowNode.Pin(1, "Cancel", FlowNode.PinTypes.Input, 0)]
public class FlowNode_MultiPlayDecideLINE : FlowNode
{
  public override void OnActivate(int pinID)
  {
    if (pinID == 0)
    {
      FlowNode_OnUrlSchemeLaunch.LINEParam_decided.iname = FlowNode_OnUrlSchemeLaunch.LINEParam_Pending.iname;
      FlowNode_OnUrlSchemeLaunch.LINEParam_decided.type = FlowNode_OnUrlSchemeLaunch.LINEParam_Pending.type;
      FlowNode_OnUrlSchemeLaunch.LINEParam_decided.creatorFUID = FlowNode_OnUrlSchemeLaunch.LINEParam_Pending.creatorFUID;
      FlowNode_OnUrlSchemeLaunch.LINEParam_decided.roomid = FlowNode_OnUrlSchemeLaunch.LINEParam_Pending.roomid;
      this.ActivateOutputLinks(100);
    }
    else
    {
      if (pinID != 1)
        return;
      DebugUtility.Log("Cancel MultiPlayLINE");
      MonoSingleton<UrlScheme>.Instance.ParamString = (string) null;
      FlowNode_OnUrlSchemeLaunch.LINEParam_Pending = new FlowNode_OnUrlSchemeLaunch.LINEParam();
      this.ActivateOutputLinks(101);
    }
  }
}
