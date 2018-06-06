// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_SendAlterMaster
// Assembly: Assembly-CSharp, Version=1.2.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9BA76916-D0BD-4DB6-A90B-FE0BCC53E511
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using GR;
using UnityEngine;

namespace SRPG
{
  [FlowNode.Pin(0, "Request", FlowNode.PinTypes.Input, 0)]
  [FlowNode.NodeType("System/SendAlterCheck", 32741)]
  [FlowNode.Pin(11, "Failed", FlowNode.PinTypes.Output, 11)]
  [FlowNode.Pin(10, "Success", FlowNode.PinTypes.Output, 10)]
  public class FlowNode_SendAlterMaster : FlowNode_Network
  {
    public override void OnActivate(int pinID)
    {
      if (pinID != 0)
        return;
      if (!string.IsNullOrEmpty(MonoSingleton<GameManager>.GetInstanceDirect().DigestHash) && !string.IsNullOrEmpty(MonoSingleton<GameManager>.GetInstanceDirect().AlterCheckHash))
      {
        this.ExecRequest((WebAPI) new ReqSendAlterData(new Network.ResponseCallback(((FlowNode_Network) this).ResponseCallback)));
        ((Behaviour) this).set_enabled(true);
      }
      else
        this.Success();
    }

    private void Success()
    {
      MonoSingleton<GameManager>.GetInstanceDirect().AlterCheckHash = (string) null;
      MonoSingleton<GameManager>.GetInstanceDirect().DigestHash = (string) null;
      MonoSingleton<GameManager>.GetInstanceDirect().PrevCheckHash = (string) null;
      ((Behaviour) this).set_enabled(false);
      this.ActivateOutputLinks(10);
    }

    public override void OnSuccess(WWWResult www)
    {
      if (Network.IsError)
      {
        Network.EErrCode errCode = Network.ErrCode;
      }
      Network.RemoveAPI();
      PlayerPrefs.SetString("PREV_CHECK_HASH", MonoSingleton<GameManager>.Instance.AlterCheckHash);
      this.Success();
    }
  }
}
