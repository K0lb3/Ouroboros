// Decompiled with JetBrains decompiler
// Type: SRPG.EventAction_PlayAnimation2
// Assembly: Assembly-CSharp, Version=1.2.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9BA76916-D0BD-4DB6-A90B-FE0BCC53E511
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using System.Collections;
using System.Diagnostics;
using UnityEngine;

namespace SRPG
{
  [EventActionInfo("New/アクター/アニメーション再生", "ユニットにアニメーションを再生させます。", 6702148, 11158596)]
  public class EventAction_PlayAnimation2 : EventAction
  {
    [StringIsActorList]
    public string ActorID;
    [HideInInspector]
    public string AnimationName;
    public EventAction_PlayAnimation2.AnimationTypes AnimationType;
    public float Delay;
    [HideInInspector]
    public bool Loop;
    public bool Async;
    private string mAnimationID;
    private TacticsUnitController mController;

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
      return (IEnumerator) new EventAction_PlayAnimation2.\u003CPreloadAssets\u003Ec__Iterator64() { \u003C\u003Ef__this = this };
    }

    public override void OnActivate()
    {
      if (Object.op_Inequality((Object) this.mController, (Object) null))
      {
        if (!string.IsNullOrEmpty(this.mAnimationID) && this.AnimationType == EventAction_PlayAnimation2.AnimationTypes.Custom && !string.IsNullOrEmpty(this.AnimationName))
        {
          if ((double) this.Delay > 0.0 && this.Async)
          {
            this.ActivateNext(true);
            return;
          }
          if ((double) this.Delay <= 0.0)
          {
            this.mController.RootMotionMode = AnimationPlayer.RootMotionModes.Velocity;
            this.mController.PlayAnimation(this.mAnimationID, this.Loop, 0.1f, 0.0f);
          }
          if (!this.Async)
            return;
          this.ActivateNext();
          return;
        }
        if (this.AnimationType == EventAction_PlayAnimation2.AnimationTypes.Idle)
        {
          if ((double) this.Delay > 0.0 && this.Async)
          {
            this.ActivateNext(true);
            return;
          }
          if ((double) this.Delay <= 0.0)
            this.mController.PlayIdle(0.0f);
          if (!this.Async)
            return;
          this.ActivateNext();
          return;
        }
      }
      this.ActivateNext();
    }

    public override void Update()
    {
      if ((double) this.Delay > 0.0)
      {
        this.Delay -= Time.get_deltaTime();
        if ((double) this.Delay > 0.0)
          return;
        if (!string.IsNullOrEmpty(this.mAnimationID) && this.AnimationType == EventAction_PlayAnimation2.AnimationTypes.Custom && !string.IsNullOrEmpty(this.AnimationName))
        {
          this.mController.RootMotionMode = AnimationPlayer.RootMotionModes.Velocity;
          this.mController.PlayAnimation(this.mAnimationID, this.Loop, 0.1f, 0.0f);
        }
        else
        {
          if (this.AnimationType != EventAction_PlayAnimation2.AnimationTypes.Idle)
            return;
          this.mController.PlayIdle(0.0f);
          if (!this.Async)
            this.ActivateNext();
          else
            this.enabled = false;
        }
      }
      else
      {
        if ((double) this.mController.GetRemainingTime(this.mAnimationID) > 0.0)
          return;
        if (!this.Async)
          this.ActivateNext();
        else
          this.enabled = false;
      }
    }

    protected override void OnDestroy()
    {
      if (!Object.op_Inequality((Object) this.mController, (Object) null) || string.IsNullOrEmpty(this.mAnimationID))
        return;
      this.mController.UnloadAnimation(this.mAnimationID);
    }

    public enum AnimationTypes
    {
      Custom,
      Idle,
    }
  }
}
