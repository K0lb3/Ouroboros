// Decompiled with JetBrains decompiler
// Type: SRPG.EventAction_MoveObject
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 85BFDF7F-5712-4D45-9CD6-3465C703DFDF
// Assembly location: S:\Desktop\Assembly-CSharp.dll

using UnityEngine;

namespace SRPG
{
  [EventActionInfo("オブジェクト/移動", "シーン上のオブジェクトを移動させます。", 4478293, 4491400)]
  public class EventAction_MoveObject : EventAction
  {
    public float Time = 1f;
    [StringIsActorID]
    public string TargetID;
    public ObjectAnimator.CurveType Curve;
    public Vector3 Position;
    public Vector3 Rotation;
    public bool Async;
    private ObjectAnimator mAnimator;

    public override void OnActivate()
    {
      GameObject actor = EventAction.FindActor(this.TargetID);
      if (Object.op_Equality((Object) actor, (Object) null))
      {
        this.ActivateNext();
      }
      else
      {
        Quaternion rotation = Quaternion.Euler(this.Rotation);
        this.mAnimator = ObjectAnimator.Get(actor);
        this.mAnimator.AnimateTo(this.Position, rotation, this.Time, this.Curve);
        if (!this.Async && (double) this.Time > 0.0)
          return;
        this.ActivateNext();
      }
    }

    public override void Update()
    {
      if (Object.op_Inequality((Object) this.mAnimator, (Object) null) && this.mAnimator.isMoving)
        return;
      this.ActivateNext();
    }

    public override void GoToEndState()
    {
      GameObject actor = EventAction.FindActor(this.TargetID);
      if (Object.op_Equality((Object) actor, (Object) null))
        return;
      Quaternion rotation = Quaternion.Euler(this.Rotation);
      this.mAnimator = ObjectAnimator.Get(actor);
      this.mAnimator.AnimateTo(this.Position, rotation, -1f, this.Curve);
    }
  }
}
