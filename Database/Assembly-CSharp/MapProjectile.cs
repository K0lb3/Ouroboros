// Decompiled with JetBrains decompiler
// Type: MapProjectile
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using SRPG;
using UnityEngine;

[DisallowMultipleComponent]
public class MapProjectile : MonoBehaviour
{
  public MapProjectile.HitEvent OnHit;
  public TacticsUnitController.ProjectileData mProjectileData;
  public MapProjectile.DistanceChangeEvent OnDistanceUpdate;
  public Transform CameraTransform;
  public Vector3 StartCameraTargetPosition;
  public Vector3 EndCameraTargetPosition;
  public Vector3 GoalPosition;
  public float Speed;
  public float HitDelay;
  public bool IsArrow;
  public Vector2 MidPoint;
  private Transform mTransform;
  private Vector3 mStartPosition;
  private Vector3 mCameraInterpStart;
  private float mElapsedTime;
  private Vector3 mCameraOffset;
  private bool mReachedGoal;
  private Vector3 mPositionHistory;
  private readonly float G;
  public float TimeScale;
  public float TopHeight;
  private float mMoveTime;
  private float mStartSpeed;
  private Vector3 mPrevPos;
  private bool mIsStartProc;

  public MapProjectile()
  {
    base.\u002Ector();
  }

  private void InitGravity()
  {
    float num1 = GameUtility.CalcDistance2D(Vector3.op_Subtraction(this.GoalPosition, this.mStartPosition));
    this.TopHeight = Mathf.Max(this.TopHeight, Mathf.Max((float) (this.mStartPosition.y + 0.100000001490116), (float) (this.GoalPosition.y + 0.100000001490116)));
    float num2 = this.TopHeight - (float) this.GoalPosition.y;
    float num3 = (float) (this.mStartPosition.y - this.GoalPosition.y);
    float num4 = (float) (2.0 * (double) this.G * ((double) num2 - (double) num3));
    float num5 = 2f * this.G * num2;
    float num6 = (double) num4 <= 0.0 ? 0.0f : Mathf.Sqrt(num4);
    float num7 = (double) num5 <= 0.0 ? 0.0f : Mathf.Sqrt(num5);
    this.mMoveTime = (num6 + num7) / this.G;
    float num8 = Mathf.Sin(Mathf.Atan(this.mMoveTime * num6 / num1));
    this.mStartSpeed = Mathf.Sqrt(Mathf.Pow(num1 / this.mMoveTime, 2f) + (float) (2.0 * (double) this.G * ((double) num2 - (double) num3)));
    this.mStartSpeed *= num8;
  }

  private void Gravity()
  {
    this.mPrevPos = ((Component) this).get_transform().get_position();
    float num1 = this.mElapsedTime / this.mMoveTime;
    float num2 = this.mStartSpeed * this.mElapsedTime - 0.5f * this.G * Mathf.Pow(this.mElapsedTime, 2f);
    Vector3 vector3_1 = Vector3.op_Subtraction(this.GoalPosition, this.mStartPosition);
    vector3_1.y = (__Null) 0.0;
    ((Component) this).get_transform().set_position(Vector3.op_Addition(Vector3.op_Addition(this.mStartPosition, Vector3.op_Multiply(vector3_1, num1)), Vector3.op_Multiply(Vector3.get_up(), num2)));
    Vector3 vector3_2 = Vector3.op_Subtraction(((Component) this).get_transform().get_position(), this.mPrevPos);
    // ISSUE: explicit reference operation
    if ((double) ((Vector3) @vector3_2).get_sqrMagnitude() > 0.0)
      ((Component) this).get_transform().set_rotation(Quaternion.LookRotation(vector3_2));
    if (!Object.op_Inequality((Object) this.CameraTransform, (Object) null))
      return;
    this.CameraTransform.set_position(Vector3.op_Addition(Vector3.Lerp(this.StartCameraTargetPosition, this.EndCameraTargetPosition, num1), this.mCameraOffset));
  }

  private void Start()
  {
    this.mTransform = ((Component) this).get_transform();
    this.mStartPosition = this.mTransform.get_position();
    this.mCameraInterpStart = this.mTransform.get_position();
    this.mPositionHistory = this.mStartPosition;
    if (Object.op_Inequality((Object) this.CameraTransform, (Object) null))
      this.mCameraOffset = Vector3.op_Subtraction(this.CameraTransform.get_position(), this.StartCameraTargetPosition);
    if (this.IsArrow)
      this.InitGravity();
    this.mIsStartProc = true;
    this.Update();
    this.mIsStartProc = false;
  }

