// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_InviteFriend
// Assembly: Assembly-CSharp, Version=1.2.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9BA76916-D0BD-4DB6-A90B-FE0BCC53E511
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using System.Text;
using UnityEngine;

namespace SRPG
{
  [FlowNode.Pin(1, "Line", FlowNode.PinTypes.Input, 1)]
  [FlowNode.Pin(10, "Success", FlowNode.PinTypes.Output, 10)]
  [FlowNode.NodeType("System/Invite Friend", 58751)]
  [FlowNode.Pin(0, "Whatsapp", FlowNode.PinTypes.Input, 0)]
  [FlowNode.Pin(2, "Twitter", FlowNode.PinTypes.Input, 2)]
  public class FlowNode_InviteFriend : FlowNode
  {
    public override void OnActivate(int pinID)
    {
      string str = LocalizedText.Get("sys.SHARE_SUBJECT") + "\n" + LocalizedText.Get("sys.SHARE_MESSAGE") + "\n" + LocalizedText.Get("sys.SHARE_URL");
      switch (pinID)
      {
        case 0:
          Application.OpenURL("https://api.whatsapp.com/send?text=" + WWW.EscapeURL(str + "?pid=User_invite&c=WhatsApp", Encoding.UTF8).Replace("+", "%20"));
          this.ActivateOutputLinks(10);
          break;
        case 1:
          Application.OpenURL(LocalizedText.Get("sys.MP_LINE_HTTP") + WWW.EscapeURL(str + "?pid=User_invite&c=Line", Encoding.UTF8).Replace("+", "%20"));
          this.ActivateOutputLinks(10);
          break;
        case 2:
          Application.OpenURL("http://twitter.com/intent/tweet?text=" + WWW.EscapeURL(str + "?pid=User_invite&c=Twitter", Encoding.UTF8));
          this.ActivateOutputLinks(10);
          break;
      }
    }
  }
}
