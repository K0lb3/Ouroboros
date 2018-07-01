// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_CopyFriendID
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 85BFDF7F-5712-4D45-9CD6-3465C703DFDF
// Assembly location: S:\Desktop\Assembly-CSharp.dll

using DeviceKit;
using GR;
using UnityEngine;

namespace SRPG
{
  [FlowNode.Pin(0, "コピー", FlowNode.PinTypes.Input, 0)]
  [FlowNode.NodeType("System/CopyFriendID", 32741)]
  [FlowNode.Pin(1, "成功", FlowNode.PinTypes.Output, 1)]
  public class FlowNode_CopyFriendID : FlowNode
  {
    public override void OnActivate(int pinID)
    {
      if (pinID != 0)
        return;
      App.SetClipboard(MonoSingleton<GameManager>.Instance.Player.FUID);
      ((Behaviour) this).set_enabled(false);
      this.ActivateOutputLinks(1);
    }
  }
}
