// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_CopyClipBoard
// Assembly: Assembly-CSharp, Version=1.2.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9BA76916-D0BD-4DB6-A90B-FE0BCC53E511
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using gu3.Device;
using UnityEngine;
using UnityEngine.UI;

namespace SRPG
{
  [FlowNode.NodeType("System/CopyClipBoard", 32741)]
  [FlowNode.Pin(2, "失敗", FlowNode.PinTypes.Output, 2)]
  [FlowNode.Pin(1, "成功", FlowNode.PinTypes.Output, 1)]
  [FlowNode.Pin(0, "コピー", FlowNode.PinTypes.Input, 0)]
  public class FlowNode_CopyClipBoard : FlowNode
  {
    [SerializeField]
    private Text Target;

    public override void OnActivate(int pinID)
    {
      if (pinID != 0)
        return;
      if (this.CopyFrom(this.Target))
        this.ActivateOutputLinks(1);
      else
        this.ActivateOutputLinks(2);
    }

    private bool CopyFrom(Text target)
    {
      if (Object.op_Equality((Object) target, (Object) null))
        return false;
      return this.CopyFrom(target.get_text());
    }

    private bool CopyFrom(string text)
    {
      if (string.IsNullOrEmpty(text))
        return false;
      Application.SetClipboard(text);
      return true;
    }
  }
}
