// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_UnitGetEffect
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 85BFDF7F-5712-4D45-9CD6-3465C703DFDF
// Assembly location: S:\Desktop\Assembly-CSharp.dll

using System.Collections;
using System.Diagnostics;
using UnityEngine;

namespace SRPG
{
  [FlowNode.NodeType("UI/UnitGetEffect", 32741)]
  [FlowNode.Pin(1, "開始", FlowNode.PinTypes.Input, 1)]
  [FlowNode.Pin(10, "終了", FlowNode.PinTypes.Output, 10)]
  public class FlowNode_UnitGetEffect : FlowNode
  {
    private UnitGetWindowController mWindow;

    public override void OnActivate(int pinID)
    {
      if (pinID != 1)
        return;
      this.mWindow = (UnitGetWindowController) ((Component) this).get_gameObject().AddComponent<UnitGetWindowController>();
      this.mWindow.Init((UnitGetParam) null);
      this.StartCoroutine(this.ShowEffect());
    }

    [DebuggerHidden]
    private IEnumerator ShowEffect()
    {
      // ISSUE: object of a compiler-generated type is created
      return (IEnumerator) new FlowNode_UnitGetEffect.\u003CShowEffect\u003Ec__IteratorCE()
      {
        \u003C\u003Ef__this = this
      };
    }
  }
}
