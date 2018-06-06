// Decompiled with JetBrains decompiler
// Type: TargetCamera
// Assembly: Assembly-CSharp, Version=1.2.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9BA76916-D0BD-4DB6-A90B-FE0BCC53E511
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using UnityEngine;

public class TargetCamera : MonoBehaviour
{
  private Vector3 mYawPitchRoll;
  private Vector3 mTargetPosition;
  private Transform mTransform;
  private float mCameraDistance;

  public TargetCamera()
  {
    base.\u002Ector();
  }

  public float CameraDistance
  {
    set
    {
      this.mCameraDistance = value;
      this.UpdatePosition();
    }
    get
    {
      return this.mCameraDistance;
    }
  }

  public Vector3 TargetPosition
  {
    get
    {
      return this.mTargetPosition;
    }
    set
    {
      this.mTargetPosition = value;
      this.UpdatePosition();
    }
  }

  public float Yaw
  {
    get
    {
      return (float) this.mYawPitchRoll.y;
    }
    set
    {
      this.mYawPitchRoll.y = (__Null) (double) value;
      this.UpdatePosition();
    }
  }

  public float Pitch
  {
    get
    {
      return (float) this.mYawPitchRoll.x;
    }
    set
    {
      this.mYawPitchRoll.x = (__Null) (double) value;
      this.UpdatePosition();
    }
  }

  public void SetPositionYaw(Vector3 pos, float yaw)
  {
    this.mTargetPosition = pos;
    this.mYawPitchRoll.y = (__Null) (double) yaw;
    this.UpdatePosition();
  }

  public void Reset()
  {
    this.mTargetPosition = Vector3.op_Addition(this.mTransform.get_position(), Vector3.op_Multiply(this.mTransform.get_forward(), this.CameraDistance));
    Vector3 forward = this.mTransform.get_forward();
    forward.y = (__Null) 0.0;
    // ISSUE: explicit reference operation
    ((Vector3) @forward).Normalize();
    this.mYawPitchRoll.x = (__Null) ((double) Mathf.Acos(Vector3.Dot(this.mTransform.get_forward(), forward)) * 57.2957801818848);
    this.mYawPitchRoll.y = (__Null) (-(double) Mathf.Atan2((float) this.mTransform.get_forward().x, (float) this.mTransform.get_forward().z) * 57.2957801818848);
    if ((double) Vector3.Dot(this.mTransform.get_forward(), Vector3.get_up()) >= 0.0)
      return;
    this.mYawPitchRoll.x = -this.mYawPitchRoll.x;
  }

  public static void CalcCameraPosition(out Vector3 position, out Quaternion rotation, Vector3 target, Vector3 rot, float distance)
  {
    rotation = TargetCamera.CalcCameraRotation(rot);
    position = Vector3.op_Subtraction(target, Vector3.op_Multiply(Quaternion.op_Multiply(rotation, Vector3.get_forward()), distance));
  }

  public static Quaternion CalcCameraRotation(Vector3 ypr)
  {
    return Quaternion.op_Multiply(Quaternion.LookRotation(Vector3.get_forward()), Quaternion.Euler((float) -ypr.x, (float) -ypr.y, (float) ypr.z));
  }

  private void UpdatePosition()
  {
    Vector3 position;
    Quaternion rotation;
    TargetCamera.CalcCameraPosition(out position, out rotation, this.mTargetPosition, this.mYawPitchRoll, this.CameraDistance);
    this.mTransform.set_position(position);
    this.mTransform.set_rotation(rotation);
  }

  private void Awake()
  {
    this.mTransform = ((Component) this).get_transform();
    this.Reset();
  }

  public static TargetCamera Get(GameObject go)
  {
    return GameUtility.RequireComponent<TargetCamera>(go);
  }

  public static TargetCamera Get(Component go)
  {
    return GameUtility.RequireComponent<TargetCamera>(go.get_gameObject());
  }
}
