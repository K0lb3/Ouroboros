// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_CopyClipBoard
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 85BFDF7F-5712-4D45-9CD6-3465C703DFDF
// Assembly location: S:\Desktop\Assembly-CSharp.dll

using UnityEngine;
using UnityEngine.UI;

namespace SRPG
{
  [FlowNode.Pin(2, "失敗", FlowNode.PinTypes.Output, 2)]
  [FlowNode.Pin(1, "成功", FlowNode.PinTypes.Output, 1)]
  [FlowNode.Pin(0, "コピー", FlowNode.PinTypes.Input, 0)]
  [FlowNode.NodeType("System/CopyClipBoard", 32741)]
  public class FlowNode_CopyClipBoard : FlowNode
  {
    [SerializeField]
    private Text Target;
    public string LocalizeText;

    public override void OnActivate(int pinID)
    {
      if (pinID != 0)
        return;
      if (string.IsNullOrEmpty(this.LocalizeText))
      {
        if (this.CopyFrom(this.Target))
          this.ActivateOutputLinks(1);
        else
          this.ActivateOutputLinks(2);
      }
      else if (this.CopyFrom(LocalizedText.Get(this.LocalizeText)))
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
      text = text.Replace("<br>", "\n");
      GUIUtility.set_systemCopyBuffer(text);
      return true;
    }
  }
}
