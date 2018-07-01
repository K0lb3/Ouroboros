// Decompiled with JetBrains decompiler
// Type: SRPG.EventAction_MoveActor2
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 85BFDF7F-5712-4D45-9CD6-3465C703DFDF
// Assembly location: S:\Desktop\Assembly-CSharp.dll

using System;
using UnityEngine;

namespace SRPG
{
  [EventActionInfo("New/アクター/移動", "アクターを指定パスに沿って移動させます。", 6702148, 11158596)]
  public class EventAction_MoveActor2 : EventAction
  {
    [SerializeField]
    [HideInInspector]
    private IntVector2[] mPoints = new IntVector2[1];
    [SerializeField]
    public Vector3[] m_WayPoints = new Vector3[1];
    [HideInInspector]
    public float Angle = -1f;
    [Tooltip("マス目にスナップするか？")]
    public bool MoveSnap = true;
    [Tooltip("地面にスナップするか？")]
    public bool GroundSnap = true;
    [Tooltip("移動速度")]
    public float RunSpeed = 4f;
    protected float BackupRunSpeed = 4f;
    [StringIsActorList]
    public string ActorID;
    public float Delay;
    public bool Async;
    public bool GotoRealPosition;
    public bool m_StartActorPos;
    protected TacticsUnitController mController;
    [Tooltip("移動する時に向きを固定化するか")]
    public bool LockRotation;
    [Tooltip("移動する時にモーションを固定化するか")]
    public bool LockMotion;
    private int mMoveIndex;
    protected bool mMoving;
    protected bool mFinishMove;
    protected bool mActorCollideGround;
    protected bool mReady;

    protected void StartMove()
    {
      if (this.GotoRealPosition && UnityEngine.Object.op_Inequality((UnityEngine.Object) this.mController, (UnityEngine.Object) null) && this.mController.Unit != null)
      {
        Array.Resize<Vector3>(ref this.m_WayPoints, this.m_WayPoints.Length + 1);
        this.m_WayPoints[this.m_WayPoints.Length - 1] = new Vector3((float) this.mController.Unit.x, 0.0f, (float) this.mController.Unit.y);
      }
      else
        this.GotoRealPosition = false;
      Vector3[] route = new Vector3[this.m_WayPoints.Length];
      for (int index = 0; index < this.m_WayPoints.Length; ++index)
        route[index] = this.m_WayPoints[index];
      if (this.m_StartActorPos && UnityEngine.Object.op_Inequality((UnityEngine.Object) this.mController, (UnityEngine.Object) null))
      {
        Transform transform = ((Component) this.mController).get_transform();
        route[0] = transform.get_position();
      }
      this.mMoveIndex = 0;
      if (!this.LockRotation && !this.LockMotion && this.GroundSnap)
      {
        double num = (double) this.mController.StartMove(route, this.Angle);
      }
      else
      {
        if (this.LockMotion)
          return;
        this.mController.StartRunning();
      }
    }

    private TacticsUnitController GetController()
    {
      TacticsUnitController tacticsUnitController = TacticsUnitController.FindByUniqueName(this.ActorID);
      if (UnityEngine.Object.op_Equality((UnityEngine.Object) tacticsUnitController, (UnityEngine.Object) null))
        tacticsUnitController = TacticsUnitController.FindByUnitID(this.ActorID);
      return tacticsUnitController;
    }

    public override void OnActivate()
    {
      this.mController = this.GetController();
      if (UnityEngine.Object.op_Equality((UnityEngine.Object) this.mController, (UnityEngine.Object) null) || this.mPoints.Length == 0)
      {
        this.ActivateNext();
      }
      else
      {
        this.mController.SetRunningSpeed(this.RunSpeed);
        this.mReady = false;
        this.mActorCollideGround = this.mController.CollideGround;
        this.mController.CollideGround = this.GroundSnap;
      }
    }

