// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_LogoutFacebook
// Assembly: Assembly-CSharp, Version=1.2.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9BA76916-D0BD-4DB6-A90B-FE0BCC53E511
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using Facebook.Unity;
using UnityEngine;

namespace SRPG
{
  [FlowNode.Pin(1, "Success", FlowNode.PinTypes.Output, 1)]
  [FlowNode.NodeType("System/LogoutFacebook", 32741)]
  [FlowNode.Pin(0, "Request", FlowNode.PinTypes.Input, 0)]
  public class FlowNode_LogoutFacebook : FlowNode
  {
    public override void OnActivate(int pinID)
    {
      if (pinID != 0)
        return;
      if (AccessToken.get_CurrentAccessToken() != null)
      {
        PlayerPrefs.SetInt("AccountLinked", 0);
        FB.LogOut();
      }
      this.ActivateOutputLinks(1);
    }
  }
}