  private void Update()
  {
    float deltaTime = Time.get_deltaTime();
    if (this.IsArrow)
      deltaTime *= this.TimeScale;
    this.mElapsedTime += deltaTime;
    if (!this.mReachedGoal && (double) this.Speed > 0.0)
    {
      if (this.IsArrow)
      {
        if ((double) this.mElapsedTime < (double) this.mMoveTime)
        {
          this.Gravity();
        }
        else
        {
          this.mElapsedTime = this.mMoveTime;
          this.Gravity();
          this.mReachedGoal = true;
          this.mCameraInterpStart = this.mTransform.get_position();
          this.mElapsedTime = 0.0f;
        }
      }
      else
      {
        Vector3 vector3_1 = Vector3.op_Subtraction(this.GoalPosition, this.mTransform.get_position());
        // ISSUE: explicit reference operation
        float magnitude = ((Vector3) @vector3_1).get_magnitude();
        float num = this.Speed * Time.get_deltaTime();
        if ((double) magnitude > 9.99999974737875E-05)
        {
          Vector3 vector3_2 = Vector3.op_Division(Vector3.op_Multiply(vector3_1, 1f), magnitude);
          this.mTransform.set_rotation(Quaternion.LookRotation(vector3_2));
          Transform mTransform = this.mTransform;
          mTransform.set_position(Vector3.op_Addition(mTransform.get_position(), Vector3.op_Multiply(vector3_2, num)));
        }
        if ((double) num >= (double) magnitude)
        {
          this.mTransform.set_position(this.GoalPosition);
          this.mReachedGoal = true;
          this.mCameraInterpStart = this.mTransform.get_position();
          this.mElapsedTime = 0.0f;
        }
        this.CalcMovedDistance();
        if (Object.op_Inequality((Object) this.CameraTransform, (Object) null))
        {
          Vector3 vector3_2 = Vector3.op_Subtraction(this.GoalPosition, this.mStartPosition);
          // ISSUE: explicit reference operation
          // ISSUE: explicit reference operation
          this.CameraTransform.set_position(Vector3.op_Addition(Vector3.Lerp(this.StartCameraTargetPosition, this.EndCameraTargetPosition, Vector3.Dot(Vector3.op_Subtraction(this.mTransform.get_position(), this.mStartPosition), ((Vector3) @vector3_2).get_normalized()) / ((Vector3) @vector3_2).get_magnitude()), this.mCameraOffset));
        }
      }
      if (!this.mIsStartProc)
        return;
    }
    else
      this.mReachedGoal = true;
    if (!this.mReachedGoal)
      return;
    if ((double) this.mElapsedTime < (double) this.HitDelay)
    {
      if ((double) this.Speed > 0.0 || !Object.op_Inequality((Object) this.CameraTransform, (Object) null))
        return;
      this.CameraTransform.set_position(Vector3.op_Addition(Vector3.Lerp(this.mCameraInterpStart, this.EndCameraTargetPosition, Mathf.Sin((float) ((double) this.mElapsedTime / (double) this.HitDelay * 3.14159274101257 * 0.5))), this.mCameraOffset));
    }
    else
    {
      if (Object.op_Inequality((Object) this.CameraTransform, (Object) null))
        this.CameraTransform.set_position(Vector3.op_Addition(this.EndCameraTargetPosition, this.mCameraOffset));
      if (this.OnHit != null)
        this.OnHit(this.mProjectileData);
      if (Object.op_Equality((Object) ((Component) this).get_gameObject().GetComponent<OneShotParticle>(), (Object) null))
        ((Component) this).get_gameObject().AddComponent<OneShotParticle>();
      GameUtility.StopEmitters(((Component) this).get_gameObject());
      Object.Destroy((Object) this);
    }
  }

  private void CalcMovedDistance()
  {
    if (this.OnDistanceUpdate == null)
      return;
    Vector3 position = ((Component) this).get_transform().get_position();
    Vector3 vector3_1 = Vector3.op_Subtraction(position, this.mPositionHistory);
    // ISSUE: explicit reference operation
    if ((double) ((Vector3) @vector3_1).get_magnitude() <= 9.99999974737875E-06)
      return;
    Vector3 vector3_2 = Vector3.op_Subtraction(this.GoalPosition, this.mStartPosition);
    vector3_2.y = (__Null) 0.0;
    // ISSUE: explicit reference operation
    ((Vector3) @vector3_2).Normalize();
    this.OnDistanceUpdate(((Component) this).get_gameObject(), Vector3.Dot(vector3_2, Vector3.op_Subtraction(position, this.mStartPosition)));
    this.mPositionHistory = position;
  }

  public float CalcDepth(Vector3 position)
  {
    Vector3 vector3 = Vector3.op_Subtraction(this.GoalPosition, this.mStartPosition);
    vector3.y = (__Null) 0.0;
    // ISSUE: explicit reference operation
    ((Vector3) @vector3).Normalize();
    return Vector3.Dot(vector3, Vector3.op_Subtraction(position, this.mStartPosition));
  }

  public delegate void HitEvent(TacticsUnitController.ProjectileData pd);

  public delegate void DistanceChangeEvent(GameObject go, float distance);
}
