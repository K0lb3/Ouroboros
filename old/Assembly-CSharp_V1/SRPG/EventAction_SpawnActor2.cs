// Decompiled with JetBrains decompiler
// Type: SRPG.EventAction_SpawnActor2
// Assembly: Assembly-CSharp, Version=1.2.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9BA76916-D0BD-4DB6-A90B-FE0BCC53E511
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using System.Collections;
using System.Diagnostics;
using UnityEngine;

namespace SRPG
{
  [EventActionInfo("New/アクター/配置", "キャラクターを配置します", 6702148, 11158596)]
  public class EventAction_SpawnActor2 : EventAction
  {
    public bool ShowEquipments = true;
    [Tooltip("マス目にスナップさせるか？")]
    public bool MoveSnap = true;
    [Tooltip("地面にスナップさせるか？")]
    public bool GroundSnap = true;
    [Tooltip("表示設定")]
    public bool Display = true;
    [Tooltip("ゆれもの設定")]
    public bool Yuremono = true;
    [StringIsActorID]
    public string ActorID;
    [StringIsLocalUnitID]
    public string UnitID;
    [StringIsJobID]
    public string JobID;
    [SerializeField]
    public Vector3 Position;
    protected TacticsUnitController mController;
    public bool Persistent;
    [HideInInspector]
    public int Angle;
    [Range(0.0f, 359f)]
    public float RotationX;
    [Range(0.0f, 359f)]
    public float RotationY;
    [Range(0.0f, 359f)]
    public float RotationZ;
    public TacticsUnitController.PostureTypes Posture;

    private GameObject GetPersistentScene()
    {
      if (Object.op_Inequality((Object) SceneBattle.Instance, (Object) null))
        return SceneBattle.Instance.CurrentScene;
      return (GameObject) null;
    }

    public override bool IsPreloadAssets
    {
      get
      {
        return true;
      }
    }

    [DebuggerHidden]
    public override IEnumerator PreloadAssets()
    {
      // ISSUE: object of a compiler-generated type is created
      return (IEnumerator) new EventAction_SpawnActor2.\u003CPreloadAssets\u003Ec__Iterator6A() { \u003C\u003Ef__this = this };
    }

    public override void OnActivate()
    {
      if (Object.op_Inequality((Object) this.mController, (Object) null))
      {
        ((Component) this.mController).get_transform().set_position(this.Position);
        this.mController.CollideGround = this.GroundSnap;
        ((Component) this.mController).get_transform().set_rotation(Quaternion.Euler(this.RotationX, this.RotationY, this.RotationZ));
        this.mController.SetVisible(this.Display);
        if (!this.Yuremono)
        {
          foreach (Behaviour componentsInChild in (YuremonoInstance[]) ((Component) this.mController).get_gameObject().GetComponentsInChildren<YuremonoInstance>())
            componentsInChild.set_enabled(false);
        }
      }
      this.ActivateNext();
    }

    protected override void OnDestroy()
    {
      if (!Object.op_Inequality((Object) this.mController, (Object) null) || this.Persistent)
        return;
      Object.Destroy((Object) ((Component) this.mController).get_gameObject());
    }
  }
}
