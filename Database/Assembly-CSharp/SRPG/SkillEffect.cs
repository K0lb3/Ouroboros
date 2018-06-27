// Decompiled with JetBrains decompiler
// Type: SRPG.SkillEffect
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using GR;
using System;
using UnityEngine;

namespace SRPG
{
  public class SkillEffect : ScriptableObject
  {
    [HideInInspector]
    public SkillEffect.SFX StartSound;
    [HideInInspector]
    public GameObject ChantEffect;
    [HideInInspector]
    public SkillEffect.SFX ChantSound;
    [HeaderBar("オーラ")]
    public GameObject AuraEffect;
    [HideInInspector]
    public SkillEffect.SFX AuraSound;
    [HideInInspector]
    public SkillEffect.AuraStopTimings StopAura;
    [HeaderBar("弾")]
    public GameObject ProjectileEffect;
    [HideInInspector]
    public SkillEffect.SFX ProjectileSound;
    public HitReactionTypes RangedHitReactionType;
    [HeaderBar("ヒット")]
    public GameObject[] ExplosionEffects;
    [HideInInspector]
    public SkillEffect.SFX[] ExplosionSounds;
    public bool AlwaysExplode;
    public GameObject TargetHitEffect;
    public Color HitColor;
    public float HitColorBlendTime;
    [HideInInspector]
    public AnimationClip ProjectileStart;
    [HideInInspector]
    public float ProjectileStartTime;
    [HideInInspector]
    public AnimationClip ProjectileEnd;
    [HideInInspector]
    public float ProjectileEndTime;
    [HeaderBar("弾道 (マップ)")]
    public SkillEffect.TrajectoryTypes MapTrajectoryType;
    public float MapProjectileSpeed;
    public float MapProjectileHitDelay;
    public float MapTrajectoryTimeScale;
    public SkillEffect.MapHitEffectTypes MapHitEffectType;
    public float MapHitEffectIntervals;
    [Tooltip("瞬間移動用として、ProjectileFrameを\n固定時間＆HitEffectなしで動作させるモード")]
    [Space(10f)]
    public bool IsTeleportMode;
    [Space(10f)]
    [Tooltip("レーザー系の際、ProjectileFrameの\nターゲット位置を指定")]
    public SkillEffect.eTargetTypeForLaser TargetTypeForLaser;
    [Tooltip("TargetTypeForLaser=StepFrontの際、\n前方のグリッド数を指定")]
    public int StepFrontTypeForLaser;
    [HideInInspector]
    public AnimationCurve PointDistribution;
    [HideInInspector]
    public AnimationCurve PointRandomness;

    public SkillEffect()
    {
      base.\u002Ector();
    }

    public void SpawnExplosionEffect(int index, Vector3 position, Quaternion rotation)
    {
      GameObject arrayElementSafe = GameUtility.GetArrayElementSafe<GameObject>(this.ExplosionEffects, index);
      if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) arrayElementSafe, (UnityEngine.Object) null))
        return;
      GameUtility.SpawnParticle(arrayElementSafe, position, rotation, (GameObject) null);
    }

    [Serializable]
    public class SFX
    {
      public string cueID;

      public bool IsCritical { get; set; }

      public void Play()
      {
        MonoSingleton<MySound>.Instance.PlaySEOneShot(this.cueID, 0.0f);
        if (!this.IsCritical)
          return;
        MonoSingleton<MySound>.Instance.PlaySEOneShot("SE_0506", 0.0f);
      }
    }

    public enum AuraStopTimings
    {
      AfterChant,
      BeforeHit,
      AfterHit,
    }

    public enum TrajectoryTypes
    {
      Straight,
      Arrow,
    }

    public enum MapHitEffectTypes
    {
      TargetRadial,
      EachTargets,
      EachGrids,
      Directional,
      EachHits,
      InstigatorRadial,
    }

    public enum eTargetTypeForLaser
    {
      Default,
      StepFront,
      FrontCenter,
    }
  }
}
