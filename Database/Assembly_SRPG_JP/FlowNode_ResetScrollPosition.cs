// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_ResetScrollPosition
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 85BFDF7F-5712-4D45-9CD6-3465C703DFDF
// Assembly location: S:\Desktop\Assembly-CSharp.dll

using System.Collections;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.UI;

namespace SRPG
{
  [FlowNode.Pin(0, "Input", FlowNode.PinTypes.Input, 0)]
  [FlowNode.Pin(10, "Output", FlowNode.PinTypes.Output, 10)]
  [FlowNode.NodeType("UI/ResetScrollPosition", 32741)]
  public class FlowNode_ResetScrollPosition : FlowNode
  {
    [SerializeField]
    private ScrollRect ScrollParent;
    [SerializeField]
    private Transform ResetTransform;
    private float mDecelerationRate;

    public override void OnActivate(int pinID)
    {
      if (pinID != 0)
        return;
      this.ResetScrollPosition();
      FlowNode_GameObject.ActivateOutputLinks((Component) this, 10);
    }

    private void ResetScrollPosition()
    {
      if (Object.op_Equality((Object) this.ScrollParent, (Object) null))
        return;
      this.mDecelerationRate = this.ScrollParent.get_decelerationRate();
      this.ScrollParent.set_decelerationRate(0.0f);
      RectTransform resetTransform = this.ResetTransform as RectTransform;
      resetTransform.set_anchoredPosition(new Vector2((float) resetTransform.get_anchoredPosition().x, 0.0f));
      this.StartCoroutine(this.RefreshScrollRect());
    }

    [DebuggerHidden]
    private IEnumerator RefreshScrollRect()
    {
      // ISSUE: object of a compiler-generated type is created
      return (IEnumerator) new FlowNode_ResetScrollPosition.\u003CRefreshScrollRect\u003Ec__IteratorC7()
      {
        \u003C\u003Ef__this = this
      };
    }
  }
}
