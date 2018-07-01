// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_PaymentRequestPurchase
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 85BFDF7F-5712-4D45-9CD6-3465C703DFDF
// Assembly location: S:\Desktop\Assembly-CSharp.dll

using GR;
using UnityEngine;

namespace SRPG
{
  [FlowNode.NodeType("Payment/RequestPurchase", 32741)]
  [FlowNode.Pin(301, "ConnectingDialogClose", FlowNode.PinTypes.Output, 301)]
  [FlowNode.Pin(300, "ConnectingDialogOpen", FlowNode.PinTypes.Output, 300)]
  [FlowNode.Pin(206, "NeedBirthday", FlowNode.PinTypes.Output, 206)]
  [FlowNode.Pin(205, "OverLimited", FlowNode.PinTypes.Output, 205)]
  [FlowNode.Pin(204, "InsufficientBalances", FlowNode.PinTypes.Output, 204)]
  [FlowNode.Pin(203, "Cancel", FlowNode.PinTypes.Output, 203)]
  [FlowNode.Pin(202, "Deferred", FlowNode.PinTypes.Output, 202)]
  [FlowNode.Pin(201, "AlreadyOwn", FlowNode.PinTypes.Output, 201)]
  [FlowNode.Pin(200, "Error", FlowNode.PinTypes.Output, 200)]
  [FlowNode.Pin(100, "Success", FlowNode.PinTypes.Output, 100)]
  [FlowNode.Pin(0, "Start", FlowNode.PinTypes.Input, 0)]
  public class FlowNode_PaymentRequestPurchase : FlowNode
  {
    private bool mSetDelegate;

    private void RemoveDelegate()
    {
      if (!this.mSetDelegate)
        return;
      MonoSingleton<PaymentManager>.Instance.OnRequestPurchase -= new PaymentManager.RequestPurchaseDelegate(this.OnRequestPurchase);
      MonoSingleton<PaymentManager>.Instance.OnRequestProcessing -= new PaymentManager.RequestProcessingDelegate(this.OnRequestProcessing);
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
        MonoSingleton<PaymentManager>.Instance.OnRequestProcessing += new PaymentManager.RequestProcessingDelegate(this.OnRequestProcessing);
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
      this.CloseProcessingDialog();
      this.ActivateOutputLinks(100);
    }

    private void Error()
    {
      DebugUtility.LogWarning("RequestPurchase error");
      ((Behaviour) this).set_enabled(false);
      this.RemoveDelegate();
      this.CloseProcessingDialog();
      this.ActivateOutputLinks(200);
    }

    private void AlreadyOwn()
    {
      DebugUtility.LogWarning("RequestPurchase alreadyown");
      ((Behaviour) this).set_enabled(false);
      this.RemoveDelegate();
      this.CloseProcessingDialog();
      this.ActivateOutputLinks(201);
    }

    private void Deferred()
    {
      DebugUtility.LogWarning("RequestPurchase deferred");
      ((Behaviour) this).set_enabled(false);
      this.RemoveDelegate();
      this.CloseProcessingDialog();
      this.ActivateOutputLinks(202);
    }

    private void Cancel()
    {
      DebugUtility.LogWarning("RequestPurchase cancel");
      ((Behaviour) this).set_enabled(false);
      this.RemoveDelegate();
      this.CloseProcessingDialog();
      this.ActivateOutputLinks(203);
    }

    private void InsufficientBalances()
    {
      DebugUtility.LogWarning("RequestPurchase insufficient balances");
      ((Behaviour) this).set_enabled(false);
      this.RemoveDelegate();
      this.CloseProcessingDialog();
      this.ActivateOutputLinks(204);
    }

    private void OverLimited()
    {
      DebugUtility.LogWarning("RequestPurchase over limited");
      ((Behaviour) this).set_enabled(false);
      this.RemoveDelegate();
      this.CloseProcessingDialog();
      this.ActivateOutputLinks(205);
    }

    private void NeedBirthday()
    {
      DebugUtility.LogWarning("RequestPurchase need birthday");
      ((Behaviour) this).set_enabled(false);
      this.RemoveDelegate();
      this.CloseProcessingDialog();
      this.ActivateOutputLinks(206);
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
        case PaymentManager.ERequestPurchaseResult.INSUFFICIENT_BALANCES:
          this.InsufficientBalances();
          break;
        case PaymentManager.ERequestPurchaseResult.OVER_LIMITED:
          this.OverLimited();
          break;
        case PaymentManager.ERequestPurchaseResult.NEED_BIRTHDAY:
          this.NeedBirthday();
          break;
        default:
          this.Error();
          break;
      }
    }

    public void OnRequestProcessing()
    {
      this.ActivateOutputLinks(300);
    }

    public void CloseProcessingDialog()
    {
      this.ActivateOutputLinks(301);
    }
  }
}
