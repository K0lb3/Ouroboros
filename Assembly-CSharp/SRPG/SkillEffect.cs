// Decompiled with JetBrains decompiler
// Type: SRPG.SkillEffect
// Assembly: Assembly-CSharp, Version=1.2.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9BA76916-D0BD-4DB6-A90B-FE0BCC53E511
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
      if (!Object.op_Inequality((Object) arrayElementSafe, (Object) null))
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
  }
}
