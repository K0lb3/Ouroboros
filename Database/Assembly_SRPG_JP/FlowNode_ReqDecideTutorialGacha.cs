// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_ReqDecideTutorialGacha
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 85BFDF7F-5712-4D45-9CD6-3465C703DFDF
// Assembly location: S:\Desktop\Assembly-CSharp.dll

using UnityEngine;

namespace SRPG
{
  [FlowNode.Pin(11, "Failed", FlowNode.PinTypes.Output, 11)]
  [FlowNode.NodeType("Gacha/ReqDecideTutorialGacha")]
  [FlowNode.Pin(0, "Request", FlowNode.PinTypes.Input, 0)]
  [FlowNode.Pin(10, "Success", FlowNode.PinTypes.Output, 10)]
  public class FlowNode_ReqDecideTutorialGacha : FlowNode_Network
  {
    private const int PIN_IN_REQUEST = 0;
    private const int PIN_OU_SUCCESS = 10;
    private const int PIN_OU_FAILED = 11;

    public override void OnActivate(int pinID)
    {
      if (pinID != 0 || ((Behaviour) this).get_enabled())
        return;
      ((Behaviour) this).set_enabled(true);
      this.Success();
    }

    public override void OnSuccess(WWWResult www)
    {
      if (Network.IsError)
      {
        Network.EErrCode errCode = Network.ErrCode;
        this.OnRetry();
      }
      else
        this.Success();
    }

    private void Success()
    {
      ((Behaviour) this).set_enabled(false);
      this.ActivateOutputLinks(10);
    }

    private void Failure()
    {
      ((Behaviour) this).set_enabled(false);
      this.ActivateOutputLinks(11);
    }
  }
}
