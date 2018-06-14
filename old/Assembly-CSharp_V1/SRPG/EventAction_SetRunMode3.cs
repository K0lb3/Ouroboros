// Decompiled with JetBrains decompiler
// Type: SRPG.EventAction_SetRunMode3
// Assembly: Assembly-CSharp, Version=1.2.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9BA76916-D0BD-4DB6-A90B-FE0BCC53E511
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using System.Collections;
using System.Diagnostics;
using UnityEngine;

namespace SRPG
{
  [EventActionInfo("New/アクター/走りアニメーション変更2", "ユニットの走りアニメーションを変更します。", 5592405, 4473992)]
  public class EventAction_SetRunMode3 : EventAction
  {
    private const string MOVIE_PATH = "Movies/";
    private const string DEMO_PATH = "Demo/";
    public EventAction_SetRunMode3.PREFIX_PATH Path;
    [StringIsActorList]
    public string ActorID;
    public string AnimationName;

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
      return (IEnumerator) new EventAction_SetRunMode3.\u003CPreloadAssets\u003Ec__Iterator68() { \u003C\u003Ef__this = this };
    }

    public override void OnActivate()
    {
      GameObject actor = EventAction.FindActor(this.ActorID);
      if (Object.op_Inequality((Object) actor, (Object) null))
      {
        TacticsUnitController component = (TacticsUnitController) actor.GetComponent<TacticsUnitController>();
        if (Object.op_Inequality((Object) component, (Object) null))
          component.SetRunAnimation(this.AnimationName);
      }
      this.ActivateNext();
    }

    public enum PREFIX_PATH
    {
      Demo,
      Movie,
    }
  }
}
