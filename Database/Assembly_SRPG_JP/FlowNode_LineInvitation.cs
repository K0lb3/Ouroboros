// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_LineInvitation
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 85BFDF7F-5712-4D45-9CD6-3465C703DFDF
// Assembly location: S:\Desktop\Assembly-CSharp.dll

using System.Text;
using UnityEngine;

namespace SRPG
{
  [FlowNode.Pin(200, "Read Done", FlowNode.PinTypes.Output, 0)]
  [FlowNode.Pin(200, "Send Done", FlowNode.PinTypes.Output, 0)]
  [FlowNode.Pin(110, "Read", FlowNode.PinTypes.Input, 0)]
  [FlowNode.Pin(100, "Send", FlowNode.PinTypes.Input, 0)]
  [FlowNode.NodeType("LINE/Invitation", 32741)]
  public class FlowNode_LineInvitation : FlowNode
  {
    public override void OnActivate(int pinID)
    {
      if (pinID == 100)
      {
        string str = LocalizedText.Get("sys.MP_LINE_INVITATION");
        DebugUtility.Log("LINE招待:" + str);
        Application.OpenURL(LocalizedText.Get("sys.MP_LINE_HTTP") + WWW.EscapeURL(str, Encoding.UTF8));
        this.ActivateOutputLinks(200);
      }
      else if (pinID == 110)
        ;
    }
  }
}
