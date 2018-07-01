// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_EventBannerStartCheck
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 85BFDF7F-5712-4D45-9CD6-3465C703DFDF
// Assembly location: S:\Desktop\Assembly-CSharp.dll

using System.Collections;
using System.Diagnostics;
using UnityEngine;

namespace SRPG
{
  [FlowNode.Pin(1, "Check", FlowNode.PinTypes.Input, 1)]
  [FlowNode.NodeType("SRPG/EventBannerStartCheck", 32741)]
  [FlowNode.Pin(10, "Finished", FlowNode.PinTypes.Output, 10)]
  public class FlowNode_EventBannerStartCheck : FlowNode
  {
    [SerializeField]
    private AppealItemLimitedShop LimitedShopAppealItem;

    public override void OnActivate(int pinID)
    {
      if (pinID != 1)
        return;
      this.StartCoroutine(this.UpdateEventBanner());
    }

    [DebuggerHidden]
    private IEnumerator UpdateEventBanner()
    {
      // ISSUE: object of a compiler-generated type is created
      return (IEnumerator) new FlowNode_EventBannerStartCheck.\u003CUpdateEventBanner\u003Ec__IteratorB5()
      {
        \u003C\u003Ef__this = this
      };
    }
  }
}
