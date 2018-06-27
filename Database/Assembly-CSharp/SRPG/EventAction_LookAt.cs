// Decompiled with JetBrains decompiler
// Type: SRPG.EventAction_LookAt
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using UnityEngine;

namespace SRPG
{
  [EventActionInfo("アクター/回転", "指定のオブジェクトを回転させます。", 5592405, 4473992)]
  public class EventAction_LookAt : EventAction
  {
    [HideInInspector]
    public float Time = 1f;
    [HideInInspector]
    public float Speed = 90f;
    [StringIsActorID]
    public string TargetID;
    public EventAction_LookAt.LookAtTypes LookAt;
    [StringIsActorID]
    [HideInInspector]
    public string LookAtID;
    [HideInInspector]
    public Vector3 LookAtPosition;
    public EventAction_LookAt.RotationModes RotationMode;
    public ObjectAnimator.CurveType Curve;
    public bool Rotate3D;
    private Quaternion mStartRotation;
    private Quaternion mEndRotation;
    private Transform mTarget;
    private Vector3 mLookAt;
    private float mTime;

    public override void OnActivate()
    {
      GameObject actor1 = EventAction.FindActor(this.TargetID);
      if (!Object.op_Equality((Object) actor1, (Object) null))
      {
        this.mTarget = actor1.get_transform();
        if (this.LookAt == EventAction_LookAt.LookAtTypes.GameObject)
        {
          GameObject actor2 = EventAction.FindActor(this.LookAtID);
          if (!Object.op_Equality((Object) actor2, (Object) null))
            this.LookAtPosition = actor2.get_transform().get_position();
          else
            goto label_11;
        }
        Vector3 vector3 = Vector3.op_Subtraction(this.LookAtPosition, ((Component) this.mTarget).get_transform().get_position());
        if (!this.Rotate3D)
          vector3.y = (__Null) 0.0;
        this.mStartRotation = this.mTarget.get_rotation();
        this.mEndRotation = Quaternion.LookRotation(vector3);
        if (this.RotationMode == EventAction_LookAt.RotationModes.Speed)
          this.Time = Quaternion.Angle(this.mStartRotation, this.mEndRotation) / this.Speed;
        if ((double) this.Time > 0.0)
          return;
        this.mTarget.set_rotation(this.mEndRotation);
      }
label_11:
      this.ActivateNext();
    }

    public override void Update()
    {
      this.mTime += UnityEngine.Time.get_deltaTime();
      this.mTarget.set_rotation(Quaternion.Slerp(this.mStartRotation, this.mEndRotation, this.Curve.Evaluate(Mathf.Clamp01(this.mTime / this.Time))));
      if ((double) this.mTime < (double) this.Time)
        return;
      this.ActivateNext();
    }

    public enum LookAtTypes
    {
      WorldPosition,
      GameObject,
    }

    public enum RotationModes
    {
      FixedTime,
      Speed,
    }
  }
}
