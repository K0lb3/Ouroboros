// Decompiled with JetBrains decompiler
// Type: SRPG.EventAction_SpawnActorWithAnime
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 85BFDF7F-5712-4D45-9CD6-3465C703DFDF
// Assembly location: S:\Desktop\Assembly-CSharp.dll

using System.Collections;
using System.Diagnostics;
using UnityEngine;

namespace SRPG
{
  [EventActionInfo("New/アクター/配置(アニメーション再生付）", "キャラクターを配置します", 6702148, 11158596)]
  internal class EventAction_SpawnActorWithAnime : EventAction_SpawnActor2
  {
    [HideInInspector]
    public string m_AnimationName;
    [HideInInspector]
    public bool m_Loop;
    public EventAction_SpawnActorWithAnime.AnimeType m_AnimeType;
    private string m_AnimationID;
    [Tooltip("走りアニメーションを指定出来ます。")]
    public string m_RunAnimation;

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
      return (IEnumerator) new EventAction_SpawnActorWithAnime.\u003CPreloadAssets\u003Ec__Iterator96()
      {
        \u003C\u003Ef__this = this
      };
    }

    public override void OnActivate()
    {
      if (Object.op_Inequality((Object) this.mController, (Object) null))
      {
        ((Component) this.mController).get_transform().set_position(this.Position);
        this.mController.CollideGround = this.GroundSnap;
        ((Component) this.mController).get_transform().set_rotation(Quaternion.Euler(this.RotationX, this.RotationY, this.RotationZ));
        this.mController.SetVisible(this.Display);
        if (!this.Yuremono)
        {
          foreach (Behaviour componentsInChild in (YuremonoInstance[]) ((Component) this.mController).get_gameObject().GetComponentsInChildren<YuremonoInstance>())
            componentsInChild.set_enabled(false);
        }
        if (this.m_AnimeType == EventAction_SpawnActorWithAnime.AnimeType.Custom && !string.IsNullOrEmpty(this.m_AnimationName))
        {
          this.mController.RootMotionMode = AnimationPlayer.RootMotionModes.Velocity;
          this.mController.PlayAnimation(this.m_AnimationID, this.m_Loop, 0.1f, 0.0f);
        }
        else if (this.m_AnimeType == EventAction_SpawnActorWithAnime.AnimeType.Idel)
          this.mController.PlayIdle(0.0f);
        if (!string.IsNullOrEmpty(this.m_RunAnimation))
          this.mController.SetRunAnimation(this.m_RunAnimation);
      }
      this.ActivateNext();
    }

    protected override void OnDestroy()
    {
      base.OnDestroy();
      if (!Object.op_Inequality((Object) this.mController, (Object) null) || string.IsNullOrEmpty(this.m_AnimationID))
        return;
      this.mController.UnloadAnimation(this.m_AnimationID);
    }

    public enum AnimeType
    {
      Custom,
      Idel,
    }
  }
}
