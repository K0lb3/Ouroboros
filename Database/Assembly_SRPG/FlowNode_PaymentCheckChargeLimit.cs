// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_PaymentCheckChargeLimit
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using GR;
using UnityEngine;

namespace SRPG
{
  [FlowNode.NodeType("Payment/CheckChargeLimit", 32741)]
  [FlowNode.Pin(0, "Start", FlowNode.PinTypes.Input, 0)]
  [FlowNode.Pin(100, "Success", FlowNode.PinTypes.Output, 100)]
  [FlowNode.Pin(200, "Error", FlowNode.PinTypes.Output, 200)]
  [FlowNode.Pin(201, "NonAge", FlowNode.PinTypes.Output, 201)]
  [FlowNode.Pin(202, "NeedCheck", FlowNode.PinTypes.Output, 202)]
  [FlowNode.Pin(203, "NeedBirthday", FlowNode.PinTypes.Output, 203)]
  [FlowNode.Pin(204, "LimitOver", FlowNode.PinTypes.Output, 204)]
  [FlowNode.Pin(205, "VipRemains", FlowNode.PinTypes.Output, 205)]
  public class FlowNode_PaymentCheckChargeLimit : FlowNode_Network
  {
    private bool mSetDelegate;

    private void RemoveDelegate()
    {
    }

    ~FlowNode_PaymentCheckChargeLimit()
    {
      try
      {
        this.RemoveDelegate();
      }
      finally
      {
        // ISSUE: explicit finalizer call
        // ISSUE: explicit non-virtual call
        __nonvirtual (((object) this).Finalize());
      }
    }

    protected override void OnDestroy()
    {
      this.RemoveDelegate();
      base.OnDestroy();
    }

    public override void OnActivate(int pinID)
    {
      if (pinID != 0)
        return;
      if (Network.Mode == Network.EConnectMode.Online)
      {
        this.ExecRequest((WebAPI) new ReqChargeCheck(new PaymentManager.Product[1]
        {
          MonoSingleton<PaymentManager>.Instance.GetProduct(GlobalVars.SelectedProductID)
        }, true, new Network.ResponseCallback(((FlowNode_Network) this).ResponseCallback)));
        ((Behaviour) this).set_enabled(true);
      }
      else
        this.Success();
    }

    private void Success()
    {
      DebugUtility.Log("CheckChargeLimit success");
      ((Behaviour) this).set_enabled(false);
      this.RemoveDelegate();
      this.ActivateOutputLinks(100);
    }

    private void Failure()
    {
      DebugUtility.Log("CheckChargeLimit failure");
      ((Behaviour) this).set_enabled(false);
      this.RemoveDelegate();
      this.ActivateOutputLinks(200);
    }

    private void NonAge()
    {
      DebugUtility.Log("CheckChargeLimit nonage");
      ((Behaviour) this).set_enabled(false);
      this.RemoveDelegate();
      this.ActivateOutputLinks(201);
    }

    private void NeedCheck()
    {
      DebugUtility.Log("CheckChargeLimit needcheck");
      ((Behaviour) this).set_enabled(false);
      this.RemoveDelegate();
      this.ActivateOutputLinks(202);
    }

    private void NeedBirthday()
    {
      DebugUtility.Log("CheckChargeLimit needbirthday");
      ((Behaviour) this).set_enabled(false);
      this.RemoveDelegate();
      this.ActivateOutputLinks(203);
    }

    private void LimitOver()
    {
      DebugUtility.Log("CheckChargeLimit limitover");
      ((Behaviour) this).set_enabled(false);
      this.RemoveDelegate();
      this.ActivateOutputLinks(204);
    }

    private void VipRemains()
    {
      DebugUtility.Log("CheckChargeLimit vipremains");
      ((Behaviour) this).set_enabled(false);
      this.RemoveDelegate();
      this.ActivateOutputLinks(205);
    }

    public void OnCheckChargeLimit(PaymentManager.ECheckChargeLimitResult result)
    {
      switch (result)
      {
        case PaymentManager.ECheckChargeLimitResult.SUCCESS:
          this.Success();
          break;
        case PaymentManager.ECheckChargeLimitResult.NONAGE:
          this.NonAge();
          break;
        case PaymentManager.ECheckChargeLimitResult.NEED_CHECK:
          this.NeedCheck();
          break;
        case PaymentManager.ECheckChargeLimitResult.NEED_BIRTHDAY:
          this.NeedBirthday();
          break;
        case PaymentManager.ECheckChargeLimitResult.LIMIT_OVER:
          this.LimitOver();
          break;
        default:
          this.Failure();
          break;
      }
    }

    public override void OnSuccess(WWWResult www)
    {
      if (Network.IsError)
      {
        switch (Network.ErrCode)
        {
          case Network.EErrCode.ChargeAge000:
            Network.RemoveAPI();
            Network.ResetError();
            this.NeedBirthday();
            break;
          case Network.EErrCode.ChargeVipRemains:
            Network.RemoveAPI();
            Network.ResetError();
            this.VipRemains();
            break;
          default:
            this.Failure();
            break;
        }
      }
      else
      {
        WebAPI.JSON_BodyResponse<JSON_ChargeCheckResponse> jsonObject = JSONParser.parseJSONObject<WebAPI.JSON_BodyResponse<JSON_ChargeCheckResponse>>(www.text);
        DebugUtility.Assert(jsonObject != null, "res == null");
        Network.RemoveAPI();
        ChargeCheckData chargeCheckData = new ChargeCheckData();
        if (!chargeCheckData.Deserialize(jsonObject.body))
        {
          this.Failure();
        }
        else
        {
          PaymentManager instance = MonoSingleton<PaymentManager>.Instance;
          if (0 < chargeCheckData.RejectIds.Length)
            this.LimitOver();
          else if (20 > chargeCheckData.Age)
            this.NonAge();
          else if (!instance.IsAgree())
            this.NeedCheck();
          else
            this.Success();
        }
      }
    }
  }
}
