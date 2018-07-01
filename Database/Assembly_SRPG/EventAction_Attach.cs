// Decompiled with JetBrains decompiler
// Type: SRPG.EventAction_Attach
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using UnityEngine;

namespace SRPG
{
  [EventActionInfo("アタッチデタッチ", "指定オブジェクトを別オブジェクトにアタッチ/デタッチします。", 5592405, 4473992)]
  public class EventAction_Attach : EventAction
  {
    public bool Detach;
    public string AttachmentID;
    [HideInInspector]
    public string TargetID;
    [HideInInspector]
    public string BoneName;

    public override void OnActivate()
    {
      GameObject actor = EventAction.FindActor(this.AttachmentID);
      GameObject gameObject = EventAction.FindActor(this.TargetID);
      if (Object.op_Equality((Object) actor, (Object) null))
        Debug.LogError((object) (this.AttachmentID + "は存在しません。"));
      if (!this.Detach)
      {
        if (Object.op_Equality((Object) gameObject, (Object) null))
          Debug.LogError((object) (this.TargetID + "は存在しません。"));
        else if (!string.IsNullOrEmpty(this.BoneName))
        {
          Transform childRecursively = GameUtility.findChildRecursively(gameObject.get_transform(), this.BoneName);
          if (Object.op_Equality((Object) childRecursively, (Object) null))
          {
            gameObject = (GameObject) null;
            Debug.LogError((object) (this.TargetID + "の子供に" + this.BoneName + "は存在しません。"));
          }
          else
            gameObject = ((Component) childRecursively).get_gameObject();
        }
      }
      if (this.Detach)
      {
        if (Object.op_Inequality((Object) actor, (Object) null))
        {
          DefaultParentReference component = (DefaultParentReference) actor.GetComponent<DefaultParentReference>();
          if (Object.op_Inequality((Object) component, (Object) null))
          {
            actor.get_transform().SetParent(component.DefaultParent, true);
            Object.DestroyImmediate((Object) component);
          }
        }
      }
      else if (Object.op_Inequality((Object) actor, (Object) null) && Object.op_Inequality((Object) gameObject, (Object) null))
      {
        if (Object.op_Equality((Object) actor.GetComponent<DefaultParentReference>(), (Object) null))
          ((DefaultParentReference) actor.get_gameObject().AddComponent<DefaultParentReference>()).DefaultParent = actor.get_transform().get_parent();
        actor.get_transform().SetParent(gameObject.get_transform(), false);
      }
      this.ActivateNext();
    }
  }
}
