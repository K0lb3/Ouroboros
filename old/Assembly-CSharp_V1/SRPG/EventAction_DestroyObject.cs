// Decompiled with JetBrains decompiler
// Type: SRPG.EventAction_DestroyObject
// Assembly: Assembly-CSharp, Version=1.2.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9BA76916-D0BD-4DB6-A90B-FE0BCC53E511
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using UnityEngine;

namespace SRPG
{
  [EventActionInfo("New/オブジェクト/削除", "指定のオブジェクトを削除します。", 6702148, 11158596)]
  public class EventAction_DestroyObject : EventAction
  {
    [StringIsObjectList]
    [SerializeField]
    public string TargetID;

    public override void OnActivate()
    {
      GameObject actor = EventAction.FindActor(this.TargetID);
      if (Object.op_Inequality((Object) actor, (Object) null))
        Object.Destroy((Object) actor);
      this.ActivateNext();
    }
  }
}
