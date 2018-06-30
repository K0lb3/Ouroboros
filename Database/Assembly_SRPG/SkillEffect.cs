namespace SRPG
{
    using GR;
    using System;
    using System.Runtime.CompilerServices;
    using UnityEngine;

    public class SkillEffect : ScriptableObject
    {
        [HideInInspector]
        public SFX StartSound;
        [HideInInspector]
        public GameObject ChantEffect;
        [HideInInspector]
        public SFX ChantSound;
        [HeaderBar("オーラ")]
        public GameObject AuraEffect;
        [HideInInspector]
        public SFX AuraSound;
        [HideInInspector]
        public AuraStopTimings StopAura;
        [HeaderBar("弾")]
        public GameObject ProjectileEffect;
        [HideInInspector]
        public SFX ProjectileSound;
        public HitReactionTypes RangedHitReactionType;
        [HeaderBar("ヒット")]
        public GameObject[] ExplosionEffects;
        [HideInInspector]
        public SFX[] ExplosionSounds;
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
        public TrajectoryTypes MapTrajectoryType;
        public float MapProjectileSpeed;
        public float MapProjectileHitDelay;
        public float MapTrajectoryTimeScale;
        public MapHitEffectTypes MapHitEffectType;
        public float MapHitEffectIntervals;
        [Tooltip("瞬間移動用として、ProjectileFrameを\n固定時間＆HitEffectなしで動作させるモード"), Space(10f)]
        public bool IsTeleportMode;
        [Space(10f), Tooltip("レーザー系の際、ProjectileFrameの\nターゲット位置を指定")]
        public eTargetTypeForLaser TargetTypeForLaser;
        [Tooltip("TargetTypeForLaser=StepFrontの際、\n前方のグリッド数を指定")]
        public int StepFrontTypeForLaser;
        [HideInInspector]
        public AnimationCurve PointDistribution;
        [HideInInspector]
        public AnimationCurve PointRandomness;

        public unsafe SkillEffect()
        {
            Keyframe[] keyframeArray2;
            Keyframe[] keyframeArray1;
            this.RangedHitReactionType = -1;
            this.HitColor = Color.get_white();
            this.ProjectileStartTime = 0.5f;
            this.ProjectileEndTime = 0.5f;
            this.MapProjectileSpeed = 1f;
            this.MapProjectileHitDelay = 0.5f;
            this.MapTrajectoryTimeScale = 1f;
            this.MapHitEffectIntervals = 0.5f;
            this.StepFrontTypeForLaser = 1;
            keyframeArray1 = new Keyframe[2];
            *(&(keyframeArray1[0])) = new Keyframe(0f, 0.5f);
            *(&(keyframeArray1[1])) = new Keyframe(1f, 0.5f);
            this.PointDistribution = new AnimationCurve(keyframeArray1);
            keyframeArray2 = new Keyframe[2];
            *(&(keyframeArray2[0])) = new Keyframe(0f, 0.2f);
            *(&(keyframeArray2[1])) = new Keyframe(1f, 0.2f);
            this.PointRandomness = new AnimationCurve(keyframeArray2);
            base..ctor();
            return;
        }

        public void SpawnExplosionEffect(int index, Vector3 position, Quaternion rotation)
        {
            GameObject obj2;
            obj2 = GameUtility.GetArrayElementSafe<GameObject>(this.ExplosionEffects, index);
            if ((obj2 != null) == null)
            {
                goto Label_0023;
            }
            GameUtility.SpawnParticle(obj2, position, rotation, null);
        Label_0023:
            return;
        }

        public enum AuraStopTimings
        {
            AfterChant,
            BeforeHit,
            AfterHit
        }

        public enum eTargetTypeForLaser
        {
            Default,
            StepFront,
            FrontCenter
        }

        public enum MapHitEffectTypes
        {
            TargetRadial,
            EachTargets,
            EachGrids,
            Directional,
            EachHits,
            InstigatorRadial
        }

        [Serializable]
        public class SFX
        {
            public string cueID;
            [CompilerGenerated]
            private bool <IsCritical>k__BackingField;

            public SFX()
            {
                base..ctor();
                return;
            }

            public void Play()
            {
                MonoSingleton<MySound>.Instance.PlaySEOneShot(this.cueID, 0f);
                if (this.IsCritical == null)
                {
                    goto Label_0034;
                }
                MonoSingleton<MySound>.Instance.PlaySEOneShot("SE_0506", 0f);
            Label_0034:
                return;
            }

            public bool IsCritical
            {
                [CompilerGenerated]
                get
                {
                    return this.<IsCritical>k__BackingField;
                }
                [CompilerGenerated]
                set
                {
                    this.<IsCritical>k__BackingField = value;
                    return;
                }
            }
        }

        public enum TrajectoryTypes
        {
            Straight,
            Arrow
        }
    }
}

