// Decompiled with JetBrains decompiler
// Type: SRPG.EventAction_WaitAsyncAnimation
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using UnityEngine;

namespace SRPG
{
  [EventActionInfo("New/同期(アニメーション)", "非同期アニメーションが完了するのを待ちます", 5592405, 4473992)]
  public class EventAction_WaitAsyncAnimation : EventAction
  {
    [StringIsActorList]
    public string ActorID;

    public override void OnActivate()
    {
    }

    public override void Update()
    {
      for (int index = 0; index < this.Sequence.Actions.Length && !Object.op_Equality((Object) this.Sequence.Actions[index], (Object) this); ++index)
      {
        if (this.Sequence.Actions[index].enabled)
        {
          if (this.Sequence.Actions[index] is EventAction_PlayAnimation3)
          {
            if ((this.Sequence.Actions[index] as EventAction_PlayAnimation3).ActorID == this.ActorID)
              return;
          }
          else if (this.Sequence.Actions[index] is EventAction_PlayAnimation4 && (this.Sequence.Actions[index] as EventAction_PlayAnimation4).ActorID == this.ActorID)
            return;
        }
      }
      this.ActivateNext();
    }
  }
}
