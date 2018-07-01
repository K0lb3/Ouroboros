// Decompiled with JetBrains decompiler
// Type: SRPG.AnimEvents.CameraMove
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 85BFDF7F-5712-4D45-9CD6-3465C703DFDF
// Assembly location: S:\Desktop\Assembly-CSharp.dll

using System.Collections.Generic;
using UnityEngine;

namespace SRPG.AnimEvents
{
  public class CameraMove : AnimEvent
  {
    public CameraMove.eCenterType CenterType;
    public CameraMove.eDistanceType DistanceType;

    public override void OnStart(GameObject go)
    {
      TacticsUnitController componentInParent = (TacticsUnitController) go.GetComponentInParent<TacticsUnitController>();
      if (!Object.op_Implicit((Object) componentInParent))
        return;
      SceneBattle instance = SceneBattle.Instance;
      if (!Object.op_Implicit((Object) instance))
        return;
      Vector3 center = Vector3.get_zero();
      float distance = GameSettings.Instance.GameCamera_SkillCameraDistance;
      List<Vector3> vector3List = new List<Vector3>();
      switch (this.CenterType)
      {
        case CameraMove.eCenterType.Self:
        case CameraMove.eCenterType.All:
          vector3List.Add(componentInParent.CenterPosition);
          if (this.CenterType != CameraMove.eCenterType.All)
            break;
          goto case CameraMove.eCenterType.Targets;
        case CameraMove.eCenterType.Targets:
          using (List<TacticsUnitController>.Enumerator enumerator = componentInParent.GetSkillTargets().GetEnumerator())
          {
            while (enumerator.MoveNext())
            {
              TacticsUnitController current = enumerator.Current;
              vector3List.Add(current.CenterPosition);
            }
            break;
          }
      }
      instance.GetCameraTargetView(out center, out distance, vector3List.ToArray());
      switch (this.DistanceType)
      {
        case CameraMove.eDistanceType.Skill:
          distance = GameSettings.Instance.GameCamera_SkillCameraDistance;
          break;
        case CameraMove.eDistanceType.Far:
          distance = GameSettings.Instance.GameCamera_DefaultDistance;
          break;
        case CameraMove.eDistanceType.MoreFar:
          distance = GameSettings.Instance.GameCamera_MoreFarDistance;
          break;
      }
      instance.InterpCameraTarget(center);
      instance.InterpCameraDistance(distance);
    }

    public enum eCenterType
    {
      Self,
      Targets,
      All,
    }

    public enum eDistanceType
    {
      Skill,
      Far,
      MoreFar,
      Auto,
    }
  }
}
