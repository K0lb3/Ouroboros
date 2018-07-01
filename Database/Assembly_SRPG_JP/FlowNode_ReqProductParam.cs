// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_ReqProductParam
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 85BFDF7F-5712-4D45-9CD6-3465C703DFDF
// Assembly location: S:\Desktop\Assembly-CSharp.dll

using GR;
using System.Collections;
using System.Diagnostics;
using UnityEngine;

namespace SRPG
{
  [FlowNode.Pin(1, "Success", FlowNode.PinTypes.Output, 1)]
  [FlowNode.Pin(0, "Request", FlowNode.PinTypes.Input, 0)]
  [FlowNode.NodeType("System/ReqProductParam", 32741)]
  [FlowNode.Pin(2, "Failed", FlowNode.PinTypes.Output, 2)]
  public class FlowNode_ReqProductParam : FlowNode_Network
  {
    public bool IsLoginBefore = true;

    public override void OnActivate(int pinID)
    {
      if (pinID != 0)
        return;
      if (Network.Mode == Network.EConnectMode.Online)
      {
        this.ExecRequest((WebAPI) new ReqProductParam(new Network.ResponseCallback(((FlowNode_Network) this).ResponseCallback)));
        ((Behaviour) this).set_enabled(true);
      }
      else
        this.Success();
    }

    private void Success()
    {
      ((Behaviour) this).set_enabled(false);
      this.ActivateOutputLinks(1);
    }

    private void Failure()
    {
      ((Behaviour) this).set_enabled(false);
      this.ActivateOutputLinks(2);
    }

    public override void OnSuccess(WWWResult www)
    {
      if (Object.op_Equality((Object) this, (Object) null))
        Network.RemoveAPI();
      else if (Network.IsError)
      {
        Network.EErrCode errCode = Network.ErrCode;
        this.OnFailed();
      }
      else
      {
        WebAPI.JSON_BodyResponse<JSON_ProductParamResponse> jsonObject = JSONParser.parseJSONObject<WebAPI.JSON_BodyResponse<JSON_ProductParamResponse>>(www.text);
        DebugUtility.Assert(jsonObject != null, "res == null");
        Network.RemoveAPI();
        ProductParamResponse productParamResponse = new ProductParamResponse();
        if (!productParamResponse.Deserialize(jsonObject.body))
          this.Failure();
        else
          this.StartCoroutine(this.CheckPaymentInit(productParamResponse));
      }
    }

    [DebuggerHidden]
    private IEnumerator CheckPaymentInit(ProductParamResponse param)
    {
      // ISSUE: object of a compiler-generated type is created
      return (IEnumerator) new FlowNode_ReqProductParam.\u003CCheckPaymentInit\u003Ec__IteratorC5()
      {
        param = param,
        \u003C\u0024\u003Eparam = param,
        \u003C\u003Ef__this = this
      };
    }
  }
}
