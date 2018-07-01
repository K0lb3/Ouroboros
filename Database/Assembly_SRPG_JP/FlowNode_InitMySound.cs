// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_InitMySound
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 85BFDF7F-5712-4D45-9CD6-3465C703DFDF
// Assembly location: S:\Desktop\Assembly-CSharp.dll

using System.Collections;
using System.Diagnostics;
using UnityEngine;

namespace SRPG
{
  [FlowNode.Pin(1, "Out", FlowNode.PinTypes.Output, 1)]
  [FlowNode.Pin(0, "In", FlowNode.PinTypes.Input, 0)]
  [FlowNode.NodeType("System/InitMySound", 65535)]
  public class FlowNode_InitMySound : FlowNode
  {
    public bool UseEmb;
    public bool ForceReInit;

    private void Init()
    {
      MyCriManager.Setup(this.UseEmb);
      DebugUtility.LogWarning("[MyCriManager] Setup:" + (object) this.UseEmb);
    }

    public override void OnActivate(int pinID)
    {
      if (pinID != 0)
        return;
      if (MyCriManager.IsInitialized())
      {
        if (!this.ForceReInit && MyCriManager.UsingEmb == this.UseEmb)
        {
          DebugUtility.LogWarning("[MyCriManager] NoNeed to Setup:" + (object) this.UseEmb);
          this.ActivateOutputLinks(1);
        }
        else
        {
          ((Behaviour) this).set_enabled(true);
          this.StartCoroutine(this.Restart());
        }
      }
      else
      {
        this.Init();
        this.ActivateOutputLinks(1);
      }
    }

    [DebuggerHidden]
    private IEnumerator Restart()
    {
      // ISSUE: object of a compiler-generated type is created
      return (IEnumerator) new FlowNode_InitMySound.\u003CRestart\u003Ec__IteratorB9()
      {
        \u003C\u003Ef__this = this
      };
    }
  }
}
