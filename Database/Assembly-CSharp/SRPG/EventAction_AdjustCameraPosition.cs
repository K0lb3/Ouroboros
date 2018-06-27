// Decompiled with JetBrains decompiler
// Type: SRPG.EventAction_AdjustCameraPosition
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using System.Collections.Generic;
using UnityEngine;

namespace SRPG
{
  [EventActionInfo("カメラ/調整", "指定したアクターが画面内に収まるようにカメラ位置を調整します。", 5592405, 4473992)]
  public class EventAction_AdjustCameraPosition : EventAction
  {
    [SerializeField]
    private string[] ActorIDs = new string[1];
    public CameraInterpSpeed InterpSpeed;

    public override void OnActivate()
    {
      Vector3 vector3_1 = Vector3.get_zero();
      List<GameObject> gameObjectList = new List<GameObject>();
      for (int index = 0; index < this.ActorIDs.Length; ++index)
      {
        GameObject actor = EventAction.FindActor(this.ActorIDs[index]);
        if (!Object.op_Equality((Object) actor, (Object) null))
        {
          vector3_1 = Vector3.op_Addition(vector3_1, actor.get_transform().get_position());
          gameObjectList.Add(actor);
        }
      }
      if (gameObjectList.Count <= 0)
      {
        this.ActivateNext();
      }
      else
      {
        Vector3 vector3_2 = Vector3.op_Multiply(vector3_1, 1f / (float) gameObjectList.Count);
        Camera main = Camera.get_main();
        Transform transform = ((Component) main).get_transform();
        // ISSUE: explicit reference operation
        // ISSUE: variable of a reference type
        Vector3& local = @vector3_2;
        // ISSUE: explicit reference operation
        // ISSUE: explicit reference operation
        (^local).y = (__Null) ((^local).y + (double) GameSettings.Instance.GameCamera_UnitHeightOffset);
        Vector3 position = Vector3.op_Subtraction(vector3_2, Vector3.op_Multiply(((Component) main).get_transform().get_forward(), GameSettings.Instance.GameCamera_EventCameraDistance));
        ObjectAnimator.Get((Component) main).AnimateTo(position, transform.get_rotation(), this.InterpSpeed.ToSpan(), ObjectAnimator.CurveType.EaseInOut);
      }
    }

    public override void Update()
    {
      if (ObjectAnimator.Get((Component) Camera.get_main()).isMoving)
        return;
      this.ActivateNext();
    }
  }
}
