// Decompiled with JetBrains decompiler
// Type: SRPG.EventAction_PlayAnimation4
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

namespace SRPG
{
  [EventActionInfo("New/アクター/アニメーション再生(複数)", "ユニットにアニメーションを再生させます。", 6702148, 11158596)]
  public class EventAction_PlayAnimation4 : EventAction
  {
    [HideInInspector]
    public EventAction_PlayAnimation4.AnimationData[] AnimationDataArray = new EventAction_PlayAnimation4.AnimationData[1]{ new EventAction_PlayAnimation4.AnimationData() };
    private bool foldout = true;
    private List<string> mAnimationIDList = new List<string>();
    private const string MOVIE_PATH = "Movies/";
    private const string DEMO_PATH = "Demo/";
    [StringIsActorList]
    public string ActorID;
    public bool Async;
    private TacticsUnitController mController;
    private int idx;
    private float mDelay;
    private bool isPlay;

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
      return (IEnumerator) new EventAction_PlayAnimation4.\u003CPreloadAssets\u003Ec__IteratorA0() { \u003C\u003Ef__this = this };
    }

    public override void OnActivate()
    {
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.mController, (UnityEngine.Object) null))
      {
        this.idx = 0;
        this.mDelay = this.AnimationDataArray[this.idx].Delay;
        if (!this.Async)
          return;
        this.ActivateNext(true);
      }
      else
        this.ActivateNext();
    }

    public override void Update()
    {
      if ((double) this.mDelay > 0.0)
        this.mDelay -= Time.get_deltaTime();
      else if (this.isPlay)
      {
        if ((double) this.mController.GetRemainingTime(this.mAnimationIDList[this.idx]) > 0.0)
          return;
        if (this.AnimationDataArray[this.idx].ApplyRootBoneAtEnd && !this.AnimationDataArray[this.idx].Loop)
        {
          this.mController.StopAnimation(this.mAnimationIDList[this.idx]);
          Transform childRecursively = GameUtility.findChildRecursively(((Component) this.mController).get_transform(), this.mController.RootMotionBoneName);
          ((Component) this.mController).get_transform().set_position(childRecursively.get_position());
          childRecursively.set_localPosition(new Vector3(0.0f, 0.0f, 0.0f));
          ((Component) this.mController).get_transform().set_rotation(Quaternion.op_Multiply(childRecursively.get_rotation(), Quaternion.Euler(90f, 0.0f, 0.0f)));
          childRecursively.set_localRotation(Quaternion.Euler(270f, 0.0f, 0.0f));
        }
        this.isPlay = false;
        ++this.idx;
        if (this.idx < this.AnimationDataArray.Length)
          this.mDelay = this.AnimationDataArray[this.idx].Delay;
        else if (this.Async)
          this.enabled = false;
        else
          this.ActivateNext();
      }
      else
      {
        this.isPlay = true;
        if (this.AnimationDataArray[this.idx].Type == EventAction_PlayAnimation4.AnimationTypes.Custom)
        {
          if (string.IsNullOrEmpty(this.mAnimationIDList[this.idx]))
            return;
          this.mController.RootMotionMode = AnimationPlayer.RootMotionModes.Velocity;
          this.mController.PlayAnimation(this.mAnimationIDList[this.idx], this.AnimationDataArray[this.idx].Loop, this.AnimationDataArray[this.idx].Interp, 0.0f);
        }
        else
        {
          this.mController.PlayIdle(0.0f);
          if (this.Async)
            this.enabled = false;
          else
            this.ActivateNext();
        }
      }
    }

    protected override void OnDestroy()
    {
      if (UnityEngine.Object.op_Equality((UnityEngine.Object) this.mController, (UnityEngine.Object) null))
        return;
      for (int index = 0; index < this.mAnimationIDList.Count; ++index)
        this.mController.UnloadAnimation(this.mAnimationIDList[index]);
    }

    public enum AnimationTypes
    {
      Custom,
      Idle,
    }

    [Serializable]
    public class AnimationData
    {
      public float Interp = 0.1f;
      public EventAction_PlayAnimation4.AnimationTypes Type;
      public string Name;
      public float Delay;
      public bool ApplyRootBoneAtEnd;
      public bool Loop;
      public EventAction_PlayAnimation4.AnimationData.PREFIX_PATH Path;

      public enum PREFIX_PATH
      {
        Demo,
        Movie,
        Default,
      }
    }
  }
}
