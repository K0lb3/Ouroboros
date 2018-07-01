// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_ReqGallery
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 85BFDF7F-5712-4D45-9CD6-3465C703DFDF
// Assembly location: S:\Desktop\Assembly-CSharp.dll

using UnityEngine;

namespace SRPG
{
  [FlowNode.Pin(0, "Request", FlowNode.PinTypes.Input, 0)]
  [FlowNode.NodeType("System/ReqGallery", 32741)]
  [FlowNode.Pin(100, "Success", FlowNode.PinTypes.Output, 100)]
  [FlowNode.Pin(101, "InProgress", FlowNode.PinTypes.Output, 101)]
  public class FlowNode_ReqGallery : FlowNode_Network
  {
    public override void OnActivate(int pinID)
    {
      if (pinID != 0)
        return;
      if (Network.Mode == Network.EConnectMode.Online)
      {
        ((Behaviour) this).set_enabled(true);
        this.ExecRequest((WebAPI) new ReqGallery(new Network.ResponseCallback(((FlowNode_Network) this).ResponseCallback)));
      }
      else
        this.Success();
    }

    private void Success()
    {
      ((Behaviour) this).set_enabled(false);
      this.ActivateOutputLinks(100);
    }

    public override void OnSuccess(WWWResult www)
    {
      if (Network.IsError)
      {
        if (Network.ErrCode == Network.EErrCode.Gallery_MigrationInProgress)
        {
          Network.ResetError();
          Network.RemoveAPI();
          this.ActivateOutputLinks(101);
        }
        else
          this.OnRetry();
      }
      else
      {
        Network.RemoveAPI();
        this.Success();
      }
    }
  }
}
