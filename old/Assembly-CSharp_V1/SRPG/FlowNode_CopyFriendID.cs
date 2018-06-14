// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_CopyFriendID
// Assembly: Assembly-CSharp, Version=1.2.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9BA76916-D0BD-4DB6-A90B-FE0BCC53E511
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using GR;
using gu3.Device;
using UnityEngine;

namespace SRPG
{
  [FlowNode.Pin(0, "コピー", FlowNode.PinTypes.Input, 0)]
  [FlowNode.Pin(1, "成功", FlowNode.PinTypes.Output, 1)]
  [FlowNode.NodeType("System/CopyFriendID", 32741)]
  public class FlowNode_CopyFriendID : FlowNode
  {
    public override void OnActivate(int pinID)
    {
      if (pinID != 0)
        return;
      Application.SetClipboard(MonoSingleton<GameManager>.Instance.Player.FUID);
      ((Behaviour) this).set_enabled(false);
      this.ActivateOutputLinks(1);
    }
  }
}
