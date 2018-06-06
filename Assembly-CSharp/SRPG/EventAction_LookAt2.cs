// Decompiled with JetBrains decompiler
// Type: SRPG.EventAction_LookAt2
// Assembly: Assembly-CSharp, Version=1.2.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9BA76916-D0BD-4DB6-A90B-FE0BCC53E511
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using UnityEngine;

namespace SRPG
{
  [EventActionInfo("New/アクター/回転", "指定のオブジェクトを回転させます。", 5592405, 4473992)]
  public class EventAction_LookAt2 : EventAction
  {
    [HideInInspector]
    public float Time = 1f;
    [HideInInspector]
    public float Speed = 90f;
    public bool m_MoveSnap = true;
    [StringIsActorList]
    public string TargetID;
    public EventAction_LookAt2.LookAtTypes LookAt;
    [StringIsActorID]
    [HideInInspector]
    public string LookAtID;
    [HideInInspector]
    public Vector3 LookAtPosition;
    public EventAction_LookAt2.RotationModes RotationMode;
    public ObjectAnimator.CurveType Curve;
    public bool Async;
    public bool Rotate3D;
    private Quaternion mStartRotation;
    private Quaternion mEndRotation;
    private Transform mTarget;
    private Vector3 mLookAt;
    public bool Reverse;
    [HideInInspector]
    [Range(0.0f, 359f)]
    public float RotateX;
    [Range(0.0f, 359f)]
    [HideInInspector]
    public float RotateY;
    [Range(0.0f, 359f)]
    [HideInInspector]
    public float RotateZ;
    private Vector3 mEulerStartRotate;
    private Vector3 mEulerEndRotate;
    private Vector3 mAddEulerAngle;
    private float mTime;

    public override void OnActivate()
    {
      GameObject actor1 = EventAction.FindActor(this.TargetID);
      if (!Object.op_Equality((Object) actor1, (Object) null))
      {
        this.mTarget = actor1.get_transform();
        if (this.LookAt == EventAction_LookAt2.LookAtTypes.GameObject)
        {
          GameObject actor2 = EventAction.FindActor(this.LookAtID);
          if (!Object.op_Equality((Object) actor2, (Object) null))
            this.LookAtPosition = actor2.get_transform().get_position();
          else
            goto label_18;
        }
        Vector3 vector3 = Vector3.op_Subtraction(this.LookAtPosition, ((Component) this.mTarget).get_transform().get_position());
        if (!this.Rotate3D)
          vector3.y = (__Null) 0.0;
        this.mStartRotation = this.mTarget.get_rotation();
        this.mEndRotation = Quaternion.LookRotation(vector3);
        if (this.LookAt == EventAction_LookAt2.LookAtTypes.WorldRotate)
          this.mEndRotation = Quaternion.Euler(this.RotateX, this.RotateY, this.RotateZ);
        // ISSUE: explicit reference operation
        this.mEulerStartRotate = ((Quaternion) @this.mStartRotation).get_eulerAngles();
        // ISSUE: explicit reference operation
        this.mEulerEndRotate = ((Quaternion) @this.mEndRotation).get_eulerAngles();
        if (!this.Reverse)
        {
          this.mAddEulerAngle = Vector3.op_Subtraction(this.mEulerEndRotate, this.mEulerStartRotate);
          this.mAddEulerAngle.x = (double) Mathf.Abs((float) this.mAddEulerAngle.x) > 180.0 || this.mAddEulerAngle.x == 0.0 ? (__Null) (double) this.mAddEulerAngle.x : (__Null) (-(double) Mathf.Sign((float) this.mAddEulerAngle.x) * (360.0 - (double) Mathf.Abs((float) this.mAddEulerAngle.x)));
          this.mAddEulerAngle.y = (double) Mathf.Abs((float) this.mAddEulerAngle.y) > 180.0 || this.mAddEulerAngle.y == 0.0 ? (__Null) (double) this.mAddEulerAngle.y : (__Null) (-(double) Mathf.Sign((float) this.mAddEulerAngle.y) * (360.0 - (double) Mathf.Abs((float) this.mAddEulerAngle.y)));
          this.mAddEulerAngle.z = (double) Mathf.Abs((float) this.mAddEulerAngle.z) > 180.0 || this.mAddEulerAngle.z == 0.0 ? (__Null) (double) this.mAddEulerAngle.z : (__Null) (-(double) Mathf.Sign((float) this.mAddEulerAngle.z) * (360.0 - (double) Mathf.Abs((float) this.mAddEulerAngle.z)));
        }
        else
        {
          this.mAddEulerAngle = Vector3.op_Subtraction(this.mEulerEndRotate, this.mEulerStartRotate);
          this.mAddEulerAngle.x = (double) Mathf.Abs((float) this.mAddEulerAngle.x) > 180.0 ? (__Null) (-(double) Mathf.Sign((float) this.mAddEulerAngle.x) * (360.0 - (double) Mathf.Abs((float) this.mAddEulerAngle.x))) : (__Null) (double) this.mAddEulerAngle.x;
          this.mAddEulerAngle.y = (double) Mathf.Abs((float) this.mAddEulerAngle.y) > 180.0 ? (__Null) (-(double) Mathf.Sign((float) this.mAddEulerAngle.y) * (360.0 - (double) Mathf.Abs((float) this.mAddEulerAngle.y))) : (__Null) (double) this.mAddEulerAngle.y;
          this.mAddEulerAngle.z = (double) Mathf.Abs((float) this.mAddEulerAngle.z) > 180.0 ? (__Null) (-(double) Mathf.Sign((float) this.mAddEulerAngle.z) * (360.0 - (double) Mathf.Abs((float) this.mAddEulerAngle.z))) : (__Null) (double) this.mAddEulerAngle.z;
        }
        if (this.RotationMode == EventAction_LookAt2.RotationModes.Speed)
          this.Time = Quaternion.Angle(this.mStartRotation, this.mEndRotation) / this.Speed;
        if ((double) this.Time <= 0.0)
        {
          this.mTarget.set_rotation(this.mEndRotation);
        }
        else
        {
          if (!this.Async)
            return;
          this.ActivateNext(true);
          return;
        }
      }
label_18:
      this.ActivateNext();
    }

    public override void Update()
    {
      this.mTime += UnityEngine.Time.get_deltaTime();
      this.mTarget.set_rotation(Quaternion.Euler(Vector3.op_Addition(this.mEulerStartRotate, Vector3.op_Multiply(this.mAddEulerAngle, this.Curve.Evaluate(Mathf.Clamp01(this.mTime / this.Time))))));
      if ((double) this.mTime < (double) this.Time)
        return;
      this.ActivateNext();
    }

    public enum LookAtTypes
    {
      WorldPosition,
      GameObject,
      WorldRotate,
    }

    public enum RotationModes
    {
      FixedTime,
      Speed,
    }
  }
}
