// Decompiled with JetBrains decompiler
// Type: SRPG.EventAction_MoveActor
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using System;
using UnityEngine;

namespace SRPG
{
  [EventActionInfo("アクター/移動", "アクターを指定パスに沿って移動させます。", 6702148, 11158596)]
  public class EventAction_MoveActor : EventAction
  {
    [SerializeField]
    [HideInInspector]
    private IntVector2[] mPoints = new IntVector2[1];
    [HideInInspector]
    public float Angle = -1f;
    [StringIsActorID]
    public string ActorID;
    public float Delay;
    public bool Async;
    public bool GotoRealPosition;
    private TacticsUnitController mController;
    private bool mMoving;
    private bool mReady;

    private void StartMove()
    {
      if (this.GotoRealPosition && UnityEngine.Object.op_Inequality((UnityEngine.Object) this.mController, (UnityEngine.Object) null) && this.mController.Unit != null)
      {
        Array.Resize<IntVector2>(ref this.mPoints, this.mPoints.Length + 1);
        this.mPoints[this.mPoints.Length - 1] = new IntVector2(this.mController.Unit.x, this.mController.Unit.y);
      }
      else
        this.GotoRealPosition = false;
      Vector3[] route = new Vector3[this.mPoints.Length];
      for (int index = 0; index < this.mPoints.Length; ++index)
        route[index] = EventAction.PointToWorld(this.mPoints[index]);
      double num = (double) this.mController.StartMove(route, this.Angle);
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
        this.ActivateNext();
      else
        this.mReady = false;
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
        if (this.mController.isMoving)
          return;
        if (this.GotoRealPosition)
          this.mController.AutoUpdateRotation = true;
        if (!this.Async)
          this.ActivateNext();
        else
          this.enabled = false;
      }
    }

    public override void GoToEndState()
    {
      if (UnityEngine.Object.op_Equality((UnityEngine.Object) this.mController, (UnityEngine.Object) null))
        this.mController = this.GetController();
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.mController, (UnityEngine.Object) null) && this.mController.IsLoading)
        return;
      Debug.LogWarning((object) ("Target grid position: " + (object) this.mPoints[this.mPoints.Length - 1]));
      Vector3 world = EventAction.PointToWorld(this.mPoints[this.mPoints.Length - 1]);
      ((Component) this.mController).get_transform().set_position(world);
      Debug.LogWarning((object) ("Setting unit " + this.mController.Unit.UnitName + " position: " + (object) world));
    }
  }
}