    public override void Update()
    {
      if (!this.mReady)
      {
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.mController, (UnityEngine.Object) null) && this.mController.IsLoading)
          return;
        if (this.Async)
          this.ActivateNext(true);
        this.mReady = true;
      }
      if (!this.mMoving)
      {
        if (this.mController.IsLoading)
          return;
        if ((double) this.Delay > 0.0)
        {
          this.Delay -= Time.get_deltaTime();
        }
        else
        {
          this.StartMove();
          this.mMoving = true;
        }
      }
      else
      {
        if (this.LockRotation || this.LockMotion || !this.GroundSnap)
        {
          if (this.UpdateMove())
            return;
        }
        else if (this.mController.isMoving)
          return;
        if (this.GotoRealPosition)
          this.mController.AutoUpdateRotation = true;
        if (!this.Async)
        {
          this.ActivateNext();
          this.mController.SetRunningSpeed(this.BackupRunSpeed);
          this.mController.CollideGround = this.mActorCollideGround;
        }
        else
        {
          this.enabled = false;
          this.mController.SetRunningSpeed(this.BackupRunSpeed);
          this.mController.CollideGround = this.mActorCollideGround;
        }
      }
    }

    protected bool UpdateMove()
    {
      bool flag = true;
      if (!UnityEngine.Object.op_Implicit((UnityEngine.Object) this.mController))
        return false;
      if (this.m_WayPoints.Length > this.mMoveIndex)
      {
        Vector3 position = ((Component) this.mController).get_transform().get_position();
        Vector3 vector3_1 = Vector3.op_Subtraction(this.m_WayPoints[this.mMoveIndex], position);
        // ISSUE: explicit reference operation
        if ((double) ((Vector3) @vector3_1).get_sqrMagnitude() < 9.99999974737875E-05)
        {
          if (++this.mMoveIndex >= this.m_WayPoints.Length)
          {
            if (!this.LockMotion)
              this.mController.StopRunning();
            flag = false;
          }
        }
        else
        {
          // ISSUE: explicit reference operation
          ((Vector3) @vector3_1).Normalize();
          Vector3 a = Vector3.op_Addition(position, Vector3.op_Multiply(Vector3.op_Multiply(vector3_1, this.RunSpeed), Time.get_deltaTime()));
          ((Component) this.mController).get_transform().set_position(a);
          if (!this.LockRotation)
          {
            float num1 = GameUtility.CalcDistance2D(a, this.m_WayPoints[this.mMoveIndex]);
            if ((double) num1 < 0.5 && this.mMoveIndex < this.m_WayPoints.Length - 1)
            {
              Vector3 vector3_2 = Vector3.op_Subtraction(this.m_WayPoints[this.mMoveIndex + 1], this.m_WayPoints[this.mMoveIndex]);
              vector3_2.y = (__Null) 0.0;
              // ISSUE: explicit reference operation
              ((Vector3) @vector3_2).Normalize();
              float num2 = (float) ((1.0 - (double) num1 / 0.5) * 0.5);
              ((Component) this.mController).get_transform().set_rotation(Quaternion.Slerp(Quaternion.LookRotation(vector3_1), Quaternion.LookRotation(vector3_2), num2));
            }
            else if (this.mMoveIndex > 1)
            {
              float num2 = GameUtility.CalcDistance2D(a, this.m_WayPoints[this.mMoveIndex - 1]);
              if ((double) num2 < 0.5)
              {
                Vector3 vector3_2 = Vector3.op_Subtraction(this.m_WayPoints[this.mMoveIndex - 1], this.m_WayPoints[this.mMoveIndex - 2]);
                vector3_2.y = (__Null) 0.0;
                // ISSUE: explicit reference operation
                ((Vector3) @vector3_2).Normalize();
                float num3 = (float) (0.5 + (double) num2 / 0.5 * 0.5);
                ((Component) this.mController).get_transform().set_rotation(Quaternion.Slerp(Quaternion.LookRotation(vector3_2), Quaternion.LookRotation(vector3_1), num3));
              }
              else
                ((Component) this.mController).get_transform().set_rotation(Quaternion.LookRotation(vector3_1));
            }
            else
              ((Component) this.mController).get_transform().set_rotation(Quaternion.LookRotation(vector3_1));
          }
        }
      }
      return flag;
    }
  }
}
