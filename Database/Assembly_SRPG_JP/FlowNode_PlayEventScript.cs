// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_PlayEventScript
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 85BFDF7F-5712-4D45-9CD6-3465C703DFDF
// Assembly location: S:\Desktop\Assembly-CSharp.dll

using System.Collections;
using System.Diagnostics;
using UnityEngine;

namespace SRPG
{
  [FlowNode.NodeType("SRPG/Play Event Script", 32741)]
  [FlowNode.Pin(10, "Finished", FlowNode.PinTypes.Output, 10)]
  [FlowNode.Pin(1, "Start", FlowNode.PinTypes.Input, 1)]
  public class FlowNode_PlayEventScript : FlowNode
  {
    public string ScriptID;

    public override void OnActivate(int pinID)
    {
      if (pinID != 1 || ((Behaviour) this).get_enabled())
        return;
      ((Behaviour) this).set_enabled(true);
      this.StartCoroutine(this.LoadAndPlayAsync("Events/" + this.ScriptID));
    }

    [DebuggerHidden]
    private IEnumerator LoadAndPlayAsync(string path)
    {
      // ISSUE: object of a compiler-generated type is created
      return (IEnumerator) new FlowNode_PlayEventScript.\u003CLoadAndPlayAsync\u003Ec__IteratorC2()
      {
        path = path,
        \u003C\u0024\u003Epath = path,
        \u003C\u003Ef__this = this
      };
    }
  }
}
