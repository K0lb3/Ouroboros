// Decompiled with JetBrains decompiler
// Type: SRPG.AnimEvents.ToggleTeleport
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 85BFDF7F-5712-4D45-9CD6-3465C703DFDF
// Assembly location: S:\Desktop\Assembly-CSharp.dll

using UnityEngine;

namespace SRPG.AnimEvents
{
  public class ToggleTeleport : AnimEvent
  {
    private bool mIsValid;
    private Vector3 mPosStart;
    private Vector3 mPosEnd;

    public override void OnStart(GameObject go)
    {
      TacticsUnitController componentInParent = (TacticsUnitController) go.GetComponentInParent<TacticsUnitController>();
      if (!Object.op_Implicit((Object) componentInParent))
        return;
      SceneBattle instance = SceneBattle.Instance;
      if (!Object.op_Implicit((Object) instance))
        return;
      this.mPosStart = componentInParent.CenterPosition;
      this.mPosEnd = componentInParent.GetTargetPos();
      instance.OnGimmickUpdate();
      componentInParent.CollideGround = false;
      this.mIsValid = true;
    }

    public override void OnTick(GameObject go, float ratio)
    {
      if (!this.mIsValid)
        return;
      TacticsUnitController componentInParent = (TacticsUnitController) go.GetComponentInParent<TacticsUnitController>();
      if (!Object.op_Implicit((Object) componentInParent))
        return;
      ((Component) componentInParent).get_transform().set_position(Vector3.Lerp(this.mPosStart, this.mPosEnd, ratio));
    }

    public override void OnEnd(GameObject go)
    {
      if (!this.mIsValid)
        return;
      TacticsUnitController componentInParent = (TacticsUnitController) go.GetComponentInParent<TacticsUnitController>();
      if (!Object.op_Implicit((Object) componentInParent))
        return;
      componentInParent.CollideGround = true;
      ((Component) componentInParent).get_transform().set_position(this.mPosEnd);
      componentInParent.SetStartPos(((Component) componentInParent).get_transform().get_position());
      componentInParent.LookAtTarget();
      SceneBattle instance = SceneBattle.Instance;
      if (Object.op_Implicit((Object) instance))
        instance.OnGimmickUpdate();
      this.mIsValid = false;
    }
  }
}
