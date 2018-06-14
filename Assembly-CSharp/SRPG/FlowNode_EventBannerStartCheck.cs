// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_EventBannerStartCheck
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using System.Collections;
using System.Diagnostics;
using UnityEngine;

namespace SRPG
{
  [FlowNode.Pin(1, "Check", FlowNode.PinTypes.Input, 1)]
  [FlowNode.Pin(10, "Finished", FlowNode.PinTypes.Output, 10)]
  [FlowNode.NodeType("SRPG/EventBannerStartCheck", 32741)]
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
      return (IEnumerator) new FlowNode_EventBannerStartCheck.\u003CUpdateEventBanner\u003Ec__IteratorC2() { \u003C\u003Ef__this = this };
    }
  }
}
