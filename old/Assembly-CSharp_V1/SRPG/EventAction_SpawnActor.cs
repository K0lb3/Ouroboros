// Decompiled with JetBrains decompiler
// Type: SRPG.EventAction_SpawnActor
// Assembly: Assembly-CSharp, Version=1.2.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9BA76916-D0BD-4DB6-A90B-FE0BCC53E511
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using System.Collections;
using System.Diagnostics;
using UnityEngine;

namespace SRPG
{
  [EventActionInfo("アクター/配置", "キャラクターを配置します", 6702148, 11158596)]
  public class EventAction_SpawnActor : EventAction
  {
    public bool ShowEquipments = true;
    [StringIsActorID]
    public string ActorID;
    [StringIsUnitID]
    public string UnitID;
    [StringIsJobID]
    public string JobID;
    [SerializeField]
    private IntVector2 Position;
    private TacticsUnitController mController;
    public bool Persistent;
    [Range(0.0f, 359f)]
    public int Angle;
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
      return (IEnumerator) new EventAction_SpawnActor.\u003CPreloadAssets\u003Ec__Iterator69() { \u003C\u003Ef__this = this };
    }

    public override void OnActivate()
    {
      if (Object.op_Inequality((Object) this.mController, (Object) null))
      {
        ((Component) this.mController).get_transform().set_position(new Vector3((float) this.Position.x + 0.5f, 0.0f, (float) this.Position.y + 0.5f));
        ((Component) this.mController).get_transform().set_rotation(Quaternion.AngleAxis((float) this.Angle, Vector3.get_up()));
        this.mController.SetVisible(true);
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
