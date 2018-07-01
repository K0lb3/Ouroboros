// Decompiled with JetBrains decompiler
// Type: SRPG.EventAction_ActivateObjectInScene
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 85BFDF7F-5712-4D45-9CD6-3465C703DFDF
// Assembly location: S:\Desktop\Assembly-CSharp.dll

using System.Collections.Generic;
using UnityEngine;

namespace SRPG
{
  [EventActionInfo("New/オブジェクト/シーン内オブジェクトを表示", "シーン内のオブジェクトを表示・非表示します", 5592405, 4473992)]
  public class EventAction_ActivateObjectInScene : EventAction
  {
    public EventAction_ActivateObjectInScene.VisibleType visibleType;
    public string objectName;
    public Vector3 objectPosition;

    public override void OnActivate()
    {
      if (string.IsNullOrEmpty(this.objectName))
        return;
      TacticsSceneSettings lastScene = TacticsSceneSettings.LastScene;
      if (Object.op_Equality((Object) lastScene, (Object) null))
        return;
      List<Transform> transformList = new List<Transform>();
      float num = float.PositiveInfinity;
      Transform transform = (Transform) null;
      Transform[] componentsInChildren = (Transform[]) ((Component) lastScene).get_gameObject().GetComponentsInChildren<Transform>(true);
      for (int index = 0; index < componentsInChildren.Length; ++index)
      {
        if (((Object) componentsInChildren[index]).get_name() == this.objectName)
        {
          Debug.Log((object) "find");
          transformList.Add(componentsInChildren[index]);
          Vector3 vector3 = Vector3.op_Subtraction(componentsInChildren[index].get_position(), this.objectPosition);
          // ISSUE: explicit reference operation
          float sqrMagnitude = ((Vector3) @vector3).get_sqrMagnitude();
          if ((double) sqrMagnitude < (double) num)
          {
            transform = componentsInChildren[index];
            num = sqrMagnitude;
          }
        }
      }
      if (Object.op_Inequality((Object) transform, (Object) null))
        ((Component) transform).get_gameObject().SetActive(this.visibleType == EventAction_ActivateObjectInScene.VisibleType.Visible);
      this.ActivateNext();
    }

    public enum VisibleType
    {
      Visible,
      Invisible,
    }
  }
}
