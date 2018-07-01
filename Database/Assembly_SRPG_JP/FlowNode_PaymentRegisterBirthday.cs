// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_PaymentRegisterBirthday
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 85BFDF7F-5712-4D45-9CD6-3465C703DFDF
// Assembly location: S:\Desktop\Assembly-CSharp.dll

using GR;
using UnityEngine;

namespace SRPG
{
  [FlowNode.Pin(0, "Start", FlowNode.PinTypes.Input, 0)]
  [FlowNode.Pin(100, "Success", FlowNode.PinTypes.Output, 100)]
  [FlowNode.Pin(200, "Error", FlowNode.PinTypes.Output, 200)]
  [FlowNode.NodeType("Payment/RegisterBirthday", 32741)]
  public class FlowNode_PaymentRegisterBirthday : FlowNode
  {
    private bool mSetDelegate;

    private void RemoveDelegate()
    {
      if (!this.mSetDelegate)
        return;
      MonoSingleton<PaymentManager>.Instance.OnRegisterBirthday -= new PaymentManager.RegisterBirthdayDelegate(this.OnRegisterBirthday);
      this.mSetDelegate = false;
    }

    ~FlowNode_PaymentRegisterBirthday()
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
        MonoSingleton<PaymentManager>.Instance.OnRegisterBirthday += new PaymentManager.RegisterBirthdayDelegate(this.OnRegisterBirthday);
        this.mSetDelegate = true;
      }
      ((Behaviour) this).set_enabled(true);
      if (MonoSingleton<PaymentManager>.Instance.RegisterBirthday(GlobalVars.EditedYear, GlobalVars.EditedMonth, GlobalVars.EditedDay))
        return;
      this.Failure();
    }

    private void Success()
    {
      DebugUtility.Log("RegisterBirthday");
      ((Behaviour) this).set_enabled(false);
      this.RemoveDelegate();
      this.ActivateOutputLinks(100);
    }

    private void Failure()
    {
      DebugUtility.Log("RegisterBirthday failure");
      ((Behaviour) this).set_enabled(false);
      this.RemoveDelegate();
      this.ActivateOutputLinks(200);
    }

    public void OnRegisterBirthday(PaymentManager.ERegisterBirthdayResult result)
    {
      if (result == PaymentManager.ERegisterBirthdayResult.SUCCESS)
        this.Success();
      else
        this.Failure();
    }
  }
}
