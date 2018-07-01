// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_ReqFirstChargeState
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 85BFDF7F-5712-4D45-9CD6-3465C703DFDF
// Assembly location: S:\Desktop\Assembly-CSharp.dll

using GR;
using UnityEngine;

namespace SRPG
{
  [FlowNode.Pin(0, "Request", FlowNode.PinTypes.Input, 0)]
  [FlowNode.Pin(11, "Failed", FlowNode.PinTypes.Output, 11)]
  [FlowNode.Pin(10, "Success", FlowNode.PinTypes.Output, 10)]
  [FlowNode.NodeType("Network/ReqFirstChargeState", 32741)]
  public class FlowNode_ReqFirstChargeState : FlowNode_Network
  {
    private const int INPUT_REQUEST = 0;
    private const int OUTPUT_SUCCESS = 10;
    private const int OUTPUT_FAILED = 11;

    public override void OnActivate(int pinID)
    {
      if (pinID != 0)
        return;
      this.ExecRequest((WebAPI) new ReqFirstChargeState(new Network.ResponseCallback(((FlowNode_Network) this).ResponseCallback)));
      ((Behaviour) this).set_enabled(true);
    }

    private void _Success()
    {
      ((Behaviour) this).set_enabled(false);
      this.ActivateOutputLinks(0);
    }

    private void _Failed()
    {
      Network.RemoveAPI();
      Network.ResetError();
      ((Behaviour) this).set_enabled(false);
      this.ActivateOutputLinks(11);
    }

    public override void OnSuccess(WWWResult www)
    {
      if (Network.IsError)
      {
        this._Failed();
      }
      else
      {
        WebAPI.JSON_BodyResponse<FlowNode_ReqFirstChargeState.JSON_FirstChargeState> jsonObject = JSONParser.parseJSONObject<WebAPI.JSON_BodyResponse<FlowNode_ReqFirstChargeState.JSON_FirstChargeState>>(www.text);
        DebugUtility.Assert(jsonObject != null, "res == null");
        Network.RemoveAPI();
        MonoSingleton<GameManager>.Instance.Player.FirstChargeStatus = jsonObject.body == null ? 0 : jsonObject.body.charge_bonus;
        this._Success();
      }
    }

    public class JSON_FirstChargeState
    {
      public int charge_bonus;
    }
  }
}
