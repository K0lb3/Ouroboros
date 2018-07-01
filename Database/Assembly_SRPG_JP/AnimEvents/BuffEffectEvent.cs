// Decompiled with JetBrains decompiler
// Type: SRPG.AnimEvents.BuffEffectEvent
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 85BFDF7F-5712-4D45-9CD6-3465C703DFDF
// Assembly location: S:\Desktop\Assembly-CSharp.dll

using UnityEngine;

namespace SRPG.AnimEvents
{
  public class BuffEffectEvent : AnimEvent
  {
    public bool IsDispTarget = true;
    public bool IsDispSelf;

    public override void OnStart(GameObject go)
    {
      TacticsUnitController componentInParent = (TacticsUnitController) go.GetComponentInParent<TacticsUnitController>();
      if (!Object.op_Implicit((Object) componentInParent))
        return;
      if (this.IsDispTarget)
        componentInParent.BuffEffectTarget();
      if (!this.IsDispSelf)
        return;
      componentInParent.BuffEffectSelf();
    }
  }
}
