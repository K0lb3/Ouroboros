// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_SendSupportEmail
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using System;
using UnityEngine;
using UnityEngine.UI;

namespace SRPG
{
  [FlowNode.Pin(3, "Failure", FlowNode.PinTypes.Output, 2)]
  [FlowNode.NodeType("Network/SendSupportEmail", 32741)]
  [FlowNode.Pin(1, "Start", FlowNode.PinTypes.Input, 0)]
  [FlowNode.Pin(2, "Success", FlowNode.PinTypes.Output, 1)]
  public class FlowNode_SendSupportEmail : FlowNode_Network
  {
    [SerializeField]
    private Text messageSubject;
    [SerializeField]
    private Text messageBody;
    [SerializeField]
    private Text email;

    public override void OnActivate(int pinID)
    {
      if (pinID != 1)
        return;
      string str = string.Format("<br>Device Language: {0}<br>Connection Type:{1}", (object) ((Enum) (object) Application.get_systemLanguage()).ToString(), (object) ((Enum) (object) Application.get_internetReachability()).ToString());
      this.messageBody.get_text();
      string message;
      if (PlayerPrefs.HasKey("PlayerName"))
        message = "App Version: " + MyApplicationPlugin.get_version() + "<br>Device Model: " + SystemInfo.get_deviceModel() + "<br>OS: " + SystemInfo.get_operatingSystem() + "<br>Player Name: " + PlayerPrefs.GetString("PlayerName") + str + "<br><br>" + this.messageBody.get_text();
      else
        message = "App Version: " + MyApplicationPlugin.get_version() + "<br>Device Model: " + SystemInfo.get_deviceModel() + "<br>OS: " + SystemInfo.get_operatingSystem() + str + "<br><br>" + this.messageBody.get_text();
      this.ExecRequest((WebAPI) new ReqSendSupportEmail(this.messageSubject.get_text(), message, this.email.get_text(), new Network.ResponseCallback(((FlowNode_Network) this).ResponseCallback)));
    }

    private void Failure()
    {
      Network.RemoveAPI();
      this.ActivateOutputLinks(3);
    }

    public override void OnSuccess(WWWResult www)
    {
      if (Network.IsError)
      {
        this.OnRetry();
      }
      else
      {
        Network.RemoveAPI();
        this.ActivateOutputLinks(2);
        ((Behaviour) this).set_enabled(false);
      }
    }
  }
}
