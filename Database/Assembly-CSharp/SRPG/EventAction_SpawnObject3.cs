// Decompiled with JetBrains decompiler
// Type: SRPG.EventAction_SpawnObject3
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using System.Collections;
using System.Diagnostics;
using UnityEngine;

namespace SRPG
{
  [EventActionInfo("New/オブジェクト/配置2", "シーンにオブジェクトを配置します。", 4478293, 4491400)]
  public class EventAction_SpawnObject3 : EventAction
  {
    public Vector3 Scale = Vector3.get_one();
    public const string ResourceDir = "EventAssets/";
    [StringIsDemoResourcePath(typeof (GameObject), "EventAssets/")]
    public string ResourceID;
    [StringIsObjectList]
    public string ObjectID;
    private LoadRequest mResourceLoadRequest;
    public bool Persistent;
    public Vector3 Position;
    [Range(0.0f, 360f)]
    private float Angle;
    [Range(0.0f, 359f)]
    public float Rotate_x;
    [Range(0.0f, 359f)]
    public float Rotate_y;
    [Range(0.0f, 359f)]
    public float Rotate_z;
    public Vector3 mousepos;
    private GameObject mGO;

    public override void OnActivate()
    {
      if (this.mResourceLoadRequest != null && Object.op_Inequality(this.mResourceLoadRequest.asset, (Object) null))
      {
        Transform transform = (this.mResourceLoadRequest.asset as GameObject).get_transform();
        Quaternion quaternion = Quaternion.Euler(this.Rotate_x, this.Rotate_y, this.Rotate_z);
        this.mGO = Object.Instantiate(this.mResourceLoadRequest.asset, Vector3.op_Addition(this.Position, transform.get_position()), Quaternion.op_Multiply(quaternion, transform.get_rotation())) as GameObject;
        this.mGO.get_transform().set_localScale(Vector3.Scale(transform.get_localScale(), this.Scale));
        if (!string.IsNullOrEmpty(this.ObjectID))
          GameUtility.RequireComponent<GameObjectID>(this.mGO).ID = this.ObjectID;
        if (this.Persistent && Object.op_Inequality((Object) TacticsSceneSettings.Instance, (Object) null))
          this.mGO.get_transform().SetParent(((Component) TacticsSceneSettings.Instance).get_transform(), true);
        this.Sequence.SpawnedObjects.Add(this.mGO);
      }
      this.ActivateNext();
    }

    protected override void OnDestroy()
    {
      base.OnDestroy();
      if (!Object.op_Inequality((Object) this.mGO, (Object) null) || this.Persistent && !Object.op_Equality((Object) this.mGO.get_transform().get_parent(), (Object) null))
        return;
      Object.Destroy((Object) this.mGO);
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
      return (IEnumerator) new EventAction_SpawnObject3.\u003CPreloadAssets\u003Ec__IteratorAA() { \u003C\u003Ef__this = this };
    }
  }
}
