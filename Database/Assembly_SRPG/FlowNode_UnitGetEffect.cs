// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_UnitGetEffect
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using System.Collections;
using System.Diagnostics;
using UnityEngine;

namespace SRPG
{
  [FlowNode.Pin(1, "開始", FlowNode.PinTypes.Input, 1)]
  [FlowNode.NodeType("UI/UnitGetEffect", 32741)]
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
      return (IEnumerator) new FlowNode_UnitGetEffect.\u003CShowEffect\u003Ec__IteratorD6() { \u003C\u003Ef__this = this };
    }
  }
}
