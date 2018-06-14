// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_PaymentRequestPurchase
// Assembly: Assembly-CSharp, Version=1.2.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9BA76916-D0BD-4DB6-A90B-FE0BCC53E511
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using GR;
using UnityEngine;

namespace SRPG
{
  [FlowNode.Pin(0, "Start", FlowNode.PinTypes.Input, 0)]
  [FlowNode.Pin(200, "Error", FlowNode.PinTypes.Output, 200)]
  [FlowNode.Pin(201, "AlreadyOwn", FlowNode.PinTypes.Output, 201)]
  [FlowNode.Pin(203, "Cancel", FlowNode.PinTypes.Output, 203)]
  [FlowNode.NodeType("Payment/RequestPurchase", 32741)]
  [FlowNode.Pin(100, "Success", FlowNode.PinTypes.Output, 100)]
  [FlowNode.Pin(202, "Deferred", FlowNode.PinTypes.Output, 202)]
  public class FlowNode_PaymentRequestPurchase : FlowNode
  {
    private bool mSetDelegate;

    private void RemoveDelegate()
    {
      if (!this.mSetDelegate)
        return;
      MonoSingleton<PaymentManager>.Instance.OnRequestPurchase -= new PaymentManager.RequestPurchaseDelegate(this.OnRequestPurchase);
      this.mSetDelegate = false;
      DebugUtility.Log("PaymentRequestPurchase.RemoveDelegate");
    }

    ~FlowNode_PaymentRequestPurchase()
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
      if (!this.mSetDelegate)
      {
        MonoSingleton<PaymentManager>.Instance.OnRequestPurchase += new PaymentManager.RequestPurchaseDelegate(this.OnRequestPurchase);
        this.mSetDelegate = true;
        DebugUtility.Log("PaymentRequestPurchase SetDelegate");
      }
      ((Behaviour) this).set_enabled(true);
      DebugUtility.LogWarning("PaymentRequestPurchase start");
      if (MonoSingleton<PaymentManager>.Instance.RequestPurchase(GlobalVars.SelectedProductID))
        return;
      this.Error();
    }

    private void Success()
    {
      DebugUtility.LogWarning("RequestPurchase success");
      ((Behaviour) this).set_enabled(false);
      this.RemoveDelegate();
      this.ActivateOutputLinks(100);
    }

    private void Error()
    {
      DebugUtility.LogWarning("RequestPurchase error");
      ((Behaviour) this).set_enabled(false);
      this.RemoveDelegate();
      this.ActivateOutputLinks(200);
    }

    private void AlreadyOwn()
    {
      DebugUtility.LogWarning("RequestPurchase alreadyown");
      ((Behaviour) this).set_enabled(false);
      this.RemoveDelegate();
      this.ActivateOutputLinks(201);
    }

    private void Deferred()
    {
      DebugUtility.LogWarning("RequestPurchase deferred");
      ((Behaviour) this).set_enabled(false);
      this.RemoveDelegate();
      this.ActivateOutputLinks(202);
    }

    private void Cancel()
    {
      DebugUtility.LogWarning("RequestPurchase cancel");
      ((Behaviour) this).set_enabled(false);
      this.RemoveDelegate();
      this.ActivateOutputLinks(203);
    }

    public void OnRequestPurchase(PaymentManager.ERequestPurchaseResult result, PaymentManager.CoinRecord record = null)
    {
      switch (result)
      {
        case PaymentManager.ERequestPurchaseResult.NONE:
        case PaymentManager.ERequestPurchaseResult.CANCEL:
          this.Cancel();
          break;
        case PaymentManager.ERequestPurchaseResult.SUCCESS:
          if (record != null)
          {
            MonoSingleton<GameManager>.Instance.Player.SetCoinPurchaseResult(record);
            MonoSingleton<GameManager>.Instance.Player.GainVipPoint(record.additionalPaidCoin);
          }
          this.Success();
          break;
        case PaymentManager.ERequestPurchaseResult.ALREADY_OWN:
          this.AlreadyOwn();
          break;
        case PaymentManager.ERequestPurchaseResult.DEFERRED:
          this.Deferred();
          break;
        default:
          this.Error();
          break;
      }
    }
  }
}
