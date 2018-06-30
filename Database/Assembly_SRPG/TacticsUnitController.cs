namespace SRPG
{
    using GR;
    using SRPG.AnimEvents;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;
    using UnityEngine;

    public class TacticsUnitController : UnitController
    {
        private const string ID_IDLE = "IDLE";
        private const string ID_RUN = "RUN";
        private const string ID_STEP = "STEP";
        private const string ID_PICKUP = "PICK";
        private const string ID_BADSTATUS = "BAD";
        private const string ID_CUSTOMRUN = "RUN_";
        private const string ID_FALL_LOOP = "FLLP";
        private const string ID_FALL_END = "FLEN";
        private const string ID_CLIMBUP = "CLMB";
        private const string ID_GENKIDAMA = "GENK";
        public const float ADJUST_MOVE_DIST = 0.3f;
        private const string ID_BATTLE_RUN = "B_RUN";
        private const string ID_BATTLE_SKILL = "B_SKL";
        private const string ID_BATTLE_BACKSTEP = "B_BS";
        private const string ID_BATTLE_DAMAGE_NORMAL = "B_DMG0";
        private const string ID_BATTLE_DAMAGE_AIR = "B_DMG1";
        private const string ID_BATTLE_DEFEND = "B_DEF";
        private const string ID_BATTLE_DODGE = "B_DGE";
        private const string ID_BATTLE_DOWN = "B_DOWN";
        private const string ID_BATTLE_DYING = "B_DIE";
        private const string ID_BATTLE_CHANT = "B_CHA";
        private const string ID_BATTLE_RUNLOOP = "B_RUNL";
        private const string ID_BATTLE_DEAD = "B_DEAD";
        private const string ID_BATTLE_PRESKILL = "B_PRS";
        private const string ID_BATTLE_TOSS_LIFT = "B_TOSS_LIFT";
        private const string ID_BATTLE_TOSS_THROW = "B_TOSS_THROW";
        private const string ID_BATTLE_TRANSFORM = "B_TRANSFORM";
        public const string ANIM_BATTLE_TOSS_LIFT = "cmn_toss_lift0";
        public const string ANIM_BATTLE_TOSS_THROW = "cmn_toss_throw0";
        public const string COLLABO_SKILL_NAME_SUB = "_sub";
        private const float CameraInterpRate = 4f;
        private const float KNOCK_BACK_SEC = 0.4f;
        public const string TRANSFORM_SKILL_NAME_SUB = "_chg";
        public static List<TacticsUnitController> Instances;
        public List<string> mCustomRunAnimations;
        private GameObject mRenkeiAuraEffect;
        private GameObject mDrainEffect;
        private GameObject mChargeGrnTargetUnitEffect;
        private GameObject mChargeRedTargetUnitEffect;
        private GameObject mVersusCursor;
        private Transform mVersusCursorRoot;
        public PostureTypes Posture;
        public Color ColorMod;
        private Texture2D mIconSmall;
        private Texture2D mIconMedium;
        private Texture2D mJobIcon;
        private string mRunAnimation;
        private int mCachedHP;
        private UnitGauge mHPGauge;
        private UnitGaugeMark mAddIconGauge;
        private float mKeepHPGaugeVisible;
        private int mCachedHpMax;
        private ChargeIcon mChargeIcon;
        private SRPG.DeathSentenceIcon mDeathSentenceIcon;
        private GameObject mOwnerIndexUI;
        private UnitGaugeMark.EMarkType mKeepUnitGaugeMarkType;
        private UnitGaugeMark.EGemIcon mKeepUnitGaugeMarkGemIconType;
        private HideGimmickAnimation mHideGimmickAnim;
        public string UniqueName;
        [NonSerialized]
        public int PlayerMemberIndex;
        private SRPG.Unit mUnit;
        private Projector mShadow;
        public UnitCursor UnitCursorTemplate;
        private UnitCursor mUnitCursor;
        public bool AutoUpdateRotation;
        private StateMachine<TacticsUnitController> mStateMachine;
        private bool mCancelAction;
        private EUnitDirection mFieldActionDir;
        private Vector2 mFieldActionPoint;
        private Color mBlendColor;
        private ColorBlendModes mBlendMode;
        private float mBlendColorTime;
        private float mBlendColorTimeMax;
        private bool mEnableColorBlending;
        private PetController mPet;
        public static readonly string ANIM_IDLE_FIELD;
        public static readonly string ANIM_IDLE_DEMO;
        public static readonly string ANIM_RUN_FIELD;
        public static readonly string ANIM_STEP;
        public static readonly string ANIM_FALL_LOOP;
        public static readonly string ANIM_FALL_END;
        public static readonly string ANIM_CLIMBUP;
        public static readonly string ANIM_PICKUP;
        public static readonly string ANIM_GENKIDAMA;
        private Dictionary<string, GameObject> mDefendSkillEffects;
        private GameObject mPickupObject;
        private Vector3[] mRoute;
        private int mRoutePos;
        private float mRunSpeed;
        private Vector3 mVelocity;
        private GridMap<int> mWalkableField;
        private bool mCollideGround;
        public bool IgnoreMove;
        private float mPostMoveAngle;
        private float mIdleInterpTime;
        private bool mLoadedPartially;
        private Vector3 mLookAtTarget;
        private float mSpinCount;
        private FieldActionEndEvent mOnFieldActionEnd;
        private Vector3 mStepStart;
        private Vector3 mStepEnd;
        private float mMoveAnimInterpTime;
        private BadStatusEffects.Desc mBadStatus;
        private GameObject mBadStatusEffect;
        private string mLoadedBadStatusAnimation;
        private GameObject mCurseEffect;
        private string mLoadedCurseAnimation;
        private bool IsCursed;
        private int mBadStatusLocks;
        public int DrainGemsOnHit;
        public GemParticle[] GemDrainEffects;
        public GameObject GemDrainHitEffect;
        public bool ShowCriticalEffectOnHit;
        public bool ShowBackstabEffectOnHit;
        public EElementEffectTypes ShowElementEffectOnHit;
        public List<ShieldState> Shields;
        private ShakeUnit mShaker;
        private float mMapTrajectoryHeight;
        private SkillVars mSkillVars;
        public bool ShouldDodgeHits;
        public bool ShouldPerfectDodge;
        public bool ShouldDefendHits;
        private static string CameraAnimationDir;
        private GameObject mLastHitEffect;
        private LogSkill.Target mHitInfo;
        private LogSkill.Target mHitInfoSelf;
        private float mCastJumpOffsetY;
        private bool mCastJumpStartComplete;
        private bool mCastJumpFallComplete;
        public Vector3 JumpFallPos;
        public IntVector2 JumpMapFallPos;
        private bool mFinishedCastJumpFall;
        private bool mIsPlayDamageMotion;
        private eKnockBackMode mKnockBackMode;
        private Grid mKnockBackGrid;
        private Vector3 mKbPosStart;
        private Vector3 mKbPosEnd;
        private float mKbPassedSec;

        static TacticsUnitController()
        {
            CameraAnimationDir = "Camera/";
            Instances = new List<TacticsUnitController>(0x10);
            ANIM_IDLE_FIELD = "idlefield0";
            ANIM_IDLE_DEMO = "cmn_demo_talk_idle0";
            ANIM_RUN_FIELD = "runfield0";
            ANIM_STEP = "cmn_step0";
            ANIM_FALL_LOOP = "cmn_fallloop0";
            ANIM_FALL_END = "cmn_fallland0";
            ANIM_CLIMBUP = "cmn_jumploop0";
            ANIM_PICKUP = "cmn_pickup0";
            ANIM_GENKIDAMA = "cmn_freeze0";
            return;
        }

        public TacticsUnitController()
        {
            this.ShowElementEffectOnHit = 1;
            this.Shields = new List<ShieldState>();
            this.mCastJumpOffsetY = 10f;
            this.ColorMod = Color.get_white();
            this.PlayerMemberIndex = -1;
            this.AutoUpdateRotation = 1;
            this.mDefendSkillEffects = new Dictionary<string, GameObject>();
            this.mRunSpeed = 4f;
            this.mVelocity = Vector3.get_zero();
            this.mCollideGround = 1;
            this.mSpinCount = 2f;
            base..ctor();
            return;
        }

        public bool AdjustMovePos(EUnitDirection edir, ref Vector3 pos)
        {
            bool flag;
            EUnitDirection direction;
            flag = 0;
            direction = edir;
            switch (direction)
            {
                case 0:
                    goto Label_0053;

                case 1:
                    goto Label_00BB;

                case 2:
                    goto Label_001F;

                case 3:
                    goto Label_0087;
            }
            goto Label_00EF;
        Label_001F:
            if ((pos.x % 1f) >= 0.3f)
            {
                goto Label_00EF;
            }
            pos.x = Mathf.Floor(pos.x) + 0.3f;
            flag = 1;
            goto Label_00EF;
        Label_0053:
            if ((pos.x % 1f) <= 0.7f)
            {
                goto Label_00EF;
            }
            pos.x = Mathf.Floor(pos.x) + 0.7f;
            flag = 1;
            goto Label_00EF;
        Label_0087:
            if ((pos.z % 1f) >= 0.3f)
            {
                goto Label_00EF;
            }
            pos.z = Mathf.Floor(pos.z) + 0.3f;
            flag = 1;
            goto Label_00EF;
        Label_00BB:
            if ((pos.z % 1f) <= 0.7f)
            {
                goto Label_00EF;
            }
            pos.z = Mathf.Floor(pos.z) + 0.7f;
            flag = 1;
        Label_00EF:
            return flag;
        }

        public void AdvanceShake()
        {
            base.get_transform().set_localPosition(this.mShaker.AdvanceShake());
            return;
        }

        private void AnimateCamera(AnimationClip cameraClip, float normalizedTime, int cameraIndex)
        {
            this.AnimateCameraInterpolated(cameraClip, normalizedTime, 1f, CameraState.Default, cameraIndex);
            return;
        }

        private unsafe void AnimateCameraInterpolated(AnimationClip cameraClip, float normalizedTime, float interpRate, CameraState startState, int cameraIndex)
        {
            Vector3 vector;
            Quaternion quaternion;
            Vector3 vector2;
            Quaternion quaternion2;
            this.CalcCameraPos(cameraClip, normalizedTime, cameraIndex, &vector, &quaternion);
            vector2 = base.get_transform().get_position() + vector;
            quaternion2 = quaternion;
            if (interpRate >= 1f)
            {
                goto Label_004B;
            }
            vector2 = Vector3.Lerp(&startState.Position, vector2, interpRate);
            quaternion2 = Quaternion.Slerp(&startState.Rotation, quaternion2, interpRate);
        Label_004B:
            this.SetActiveCameraPosition(vector2, this.mSkillVars.mCameraShakeOffset * quaternion2);
            return;
        }

        [DebuggerHidden]
        private IEnumerator AnimateProjectile(AnimationClip clip, float length, Vector3 basePosition, Quaternion baseRotation, ProjectileStopEvent callback, ProjectileData pd)
        {
            <AnimateProjectile>c__Iterator54 iterator;
            iterator = new <AnimateProjectile>c__Iterator54();
            iterator.length = length;
            iterator.clip = clip;
            iterator.pd = pd;
            iterator.basePosition = basePosition;
            iterator.baseRotation = baseRotation;
            iterator.callback = callback;
            iterator.<$>length = length;
            iterator.<$>clip = clip;
            iterator.<$>pd = pd;
            iterator.<$>basePosition = basePosition;
            iterator.<$>baseRotation = baseRotation;
            iterator.<$>callback = callback;
            return iterator;
        }

        private void AnimationUpdated(GameObject go)
        {
            if ((base.mCharacterSettings != null) == null)
            {
                goto Label_0021;
            }
            base.mCharacterSettings.SetSkeleton("small");
        Label_0021:
            return;
        }

        private void ApplyColorBlending(Color color)
        {
            int num;
            num = 0;
            goto Label_004E;
        Label_0007:
            base.CharacterMaterials[num].EnableKeyword("COLORBLEND_ON");
            base.CharacterMaterials[num].DisableKeyword("COLORBLEND_OFF");
            base.CharacterMaterials[num].SetColor("_blendColor", color);
            num += 1;
        Label_004E:
            if (num < base.CharacterMaterials.Count)
            {
                goto Label_0007;
            }
            return;
        }

        [DebuggerHidden]
        private IEnumerator AsyncHitTimer()
        {
            <AsyncHitTimer>c__Iterator57 iterator;
            iterator = new <AsyncHitTimer>c__Iterator57();
            iterator.<>f__this = this;
            return iterator;
        }

        private void attachBadStatusEffect(GameObject go_effect, string attach_target, bool is_use_cs)
        {
            Transform transform;
            if (go_effect != null)
            {
                goto Label_000C;
            }
            return;
        Label_000C:
            transform = null;
            if (string.IsNullOrEmpty(attach_target) != null)
            {
                goto Label_0053;
            }
            if (is_use_cs == null)
            {
                goto Label_0046;
            }
            if (base.mCharacterSettings == null)
            {
                goto Label_0046;
            }
            transform = GameUtility.findChildRecursively(base.mCharacterSettings.get_transform(), attach_target);
            goto Label_0053;
        Label_0046:
            transform = GameUtility.findChildRecursively(base.get_transform(), attach_target);
        Label_0053:
            if ((transform == null) == null)
            {
                goto Label_0066;
            }
            transform = base.GetCharacterRoot();
        Label_0066:
            go_effect.get_transform().SetParent(transform, 0);
            if (base.IsVisible() != null)
            {
                goto Label_0085;
            }
            base.SetVisible(0);
        Label_0085:
            return;
        }

        private void Awake()
        {
            this.mRunAnimation = "RUN";
            Instances.Add(this);
            base.LoadEquipments = 1;
            base.KeepUnitHidden = 1;
            this.mStateMachine = new StateMachine<TacticsUnitController>(this);
            return;
        }

        public void BeginLoadPickupAnimation()
        {
            base.LoadUnitAnimationAsync("PICK", ANIM_PICKUP, 0, 0);
            return;
        }

        public void BuffEffectSelf()
        {
            if (this.mSkillVars == null)
            {
                goto Label_001B;
            }
            if (this.mSkillVars.Skill != null)
            {
                goto Label_001C;
            }
        Label_001B:
            return;
        Label_001C:
            if (this.mSkillVars.Skill.IsPrevApply() != null)
            {
                goto Label_0032;
            }
            return;
        Label_0032:
            if (this.mHitInfoSelf != null)
            {
                goto Label_003E;
            }
            return;
        Label_003E:
            if (this.mHitInfoSelf.hits.Count > 0)
            {
                goto Label_007F;
            }
            if (this.mHitInfoSelf.buff.CheckEffect() != null)
            {
                goto Label_007F;
            }
            if (this.mHitInfoSelf.debuff.CheckEffect() != null)
            {
                goto Label_007F;
            }
            return;
        Label_007F:
            if (this.mHitInfoSelf.IsBuffEffect() == null)
            {
                goto Label_00C2;
            }
            SceneBattle.Instance.PopupParamChange(base.CenterPosition, this.mHitInfoSelf.buff, this.mHitInfoSelf.debuff, 0);
            this.mSkillVars.mIsFinishedBuffEffectSelf = 1;
        Label_00C2:
            return;
        }

        public unsafe void BuffEffectTarget()
        {
            TacticsUnitController controller;
            List<TacticsUnitController>.Enumerator enumerator;
            if (this.mSkillVars == null)
            {
                goto Label_002B;
            }
            if (this.mSkillVars.Skill == null)
            {
                goto Label_002B;
            }
            if (this.mSkillVars.Targets != null)
            {
                goto Label_002C;
            }
        Label_002B:
            return;
        Label_002C:
            if (this.mSkillVars.Skill.IsPrevApply() != null)
            {
                goto Label_0042;
            }
            return;
        Label_0042:
            enumerator = this.mSkillVars.Targets.GetEnumerator();
        Label_0053:
            try
            {
                goto Label_00BD;
            Label_0058:
                controller = &enumerator.Current;
                if (controller.mHitInfo.IsBuffEffect() != null)
                {
                    goto Label_0080;
                }
                if (controller.mHitInfo.ChangeValueCT == null)
                {
                    goto Label_00BD;
                }
            Label_0080:
                SceneBattle.Instance.PopupParamChange(controller.CenterPosition, controller.mHitInfo.buff, controller.mHitInfo.debuff, controller.mHitInfo.ChangeValueCT);
                this.mSkillVars.mIsFinishedBuffEffectTarget = 1;
            Label_00BD:
                if (&enumerator.MoveNext() != null)
                {
                    goto Label_0058;
                }
                goto Label_00DA;
            }
            finally
            {
            Label_00CE:
                ((List<TacticsUnitController>.Enumerator) enumerator).Dispose();
            }
        Label_00DA:
            return;
        }

        public void CacheIcons()
        {
            if (this.Unit == null)
            {
                goto Label_0018;
            }
            base.StartCoroutine(this.CacheIconsAsync());
        Label_0018:
            return;
        }

        [DebuggerHidden]
        private IEnumerator CacheIconsAsync()
        {
            <CacheIconsAsync>c__Iterator50 iterator;
            iterator = new <CacheIconsAsync>c__Iterator50();
            iterator.<>f__this = this;
            return iterator;
        }

        private void CalcCameraPos(AnimationClip clip, float normalizedTime, int cameraIndex, out Vector3 pos, out Quaternion rotation)
        {
            GameObject obj2;
            GameObject obj3;
            GameObject obj4;
            Transform transform;
            Transform transform2;
            Transform transform3;
            Transform transform4;
            *(pos) = Vector3.get_zero();
            *(rotation) = Quaternion.get_identity();
            obj2 = new GameObject();
            obj3 = new GameObject("Camera001");
            obj4 = new GameObject("Camera002");
            transform = obj2.get_transform();
            transform2 = obj3.get_transform();
            transform3 = obj4.get_transform();
            transform.SetParent(base.get_transform(), 0);
            transform2.SetParent(transform, 0);
            transform3.SetParent(transform, 0);
            clip.SampleAnimation(obj2, normalizedTime * clip.get_length());
            transform4 = (cameraIndex == null) ? transform2 : transform3;
            *(pos) = (transform4.get_position() - base.get_transform().get_position()) + base.RootMotionInverse;
            *(rotation) = transform4.get_rotation();
            Object.DestroyImmediate(obj2);
            *(rotation) *= GameUtility.Yaw180;
            return;
        }

        private void CalcEnemyPos(AnimationClip clip, float normalizedTime, out Vector3 pos, out Quaternion rotation)
        {
            GameObject obj2;
            GameObject obj3;
            Transform transform;
            Transform transform2;
            Quaternion quaternion;
            obj2 = new GameObject();
            obj3 = new GameObject("Enm_Distance_dummy");
            transform = obj2.get_transform();
            transform2 = obj3.get_transform();
            transform2.SetParent(transform, 0);
            transform.SetParent(base.get_transform(), 0);
            clip.SampleAnimation(obj2, normalizedTime * clip.get_length());
            *(pos) = transform2.get_position() + base.RootMotionInverse;
            quaternion = transform2.get_localRotation() * Quaternion.AngleAxis(90f, Vector3.get_right());
            *(rotation) = transform.get_rotation() * quaternion;
            Object.DestroyImmediate(obj2);
            return;
        }

        public static EUnitDirection CalcUnitDirection(float x, float y)
        {
            float num;
            num = ((57.29578f * Mathf.Atan2(y, x)) + 360f) % 360f;
            if (325f <= num)
            {
                goto Label_0030;
            }
            if (num >= 45f)
            {
                goto Label_0032;
            }
        Label_0030:
            return 0;
        Label_0032:
            if (45f > num)
            {
                goto Label_004A;
            }
            if (num >= 135f)
            {
                goto Label_004A;
            }
            return 1;
        Label_004A:
            if (135f > num)
            {
                goto Label_0062;
            }
            if (num >= 225f)
            {
                goto Label_0062;
            }
            return 2;
        Label_0062:
            return 3;
        }

        public unsafe EUnitDirection CalcUnitDirectionFromRotation()
        {
            Transform transform;
            Vector3 vector;
            Vector3 vector2;
            transform = base.get_transform();
            return CalcUnitDirection(&transform.get_forward().x, &transform.get_forward().z);
        }

        public void CancelAction()
        {
            this.mCancelAction = 1;
            return;
        }

        public void CastJump()
        {
            this.GotoState<State_JumpCast>();
            return;
        }

        public void CastJumpFall(bool is_play_damage_motion)
        {
            this.mIsPlayDamageMotion = is_play_damage_motion;
            this.GotoState<State_JumpCastFall>();
            return;
        }

        private bool CheckCollision(int x0, int y0, int x1, int y1)
        {
            int num;
            int num2;
            if (this.mWalkableField.isValid(x1, y1) != null)
            {
                goto Label_0015;
            }
            return 1;
        Label_0015:
            num = this.mWalkableField.get(x0, y0);
            num2 = this.mWalkableField.get(x1, y1);
            return ((num2 < 0) ? 1 : (BattleMap.MAX_GRID_MOVING < Mathf.Abs(num - num2)));
        }

        public void ClearBadStatusLocks()
        {
            this.mBadStatusLocks = 0;
            return;
        }

        public int CountSkillHits()
        {
            int num;
            int num2;
            int num3;
            SkillSequence.SkillTypes types;
            num = 0;
            switch (this.mSkillVars.mSkillSequence.SkillType)
            {
                case 0:
                    goto Label_0032;

                case 1:
                    goto Label_00A5;

                case 2:
                    goto Label_00AC;

                case 3:
                    goto Label_00AC;

                case 4:
                    goto Label_00AC;
            }
            goto Label_011F;
        Label_0032:
            if ((this.mSkillVars.mSkillAnimation != null) == null)
            {
                goto Label_011F;
            }
            if (this.mSkillVars.mSkillAnimation.events == null)
            {
                goto Label_011F;
            }
            num2 = 0;
            goto Label_0088;
        Label_0064:
            if ((this.mSkillVars.mSkillAnimation.events[num2] as HitFrame) == null)
            {
                goto Label_0084;
            }
            num += 1;
        Label_0084:
            num2 += 1;
        Label_0088:
            if (num2 < ((int) this.mSkillVars.mSkillAnimation.events.Length))
            {
                goto Label_0064;
            }
            goto Label_011F;
        Label_00A5:
            num = 1;
            goto Label_011F;
        Label_00AC:
            if ((this.mSkillVars.mSkillAnimation != null) == null)
            {
                goto Label_011F;
            }
            if (this.mSkillVars.mSkillAnimation.events == null)
            {
                goto Label_011F;
            }
            num3 = 0;
            goto Label_0102;
        Label_00DE:
            if ((this.mSkillVars.mSkillAnimation.events[num3] as ProjectileFrame) == null)
            {
                goto Label_00FE;
            }
            num += 1;
        Label_00FE:
            num3 += 1;
        Label_0102:
            if (num3 < ((int) this.mSkillVars.mSkillAnimation.events.Length))
            {
                goto Label_00DE;
            }
        Label_011F:
            return num;
        }

        private int CountSkillUnitVoice()
        {
            int num;
            int num2;
            num = 0;
            if ((this.mSkillVars.mSkillAnimation != null) == null)
            {
                goto Label_0070;
            }
            if (this.mSkillVars.mSkillAnimation.events == null)
            {
                goto Label_0070;
            }
            num2 = 0;
            goto Label_0058;
        Label_0034:
            if ((this.mSkillVars.mSkillAnimation.events[num2] as UnitVoiceEvent) == null)
            {
                goto Label_0054;
            }
            num += 1;
        Label_0054:
            num2 += 1;
        Label_0058:
            if (num2 < ((int) this.mSkillVars.mSkillAnimation.events.Length))
            {
                goto Label_0034;
            }
        Label_0070:
            return num;
        }

        private void createEffect(TacticsUnitController target, GameObject go_effect)
        {
            GameObject obj2;
            obj2 = Object.Instantiate(go_effect, Vector3.get_zero(), Quaternion.get_identity()) as GameObject;
            if (obj2 == null)
            {
                goto Label_003A;
            }
            obj2.get_transform().SetParent(target.get_transform(), 0);
            GameUtility.RequireComponent<OneShotParticle>(obj2);
        Label_003A:
            return;
        }

        public bool CreateOwnerIndexUI(Canvas canvas, GameObject templeteUI, JSON_MyPhotonPlayerParam param)
        {
            UIProjector projector;
            if ((this.mOwnerIndexUI != null) == null)
            {
                goto Label_0013;
            }
            return 1;
        Label_0013:
            if ((canvas == null) != null)
            {
                goto Label_0031;
            }
            if ((templeteUI == null) != null)
            {
                goto Label_0031;
            }
            if (param != null)
            {
                goto Label_0033;
            }
        Label_0031:
            return 0;
        Label_0033:
            this.mOwnerIndexUI = Object.Instantiate<GameObject>(templeteUI);
            if ((this.mOwnerIndexUI == null) == null)
            {
                goto Label_0052;
            }
            return 0;
        Label_0052:
            DataSource.Bind<JSON_MyPhotonPlayerParam>(this.mOwnerIndexUI, param);
            projector = base.get_gameObject().AddComponent<UIProjector>();
            projector.UIObject = this.mOwnerIndexUI.get_transform() as RectTransform;
            projector.AutoDestroyUIObject = 1;
            projector.LocalOffset = Vector3.get_up();
            projector.SetCanvas(canvas);
            return 1;
        }

        public bool CreateVersusCursor(GameObject templeteUI)
        {
            if ((this.mVersusCursor != null) == null)
            {
                goto Label_0013;
            }
            return 1;
        Label_0013:
            if ((templeteUI == null) == null)
            {
                goto Label_0021;
            }
            return 0;
        Label_0021:
            this.mVersusCursor = Object.Instantiate<GameObject>(templeteUI);
            if ((this.mVersusCursor == null) == null)
            {
                goto Label_0040;
            }
            return 0;
        Label_0040:
            this.mVersusCursor.get_transform().SetParent(base.get_gameObject().get_transform(), 0);
            this.mVersusCursor.get_transform().set_localPosition(Vector3.get_up() * 1.3f);
            this.mVersusCursor.get_transform().set_localScale(new Vector3(0.4f, 0.4f, 0.4f));
            GameUtility.SetLayer(this.mVersusCursor, GameUtility.LayerUI, 1);
            this.mVersusCursorRoot = this.mVersusCursor.get_transform().FindChild("Root");
            return 1;
        }

        public void DeathSentenceCountDown(bool isShow, float LifeTime)
        {
            if (isShow == null)
            {
                goto Label_000D;
            }
            this.ShowHPGauge(1);
        Label_000D:
            if ((this.mDeathSentenceIcon != null) == null)
            {
                goto Label_0046;
            }
            this.mDeathSentenceIcon.Open();
            this.mDeathSentenceIcon.Countdown(Mathf.Max(this.Unit.DeathCount, 0), LifeTime);
        Label_0046:
            return;
        }

        public void DeleteGimmickIconAll()
        {
            if ((this.mAddIconGauge != null) == null)
            {
                goto Label_001C;
            }
            this.mAddIconGauge.DeleteIconAll();
        Label_001C:
            return;
        }

        public void DirectionOff_EndSkill()
        {
            this.SkillEffectSelf();
            this.FinishSkill();
            return;
        }

        public void DirectionOff_LoadSkill(SkillParam skillParam, bool is_cs, bool is_cs_sub)
        {
            this.mSkillVars = new SkillVars();
            this.mSkillVars.Skill = skillParam;
            this.mSkillVars.mIsCollaboSkill = is_cs;
            this.mSkillVars.mIsCollaboSkillSub = is_cs_sub;
            base.AddLoadThreadCount();
            base.StartCoroutine(this.LoadSkillSequenceAsync(skillParam, 0));
            if (string.IsNullOrEmpty(skillParam.effect) != null)
            {
                goto Label_0061;
            }
            this.LoadSkillEffect(skillParam.effect, 0);
        Label_0061:
            return;
        }

        public bool DirectionOff_OnEventStart()
        {
            bool flag;
            int num;
            TacticsUnitController controller;
            int num2;
            TacticsUnitController controller2;
            bool flag2;
            bool flag3;
            int num3;
            bool flag4;
            bool flag5;
            string str;
            float num4;
            int num5;
            TacticsUnitController controller3;
            int num6;
            float num7;
            if (this.mSkillVars == null)
            {
                goto Label_04A6;
            }
            this.mSkillVars.NumHitsLeft -= 1;
            if (this.mSkillVars.NumHitsLeft >= 0)
            {
                goto Label_003B;
            }
            this.mSkillVars.NumHitsLeft = 0;
        Label_003B:
            flag = 0;
            num = 0;
            goto Label_006C;
        Label_0044:
            controller = this.mSkillVars.Targets[num];
            if (controller.mHitInfo.IsCombo() == null)
            {
                goto Label_0068;
            }
            flag = 1;
        Label_0068:
            num += 1;
        Label_006C:
            if (num < this.mSkillVars.Targets.Count)
            {
                goto Label_0044;
            }
            if (flag != null)
            {
                goto Label_0094;
            }
            this.mSkillVars.NumHitsLeft = 0;
        Label_0094:
            num2 = 0;
            goto Label_02C7;
        Label_009B:
            controller2 = this.mSkillVars.Targets[num2];
            flag2 = controller2.ShouldDodgeHits;
            flag3 = controller2.ShouldPerfectDodge;
            if (controller2.mHitInfo.IsCombo() == null)
            {
                goto Label_0146;
            }
            num3 = (controller2.mHitInfo.hits.Count - 1) - this.mSkillVars.NumHitsLeft;
            if ((0 > num3) || (num3 >= controller2.mHitInfo.hits.Count))
            {
                goto Label_0146;
            }
            flag2 = controller2.mHitInfo.hits[num3].is_avoid;
            flag3 = controller2.mHitInfo.hits[num3].is_pf_avoid;
        Label_0146:
            if (flag2 == null)
            {
                goto Label_0252;
            }
            flag4 = 1;
            if (this.mSkillVars.UseBattleScene == null)
            {
                goto Label_017C;
            }
            flag4 = this.mSkillVars.NumHitsLeft == (this.mSkillVars.TotalHits - 1);
        Label_017C:
            if ((controller2.mHitInfo.IsCombo() == null) && (this.mSkillVars.NumHitsLeft != null))
            {
                goto Label_02C3;
            }
            this.mSkillVars.Targets[num2].PlayDodge(flag3, flag4);
            if ((this.mSkillVars.NumHitsLeft != null) || (controller2.mKnockBackGrid == null))
            {
                goto Label_01DB;
            }
            controller2.mKnockBackMode = 1;
        Label_01DB:
            if (((this.mSkillVars.mSkillEffect.AlwaysExplode == null) || (this.mSkillVars.Skill.effect_type == 0x12)) || (this.mSkillVars.Skill.effect_type == 0x16))
            {
                goto Label_02C3;
            }
            this.SpawnHitEffect(this.mSkillVars.Targets[num2].get_transform().get_position(), this.mSkillVars.NumHitsLeft == 0);
            goto Label_02C3;
        Label_0252:
            if ((controller2.mHitInfo.IsCombo() == null) && (this.mSkillVars.NumHitsLeft != null))
            {
                goto Label_02C3;
            }
            this.HitTarget(this.mSkillVars.Targets[num2], this.mSkillVars.mSkillEffect.RangedHitReactionType, 0);
            MonoSingleton<GameManager>.Instance.Player.OnDamageToEnemy(this.mUnit, controller2.Unit, controller2.mHitInfo.GetTotalHpDamage());
        Label_02C3:
            num2 += 1;
        Label_02C7:
            if (num2 < this.mSkillVars.Targets.Count)
            {
                goto Label_009B;
            }
            flag5 = this.mSkillVars.MaxPlayVoice == this.mSkillVars.NumPlayVoice;
            str = "battle_0031";
            this.mSkillVars.NumPlayVoice -= 1;
            if (flag5 == null)
            {
                goto Label_0497;
            }
            if ((this.mSkillVars.Skill.IsReactionSkill() == null) || (this.mSkillVars.Skill.IsDamagedSkill() == null))
            {
                goto Label_0349;
            }
            str = "battle_0013";
        Label_0349:
            if (this.ShowCriticalEffectOnHit == null)
            {
                goto Label_035B;
            }
            str = "battle_0012";
        Label_035B:
            if (string.IsNullOrEmpty(str) != null)
            {
                goto Label_048A;
            }
            num4 = 0f;
            if (this.mSkillVars.Targets == null)
            {
                goto Label_0464;
            }
            num5 = 0;
            goto Label_044D;
        Label_0386:
            controller3 = this.mSkillVars.Targets[num5];
            if (controller3.ShouldDodgeHits == null)
            {
                goto Label_03AB;
            }
            goto Label_0447;
        Label_03AB:
            num6 = 0;
            if (this.mSkillVars.TotalHits <= 1)
            {
                goto Label_03DE;
            }
            num6 = controller3.mHitInfo.GetTotalHpDamage() / this.mSkillVars.TotalHits;
            goto Label_03EC;
        Label_03DE:
            num6 = controller3.mHitInfo.GetTotalHpDamage();
        Label_03EC:
            num7 = (this.Unit.MaximumStatus.param.hp == null) ? 1f : ((1f * ((float) num6)) / ((float) this.Unit.MaximumStatus.param.hp));
            num4 = Mathf.Max(num7, num4);
        Label_0447:
            num5 += 1;
        Label_044D:
            if (num5 < this.mSkillVars.Targets.Count)
            {
                goto Label_0386;
            }
        Label_0464:
            if (num4 < 0.75f)
            {
                goto Label_0477;
            }
            str = "battle_0033";
        Label_0477:
            if (num4 <= 0.5f)
            {
                goto Label_048A;
            }
            str = "battle_0032";
        Label_048A:
            this.Unit.PlayBattleVoice(str);
        Label_0497:
            return (this.mSkillVars.NumHitsLeft > 0);
        Label_04A6:
            return 0;
        }

        public void DirectionOff_StartSkill(Vector3 targetPosition, Camera activeCamera, TacticsUnitController[] targets, Vector3[] hitGrids, SkillParam skill)
        {
            this.mSkillVars.HitGrids = hitGrids;
            this.InternalStartSkill(targets, targetPosition, activeCamera, 0);
            return;
        }

        public void DisableChargeTargetUnit()
        {
            if (this.mChargeGrnTargetUnitEffect == null)
            {
                goto Label_002E;
            }
            GameUtility.StopEmitters(this.mChargeGrnTargetUnitEffect);
            GameUtility.RequireComponent<OneShotParticle>(this.mChargeGrnTargetUnitEffect);
            this.mChargeGrnTargetUnitEffect = null;
        Label_002E:
            if (this.mChargeRedTargetUnitEffect == null)
            {
                goto Label_005C;
            }
            GameUtility.StopEmitters(this.mChargeRedTargetUnitEffect);
            GameUtility.RequireComponent<OneShotParticle>(this.mChargeRedTargetUnitEffect);
            this.mChargeRedTargetUnitEffect = null;
        Label_005C:
            return;
        }

        private void DisableColorBlending()
        {
            int num;
            num = 0;
            goto Label_0037;
        Label_0007:
            base.CharacterMaterials[num].EnableKeyword("COLORBLEND_OFF");
            base.CharacterMaterials[num].DisableKeyword("COLORBLEND_ON");
            num += 1;
        Label_0037:
            if (num < base.CharacterMaterials.Count)
            {
                goto Label_0007;
            }
            return;
        }

        private void DrainGems(TacticsUnitController goal)
        {
            GameSettings settings;
            int num;
            Transform transform;
            Transform transform2;
            Vector3 vector;
            Vector3 vector2;
            Vector3 vector3;
            int num2;
            Quaternion quaternion;
            int num3;
            GemParticleHitEffect effect;
            if (this.GemDrainEffects == null)
            {
                goto Label_0017;
            }
            if (this.DrainGemsOnHit > 0)
            {
                goto Label_0018;
            }
        Label_0017:
            return;
        Label_0018:
            settings = GameSettings.Instance;
            num = this.DrainGemsOnHit + Random.Range(0, settings.Gem_DrainCount_Randomness);
            transform = goal.get_transform();
            vector = base.get_transform().get_position() - transform.get_position();
            vector2 = base.CenterPosition;
            vector3 = goal.CenterPosition - transform.get_position();
            num2 = 0;
            goto Label_01AA;
        Label_0076:
            quaternion = Quaternion.AngleAxis((float) Random.Range(0x87, 0xe1), vector) * Quaternion.AngleAxis((float) Random.Range(70, 110), Vector3.get_right());
            num3 = 0;
            goto Label_0195;
        Label_00B0:
            if (this.GemDrainEffects[num3].get_gameObject().get_activeInHierarchy() != null)
            {
                goto Label_018F;
            }
            this.GemDrainEffects[num3].Reset();
            this.GemDrainEffects[num3].get_gameObject().SetActive(1);
            this.GemDrainEffects[num3].get_transform().set_position(vector2);
            this.GemDrainEffects[num3].get_transform().set_rotation(quaternion);
            this.GemDrainEffects[num3].TargetObject = transform;
            this.GemDrainEffects[num3].TargetOffset = vector3;
            if ((this.GemDrainHitEffect != null) == null)
            {
                goto Label_01A4;
            }
            if (GemParticleHitEffect.IsEnable != null)
            {
                goto Label_0154;
            }
            GemParticleHitEffect.IsEnable = 1;
        Label_0154:
            this.GemDrainEffects[num3].get_gameObject().AddComponent<GemParticleHitEffect>();
            this.GemDrainEffects[num3].get_gameObject().GetComponent<GemParticleHitEffect>().EffectPrefab = this.GemDrainHitEffect;
            goto Label_01A4;
        Label_018F:
            num3 += 1;
        Label_0195:
            if (num3 < ((int) this.GemDrainEffects.Length))
            {
                goto Label_00B0;
            }
        Label_01A4:
            num2 += 1;
        Label_01AA:
            if (num2 < num)
            {
                goto Label_0076;
            }
            return;
        }

        public void EnableChargeTargetUnit(GameObject eff, bool is_grn)
        {
            if (is_grn == null)
            {
                goto Label_0068;
            }
            if (this.mChargeGrnTargetUnitEffect != null)
            {
                goto Label_00C5;
            }
            if (eff == null)
            {
                goto Label_00C5;
            }
            this.mChargeGrnTargetUnitEffect = Object.Instantiate(eff, Vector3.get_zero(), Quaternion.get_identity()) as GameObject;
            if (this.mChargeGrnTargetUnitEffect == null)
            {
                goto Label_00C5;
            }
            this.mChargeGrnTargetUnitEffect.get_transform().SetParent(base.get_transform(), 0);
            goto Label_00C5;
        Label_0068:
            if (this.mChargeRedTargetUnitEffect != null)
            {
                goto Label_00C5;
            }
            if (eff == null)
            {
                goto Label_00C5;
            }
            this.mChargeRedTargetUnitEffect = Object.Instantiate(eff, Vector3.get_zero(), Quaternion.get_identity()) as GameObject;
            if (this.mChargeRedTargetUnitEffect == null)
            {
                goto Label_00C5;
            }
            this.mChargeRedTargetUnitEffect.get_transform().SetParent(base.get_transform(), 0);
        Label_00C5:
            return;
        }

        private unsafe void execKnockBack()
        {
            SceneBattle battle;
            float num;
            GameObject obj2;
            Vector3 vector;
            eKnockBackMode mode;
            Vector3 vector2;
            if (this.mKnockBackMode != null)
            {
                goto Label_000C;
            }
            return;
        Label_000C:
            if (this.mKnockBackGrid != null)
            {
                goto Label_001F;
            }
            this.mKnockBackMode = 0;
            return;
        Label_001F:
            battle = SceneBattle.Instance;
            if (battle != null)
            {
                goto Label_0038;
            }
            this.mKnockBackMode = 0;
            return;
        Label_0038:
            mode = this.mKnockBackMode;
            if (mode == 1)
            {
                goto Label_0055;
            }
            if (mode == 2)
            {
                goto Label_00A2;
            }
            goto Label_0183;
        Label_0055:
            this.mKbPosStart = base.CenterPosition;
            this.mKbPosEnd = battle.CalcGridCenter(this.mKnockBackGrid);
            this.mKbPassedSec = 0f;
            this.mKnockBackMode = 2;
            if (base.IsVisible() == null)
            {
                goto Label_0183;
            }
            this.createEffect(this, battle.KnockBackEffect);
            goto Label_0183;
        Label_00A2:
            this.mKbPassedSec += Time.get_deltaTime();
            num = this.mKbPassedSec / 0.4f;
            if (num < 1f)
            {
                goto Label_00F0;
            }
            base.get_transform().set_position(this.mKbPosEnd);
            this.HideGimmickForTargetGrid(this);
            this.mKnockBackMode = 0;
            goto Label_0117;
        Label_00F0:
            num *= 2f - num;
            base.get_transform().set_position(Vector3.Lerp(this.mKbPosStart, this.mKbPosEnd, num));
        Label_0117:
            if (base.IsVisible() != null)
            {
                goto Label_0183;
            }
            obj2 = battle.GetJumpSpotEffect(this.Unit);
            if (obj2 == null)
            {
                goto Label_0183;
            }
            obj2.get_transform().set_position(base.get_transform().get_position());
            vector = obj2.get_transform().get_position();
            &vector.y = &GameUtility.RaycastGround(vector).y;
            obj2.get_transform().set_position(vector);
        Label_0183:
            return;
        }

        public void FadeBlendColor(Color color, float time)
        {
            this.mBlendMode = 1;
            this.mBlendColor = color;
            this.mBlendColorTime = 0f;
            this.mBlendColorTimeMax = time;
            return;
        }

        public static TacticsUnitController FindByUniqueName(string uniqueName)
        {
            int num;
            num = Instances.Count - 1;
            goto Label_003D;
        Label_0012:
            if ((Instances[num].UniqueName == uniqueName) == null)
            {
                goto Label_0039;
            }
            return Instances[num];
        Label_0039:
            num -= 1;
        Label_003D:
            if (num >= 0)
            {
                goto Label_0012;
            }
            return null;
        }

        public static TacticsUnitController FindByUnitID(string unitID)
        {
            int num;
            num = Instances.Count - 1;
            goto Label_005C;
        Label_0012:
            if (Instances[num].Unit == null)
            {
                goto Label_0058;
            }
            if ((Instances[num].Unit.UnitParam.iname == unitID) == null)
            {
                goto Label_0058;
            }
            return Instances[num];
        Label_0058:
            num -= 1;
        Label_005C:
            if (num >= 0)
            {
                goto Label_0012;
            }
            return null;
        }

        private void FinishSkill()
        {
            int num;
            LogSkill.Target target;
            num = 0;
            goto Label_007E;
        Label_0007:
            target = this.mSkillVars.Targets[num].mHitInfo;
            if (target.IsFailCondition() != null)
            {
                goto Label_0044;
            }
            if (target.IsCureCondition() != null)
            {
                goto Label_0044;
            }
            if (target.IsBuffEffect() != null)
            {
                goto Label_0044;
            }
            goto Label_007A;
        Label_0044:
            if (this.mSkillVars.Targets[num].Unit.IsDead != null)
            {
                goto Label_007A;
            }
            this.mSkillVars.Targets[num].UpdateBadStatus();
        Label_007A:
            num += 1;
        Label_007E:
            if (num < this.mSkillVars.Targets.Count)
            {
                goto Label_0007;
            }
            if (this.mSkillVars.UseBattleScene != null)
            {
                goto Label_00BA;
            }
            this.StepTo(this.mSkillVars.mStartPosition);
            goto Label_00C5;
        Label_00BA:
            this.PlayIdle(0f);
        Label_00C5:
            this.ResetSkill();
            return;
        }

        public unsafe string GetAnmNameTransformSkill()
        {
            if (this.mSkillVars == null)
            {
                goto Label_001B;
            }
            if (this.mSkillVars.mSkillSequence != null)
            {
                goto Label_001D;
            }
        Label_001B:
            return null;
        Label_001D:
            if (string.IsNullOrEmpty(&this.mSkillVars.mSkillSequence.SkillAnimation.Name) == null)
            {
                goto Label_003E;
            }
            return null;
        Label_003E:
            return (&this.mSkillVars.mSkillSequence.SkillAnimation.Name + "_chg");
        }

        public float GetShakeProgress()
        {
            return (float) (1 - (this.mShaker.ShakeCount / this.mShaker.ShakeMaxCount));
        }

        public SkillSequence.MapCameraTypes GetSkillCameraType()
        {
            if (this.mSkillVars == null)
            {
                goto Label_002C;
            }
            if (this.mSkillVars.mSkillSequence == null)
            {
                goto Label_002C;
            }
            return this.mSkillVars.mSkillSequence.MapCameraType;
        Label_002C:
            return 0;
        }

        public List<TacticsUnitController> GetSkillTargets()
        {
            if (this.mSkillVars == null)
            {
                goto Label_001B;
            }
            if (this.mSkillVars.Targets != null)
            {
                goto Label_0021;
            }
        Label_001B:
            return new List<TacticsUnitController>();
        Label_0021:
            return this.mSkillVars.Targets;
        }

        public Vector3 GetTargetPos()
        {
            SceneBattle battle;
            if (this.mSkillVars != null)
            {
                goto Label_0011;
            }
            return Vector3.get_zero();
        Label_0011:
            if (this.mSkillVars.mTeleportGrid == null)
            {
                goto Label_0059;
            }
            battle = SceneBattle.Instance;
            if (battle == null)
            {
                goto Label_0059;
            }
            return battle.CalcGridCenter(this.mSkillVars.mTeleportGrid.x, this.mSkillVars.mTeleportGrid.y);
        Label_0059:
            return base.get_transform().get_position();
        }

        public List<TacticsUnitController> GetTargetTucLists()
        {
            if (this.mSkillVars == null)
            {
                goto Label_001B;
            }
            if (this.mSkillVars.Targets != null)
            {
                goto Label_0021;
            }
        Label_001B:
            return new List<TacticsUnitController>();
        Label_0021:
            return this.mSkillVars.Targets;
        }

        private void GotoState<T>() where T: State, new()
        {
            this.mStateMachine.GotoState<T>();
            return;
        }

        public void HideCursor(bool immediate)
        {
            if ((this.mUnitCursor == null) == null)
            {
                goto Label_0012;
            }
            return;
        Label_0012:
            if (immediate == null)
            {
                goto Label_0028;
            }
            GameUtility.DestroyGameObject(this.mUnitCursor);
            goto Label_0033;
        Label_0028:
            this.mUnitCursor.FadeOut();
        Label_0033:
            this.mUnitCursor = null;
            return;
        }

        public void HideGimmickForTargetGrid(TacticsUnitController target)
        {
            SceneBattle battle;
            Grid grid;
            SRPG.Unit unit;
            TacticsUnitController controller;
            if (target != null)
            {
                goto Label_000C;
            }
            return;
        Label_000C:
            if (target.Unit == null)
            {
                goto Label_0028;
            }
            if (target.Unit.IsJump == null)
            {
                goto Label_0028;
            }
            return;
        Label_0028:
            battle = SceneBattle.Instance;
            if (battle != null)
            {
                goto Label_003A;
            }
            return;
        Label_003A:
            grid = battle.Battle.GetUnitGridPosition(target.Unit);
            unit = battle.Battle.FindGimmickAtGrid(grid, 1, null);
            controller = battle.FindUnitController(unit);
            if ((controller != null) == null)
            {
                goto Label_0085;
            }
            if (controller.Unit.IsBreakObj != null)
            {
                goto Label_0085;
            }
            controller.ScaleHide();
        Label_0085:
            return;
        }

        public void HideGimmickIcon(EUnitType Type)
        {
            if ((this.mAddIconGauge != null) == null)
            {
                goto Label_001C;
            }
            this.mAddIconGauge.SetEndAnimationAll();
        Label_001C:
            return;
        }

        private void HitDelayed(TacticsUnitController target)
        {
            bool flag;
            HitReactionTypes types;
            bool flag2;
            bool flag3;
            int num;
            if (this.IsRangedRaySkillType() != null)
            {
                goto Label_0023;
            }
            this.mSkillVars.TotalHits = 1;
            this.mSkillVars.NumHitsLeft = 0;
        Label_0023:
            if (this.mSkillVars.mSkillEffect.HitColorBlendTime <= 0f)
            {
                goto Label_0063;
            }
            target.FadeBlendColor(this.mSkillVars.mSkillEffect.HitColor, this.mSkillVars.mSkillEffect.HitColorBlendTime);
        Label_0063:
            flag = 1;
            types = this.mSkillVars.mSkillEffect.RangedHitReactionType;
            flag2 = target.ShouldDodgeHits;
            flag3 = target.ShouldPerfectDodge;
            if (this.IsRangedRaySkillType() == null)
            {
                goto Label_010E;
            }
            if (target.mHitInfo.IsCombo() == null)
            {
                goto Label_010E;
            }
            num = (target.mHitInfo.hits.Count - 1) - this.mSkillVars.NumHitsLeft;
            if (0 > num)
            {
                goto Label_010E;
            }
            if (num >= target.mHitInfo.hits.Count)
            {
                goto Label_010E;
            }
            flag2 = target.mHitInfo.hits[num].is_avoid;
            flag3 = target.mHitInfo.hits[num].is_pf_avoid;
        Label_010E:
            if (types == -1)
            {
                goto Label_0136;
            }
            if (flag2 == null)
            {
                goto Label_0136;
            }
            target.PlayDodge(flag3, 1);
            types = -1;
            flag = this.mSkillVars.mSkillEffect.AlwaysExplode;
        Label_0136:
            if (flag == null)
            {
                goto Label_017A;
            }
            this.HitTarget(target, this.mSkillVars.mSkillEffect.RangedHitReactionType, 1);
            MonoSingleton<GameManager>.Instance.Player.OnDamageToEnemy(this.mUnit, target.Unit, target.mHitInfo.GetTotalHpDamage());
        Label_017A:
            return;
        }

        private void HitTarget(TacticsUnitController target, HitReactionTypes hitReaction, bool doReaction)
        {
            bool flag;
            bool flag2;
            bool flag3;
            bool flag4;
            bool flag5;
            bool flag6;
            int num;
            int num2;
            int num3;
            int num4;
            bool flag7;
            int num5;
            bool flag8;
            bool flag9;
            bool flag10;
            float num6;
            int num7;
            float num8;
            flag = target.mHitInfo.IsCombo();
            flag2 = this.mSkillVars.NumHitsLeft == 0;
            flag3 = this.mSkillVars.UseBattleScene == 0;
            flag4 = 0;
            flag5 = target.mHitInfo.shieldDamage > 0;
            flag6 = ((target.mHitInfo.hitType & 0x20) == 0) == 0;
            num = 0;
            num2 = 0;
            num3 = 0;
            num4 = 0;
            flag7 = target.ShouldDodgeHits;
            if (flag == null)
            {
                goto Label_00FE;
            }
            num5 = (target.mHitInfo.hits.Count - 1) - this.mSkillVars.NumHitsLeft;
            if ((num5 >= 0) && (num5 < target.mHitInfo.hits.Count))
            {
                goto Label_00AE;
            }
            num5 = 0;
        Label_00AE:
            num = target.mHitInfo.hits[num5].hp_damage;
            num2 = target.mHitInfo.hits[num5].mp_damage;
            flag7 = target.mHitInfo.hits[num5].is_avoid;
            goto Label_0118;
        Label_00FE:
            num = target.mHitInfo.GetTotalHpDamage();
            num2 = target.mHitInfo.GetTotalMpDamage();
        Label_0118:
            if (((flag3 == null) || (num >= 1)) || (num3 >= 1))
            {
                goto Label_0130;
            }
            flag3 = 1;
        Label_0130:
            num3 = target.mHitInfo.GetTotalHpHeal();
            num4 = target.mHitInfo.GetTotalMpHeal();
            if ((doReaction == null) || ((num < 1) && (num2 < 1)))
            {
                goto Label_0168;
            }
            target.PlayDamageReaction(1, hitReaction);
        Label_0168:
            if ((target.mKnockBackGrid == null) || (flag2 == null))
            {
                goto Label_0180;
            }
            target.mKnockBackMode = 1;
        Label_0180:
            if (((this.mSkillVars.Skill.effect_type == 0x16) || ((target != this) == null)) || (this.mSkillVars.NumHitsLeft != (this.mSkillVars.TotalHits - 1)))
            {
                goto Label_01D8;
            }
            target.AutoUpdateRotation = 0;
            target.LookAt(base.get_transform().get_position());
        Label_01D8:
            target.DrainGems(this);
            base.StartCoroutine(this.ShowHitPopup(target, target.ShowCriticalEffectOnHit, (target.ShowBackstabEffectOnHit == null) ? 0 : (SceneBattle.Instance != null), target.ShouldDefendHits, target.ShowElementEffectOnHit == 2, target.ShowElementEffectOnHit == 0, 0, 0, 0, 0, flag5, flag6));
            if (((target.ShouldDefendHits != null) || (this.mSkillVars.Skill.effect_type == 0x12)) || (this.mSkillVars.Skill.effect_type == 0x16))
            {
                goto Label_0277;
            }
            this.SpawnHitEffect(target.get_transform().get_position(), flag2);
        Label_0277:
            if ((flag == null) && (flag2 == null))
            {
                goto Label_05A7;
            }
            flag8 = 0;
            if ((this.mSkillVars.Skill.IsDamagedSkill() == null) || (flag7 != null))
            {
                goto Label_045A;
            }
            flag9 = (num < 1) == 0;
            flag10 = (num2 < 1) == 0;
            base.StartCoroutine(this.ShowHitPopup(target, 0, 0, 0, 0, 0, flag9, num, flag10, num2, 0, flag6));
            if ((flag10 == null) || (flag2 == null))
            {
                goto Label_02E3;
            }
            flag4 = 1;
        Label_02E3:
            if ((flag3 == null) || (flag9 == null))
            {
                goto Label_045A;
            }
            num6 = (flag2 == null) ? 0.1f : 0.5f;
            target.UpdateHPRelative(-num, num6, this.mSkillVars.Skill.IsMhmDamage());
            target.OnHitGaugeWeakRegist(this.mUnit);
            target.ChargeIcon.Close();
            if (target.mHitInfo.IsDefend() == null)
            {
                goto Label_038C;
            }
            if ((this.mSkillVars.TotalHits > 1) && ((this.mSkillVars.TotalHits - this.mSkillVars.NumHitsLeft) > 1))
            {
                goto Label_045A;
            }
            target.Unit.PlayBattleVoice("battle_0016");
            goto Label_045A;
        Label_038C:
            num7 = Mathf.Max(num, num2);
            if (this.mSkillVars.TotalHits <= 1)
            {
                goto Label_03B8;
            }
            num7 /= this.mSkillVars.TotalHits;
        Label_03B8:
            num8 = (target.Unit.MaximumStatus.param.hp == null) ? 1f : ((1f * ((float) num7)) / ((float) target.Unit.MaximumStatus.param.hp));
            if (num8 < 0.75f)
            {
                goto Label_0429;
            }
            target.Unit.PlayBattleVoice("battle_0036");
            goto Label_045A;
        Label_0429:
            if (num8 <= 0.5f)
            {
                goto Label_044A;
            }
            target.Unit.PlayBattleVoice("battle_0035");
            goto Label_045A;
        Label_044A:
            target.Unit.PlayBattleVoice("battle_0034");
        Label_045A:
            base.StartCoroutine(this.ShowHealPopup(target, num3, num4));
            if (num3 < 1)
            {
                goto Label_04BB;
            }
            if (flag3 == null)
            {
                goto Label_04BB;
            }
            target.UpdateHPRelative(num3, 0.5f, 0);
            if (flag8 != null)
            {
                goto Label_04BB;
            }
            if (this.Unit == target.Unit)
            {
                goto Label_04BB;
            }
            if (num3 <= 0)
            {
                goto Label_04BB;
            }
            target.Unit.PlayBattleVoice("battle_0018");
            flag8 = 1;
        Label_04BB:
            if (num4 < 1)
            {
                goto Label_04F0;
            }
            flag4 = 1;
            if (flag8 != null)
            {
                goto Label_04F0;
            }
            if (this.Unit == target.Unit)
            {
                goto Label_04F0;
            }
            target.Unit.PlayBattleVoice("battle_0018");
            flag8 = 1;
        Label_04F0:
            if (this.mSkillVars.mIsFinishedBuffEffectTarget != null)
            {
                goto Label_0591;
            }
            if (target.mHitInfo.IsBuffEffect() != null)
            {
                goto Label_0520;
            }
            if (target.mHitInfo.ChangeValueCT == null)
            {
                goto Label_0591;
            }
        Label_0520:
            SceneBattle.Instance.PopupParamChange(target.CenterPosition, target.mHitInfo.buff, target.mHitInfo.debuff, target.mHitInfo.ChangeValueCT);
            if (flag8 != null)
            {
                goto Label_0591;
            }
            if (this.Unit == target.Unit)
            {
                goto Label_0591;
            }
            if (target.mHitInfo.buff.CheckEffect() == null)
            {
                goto Label_0591;
            }
            target.Unit.PlayBattleVoice("battle_0018");
            flag8 = 1;
        Label_0591:
            if (flag4 == null)
            {
                goto Label_05A7;
            }
            UnitQueue.Instance.Refresh(target.Unit);
        Label_05A7:
            return;
        }

        private unsafe bool HitTimerExists(TacticsUnitController target)
        {
            int num;
            HitTimer timer;
            num = 0;
            goto Label_0031;
        Label_0007:
            timer = this.mSkillVars.HitTimers[num];
            if ((&timer.Target == target) == null)
            {
                goto Label_002D;
            }
            return 1;
        Label_002D:
            num += 1;
        Label_0031:
            if (num < this.mSkillVars.HitTimers.Count)
            {
                goto Label_0007;
            }
            return 0;
        }

        public void InitHPGauge(Canvas canvas, UnitGauge gaugeTemplate)
        {
            if ((canvas == null) != null)
            {
                goto Label_0018;
            }
            if ((gaugeTemplate == null) == null)
            {
                goto Label_0019;
            }
        Label_0018:
            return;
        Label_0019:
            if ((this.mHPGauge == null) == null)
            {
                goto Label_0047;
            }
            this.mHPGauge = Object.Instantiate<UnitGauge>(gaugeTemplate);
            this.mHPGauge.SetOwner(this.Unit);
        Label_0047:
            if ((this.mAddIconGauge == null) == null)
            {
                goto Label_0069;
            }
            this.mAddIconGauge = this.mHPGauge.GetComponent<UnitGaugeMark>();
        Label_0069:
            if ((this.mChargeIcon == null) == null)
            {
                goto Label_008B;
            }
            this.mChargeIcon = this.mHPGauge.GetComponent<ChargeIcon>();
        Label_008B:
            if ((this.mDeathSentenceIcon == null) == null)
            {
                goto Label_00BE;
            }
            this.mDeathSentenceIcon = this.mHPGauge.GetComponent<SRPG.DeathSentenceIcon>();
            this.mDeathSentenceIcon.Init(this.Unit);
        Label_00BE:
            this.ResetHPGauge();
            return;
        }

        public void InitShake(Vector3 basePosition, Vector3 direction)
        {
            if (this.mShaker != null)
            {
                goto Label_0016;
            }
            this.mShaker = new ShakeUnit();
        Label_0016:
            this.mShaker.Init(basePosition, direction);
            return;
        }

        private void InternalStartSkill(TacticsUnitController[] targets, Vector3 targetPosition, Camera activeCamera, bool doStateChange)
        {
            TacticsUnitController controller;
            int num;
            if (this.mSkillVars.mSkillSequence != null)
            {
                goto Label_001B;
            }
            Debug.LogError("SkillSequence not loaded yet");
            return;
        Label_001B:
            this.mCollideGround = this.mSkillVars.UseBattleScene == 0;
            this.mSkillVars.mActiveCamera = activeCamera;
            this.mSkillVars.Targets = new List<TacticsUnitController>(targets);
            if (this.mSkillVars.UseBattleScene == null)
            {
                goto Label_0063;
            }
            base.RootMotionMode = 1;
        Label_0063:
            this.mSkillVars.HitTimerThread = base.StartCoroutine(this.AsyncHitTimer());
            this.mSkillVars.mStartPosition = base.get_transform().get_position();
            if (((int) targets.Length) != 1)
            {
                goto Label_00E2;
            }
            this.mSkillVars.mTargetController = targets[0];
            this.mSkillVars.mTargetControllerPosition = this.mSkillVars.mTargetController.get_transform().get_position();
            this.mSkillVars.mTargetController.mCollideGround = this.mCollideGround;
        Label_00E2:
            if (this.mSkillVars.UseBattleScene == null)
            {
                goto Label_010D;
            }
            this.mSkillVars.mTargetPosition = this.mSkillVars.mTargetControllerPosition;
            goto Label_0119;
        Label_010D:
            this.mSkillVars.mTargetPosition = targetPosition;
        Label_0119:
            this.mSkillVars.MaxPlayVoice = this.mSkillVars.NumPlayVoice = this.CountSkillUnitVoice();
            this.mSkillVars.TotalHits = this.mSkillVars.NumHitsLeft = this.CountSkillHits();
            this.mSkillVars.mAuraEnable = this.mSkillVars.mSkillEffect.AuraEffect != null;
            this.mSkillVars.mCameraID = 0;
            this.mSkillVars.mChantCameraID = 0;
            this.mSkillVars.mSkillCameraID = 0;
            this.mSkillVars.mProjectileDataLists.Clear();
            this.mSkillVars.mNumShotCount = 0;
            if (doStateChange == null)
            {
                goto Label_01DC;
            }
            if (this.mSkillVars.Skill.cast_type != 2)
            {
                goto Label_01DC;
            }
            this.GotoState<State_JumpCastComplete>();
            return;
        Label_01DC:
            if ((this.mSkillVars.mSkillEffect != null) == null)
            {
                goto Label_021C;
            }
            if (this.mSkillVars.mSkillEffect.StartSound == null)
            {
                goto Label_021C;
            }
            this.mSkillVars.mSkillEffect.StartSound.Play();
        Label_021C:
            if (doStateChange == null)
            {
                goto Label_02C4;
            }
            if ((this.mSkillVars.mSkillChantAnimation != null) == null)
            {
                goto Label_0240;
            }
            this.GotoState<State_SkillChant>();
            return;
        Label_0240:
            if (this.mSkillVars.Skill.effect_type != 0x12)
            {
                goto Label_025E;
            }
            this.GotoState<State_ChangeGrid>();
            return;
        Label_025E:
            if (this.mSkillVars.Skill.effect_type != 0x16)
            {
                goto Label_027C;
            }
            this.GotoState<State_Throw>();
            return;
        Label_027C:
            this.GotoState<State_Skill>();
            if (this.mSkillVars.Skill.IsTransformSkill() == null)
            {
                goto Label_02C4;
            }
            if (this.mSkillVars.Targets.Count == null)
            {
                goto Label_02C4;
            }
            controller = this.mSkillVars.Targets[0];
            controller.PlayAfterTransform();
        Label_02C4:
            return;
        }

        public bool IsA(string id)
        {
            if (this.Unit == null)
            {
                goto Label_0028;
            }
            if ((this.Unit.UnitParam.iname == id) == null)
            {
                goto Label_0028;
            }
            return 1;
        Label_0028:
            return (this.UniqueName == id);
        }

        public bool IsCastJumpFallComplete()
        {
            return this.mCastJumpFallComplete;
        }

        public bool IsCastJumpStartComplete()
        {
            return this.mCastJumpStartComplete;
        }

        public bool IsDeathSentenceCountDownPlaying()
        {
            if ((this.mDeathSentenceIcon != null) == null)
            {
                goto Label_001D;
            }
            return this.mDeathSentenceIcon.IsDeathSentenceCountDownPlaying;
        Label_001D:
            return 0;
        }

        public bool IsFinishedCastJumpFall()
        {
            return this.mFinishedCastJumpFall;
        }

        public bool IsFinishedLoadSkillEffect()
        {
            return (this.mSkillVars.mSkillEffect != null);
        }

        public bool IsJumpCant()
        {
            return ((this.Unit.CastSkill == null) ? 0 : (2 == this.Unit.CastSkill.CastType));
        }

        private bool IsRangedRaySkillType()
        {
            SkillSequence.SkillTypes types;
            if (this.mSkillVars == null)
            {
                goto Label_0047;
            }
            if (this.mSkillVars.mSkillSequence == null)
            {
                goto Label_0047;
            }
            switch ((this.mSkillVars.mSkillSequence.SkillType - 2))
            {
                case 0:
                    goto Label_0045;

                case 1:
                    goto Label_0045;

                case 2:
                    goto Label_0045;
            }
            goto Label_0047;
        Label_0045:
            return 1;
        Label_0047:
            return 0;
        }

        private bool IsRangedSkillType()
        {
            SkillSequence.SkillTypes types;
            if (this.mSkillVars == null)
            {
                goto Label_004B;
            }
            if (this.mSkillVars.mSkillSequence == null)
            {
                goto Label_004B;
            }
            switch ((this.mSkillVars.mSkillSequence.SkillType - 1))
            {
                case 0:
                    goto Label_0049;

                case 1:
                    goto Label_0049;

                case 2:
                    goto Label_0049;

                case 3:
                    goto Label_0049;
            }
            goto Label_004B;
        Label_0049:
            return 1;
        Label_004B:
            return 0;
        }

        public bool IsShakeEnd()
        {
            return ((this.mShaker.ShakeCount > 0) == 0);
        }

        public bool IsSkillMirror()
        {
            if ((this.mSkillVars == null) || (this.mSkillVars.mSkillSequence == null))
            {
                goto Label_0042;
            }
            return ((this.mSkillVars.mSkillSequence.NotMirror != null) ? 0 : (this.Unit.Side == 1));
        Label_0042:
            return 0;
        }

        public bool IsStartSkill()
        {
            return ((this.mSkillVars == null) == 0);
        }

        protected override unsafe void LateUpdate()
        {
            Transform transform1;
            Transform transform;
            base.LateUpdate();
            if (&this.mVelocity.get_sqrMagnitude() <= 0f)
            {
                goto Label_0043;
            }
            transform1 = base.get_transform();
            transform1.set_position(transform1.get_position() + (this.mVelocity * Time.get_deltaTime()));
        Label_0043:
            if (this.mCollideGround == null)
            {
                goto Label_0054;
            }
            this.SnapToGround();
        Label_0054:
            this.UpdateColorBlending();
            return;
        }

        public void LoadDamageAnimations()
        {
            if (this.mUnit == null)
            {
                goto Label_001C;
            }
            if (this.mUnit.IsBreakObj == null)
            {
                goto Label_001C;
            }
            return;
        Label_001C:
            base.LoadUnitAnimationAsync("B_DMG0", "cmn_damage0", 0, 0);
            base.LoadUnitAnimationAsync("B_DMG1", "cmn_damageair0", 0, 0);
            base.LoadUnitAnimationAsync("B_DOWN", "cmn_down0", 0, 0);
            return;
        }

        public void LoadDeathAnimation(DeathAnimationTypes mask)
        {
            if ((mask & 1) == null)
            {
                goto Label_0030;
            }
            if ((base.FindAnimation("B_DEAD") == null) == null)
            {
                goto Label_0030;
            }
            base.LoadUnitAnimationAsync("B_DEAD", "cmn_downstand0", 0, 0);
        Label_0030:
            return;
        }

        public void LoadDefendAnimations()
        {
            if (this.mUnit == null)
            {
                goto Label_001C;
            }
            if (this.mUnit.IsBreakObj == null)
            {
                goto Label_001C;
            }
            return;
        Label_001C:
            base.LoadUnitAnimationAsync("B_DEF", "cmn_guard0", 0, 0);
            return;
        }

        public void LoadDefendSkillEffect(string skillEffectName)
        {
            DebugUtility.Log("LoadDefendSkillEffect: " + skillEffectName);
            if (string.IsNullOrEmpty(skillEffectName) == null)
            {
                goto Label_001C;
            }
            return;
        Label_001C:
            base.AddLoadThreadCount();
            base.StartCoroutine(this.LoadDefendSkillEffectAsync(skillEffectName));
            return;
        }

        [DebuggerHidden]
        private IEnumerator LoadDefendSkillEffectAsync(string skillEffectName)
        {
            <LoadDefendSkillEffectAsync>c__Iterator51 iterator;
            iterator = new <LoadDefendSkillEffectAsync>c__Iterator51();
            iterator.skillEffectName = skillEffectName;
            iterator.<$>skillEffectName = skillEffectName;
            iterator.<>f__this = this;
            return iterator;
        }

        public void LoadDodgeAnimation()
        {
            if (this.mUnit == null)
            {
                goto Label_001C;
            }
            if (this.mUnit.IsBreakObj == null)
            {
                goto Label_001C;
            }
            return;
        Label_001C:
            base.LoadUnitAnimationAsync("B_DGE", "cmn_dodge0", 0, 0);
            return;
        }

        public void LoadDyingAnimation()
        {
            base.LoadUnitAnimationAsync("B_DIE", "cmn_downstand0", 0, 0);
            return;
        }

        public void LoadGenkidamaAnimation(bool load)
        {
            if (load == null)
            {
                goto Label_001D;
            }
            base.LoadUnitAnimationAsync("GENK", ANIM_GENKIDAMA, 0, 0);
            goto Label_0028;
        Label_001D:
            base.UnloadAnimation("GENK");
        Label_0028:
            return;
        }

        public void LoadRunAnimation(string animationName)
        {
            if (string.IsNullOrEmpty(animationName) == null)
            {
                goto Label_000C;
            }
            return;
        Label_000C:
            base.LoadAnimationAsync("RUN_" + animationName, "CHM/" + animationName);
            return;
        }

        public void LoadRunAnimation(string animationName, string path)
        {
            if (string.IsNullOrEmpty(animationName) == null)
            {
                goto Label_000C;
            }
            return;
        Label_000C:
            base.LoadAnimationAsync("RUN_" + animationName, path);
            return;
        }

        public void LoadSkillEffect(string skillEffectName, bool is_cs_sub)
        {
            this.mSkillVars.mSkillEffect = null;
            if (is_cs_sub == null)
            {
                goto Label_001F;
            }
            skillEffectName = skillEffectName + "_sub";
        Label_001F:
            DebugUtility.Log("LoadSkillEffect: " + skillEffectName);
            if (string.IsNullOrEmpty(skillEffectName) == null)
            {
                goto Label_003B;
            }
            return;
        Label_003B:
            base.AddLoadThreadCount();
            base.StartCoroutine(this.LoadSkillEffectAsync(skillEffectName));
            return;
        }

        [DebuggerHidden]
        private IEnumerator LoadSkillEffectAsync(string skillEffectName)
        {
            <LoadSkillEffectAsync>c__Iterator56 iterator;
            iterator = new <LoadSkillEffectAsync>c__Iterator56();
            iterator.skillEffectName = skillEffectName;
            iterator.<$>skillEffectName = skillEffectName;
            iterator.<>f__this = this;
            return iterator;
        }

        public void LoadSkillSequence(SkillParam skillParam, bool loadJobAnimation, bool useBattleScene, bool is_cs, bool is_cs_sub)
        {
            this.mSkillVars = new SkillVars();
            this.mSkillVars.UseBattleScene = useBattleScene;
            if (useBattleScene == null)
            {
                goto Label_001D;
            }
        Label_001D:
            this.mSkillVars.Skill = skillParam;
            this.mSkillVars.mIsCollaboSkill = is_cs;
            this.mSkillVars.mIsCollaboSkillSub = is_cs_sub;
            base.AddLoadThreadCount();
            base.StartCoroutine(this.LoadSkillSequenceAsync(skillParam, loadJobAnimation));
            return;
        }

        [DebuggerHidden]
        private IEnumerator LoadSkillSequenceAsync(SkillParam skillParam, bool loadJobAnimation)
        {
            <LoadSkillSequenceAsync>c__Iterator55 iterator;
            iterator = new <LoadSkillSequenceAsync>c__Iterator55();
            iterator.skillParam = skillParam;
            iterator.loadJobAnimation = loadJobAnimation;
            iterator.<$>skillParam = skillParam;
            iterator.<$>loadJobAnimation = loadJobAnimation;
            iterator.<>f__this = this;
            return iterator;
        }

        public void LoadTransformAnimation(string name)
        {
            if ((base.FindAnimation("B_TRANSFORM") == null) == null)
            {
                goto Label_0024;
            }
            base.LoadUnitAnimationAsync("B_TRANSFORM", name, 0, 0);
        Label_0024:
            return;
        }

        public void LockUpdateBadStatus(EUnitCondition condition, bool is_force)
        {
            if (is_force != null)
            {
                goto Label_0018;
            }
            if (this.Unit.IsUnitCondition(condition) == null)
            {
                goto Label_0018;
            }
            return;
        Label_0018:
            this.mBadStatusLocks |= (int) condition;
            return;
        }

        public void LookAt(Component target)
        {
            this.LookAt(target.get_transform().get_position());
            return;
        }

        public unsafe void LookAt(Vector3 position)
        {
            Transform transform;
            Vector3 vector;
            if (this.mUnit == null)
            {
                goto Label_001C;
            }
            if (this.mUnit.IsBreakObj == null)
            {
                goto Label_001C;
            }
            return;
        Label_001C:
            transform = base.get_transform();
            vector = position - transform.get_position();
            &vector.y = 0f;
            transform.set_rotation(Quaternion.LookRotation(vector));
            return;
        }

        public void LookAtTarget()
        {
            TacticsUnitController controller;
            if (this.mSkillVars == null)
            {
                goto Label_001B;
            }
            if (this.mSkillVars.Skill != null)
            {
                goto Label_001C;
            }
        Label_001B:
            return;
        Label_001C:
            if (this.mSkillVars.Skill.IsTargetTeleport != null)
            {
                goto Label_0032;
            }
            return;
        Label_0032:
            if (this.mSkillVars.Targets == null)
            {
                goto Label_0057;
            }
            if (this.mSkillVars.Targets.Count != null)
            {
                goto Label_0058;
            }
        Label_0057:
            return;
        Label_0058:
            controller = this.mSkillVars.Targets[0];
            if (controller == null)
            {
                goto Label_0081;
            }
            this.LookAt(controller.CenterPosition);
        Label_0081:
            return;
        }

        private void MoveAgain()
        {
            this.GotoState<State_Move>();
            return;
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();
            GameUtility.DestroyGameObject(this.mPet);
            GameUtility.DestroyGameObject(this.mHPGauge);
            Instances.Remove(this);
            return;
        }

        protected override void OnEvent(AnimEvent e, float time, float weight)
        {
            if (this.mSkillVars == null)
            {
                goto Label_0044;
            }
            if ((e as CameraShake) == null)
            {
                goto Label_0044;
            }
            this.mSkillVars.mCameraShakeOffset = (e as CameraShake).CalcOffset(time, this.mSkillVars.mCameraShakeSeedX, this.mSkillVars.mCameraShakeSeedY);
            return;
        Label_0044:
            return;
        }

        protected override unsafe void OnEventStart(AnimEvent e, float weight)
        {
            HitFrame frame;
            int num;
            TacticsUnitController controller;
            bool flag;
            bool flag2;
            int num2;
            bool flag3;
            ChantFrame frame2;
            Vector3 vector;
            Quaternion quaternion;
            Transform transform;
            ProjectileFrame frame3;
            Vector3 vector2;
            Quaternion quaternion2;
            ProjectileData data;
            ProjectileFrameHitOnly only;
            Transform transform2;
            Transform transform3;
            MapProjectile projectile;
            float num3;
            Vector3 vector3;
            int num4;
            float num5;
            float num6;
            Vector3 vector4;
            float num7;
            float num8;
            AuraFrame frame4;
            Vector3 vector5;
            Quaternion quaternion3;
            GameObject obj2;
            Transform transform4;
            Transform transform5;
            bool flag4;
            float num9;
            int num10;
            TacticsUnitController controller2;
            int num11;
            float num12;
            Vector3 vector6;
            SkillSequence.SkillTypes types;
            TargetState.StateTypes types2;
            if (this.mSkillVars == null)
            {
                goto Label_0D4D;
            }
            if ((e as ToggleCamera) == null)
            {
                goto Label_007A;
            }
            this.mSkillVars.mCameraID = (e as ToggleCamera).CameraID;
            if ((this.mSkillVars.mCameraID >= 0) && (this.mSkillVars.mCameraID <= 1))
            {
                goto Label_0079;
            }
            Debug.LogError("Invalid CameraID: " + ((int) this.mSkillVars.mCameraID));
            this.mSkillVars.mCameraID = 0;
        Label_0079:
            return;
        Label_007A:
            if (((e as HitFrame) == null) || ((this.mSkillVars.mSkillSequence.SkillType != null) && (((this.mSkillVars.mSkillEffect != null) == null) || (this.mSkillVars.mSkillEffect.IsTeleportMode == null))))
            {
                goto Label_02EE;
            }
            frame = e as HitFrame;
            this.mSkillVars.NumHitsLeft -= 1;
            num = 0;
            goto Label_02D7;
        Label_00E6:
            controller = this.mSkillVars.Targets[num];
            flag = controller.ShouldDodgeHits;
            flag2 = controller.ShouldPerfectDodge;
            if (controller.mHitInfo.IsCombo() == null)
            {
                goto Label_0187;
            }
            num2 = (controller.mHitInfo.hits.Count - 1) - this.mSkillVars.NumHitsLeft;
            if ((0 > num2) || (num2 >= controller.mHitInfo.hits.Count))
            {
                goto Label_0187;
            }
            flag = controller.mHitInfo.hits[num2].is_avoid;
            flag2 = controller.mHitInfo.hits[num2].is_pf_avoid;
        Label_0187:
            if (flag == null)
            {
                goto Label_028F;
            }
            flag3 = 1;
            if (this.mSkillVars.UseBattleScene == null)
            {
                goto Label_01BC;
            }
            flag3 = this.mSkillVars.NumHitsLeft == (this.mSkillVars.TotalHits - 1);
        Label_01BC:
            if ((controller.mHitInfo.IsCombo() == null) && (this.mSkillVars.NumHitsLeft != null))
            {
                goto Label_0218;
            }
            this.mSkillVars.Targets[num].PlayDodge(flag2, flag3);
            if ((this.mSkillVars.NumHitsLeft != null) || (controller.mKnockBackGrid == null))
            {
                goto Label_0218;
            }
            controller.mKnockBackMode = 1;
        Label_0218:
            if (((this.mSkillVars.mSkillEffect.AlwaysExplode == null) || (this.mSkillVars.Skill.effect_type == 0x12)) || (this.mSkillVars.Skill.effect_type == 0x16))
            {
                goto Label_02D3;
            }
            this.SpawnHitEffect(this.mSkillVars.Targets[num].get_transform().get_position(), this.mSkillVars.NumHitsLeft == 0);
            goto Label_02D3;
        Label_028F:
            this.HitTarget(this.mSkillVars.Targets[num], frame.ReactionType, 1);
            MonoSingleton<GameManager>.Instance.Player.OnDamageToEnemy(this.mUnit, controller.Unit, controller.mHitInfo.GetTotalHpDamage());
        Label_02D3:
            num += 1;
        Label_02D7:
            if (num < this.mSkillVars.Targets.Count)
            {
                goto Label_00E6;
            }
            return;
        Label_02EE:
            if ((e as ChantFrame) == null)
            {
                goto Label_03E3;
            }
            frame2 = (ChantFrame) e;
            if ((this.mSkillVars.mSkillEffect.ChantEffect != null) == null)
            {
                goto Label_03E2;
            }
            frame2.CalcPosition(base.UnitObject, this.mSkillVars.mSkillEffect.ChantEffect, &vector, &quaternion);
            this.mSkillVars.mChantEffect = (GameObject) Object.Instantiate(this.mSkillVars.mSkillEffect.ChantEffect, vector, quaternion);
            GameUtility.SetLayer(this.mSkillVars.mChantEffect, base.get_gameObject().get_layer(), 1);
            if (frame2.AttachEffect == null)
            {
                goto Label_03E2;
            }
            if (frame2.BoneName.Length <= 0)
            {
                goto Label_03BE;
            }
            transform = GameUtility.findChildRecursively(base.UnitObject.get_transform(), frame2.BoneName);
            goto Label_03CB;
        Label_03BE:
            transform = base.UnitObject.get_transform();
        Label_03CB:
            this.mSkillVars.mChantEffect.get_transform().set_parent(transform);
        Label_03E2:
            return;
        Label_03E3:
            if ((((e as ProjectileFrame) == null) || ((this.mSkillVars.mSkillEffect != null) == null)) || (this.IsRangedSkillType() == null))
            {
                goto Label_09BC;
            }
            this.mSkillVars.mProjectileTriggered = 1;
            frame3 = (ProjectileFrame) e;
            data = new ProjectileData();
            if ((frame3 as ProjectileFrameHitOnly) == null)
            {
                goto Label_0458;
            }
            data.mIsHitOnly = 1;
            only = (ProjectileFrameHitOnly) frame3;
            data.mIsNotSpawnLandingEffect = only.IsSpawnLandingEffect == 0;
        Label_0458:
            if (((this.mSkillVars.mSkillEffect.ProjectileEffect != null) == null) || (data.mIsHitOnly != null))
            {
                goto Label_04CA;
            }
            frame3.CalcPosition(base.UnitObject, this.mSkillVars.mSkillEffect.ProjectileEffect, &vector2, &quaternion2);
            data.mProjectile = Object.Instantiate(this.mSkillVars.mSkillEffect.ProjectileEffect, vector2, quaternion2) as GameObject;
            goto Label_0516;
        Label_04CA:
            frame3.CalcPosition(base.UnitObject, Vector3.get_zero(), Quaternion.get_identity(), &vector2, &quaternion2);
            data.mProjectile = new GameObject("PROJECTILE");
            transform2 = data.mProjectile.get_transform();
            transform2.set_position(vector2);
            transform2.set_rotation(quaternion2);
        Label_0516:
            this.mSkillVars.mProjectileDataLists.Add(data);
            this.mSkillVars.mNumShotCount += 1;
            this.mSkillVars.HitTargets.Clear();
            if (this.mSkillVars.mSkillEffect.ProjectileSound == null)
            {
                goto Label_0575;
            }
            this.mSkillVars.mSkillEffect.ProjectileSound.Play();
        Label_0575:
            if (this.mSkillVars.UseBattleScene == null)
            {
                goto Label_05E2;
            }
            if (data.mProjectileThread != null)
            {
                goto Label_09BB;
            }
            data.mProjectileThread = base.StartCoroutine(this.AnimateProjectile(this.mSkillVars.mSkillEffect.ProjectileStart, this.mSkillVars.mSkillEffect.ProjectileStartTime, data.mProjectile.get_transform().get_position(), Quaternion.get_identity(), null, data));
            goto Label_09BB;
        Label_05E2:
            transform3 = base.get_transform();
            projectile = GameUtility.RequireComponent<MapProjectile>(data.mProjectile);
            if (this.mSkillVars.mSkillEffect.MapHitEffectType != 4)
            {
                goto Label_06ED;
            }
            num3 = 0f;
            vector3 = this.mSkillVars.mTargetPosition - transform3.get_position();
            &vector3.y = 0f;
            &vector3.Normalize();
            num4 = 0;
            goto Label_068D;
        Label_0649:
            num5 = Vector3.Dot(this.mSkillVars.Targets[num4].get_transform().get_position() - transform3.get_position(), vector3);
            if (num5 < num3)
            {
                goto Label_0687;
            }
            num3 = num5;
        Label_0687:
            num4 += 1;
        Label_068D:
            if (num4 < this.mSkillVars.Targets.Count)
            {
                goto Label_0649;
            }
            this.mSkillVars.mTargetPosition = transform3.get_position() + (vector3 * num3);
            &this.mSkillVars.mTargetPosition.y = &GameUtility.RaycastGround(this.mSkillVars.mTargetPosition).y;
        Label_06ED:
            this.ReflectTargetTypeToPos(&this.mSkillVars.mTargetPosition);
            projectile.StartCameraTargetPosition = transform3.get_position();
            projectile.EndCameraTargetPosition = this.mSkillVars.mTargetPosition;
            projectile.GoalPosition = this.mSkillVars.mTargetPosition + (Vector3.get_up() * 0.5f);
            projectile.Speed = this.mSkillVars.mSkillEffect.MapProjectileSpeed;
            projectile.HitDelay = this.mSkillVars.mSkillEffect.MapProjectileHitDelay;
            projectile.OnHit = new MapProjectile.HitEvent(this.OnProjectileHit);
            projectile.OnDistanceUpdate = new MapProjectile.DistanceChangeEvent(this.OnProjectileDistanceChange);
            projectile.mProjectileData = data;
            if ((this.mSkillVars.mSkillEffect.IsTeleportMode == null) || (e.End <= (e.Start + 0.1f)))
            {
                goto Label_0803;
            }
            num6 = e.End - e.Start;
            vector4 = projectile.EndCameraTargetPosition - projectile.StartCameraTargetPosition;
            projectile.Speed = &vector4.get_magnitude() / num6;
        Label_0803:
            if ((((this.mSkillVars.mActiveCamera != null) == null) || (this.mSkillVars.Skill.effect_type == 0x12)) || (this.mSkillVars.mSkillSequence.MapCameraType != null))
            {
                goto Label_08F7;
            }
            projectile.CameraTransform = this.mSkillVars.mActiveCamera.get_transform();
            if ((this.IsRangedRaySkillType() == null) || (this.mSkillVars.TotalHits <= 1))
            {
                goto Label_08F7;
            }
            switch ((this.mSkillVars.mSkillSequence.SkillType - 2))
            {
                case 0:
                    goto Label_08A4;

                case 1:
                    goto Label_08B1;

                case 2:
                    goto Label_08CF;
            }
            goto Label_08F7;
        Label_08A4:
            projectile.CameraTransform = null;
            goto Label_08F7;
        Label_08B1:
            if (this.mSkillVars.mNumShotCount <= 1)
            {
                goto Label_08F7;
            }
            projectile.CameraTransform = null;
            goto Label_08F7;
        Label_08CF:
            if (this.mSkillVars.mNumShotCount >= this.mSkillVars.TotalHits)
            {
                goto Label_08F7;
            }
            projectile.CameraTransform = null;
        Label_08F7:
            if (this.mSkillVars.mSkillEffect.MapTrajectoryType != 1)
            {
                goto Label_09BB;
            }
            projectile.IsArrow = 1;
            num7 = this.mMapTrajectoryHeight;
            if (GameUtility.CalcDistance2D(projectile.GoalPosition - projectile.get_transform().get_position()) > 1.2f)
            {
                goto Label_096C;
            }
            projectile.IsArrow = 0;
            projectile.Speed = this.mSkillVars.mSkillEffect.MapProjectileSpeed;
            goto Label_09BB;
        Label_096C:
            projectile.TimeScale = this.mSkillVars.mSkillEffect.MapTrajectoryTimeScale;
            if (&this.mSkillVars.mStartPosition.y < num7)
            {
                goto Label_09AC;
            }
            num7 = &this.mSkillVars.mStartPosition.y;
        Label_09AC:
            projectile.TopHeight = num7 + 0.5f;
        Label_09BB:
            return;
        Label_09BC:
            if ((e as CameraShake) == null)
            {
                goto Label_09E8;
            }
            this.mSkillVars.mCameraShakeSeedX = Random.get_value();
            this.mSkillVars.mCameraShakeSeedY = Random.get_value();
            return;
        Label_09E8:
            if (((e as TargetState) == null) || ((this.mSkillVars.mTargetController != null) == null))
            {
                goto Label_0A72;
            }
            switch ((e as TargetState).State)
            {
                case 0:
                    goto Label_0A43;

                case 1:
                    goto Label_0A2E;

                case 2:
                    goto Label_0A5D;
            }
            goto Label_0A72;
        Label_0A2E:
            this.mSkillVars.mTargetController.PlayDown();
            goto Label_0A72;
        Label_0A43:
            this.mSkillVars.mTargetController.PlayIdle(0f);
            goto Label_0A72;
        Label_0A5D:
            this.mSkillVars.mTargetController.PlayKirimomi();
        Label_0A72:
            if ((e as AuraFrame) == null)
            {
                goto Label_0B6F;
            }
            if (this.mSkillVars.mAuraEnable != null)
            {
                goto Label_0A8E;
            }
            return;
        Label_0A8E:
            if (((this.mSkillVars.mSkillEffect != null) == null) || ((this.mSkillVars.mSkillEffect.AuraEffect != null) == null))
            {
                goto Label_0B6E;
            }
            frame4 = e as AuraFrame;
            frame4.CalcPosition(base.UnitObject, this.mSkillVars.mSkillEffect.AuraEffect, &vector5, &quaternion3);
            obj2 = Object.Instantiate(this.mSkillVars.mSkillEffect.AuraEffect, vector5, quaternion3) as GameObject;
            transform4 = obj2.get_transform();
            transform5 = null;
            if (string.IsNullOrEmpty(frame4.BoneName) != null)
            {
                goto Label_0B3E;
            }
            transform5 = GameUtility.findChildRecursively(base.UnitObject.get_transform(), frame4.BoneName);
        Label_0B3E:
            if ((transform5 == null) == null)
            {
                goto Label_0B53;
            }
            transform5 = base.GetCharacterRoot();
        Label_0B53:
            transform4.set_parent(transform5);
            this.mSkillVars.mAuras.Add(obj2);
        Label_0B6E:
            return;
        Label_0B6F:
            if ((e as UnitVoiceEvent) == null)
            {
                goto Label_0D4D;
            }
            flag4 = this.mSkillVars.MaxPlayVoice == this.mSkillVars.NumPlayVoice;
            this.mSkillVars.NumPlayVoice -= 1;
            if (flag4 == null)
            {
                goto Label_0C05;
            }
            if ((this.mSkillVars.Skill.IsReactionSkill() == null) || (this.mSkillVars.Skill.IsDamagedSkill() == null))
            {
                goto Label_0BE9;
            }
            this.Unit.PlayBattleVoice("battle_0013");
            return;
        Label_0BE9:
            if (this.ShowCriticalEffectOnHit == null)
            {
                goto Label_0C05;
            }
            this.Unit.PlayBattleVoice("battle_0012");
            return;
        Label_0C05:
            num9 = 0f;
            if (this.mSkillVars.Targets == null)
            {
                goto Label_0D02;
            }
            num10 = 0;
            goto Label_0CEB;
        Label_0C24:
            controller2 = this.mSkillVars.Targets[num10];
            if (controller2.ShouldDodgeHits == null)
            {
                goto Label_0C49;
            }
            goto Label_0CE5;
        Label_0C49:
            num11 = 0;
            if (this.mSkillVars.TotalHits <= 1)
            {
                goto Label_0C7C;
            }
            num11 = controller2.mHitInfo.GetTotalHpDamage() / this.mSkillVars.TotalHits;
            goto Label_0C8A;
        Label_0C7C:
            num11 = controller2.mHitInfo.GetTotalHpDamage();
        Label_0C8A:
            num12 = (this.Unit.MaximumStatus.param.hp == null) ? 1f : ((1f * ((float) num11)) / ((float) this.Unit.MaximumStatus.param.hp));
            num9 = Mathf.Max(num12, num9);
        Label_0CE5:
            num10 += 1;
        Label_0CEB:
            if (num10 < this.mSkillVars.Targets.Count)
            {
                goto Label_0C24;
            }
        Label_0D02:
            if (num9 < 0.75f)
            {
                goto Label_0D1F;
            }
            this.Unit.PlayBattleVoice("battle_0033");
            return;
        Label_0D1F:
            if (num9 <= 0.5f)
            {
                goto Label_0D3C;
            }
            this.Unit.PlayBattleVoice("battle_0032");
            return;
        Label_0D3C:
            this.Unit.PlayBattleVoice("battle_0031");
            return;
        Label_0D4D:
            return;
        }

        private void OnHitGaugeWeakRegist(SRPG.Unit attacker)
        {
            SkillData data;
            if (this.mSkillVars == null)
            {
                goto Label_001B;
            }
            if (this.mSkillVars.Skill != null)
            {
                goto Label_001C;
            }
        Label_001B:
            return;
        Label_001C:
            data = this.mUnit.GetSkillData(this.mSkillVars.Skill.iname);
            if (data != null)
            {
                goto Label_005D;
            }
            data = new SkillData();
            data.Setup(this.mSkillVars.Skill.iname, 1, 1, null);
        Label_005D:
            this.mHPGauge.ActivateElementIcon(1);
            this.mHPGauge.OnAttack(data, data.ElementType, data.ElementValue, attacker.Element, attacker.CurrentStatus.element_assist[attacker.Element]);
            return;
        }

        private void OnProjectileDistanceChange(GameObject go, float distance)
        {
            MapProjectile projectile;
            int num;
            float num2;
            SkillEffect.MapHitEffectTypes types;
            projectile = go.GetComponent<MapProjectile>();
            if (this.mSkillVars.mSkillEffect.MapHitEffectType == 4)
            {
                goto Label_0024;
            }
            goto Label_0101;
        Label_0024:
            num = 0;
            goto Label_00E6;
        Label_002B:
            if (this.mSkillVars.HitTargets.Contains(this.mSkillVars.Targets[num]) == null)
            {
                goto Label_0056;
            }
            goto Label_00E2;
        Label_0056:
            if (projectile.CalcDepth(this.mSkillVars.Targets[num].get_transform().get_position()) > (distance + 0.5f))
            {
                goto Label_00E2;
            }
            this.mSkillVars.HitTimers.Add(new HitTimer(this.mSkillVars.Targets[num], Time.get_time() + this.mSkillVars.mSkillEffect.MapProjectileHitDelay));
            this.mSkillVars.HitTargets.Add(this.mSkillVars.Targets[num]);
        Label_00E2:
            num += 1;
        Label_00E6:
            if (num < this.mSkillVars.Targets.Count)
            {
                goto Label_002B;
            }
        Label_0101:
            return;
        }

        private unsafe void OnProjectileHit(ProjectileData pd)
        {
            TacticsUnitController controller;
            bool flag;
            int num;
            int num2;
            Vector3 vector;
            float num3;
            int num4;
            Vector3 vector2;
            float num5;
            Vector3 vector3;
            Vector3 vector4;
            int num6;
            Vector3 vector5;
            float num7;
            SkillEffect.MapHitEffectTypes types;
            controller = this;
            if (this.mSkillVars.mSkillEffect.IsTeleportMode != null)
            {
                goto Label_002A;
            }
            controller.mSkillVars.NumHitsLeft -= 1;
        Label_002A:
            if (pd == null)
            {
                goto Label_0071;
            }
            if ((pd.mProjectile != null) == null)
            {
                goto Label_0071;
            }
            GameUtility.StopEmitters(pd.mProjectile);
            GameUtility.RequireComponent<OneShotParticle>(pd.mProjectile);
            pd.mProjectile = null;
            controller.mSkillVars.mProjectileDataLists.Remove(pd);
        Label_0071:
            if (this.mSkillVars.mSkillEffect.IsTeleportMode != null)
            {
                goto Label_03DA;
            }
            flag = 1;
            if (pd == null)
            {
                goto Label_0098;
            }
            flag = pd.mIsNotSpawnLandingEffect == 0;
        Label_0098:
            if ((controller.mSkillVars.mSkillEffect.TargetHitEffect != null) == null)
            {
                goto Label_00E0;
            }
            if (flag == null)
            {
                goto Label_00E0;
            }
            GameUtility.SpawnParticle(controller.mSkillVars.mSkillEffect.TargetHitEffect, controller.mSkillVars.mTargetPosition, Quaternion.get_identity(), null);
        Label_00E0:
            switch (this.mSkillVars.mSkillEffect.MapHitEffectType)
            {
                case 0:
                    goto Label_017B;

                case 1:
                    goto Label_0116;

                case 2:
                    goto Label_03DA;

                case 3:
                    goto Label_02F3;

                case 4:
                    goto Label_03DA;

                case 5:
                    goto Label_0234;
            }
            goto Label_03DA;
        Label_0116:
            num = 0;
            goto Label_0160;
        Label_011D:
            this.mSkillVars.HitTimers.Add(new HitTimer(this.mSkillVars.Targets[num], Time.get_time() + (((float) num) * this.mSkillVars.mSkillEffect.MapHitEffectIntervals)));
            num += 1;
        Label_0160:
            if (num < this.mSkillVars.Targets.Count)
            {
                goto Label_011D;
            }
            goto Label_03DA;
        Label_017B:
            num2 = 0;
            goto Label_0219;
        Label_0182:
            vector = this.mSkillVars.Targets[num2].get_transform().get_position() - this.mSkillVars.mTargetPosition;
            &vector.y = 0f;
            float introduced15 = Mathf.Abs(&vector.x);
            num3 = introduced15 + Mathf.Abs(&vector.z);
            this.mSkillVars.HitTimers.Add(new HitTimer(this.mSkillVars.Targets[num2], Time.get_time() + (num3 * this.mSkillVars.mSkillEffect.MapHitEffectIntervals)));
            num2 += 1;
        Label_0219:
            if (num2 < this.mSkillVars.Targets.Count)
            {
                goto Label_0182;
            }
            goto Label_03DA;
        Label_0234:
            num4 = 0;
            goto Label_02D7;
        Label_023C:
            vector2 = this.mSkillVars.Targets[num4].get_transform().get_position() - base.get_transform().get_position();
            &vector2.y = 0f;
            float introduced16 = Mathf.Abs(&vector2.x);
            num5 = introduced16 + Mathf.Abs(&vector2.z);
            this.mSkillVars.HitTimers.Add(new HitTimer(this.mSkillVars.Targets[num4], Time.get_time() + (num5 * this.mSkillVars.mSkillEffect.MapHitEffectIntervals)));
            num4 += 1;
        Label_02D7:
            if (num4 < this.mSkillVars.Targets.Count)
            {
                goto Label_023C;
            }
            goto Label_03DA;
        Label_02F3:
            vector3 = controller.get_transform().get_position();
            &vector3.y = 0f;
            vector4 = controller.get_transform().get_forward();
            &vector4.y = 0f;
            &vector4.Normalize();
            num6 = 0;
            goto Label_03BE;
        Label_0334:
            vector5 = this.mSkillVars.Targets[num6].get_transform().get_position();
            &vector5.y = 0f;
            num7 = Mathf.Max(Vector3.Dot(vector5 - vector3, vector4), 0f);
            this.mSkillVars.HitTimers.Add(new HitTimer(this.mSkillVars.Targets[num6], num7 * this.mSkillVars.mSkillEffect.MapHitEffectIntervals));
            num6 += 1;
        Label_03BE:
            if (num6 < this.mSkillVars.Targets.Count)
            {
                goto Label_0334;
            }
        Label_03DA:
            if (pd == null)
            {
                goto Label_03E7;
            }
            pd.mProjectileHitsTarget = 1;
        Label_03E7:
            return;
        }

        public void OnUnitGaugeModeChange(HPGaugeModes Mode)
        {
            HPGaugeModes modes;
            if ((this.mChargeIcon != null) == null)
            {
                goto Label_007A;
            }
            if (this.Unit.CastSkill != null)
            {
                goto Label_0031;
            }
            this.mChargeIcon.Close();
            goto Label_007A;
        Label_0031:
            modes = Mode;
            switch (modes)
            {
                case 0:
                    goto Label_006A;

                case 1:
                    goto Label_004A;

                case 2:
                    goto Label_005A;
            }
            goto Label_007A;
        Label_004A:
            this.mChargeIcon.Close();
            goto Label_007A;
        Label_005A:
            this.mChargeIcon.Open();
            goto Label_007A;
        Label_006A:
            this.mChargeIcon.Open();
        Label_007A:
            return;
        }

        protected override void OnVisibilityChange(bool visible)
        {
            if (visible == null)
            {
                goto Label_002C;
            }
            if ((this.mShadow != null) == null)
            {
                goto Label_002C;
            }
            this.mShadow.get_gameObject().set_layer(GameUtility.LayerDefault);
        Label_002C:
            return;
        }

        private void PlayAfterTransform()
        {
            if ((base.FindAnimation("B_TRANSFORM") == null) == null)
            {
                goto Label_0017;
            }
            return;
        Label_0017:
            this.GotoState<State_AfterTransform>();
            return;
        }

        public void PlayDamage(HitReactionTypes hitType)
        {
            HitReactionTypes types;
            if (this.mSkillVars == null)
            {
                goto Label_000C;
            }
            return;
        Label_000C:
            if (this.Unit.IsEnablePlayAnimCondition() != null)
            {
                goto Label_001D;
            }
            return;
        Label_001D:
            if (this.mUnit == null)
            {
                goto Label_0039;
            }
            if (this.mUnit.IsBreakObj == null)
            {
                goto Label_0039;
            }
            return;
        Label_0039:
            types = hitType;
            switch (types)
            {
                case 0:
                    goto Label_0052;

                case 1:
                    goto Label_005D;

                case 2:
                    goto Label_0068;
            }
            goto Label_0073;
        Label_0052:
            this.GotoState<State_NormalDamage>();
            goto Label_0073;
        Label_005D:
            this.GotoState<State_AerialDamage>();
            goto Label_0073;
        Label_0068:
            this.PlayKirimomi();
        Label_0073:
            return;
        }

        public void PlayDamageReaction(int damage, HitReactionTypes hitType)
        {
            if (this.ShouldDefendHits == null)
            {
                goto Label_0010;
            }
            goto Label_001E;
        Label_0010:
            if (damage <= 0)
            {
                goto Label_001E;
            }
            this.PlayDamage(hitType);
        Label_001E:
            return;
        }

        public void PlayDead(DeathAnimationTypes type)
        {
            this.GotoState<State_Dead>();
            return;
        }

        public void PlayDodge(bool perfectAvoid, bool is_disp_popup)
        {
            if (is_disp_popup == null)
            {
                goto Label_004B;
            }
            if ((SceneBattle.Instance != null) == null)
            {
                goto Label_004B;
            }
            if (perfectAvoid == null)
            {
                goto Label_0036;
            }
            SceneBattle.Instance.PopupPefectAvoid(base.CenterPosition, 0f);
            goto Label_004B;
        Label_0036:
            SceneBattle.Instance.PopupMiss(base.CenterPosition, 0f);
        Label_004B:
            if (this.mSkillVars == null)
            {
                goto Label_0057;
            }
            return;
        Label_0057:
            if (this.Unit.IsEnablePlayAnimCondition() != null)
            {
                goto Label_0068;
            }
            return;
        Label_0068:
            if ((base.FindAnimation("B_DGE") == null) == null)
            {
                goto Label_007F;
            }
            return;
        Label_007F:
            this.GotoState<State_Dodge>();
            return;
        }

        public void PlayDown()
        {
            this.GotoState<State_Down>();
            return;
        }

        public void PlayGenkidama()
        {
            base.PlayAnimation("GENK", 1, 0.2f, 0f);
            return;
        }

        public void PlayIdle(float interpTime)
        {
            this.mIdleInterpTime = interpTime;
            this.GotoState<State_Idle>();
            return;
        }

        private void PlayIdleSmooth()
        {
            this.PlayIdle(0.1f);
            return;
        }

        public void PlayKirimomi()
        {
            this.GotoState<State_Kirimomi>();
            return;
        }

        public void PlayLookAt(Component target, float spin)
        {
            this.PlayLookAt(target.get_transform().get_position(), spin);
            return;
        }

        public void PlayLookAt(Vector3 target, float spin)
        {
            this.mSpinCount = spin;
            this.mLookAtTarget = target;
            this.GotoState<State_LookAt>();
            return;
        }

        public void PlayPickup(GameObject pickupObject)
        {
            this.mPickupObject = pickupObject;
            this.GotoState<State_Pickup>();
            return;
        }

        public void PlayPreSkillAnimation(Camera cam, Vector3 targetPos)
        {
            if (this.mSkillVars == null)
            {
                goto Label_0029;
            }
            this.mSkillVars.mTargetPosition = targetPos;
            this.mSkillVars.mActiveCamera = cam;
            this.GotoState<State_PreSkill>();
        Label_0029:
            return;
        }

        public void PlayTakenAnimation()
        {
            this.GotoState<State_Taken>();
            return;
        }

        public void PlayTrickKnockBack(bool is_dmg_anm)
        {
            if (this.mKnockBackGrid != null)
            {
                goto Label_000C;
            }
            return;
        Label_000C:
            if (is_dmg_anm == null)
            {
                goto Label_0019;
            }
            this.PlayDamage(0);
        Label_0019:
            this.mKnockBackMode = 1;
            return;
        }

        public void PlayVersusCursor(bool play)
        {
            Animation animation;
            if ((this.mVersusCursor == null) == null)
            {
                goto Label_0012;
            }
            return;
        Label_0012:
            if ((this.mVersusCursorRoot == null) == null)
            {
                goto Label_0024;
            }
            return;
        Label_0024:
            animation = this.mVersusCursorRoot.GetComponent<Animation>();
            if ((animation != null) == null)
            {
                goto Label_0043;
            }
            animation.set_enabled(play);
        Label_0043:
            return;
        }

        protected override unsafe void PostSetup()
        {
            GameSettings settings;
            float num;
            CharacterSettings settings2;
            SRPG.Unit unit;
            int num2;
            SkillParam param;
            GameObject obj2;
            Transform transform;
            Transform transform2;
            settings = GameSettings.Instance;
            num = &settings.Quest.MapCharacterScale;
            base.RootMotionScale = num;
            base.get_transform().set_localScale(Vector3.get_one() * num);
            settings2 = base.UnitObject.GetComponent<CharacterSettings>();
            if ((settings2 != null) == null)
            {
                goto Label_01B5;
            }
            unit = this.Unit;
            if (unit == null)
            {
                goto Label_00E6;
            }
            if (unit.Side != 1)
            {
                goto Label_0071;
            }
            base.VesselColor = settings.Character_EnemyGlowColor;
            goto Label_007D;
        Label_0071:
            base.VesselColor = settings.Character_PlayerGlowColor;
        Label_007D:
            num2 = 0;
            goto Label_00D4;
        Label_0085:
            param = unit.BattleSkills[num2].SkillParam;
            if (param.type != 4)
            {
                goto Label_00CE;
            }
            if (param.effect_type == 3)
            {
                goto Label_00C1;
            }
            if (param.effect_type != 10)
            {
                goto Label_00CE;
            }
        Label_00C1:
            this.LoadDefendSkillEffect(param.defend_effect);
        Label_00CE:
            num2 += 1;
        Label_00D4:
            if (num2 < unit.BattleSkills.Count)
            {
                goto Label_0085;
            }
        Label_00E6:
            settings2.SetSkeleton("small");
            if (base.mUnitObjectLists.Count > 1)
            {
                goto Label_01B5;
            }
            if ((settings2.ShadowProjector != null) == null)
            {
                goto Label_01B5;
            }
            obj2 = settings2.ShadowProjector.get_gameObject();
            this.mShadow = ((GameObject) Object.Instantiate(obj2, base.get_transform().get_position() + obj2.get_transform().get_position(), obj2.get_transform().get_rotation())).GetComponent<Projector>();
            this.mShadow.get_transform().SetParent(base.GetCharacterRoot(), 1);
            this.mShadow.set_ignoreLayers(~(1 << (LayerMask.NameToLayer("BG") & 0x1f)));
            GameUtility.SetLayer(this.mShadow, GameUtility.LayerHidden, 1);
            this.mShadow.set_orthographicSize(this.mShadow.get_orthographicSize() * num);
        Label_01B5:
            if ((this.mPet != null) == null)
            {
                goto Label_01F7;
            }
            transform = base.get_transform();
            transform2 = this.mPet.get_transform();
            transform2.set_position(transform.get_position());
            transform2.set_rotation(transform.get_rotation());
        Label_01F7:
            if (this.Unit == null)
            {
                goto Label_0208;
            }
            this.CacheIcons();
        Label_0208:
            return;
        }

        public void ReflectDispModel()
        {
            int num;
            if (this.mUnit == null)
            {
                goto Label_001B;
            }
            if (this.mUnit.IsBreakObj != null)
            {
                goto Label_001C;
            }
        Label_001B:
            return;
        Label_001C:
            num = this.Unit.GetMapBreakNowStage(this.Unit.CurrentStatus.param.hp);
            if (base.SetActivateUnitObject(num) == null)
            {
                goto Label_0097;
            }
            if (this.mBadStatusEffect == null)
            {
                goto Label_0097;
            }
            if (this.mBadStatus == null)
            {
                goto Label_0097;
            }
            if ((this.mBadStatus.Effect != null) == null)
            {
                goto Label_0097;
            }
            this.attachBadStatusEffect(this.mBadStatusEffect, this.mBadStatus.AttachTarget, 1);
        Label_0097:
            return;
        }

        private unsafe bool ReflectTargetTypeToPos(ref Vector3 pos)
        {
            SceneBattle battle;
            BattleCore core;
            EUnitDirection direction;
            int num;
            IntVector2 vector;
            SkillData data;
            int num2;
            int num3;
            IntVector2 vector2;
            SkillEffect.eTargetTypeForLaser laser;
            Vector3 vector3;
            ESelectType type;
            Vector3 vector4;
            if (this.mSkillVars.Skill == null)
            {
                goto Label_0026;
            }
            if ((this.mSkillVars.mSkillEffect == null) == null)
            {
                goto Label_0028;
            }
        Label_0026:
            return 0;
        Label_0028:
            if (SkillParam.IsTypeLaser(this.mSkillVars.Skill.select_scope) != null)
            {
                goto Label_0044;
            }
            return 0;
        Label_0044:
            battle = SceneBattle.Instance;
            core = null;
            if (battle == null)
            {
                goto Label_005E;
            }
            core = battle.Battle;
        Label_005E:
            if (core != null)
            {
                goto Label_0066;
            }
            return 0;
        Label_0066:
            direction = battle.UnitDirectionFromPosition(base.get_transform().get_position(), *(pos), this.mSkillVars.Skill);
            laser = this.mSkillVars.mSkillEffect.TargetTypeForLaser;
            if (laser == 1)
            {
                goto Label_00B0;
            }
            if (laser == 2)
            {
                goto Label_0139;
            }
            goto Label_0267;
        Label_00B0:
            num = this.mSkillVars.mSkillEffect.StepFrontTypeForLaser;
            vector = battle.CalcCoord(base.get_transform().get_position());
            pos.x = ((float) (&vector.x + (SRPG.Unit.DIRECTION_OFFSETS[direction, 0] * num))) + 0.5f;
            pos.z = ((float) (&vector.y + (SRPG.Unit.DIRECTION_OFFSETS[direction, 1] * num))) + 0.5f;
            pos.y = &GameUtility.RaycastGround(*(pos)).y;
            goto Label_0269;
        Label_0139:
            data = this.mUnit.GetSkillData(this.mSkillVars.Skill.iname);
            if (data == null)
            {
                goto Label_0269;
            }
            num2 = this.mUnit.GetAttackRangeMax(data);
            if (num2 <= 0)
            {
                goto Label_0269;
            }
            num3 = num2 - this.mUnit.GetAttackRangeMin(data);
            switch ((this.mSkillVars.Skill.select_scope - 9))
            {
                case 0:
                    goto Label_01BB;

                case 1:
                    goto Label_01C6;

                case 2:
                    goto Label_01D1;

                case 3:
                    goto Label_01D1;

                case 4:
                    goto Label_01C6;
            }
            goto Label_01D1;
        Label_01BB:
            num3 += 1;
            goto Label_01D1;
        Label_01C6:
            num3 += 2;
        Label_01D1:
            vector2 = battle.CalcCoord(base.get_transform().get_position());
            pos.x = (((float) &vector2.x) + ((((float) SRPG.Unit.DIRECTION_OFFSETS[direction, 0]) * (((float) num3) + 1f)) / 2f)) + 0.5f;
            pos.z = (((float) &vector2.y) + ((((float) SRPG.Unit.DIRECTION_OFFSETS[direction, 1]) * (((float) num3) + 1f)) / 2f)) + 0.5f;
            pos.y = &GameUtility.RaycastGround(*(pos)).y;
            goto Label_0269;
        Label_0267:
            return 0;
        Label_0269:
            return 1;
        }

        public void RemoveObsoleteShieldStates()
        {
            int num;
            if (this.mUnit != null)
            {
                goto Label_000C;
            }
            return;
        Label_000C:
            num = this.Shields.Count - 1;
            goto Label_0055;
        Label_001F:
            if (this.mUnit.Shields.Contains(this.Shields[num].Target) != null)
            {
                goto Label_0051;
            }
            this.Shields.RemoveAt(num);
        Label_0051:
            num -= 1;
        Label_0055:
            if (num >= 0)
            {
                goto Label_001F;
            }
            return;
        }

        public void ResetColorMod()
        {
            this.ColorMod = Color.get_white();
            return;
        }

        public unsafe void ResetHPGauge()
        {
            GameSettings settings;
            Color32[] colorArray;
            this.mCachedHP = this.Unit.CurrentStatus.param.hp;
            this.mCachedHpMax = this.Unit.MaximumStatus.param.hp;
            if ((this.mHPGauge != null) == null)
            {
                goto Label_00FC;
            }
            settings = GameSettings.Instance;
            colorArray = new Color32[1];
            *(&(colorArray[0])) = (this.Unit.Side != null) ? settings.Gauge_EnemyHP_Base : settings.Gauge_PlayerHP_Base;
            this.mHPGauge.MainGauge.Colors = colorArray;
            this.mHPGauge.MainGauge.AnimateRangedValue(this.mCachedHP, this.Unit.MaximumStatus.param.hp, 0f);
            if ((this.mHPGauge.MaxGauge != null) == null)
            {
                goto Label_00FC;
            }
            this.mHPGauge.MaxGauge.UpdateValue(1f);
        Label_00FC:
            return;
        }

        public void ResetRotation()
        {
            base.get_transform().set_rotation(SRPG_Extensions.ToRotation(this.mUnit.Direction));
            return;
        }

        public void ResetScale()
        {
            this.HideGimmickAnim.ResetScale();
            return;
        }

        private unsafe void ResetSkill()
        {
            ProjectileData data;
            List<ProjectileData>.Enumerator enumerator;
            this.mCollideGround = 1;
            if ((this.mSkillVars.mTargetController != null) == null)
            {
                goto Label_002E;
            }
            this.mSkillVars.mTargetController.mCollideGround = 1;
        Label_002E:
            this.StopAura();
            this.mLastHitEffect = null;
            if (this.mSkillVars.HitTimerThread == null)
            {
                goto Label_005C;
            }
            base.StopCoroutine(this.mSkillVars.HitTimerThread);
        Label_005C:
            enumerator = this.mSkillVars.mProjectileDataLists.GetEnumerator();
        Label_006D:
            try
            {
                goto Label_0085;
            Label_0072:
                data = &enumerator.Current;
                GameUtility.DestroyGameObject(data.mProjectile);
            Label_0085:
                if (&enumerator.MoveNext() != null)
                {
                    goto Label_0072;
                }
                goto Label_00A2;
            }
            finally
            {
            Label_0096:
                ((List<ProjectileData>.Enumerator) enumerator).Dispose();
            }
        Label_00A2:
            this.mSkillVars.mProjectileDataLists.Clear();
            this.mSkillVars.mNumShotCount = 0;
            this.UnloadBattleAnimations();
            if (base.UseSubEquipment == null)
            {
                goto Label_00D5;
            }
            base.ResetSubEquipments();
        Label_00D5:
            this.mSkillVars = null;
            this.mHitInfoSelf = null;
            return;
        }

        public void ScaleHide()
        {
            this.HideGimmickAnim.Enable = 1;
            this.HideGimmickAnim.IsHide = 1;
            return;
        }

        public void ScaleShow()
        {
            this.HideGimmickAnim.Enable = 1;
            this.HideGimmickAnim.IsHide = 0;
            return;
        }

        private void SetActiveCameraPosition(Vector3 position, Quaternion rotation)
        {
            Transform transform;
            transform = this.mSkillVars.mActiveCamera.get_transform();
            transform.set_position(position);
            transform.set_rotation(rotation);
            return;
        }

        public void SetArrowTrajectoryHeight(float Height)
        {
            this.mMapTrajectoryHeight = GameUtility.InternalToMapHeight(Height);
            return;
        }

        public unsafe void SetCastJump()
        {
            Vector3 vector;
            this.CollideGround = 1;
            base.SetVisible(0);
            this.mCastJumpStartComplete = 1;
            this.mCastJumpFallComplete = 0;
            this.mFinishedCastJumpFall = 0;
            vector = base.get_transform().get_position();
            &vector.y += this.mCastJumpOffsetY;
            base.get_transform().set_position(vector);
            return;
        }

        public void SetDrainEffect(GameObject eff)
        {
            Transform transform;
            if ((eff != null) == null)
            {
                goto Label_0081;
            }
            transform = GameUtility.findChildRecursively(base.get_transform(), "Bip001");
            if ((transform == null) == null)
            {
                goto Label_0030;
            }
            transform = base.get_transform();
        Label_0030:
            this.mDrainEffect = Object.Instantiate(eff, base.get_transform().get_position(), base.get_transform().get_rotation()) as GameObject;
            this.mDrainEffect.get_transform().SetParent(transform, 0);
            SRPG_Extensions.RequireComponent<OneShotParticle>(this.mDrainEffect);
            this.mDrainEffect.SetActive(0);
        Label_0081:
            return;
        }

        public void SetGimmickIcon(SRPG.Unit TargetUnit)
        {
            if (TargetUnit != null)
            {
                goto Label_0007;
            }
            return;
        Label_0007:
            if ((this.mAddIconGauge != null) == null)
            {
                goto Label_00CC;
            }
            if (TargetUnit.UnitType != 1)
            {
                goto Label_0035;
            }
            this.mAddIconGauge.ChangeAnimationByUnitType(TargetUnit.UnitType);
        Label_0035:
            if (TargetUnit.UnitType != 2)
            {
                goto Label_0062;
            }
            if (TargetUnit.EventTrigger == null)
            {
                goto Label_0062;
            }
            this.mAddIconGauge.SetGemIcon(TargetUnit.EventTrigger.GimmickType);
        Label_0062:
            if (this.mKeepUnitGaugeMarkType == null)
            {
                goto Label_00AA;
            }
            if (this.mKeepUnitGaugeMarkType != this.mAddIconGauge.MarkType)
            {
                goto Label_0099;
            }
            if (this.mKeepUnitGaugeMarkGemIconType == this.mAddIconGauge.GemIconType)
            {
                goto Label_00AA;
            }
        Label_0099:
            this.mAddIconGauge.SetEndAnimation(this.mKeepUnitGaugeMarkType);
        Label_00AA:
            this.mKeepUnitGaugeMarkType = this.mAddIconGauge.MarkType;
            this.mKeepUnitGaugeMarkGemIconType = this.mAddIconGauge.GemIconType;
        Label_00CC:
            return;
        }

        public void SetHitInfo(LogSkill.Target target)
        {
            this.mHitInfo = target;
            return;
        }

        public void SetHitInfoSelf(LogSkill.Target selfTarget)
        {
            this.mHitInfoSelf = selfTarget;
            return;
        }

        public unsafe void SetHPChangeYosou(int newHP, int hpmax_damage)
        {
            GameSettings settings;
            Color32[] colorArray;
            Color32[] colorArray2;
            Color32[] colorArray3;
            if (((this.mHPGauge != null) == null) || ((this.mHPGauge.MainGauge != null) == null))
            {
                goto Label_03EF;
            }
            settings = GameSettings.Instance;
            if ((hpmax_damage <= 0) || ((this.mHPGauge.MaxGauge != null) == null))
            {
                goto Label_0184;
            }
            this.mCachedHpMax = Math.Max(this.Unit.MaximumStatus.param.hp - hpmax_damage, 0);
            this.mCachedHP = Mathf.Clamp(this.mCachedHP, 0, this.mCachedHpMax);
            this.mHPGauge.MaxGauge.UpdateValue((this.Unit.MaximumStatus.param.hp == null) ? 1f : Mathf.Clamp01(((float) this.mCachedHpMax) / ((float) this.Unit.MaximumStatus.param.hp)));
            colorArray = new Color32[1];
            *(&(colorArray[0])) = (this.Unit.Side != null) ? settings.Gauge_EnemyHP_Base : settings.Gauge_PlayerHP_Base;
            this.mHPGauge.MainGauge.Colors = colorArray;
            this.mHPGauge.MainGauge.UpdateValue(Mathf.Clamp01(((this.mCachedHpMax == null) ? 1f : Mathf.Clamp01(((float) this.mCachedHP) / ((float) this.mCachedHpMax))) * this.mHPGauge.MaxGauge.Value));
            goto Label_03EF;
        Label_0184:
            if (newHP >= this.mCachedHP)
            {
                goto Label_0292;
            }
            if (newHP > 0)
            {
                goto Label_01E1;
            }
            colorArray2 = new Color32[1];
            *(&(colorArray2[0])) = (this.Unit.Side != null) ? settings.Gauge_EnemyHP_Damage : settings.Gauge_PlayerHP_Damage;
            &(colorArray2[0]).a = 0xff;
            goto Label_027C;
        Label_01E1:
            colorArray2 = new Color32[2];
            *(&(colorArray2[0])) = (this.Unit.Side != null) ? settings.Gauge_EnemyHP_Base : settings.Gauge_PlayerHP_Base;
            *(&(colorArray2[1])) = (this.Unit.Side != null) ? settings.Gauge_EnemyHP_Damage : settings.Gauge_PlayerHP_Damage;
            &(colorArray2[0]).a = (byte) ((newHP * 0xff) / this.mCachedHP);
            &(colorArray2[1]).a = (byte) (0xff - &(colorArray2[0]).a);
        Label_027C:
            this.mHPGauge.MainGauge.UpdateColors(colorArray2);
            goto Label_03EF;
        Label_0292:
            if (newHP <= this.mCachedHP)
            {
                goto Label_03E9;
            }
            colorArray3 = new Color32[2];
            *(&(colorArray3[0])) = (this.Unit.Side != null) ? settings.Gauge_EnemyHP_Base : settings.Gauge_PlayerHP_Base;
            *(&(colorArray3[1])) = (this.Unit.Side != null) ? settings.Gauge_EnemyHP_Heal : settings.Gauge_PlayerHP_Heal;
            &(colorArray3[0]).a = (this.Unit.MaximumStatus.param.hp == null) ? 0 : ((byte) ((this.mCachedHP * 0xff) / this.Unit.MaximumStatus.param.hp));
            &(colorArray3[1]).a = (byte) (0xff - &(colorArray3[0]).a);
            this.mHPGauge.MainGauge.Value = (this.Unit.MaximumStatus.param.hp == null) ? 1f : Mathf.Clamp01(((float) newHP) / ((float) this.Unit.MaximumStatus.param.hp));
            this.mHPGauge.MainGauge.UpdateColors(colorArray3);
            goto Label_03EF;
        Label_03E9:
            this.ResetHPGauge();
        Label_03EF:
            return;
        }

        public void SetHpCostSkill(int SkillHpCost)
        {
            this.mSkillVars.HpCostDamage = SkillHpCost;
            return;
        }

        public void SetHPGaugeMode(HPGaugeModes mode, SkillData skill, SRPG.Unit attacker)
        {
            UnitGauge gauge;
            if ((this.mHPGauge != null) == null)
            {
                goto Label_0098;
            }
            gauge = this.mHPGauge.GetComponent<UnitGauge>();
            if ((gauge != null) == null)
            {
                goto Label_0037;
            }
            gauge.Mode = mode;
            this.OnUnitGaugeModeChange(mode);
        Label_0037:
            if (mode != 1)
            {
                goto Label_008D;
            }
            if (skill == null)
            {
                goto Label_0098;
            }
            if (attacker == null)
            {
                goto Label_0098;
            }
            this.mHPGauge.Focus(skill, skill.ElementType, skill.ElementValue, attacker.Element, attacker.CurrentStatus.element_assist[attacker.Element]);
            goto Label_0098;
        Label_008D:
            this.mHPGauge.DeactivateElementIcon();
        Label_0098:
            return;
        }

        public void SetLandingGrid(Grid landing)
        {
            if (this.mSkillVars != null)
            {
                goto Label_000C;
            }
            return;
        Label_000C:
            this.mSkillVars.mLandingGrid = landing;
            return;
        }

        public void SetLastHitEffect(GameObject effect)
        {
            this.mLastHitEffect = effect;
            return;
        }

        private void SetMonochrome(bool enable)
        {
            int num;
            int num2;
            if (enable == null)
            {
                goto Label_0053;
            }
            num = 0;
            goto Label_003D;
        Label_000D:
            base.CharacterMaterials[num].EnableKeyword("MONOCHROME_ON");
            base.CharacterMaterials[num].DisableKeyword("MONOCHROME_OFF");
            num += 1;
        Label_003D:
            if (num < base.CharacterMaterials.Count)
            {
                goto Label_000D;
            }
            goto Label_009B;
        Label_0053:
            num2 = 0;
            goto Label_008A;
        Label_005A:
            base.CharacterMaterials[num2].EnableKeyword("MONOCHROME_OFF");
            base.CharacterMaterials[num2].DisableKeyword("MONOCHROME_ON");
            num2 += 1;
        Label_008A:
            if (num2 < base.CharacterMaterials.Count)
            {
                goto Label_005A;
            }
        Label_009B:
            return;
        }

        public void SetRenkeiAura(GameObject eff)
        {
            if ((this.mRenkeiAuraEffect != null) == null)
            {
                goto Label_002F;
            }
            GameUtility.StopEmitters(this.mRenkeiAuraEffect);
            GameUtility.RequireComponent<OneShotParticle>(this.mRenkeiAuraEffect);
            this.mRenkeiAuraEffect = null;
        Label_002F:
            if ((eff != null) == null)
            {
                goto Label_006D;
            }
            this.mRenkeiAuraEffect = Object.Instantiate(eff, Vector3.get_zero(), Quaternion.get_identity()) as GameObject;
            this.mRenkeiAuraEffect.get_transform().SetParent(base.get_transform(), 0);
        Label_006D:
            return;
        }

        public void SetRunAnimation(string animationName)
        {
            bool flag;
            string str;
            flag = 0;
            if (string.IsNullOrEmpty(animationName) != null)
            {
                goto Label_001E;
            }
            str = "RUN_" + animationName;
            goto Label_0024;
        Label_001E:
            str = "RUN";
        Label_0024:
            if ((str == this.mRunAnimation) == null)
            {
                goto Label_0036;
            }
            return;
        Label_0036:
            if (base.IsAnimationPlaying(this.mRunAnimation) == null)
            {
                goto Label_0055;
            }
            base.StopAnimation(this.mRunAnimation);
            flag = 1;
        Label_0055:
            this.mRunAnimation = str;
            if (flag == null)
            {
                goto Label_006F;
            }
            base.PlayAnimation(this.mRunAnimation, 1);
        Label_006F:
            return;
        }

        public void SetRunningSpeed(float speed)
        {
            this.mRunSpeed = speed;
            return;
        }

        public void SetStartPos(Vector3 pos)
        {
            if (this.mSkillVars != null)
            {
                goto Label_000C;
            }
            return;
        Label_000C:
            this.mSkillVars.mStartPosition = pos;
            return;
        }

        public void SetTeleportGrid(Grid teleport)
        {
            if (this.mSkillVars != null)
            {
                goto Label_000C;
            }
            return;
        Label_000C:
            this.mSkillVars.mTeleportGrid = teleport;
            return;
        }

        private void SetupPet()
        {
            Type[] typeArray1;
            GameObject obj2;
            if (this.mUnit.Job == null)
            {
                goto Label_008A;
            }
            if (string.IsNullOrEmpty(this.mUnit.Job.Param.pet) != null)
            {
                goto Label_008A;
            }
            typeArray1 = new Type[] { typeof(PetController) };
            obj2 = new GameObject("Pet", typeArray1);
            this.mPet = obj2.GetComponent<PetController>();
            this.mPet.Owner = base.get_gameObject();
            this.mPet.PetID = this.mUnit.Job.Param.pet;
        Label_008A:
            return;
        }

        public void SetupUnit(SRPG.Unit unit)
        {
            this.mUnit = unit;
            this.mCachedHP = unit.CurrentStatus.param.hp;
            this.mCachedHpMax = unit.MaximumStatus.param.hp;
            this.UniqueName = unit.UniqueName;
            base.get_transform().set_rotation(SRPG_Extensions.ToRotation(this.mUnit.Direction));
            this.SetupPet();
            base.SetupUnit(unit.UnitData, -1);
            return;
        }

        private void ShowCriticalEffect(Vector3 position, float yOffset)
        {
            Camera camera;
            GameSettings settings;
            FlashEffect effect;
            CameraShakeEffect effect2;
            camera = Camera.get_main();
            if ((camera != null) == null)
            {
                goto Label_0084;
            }
            settings = GameSettings.Instance;
            effect = camera.get_gameObject().AddComponent<FlashEffect>();
            effect.Strength = settings.CriticalHit_FlashStrength;
            effect.Duration = settings.CriticalHit_FlashDuration;
            effect2 = camera.get_gameObject().AddComponent<CameraShakeEffect>();
            effect2.Duration = settings.CriticalHit_ShakeDuration;
            effect2.AmplitudeX = settings.CriticalHit_ShakeAmplitudeX;
            effect2.AmplitudeY = settings.CriticalHit_ShakeAmplitudeY;
            effect2.FrequencyX = settings.CriticalHit_ShakeFrequencyX;
            effect2.FrequencyY = settings.CriticalHit_ShakeFrequencyY;
        Label_0084:
            if ((SceneBattle.Instance != null) == null)
            {
                goto Label_00A0;
            }
            SceneBattle.Instance.PopupCritical(position, yOffset);
        Label_00A0:
            return;
        }

        public void ShowCursor(UnitCursor prefab, Color color)
        {
            if ((this.mUnitCursor != null) == null)
            {
                goto Label_0012;
            }
            return;
        Label_0012:
            if ((prefab == null) == null)
            {
                goto Label_001F;
            }
            return;
        Label_001F:
            this.mUnitCursor = Object.Instantiate<UnitCursor>(prefab);
            this.mUnitCursor.get_transform().set_parent(base.get_transform());
            this.mUnitCursor.Color = color;
            this.mUnitCursor.get_transform().set_localPosition(Vector3.get_up() * 0.3f);
            return;
        }

        [DebuggerHidden]
        private IEnumerator ShowHealPopup(TacticsUnitController target, int hpHeal, int mpHeal)
        {
            <ShowHealPopup>c__Iterator53 iterator;
            iterator = new <ShowHealPopup>c__Iterator53();
            iterator.hpHeal = hpHeal;
            iterator.target = target;
            iterator.mpHeal = mpHeal;
            iterator.<$>hpHeal = hpHeal;
            iterator.<$>target = target;
            iterator.<$>mpHeal = mpHeal;
            return iterator;
        }

        [DebuggerHidden]
        private IEnumerator ShowHitPopup(TacticsUnitController target, bool critical, bool backstab, bool guard, bool weak, bool resist, bool hpDamage, int hpDamageValue, bool mpDamage, int mpDamageValue, bool absorb, bool is_guts)
        {
            <ShowHitPopup>c__Iterator52 iterator;
            iterator = new <ShowHitPopup>c__Iterator52();
            iterator.guard = guard;
            iterator.target = target;
            iterator.critical = critical;
            iterator.backstab = backstab;
            iterator.weak = weak;
            iterator.resist = resist;
            iterator.absorb = absorb;
            iterator.is_guts = is_guts;
            iterator.hpDamage = hpDamage;
            iterator.hpDamageValue = hpDamageValue;
            iterator.mpDamage = mpDamage;
            iterator.mpDamageValue = mpDamageValue;
            iterator.<$>guard = guard;
            iterator.<$>target = target;
            iterator.<$>critical = critical;
            iterator.<$>backstab = backstab;
            iterator.<$>weak = weak;
            iterator.<$>resist = resist;
            iterator.<$>absorb = absorb;
            iterator.<$>is_guts = is_guts;
            iterator.<$>hpDamage = hpDamage;
            iterator.<$>hpDamageValue = hpDamageValue;
            iterator.<$>mpDamage = mpDamage;
            iterator.<$>mpDamageValue = mpDamageValue;
            iterator.<>f__this = this;
            return iterator;
        }

        public void ShowHPGauge(bool visible)
        {
            if ((this.mHPGauge != null) == null)
            {
                goto Label_0022;
            }
            this.mHPGauge.get_gameObject().SetActive(visible);
        Label_0022:
            this.mKeepHPGaugeVisible = -1f;
            return;
        }

        public void ShowOwnerIndexUI(bool show)
        {
            if ((this.mOwnerIndexUI == null) == null)
            {
                goto Label_0012;
            }
            return;
        Label_0012:
            this.mOwnerIndexUI.get_gameObject().SetActive(show);
            return;
        }

        public void ShowVersusCursor(bool show)
        {
            if ((this.mVersusCursor == null) == null)
            {
                goto Label_0012;
            }
            return;
        Label_0012:
            this.mVersusCursor.get_gameObject().SetActive(show);
            return;
        }

        public void SkillEffectSelf()
        {
            bool flag;
            int num;
            int num2;
            int num3;
            int num4;
            int num5;
            bool flag2;
            int num6;
            SceneBattle battle;
            Vector3 vector;
            if (this.mHitInfoSelf != null)
            {
                goto Label_000C;
            }
            return;
        Label_000C:
            if (this.mHitInfoSelf.hits.Count > 0)
            {
                goto Label_004D;
            }
            if (this.mHitInfoSelf.buff.CheckEffect() != null)
            {
                goto Label_004D;
            }
            if (this.mHitInfoSelf.debuff.CheckEffect() != null)
            {
                goto Label_004D;
            }
            return;
        Label_004D:
            flag = this.mSkillVars.UseBattleScene == 0;
            num = this.mHitInfoSelf.GetTotalHpDamage();
            num2 = this.mHitInfoSelf.GetTotalMpDamage();
            num3 = this.mHitInfoSelf.GetTotalHpHeal();
            num4 = this.mHitInfoSelf.GetTotalMpHeal();
            num5 = num + this.mSkillVars.HpCostDamage;
            if (flag == null)
            {
                goto Label_00B2;
            }
            if (num >= 1)
            {
                goto Label_00B2;
            }
            if (num3 >= 1)
            {
                goto Label_00B2;
            }
            flag = 0;
        Label_00B2:
            flag2 = 0;
            num6 = 0;
            goto Label_00E3;
        Label_00BD:
            if (this.mHitInfoSelf.debuff.bits[num6] == null)
            {
                goto Label_00DD;
            }
            flag2 = 1;
            goto Label_00FC;
        Label_00DD:
            num6 += 1;
        Label_00E3:
            if (num6 < ((int) this.mHitInfoSelf.debuff.bits.Length))
            {
                goto Label_00BD;
            }
        Label_00FC:
            if (num5 >= 1)
            {
                goto Label_0112;
            }
            if (num2 >= 1)
            {
                goto Label_0112;
            }
            if (flag2 == null)
            {
                goto Label_011A;
            }
        Label_0112:
            this.PlayDamageReaction(1, 0);
        Label_011A:
            if (num5 <= 0)
            {
                goto Label_017E;
            }
            SceneBattle.Instance.PopupDamageNumber(base.CenterPosition, num5);
            MonoSingleton<GameManager>.Instance.Player.OnDamageToEnemy(this.mUnit, this.mUnit, num5);
            if (flag == null)
            {
                goto Label_017E;
            }
            this.UpdateHPRelative(-num5, 0.5f, 0);
            this.OnHitGaugeWeakRegist(this.mUnit);
            this.ChargeIcon.Close();
        Label_017E:
            if (num3 >= 1)
            {
                goto Label_018D;
            }
            if (num4 < 1)
            {
                goto Label_020A;
            }
        Label_018D:
            base.StartCoroutine(this.ShowHealPopup(this, num3, num4));
            if ((this.mDrainEffect != null) == null)
            {
                goto Label_01EC;
            }
            this.mDrainEffect.get_transform().set_position(base.CenterPosition);
            this.mDrainEffect.get_transform().set_rotation(base.get_transform().get_rotation());
            this.mDrainEffect.SetActive(1);
        Label_01EC:
            if (flag == null)
            {
                goto Label_020A;
            }
            this.UpdateHPRelative(num3, 0.5f, 0);
            this.ChargeIcon.Close();
        Label_020A:
            if (this.mSkillVars.mIsFinishedBuffEffectSelf != null)
            {
                goto Label_02A1;
            }
            if (this.mHitInfoSelf.IsBuffEffect() == null)
            {
                goto Label_02A1;
            }
            battle = SceneBattle.Instance;
            if (battle == null)
            {
                goto Label_02A1;
            }
            vector = base.CenterPosition;
            if (this.mSkillVars.UseBattleScene == null)
            {
                goto Label_0281;
            }
            if (battle.Battle == null)
            {
                goto Label_0281;
            }
            vector = battle.CalcGridCenter(battle.Battle.GetUnitGridPosition(this.mHitInfoSelf.target));
        Label_0281:
            battle.PopupParamChange(vector, this.mHitInfoSelf.buff, this.mHitInfoSelf.debuff, 0);
        Label_02A1:
            this.UpdateBadStatus();
            return;
        }

        public unsafe void SkillTurn(LogSkill Act, EUnitDirection AxisDirection)
        {
            SceneBattle battle;
            Vector3 vector;
            Vector3 vector2;
            IntVector2 vector3;
            IntVector2 vector4;
            IntVector2 vector5;
            SkillSequence.SkillTurnTypes types;
            if (this.mSkillVars == null)
            {
                goto Label_001B;
            }
            if (this.mSkillVars.mSkillSequence != null)
            {
                goto Label_001C;
            }
        Label_001B:
            return;
        Label_001C:
            battle = SceneBattle.Instance;
            if ((battle == null) == null)
            {
                goto Label_002F;
            }
            return;
        Label_002F:
            vector = battle.CalcGridCenter(Act.self.x, Act.self.y);
            vector2 = Vector3.get_zero();
            vector3 = new IntVector2();
            if (Act.CauseOfReaction == null)
            {
                goto Label_00AB;
            }
            vector2 = battle.CalcGridCenter(Act.CauseOfReaction.x, Act.CauseOfReaction.y);
            &vector3.x = Act.CauseOfReaction.x;
            &vector3.y = Act.CauseOfReaction.y;
            goto Label_011E;
        Label_00AB:
            vector2 = battle.CalcGridCenter(&Act.pos.x, &Act.pos.y);
            &vector3.x = &Act.pos.x;
            &vector3.y = &Act.pos.y;
            if (this.ReflectTargetTypeToPos(&vector2) == null)
            {
                goto Label_011E;
            }
            vector4 = battle.CalcCoord(vector2);
            &vector3.x = &vector4.x;
            &vector3.y = &vector4.y;
        Label_011E:
            switch (this.mSkillVars.mSkillSequence.SkillTurnType)
            {
                case 0:
                    goto Label_018D;

                case 1:
                    goto Label_0148;

                case 2:
                    goto Label_0192;
            }
            goto Label_01D6;
        Label_0148:
            vector5 = battle.CalcCoord(base.get_transform().get_position());
            if (&vector5.x != &vector3.x)
            {
                goto Label_0181;
            }
            if (&vector5.y == &vector3.y)
            {
                goto Label_01D6;
            }
        Label_0181:
            this.LookAt(vector2);
            goto Label_01D6;
        Label_018D:
            goto Label_01D6;
        Label_0192:
            &vector2.x = &vector.x + ((float) SRPG.Unit.DIRECTION_OFFSETS[AxisDirection, 0]);
            &vector2.z = &vector.z + ((float) SRPG.Unit.DIRECTION_OFFSETS[AxisDirection, 1]);
            this.LookAt(vector2);
        Label_01D6:
            return;
        }

        public unsafe void SnapToGround()
        {
            Transform transform;
            Vector3 vector;
            Vector3 vector2;
            transform = base.get_transform();
            vector = transform.get_position();
            &vector.y = &GameUtility.RaycastGround(vector).y;
            transform.set_position(vector);
            return;
        }

        private void SpawnDefendHitEffect(SkillData defSkill, int useCount, int useCountMax)
        {
            string str;
            GameObject obj2;
            GameObject obj3;
            Animator animator;
            if (defSkill != null)
            {
                goto Label_0007;
            }
            return;
        Label_0007:
            str = defSkill.SkillParam.defend_effect;
            if (string.IsNullOrEmpty(str) != null)
            {
                goto Label_002F;
            }
            if (this.mDefendSkillEffects.ContainsKey(str) != null)
            {
                goto Label_0030;
            }
        Label_002F:
            return;
        Label_0030:
            obj2 = this.mDefendSkillEffects[str];
            if ((obj2 != null) == null)
            {
                goto Label_0097;
            }
            animator = GameUtility.SpawnParticle(obj2, base.get_transform().get_position(), Quaternion.get_identity(), null).GetComponent<Animator>();
            if ((animator != null) == null)
            {
                goto Label_0097;
            }
            animator.SetInteger("skill_count", useCount);
            if (useCountMax <= 0)
            {
                goto Label_0097;
            }
            animator.SetFloat("skill_count_norm", ((float) useCount) / ((float) useCountMax));
        Label_0097:
            return;
        }

        private void SpawnHitEffect(Vector3 pos, bool isLast)
        {
            this.mSkillVars.mSkillEffect.SpawnExplosionEffect(0, pos, Quaternion.get_identity());
            if (isLast == null)
            {
                goto Label_0050;
            }
            if ((this.mLastHitEffect != null) == null)
            {
                goto Label_0050;
            }
            GameUtility.SpawnParticle(this.mLastHitEffect, pos, Quaternion.get_identity(), null);
            TimeManager.StartHitSlow(0.1f, 0.3f);
        Label_0050:
            return;
        }

        protected void Start()
        {
            base.OnAnimationUpdate = new AnimationPlayer.AnimationUpdateEvent(this.AnimationUpdated);
            base.Start();
            if (this.mUnit == null)
            {
                goto Label_0033;
            }
            if (this.mUnit.IsBreakObj != null)
            {
                goto Label_0067;
            }
        Label_0033:
            if (this.Posture != null)
            {
                goto Label_0055;
            }
            base.LoadUnitAnimationAsync("IDLE", ANIM_IDLE_FIELD, 1, 0);
            goto Label_0067;
        Label_0055:
            base.LoadUnitAnimationAsync("IDLE", ANIM_IDLE_DEMO, 0, 0);
        Label_0067:
            this.GotoState<State_WaitResources>();
            return;
        }

        public unsafe float StartMove(Vector3[] route, float directionAngle)
        {
            float num;
            int num2;
            Vector3 vector;
            if (this.IgnoreMove == null)
            {
                goto Label_0018;
            }
            this.IgnoreMove = 0;
            return 0f;
        Label_0018:
            base.get_transform().set_position(*(&(route[0])));
            this.mPostMoveAngle = directionAngle;
            this.mRoute = route;
            this.mRoutePos = 0;
            this.GotoState<State_Move>();
            num = 0f;
            num2 = 1;
            goto Label_0085;
        Label_0057:
            vector = *(&(route[num2 - 1])) - *(&(route[num2]));
            num += &vector.get_magnitude();
            num2 += 1;
        Label_0085:
            if (num2 < ((int) route.Length))
            {
                goto Label_0057;
            }
            return (num / this.mRunSpeed);
        }

        public void StartRunning()
        {
            if (this.mStateMachine.IsInState<State_RunLoop>() != null)
            {
                goto Label_0016;
            }
            this.GotoState<State_RunLoop>();
        Label_0016:
            return;
        }

        public void StartSkill(TacticsUnitController target, Camera activeCamera, SkillParam skill)
        {
            TacticsUnitController[] controllerArray1;
            controllerArray1 = new TacticsUnitController[] { target };
            this.InternalStartSkill(controllerArray1, target.get_transform().get_position(), activeCamera, 1);
            return;
        }

        public void StartSkill(Vector3 targetPosition, Camera activeCamera, TacticsUnitController[] targets, Vector3[] hitGrids, SkillParam skill)
        {
            this.mSkillVars.HitGrids = hitGrids;
            this.InternalStartSkill(targets, targetPosition, activeCamera, 1);
            return;
        }

        public unsafe void StepTo(Vector3 dest)
        {
            Vector3 vector;
            this.mStepStart = base.get_transform().get_position();
            this.mStepEnd = dest;
            vector = this.mStepEnd - this.mStepStart;
            if (&vector.get_magnitude() > &GameSettings.Instance.Quest.AnimateGridSnapRadius)
            {
                goto Label_0050;
            }
            this.GotoState<State_StepNoAnimation>();
            goto Label_0056;
        Label_0050:
            this.GotoState<State_Step>();
        Label_0056:
            return;
        }

        private void StopAura()
        {
            int num;
            GameObject obj2;
            if (this.mSkillVars.mAuras.Count <= 0)
            {
                goto Label_0068;
            }
            num = this.mSkillVars.mAuras.Count - 1;
            goto Label_0051;
        Label_002E:
            obj2 = this.mSkillVars.mAuras[num];
            GameUtility.RequireComponent<OneShotParticle>(obj2);
            GameUtility.StopEmitters(obj2);
            num -= 1;
        Label_0051:
            if (num >= 0)
            {
                goto Label_002E;
            }
            this.mSkillVars.mAuras.Clear();
        Label_0068:
            this.mSkillVars.mAuraEnable = 0;
            return;
        }

        public void StopRenkeiAura()
        {
            this.SetRenkeiAura(null);
            return;
        }

        public void StopRunning()
        {
            if (this.mStateMachine.IsInState<State_Idle>() != null)
            {
                goto Label_001B;
            }
            this.PlayIdle(0f);
        Label_001B:
            return;
        }

        public unsafe bool TriggerFieldAction(Vector3 velocity, bool resetToMove)
        {
            Transform transform;
            float num;
            float num2;
            float num3;
            float num4;
            float num5;
            EUnitDirection direction;
            float num6;
            float num7;
            float num8;
            float num9;
            float num10;
            IntVector2 vector;
            int num11;
            int num12;
            int num13;
            int num14;
            float num15;
            float num16;
            float num17;
            Vector3 vector2;
            GameSettings settings;
            Vector3 vector3;
            Vector3 vector4;
            Vector3 vector5;
            Vector3 vector6;
            Vector3 vector7;
            Vector3 vector8;
            Vector3 vector9;
            Vector3 vector10;
            Vector3 vector11;
            Vector3 vector12;
            Vector3 vector13;
            EUnitDirection direction2;
            transform = base.get_transform();
            if (resetToMove == null)
            {
                goto Label_0024;
            }
            this.mOnFieldActionEnd = new FieldActionEndEvent(this.MoveAgain);
            goto Label_0036;
        Label_0024:
            this.mOnFieldActionEnd = new FieldActionEndEvent(this.PlayIdleSmooth);
        Label_0036:
            num = Mathf.Floor(&transform.get_position().x);
            num2 = Mathf.Floor(&transform.get_position().z);
            num3 = num + 1f;
            num4 = num2 + 1f;
            num5 = 3.402823E+38f;
            direction = 2;
            this.mFieldActionPoint = new Vector2(&base.get_transform().get_position().x, &base.get_transform().get_position().z);
            num6 = 0.3f;
            if (&velocity.x >= 0f)
            {
                goto Label_00EE;
            }
            num7 = &transform.get_position().x - num;
            if (num7 >= num5)
            {
                goto Label_0122;
            }
            num5 = num7;
            direction = 2;
            goto Label_0122;
        Label_00EE:
            if (&velocity.x <= 0f)
            {
                goto Label_0122;
            }
            num8 = num3 - &transform.get_position().x;
            if (num8 >= num5)
            {
                goto Label_0122;
            }
            num5 = num8;
            direction = 0;
        Label_0122:
            if (&velocity.z >= 0f)
            {
                goto Label_015B;
            }
            num9 = &transform.get_position().z - num2;
            if (num9 >= num5)
            {
                goto Label_0190;
            }
            num5 = num9;
            direction = 3;
            goto Label_0190;
        Label_015B:
            if (&velocity.z <= 0f)
            {
                goto Label_0190;
            }
            num10 = num4 - &transform.get_position().z;
            if (num10 >= num5)
            {
                goto Label_0190;
            }
            num5 = num10;
            direction = 1;
        Label_0190:
            if (num5 > 0.3f)
            {
                goto Label_0414;
            }
            vector = SRPG_Extensions.ToOffset(direction);
            num11 = Mathf.FloorToInt(&transform.get_position().x);
            num12 = Mathf.FloorToInt(&transform.get_position().z);
            num13 = num11 + &vector.x;
            num14 = num12 + &vector.y;
            if (this.mWalkableField == null)
            {
                goto Label_0414;
            }
            if (this.mWalkableField.isValid(num13, num14) == null)
            {
                goto Label_0414;
            }
            if (this.mWalkableField.get(num13, num14) < 0)
            {
                goto Label_0414;
            }
            num15 = &GameUtility.RaycastGround(transform.get_position()).y;
            num17 = GameUtility.RaycastGround(((float) num13) + 0.5f, ((float) num14) + 0.5f) - num15;
            direction2 = direction;
            switch (direction2)
            {
                case 0:
                    goto Label_0287;

                case 1:
                    goto Label_02AF;

                case 2:
                    goto Label_0273;

                case 3:
                    goto Label_029B;
            }
            goto Label_02C4;
        Label_0273:
            &this.mFieldActionPoint.x = num - num6;
            goto Label_02C4;
        Label_0287:
            &this.mFieldActionPoint.x = num3 + num6;
            goto Label_02C4;
        Label_029B:
            &this.mFieldActionPoint.y = num2 - num6;
            goto Label_02C4;
        Label_02AF:
            &this.mFieldActionPoint.y = num4 + num6;
        Label_02C4:
            &vector2..ctor(&this.mFieldActionPoint.x, 0f, &this.mFieldActionPoint.y);
            direction2 = direction;
            switch (direction2)
            {
                case 0:
                    goto Label_0306;

                case 1:
                    goto Label_0359;

                case 2:
                    goto Label_0306;

                case 3:
                    goto Label_0359;
            }
            goto Label_03AC;
        Label_0306:
            if (this.AdjustMovePos(3, &vector2) == null)
            {
                goto Label_032D;
            }
            this.mFieldActionPoint = new Vector2(&vector2.x, &vector2.z);
        Label_032D:
            if (this.AdjustMovePos(1, &vector2) == null)
            {
                goto Label_03AC;
            }
            this.mFieldActionPoint = new Vector2(&vector2.x, &vector2.z);
            goto Label_03AC;
        Label_0359:
            if (this.AdjustMovePos(2, &vector2) == null)
            {
                goto Label_0380;
            }
            this.mFieldActionPoint = new Vector2(&vector2.x, &vector2.z);
        Label_0380:
            if (this.AdjustMovePos(0, &vector2) == null)
            {
                goto Label_03AC;
            }
            this.mFieldActionPoint = new Vector2(&vector2.x, &vector2.z);
        Label_03AC:
            settings = GameSettings.Instance;
            this.mFieldActionDir = direction;
            if (num17 > settings.Unit_FallAnimationThreshold)
            {
                goto Label_03D1;
            }
            this.GotoState<State_FieldActionFall>();
            return 1;
        Label_03D1:
            if (num17 > -settings.Unit_StepAnimationThreshold)
            {
                goto Label_03E8;
            }
            this.GotoState<State_FieldActionJump>();
            return 1;
        Label_03E8:
            if (num17 < settings.Unit_JumpAnimationThreshold)
            {
                goto Label_03FE;
            }
            this.GotoState<State_FieldActionJumpUp>();
            return 1;
        Label_03FE:
            if (num17 < settings.Unit_StepAnimationThreshold)
            {
                goto Label_0414;
            }
            this.GotoState<State_FieldActionJump>();
            return 1;
        Label_0414:
            return 0;
        }

        public void UnloadBattleAnimations()
        {
            base.UnloadAnimation("B_DMG0");
            base.UnloadAnimation("B_DMG1");
            base.UnloadAnimation("B_DOWN");
            base.UnloadAnimation("B_DGE");
            base.UnloadAnimation("B_DEF");
            base.UnloadAnimation("B_SKL");
            base.UnloadAnimation("B_CHA");
            base.UnloadAnimation("B_PRS");
            base.UnloadAnimation("B_BS");
            base.UnloadAnimation("B_RUN");
            base.UnloadAnimation("B_RUNL");
            this.LoadGenkidamaAnimation(0);
            base.UnloadAnimation("B_TOSS_LIFT");
            base.UnloadAnimation("B_TOSS_THROW");
            base.UnloadAnimation("B_TRANSFORM");
            return;
        }

        public void UnloadPickupAnimation()
        {
            base.UnloadAnimation("PICK");
            return;
        }

        public void UnloadRunAnimation(string animationName)
        {
            base.UnloadAnimation("RUN_" + animationName);
            return;
        }

        public void UnlockUpdateBadStatus(EUnitCondition condition)
        {
            this.mBadStatusLocks &= ~((int) condition);
            return;
        }

        protected override void Update()
        {
            base.Update();
            if (this.mStateMachine == null)
            {
                goto Label_001C;
            }
            this.mStateMachine.Update();
        Label_001C:
            this.UpdateGauges();
            this.HideGimmickAnim.Update();
            this.execKnockBack();
            return;
        }

        public void UpdateBadStatus()
        {
            BadStatusEffects.Desc desc;
            bool flag;
            int num;
            EUnitCondition condition;
            bool flag2;
            if ((BadStatusEffects.Effects != null) && (this.Unit != null))
            {
                goto Label_0016;
            }
            return;
        Label_0016:
            desc = null;
            flag = 0;
            num = 0;
            goto Label_00AE;
        Label_0021:
            condition = BadStatusEffects.Effects[num].Key;
            if ((this.mBadStatusLocks & ((int) condition)) == null)
            {
                goto Label_0045;
            }
            goto Label_00AA;
        Label_0045:
            if (this.Unit.IsUnitCondition(condition) == null)
            {
                goto Label_00AA;
            }
            if (desc != null)
            {
                goto Label_0097;
            }
            flag2 = 1;
            if ((condition != 0x80000L) || (this.Unit.IsUnitCondition(0x1000000L) == null))
            {
                goto Label_0084;
            }
            flag2 = 0;
        Label_0084:
            if (flag2 == null)
            {
                goto Label_0097;
            }
            desc = BadStatusEffects.Effects[num];
        Label_0097:
            if (this.Unit.IsCurseUnitCondition(condition) == null)
            {
                goto Label_00AA;
            }
            flag = 1;
        Label_00AA:
            num += 1;
        Label_00AE:
            if (num < BadStatusEffects.Effects.Count)
            {
                goto Label_0021;
            }
            if (this.Unit.IsUnitCondition(0x800L) != null)
            {
                goto Label_00F5;
            }
            if ((this.mDeathSentenceIcon != null) == null)
            {
                goto Label_0101;
            }
            this.mDeathSentenceIcon.Close();
            goto Label_0101;
        Label_00F5:
            this.DeathSentenceCountDown(0, 0f);
        Label_0101:
            if ((this.IsCursed == flag) || ((BadStatusEffects.CurseEffect != null) == null))
            {
                goto Label_0168;
            }
            if (flag == null)
            {
                goto Label_014A;
            }
            this.mCurseEffect = Object.Instantiate<GameObject>(BadStatusEffects.CurseEffect);
            this.attachBadStatusEffect(this.mCurseEffect, BadStatusEffects.CurseEffectAttachTarget, 0);
            goto Label_0168;
        Label_014A:
            GameUtility.StopEmitters(this.mCurseEffect);
            this.mCurseEffect.AddComponent<OneShotParticle>();
            this.mCurseEffect = null;
        Label_0168:
            this.IsCursed = flag;
            if (desc != this.mBadStatus)
            {
                goto Label_017C;
            }
            return;
        Label_017C:
            if ((this.mBadStatusEffect != null) == null)
            {
                goto Label_01AB;
            }
            GameUtility.StopEmitters(this.mBadStatusEffect);
            this.mBadStatusEffect.AddComponent<OneShotParticle>();
            this.mBadStatusEffect = null;
        Label_01AB:
            this.mBadStatus = desc;
            if (this.mBadStatus == null)
            {
                goto Label_0270;
            }
            if ((this.mBadStatus.Effect != null) == null)
            {
                goto Label_0201;
            }
            this.mBadStatusEffect = Object.Instantiate<GameObject>(this.mBadStatus.Effect);
            this.attachBadStatusEffect(this.mBadStatusEffect, this.mBadStatus.AttachTarget, 0);
        Label_0201:
            if ((string.IsNullOrEmpty(this.mBadStatus.AnimationName) != null) || (string.IsNullOrEmpty(this.mLoadedBadStatusAnimation) == null))
            {
                goto Label_024F;
            }
            this.mLoadedBadStatusAnimation = this.mBadStatus.AnimationName;
            base.LoadUnitAnimationAsync("BAD", this.mBadStatus.AnimationName, 0, 0);
        Label_024F:
            if (this.Unit.IsUnitCondition(0x800L) == null)
            {
                goto Label_0270;
            }
            this.mDeathSentenceIcon.Open();
        Label_0270:
            this.SetMonochrome(((this.mBadStatus == null) || (this.mBadStatus.Key != 0x20L)) ? 0 : 1);
            return;
        }

        private unsafe void UpdateColorBlending()
        {
            Vector3 vector;
            StaticLightVolume volume;
            Color color;
            Color color2;
            GameSettings settings;
            int num;
            Color color3;
            float num2;
            float num3;
            int num4;
            int num5;
            ColorBlendModes modes;
            vector = base.get_transform().get_position();
            volume = StaticLightVolume.FindVolume(vector);
            if ((volume == null) == null)
            {
                goto Label_003B;
            }
            settings = GameSettings.Instance;
            color = settings.Character_DefaultDirectLitColor;
            color2 = settings.Character_DefaultIndirectLitColor;
            goto Label_0046;
        Label_003B:
            volume.CalcLightColor(vector, &color, &color2);
        Label_0046:
            num = 0;
            goto Label_0084;
        Label_004E:
            base.CharacterMaterials[num].SetColor("_directLitColor", color);
            base.CharacterMaterials[num].SetColor("_indirectLitColor", color2);
            num += 1;
        Label_0084:
            if (num < base.CharacterMaterials.Count)
            {
                goto Label_004E;
            }
            &color3..ctor(0f, 0f, 0f, 0f);
            if (this.mBadStatus == null)
            {
                goto Label_0127;
            }
            if (&this.mBadStatus.BlendColor.a <= 0f)
            {
                goto Label_0127;
            }
            num2 = (1f - Mathf.Cos(Time.get_time() * 3.141593f)) * 0.5f;
            color3 = Color.Lerp(this.mBadStatus.BlendColor, new Color(1f, 1f, 1f, 0f), num2 * 0.5f);
        Label_0127:
            modes = this.mBlendMode;
            if (modes == 1)
            {
                goto Label_0144;
            }
            if (modes == 2)
            {
                goto Label_01B4;
            }
            goto Label_01B4;
        Label_0144:
            this.mBlendColorTime += Time.get_deltaTime();
            num3 = Mathf.Clamp01(this.mBlendColorTime / this.mBlendColorTimeMax);
            if (num3 < 1f)
            {
                goto Label_0182;
            }
            this.mBlendMode = 0;
            goto Label_01AA;
        Label_0182:
            color3 = Color.Lerp(this.mBlendColor, new Color(1f, 1f, 1f, 0f), num3);
        Label_01AA:
            goto Label_01B4;
        Label_01B4:
            if (&color3.a <= 0f)
            {
                goto Label_01D9;
            }
            this.ApplyColorBlending(color3);
            this.mEnableColorBlending = 1;
            goto Label_01F1;
        Label_01D9:
            if (this.mEnableColorBlending == null)
            {
                goto Label_01F1;
            }
            this.mEnableColorBlending = 0;
            this.DisableColorBlending();
        Label_01F1:
            if (&this.ColorMod.r < 1f)
            {
                goto Label_026C;
            }
            if (&this.ColorMod.g < 1f)
            {
                goto Label_026C;
            }
            if (&this.ColorMod.b < 1f)
            {
                goto Label_026C;
            }
            num4 = 0;
            goto Label_0255;
        Label_0238:
            base.CharacterMaterials[num4].DisableKeyword("USE_COLORMOD");
            num4 += 1;
        Label_0255:
            if (num4 < base.CharacterMaterials.Count)
            {
                goto Label_0238;
            }
            goto Label_02C0;
        Label_026C:
            num5 = 0;
            goto Label_02AE;
        Label_0274:
            base.CharacterMaterials[num5].EnableKeyword("USE_COLORMOD");
            base.CharacterMaterials[num5].SetColor("_colorMod", this.ColorMod);
            num5 += 1;
        Label_02AE:
            if (num5 < base.CharacterMaterials.Count)
            {
                goto Label_0274;
            }
        Label_02C0:
            return;
        }

        private void UpdateGauges()
        {
            if ((this.mHPGauge != null) == null)
            {
                goto Label_0074;
            }
            if (this.mKeepHPGaugeVisible < 0f)
            {
                goto Label_0074;
            }
            if (this.mHPGauge.get_gameObject().get_activeInHierarchy() == null)
            {
                goto Label_0074;
            }
            this.mKeepHPGaugeVisible -= Time.get_deltaTime();
            if (this.mKeepHPGaugeVisible > 0f)
            {
                goto Label_0074;
            }
            this.mHPGauge.get_gameObject().SetActive(0);
            this.mKeepHPGaugeVisible = 0f;
        Label_0074:
            if ((this.mAddIconGauge != null) == null)
            {
                goto Label_00AC;
            }
            this.mAddIconGauge.IsGaugeUpdate = this.IsHPGaugeChanging;
            this.mAddIconGauge.IsUnitDead = this.Unit.IsDead;
        Label_00AC:
            return;
        }

        public void UpdateHPRelative(int delta, float duration, bool is_hpmax_damage)
        {
            int num;
            float num2;
            num = this.mCachedHpMax;
            if (is_hpmax_damage == null)
            {
                goto Label_001F;
            }
            this.mCachedHpMax = Math.Max(num + delta, 0);
            delta = 0;
        Label_001F:
            this.mCachedHP += delta;
            this.mCachedHP = Mathf.Clamp(this.mCachedHP, 0, this.mCachedHpMax);
            if (((this.mHPGauge == null) == null) && (this.mHPGauge.MainGauge != null))
            {
                goto Label_006C;
            }
            return;
        Label_006C:
            if ((is_hpmax_damage == null) || (this.mHPGauge.MaxGauge == null))
            {
                goto Label_00FF;
            }
            num2 = (num == null) ? 1f : Mathf.Clamp01(((float) this.mCachedHpMax) / ((float) num));
            this.mHPGauge.MaxGauge.AnimateValue(num2, duration);
            this.mHPGauge.MainGauge.AnimateValue(Mathf.Clamp01(((this.mCachedHpMax == null) ? 0f : Mathf.Clamp01(((float) this.mCachedHP) / ((float) this.mCachedHpMax))) * num2), duration);
            goto Label_011C;
        Label_00FF:
            this.mHPGauge.MainGauge.AnimateRangedValue(this.mCachedHP, this.mCachedHpMax, duration);
        Label_011C:
            this.mHPGauge.get_gameObject().SetActive(1);
            if (this.mKeepHPGaugeVisible < 0f)
            {
                goto Label_0148;
            }
            this.mKeepHPGaugeVisible = 1.5f;
        Label_0148:
            return;
        }

        private void UpdateRotation()
        {
            if (this.mUnit == null)
            {
                goto Label_0041;
            }
            base.get_transform().set_rotation(Quaternion.Slerp(base.get_transform().get_rotation(), SRPG_Extensions.ToRotation(this.mUnit.Direction), Time.get_deltaTime() * 10f));
        Label_0041:
            return;
        }

        public unsafe void UpdateShields(bool is_update_turn)
        {
            ShieldState state;
            List<ShieldState>.Enumerator enumerator;
            List<SRPG.Unit.UnitShield>.Enumerator enumerator2;
            ShieldState state2;
            <UpdateShields>c__AnonStorey1F4 storeyf;
            if (this.mUnit != null)
            {
                goto Label_000C;
            }
            return;
        Label_000C:
            enumerator = this.Shields.GetEnumerator();
        Label_0018:
            try
            {
                goto Label_005C;
            Label_001D:
                state = &enumerator.Current;
                if (state.Target.hp != state.LastHP)
                {
                    goto Label_0055;
                }
                if (state.Target.is_efficacy == null)
                {
                    goto Label_005C;
                }
            Label_0055:
                state.Dirty = 1;
            Label_005C:
                if (&enumerator.MoveNext() != null)
                {
                    goto Label_001D;
                }
                goto Label_0079;
            }
            finally
            {
            Label_006D:
                ((List<ShieldState>.Enumerator) enumerator).Dispose();
            }
        Label_0079:
            storeyf = new <UpdateShields>c__AnonStorey1F4();
            enumerator2 = this.mUnit.Shields.GetEnumerator();
        Label_0091:
            try
            {
                goto Label_0115;
            Label_0096:
                storeyf.unit_shield = &enumerator2.Current;
                if (this.Shields.Find(new Predicate<ShieldState>(storeyf.<>m__CD)) == null)
                {
                    goto Label_00C8;
                }
                goto Label_0115;
            Label_00C8:
                state2 = new ShieldState();
                state2.Target = storeyf.unit_shield;
                state2.LastHP = storeyf.unit_shield.hp;
                state2.LastTurn = storeyf.unit_shield.turn;
                this.Shields.Add(state2);
            Label_0115:
                if (&enumerator2.MoveNext() != null)
                {
                    goto Label_0096;
                }
                goto Label_0132;
            }
            finally
            {
            Label_0126:
                ((List<SRPG.Unit.UnitShield>.Enumerator) enumerator2).Dispose();
            }
        Label_0132:
            return;
        }

        public int HPPercentage
        {
            get
            {
                int num;
                num = (this.Unit.MaximumStatus.param.hp == null) ? 100 : ((this.Unit.CurrentStatus.param.hp * 100) / this.Unit.MaximumStatus.param.hp);
                return Mathf.Clamp(num, 0, 100);
            }
        }

        public int VisibleHPValue
        {
            get
            {
                return this.mCachedHP;
            }
        }

        public ChargeIcon ChargeIcon
        {
            get
            {
                return this.mChargeIcon;
            }
            set
            {
            }
        }

        public SRPG.DeathSentenceIcon DeathSentenceIcon
        {
            get
            {
                return this.mDeathSentenceIcon;
            }
            set
            {
            }
        }

        public bool IsHPGaugeChanging
        {
            get
            {
                if ((this.mHPGauge != null) == null)
                {
                    goto Label_009F;
                }
                if (this.mHPGauge.get_gameObject().get_activeInHierarchy() == null)
                {
                    goto Label_009F;
                }
                if ((this.mHPGauge.MainGauge != null) == null)
                {
                    goto Label_009F;
                }
                if (this.mHPGauge.MainGauge.get_gameObject().get_activeInHierarchy() == null)
                {
                    goto Label_009F;
                }
                if ((this.mHPGauge.MaxGauge != null) == null)
                {
                    goto Label_008E;
                }
                return (this.mHPGauge.MainGauge.IsAnimating | this.mHPGauge.MaxGauge.IsAnimating);
            Label_008E:
                return this.mHPGauge.MainGauge.IsAnimating;
            Label_009F:
                return 0;
            }
        }

        public RectTransform HPGaugeTransform
        {
            get
            {
                return (((this.mHPGauge != null) == null) ? null : this.mHPGauge.GetComponent<RectTransform>());
            }
        }

        private HideGimmickAnimation HideGimmickAnim
        {
            get
            {
                if (this.mHideGimmickAnim != null)
                {
                    goto Label_0022;
                }
                this.mHideGimmickAnim = new HideGimmickAnimation();
                this.mHideGimmickAnim.Init(this);
            Label_0022:
                return this.mHideGimmickAnim;
            }
            set
            {
            }
        }

        public SRPG.Unit Unit
        {
            get
            {
                return this.mUnit;
            }
        }

        public Vector2 FieldActionPoint
        {
            get
            {
                return this.mFieldActionPoint;
            }
        }

        public bool IsPlayingFieldAction
        {
            get
            {
                return this.mStateMachine.IsInKindOfState<State_FieldAction>();
            }
        }

        public bool HasCursor
        {
            get
            {
                return (this.mUnitCursor != null);
            }
        }

        public bool CollideGround
        {
            get
            {
                return this.mCollideGround;
            }
            set
            {
                this.mCollideGround = value;
                return;
            }
        }

        public GridMap<int> WalkableField
        {
            set
            {
                this.mWalkableField = value;
                return;
            }
        }

        public bool isIdle
        {
            get
            {
                return ((this.mStateMachine.IsInState<State_Idle>() != null) ? 1 : this.mStateMachine.IsInState<State_Down>());
            }
        }

        public bool isMoving
        {
            get
            {
                return ((this.mStateMachine.IsInState<State_Move>() != null) ? 1 : this.mStateMachine.IsInState<State_RunLoop>());
            }
        }

        public bool IsLoadedPartially
        {
            get
            {
                return this.mLoadedPartially;
            }
        }

        public bool ShakeStart
        {
            get
            {
                return this.mShaker.ShakeStart;
            }
            set
            {
                this.mShaker.ShakeStart = 1;
                return;
            }
        }

        public bool HasPreSkillAnimation
        {
            get
            {
                return ((this.mSkillVars == null) ? 0 : (this.mSkillVars.mSkillStartAnimation != null));
            }
        }

        public bool IsSkillParentPosZero
        {
            get
            {
                if (this.mSkillVars != null)
                {
                    goto Label_000D;
                }
                return 0;
            Label_000D:
                if ((this.mSkillVars.mSkillAnimation == null) == null)
                {
                    goto Label_0025;
                }
                return 0;
            Label_0025:
                return this.mSkillVars.mSkillAnimation.IsParentPosZero;
            }
        }

        public bool IsPreSkillParentPosZero
        {
            get
            {
                if (this.mSkillVars != null)
                {
                    goto Label_000D;
                }
                return 0;
            Label_000D:
                if ((this.mSkillVars.mSkillStartAnimation == null) == null)
                {
                    goto Label_0025;
                }
                return 0;
            Label_0025:
                return this.mSkillVars.mSkillStartAnimation.IsParentPosZero;
            }
        }

        public SkillEffect LoadedSkillEffect
        {
            get
            {
                return ((this.mSkillVars == null) ? null : this.mSkillVars.mSkillEffect);
            }
        }

        public Grid KnockBackGrid
        {
            set
            {
                this.mKnockBackMode = 0;
                this.mKnockBackGrid = value;
                return;
            }
        }

        public bool IsBusy
        {
            get
            {
                return ((this.isIdle == null) ? 1 : ((this.mKnockBackMode == 0) == 0));
            }
        }

        [CompilerGenerated]
        private sealed class <AnimateProjectile>c__Iterator54 : IEnumerator, IDisposable, IEnumerator<object>
        {
            internal float <pos>__0;
            internal float length;
            internal AnimationClip clip;
            internal TacticsUnitController.ProjectileData pd;
            internal Vector3 basePosition;
            internal Quaternion baseRotation;
            internal TacticsUnitController.ProjectileStopEvent callback;
            internal int $PC;
            internal object $current;
            internal float <$>length;
            internal AnimationClip <$>clip;
            internal TacticsUnitController.ProjectileData <$>pd;
            internal Vector3 <$>basePosition;
            internal Quaternion <$>baseRotation;
            internal TacticsUnitController.ProjectileStopEvent <$>callback;

            public <AnimateProjectile>c__Iterator54()
            {
                base..ctor();
                return;
            }

            [DebuggerHidden]
            public void Dispose()
            {
                this.$PC = -1;
                return;
            }

            public bool MoveNext()
            {
                Transform transform1;
                uint num;
                bool flag;
                num = this.$PC;
                this.$PC = -1;
                switch (num)
                {
                    case 0:
                        goto Label_0025;

                    case 1:
                        goto Label_00E8;

                    case 2:
                        goto Label_0134;
                }
                goto Label_013B;
            Label_0025:
                this.<pos>__0 = 0f;
                goto Label_00E8;
            Label_0035:
                this.clip.SampleAnimation(this.pd.mProjectile, (this.<pos>__0 / this.length) * this.clip.get_length());
                transform1 = this.pd.mProjectile.get_transform();
                transform1.set_position(transform1.get_position() + this.basePosition);
                this.pd.mProjectile.get_transform().set_rotation(this.baseRotation * this.pd.mProjectile.get_transform().get_rotation());
                this.<pos>__0 += Time.get_deltaTime();
                this.$current = new WaitForEndOfFrame();
                this.$PC = 1;
                goto Label_013D;
            Label_00E8:
                if (this.<pos>__0 < this.length)
                {
                    goto Label_0035;
                }
                this.pd.mProjectileThread = null;
                if (this.callback == null)
                {
                    goto Label_0121;
                }
                this.callback(this.pd);
            Label_0121:
                this.$current = null;
                this.$PC = 2;
                goto Label_013D;
            Label_0134:
                this.$PC = -1;
            Label_013B:
                return 0;
            Label_013D:
                return 1;
                return flag;
            }

            [DebuggerHidden]
            public void Reset()
            {
                throw new NotSupportedException();
            }

            object IEnumerator<object>.Current
            {
                [DebuggerHidden]
                get
                {
                    return this.$current;
                }
            }

            object IEnumerator.Current
            {
                [DebuggerHidden]
                get
                {
                    return this.$current;
                }
            }
        }

        [CompilerGenerated]
        private sealed class <AsyncHitTimer>c__Iterator57 : IEnumerator, IDisposable, IEnumerator<object>
        {
            internal int <i>__0;
            internal int $PC;
            internal object $current;
            internal TacticsUnitController <>f__this;

            public <AsyncHitTimer>c__Iterator57()
            {
                base..ctor();
                return;
            }

            [DebuggerHidden]
            public void Dispose()
            {
                this.$PC = -1;
                return;
            }

            public unsafe bool MoveNext()
            {
                uint num;
                TacticsUnitController.HitTimer timer;
                TacticsUnitController.HitTimer timer2;
                int num2;
                bool flag;
                num = this.$PC;
                this.$PC = -1;
                switch (num)
                {
                    case 0:
                        goto Label_0021;

                    case 1:
                        goto Label_011D;
                }
                goto Label_0129;
            Label_0021:
                if (this.<>f__this.mSkillVars == null)
                {
                    goto Label_0122;
                }
                if (this.<>f__this.mSkillVars.HitTimers != null)
                {
                    goto Label_004B;
                }
                goto Label_0122;
            Label_004B:
                this.<i>__0 = 0;
                goto Label_00E6;
            Label_0057:
                timer = this.<>f__this.mSkillVars.HitTimers[this.<i>__0];
                if (&timer.HitTime > Time.get_time())
                {
                    goto Label_00D8;
                }
                timer2 = this.<>f__this.mSkillVars.HitTimers[this.<i>__0];
                this.<>f__this.HitDelayed(&timer2.Target);
                this.<>f__this.mSkillVars.HitTimers.RemoveAt(this.<i>__0--);
            Label_00D8:
                this.<i>__0 += 1;
            Label_00E6:
                if (this.<i>__0 < this.<>f__this.mSkillVars.HitTimers.Count)
                {
                    goto Label_0057;
                }
                this.$current = new WaitForEndOfFrame();
                this.$PC = 1;
                goto Label_012B;
            Label_011D:
                goto Label_0021;
            Label_0122:
                this.$PC = -1;
            Label_0129:
                return 0;
            Label_012B:
                return 1;
                return flag;
            }

            [DebuggerHidden]
            public void Reset()
            {
                throw new NotSupportedException();
            }

            object IEnumerator<object>.Current
            {
                [DebuggerHidden]
                get
                {
                    return this.$current;
                }
            }

            object IEnumerator.Current
            {
                [DebuggerHidden]
                get
                {
                    return this.$current;
                }
            }
        }

        [CompilerGenerated]
        private sealed class <CacheIconsAsync>c__Iterator50 : IEnumerator, IDisposable, IEnumerator<object>
        {
            internal ArtifactParam <skin>__0;
            internal string <jobId>__1;
            internal LoadRequest <req1>__2;
            internal LoadRequest <req2>__3;
            internal LoadRequest <req3>__4;
            internal int $PC;
            internal object $current;
            internal TacticsUnitController <>f__this;

            public <CacheIconsAsync>c__Iterator50()
            {
                base..ctor();
                return;
            }

            [DebuggerHidden]
            public void Dispose()
            {
                this.$PC = -1;
                return;
            }

            public bool MoveNext()
            {
                uint num;
                bool flag;
                num = this.$PC;
                this.$PC = -1;
                switch (num)
                {
                    case 0:
                        goto Label_0029;

                    case 1:
                        goto Label_00F0;

                    case 2:
                        goto Label_010D;

                    case 3:
                        goto Label_012A;
                }
                goto Label_01C4;
            Label_0029:
                this.<skin>__0 = this.<>f__this.UnitData.GetSelectedSkin(-1);
                this.<jobId>__1 = this.<>f__this.Unit.UnitData.CurrentJobId;
                this.<req1>__2 = AssetManager.LoadAsync<Texture2D>(AssetPath.UnitSkinIconSmall(this.<>f__this.Unit.UnitParam, this.<skin>__0, this.<jobId>__1));
                this.<req2>__3 = AssetManager.LoadAsync<Texture2D>(AssetPath.UnitSkinIconMedium(this.<>f__this.Unit.UnitParam, this.<skin>__0, this.<jobId>__1));
                this.<req3>__4 = AssetManager.LoadAsync<Texture2D>(AssetPath.UnitCurrentJobIconSmall(this.<>f__this.Unit.UnitData));
                this.$current = this.<req1>__2.StartCoroutine();
                this.$PC = 1;
                goto Label_01C6;
            Label_00F0:
                this.$current = this.<req2>__3.StartCoroutine();
                this.$PC = 2;
                goto Label_01C6;
            Label_010D:
                this.$current = this.<req3>__4.StartCoroutine();
                this.$PC = 3;
                goto Label_01C6;
            Label_012A:
                this.<>f__this.mIconSmall = this.<req1>__2.asset as Texture2D;
                this.<>f__this.mIconMedium = this.<req2>__3.asset as Texture2D;
                this.<>f__this.mJobIcon = this.<req3>__4.asset as Texture2D;
                if ((this.<>f__this.mIconSmall != null) == null)
                {
                    goto Label_0191;
                }
            Label_0191:
                if ((this.<>f__this.mIconMedium != null) == null)
                {
                    goto Label_01A7;
                }
            Label_01A7:
                if ((this.<>f__this.mJobIcon != null) == null)
                {
                    goto Label_01BD;
                }
            Label_01BD:
                this.$PC = -1;
            Label_01C4:
                return 0;
            Label_01C6:
                return 1;
                return flag;
            }

            [DebuggerHidden]
            public void Reset()
            {
                throw new NotSupportedException();
            }

            object IEnumerator<object>.Current
            {
                [DebuggerHidden]
                get
                {
                    return this.$current;
                }
            }

            object IEnumerator.Current
            {
                [DebuggerHidden]
                get
                {
                    return this.$current;
                }
            }
        }

        [CompilerGenerated]
        private sealed class <LoadDefendSkillEffectAsync>c__Iterator51 : IEnumerator, IDisposable, IEnumerator<object>
        {
            internal string skillEffectName;
            internal LoadRequest <req>__0;
            internal int $PC;
            internal object $current;
            internal string <$>skillEffectName;
            internal TacticsUnitController <>f__this;

            public <LoadDefendSkillEffectAsync>c__Iterator51()
            {
                base..ctor();
                return;
            }

            [DebuggerHidden]
            public void Dispose()
            {
                this.$PC = -1;
                return;
            }

            public bool MoveNext()
            {
                uint num;
                bool flag;
                num = this.$PC;
                this.$PC = -1;
                switch (num)
                {
                    case 0:
                        goto Label_0025;

                    case 1:
                        goto Label_006D;

                    case 2:
                        goto Label_00D2;
                }
                goto Label_00D9;
            Label_0025:
                this.<req>__0 = GameUtility.LoadResourceAsyncChecked<GameObject>("Effects/" + this.skillEffectName);
                if (this.<req>__0.isDone != null)
                {
                    goto Label_006D;
                }
                this.$current = this.<req>__0.StartCoroutine();
                this.$PC = 1;
                goto Label_00DB;
            Label_006D:
                if ((this.<req>__0.asset == null) == null)
                {
                    goto Label_008E;
                }
                this.<>f__this.SetLoadError();
            Label_008E:
                this.<>f__this.mDefendSkillEffects[this.skillEffectName] = (GameObject) this.<req>__0.asset;
                this.<>f__this.RemoveLoadThreadCount();
                this.$current = null;
                this.$PC = 2;
                goto Label_00DB;
            Label_00D2:
                this.$PC = -1;
            Label_00D9:
                return 0;
            Label_00DB:
                return 1;
                return flag;
            }

            [DebuggerHidden]
            public void Reset()
            {
                throw new NotSupportedException();
            }

            object IEnumerator<object>.Current
            {
                [DebuggerHidden]
                get
                {
                    return this.$current;
                }
            }

            object IEnumerator.Current
            {
                [DebuggerHidden]
                get
                {
                    return this.$current;
                }
            }
        }

        [CompilerGenerated]
        private sealed class <LoadSkillEffectAsync>c__Iterator56 : IEnumerator, IDisposable, IEnumerator<object>
        {
            internal string skillEffectName;
            internal LoadRequest <req>__0;
            internal int $PC;
            internal object $current;
            internal string <$>skillEffectName;
            internal TacticsUnitController <>f__this;

            public <LoadSkillEffectAsync>c__Iterator56()
            {
                base..ctor();
                return;
            }

            [DebuggerHidden]
            public void Dispose()
            {
                this.$PC = -1;
                return;
            }

            public bool MoveNext()
            {
                uint num;
                bool flag;
                num = this.$PC;
                this.$PC = -1;
                switch (num)
                {
                    case 0:
                        goto Label_0025;

                    case 1:
                        goto Label_006D;

                    case 2:
                        goto Label_00CC;
                }
                goto Label_00D3;
            Label_0025:
                this.<req>__0 = GameUtility.LoadResourceAsyncChecked<SkillEffect>("SkillEff/" + this.skillEffectName);
                if (this.<req>__0.isDone != null)
                {
                    goto Label_006D;
                }
                this.$current = this.<req>__0.StartCoroutine();
                this.$PC = 1;
                goto Label_00D5;
            Label_006D:
                if ((this.<req>__0.asset == null) == null)
                {
                    goto Label_008E;
                }
                this.<>f__this.SetLoadError();
            Label_008E:
                this.<>f__this.mSkillVars.mSkillEffect = (SkillEffect) this.<req>__0.asset;
                this.<>f__this.RemoveLoadThreadCount();
                this.$current = null;
                this.$PC = 2;
                goto Label_00D5;
            Label_00CC:
                this.$PC = -1;
            Label_00D3:
                return 0;
            Label_00D5:
                return 1;
                return flag;
            }

            [DebuggerHidden]
            public void Reset()
            {
                throw new NotSupportedException();
            }

            object IEnumerator<object>.Current
            {
                [DebuggerHidden]
                get
                {
                    return this.$current;
                }
            }

            object IEnumerator.Current
            {
                [DebuggerHidden]
                get
                {
                    return this.$current;
                }
            }
        }

        [CompilerGenerated]
        private sealed class <LoadSkillSequenceAsync>c__Iterator55 : IEnumerator, IDisposable, IEnumerator<object>
        {
            internal SkillParam skillParam;
            internal LoadRequest <equipReq>__0;
            internal LoadRequest <artifactReq>__1;
            internal JobData <jdata>__2;
            internal ArtifactData <afData>__3;
            internal ArtifactParam <artifact>__4;
            internal int <armIndex>__5;
            internal ArtifactParam <artifact>__6;
            internal EquipmentSet <artifact>__7;
            internal EquipmentSet <equipment>__8;
            internal GameObject <primary>__9;
            internal List<GameObject> <primary_lists>__10;
            internal GameObject <secondary>__11;
            internal List<GameObject> <secondary_lists>__12;
            internal bool <hidden>__13;
            internal string <name>__14;
            internal bool loadJobAnimation;
            internal string <name>__15;
            internal string <name>__16;
            internal string <name>__17;
            internal int $PC;
            internal object $current;
            internal SkillParam <$>skillParam;
            internal bool <$>loadJobAnimation;
            internal TacticsUnitController <>f__this;

            public <LoadSkillSequenceAsync>c__Iterator55()
            {
                base..ctor();
                return;
            }

            [DebuggerHidden]
            public void Dispose()
            {
                this.$PC = -1;
                return;
            }

            public unsafe bool MoveNext()
            {
                uint num;
                bool flag;
                num = this.$PC;
                this.$PC = -1;
                switch (num)
                {
                    case 0:
                        goto Label_0039;

                    case 1:
                        goto Label_015F;

                    case 2:
                        goto Label_0223;

                    case 3:
                        goto Label_0353;

                    case 4:
                        goto Label_03EE;

                    case 5:
                        goto Label_048E;

                    case 6:
                        goto Label_0AC5;

                    case 7:
                        goto Label_0EC1;
                }
                goto Label_0EC8;
            Label_0039:
                this.<>f__this.mSkillVars.mSkillSequence = SkillSequence.FindSequence(this.skillParam.motion);
                if (this.<>f__this.mSkillVars.mSkillSequence != null)
                {
                    goto Label_007E;
                }
                this.<>f__this.SetLoadError();
                goto Label_0EC8;
            Label_007E:
                this.<equipReq>__0 = null;
                this.<artifactReq>__1 = null;
                this.<jdata>__2 = this.<>f__this.mUnit.UnitData.FindJobDataBySkillData(this.skillParam);
                this.<afData>__3 = null;
                if (this.<jdata>__2 != null)
                {
                    goto Label_010C;
                }
                this.<afData>__3 = this.<>f__this.mUnit.UnitData.FindArtifactDataBySkillParam(this.skillParam);
                if ((this.<afData>__3 == null) || (string.IsNullOrEmpty(this.<afData>__3.ArtifactParam.asset) == null))
                {
                    goto Label_010C;
                }
                this.<afData>__3 = null;
            Label_010C:
                if (this.<afData>__3 == null)
                {
                    goto Label_027C;
                }
                this.<artifactReq>__1 = GameUtility.LoadResourceAsyncChecked<EquipmentSet>(AssetPath.Artifacts(this.<afData>__3.ArtifactParam));
                if (this.<artifactReq>__1.isDone != null)
                {
                    goto Label_015F;
                }
                this.$current = this.<artifactReq>__1.StartCoroutine();
                this.$PC = 1;
                goto Label_0ECA;
            Label_015F:
                this.<artifactReq>__1 = ((this.<artifactReq>__1 == null) || ((this.<artifactReq>__1.asset != null) == null)) ? null : this.<artifactReq>__1;
                if (this.<>f__this.Unit.UnitData.CurrentJob == null)
                {
                    goto Label_0256;
                }
                this.<artifact>__4 = MonoSingleton<GameManager>.Instance.MasterParam.GetArtifactParam(this.<>f__this.Unit.UnitData.CurrentJob.Param.artifact);
                this.<equipReq>__0 = GameUtility.LoadResourceAsyncChecked<EquipmentSet>(AssetPath.Artifacts(this.<artifact>__4));
                if (this.<equipReq>__0.isDone != null)
                {
                    goto Label_0223;
                }
                this.$current = this.<equipReq>__0.StartCoroutine();
                this.$PC = 2;
                goto Label_0ECA;
            Label_0223:
                this.<equipReq>__0 = ((this.<equipReq>__0 == null) || ((this.<equipReq>__0.asset != null) == null)) ? null : this.<equipReq>__0;
            Label_0256:
                if ((this.<artifactReq>__1 != null) || (this.<equipReq>__0 != null))
                {
                    goto Label_04AF;
                }
                this.<>f__this.SetLoadError();
                goto Label_04AF;
            Label_027C:
                if ((this.<jdata>__2 == null) || ((this.<jdata>__2.JobID != this.<>f__this.Unit.Job.JobID) == null))
                {
                    goto Label_04AF;
                }
                if (this.<jdata>__2.Artifacts == null)
                {
                    goto Label_0421;
                }
                this.<armIndex>__5 = JobData.GetArtifactSlotIndex(1);
                this.<artifact>__6 = this.<>f__this.mUnit.UnitData.GetEquipArtifactParam(this.<armIndex>__5, this.<jdata>__2);
                if ((this.<artifact>__6 == null) || (this.<artifact>__6.type != 1))
                {
                    goto Label_0386;
                }
                this.<artifactReq>__1 = GameUtility.LoadResourceAsyncChecked<EquipmentSet>(AssetPath.Artifacts(this.<artifact>__6));
                if (this.<artifactReq>__1.isDone != null)
                {
                    goto Label_0353;
                }
                this.$current = this.<artifactReq>__1.StartCoroutine();
                this.$PC = 3;
                goto Label_0ECA;
            Label_0353:
                this.<artifactReq>__1 = ((this.<artifactReq>__1 == null) || ((this.<artifactReq>__1.asset != null) == null)) ? null : this.<artifactReq>__1;
            Label_0386:
                this.<artifact>__6 = MonoSingleton<GameManager>.Instance.MasterParam.GetArtifactParam(this.<jdata>__2.Param.artifact);
                this.<equipReq>__0 = GameUtility.LoadResourceAsyncChecked<EquipmentSet>(AssetPath.Artifacts(this.<artifact>__6));
                if (this.<equipReq>__0.isDone != null)
                {
                    goto Label_03EE;
                }
                this.$current = this.<equipReq>__0.StartCoroutine();
                this.$PC = 4;
                goto Label_0ECA;
            Label_03EE:
                this.<equipReq>__0 = ((this.<equipReq>__0 == null) || ((this.<equipReq>__0.asset != null) == null)) ? null : this.<equipReq>__0;
            Label_0421:
                if ((this.<equipReq>__0 != null) || (string.IsNullOrEmpty(this.<jdata>__2.Param.wepmdl) != null))
                {
                    goto Label_048E;
                }
                this.<equipReq>__0 = GameUtility.LoadResourceAsyncChecked<EquipmentSet>(AssetPath.JobEquipment(this.<jdata>__2.Param));
                if (this.<equipReq>__0.isDone != null)
                {
                    goto Label_048E;
                }
                this.$current = this.<equipReq>__0.StartCoroutine();
                this.$PC = 5;
                goto Label_0ECA;
            Label_048E:
                if ((this.<equipReq>__0 != null) || (this.<artifactReq>__1 != null))
                {
                    goto Label_04AF;
                }
                this.<>f__this.SetLoadError();
            Label_04AF:
                if ((this.<equipReq>__0 == null) && (this.<artifactReq>__1 == null))
                {
                    goto Label_0847;
                }
                this.<artifact>__7 = (this.<artifactReq>__1 == null) ? null : (this.<artifactReq>__1.asset as EquipmentSet);
                this.<equipment>__8 = (this.<equipReq>__0 == null) ? null : (this.<equipReq>__0.asset as EquipmentSet);
                this.<primary>__9 = null;
                this.<primary_lists>__10 = new List<GameObject>();
                if (((this.<artifact>__7 != null) == null) || (this.<artifact>__7.PrimaryForceOverride == null))
                {
                    goto Label_056D;
                }
                this.<primary>__9 = this.<artifact>__7.PrimaryHand;
                this.<primary_lists>__10 = this.<artifact>__7.PrimaryHandChangeLists;
                goto Label_0689;
            Label_056D:
                if (((this.<equipment>__8 != null) == null) || (this.<equipment>__8.PrimaryForceOverride == null))
                {
                    goto Label_05B5;
                }
                this.<primary>__9 = this.<equipment>__8.PrimaryHand;
                this.<primary_lists>__10 = this.<equipment>__8.PrimaryHandChangeLists;
                goto Label_0689;
            Label_05B5:
                this.<primary>__9 = (((this.<artifact>__7 != null) == null) || ((this.<artifact>__7.PrimaryHand != null) == null)) ? null : this.<artifact>__7.PrimaryHand;
                this.<primary_lists>__10 = (((this.<artifact>__7 != null) == null) || (this.<artifact>__7.PrimaryHandChangeLists == null)) ? new List<GameObject>() : this.<artifact>__7.PrimaryHandChangeLists;
                if ((((this.<primary>__9 == null) == null) || ((this.<equipment>__8 != null) == null)) || ((this.<equipment>__8.PrimaryHand != null) == null))
                {
                    goto Label_0689;
                }
                this.<primary>__9 = this.<equipment>__8.PrimaryHand;
                this.<primary_lists>__10 = this.<equipment>__8.PrimaryHandChangeLists;
            Label_0689:
                this.<secondary>__11 = null;
                this.<secondary_lists>__12 = new List<GameObject>();
                if (((this.<artifact>__7 != null) == null) || (this.<artifact>__7.SecondaryForceOverride == null))
                {
                    goto Label_06E3;
                }
                this.<secondary>__11 = this.<artifact>__7.SecondaryHand;
                this.<secondary_lists>__12 = this.<artifact>__7.SecondaryHandChangeLists;
                goto Label_07FF;
            Label_06E3:
                if (((this.<equipment>__8 != null) == null) || (this.<equipment>__8.PrimaryForceOverride == null))
                {
                    goto Label_072B;
                }
                this.<secondary>__11 = this.<equipment>__8.SecondaryHand;
                this.<secondary_lists>__12 = this.<equipment>__8.SecondaryHandChangeLists;
                goto Label_07FF;
            Label_072B:
                this.<secondary>__11 = (((this.<artifact>__7 != null) == null) || ((this.<artifact>__7.SecondaryHand != null) == null)) ? null : this.<artifact>__7.SecondaryHand;
                this.<secondary_lists>__12 = (((this.<artifact>__7 != null) == null) || (this.<artifact>__7.SecondaryHandChangeLists == null)) ? new List<GameObject>() : this.<artifact>__7.SecondaryHandChangeLists;
                if ((this.<secondary>__11 == null) == null)
                {
                    goto Label_07FF;
                }
                if ((this.<equipment>__8 != null) == null)
                {
                    goto Label_07FF;
                }
                if ((this.<equipment>__8.SecondaryHand != null) == null)
                {
                    goto Label_07FF;
                }
                this.<secondary>__11 = this.<equipment>__8.SecondaryHand;
                this.<secondary_lists>__12 = this.<equipment>__8.SecondaryHandChangeLists;
            Label_07FF:
                this.<>f__this.HideEquipments();
                this.<hidden>__13 = this.skillParam.cast_type == 2;
                this.<>f__this.SetSubEquipment(this.<primary>__9, this.<secondary>__11, this.<primary_lists>__10, this.<secondary_lists>__12, this.<hidden>__13);
            Label_0847:
                if (string.IsNullOrEmpty(&this.<>f__this.mSkillVars.mSkillSequence.ChantAnimation.Name) != null)
                {
                    goto Label_08E2;
                }
                this.<name>__14 = &this.<>f__this.mSkillVars.mSkillSequence.ChantAnimation.Name;
                if (this.<>f__this.mSkillVars.mIsCollaboSkillSub == null)
                {
                    goto Label_08B6;
                }
                this.<name>__14 = this.<name>__14 + "_sub";
            Label_08B6:
                this.<>f__this.LoadUnitAnimationAsync("B_CHA", this.<name>__14, this.loadJobAnimation, this.<>f__this.mSkillVars.mIsCollaboSkill);
            Label_08E2:
                if (string.IsNullOrEmpty(&this.<>f__this.mSkillVars.mSkillSequence.SkillAnimation.Name) != null)
                {
                    goto Label_097D;
                }
                this.<name>__15 = &this.<>f__this.mSkillVars.mSkillSequence.SkillAnimation.Name;
                if (this.<>f__this.mSkillVars.mIsCollaboSkillSub == null)
                {
                    goto Label_0951;
                }
                this.<name>__15 = this.<name>__15 + "_sub";
            Label_0951:
                this.<>f__this.LoadUnitAnimationAsync("B_SKL", this.<name>__15, this.loadJobAnimation, this.<>f__this.mSkillVars.mIsCollaboSkill);
            Label_097D:
                if (string.IsNullOrEmpty(&this.<>f__this.mSkillVars.mSkillSequence.EndAnimation.Name) != null)
                {
                    goto Label_0A18;
                }
                this.<name>__16 = &this.<>f__this.mSkillVars.mSkillSequence.EndAnimation.Name;
                if (this.<>f__this.mSkillVars.mIsCollaboSkillSub == null)
                {
                    goto Label_09EC;
                }
                this.<name>__16 = this.<name>__16 + "_sub";
            Label_09EC:
                this.<>f__this.LoadUnitAnimationAsync("B_BS", this.<name>__16, this.loadJobAnimation, this.<>f__this.mSkillVars.mIsCollaboSkill);
            Label_0A18:
                if (string.IsNullOrEmpty(this.<>f__this.mSkillVars.mSkillSequence.StartAnimation) != null)
                {
                    goto Label_0AC5;
                }
                this.<name>__17 = this.<>f__this.mSkillVars.mSkillSequence.StartAnimation;
                if (this.<>f__this.mSkillVars.mIsCollaboSkillSub == null)
                {
                    goto Label_0A7D;
                }
                this.<name>__17 = this.<name>__17 + "_sub";
            Label_0A7D:
                this.<>f__this.LoadUnitAnimationAsync("B_PRS", this.<name>__17, this.loadJobAnimation, this.<>f__this.mSkillVars.mIsCollaboSkill);
                goto Label_0AC5;
            Label_0AAE:
                this.$current = new WaitForEndOfFrame();
                this.$PC = 6;
                goto Label_0ECA;
            Label_0AC5:
                if (this.<>f__this.LoadingAnimationCount > 0)
                {
                    goto Label_0AAE;
                }
                this.<>f__this.mSkillVars.mSkillChantAnimation = this.<>f__this.FindAnimation("B_CHA");
                this.<>f__this.mSkillVars.mSkillAnimation = this.<>f__this.FindAnimation("B_SKL");
                this.<>f__this.mSkillVars.mSkillEndAnimation = this.<>f__this.FindAnimation("B_BS");
                this.<>f__this.mSkillVars.mSkillStartAnimation = this.<>f__this.FindAnimation("B_PRS");
                if ((this.<>f__this.mSkillVars.mSkillChantAnimation != null) == null)
                {
                    goto Label_0BA0;
                }
                if (this.<>f__this.mSkillVars.mSkillChantAnimation.IsValid != null)
                {
                    goto Label_0BA0;
                }
                this.<>f__this.SetLoadError();
                Debug.LogError("Invalid Chant Animation");
            Label_0BA0:
                if ((this.<>f__this.mSkillVars.mSkillAnimation != null) == null)
                {
                    goto Label_0BEA;
                }
                if (this.<>f__this.mSkillVars.mSkillAnimation.IsValid != null)
                {
                    goto Label_0BEA;
                }
                this.<>f__this.SetLoadError();
                Debug.LogError("Invalid Skill Animation");
            Label_0BEA:
                if ((this.<>f__this.mSkillVars.mSkillEndAnimation != null) == null)
                {
                    goto Label_0C34;
                }
                if (this.<>f__this.mSkillVars.mSkillEndAnimation.IsValid != null)
                {
                    goto Label_0C34;
                }
                this.<>f__this.SetLoadError();
                Debug.LogError("Invalid End Animation");
            Label_0C34:
                if ((this.<>f__this.mSkillVars.mSkillStartAnimation != null) == null)
                {
                    goto Label_0C7E;
                }
                if (this.<>f__this.mSkillVars.mSkillStartAnimation.IsValid != null)
                {
                    goto Label_0C7E;
                }
                this.<>f__this.SetLoadError();
                Debug.LogError("Invalid Start Animation");
            Label_0C7E:
                if (&this.<>f__this.mSkillVars.mSkillSequence.ChantAnimation.UseCamera == null)
                {
                    goto Label_0CE2;
                }
                if ((this.<>f__this.mSkillVars.mSkillChantAnimation != null) == null)
                {
                    goto Label_0CE2;
                }
                this.<>f__this.mSkillVars.mSkillChantCameraClip = this.<>f__this.mSkillVars.mSkillChantAnimation.animation;
                goto Label_0D35;
            Label_0CE2:
                if (string.IsNullOrEmpty(this.<>f__this.mSkillVars.mSkillSequence.ChantCameraClipName) != null)
                {
                    goto Label_0D35;
                }
                this.<>f__this.mSkillVars.mSkillChantCameraClip = AssetManager.Load<AnimationClip>(TacticsUnitController.CameraAnimationDir + this.<>f__this.mSkillVars.mSkillSequence.ChantCameraClipName);
            Label_0D35:
                if (&this.<>f__this.mSkillVars.mSkillSequence.SkillAnimation.UseCamera == null)
                {
                    goto Label_0D99;
                }
                if ((this.<>f__this.mSkillVars.mSkillAnimation != null) == null)
                {
                    goto Label_0D99;
                }
                this.<>f__this.mSkillVars.mSkillCameraClip = this.<>f__this.mSkillVars.mSkillAnimation.animation;
                goto Label_0DEC;
            Label_0D99:
                if (string.IsNullOrEmpty(this.<>f__this.mSkillVars.mSkillSequence.MainCameraClipName) != null)
                {
                    goto Label_0DEC;
                }
                this.<>f__this.mSkillVars.mSkillCameraClip = AssetManager.Load<AnimationClip>(TacticsUnitController.CameraAnimationDir + this.<>f__this.mSkillVars.mSkillSequence.MainCameraClipName);
            Label_0DEC:
                if (&this.<>f__this.mSkillVars.mSkillSequence.EndAnimation.UseCamera == null)
                {
                    goto Label_0E50;
                }
                if ((this.<>f__this.mSkillVars.mSkillEndAnimation != null) == null)
                {
                    goto Label_0E50;
                }
                this.<>f__this.mSkillVars.mSkillEndCameraClip = this.<>f__this.mSkillVars.mSkillEndAnimation.animation;
                goto Label_0EA3;
            Label_0E50:
                if (string.IsNullOrEmpty(this.<>f__this.mSkillVars.mSkillSequence.EndCameraClipName) != null)
                {
                    goto Label_0EA3;
                }
                this.<>f__this.mSkillVars.mSkillEndCameraClip = AssetManager.Load<AnimationClip>(TacticsUnitController.CameraAnimationDir + this.<>f__this.mSkillVars.mSkillSequence.EndCameraClipName);
            Label_0EA3:
                this.<>f__this.RemoveLoadThreadCount();
                this.$current = null;
                this.$PC = 7;
                goto Label_0ECA;
            Label_0EC1:
                this.$PC = -1;
            Label_0EC8:
                return 0;
            Label_0ECA:
                return 1;
                return flag;
            }

            [DebuggerHidden]
            public void Reset()
            {
                throw new NotSupportedException();
            }

            object IEnumerator<object>.Current
            {
                [DebuggerHidden]
                get
                {
                    return this.$current;
                }
            }

            object IEnumerator.Current
            {
                [DebuggerHidden]
                get
                {
                    return this.$current;
                }
            }
        }

        [CompilerGenerated]
        private sealed class <ShowHealPopup>c__Iterator53 : IEnumerator, IDisposable, IEnumerator<object>
        {
            internal int hpHeal;
            internal TacticsUnitController target;
            internal Vector3 <tpos>__0;
            internal int mpHeal;
            internal Vector3 <tpos>__1;
            internal int $PC;
            internal object $current;
            internal int <$>hpHeal;
            internal TacticsUnitController <$>target;
            internal int <$>mpHeal;

            public <ShowHealPopup>c__Iterator53()
            {
                base..ctor();
                return;
            }

            [DebuggerHidden]
            public void Dispose()
            {
                this.$PC = -1;
                return;
            }

            public bool MoveNext()
            {
                uint num;
                bool flag;
                num = this.$PC;
                this.$PC = -1;
                switch (num)
                {
                    case 0:
                        goto Label_0029;

                    case 1:
                        goto Label_004C;

                    case 2:
                        goto Label_00A0;

                    case 3:
                        goto Label_00C3;
                }
                goto Label_00F2;
            Label_0029:
                if (this.hpHeal < 1)
                {
                    goto Label_00A0;
                }
                this.$current = new WaitForEndOfFrame();
                this.$PC = 1;
                goto Label_00F4;
            Label_004C:
                this.<tpos>__0 = this.target.CenterPosition;
                SceneBattle.Instance.PopupHpHealNumber(this.<tpos>__0, this.hpHeal);
                this.target.ReflectDispModel();
                this.$current = new WaitForSeconds(GameSettings.Instance.HitPopup_PopDeley);
                this.$PC = 2;
                goto Label_00F4;
            Label_00A0:
                if (this.mpHeal < 1)
                {
                    goto Label_00EB;
                }
                this.$current = new WaitForEndOfFrame();
                this.$PC = 3;
                goto Label_00F4;
            Label_00C3:
                this.<tpos>__1 = this.target.CenterPosition;
                SceneBattle.Instance.PopupMpHealNumber(this.<tpos>__1, this.mpHeal);
            Label_00EB:
                this.$PC = -1;
            Label_00F2:
                return 0;
            Label_00F4:
                return 1;
                return flag;
            }

            [DebuggerHidden]
            public void Reset()
            {
                throw new NotSupportedException();
            }

            object IEnumerator<object>.Current
            {
                [DebuggerHidden]
                get
                {
                    return this.$current;
                }
            }

            object IEnumerator.Current
            {
                [DebuggerHidden]
                get
                {
                    return this.$current;
                }
            }
        }

        [CompilerGenerated]
        private sealed class <ShowHitPopup>c__Iterator52 : IEnumerator, IDisposable, IEnumerator<object>
        {
            internal bool guard;
            internal TacticsUnitController target;
            internal int <countMax>__0;
            internal GameSettings <gs>__1;
            internal float <yOffset>__2;
            internal SceneBattle <battle>__3;
            internal float <spacing>__4;
            internal bool critical;
            internal bool backstab;
            internal bool weak;
            internal bool resist;
            internal bool absorb;
            internal bool is_guts;
            internal bool hpDamage;
            internal Vector3 <tpos>__5;
            internal int hpDamageValue;
            internal bool mpDamage;
            internal Vector3 <tpos>__6;
            internal int mpDamageValue;
            internal int $PC;
            internal object $current;
            internal bool <$>guard;
            internal TacticsUnitController <$>target;
            internal bool <$>critical;
            internal bool <$>backstab;
            internal bool <$>weak;
            internal bool <$>resist;
            internal bool <$>absorb;
            internal bool <$>is_guts;
            internal bool <$>hpDamage;
            internal int <$>hpDamageValue;
            internal bool <$>mpDamage;
            internal int <$>mpDamageValue;
            internal TacticsUnitController <>f__this;

            public <ShowHitPopup>c__Iterator52()
            {
                base..ctor();
                return;
            }

            [DebuggerHidden]
            public void Dispose()
            {
                this.$PC = -1;
                return;
            }

            public bool MoveNext()
            {
                uint num;
                bool flag;
                num = this.$PC;
                this.$PC = -1;
                switch (num)
                {
                    case 0:
                        goto Label_0065;

                    case 1:
                        goto Label_02E4;

                    case 2:
                        goto Label_033A;

                    case 3:
                        goto Label_036C;

                    case 4:
                        goto Label_03C2;

                    case 5:
                        goto Label_03F4;

                    case 6:
                        goto Label_044A;

                    case 7:
                        goto Label_047C;

                    case 8:
                        goto Label_04D2;

                    case 9:
                        goto Label_0505;

                    case 10:
                        goto Label_055C;

                    case 11:
                        goto Label_058F;

                    case 12:
                        goto Label_05E6;

                    case 13:
                        goto Label_0619;

                    case 14:
                        goto Label_0670;

                    case 15:
                        goto Label_0693;

                    case 0x10:
                        goto Label_0759;

                    case 0x11:
                        goto Label_077C;

                    case 0x12:
                        goto Label_07BD;
                }
                goto Label_07C4;
            Label_0065:
                if (this.guard == null)
                {
                    goto Label_010D;
                }
                if (this.target.mHitInfo == null)
                {
                    goto Label_010D;
                }
                this.<countMax>__0 = 0;
                if (this.target.mHitInfo.defSkill.SkillParam.count <= 0)
                {
                    goto Label_00DC;
                }
                this.<countMax>__0 = this.target.mHitInfo.target.GetSkillUseCountMax(this.target.mHitInfo.defSkill);
            Label_00DC:
                this.target.SpawnDefendHitEffect(this.target.mHitInfo.defSkill, this.target.mHitInfo.defSkillUseCount, this.<countMax>__0);
            Label_010D:
                this.<gs>__1 = GameSettings.Instance;
                this.<yOffset>__2 = 0f;
                this.<battle>__3 = SceneBattle.Instance;
                if (this.<gs>__1.HitPopup_YSpacing >= 0f)
                {
                    goto Label_02A0;
                }
                this.<spacing>__4 = Mathf.Abs(this.<gs>__1.HitPopup_YSpacing);
                if (this.critical == null)
                {
                    goto Label_0187;
                }
                if (this.<battle>__3.HasCriticalPopup == null)
                {
                    goto Label_0187;
                }
                this.<yOffset>__2 += this.<spacing>__4;
            Label_0187:
                if (this.backstab == null)
                {
                    goto Label_01B5;
                }
                if (this.<battle>__3.HasBackstabPopup == null)
                {
                    goto Label_01B5;
                }
                this.<yOffset>__2 += this.<spacing>__4;
            Label_01B5:
                if (this.weak == null)
                {
                    goto Label_01E3;
                }
                if (this.<battle>__3.HasWeakPopup == null)
                {
                    goto Label_01E3;
                }
                this.<yOffset>__2 += this.<spacing>__4;
            Label_01E3:
                if (this.resist == null)
                {
                    goto Label_0211;
                }
                if (this.<battle>__3.HasResistPopup == null)
                {
                    goto Label_0211;
                }
                this.<yOffset>__2 += this.<spacing>__4;
            Label_0211:
                if (this.guard == null)
                {
                    goto Label_023F;
                }
                if (this.<battle>__3.HasGuardPopup == null)
                {
                    goto Label_023F;
                }
                this.<yOffset>__2 += this.<spacing>__4;
            Label_023F:
                if (this.absorb == null)
                {
                    goto Label_026D;
                }
                if (this.<battle>__3.HasAbsorbPopup == null)
                {
                    goto Label_026D;
                }
                this.<yOffset>__2 += this.<spacing>__4;
            Label_026D:
                if (this.is_guts == null)
                {
                    goto Label_02B2;
                }
                if (this.<battle>__3.HasGutsPopup == null)
                {
                    goto Label_02B2;
                }
                this.<yOffset>__2 += this.<spacing>__4;
                goto Label_02B2;
            Label_02A0:
                this.<yOffset>__2 = -this.<gs>__1.HitPopup_YSpacing;
            Label_02B2:
                if (this.critical == null)
                {
                    goto Label_033A;
                }
                if (this.<battle>__3.HasCriticalPopup == null)
                {
                    goto Label_033A;
                }
                this.$current = new WaitForEndOfFrame();
                this.$PC = 1;
                goto Label_07C6;
            Label_02E4:
                this.<yOffset>__2 += this.<gs>__1.HitPopup_YSpacing;
                this.<>f__this.ShowCriticalEffect(this.target.CenterPosition, this.<yOffset>__2);
                this.$current = new WaitForSeconds(this.<gs>__1.HitPopup_PopDeley);
                this.$PC = 2;
                goto Label_07C6;
            Label_033A:
                if (this.backstab == null)
                {
                    goto Label_03C2;
                }
                if (this.<battle>__3.HasBackstabPopup == null)
                {
                    goto Label_03C2;
                }
                this.$current = new WaitForEndOfFrame();
                this.$PC = 3;
                goto Label_07C6;
            Label_036C:
                this.<yOffset>__2 += this.<gs>__1.HitPopup_YSpacing;
                this.<battle>__3.PopupBackstab(this.target.CenterPosition, this.<yOffset>__2);
                this.$current = new WaitForSeconds(this.<gs>__1.HitPopup_PopDeley);
                this.$PC = 4;
                goto Label_07C6;
            Label_03C2:
                if (this.weak == null)
                {
                    goto Label_044A;
                }
                if (this.<battle>__3.HasWeakPopup == null)
                {
                    goto Label_044A;
                }
                this.$current = new WaitForEndOfFrame();
                this.$PC = 5;
                goto Label_07C6;
            Label_03F4:
                this.<yOffset>__2 += this.<gs>__1.HitPopup_YSpacing;
                this.<battle>__3.PopupWeak(this.target.CenterPosition, this.<yOffset>__2);
                this.$current = new WaitForSeconds(this.<gs>__1.HitPopup_PopDeley);
                this.$PC = 6;
                goto Label_07C6;
            Label_044A:
                if (this.resist == null)
                {
                    goto Label_04D2;
                }
                if (this.<battle>__3.HasResistPopup == null)
                {
                    goto Label_04D2;
                }
                this.$current = new WaitForEndOfFrame();
                this.$PC = 7;
                goto Label_07C6;
            Label_047C:
                this.<yOffset>__2 += this.<gs>__1.HitPopup_YSpacing;
                this.<battle>__3.PopupResist(this.target.CenterPosition, this.<yOffset>__2);
                this.$current = new WaitForSeconds(this.<gs>__1.HitPopup_PopDeley);
                this.$PC = 8;
                goto Label_07C6;
            Label_04D2:
                if (this.guard == null)
                {
                    goto Label_055C;
                }
                if (this.<battle>__3.HasGuardPopup == null)
                {
                    goto Label_055C;
                }
                this.$current = new WaitForEndOfFrame();
                this.$PC = 9;
                goto Label_07C6;
            Label_0505:
                this.<yOffset>__2 += this.<gs>__1.HitPopup_YSpacing;
                this.<battle>__3.PopupGuard(this.target.CenterPosition, this.<yOffset>__2);
                this.$current = new WaitForSeconds(this.<gs>__1.HitPopup_PopDeley);
                this.$PC = 10;
                goto Label_07C6;
            Label_055C:
                if (this.absorb == null)
                {
                    goto Label_05E6;
                }
                if (this.<battle>__3.HasAbsorbPopup == null)
                {
                    goto Label_05E6;
                }
                this.$current = new WaitForEndOfFrame();
                this.$PC = 11;
                goto Label_07C6;
            Label_058F:
                this.<yOffset>__2 += this.<gs>__1.HitPopup_YSpacing;
                this.<battle>__3.PopupAbsorb(this.target.CenterPosition, this.<yOffset>__2);
                this.$current = new WaitForSeconds(this.<gs>__1.HitPopup_PopDeley);
                this.$PC = 12;
                goto Label_07C6;
            Label_05E6:
                if (this.is_guts == null)
                {
                    goto Label_0670;
                }
                if (this.<battle>__3.HasGutsPopup == null)
                {
                    goto Label_0670;
                }
                this.$current = new WaitForEndOfFrame();
                this.$PC = 13;
                goto Label_07C6;
            Label_0619:
                this.<yOffset>__2 += this.<gs>__1.HitPopup_YSpacing;
                this.<battle>__3.PopupGuts(this.target.CenterPosition, this.<yOffset>__2);
                this.$current = new WaitForSeconds(this.<gs>__1.HitPopup_PopDeley);
                this.$PC = 14;
                goto Label_07C6;
            Label_0670:
                if (this.hpDamage == null)
                {
                    goto Label_0759;
                }
                this.$current = new WaitForEndOfFrame();
                this.$PC = 15;
                goto Label_07C6;
            Label_0693:
                this.<tpos>__5 = this.target.CenterPosition;
                if (this.<>f__this.mSkillVars == null)
                {
                    goto Label_0714;
                }
                if (this.<>f__this.mSkillVars.Skill == null)
                {
                    goto Label_0714;
                }
                if (this.<>f__this.mSkillVars.Skill.DamageDispType == null)
                {
                    goto Label_0714;
                }
                SceneBattle.Instance.PopupDamageDsgNumber(this.<tpos>__5, this.hpDamageValue, this.<>f__this.mSkillVars.Skill.DamageDispType);
                goto Label_072B;
            Label_0714:
                SceneBattle.Instance.PopupDamageNumber(this.<tpos>__5, this.hpDamageValue);
            Label_072B:
                this.target.ReflectDispModel();
                this.$current = new WaitForSeconds(this.<gs>__1.HitPopup_PopDeley);
                this.$PC = 0x10;
                goto Label_07C6;
            Label_0759:
                if (this.mpDamage == null)
                {
                    goto Label_07A4;
                }
                this.$current = new WaitForEndOfFrame();
                this.$PC = 0x11;
                goto Label_07C6;
            Label_077C:
                this.<tpos>__6 = this.target.CenterPosition;
                SceneBattle.Instance.PopupDamageNumber(this.<tpos>__6, this.mpDamageValue);
            Label_07A4:
                this.$current = (int) 0;
                this.$PC = 0x12;
                goto Label_07C6;
            Label_07BD:
                this.$PC = -1;
            Label_07C4:
                return 0;
            Label_07C6:
                return 1;
                return flag;
            }

            [DebuggerHidden]
            public void Reset()
            {
                throw new NotSupportedException();
            }

            object IEnumerator<object>.Current
            {
                [DebuggerHidden]
                get
                {
                    return this.$current;
                }
            }

            object IEnumerator.Current
            {
                [DebuggerHidden]
                get
                {
                    return this.$current;
                }
            }
        }

        [CompilerGenerated]
        private sealed class <UpdateShields>c__AnonStorey1F4
        {
            internal Unit.UnitShield unit_shield;

            public <UpdateShields>c__AnonStorey1F4()
            {
                base..ctor();
                return;
            }

            internal bool <>m__CD(TacticsUnitController.ShieldState sh)
            {
                return (sh.Target == this.unit_shield);
            }
        }

        [StructLayout(LayoutKind.Sequential)]
        private struct CameraState
        {
            public Vector3 Position;
            public Quaternion Rotation;
            public static TacticsUnitController.CameraState Default;
            public void CacheCurrent(Camera camera)
            {
                Transform transform;
                transform = camera.get_transform();
                this.Position = transform.get_position();
                this.Rotation = transform.get_rotation();
                return;
            }
        }

        private enum ColorBlendModes
        {
            None,
            Fade,
            Blink
        }

        [Flags]
        public enum DeathAnimationTypes
        {
            Normal = 1
        }

        public enum EElementEffectTypes
        {
            NotEffective,
            Normal,
            Effective
        }

        private enum eKnockBackMode
        {
            IDLE,
            START,
            EXEC
        }

        private delegate void FieldActionEndEvent();

        private class HideGimmickAnimation
        {
            private Vector3 mBaseScale;
            private TacticsUnitController mGimmickController;
            public bool IsHide;
            public bool Enable;
            private float mWait;
            private float mCurrentWait;
            private float mSpeed;

            public HideGimmickAnimation()
            {
                this.mWait = 1f;
                this.mSpeed = 5f;
                base..ctor();
                return;
            }

            public void Init(TacticsUnitController GimmickController)
            {
                if ((GimmickController == null) != null)
                {
                    goto Label_0037;
                }
                if (GimmickController.Unit == null)
                {
                    goto Label_0037;
                }
                if (GimmickController.Unit.IsGimmick == null)
                {
                    goto Label_0037;
                }
                if (GimmickController.mUnit.IsBreakObj == null)
                {
                    goto Label_0038;
                }
            Label_0037:
                return;
            Label_0038:
                this.mGimmickController = GimmickController;
                this.mBaseScale = this.mGimmickController.get_transform().get_localScale();
                this.mCurrentWait = this.mWait;
                this.Enable = 0;
                this.IsHide = 1;
                return;
            }

            public void ResetScale()
            {
                this.mCurrentWait = this.mWait;
                this.Enable = 0;
                this.IsHide = 1;
                this.mGimmickController.get_transform().set_localScale(this.mBaseScale);
                return;
            }

            public void Update()
            {
                if (this.mGimmickController != null)
                {
                    goto Label_0011;
                }
                return;
            Label_0011:
                if (this.Enable != null)
                {
                    goto Label_001D;
                }
                return;
            Label_001D:
                if (this.IsHide == null)
                {
                    goto Label_009F;
                }
                if (0f >= this.mCurrentWait)
                {
                    goto Label_007A;
                }
                this.mCurrentWait -= this.mSpeed * Time.get_deltaTime();
                this.mGimmickController.get_transform().set_localScale(this.mBaseScale * (this.mCurrentWait / this.mWait));
                return;
            Label_007A:
                this.mGimmickController.get_transform().set_localScale(Vector3.get_zero());
                this.mCurrentWait = 0f;
                goto Label_0114;
            Label_009F:
                if (this.mWait <= this.mCurrentWait)
                {
                    goto Label_00F2;
                }
                this.mCurrentWait += this.mSpeed * Time.get_deltaTime();
                this.mGimmickController.get_transform().set_localScale(this.mBaseScale * (this.mCurrentWait / this.mWait));
                return;
            Label_00F2:
                this.mGimmickController.get_transform().set_localScale(this.mBaseScale);
                this.mCurrentWait = this.mWait;
            Label_0114:
                this.Enable = 0;
                return;
            }
        }

        [StructLayout(LayoutKind.Sequential)]
        private struct HitTimer
        {
            public TacticsUnitController Target;
            public float HitTime;
            public HitTimer(TacticsUnitController target, float hitTime)
            {
                this.Target = target;
                this.HitTime = hitTime;
                return;
            }
        }

        public enum HPGaugeModes
        {
            Normal,
            Attack,
            Target,
            Change
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct OutputPoint
        {
            public int Value;
            public SRPG.TacticsUnitController.PointType PointType;
            public bool Critical;
        }

        public enum PointType
        {
            Damage,
            Heal
        }

        public enum PostureTypes
        {
            Combat,
            NonCombat
        }

        public class ProjectileData
        {
            public GameObject mProjectile;
            public Coroutine mProjectileThread;
            public bool mProjectileHitsTarget;
            public bool mProjStartAnimEnded;
            public bool mIsHitOnly;
            public bool mIsNotSpawnLandingEffect;

            public ProjectileData()
            {
                base..ctor();
                return;
            }
        }

        private delegate void ProjectileStopEvent(TacticsUnitController.ProjectileData pd);

        private class ShakeUnit
        {
            private Vector3 mShakeBasePos;
            private Vector3 mShakeOffset;
            private float mShakeSpeed;
            private int mShakeMaxCount;
            private int mShakeCount;
            private bool mIsShakeStart;

            public ShakeUnit()
            {
                this.mShakeSpeed = 0.0125f;
                this.mShakeMaxCount = 8;
                this.mShakeCount = 8;
                base..ctor();
                return;
            }

            public Vector3 AdvanceShake()
            {
                if (this.mIsShakeStart != null)
                {
                    goto Label_0012;
                }
                return this.mShakeBasePos;
            Label_0012:
                if (0 >= this.mShakeCount)
                {
                    goto Label_005C;
                }
                if ((this.mShakeCount % 2) != null)
                {
                    goto Label_003C;
                }
                this.mShakeOffset = -this.mShakeOffset;
            Label_003C:
                this.mShakeCount -= 1;
                return (this.mShakeBasePos + this.mShakeOffset);
            Label_005C:
                this.mIsShakeStart = 0;
                return this.mShakeBasePos;
            }

            public unsafe void Init(Vector3 basePosition, Vector3 direction)
            {
                GameSettings settings;
                Vector3 vector;
                settings = GameSettings.Instance;
                this.mShakeSpeed = settings.ShakeUnit_Offset;
                this.mShakeMaxCount = settings.ShakeUnit_MaxCount;
                this.mShakeBasePos = basePosition;
                this.mShakeOffset = &Vector3.Cross(direction, new Vector3(0f, 1f, 0f)).get_normalized();
                this.mShakeOffset *= this.mShakeSpeed;
                this.mShakeCount = this.mShakeMaxCount;
                this.mIsShakeStart = 0;
                return;
            }

            public bool ShakeStart
            {
                get
                {
                    return this.mIsShakeStart;
                }
                set
                {
                    this.mIsShakeStart = value;
                    return;
                }
            }

            public int ShakeMaxCount
            {
                get
                {
                    return this.mShakeMaxCount;
                }
                set
                {
                }
            }

            public int ShakeCount
            {
                get
                {
                    return this.mShakeCount;
                }
                set
                {
                }
            }
        }

        public class ShieldState
        {
            public Unit.UnitShield Target;
            public int LastHP;
            public int LastTurn;
            public bool Dirty;

            public ShieldState()
            {
                base..ctor();
                return;
            }

            public void ClearDirty()
            {
                this.LastHP = this.Target.hp;
                this.LastTurn = this.Target.turn;
                this.Target.is_efficacy = 0;
                this.Dirty = 0;
                return;
            }
        }

        private class SkillVars
        {
            private int mDefaultLayer;
            public bool SuppressDamageOutput;
            public int HpCostDamage;
            public SkillParam Skill;
            public bool mIsCollaboSkill;
            public bool mIsCollaboSkillSub;
            public List<TacticsUnitController> Targets;
            public Vector3 mStartPosition;
            public Vector3 mTargetPosition;
            public Vector3 mTargetControllerPosition;
            public TacticsUnitController mTargetController;
            public bool mAuraEnable;
            public List<GameObject> mAuras;
            public Camera mActiveCamera;
            public StatusParam Param;
            public int mCameraID;
            public int mChantCameraID;
            public int mSkillCameraID;
            public Quaternion mCameraShakeOffset;
            public float mCameraShakeSeedX;
            public float mCameraShakeSeedY;
            public int MaxPlayVoice;
            public int NumPlayVoice;
            public int TotalHits;
            public int NumHitsLeft;
            public SkillSequence mSkillSequence;
            public AnimDef mSkillChantAnimation;
            public AnimDef mSkillAnimation;
            public AnimDef mSkillEndAnimation;
            public AnimDef mSkillStartAnimation;
            public AnimationClip mSkillChantCameraClip;
            public AnimationClip mSkillCameraClip;
            public AnimationClip mSkillEndCameraClip;
            public GameObject mChantEffect;
            public List<TacticsUnitController.ProjectileData> mProjectileDataLists;
            public int mNumShotCount;
            public bool mProjectileTriggered;
            public SkillEffect mSkillEffect;
            public List<TacticsUnitController.OutputPoint> mOutputPoints;
            public bool UseBattleScene;
            public Vector3[] HitGrids;
            public List<TacticsUnitController.HitTimer> HitTimers;
            public Coroutine HitTimerThread;
            public List<TacticsUnitController> HitTargets;
            public Grid mLandingGrid;
            public Grid mTeleportGrid;
            public bool mIsFinishedBuffEffectTarget;
            public bool mIsFinishedBuffEffectSelf;

            public SkillVars()
            {
                this.mAuras = new List<GameObject>(4);
                this.mCameraShakeOffset = Quaternion.get_identity();
                this.mProjectileDataLists = new List<TacticsUnitController.ProjectileData>();
                this.mOutputPoints = new List<TacticsUnitController.OutputPoint>(0x20);
                this.HitTimers = new List<TacticsUnitController.HitTimer>();
                this.HitTargets = new List<TacticsUnitController>(4);
                base..ctor();
                return;
            }
        }

        private class State : State<TacticsUnitController>
        {
            public State()
            {
                base..ctor();
                return;
            }
        }

        private class State_AerialDamage : TacticsUnitController.State
        {
            public State_AerialDamage()
            {
                base..ctor();
                return;
            }

            public override void Begin(TacticsUnitController self)
            {
                self.PlayAnimation("B_DMG1", 0, 0.1f, 0f);
                return;
            }
        }

        private class State_AfterTransform : TacticsUnitController.State
        {
            public State_AfterTransform()
            {
                base..ctor();
                return;
            }

            public override void Begin(TacticsUnitController self)
            {
                self.PlayAnimation("B_TRANSFORM", 0);
                return;
            }

            public override void Update(TacticsUnitController self)
            {
                if (self.GetRemainingTime("B_TRANSFORM") > 0.1f)
                {
                    goto Label_0020;
                }
                self.PlayIdle(0.1f);
            Label_0020:
                return;
            }
        }

        private class State_ChangeGrid : TacticsUnitController.State
        {
            private bool mChanged;
            private bool mStartEffect;
            private GameObject mSelfEffect;
            private GameObject mTargetEffect;
            private ParticleSystem mSelfParticle;

            public State_ChangeGrid()
            {
                base..ctor();
                return;
            }

            public override void Begin(TacticsUnitController self)
            {
                TacticsUnitController controller;
                self.PlayAnimation("B_SKL", 0);
                controller = self.mSkillVars.Targets[0];
                controller.AutoUpdateRotation = 0;
                controller.LookAt(self.CenterPosition);
                return;
            }

            private void ChangePosition()
            {
            }

            private GameObject CreateEffect(GameObject EffectPrefab, TacticsUnitController Parent)
            {
                GameObject obj2;
                obj2 = Object.Instantiate(EffectPrefab, Parent.get_transform().get_position(), Parent.get_transform().get_rotation()) as GameObject;
                if ((obj2 != null) == null)
                {
                    goto Label_0046;
                }
                SRPG_Extensions.RequireComponent<OneShotParticle>(obj2);
                obj2.get_transform().SetParent(Parent.get_transform());
            Label_0046:
                return obj2;
            }

            [DebuggerHidden]
            private IEnumerator EffectEndWait(float Seconds, TacticsUnitController self)
            {
                <EffectEndWait>c__Iterator59 iterator;
                iterator = new <EffectEndWait>c__Iterator59();
                iterator.Seconds = Seconds;
                iterator.self = self;
                iterator.<$>Seconds = Seconds;
                iterator.<$>self = self;
                return iterator;
            }

            public override void End(TacticsUnitController self)
            {
            }

            public override unsafe void Update(TacticsUnitController self)
            {
                float num;
                TacticsUnitController controller;
                Vector3 vector;
                SceneBattle battle;
                GameObject obj2;
                Vector3 vector2;
                Vector3 vector3;
                if (this.mStartEffect != null)
                {
                    goto Label_003B;
                }
                if (self.GetNormalizedTime("B_SKL") >= 1f)
                {
                    goto Label_0023;
                }
                return;
            Label_0023:
                self.StartCoroutine(this.Wait(0.2f, self));
                goto Label_0190;
            Label_003B:
                if ((this.mSelfEffect == null) == null)
                {
                    goto Label_005D;
                }
                if ((this.mTargetEffect == null) == null)
                {
                    goto Label_005D;
                }
            Label_005D:
                if ((this.mSelfParticle != null) == null)
                {
                    goto Label_0190;
                }
                if ((this.mSelfParticle.get_time() / this.mSelfParticle.get_duration()) < 1f)
                {
                    goto Label_0190;
                }
                if (this.mChanged != null)
                {
                    goto Label_0190;
                }
                this.mChanged = 1;
                controller = self.mSkillVars.Targets[0];
                vector = self.get_transform().get_position();
                self.get_transform().set_position(controller.get_transform().get_position());
                controller.get_transform().set_position(vector);
                if (controller.IsVisible() != null)
                {
                    goto Label_0167;
                }
                battle = SceneBattle.Instance;
                if (battle == null)
                {
                    goto Label_0167;
                }
                obj2 = battle.GetJumpSpotEffect(controller.Unit);
                if (obj2 == null)
                {
                    goto Label_0161;
                }
                obj2.get_transform().set_position(controller.get_transform().get_position());
                vector2 = obj2.get_transform().get_position();
                &vector2.y = &GameUtility.RaycastGround(vector2).y;
                obj2.get_transform().set_position(vector2);
            Label_0161:
                battle.OnGimmickUpdate();
            Label_0167:
                self.mSkillVars.mStartPosition = self.get_transform().get_position();
                self.StartCoroutine(this.EffectEndWait(0.2f, self));
            Label_0190:
                return;
            }

            [DebuggerHidden]
            private IEnumerator Wait(float Seconds, TacticsUnitController self)
            {
                <Wait>c__Iterator58 iterator;
                iterator = new <Wait>c__Iterator58();
                iterator.self = self;
                iterator.Seconds = Seconds;
                iterator.<$>self = self;
                iterator.<$>Seconds = Seconds;
                iterator.<>f__this = this;
                return iterator;
            }

            [CompilerGenerated]
            private sealed class <EffectEndWait>c__Iterator59 : IEnumerator, IDisposable, IEnumerator<object>
            {
                internal float Seconds;
                internal TacticsUnitController self;
                internal int $PC;
                internal object $current;
                internal float <$>Seconds;
                internal TacticsUnitController <$>self;

                public <EffectEndWait>c__Iterator59()
                {
                    base..ctor();
                    return;
                }

                [DebuggerHidden]
                public void Dispose()
                {
                    this.$PC = -1;
                    return;
                }

                public bool MoveNext()
                {
                    uint num;
                    bool flag;
                    num = this.$PC;
                    this.$PC = -1;
                    switch (num)
                    {
                        case 0:
                            goto Label_0021;

                        case 1:
                            goto Label_003E;
                    }
                    goto Label_0050;
                Label_0021:
                    this.$current = new WaitForSeconds(this.Seconds);
                    this.$PC = 1;
                    goto Label_0052;
                Label_003E:
                    this.self.GotoState<TacticsUnitController.State_SkillEnd>();
                    this.$PC = -1;
                Label_0050:
                    return 0;
                Label_0052:
                    return 1;
                    return flag;
                }

                [DebuggerHidden]
                public void Reset()
                {
                    throw new NotSupportedException();
                }

                object IEnumerator<object>.Current
                {
                    [DebuggerHidden]
                    get
                    {
                        return this.$current;
                    }
                }

                object IEnumerator.Current
                {
                    [DebuggerHidden]
                    get
                    {
                        return this.$current;
                    }
                }
            }

            [CompilerGenerated]
            private sealed class <Wait>c__Iterator58 : IEnumerator, IDisposable, IEnumerator<object>
            {
                internal TacticsUnitController self;
                internal TacticsUnitController <target>__0;
                internal ParticleSystem[] <particles>__1;
                internal int <i>__2;
                internal float Seconds;
                internal int $PC;
                internal object $current;
                internal TacticsUnitController <$>self;
                internal float <$>Seconds;
                internal TacticsUnitController.State_ChangeGrid <>f__this;

                public <Wait>c__Iterator58()
                {
                    base..ctor();
                    return;
                }

                [DebuggerHidden]
                public void Dispose()
                {
                    this.$PC = -1;
                    return;
                }

                public bool MoveNext()
                {
                    uint num;
                    bool flag;
                    num = this.$PC;
                    this.$PC = -1;
                    switch (num)
                    {
                        case 0:
                            goto Label_0021;

                        case 1:
                            goto Label_01CD;
                    }
                    goto Label_0226;
                Label_0021:
                    this.<>f__this.mStartEffect = 1;
                    this.<target>__0 = this.self.mSkillVars.Targets[0];
                    if (this.self.mSkillVars == null)
                    {
                        goto Label_01B0;
                    }
                    if (this.self.mSkillVars.mSkillEffect.ExplosionEffects == null)
                    {
                        goto Label_01B0;
                    }
                    this.<>f__this.mSelfEffect = this.<>f__this.CreateEffect(this.self.mSkillVars.mSkillEffect.ExplosionEffects[0], this.self);
                    this.<particles>__1 = this.<>f__this.mSelfEffect.GetComponentsInChildren<ParticleSystem>();
                    this.<i>__2 = 0;
                    goto Label_0148;
                Label_00C8:
                    if ((this.<>f__this.mSelfParticle == null) == null)
                    {
                        goto Label_00FB;
                    }
                    this.<>f__this.mSelfParticle = this.<particles>__1[this.<i>__2];
                    goto Label_013A;
                Label_00FB:
                    if (this.<>f__this.mSelfParticle.get_duration() <= this.<particles>__1[this.<i>__2].get_duration())
                    {
                        goto Label_013A;
                    }
                    this.<>f__this.mSelfParticle = this.<particles>__1[this.<i>__2];
                Label_013A:
                    this.<i>__2 += 1;
                Label_0148:
                    if (this.<i>__2 < ((int) this.<particles>__1.Length))
                    {
                        goto Label_00C8;
                    }
                    this.<>f__this.mTargetEffect = this.<>f__this.CreateEffect(this.self.mSkillVars.mSkillEffect.ExplosionEffects[0], this.<target>__0);
                    this.<>f__this.mSelfEffect.SetActive(0);
                    this.<>f__this.mTargetEffect.SetActive(0);
                Label_01B0:
                    this.$current = new WaitForSeconds(this.Seconds);
                    this.$PC = 1;
                    goto Label_0228;
                Label_01CD:
                    this.<>f__this.mSelfEffect.SetActive(1);
                    this.<>f__this.mTargetEffect.SetActive(1);
                    if (this.self.Unit.IsBreakObj != null)
                    {
                        goto Label_021F;
                    }
                    this.self.PlayAnimation("IDLE", 1, 0.1f, 0f);
                Label_021F:
                    this.$PC = -1;
                Label_0226:
                    return 0;
                Label_0228:
                    return 1;
                    return flag;
                }

                [DebuggerHidden]
                public void Reset()
                {
                    throw new NotSupportedException();
                }

                object IEnumerator<object>.Current
                {
                    [DebuggerHidden]
                    get
                    {
                        return this.$current;
                    }
                }

                object IEnumerator.Current
                {
                    [DebuggerHidden]
                    get
                    {
                        return this.$current;
                    }
                }
            }
        }

        private class State_Dead : TacticsUnitController.State
        {
            public State_Dead()
            {
                base..ctor();
                return;
            }

            public override void Begin(TacticsUnitController self)
            {
                self.RootMotionMode = 2;
                self.PlayAnimation("B_DEAD", 0);
                if (self.mUnit == null)
                {
                    goto Label_002E;
                }
                if (self.mUnit.IsBreakObj != null)
                {
                    goto Label_0052;
                }
            Label_002E:
                self.Unit.PlayBattleVoice("battle_0028");
                MonoSingleton<MySound>.Instance.PlaySEOneShot("SE_6050", 0f);
            Label_0052:
                return;
            }

            public override void Update(TacticsUnitController self)
            {
                if (self.GetRemainingTime("B_DEAD") > 0f)
                {
                    goto Label_004F;
                }
                Object.Destroy(self.get_gameObject());
                if (self.mUnit == null)
                {
                    goto Label_003B;
                }
                if (self.mUnit.IsBreakObj != null)
                {
                    goto Label_004F;
                }
            Label_003B:
                MonoSingleton<MySound>.Instance.PlaySEOneShot("SE_6051", 0f);
            Label_004F:
                return;
            }
        }

        private class State_Defend : TacticsUnitController.State
        {
            public State_Defend()
            {
                base..ctor();
                return;
            }

            public override void Begin(TacticsUnitController self)
            {
                self.PlayAnimation("B_DEF", 0, 0.1f, 0f);
                return;
            }

            public override void Update(TacticsUnitController self)
            {
                if (self.GetRemainingTime("B_DEF") > 0.1f)
                {
                    goto Label_0020;
                }
                self.PlayIdle(0.1f);
            Label_0020:
                return;
            }
        }

        private class State_Dodge : TacticsUnitController.State
        {
            public State_Dodge()
            {
                base..ctor();
                return;
            }

            public override void Begin(TacticsUnitController self)
            {
                self.Unit.PlayBattleVoice("battle_0017");
                self.PlayAnimation("B_DGE", 0, 0.1f, 0f);
                return;
            }

            public override void Update(TacticsUnitController self)
            {
                if (self.GetRemainingTime("B_DGE") > 0.1f)
                {
                    goto Label_0020;
                }
                self.PlayIdle(0.1f);
            Label_0020:
                return;
            }
        }

        private class State_Down : TacticsUnitController.State
        {
            public State_Down()
            {
                base..ctor();
                return;
            }

            public override void Begin(TacticsUnitController self)
            {
                self.PlayAnimation("B_DOWN", 1);
                return;
            }

            public override void End(TacticsUnitController self)
            {
            }
        }

        private class State_FieldAction : TacticsUnitController.State
        {
            public State_FieldAction()
            {
                base..ctor();
                return;
            }
        }

        private class State_FieldActionClimpUp : TacticsUnitController.State_FieldAction
        {
            public State_FieldActionClimpUp()
            {
                base..ctor();
                return;
            }
        }

        private class State_FieldActionFall : TacticsUnitController.State_FieldAction
        {
            private Vector3 mStartPosition;
            private Vector3 mEndPosition;
            private float mTime;
            private float mDuration;

            public State_FieldActionFall()
            {
                this.mDuration = 0.5f;
                base..ctor();
                return;
            }

            public override unsafe void Begin(TacticsUnitController self)
            {
                GameSettings settings;
                this.mStartPosition = self.get_transform().get_position();
                this.mEndPosition = GameUtility.RaycastGround(self.mFieldActionPoint);
                settings = GameSettings.Instance;
                if ((settings != null) == null)
                {
                    goto Label_0064;
                }
                this.mDuration = settings.Unit_FallMinTime + (Mathf.Abs(&this.mStartPosition.y - &this.mEndPosition.y) * settings.Unit_FallTimePerHeight);
            Label_0064:
                self.get_transform().set_rotation(SRPG_Extensions.ToRotation(self.mFieldActionDir));
                self.mCollideGround = 0;
                self.PlayAnimation("FLLP", 1, 0.1f, 0f);
                return;
            }

            public override void End(TacticsUnitController self)
            {
                self.mCollideGround = 1;
                return;
            }

            public override unsafe void Update(TacticsUnitController self)
            {
                float num;
                Vector3 vector;
                this.mTime += Time.get_deltaTime();
                num = Mathf.Clamp01(this.mTime / this.mDuration);
                vector = Vector3.Lerp(this.mStartPosition, this.mEndPosition, num);
                &vector.y = Mathf.Lerp(&this.mStartPosition.y, &this.mEndPosition.y, 1f - Mathf.Cos((num * 3.141593f) * 0.5f));
                if (num < 1f)
                {
                    goto Label_009A;
                }
                self.get_transform().set_position(this.mEndPosition);
                self.mOnFieldActionEnd();
                return;
            Label_009A:
                self.get_transform().set_position(vector);
                return;
            }
        }

        private class State_FieldActionJump : TacticsUnitController.State_FieldAction
        {
            private Vector3 mStartPosition;
            private Vector3 mEndPosition;
            private float mTime;
            private float mAnimRate;
            private float mJumpHeight;

            public State_FieldActionJump()
            {
                base..ctor();
                return;
            }

            public override unsafe void Begin(TacticsUnitController self)
            {
                float num;
                this.mStartPosition = self.get_transform().get_position();
                this.mEndPosition = GameUtility.RaycastGround(self.mFieldActionPoint);
                num = &this.mEndPosition.y - &this.mStartPosition.y;
                this.mJumpHeight = Mathf.Abs(num);
                if (num >= 0f)
                {
                    goto Label_0063;
                }
                this.mJumpHeight *= 0.5f;
            Label_0063:
                this.mAnimRate = 1f / (GameUtility.CalcDistance2D(this.mStartPosition, this.mEndPosition) / &GameSettings.Instance.Quest.MapRunSpeedMax);
                self.get_transform().set_rotation(SRPG_Extensions.ToRotation(self.mFieldActionDir));
                self.mCollideGround = 0;
                return;
            }

            public override void End(TacticsUnitController self)
            {
                self.mCollideGround = 1;
                return;
            }

            public override unsafe void Update(TacticsUnitController self)
            {
                Vector3 vector;
                this.mTime = Mathf.Clamp01(this.mTime + (this.mAnimRate * Time.get_deltaTime()));
                vector = Vector3.Lerp(this.mStartPosition, this.mEndPosition, this.mTime);
                &vector.y += Mathf.Sin(this.mTime * 3.141593f) * this.mJumpHeight;
                if (this.mTime < 1f)
                {
                    goto Label_0089;
                }
                self.get_transform().set_position(this.mEndPosition);
                self.mOnFieldActionEnd();
                return;
            Label_0089:
                self.get_transform().set_position(vector);
                return;
            }
        }

        private class State_FieldActionJumpUp : TacticsUnitController.State_FieldAction
        {
            private Vector3 mStartPosition;
            private Vector3 mEndPosition;
            private float mTime;
            private float mDuration;
            private bool mFalling;
            private float mLastY;

            public State_FieldActionJumpUp()
            {
                base..ctor();
                return;
            }

            public override unsafe void Begin(TacticsUnitController self)
            {
                GameSettings settings;
                this.mEndPosition = GameUtility.RaycastGround(self.mFieldActionPoint);
                this.mStartPosition = self.get_transform().get_position();
                self.PlayAnimation("CLMB", 1, 0.1f, 0f);
                self.RootMotionMode = 2;
                self.mCollideGround = 0;
                settings = GameSettings.Instance;
                this.mDuration = settings.Unit_JumpMinTime + (settings.Unit_JumpTimePerHeight * (&this.mEndPosition.y - &this.mStartPosition.y));
                return;
            }

            public override void End(TacticsUnitController self)
            {
                self.RootMotionMode = 0;
                self.mCollideGround = 1;
                return;
            }

            public override unsafe void Update(TacticsUnitController self)
            {
                GameSettings settings;
                float num;
                Vector3 vector;
                float num2;
                float num3;
                settings = GameSettings.Instance;
                this.mTime += Time.get_deltaTime();
                num = Mathf.Clamp01(this.mTime / this.mDuration);
                vector = Vector3.get_zero();
                num2 = settings.Unit_JumpZCurve.Evaluate(num);
                num3 = settings.Unit_JumpYCurve.Evaluate(num);
                &vector.x = Mathf.Lerp(&this.mStartPosition.x, &this.mEndPosition.x, num2);
                &vector.z = Mathf.Lerp(&this.mStartPosition.z, &this.mEndPosition.z, num2);
                &vector.y = Mathf.Lerp(&this.mStartPosition.y, &this.mEndPosition.y, num) + num3;
                self.get_transform().set_position(vector);
                if (this.mFalling != null)
                {
                    goto Label_00F9;
                }
                if (num3 >= this.mLastY)
                {
                    goto Label_00F9;
                }
                self.PlayAnimation("FLLP", 1, 0.1f, 0f);
                this.mFalling = 1;
            Label_00F9:
                this.mLastY = num3;
                if (num < 1f)
                {
                    goto Label_0118;
                }
                self.mOnFieldActionEnd();
                return;
            Label_0118:
                return;
            }
        }

        private class State_Idle : TacticsUnitController.State
        {
            public State_Idle()
            {
                base..ctor();
                return;
            }

            public override void Begin(TacticsUnitController self)
            {
                string str;
                if (self.mUnit == null)
                {
                    goto Label_001C;
                }
                if (self.mUnit.IsBreakObj == null)
                {
                    goto Label_001C;
                }
                return;
            Label_001C:
                str = "IDLE";
                self.RootMotionMode = 0;
                self.UpdateBadStatus();
                if (self.mBadStatus == null)
                {
                    goto Label_0086;
                }
                if (string.IsNullOrEmpty(self.mBadStatus.AnimationName) != null)
                {
                    goto Label_0086;
                }
                if ((self.FindAnimation("BAD") != null) == null)
                {
                    goto Label_0086;
                }
                if ((self.mBadStatus.AnimationName == self.mLoadedBadStatusAnimation) == null)
                {
                    goto Label_0086;
                }
                str = "BAD";
            Label_0086:
                if (self.GetTargetWeight(str) >= 1f)
                {
                    goto Label_00AA;
                }
                self.PlayAnimation(str, 1, self.mIdleInterpTime, 0f);
            Label_00AA:
                return;
            }

            public override void Update(TacticsUnitController self)
            {
                string str;
                if ((self.mUnit == null) || (self.mUnit.IsBreakObj == null))
                {
                    goto Label_001C;
                }
                return;
            Label_001C:
                str = (self.mBadStatus == null) ? null : self.mBadStatus.AnimationName;
                if (string.IsNullOrEmpty(str) != null)
                {
                    goto Label_010C;
                }
                if (string.IsNullOrEmpty(self.mLoadedBadStatusAnimation) != null)
                {
                    goto Label_009C;
                }
                if ((self.mLoadedBadStatusAnimation != str) == null)
                {
                    goto Label_009C;
                }
                if (self.FindAnimation("BAD") == null)
                {
                    goto Label_0159;
                }
                self.UnloadAnimation("BAD");
                self.StopAnimation("BAD");
                self.mLoadedBadStatusAnimation = null;
                goto Label_0107;
            Label_009C:
                if (string.IsNullOrEmpty(self.mLoadedBadStatusAnimation) == null)
                {
                    goto Label_00C6;
                }
                self.LoadUnitAnimationAsync("BAD", str, 0, 0);
                self.mLoadedBadStatusAnimation = str;
                goto Label_0107;
            Label_00C6:
                if ((self.FindAnimation("BAD") != null) == null)
                {
                    goto Label_0159;
                }
                if (self.GetTargetWeight("BAD") >= 1f)
                {
                    goto Label_0159;
                }
                self.PlayAnimation("BAD", 1, 0.25f, 0f);
            Label_0107:
                goto Label_0159;
            Label_010C:
                if (string.IsNullOrEmpty(self.mLoadedBadStatusAnimation) != null)
                {
                    goto Label_0159;
                }
                if (self.FindAnimation("BAD") == null)
                {
                    goto Label_0159;
                }
                self.UnloadAnimation("BAD");
                self.mLoadedBadStatusAnimation = null;
                self.PlayAnimation("IDLE", 1, 0.25f, 0f);
            Label_0159:
                if (self.AutoUpdateRotation == null)
                {
                    goto Label_016A;
                }
                self.UpdateRotation();
            Label_016A:
                return;
            }
        }

        private class State_JumpCast : TacticsUnitController.State
        {
            private float mBasePosY;
            private float mCastTime;
            private float mElapsed;

            public State_JumpCast()
            {
                this.mCastTime = 0.5f;
                base..ctor();
                return;
            }

            public override unsafe void Begin(TacticsUnitController self)
            {
                Vector3 vector;
                this.mBasePosY = &self.get_transform().get_position().y;
                this.mElapsed = 0f;
                self.CollideGround = 0;
                self.mCastJumpStartComplete = 0;
                self.mCastJumpFallComplete = 0;
                self.PlayAnimation("CLMB", 0, this.mCastTime / 10f, 0f);
                MonoSingleton<MySound>.Instance.PlaySEOneShot("SE_7036", 0f);
                return;
            }

            public override void End(TacticsUnitController self)
            {
                self.CollideGround = 1;
                self.SetVisible(0);
                self.mCastJumpStartComplete = 1;
                return;
            }

            public override unsafe void Update(TacticsUnitController self)
            {
                Vector3 vector;
                if (this.mCastTime > this.mElapsed)
                {
                    goto Label_001D;
                }
                self.PlayIdle(0f);
                return;
            Label_001D:
                this.mElapsed += Time.get_deltaTime();
                if (this.mCastTime >= this.mElapsed)
                {
                    goto Label_004C;
                }
                this.mElapsed = this.mCastTime;
            Label_004C:
                vector = self.get_transform().get_position();
                &vector.y = this.mBasePosY + (self.mCastJumpOffsetY * (this.mElapsed / this.mCastTime));
                self.get_transform().set_position(vector);
                return;
            }
        }

        private class State_JumpCastComplete : TacticsUnitController.State
        {
            private MotionMode Mode;
            private GameObject mFallEffect;
            private GameObject mHitEffect;
            private Vector3 mBasePos;
            private IntVector2 mBaseMapPos;
            private float mCastTime;
            private float mElapsed;
            private TacticsUnitController[] mExcludes;
            private static readonly Color SceneFadeColor;
            private float mFallStartWait;
            private float TransStartTime;
            private float TransWaitTime;
            private bool beforeVisible;
            private float ReturnTime;
            private bool isDirect;

            static State_JumpCastComplete()
            {
                SceneFadeColor = new Color(0.2f, 0.2f, 0.2f, 1f);
                return;
            }

            public State_JumpCastComplete()
            {
                this.mCastTime = 0.3f;
                this.mFallStartWait = 0.7f;
                this.TransWaitTime = 0.1f;
                this.beforeVisible = 1;
                this.ReturnTime = 0.3f;
                base..ctor();
                return;
            }

            public override unsafe void Begin(TacticsUnitController self)
            {
                SceneBattle battle;
                this.mBasePos = self.get_transform().get_position();
                this.mBaseMapPos = new IntVector2(self.Unit.x, self.Unit.y);
                if (self.mSkillVars.mLandingGrid == null)
                {
                    goto Label_006D;
                }
                this.mBaseMapPos = new IntVector2(self.mSkillVars.mLandingGrid.x, self.mSkillVars.mLandingGrid.y);
            Label_006D:
                this.mElapsed = 0f;
                self.mCastJumpFallComplete = 0;
                self.CollideGround = 0;
                this.Mode = 0;
                self.AnimateVessel(1f, 0f);
                if ((SceneBattle.Instance != null) == null)
                {
                    goto Label_0134;
                }
                battle = SceneBattle.Instance;
                this.mExcludes = battle.GetActiveUnits();
                Array.Resize<TacticsUnitController>(&this.mExcludes, ((int) this.mExcludes.Length) + 1);
                this.mExcludes[((int) this.mExcludes.Length) - 1] = self;
                FadeController.Instance.BeginSceneFade(SceneFadeColor, 0.5f, this.mExcludes, null);
                this.mBasePos = battle.CalcGridCenter(&this.mBaseMapPos.x, &this.mBaseMapPos.y);
                self.mSkillVars.mStartPosition = this.mBasePos;
            Label_0134:
                self.PlayAnimation("FLLP", 1, 0f, 0f);
                return;
            }

            public override void End(TacticsUnitController self)
            {
                self.CollideGround = 1;
                self.mCastJumpFallComplete = 1;
                self.get_transform().set_position(this.mBasePos);
                return;
            }

            private unsafe void EnterFall()
            {
                Vector3 vector;
                base.self.SetVisible(1);
                base.self.SetEquipmentsVisible(1);
                if (base.self.mSkillVars == null)
                {
                    goto Label_00A8;
                }
                if ((base.self.mSkillVars.mSkillEffect.AuraEffect != null) == null)
                {
                    goto Label_00A8;
                }
                this.mFallEffect = Object.Instantiate(base.self.mSkillVars.mSkillEffect.AuraEffect, base.self.get_transform().get_position(), base.self.get_transform().get_rotation()) as GameObject;
                this.mFallEffect.get_transform().SetParent(base.self.get_transform());
            Label_00A8:
                vector = base.self.get_transform().get_position();
                &vector.y = base.self.mCastJumpOffsetY;
                base.self.get_transform().set_position(vector);
                this.Mode = 1;
                this.mElapsed = 0f;
                return;
            }

            private unsafe void EnterReturn()
            {
                int num;
                Vector3 vector;
                float num2;
                float num3;
                float num4;
                if ((this.mFallEffect != null) == null)
                {
                    goto Label_0028;
                }
                GameUtility.RequireComponent<OneShotParticle>(this.mFallEffect);
                GameUtility.StopEmitters(this.mFallEffect);
            Label_0028:
                if ((this.mHitEffect != null) == null)
                {
                    goto Label_0045;
                }
                GameUtility.RequireComponent<OneShotParticle>(this.mHitEffect);
            Label_0045:
                num = 0;
                goto Label_00F9;
            Label_004C:
                vector = base.self.mSkillVars.Targets[num].get_transform().get_position() - base.self.mSkillVars.mTargetPosition;
                &vector.y = 0f;
                float introduced5 = Mathf.Abs(&vector.x);
                num2 = introduced5 + Mathf.Abs(&vector.z);
                base.self.mSkillVars.HitTimers.Add(new TacticsUnitController.HitTimer(base.self.mSkillVars.Targets[num], Time.get_time() + (num2 * base.self.mSkillVars.mSkillEffect.MapHitEffectIntervals)));
                num += 1;
            Label_00F9:
                if (num < base.self.mSkillVars.Targets.Count)
                {
                    goto Label_004C;
                }
                base.self.PlayAnimation("STEP", 0);
                num3 = (float) (&this.mBaseMapPos.x - &base.self.JumpMapFallPos.x);
                num4 = (float) (&this.mBaseMapPos.y - &base.self.JumpMapFallPos.y);
                if ((num3 * num3) > 1f)
                {
                    goto Label_0185;
                }
                if ((num4 * num4) > 1f)
                {
                    goto Label_0185;
                }
                this.isDirect = 1;
            Label_0185:
                this.mElapsed = 0f;
                this.TransStartTime = Time.get_time();
                return;
            }

            private unsafe void FallUpdate(TacticsUnitController self)
            {
                Vector3 vector;
                if (this.mElapsed < this.mCastTime)
                {
                    goto Label_0070;
                }
                if (self.mSkillVars == null)
                {
                    goto Label_0062;
                }
                if ((self.mSkillVars.mSkillEffect.TargetHitEffect != null) == null)
                {
                    goto Label_0062;
                }
                this.mHitEffect = Object.Instantiate(self.mSkillVars.mSkillEffect.TargetHitEffect, self.JumpFallPos, Quaternion.get_identity()) as GameObject;
            Label_0062:
                this.Mode = 2;
                this.EnterReturn();
                return;
            Label_0070:
                this.mElapsed += Time.get_deltaTime();
                if (this.mElapsed < this.mCastTime)
                {
                    goto Label_009F;
                }
                this.mElapsed = this.mCastTime;
            Label_009F:
                vector = self.JumpFallPos;
                &vector.y = &self.JumpFallPos.y + (self.mCastJumpOffsetY * (1f - (this.mElapsed / this.mCastTime)));
                self.get_transform().set_position(vector);
                return;
            }

            private void FallWaitUpdate(TacticsUnitController self)
            {
                if (this.mElapsed < this.mFallStartWait)
                {
                    goto Label_0018;
                }
                this.EnterFall();
                return;
            Label_0018:
                this.mElapsed += Time.get_deltaTime();
                if (this.mElapsed < this.mFallStartWait)
                {
                    goto Label_0047;
                }
                this.mElapsed = this.mFallStartWait;
            Label_0047:
                return;
            }

            private void ReturnUpdate(TacticsUnitController self)
            {
                SceneBattle battle;
                Vector3 vector;
                bool flag;
                if (this.TransWaitTime <= (Time.get_time() - this.TransStartTime))
                {
                    goto Label_0018;
                }
                return;
            Label_0018:
                this.mElapsed += Time.get_deltaTime();
                if (this.mElapsed < this.ReturnTime)
                {
                    goto Label_0064;
                }
                this.mElapsed = this.ReturnTime;
                battle = SceneBattle.Instance;
                if (battle == null)
                {
                    goto Label_005E;
                }
                battle.OnGimmickUpdate();
            Label_005E:
                self.GotoState<TacticsUnitController.State_SkillEnd>();
            Label_0064:
                vector = this.mBasePos - self.JumpFallPos;
                self.get_transform().set_position(self.JumpFallPos + (vector * (this.mElapsed / this.ReturnTime)));
                flag = this.beforeVisible;
                if (this.isDirect != null)
                {
                    goto Label_00E8;
                }
                if ((this.ReturnTime * 0.85f) >= this.mElapsed)
                {
                    goto Label_00CF;
                }
                flag = 1;
                goto Label_00E8;
            Label_00CF:
                if ((this.ReturnTime * 0.15f) >= this.mElapsed)
                {
                    goto Label_00E8;
                }
                flag = 0;
            Label_00E8:
                if (this.beforeVisible == flag)
                {
                    goto Label_00FB;
                }
                self.SetVisible(flag);
            Label_00FB:
                this.beforeVisible = flag;
                return;
            }

            public override void Update(TacticsUnitController self)
            {
                if (this.Mode != null)
                {
                    goto Label_0017;
                }
                this.FallWaitUpdate(self);
                goto Label_0042;
            Label_0017:
                if (this.Mode != 1)
                {
                    goto Label_002F;
                }
                this.FallUpdate(self);
                goto Label_0042;
            Label_002F:
                if (this.Mode != 2)
                {
                    goto Label_0042;
                }
                this.ReturnUpdate(self);
            Label_0042:
                return;
            }

            private enum MotionMode
            {
                FALL_WAIT,
                FALL,
                RETURN
            }
        }

        private class State_JumpCastFall : TacticsUnitController.State
        {
            private float mBasePosY;
            private float mCastTime;
            private float mElapsed;
            private eMotionMode mMotionMode;

            public State_JumpCastFall()
            {
                this.mCastTime = 0.5f;
                base..ctor();
                return;
            }

            public override unsafe void Begin(TacticsUnitController self)
            {
                Vector3 vector;
                Vector3 vector2;
                this.mBasePosY = &self.get_transform().get_position().y;
                this.mElapsed = 0f;
                self.CollideGround = 0;
                self.mFinishedCastJumpFall = 0;
                self.PlayAnimation("IDLE", 0);
                vector = self.get_transform().get_position();
                &vector.y += self.mCastJumpOffsetY;
                self.get_transform().set_position(vector);
                self.SetVisible(1);
                this.mMotionMode = 0;
                return;
            }

            public override void End(TacticsUnitController self)
            {
                self.CollideGround = 1;
                self.mFinishedCastJumpFall = 1;
                return;
            }

            public override unsafe void Update(TacticsUnitController self)
            {
                SceneBattle battle;
                Vector3 vector;
                eMotionMode mode;
                mode = this.mMotionMode;
                if (mode == null)
                {
                    goto Label_0019;
                }
                if (mode == 1)
                {
                    goto Label_00FC;
                }
                goto Label_0121;
            Label_0019:
                if (this.mElapsed < this.mCastTime)
                {
                    goto Label_0087;
                }
                battle = SceneBattle.Instance;
                if (battle == null)
                {
                    goto Label_004E;
                }
                self.createEffect(self, battle.JumpFallEffect);
                battle.OnGimmickUpdate();
            Label_004E:
                if (self.mIsPlayDamageMotion == null)
                {
                    goto Label_007B;
                }
                self.PlayAnimation("B_DMG0", 0, 0.1f, 0f);
                this.mMotionMode = 1;
                goto Label_0086;
            Label_007B:
                self.PlayIdle(0f);
            Label_0086:
                return;
            Label_0087:
                this.mElapsed += Time.get_deltaTime();
                if (this.mElapsed <= this.mCastTime)
                {
                    goto Label_00B6;
                }
                this.mElapsed = this.mCastTime;
            Label_00B6:
                vector = self.get_transform().get_position();
                &vector.y = this.mBasePosY + (self.mCastJumpOffsetY * ((this.mCastTime - this.mElapsed) / this.mCastTime));
                self.get_transform().set_position(vector);
                goto Label_0121;
            Label_00FC:
                if (self.GetRemainingTime("B_DMG0") > 0.1f)
                {
                    goto Label_0121;
                }
                self.PlayIdle(0.1f);
            Label_0121:
                return;
            }

            private enum eMotionMode
            {
                Fall,
                Damage
            }
        }

        private class State_Kirimomi : TacticsUnitController.State
        {
            private Quaternion mRotationOld;

            public State_Kirimomi()
            {
                base..ctor();
                return;
            }

            public override void Begin(TacticsUnitController self)
            {
                this.mRotationOld = self.UnitObject.get_transform().get_localRotation();
                return;
            }

            public override void End(TacticsUnitController self)
            {
                self.UnitObject.get_transform().set_localRotation(this.mRotationOld);
                return;
            }

            public override unsafe void Update(TacticsUnitController self)
            {
                self.UnitObject.get_transform().set_localRotation(Quaternion.AngleAxis(Time.get_time() * &GameSettings.Instance.Quest.KirimomiRotationRate, Vector3.get_up()));
                return;
            }
        }

        private class State_LookAt : TacticsUnitController.State
        {
            private float mTime;
            private float mSpinTime;
            private float mSpinCount;
            private Transform mTransform;
            private Vector3 mStartPosition;
            private Quaternion mEndRotation;
            private float mJumpHeight;

            public State_LookAt()
            {
                this.mSpinTime = 0.25f;
                this.mSpinCount = 2f;
                this.mJumpHeight = 0.2f;
                base..ctor();
                return;
            }

            public override unsafe void Begin(TacticsUnitController self)
            {
                Vector3 vector;
                this.mSpinCount = self.mSpinCount;
                this.mSpinTime = this.mSpinCount * 0.125f;
                this.mJumpHeight = this.mSpinCount * 0.1f;
                this.mTransform = self.get_transform();
                vector = self.mLookAtTarget - this.mTransform.get_position();
                &vector.y = 0f;
                this.mStartPosition = this.mTransform.get_position();
                this.mEndRotation = Quaternion.LookRotation(vector);
                return;
            }

            public override unsafe void Update(TacticsUnitController self)
            {
                float num;
                Quaternion quaternion;
                Quaternion quaternion2;
                Vector3 vector;
                this.mTime += Time.get_deltaTime();
                num = Mathf.Clamp01(this.mTime / this.mSpinTime);
                quaternion2 = Quaternion.AngleAxis(((1f - num) * this.mSpinCount) * 360f, Vector3.get_up()) * this.mEndRotation;
                this.mTransform.set_rotation(quaternion2);
                vector = this.mStartPosition;
                &vector.y += Mathf.Sin(num * 3.141593f) * this.mJumpHeight;
                this.mTransform.set_position(vector);
                if (num < 1f)
                {
                    goto Label_00A8;
                }
                self.PlayIdle(0f);
                return;
            Label_00A8:
                return;
            }
        }

        private class State_Move : TacticsUnitController.State
        {
            private Vector3 mStartPos;
            private bool mAdjustDirection;
            private Quaternion mDesiredRotation;
            private float mRotationRate;

            public State_Move()
            {
                base..ctor();
                return;
            }

            public override unsafe void Begin(TacticsUnitController self)
            {
                if (self.mRoutePos >= ((int) self.mRoute.Length))
                {
                    goto Label_0079;
                }
                self.PlayAnimation(self.mRunAnimation, 1, self.mMoveAnimInterpTime, 0f);
                this.LookToward(self, *(&(self.mRoute[self.mRoutePos])) - self.get_transform().get_position());
                this.mStartPos = *(&(self.mRoute[self.mRoutePos]));
                goto Label_008A;
            Label_0079:
                this.mStartPos = self.get_transform().get_position();
            Label_008A:
                if (self.mPostMoveAngle < 0f)
                {
                    goto Label_00C2;
                }
                this.mAdjustDirection = 1;
                this.mDesiredRotation = Quaternion.AngleAxis(self.mPostMoveAngle, Vector3.get_up());
                this.mRotationRate = 720f;
            Label_00C2:
                return;
            }

            private unsafe void LookToward(TacticsUnitController self, Vector3 v)
            {
                &v.y = 0f;
                if (&v.get_magnitude() > 0.0001f)
                {
                    goto Label_001E;
                }
                return;
            Label_001E:
                self.get_transform().set_rotation(Quaternion.LookRotation(v));
                return;
            }

            public override unsafe void Update(TacticsUnitController self)
            {
                Transform transform;
                Quaternion quaternion;
                Vector3 vector;
                float num;
                Vector3 vector2;
                Vector3 vector3;
                float num2;
                float num3;
                Vector3 vector4;
                float num4;
                float num5;
                Vector3 vector5;
                float num6;
                Vector3 vector6;
                float num7;
                transform = self.get_transform();
                if (((int) self.mRoute.Length) > self.mRoutePos)
                {
                    goto Label_0079;
                }
                if (this.mAdjustDirection == null)
                {
                    goto Label_0061;
                }
                quaternion = transform.get_rotation();
                if (Quaternion.Angle(quaternion, this.mDesiredRotation) <= 1f)
                {
                    goto Label_0061;
                }
                transform.set_rotation(Quaternion.RotateTowards(quaternion, this.mDesiredRotation, this.mRotationRate * Time.get_deltaTime()));
                return;
            Label_0061:
                transform.set_rotation(this.mDesiredRotation);
                self.PlayIdle(0f);
                return;
            Label_0079:
                vector = *(&(self.mRoute[self.mRoutePos])) - this.mStartPos;
                &vector.y = 0f;
                num = &vector.get_sqrMagnitude();
                if (num > 0.0001f)
                {
                    goto Label_010F;
                }
                transform.set_position(*(&(self.mRoute[self.mRoutePos])));
                self.mRoutePos += 1;
                if (this.mAdjustDirection != null)
                {
                    goto Label_010E;
                }
                if (((int) self.mRoute.Length) > self.mRoutePos)
                {
                    goto Label_010E;
                }
                self.PlayIdle(0f);
                return;
            Label_010E:
                return;
            Label_010F:
                if (num <= 0.0001f)
                {
                    goto Label_0318;
                }
                vector2 = vector;
                &vector2.y = 0f;
                &vector2.Normalize();
                if (self.TriggerFieldAction(vector2, 1) == null)
                {
                    goto Label_014A;
                }
                self.mMoveAnimInterpTime = 0.1f;
                return;
            Label_014A:
                vector3 = self.get_transform().get_position() + ((vector2 * self.mRunSpeed) * Time.get_deltaTime());
                self.get_transform().set_position(vector3);
                num3 = GameUtility.CalcDistance2D(vector3, *(&(self.mRoute[self.mRoutePos])));
                if (num3 >= 0.5f)
                {
                    goto Label_0243;
                }
                if (self.mRoutePos >= (((int) self.mRoute.Length) - 1))
                {
                    goto Label_0243;
                }
                vector4 = *(&(self.mRoute[self.mRoutePos + 1])) - *(&(self.mRoute[self.mRoutePos]));
                &vector4.y = 0f;
                &vector4.Normalize();
                num4 = (1f - (num3 / 0.5f)) * 0.5f;
                self.get_transform().set_rotation(Quaternion.Slerp(Quaternion.LookRotation(vector2), Quaternion.LookRotation(vector4), num4));
                goto Label_0318;
            Label_0243:
                if (self.mRoutePos <= 1)
                {
                    goto Label_030F;
                }
                num5 = GameUtility.CalcDistance2D(vector3, *(&(self.mRoute[self.mRoutePos - 1])));
                if (num5 >= 0.5f)
                {
                    goto Label_0301;
                }
                vector5 = *(&(self.mRoute[self.mRoutePos - 1])) - *(&(self.mRoute[self.mRoutePos - 2]));
                &vector5.y = 0f;
                &vector5.Normalize();
                num6 = 0.5f + ((num5 / 0.5f) * 0.5f);
                self.get_transform().set_rotation(Quaternion.Slerp(Quaternion.LookRotation(vector5), Quaternion.LookRotation(vector2), num6));
                goto Label_030A;
            Label_0301:
                this.LookToward(self, vector2);
            Label_030A:
                goto Label_0318;
            Label_030F:
                this.LookToward(self, vector2);
            Label_0318:
                vector6 = transform.get_position() - this.mStartPos;
                &vector6.y = 0f;
                if ((Vector3.Dot(vector, vector6) / num) < 0.9999f)
                {
                    goto Label_03C4;
                }
                this.mStartPos = *(&(self.mRoute[self.mRoutePos]));
                self.get_transform().set_position(*(&(self.mRoute[self.mRoutePos])));
                self.mRoutePos += 1;
                if (this.mAdjustDirection != null)
                {
                    goto Label_03C4;
                }
                if (((int) self.mRoute.Length) > self.mRoutePos)
                {
                    goto Label_03C4;
                }
                self.PlayIdle(0f);
                return;
            Label_03C4:
                return;
            }
        }

        private class State_NormalDamage : TacticsUnitController.State
        {
            public State_NormalDamage()
            {
                base..ctor();
                return;
            }

            public override void Begin(TacticsUnitController self)
            {
                self.PlayAnimation("B_DMG0", 0, 0.1f, 0f);
                return;
            }

            public override void Update(TacticsUnitController self)
            {
                if (self.GetRemainingTime("B_DMG0") > 0.1f)
                {
                    goto Label_0020;
                }
                self.PlayIdle(0.1f);
            Label_0020:
                return;
            }
        }

        private class State_Pickup : TacticsUnitController.State
        {
            private Vector3 mObjectStartPos;
            private Vector3 mTopPos;
            private bool mPickedUp;
            private float mPostPickupDelay;

            public State_Pickup()
            {
                base..ctor();
                return;
            }

            public override unsafe void Begin(TacticsUnitController self)
            {
                MapPickup pickup;
                Vector3 vector;
                self.AutoUpdateRotation = 0;
                this.mPostPickupDelay = &GameSettings.Instance.Quest.WaitAfterUnitPickupGimmick;
                self.PlayAnimation("PICK", 0);
                self.SetEquipmentsVisible(0);
                this.mObjectStartPos = self.mPickupObject.get_transform().get_position();
                this.mTopPos = this.mObjectStartPos;
                if ((self.mCharacterSettings != null) == null)
                {
                    goto Label_0093;
                }
                &this.mTopPos.y += self.mCharacterSettings.Height * &self.get_transform().get_lossyScale().y;
            Label_0093:
                pickup = self.mPickupObject.GetComponentInChildren<MapPickup>();
                if ((pickup != null) == null)
                {
                    goto Label_00CD;
                }
                if ((pickup.Shadow != null) == null)
                {
                    goto Label_00CD;
                }
                pickup.Shadow.get_gameObject().SetActive(0);
            Label_00CD:
                return;
            }

            public override void Update(TacticsUnitController self)
            {
                float num;
                Vector3 vector;
                Transform transform;
                MapPickup pickup;
                num = self.GetNormalizedTime("PICK");
                vector = Vector3.Lerp(this.mObjectStartPos, this.mTopPos, Mathf.Sin((num * 3.141593f) * 0.5f));
                transform = self.mPickupObject.get_transform();
                transform.set_rotation(Quaternion.LookRotation(Vector3.get_back()));
                transform.set_position(vector);
                if (self.GetRemainingTime("PICK") > 0f)
                {
                    goto Label_00BA;
                }
                if (this.mPickedUp != null)
                {
                    goto Label_00A8;
                }
                pickup = self.mPickupObject.GetComponentInChildren<MapPickup>();
                if ((pickup != null) == null)
                {
                    goto Label_00A1;
                }
                if (pickup.OnPickup == null)
                {
                    goto Label_00A1;
                }
                pickup.OnPickup();
            Label_00A1:
                this.mPickedUp = 1;
            Label_00A8:
                this.mPostPickupDelay -= Time.get_deltaTime();
            Label_00BA:
                if (this.mPostPickupDelay > 0f)
                {
                    goto Label_00D1;
                }
                self.GotoState<TacticsUnitController.State_PostPickup>();
                return;
            Label_00D1:
                return;
            }
        }

        private class State_PostPickup : TacticsUnitController.State
        {
            public State_PostPickup()
            {
                base..ctor();
                return;
            }

            public override void Begin(TacticsUnitController self)
            {
                ObjectAnimator.Get(self.mPickupObject).ScaleTo(Vector3.get_zero(), 0.2f, 0);
                GameUtility.StopEmitters(self.mPickupObject);
                self.SetEquipmentsVisible(1);
                return;
            }

            public override void End(TacticsUnitController self)
            {
                self.mPickupObject = null;
                self.UnloadPickupAnimation();
                return;
            }

            public override void Update(TacticsUnitController self)
            {
                if (ObjectAnimator.Get(self.mPickupObject).isMoving == null)
                {
                    goto Label_0016;
                }
                return;
            Label_0016:
                self.PlayIdle(0f);
                return;
            }
        }

        private class State_PreSkill : TacticsUnitController.State
        {
            public State_PreSkill()
            {
                base..ctor();
                return;
            }

            public override void Begin(TacticsUnitController self)
            {
                self.PlayAnimation("B_PRS", 0);
                self.mSkillVars.mAuraEnable = 0;
                self.RootMotionMode = 1;
                return;
            }

            public override void End(TacticsUnitController self)
            {
                self.RootMotionMode = 0;
                return;
            }

            public override void Update(TacticsUnitController self)
            {
                float num;
                num = self.GetNormalizedTime("B_PRS");
                self.AnimateCamera(self.mSkillVars.mSkillStartAnimation.animation, num, self.mSkillVars.mCameraID);
                if (num < 1f)
                {
                    goto Label_0044;
                }
                self.PlayIdle(0f);
            Label_0044:
                return;
            }
        }

        private class State_RangedSkillEnd : TacticsUnitController.State
        {
            private float mAnimationLength;
            private float mStateTime;
            private bool mUnitAnimationEnded;
            private bool mHit;
            private bool mProjEndAnimEnded;

            public State_RangedSkillEnd()
            {
                base..ctor();
                return;
            }

            public override void Begin(TacticsUnitController self)
            {
                this.mAnimationLength = self.mSkillVars.mSkillSequence.EndLength;
                if (self.mSkillVars.mProjectileTriggered != null)
                {
                    goto Label_002D;
                }
                self.OnProjectileHit(null);
            Label_002D:
                if ((self.FindAnimation("B_BS") != null) == null)
                {
                    goto Label_0059;
                }
                self.PlayAnimation("B_BS", 0, 0.1f, 0f);
            Label_0059:
                return;
            }

            public override void End(TacticsUnitController self)
            {
                self.mSkillVars.mTargetController = null;
                return;
            }

            private void OnHit(TacticsUnitController.ProjectileData pd)
            {
                base.self.OnProjectileHit(pd);
                return;
            }

            public override unsafe void Update(TacticsUnitController self)
            {
                TacticsUnitController.ProjectileData data;
                List<TacticsUnitController.ProjectileData>.Enumerator enumerator;
                ParticleSystem system;
                Vector3 vector;
                Quaternion quaternion;
                GameSettings settings;
                Transform transform;
                Vector3 vector2;
                Quaternion quaternion2;
                TacticsUnitController.ProjectileData data2;
                List<TacticsUnitController.ProjectileData>.Enumerator enumerator2;
                bool flag;
                TacticsUnitController.ProjectileData data3;
                List<TacticsUnitController.ProjectileData>.Enumerator enumerator3;
                this.mStateTime += Time.get_deltaTime();
                if (self.mSkillVars.UseBattleScene == null)
                {
                    goto Label_01E1;
                }
                enumerator = self.mSkillVars.mProjectileDataLists.GetEnumerator();
            Label_0033:
                try
                {
                    goto Label_00F7;
                Label_0038:
                    data = &enumerator.Current;
                    if (data.mProjStartAnimEnded != null)
                    {
                        goto Label_00F7;
                    }
                    if ((data.mProjectile != null) == null)
                    {
                        goto Label_00F7;
                    }
                    if (data.mProjectileThread == null)
                    {
                        goto Label_006C;
                    }
                    goto Label_0324;
                Label_006C:
                    data.mProjStartAnimEnded = 1;
                    if ((self.mSkillVars.mTargetController != null) == null)
                    {
                        goto Label_00F7;
                    }
                    data.mProjectileThread = self.StartCoroutine(self.AnimateProjectile(self.mSkillVars.mSkillEffect.ProjectileEnd, self.mSkillVars.mSkillEffect.ProjectileEndTime, self.mSkillVars.mTargetControllerPosition, Quaternion.get_identity(), new TacticsUnitController.ProjectileStopEvent(this.OnHit), data));
                    system = data.mProjectile.GetComponent<ParticleSystem>();
                    if ((system != null) == null)
                    {
                        goto Label_00F7;
                    }
                    system.Clear(1);
                Label_00F7:
                    if (&enumerator.MoveNext() != null)
                    {
                        goto Label_0038;
                    }
                    goto Label_0114;
                }
                finally
                {
                Label_0108:
                    ((List<TacticsUnitController.ProjectileData>.Enumerator) enumerator).Dispose();
                }
            Label_0114:
                if (self.mSkillVars.mIsCollaboSkillSub != null)
                {
                    goto Label_01E1;
                }
                if ((self.mSkillVars.mSkillEndCameraClip != null) == null)
                {
                    goto Label_0190;
                }
                self.CalcCameraPos(self.mSkillVars.mSkillEndCameraClip, Mathf.Clamp01(this.mStateTime / this.mAnimationLength), 0, &vector, &quaternion);
                self.SetActiveCameraPosition(self.mSkillVars.mTargetControllerPosition + vector, self.mSkillVars.mCameraShakeOffset * quaternion);
                goto Label_01E1;
            Label_0190:
                transform = &GameSettings.Instance.Quest.BattleCamera;
                vector2 = transform.get_position();
                quaternion2 = transform.get_rotation();
                self.SetActiveCameraPosition(self.mSkillVars.mTargetControllerPosition + vector2, self.mSkillVars.mCameraShakeOffset * quaternion2);
            Label_01E1:
                if (this.mUnitAnimationEnded != null)
                {
                    goto Label_022E;
                }
                if (self.GetRemainingTime("B_BS") > 0f)
                {
                    goto Label_022E;
                }
                if (self.mSkillVars.UseBattleScene == null)
                {
                    goto Label_0227;
                }
                self.PlayAnimation("IDLE", 1, 0.1f, 0f);
            Label_0227:
                this.mUnitAnimationEnded = 1;
            Label_022E:
                enumerator2 = self.mSkillVars.mProjectileDataLists.GetEnumerator();
            Label_0240:
                try
                {
                    goto Label_025F;
                Label_0245:
                    data2 = &enumerator2.Current;
                    if (data2.mProjectileThread == null)
                    {
                        goto Label_025F;
                    }
                    goto Label_0324;
                Label_025F:
                    if (&enumerator2.MoveNext() != null)
                    {
                        goto Label_0245;
                    }
                    goto Label_027D;
                }
                finally
                {
                Label_0270:
                    ((List<TacticsUnitController.ProjectileData>.Enumerator) enumerator2).Dispose();
                }
            Label_027D:
                if (this.mStateTime < this.mAnimationLength)
                {
                    goto Label_02A6;
                }
                if (this.mHit == null)
                {
                    goto Label_02A6;
                }
                self.SkillEffectSelf();
                self.FinishSkill();
                return;
            Label_02A6:
                if (this.mHit != null)
                {
                    goto Label_0324;
                }
                if (self.mSkillVars.HitTimers.Count > 0)
                {
                    goto Label_0324;
                }
                flag = 1;
                enumerator3 = self.mSkillVars.mProjectileDataLists.GetEnumerator();
            Label_02DC:
                try
                {
                    goto Label_02FE;
                Label_02E1:
                    data3 = &enumerator3.Current;
                    if (data3.mProjectileHitsTarget != null)
                    {
                        goto Label_02FE;
                    }
                    flag = 0;
                    goto Label_030A;
                Label_02FE:
                    if (&enumerator3.MoveNext() != null)
                    {
                        goto Label_02E1;
                    }
                Label_030A:
                    goto Label_031C;
                }
                finally
                {
                Label_030F:
                    ((List<TacticsUnitController.ProjectileData>.Enumerator) enumerator3).Dispose();
                }
            Label_031C:
                this.mHit = flag;
            Label_0324:
                return;
            }
        }

        private class State_RunLoop : TacticsUnitController.State
        {
            public State_RunLoop()
            {
                base..ctor();
                return;
            }

            public override void Begin(TacticsUnitController self)
            {
                self.PlayAnimation(self.mRunAnimation, 1);
                return;
            }

            public override void End(TacticsUnitController self)
            {
            }

            public override void Update(TacticsUnitController self)
            {
            }
        }

        private class State_Skill : TacticsUnitController.State
        {
            private TacticsUnitController.CameraState mStartCameraState;
            private Quaternion mTargetRotation;
            private float mWaitTime;
            private bool mIsProcessed;

            public State_Skill()
            {
                base..ctor();
                return;
            }

            public override unsafe void Begin(TacticsUnitController self)
            {
                if (self.mSkillVars.UseBattleScene == null)
                {
                    goto Label_0026;
                }
                &this.mStartCameraState.CacheCurrent(self.mSkillVars.mActiveCamera);
            Label_0026:
                self.PlayAnimation("B_SKL", 0);
                if (self.mSkillVars.mSkillEffect.StopAura != 1)
                {
                    goto Label_004E;
                }
                self.StopAura();
            Label_004E:
                if ((self.mSkillVars.mTargetController != null) == null)
                {
                    goto Label_007F;
                }
                this.mTargetRotation = self.mSkillVars.mTargetController.get_transform().get_rotation();
            Label_007F:
                this.mWaitTime = self.mSkillVars.mSkillEffect.ProjectileStartTime;
                return;
            }

            public override void End(TacticsUnitController self)
            {
                if (self.mSkillVars.mSkillEffect.StopAura != 2)
                {
                    goto Label_001C;
                }
                self.StopAura();
            Label_001C:
                return;
            }

            public override unsafe void Update(TacticsUnitController self)
            {
                float num;
                Vector3 vector;
                Quaternion quaternion;
                Transform transform;
                self.mSkillVars.mSkillCameraID = self.mSkillVars.mCameraID;
                this.mWaitTime = Mathf.Max(this.mWaitTime - Time.get_deltaTime(), 0f);
                num = self.GetNormalizedTime("B_SKL");
                if (self.mSkillVars.UseBattleScene == null)
                {
                    goto Label_014F;
                }
                if (self.mSkillVars.mSkillAnimation.CurveNames.Contains("Enm_Distance_dummy") == null)
                {
                    goto Label_00C8;
                }
                if ((self.mSkillVars.mTargetController != null) == null)
                {
                    goto Label_00C8;
                }
                self.CalcEnemyPos(self.mSkillVars.mSkillAnimation.animation, num, &vector, &quaternion);
                transform = self.mSkillVars.mTargetController.get_transform();
                transform.set_position(vector);
                transform.set_rotation(quaternion * this.mTargetRotation);
            Label_00C8:
                if ((self.mSkillVars.mSkillCameraClip != null) == null)
                {
                    goto Label_014F;
                }
                if (self.mSkillVars.mIsCollaboSkillSub != null)
                {
                    goto Label_014F;
                }
                if (self.mSkillVars.mSkillSequence.InterpSkillCamera == null)
                {
                    goto Label_0132;
                }
                self.AnimateCameraInterpolated(self.mSkillVars.mSkillCameraClip, num, 4f * num, this.mStartCameraState, self.mSkillVars.mSkillCameraID);
                goto Label_014F;
            Label_0132:
                self.AnimateCamera(self.mSkillVars.mSkillCameraClip, num, self.mSkillVars.mSkillCameraID);
            Label_014F:
                if (self.GetRemainingTime("B_SKL") <= 0f)
                {
                    goto Label_0165;
                }
                return;
            Label_0165:
                if (this.mIsProcessed != null)
                {
                    goto Label_0193;
                }
                if (self.mSkillVars.Skill.IsTransformSkill() == null)
                {
                    goto Label_0193;
                }
                self.SetVisible(0);
                this.mIsProcessed = 1;
            Label_0193:
                if (this.mWaitTime <= 0f)
                {
                    goto Label_01A4;
                }
                return;
            Label_01A4:
                if (self.mSkillVars.mSkillSequence.SkillType != null)
                {
                    goto Label_01C0;
                }
                self.GotoState<TacticsUnitController.State_SkillEnd>();
                return;
            Label_01C0:
                self.GotoState<TacticsUnitController.State_RangedSkillEnd>();
                return;
            }
        }

        private class State_SkillChant : TacticsUnitController.State
        {
            private TacticsUnitController.CameraState mStartCameraState;

            public State_SkillChant()
            {
                TacticsUnitController.CameraState state;
                this.mStartCameraState = new TacticsUnitController.CameraState();
                base..ctor();
                return;
            }

            public override unsafe void Begin(TacticsUnitController self)
            {
                if (self.mSkillVars.UseBattleScene == null)
                {
                    goto Label_0026;
                }
                &this.mStartCameraState.CacheCurrent(self.mSkillVars.mActiveCamera);
            Label_0026:
                self.PlayAnimation("B_CHA", 0);
                if ((self.mSkillVars.mSkillEffect != null) == null)
                {
                    goto Label_0072;
                }
                if (self.mSkillVars.mSkillEffect.ChantSound == null)
                {
                    goto Label_0072;
                }
                self.mSkillVars.mSkillEffect.ChantSound.Play();
            Label_0072:
                return;
            }

            public override void End(TacticsUnitController self)
            {
                if (self.mSkillVars.mSkillEffect.StopAura != null)
                {
                    goto Label_001B;
                }
                self.StopAura();
            Label_001B:
                return;
            }

            public override void Update(TacticsUnitController self)
            {
                float num;
                ParticleSystem system;
                TacticsUnitController controller;
                self.mSkillVars.mChantCameraID = self.mSkillVars.mCameraID;
                num = self.GetNormalizedTime("B_CHA");
                if (self.mSkillVars.UseBattleScene == null)
                {
                    goto Label_00B9;
                }
                if (self.mSkillVars.mIsCollaboSkillSub != null)
                {
                    goto Label_00B9;
                }
                if ((self.mSkillVars.mSkillChantCameraClip != null) == null)
                {
                    goto Label_00B9;
                }
                if (self.mSkillVars.mSkillSequence.InterpChantCamera == null)
                {
                    goto Label_009C;
                }
                self.AnimateCameraInterpolated(self.mSkillVars.mSkillChantCameraClip, num, 4f * num, this.mStartCameraState, self.mSkillVars.mChantCameraID);
                goto Label_00B9;
            Label_009C:
                self.AnimateCamera(self.mSkillVars.mSkillChantCameraClip, num, self.mSkillVars.mChantCameraID);
            Label_00B9:
                if (num >= 1f)
                {
                    goto Label_00C5;
                }
                return;
            Label_00C5:
                if ((self.mSkillVars.mChantEffect != null) == null)
                {
                    goto Label_013A;
                }
                if ((self.mSkillVars.mChantEffect.GetComponentInChildren<ParticleSystem>() != null) == null)
                {
                    goto Label_011E;
                }
                GameUtility.StopEmitters(self.mSkillVars.mChantEffect);
                self.mSkillVars.mChantEffect.AddComponent<OneShotParticle>();
                goto Label_012E;
            Label_011E:
                Object.DestroyImmediate(self.mSkillVars.mChantEffect);
            Label_012E:
                self.mSkillVars.mChantEffect = null;
            Label_013A:
                if (self.mSkillVars.Skill.effect_type != 0x12)
                {
                    goto Label_0158;
                }
                self.GotoState<TacticsUnitController.State_ChangeGrid>();
                return;
            Label_0158:
                if (self.mSkillVars.Skill.effect_type != 0x16)
                {
                    goto Label_0176;
                }
                self.GotoState<TacticsUnitController.State_Throw>();
                return;
            Label_0176:
                self.GotoState<TacticsUnitController.State_Skill>();
                if (self.mSkillVars.Skill.IsTransformSkill() == null)
                {
                    goto Label_01BE;
                }
                if (self.mSkillVars.Targets.Count == null)
                {
                    goto Label_01BE;
                }
                controller = self.mSkillVars.Targets[0];
                controller.PlayAfterTransform();
            Label_01BE:
                return;
            }
        }

        private class State_SkillEnd : TacticsUnitController.State
        {
            public State_SkillEnd()
            {
                base..ctor();
                return;
            }

            public override void Begin(TacticsUnitController self)
            {
                if ((self.FindAnimation("B_BS") != null) == null)
                {
                    goto Label_002C;
                }
                self.PlayAnimation("B_BS", 0, 0.1f, 0f);
            Label_002C:
                return;
            }

            public override void Update(TacticsUnitController self)
            {
                if (self.GetRemainingTime("B_BS") > 0f)
                {
                    goto Label_0021;
                }
                self.SkillEffectSelf();
                self.FinishSkill();
            Label_0021:
                return;
            }
        }

        private class State_Step : TacticsUnitController.State
        {
            private float mLandTime;

            public State_Step()
            {
                base..ctor();
                return;
            }

            public override unsafe void Begin(TacticsUnitController self)
            {
                AnimDef def;
                int num;
                def = self.GetAnimation("STEP");
                this.mLandTime = def.Length;
                num = 0;
                goto Label_004D;
            Label_001F:
                if ((def.events[num] as Marker) == null)
                {
                    goto Label_0049;
                }
                this.mLandTime = def.events[num].End;
                goto Label_005B;
            Label_0049:
                num += 1;
            Label_004D:
                if (num < ((int) def.events.Length))
                {
                    goto Label_001F;
                }
            Label_005B:
                self.PlayAnimation("STEP", 0);
                self.SetSpeed("STEP", &GameSettings.Instance.Quest.GridSnapSpeed);
                return;
            }

            public override void Update(TacticsUnitController self)
            {
                float num;
                float num2;
                float num3;
                Vector3 vector;
                if (self.mCancelAction == null)
                {
                    goto Label_001E;
                }
                self.mCancelAction = 0;
                self.PlayIdle(0f);
                return;
            Label_001E:
                num = self.GetRemainingTime("STEP");
                num3 = Mathf.Clamp01((self.GetAnimation("STEP").Length - num) / this.mLandTime);
                vector = Vector3.Lerp(self.mStepStart, self.mStepEnd, num3);
                self.get_transform().set_position(vector);
                if (num > 0f)
                {
                    goto Label_0081;
                }
                self.PlayIdle(0.2f);
                return;
            Label_0081:
                return;
            }
        }

        private class State_StepNoAnimation : TacticsUnitController.State
        {
            public State_StepNoAnimation()
            {
                base..ctor();
                return;
            }

            public override unsafe void Update(TacticsUnitController self)
            {
                Vector3 vector;
                if (self.mCancelAction == null)
                {
                    goto Label_001E;
                }
                self.mCancelAction = 0;
                self.PlayIdle(0f);
                return;
            Label_001E:
                self.mStepStart = Vector3.Lerp(self.mStepStart, self.mStepEnd, Time.get_deltaTime() * 10f);
                vector = self.mStepStart - self.mStepEnd;
                if (&vector.get_magnitude() >= 0.01f)
                {
                    goto Label_0080;
                }
                self.get_transform().set_position(self.mStepEnd);
                self.PlayIdle(0.1f);
                return;
            Label_0080:
                self.get_transform().set_position(self.mStepStart);
                return;
            }
        }

        private class State_Taken : TacticsUnitController.State
        {
            public State_Taken()
            {
                base..ctor();
                return;
            }

            public override void Begin(TacticsUnitController self)
            {
                self.PlayAnimation("PICK", 0);
                return;
            }
        }

        private class State_Throw : TacticsUnitController.State
        {
            private const float START_WAIT_TIME = 0.1f;
            private const float TURN_WAIT_TIME = 0.1f;
            private const float ACC_WAIT_TIME = 0.3f;
            private const float FINISH_WAIT_TIME = 0.2f;
            private SceneBattle mSceneBattle;
            private TacticsUnitController mTargetTuc;

            public State_Throw()
            {
                base..ctor();
                return;
            }

            public override void Begin(TacticsUnitController self)
            {
                this.mSceneBattle = SceneBattle.Instance;
                if (self.mSkillVars.Targets.Count <= 0)
                {
                    goto Label_0038;
                }
                this.mTargetTuc = self.mSkillVars.Targets[0];
            Label_0038:
                if (this.mSceneBattle == null)
                {
                    goto Label_0058;
                }
                if (this.mTargetTuc != null)
                {
                    goto Label_005F;
                }
            Label_0058:
                self.GotoState<TacticsUnitController.State_SkillEnd>();
                return;
            Label_005F:
                self.LoadUnitAnimationAsync("B_TOSS_LIFT", "cmn_toss_lift0", 0, 0);
                self.LoadUnitAnimationAsync("B_TOSS_THROW", "cmn_toss_throw0", 0, 0);
                self.StartCoroutine(this.execPerformance(self));
                return;
            }

            [DebuggerHidden]
            private IEnumerator execPerformance(TacticsUnitController self)
            {
                <execPerformance>c__Iterator5A iteratora;
                iteratora = new <execPerformance>c__Iterator5A();
                iteratora.self = self;
                iteratora.<$>self = self;
                iteratora.<>f__this = this;
                return iteratora;
            }

            [DebuggerHidden]
            private IEnumerator lerpBound(TacticsUnitController target)
            {
                float num;
                float num2;
                <lerpBound>c__Iterator5E iteratore;
                iteratore = new <lerpBound>c__Iterator5E();
                iteratore.target = target;
                iteratore.<$>target = target;
                return iteratore;
            }

            [DebuggerHidden]
            private IEnumerator lerpPickUp(TacticsUnitController self, TacticsUnitController target)
            {
                float num;
                <lerpPickUp>c__Iterator5C iteratorc;
                iteratorc = new <lerpPickUp>c__Iterator5C();
                iteratorc.target = target;
                iteratorc.self = self;
                iteratorc.<$>target = target;
                iteratorc.<$>self = self;
                return iteratorc;
            }

            [DebuggerHidden]
            private IEnumerator lerpThrow(TacticsUnitController target, Vector3 target_pos)
            {
                float num;
                float num2;
                float num3;
                <lerpThrow>c__Iterator5D iteratord;
                iteratord = new <lerpThrow>c__Iterator5D();
                iteratord.target = target;
                iteratord.target_pos = target_pos;
                iteratord.<$>target = target;
                iteratord.<$>target_pos = target_pos;
                return iteratord;
            }

            [DebuggerHidden]
            private IEnumerator lerpTurn(TacticsUnitController target, Vector3 target_pos)
            {
                float num;
                <lerpTurn>c__Iterator5B iteratorb;
                iteratorb = new <lerpTurn>c__Iterator5B();
                iteratorb.target = target;
                iteratorb.target_pos = target_pos;
                iteratorb.<$>target = target;
                iteratorb.<$>target_pos = target_pos;
                return iteratorb;
            }

            [CompilerGenerated]
            private sealed class <execPerformance>c__Iterator5A : IEnumerator, IDisposable, IEnumerator<object>
            {
                internal TacticsUnitController self;
                internal Vector3 <target_pos>__0;
                internal int $PC;
                internal object $current;
                internal TacticsUnitController <$>self;
                internal TacticsUnitController.State_Throw <>f__this;

                public <execPerformance>c__Iterator5A()
                {
                    base..ctor();
                    return;
                }

                [DebuggerHidden]
                public void Dispose()
                {
                    this.$PC = -1;
                    return;
                }

                public bool MoveNext()
                {
                    uint num;
                    bool flag;
                    num = this.$PC;
                    this.$PC = -1;
                    switch (num)
                    {
                        case 0:
                            goto Label_0055;

                        case 1:
                            goto Label_0071;

                        case 2:
                            goto Label_0089;

                        case 3:
                            goto Label_00B1;

                        case 4:
                            goto Label_010A;

                        case 5:
                            goto Label_0126;

                        case 6:
                            goto Label_016B;

                        case 7:
                            goto Label_0218;

                        case 8:
                            goto Label_0244;

                        case 9:
                            goto Label_0284;

                        case 10:
                            goto Label_02A1;

                        case 11:
                            goto Label_02BE;

                        case 12:
                            goto Label_02FE;

                        case 13:
                            goto Label_03DE;

                        case 14:
                            goto Label_0452;
                    }
                    goto Label_0464;
                Label_0055:
                    this.$current = new WaitForSeconds(0.1f);
                    this.$PC = 1;
                    goto Label_0466;
                Label_0071:
                    goto Label_0089;
                Label_0076:
                    this.$current = null;
                    this.$PC = 2;
                    goto Label_0466;
                Label_0089:
                    if (this.self.IsLoading != null)
                    {
                        goto Label_0076;
                    }
                    goto Label_00B1;
                Label_009E:
                    this.$current = null;
                    this.$PC = 3;
                    goto Label_0466;
                Label_00B1:
                    if (this.<>f__this.mTargetTuc.IsLoading != null)
                    {
                        goto Label_009E;
                    }
                    this.self.AutoUpdateRotation = 0;
                    this.$current = this.<>f__this.lerpTurn(this.self, this.<>f__this.mTargetTuc.get_transform().get_position());
                    this.$PC = 4;
                    goto Label_0466;
                Label_010A:
                    this.$current = new WaitForSeconds(0.1f);
                    this.$PC = 5;
                    goto Label_0466;
                Label_0126:
                    this.self.SetPrimaryEquipmentsVisible(0);
                    this.self.SetSecondaryEquipmentsVisible(0);
                    this.self.PlayAnimation("B_TOSS_LIFT", 0);
                    this.$current = new WaitForSeconds(0.2f);
                    this.$PC = 6;
                    goto Label_0466;
                Label_016B:
                    if (this.<>f__this.mTargetTuc.Unit.IsBreakObj != null)
                    {
                        goto Label_01A5;
                    }
                    this.<>f__this.mTargetTuc.PlayAnimation("B_DMG0", 0, 0f, 0.2f);
                Label_01A5:
                    if (this.<>f__this.mTargetTuc.Unit.IsBreakObj != null)
                    {
                        goto Label_01D9;
                    }
                    this.<>f__this.mTargetTuc.SetSpeed("B_DMG0", 0f);
                Label_01D9:
                    this.<>f__this.mTargetTuc.mCollideGround = 0;
                    this.$current = this.<>f__this.lerpPickUp(this.self, this.<>f__this.mTargetTuc);
                    this.$PC = 7;
                    goto Label_0466;
                Label_0218:
                    this.<>f__this.mSceneBattle.OnGimmickUpdate();
                    this.$current = new WaitForSeconds(0.1f);
                    this.$PC = 8;
                    goto Label_0466;
                Label_0244:
                    this.<target_pos>__0 = this.self.mSkillVars.mTargetPosition;
                    this.$current = this.<>f__this.lerpTurn(this.self, this.<target_pos>__0);
                    this.$PC = 9;
                    goto Label_0466;
                Label_0284:
                    this.$current = new WaitForSeconds(0.1f);
                    this.$PC = 10;
                    goto Label_0466;
                Label_02A1:
                    this.$current = new WaitForSeconds(0.3f);
                    this.$PC = 11;
                    goto Label_0466;
                Label_02BE:
                    this.self.PlayAnimation("B_TOSS_THROW", 0);
                    this.$current = this.<>f__this.lerpThrow(this.<>f__this.mTargetTuc, this.<target_pos>__0);
                    this.$PC = 12;
                    goto Label_0466;
                Label_02FE:
                    this.self.PlayAnimation("IDLE", 1);
                    this.self.SetPrimaryEquipmentsVisible(1);
                    this.self.SetSecondaryEquipmentsVisible(1);
                    if (this.self.mSkillVars == null)
                    {
                        goto Label_03B5;
                    }
                    if ((this.self.mSkillVars.mSkillEffect != null) == null)
                    {
                        goto Label_03B5;
                    }
                    if (this.self.mSkillVars.mSkillEffect.ExplosionEffects == null)
                    {
                        goto Label_03B5;
                    }
                    if (((int) this.self.mSkillVars.mSkillEffect.ExplosionEffects.Length) == null)
                    {
                        goto Label_03B5;
                    }
                    this.self.createEffect(this.<>f__this.mTargetTuc, this.self.mSkillVars.mSkillEffect.ExplosionEffects[0]);
                Label_03B5:
                    this.$current = this.<>f__this.lerpBound(this.<>f__this.mTargetTuc);
                    this.$PC = 13;
                    goto Label_0466;
                Label_03DE:
                    if (this.<>f__this.mTargetTuc.Unit.IsBreakObj != null)
                    {
                        goto Label_040E;
                    }
                    this.<>f__this.mTargetTuc.PlayAnimation("IDLE", 1);
                Label_040E:
                    this.<>f__this.mTargetTuc.mCollideGround = 1;
                    this.self.HideGimmickForTargetGrid(this.<>f__this.mTargetTuc);
                    this.$current = new WaitForSeconds(0.2f);
                    this.$PC = 14;
                    goto Label_0466;
                Label_0452:
                    this.self.GotoState<TacticsUnitController.State_SkillEnd>();
                    this.$PC = -1;
                Label_0464:
                    return 0;
                Label_0466:
                    return 1;
                    return flag;
                }

                [DebuggerHidden]
                public void Reset()
                {
                    throw new NotSupportedException();
                }

                object IEnumerator<object>.Current
                {
                    [DebuggerHidden]
                    get
                    {
                        return this.$current;
                    }
                }

                object IEnumerator.Current
                {
                    [DebuggerHidden]
                    get
                    {
                        return this.$current;
                    }
                }
            }

            [CompilerGenerated]
            private sealed class <lerpBound>c__Iterator5E : IEnumerator, IDisposable, IEnumerator<object>
            {
                internal TacticsUnitController target;
                internal Vector3 <base_pos>__0;
                internal float <passed_time>__1;
                internal float <rate>__2;
                internal Vector3 <pos>__3;
                internal int $PC;
                internal object $current;
                internal TacticsUnitController <$>target;

                public <lerpBound>c__Iterator5E()
                {
                    base..ctor();
                    return;
                }

                [DebuggerHidden]
                public void Dispose()
                {
                    this.$PC = -1;
                    return;
                }

                public unsafe bool MoveNext()
                {
                    uint num;
                    bool flag;
                    num = this.$PC;
                    this.$PC = -1;
                    switch (num)
                    {
                        case 0:
                            goto Label_0021;

                        case 1:
                            goto Label_00DE;
                    }
                    goto Label_0100;
                Label_0021:
                    this.<base_pos>__0 = this.target.get_transform().get_position();
                    this.<passed_time>__1 = 0f;
                Label_0042:
                    this.<passed_time>__1 += Time.get_deltaTime();
                    this.<rate>__2 = Mathf.Clamp01(this.<passed_time>__1 / 0.15f);
                    this.<pos>__3 = this.<base_pos>__0;
                    &this.<pos>__3.y += Mathf.Sin(this.<rate>__2 * 3.141593f) * 0.4f;
                    this.target.get_transform().set_position(this.<pos>__3);
                    if (this.<rate>__2 < 1f)
                    {
                        goto Label_00CB;
                    }
                    goto Label_00E3;
                Label_00CB:
                    this.$current = null;
                    this.$PC = 1;
                    goto Label_0102;
                Label_00DE:
                    goto Label_0042;
                Label_00E3:
                    this.target.get_transform().set_position(this.<base_pos>__0);
                    this.$PC = -1;
                Label_0100:
                    return 0;
                Label_0102:
                    return 1;
                    return flag;
                }

                [DebuggerHidden]
                public void Reset()
                {
                    throw new NotSupportedException();
                }

                object IEnumerator<object>.Current
                {
                    [DebuggerHidden]
                    get
                    {
                        return this.$current;
                    }
                }

                object IEnumerator.Current
                {
                    [DebuggerHidden]
                    get
                    {
                        return this.$current;
                    }
                }
            }

            [CompilerGenerated]
            private sealed class <lerpPickUp>c__Iterator5C : IEnumerator, IDisposable, IEnumerator<object>
            {
                internal TacticsUnitController target;
                internal Vector3 <stt_pos>__0;
                internal TacticsUnitController self;
                internal Vector3 <end_pos>__1;
                internal float <passed_time>__2;
                internal float <rate>__3;
                internal Vector3 <pos>__4;
                internal Vector3 <pos_y>__5;
                internal int $PC;
                internal object $current;
                internal TacticsUnitController <$>target;
                internal TacticsUnitController <$>self;

                public <lerpPickUp>c__Iterator5C()
                {
                    base..ctor();
                    return;
                }

                [DebuggerHidden]
                public void Dispose()
                {
                    this.$PC = -1;
                    return;
                }

                public unsafe bool MoveNext()
                {
                    uint num;
                    bool flag;
                    num = this.$PC;
                    this.$PC = -1;
                    switch (num)
                    {
                        case 0:
                            goto Label_0021;

                        case 1:
                            goto Label_0139;
                    }
                    goto Label_0145;
                Label_0021:
                    this.<stt_pos>__0 = this.target.get_transform().get_position();
                    this.<end_pos>__1 = this.self.get_transform().get_position();
                    &this.<end_pos>__1.y += this.self.Height;
                    this.<passed_time>__2 = 0f;
                Label_0075:
                    this.<passed_time>__2 += Time.get_deltaTime();
                    this.<rate>__3 = Mathf.Clamp01(this.<passed_time>__2 / 0.2f);
                    this.<pos>__4 = Vector3.Lerp(this.<stt_pos>__0, this.<end_pos>__1, this.<rate>__3);
                    this.<pos_y>__5 = Vector3.Lerp(this.<stt_pos>__0, this.<end_pos>__1, this.<rate>__3 * (2f - this.<rate>__3));
                    &this.<pos>__4.y = &this.<pos_y>__5.y;
                    this.target.get_transform().set_position(this.<pos>__4);
                    if (this.<rate>__3 < 1f)
                    {
                        goto Label_0126;
                    }
                    goto Label_013E;
                Label_0126:
                    this.$current = null;
                    this.$PC = 1;
                    goto Label_0147;
                Label_0139:
                    goto Label_0075;
                Label_013E:
                    this.$PC = -1;
                Label_0145:
                    return 0;
                Label_0147:
                    return 1;
                    return flag;
                }

                [DebuggerHidden]
                public void Reset()
                {
                    throw new NotSupportedException();
                }

                object IEnumerator<object>.Current
                {
                    [DebuggerHidden]
                    get
                    {
                        return this.$current;
                    }
                }

                object IEnumerator.Current
                {
                    [DebuggerHidden]
                    get
                    {
                        return this.$current;
                    }
                }
            }

            [CompilerGenerated]
            private sealed class <lerpThrow>c__Iterator5D : IEnumerator, IDisposable, IEnumerator<object>
            {
                internal TacticsUnitController target;
                internal Vector3 <stt_pos>__0;
                internal Vector3 target_pos;
                internal Vector3 <end_pos>__1;
                internal float <dist>__2;
                internal float <time>__3;
                internal float <passed_time>__4;
                internal float <rate>__5;
                internal Vector3 <pos>__6;
                internal int $PC;
                internal object $current;
                internal TacticsUnitController <$>target;
                internal Vector3 <$>target_pos;

                public <lerpThrow>c__Iterator5D()
                {
                    base..ctor();
                    return;
                }

                [DebuggerHidden]
                public void Dispose()
                {
                    this.$PC = -1;
                    return;
                }

                public unsafe bool MoveNext()
                {
                    uint num;
                    bool flag;
                    num = this.$PC;
                    this.$PC = -1;
                    switch (num)
                    {
                        case 0:
                            goto Label_0021;

                        case 1:
                            goto Label_012B;
                    }
                    goto Label_014D;
                Label_0021:
                    this.<stt_pos>__0 = this.target.get_transform().get_position();
                    this.<end_pos>__1 = this.target_pos;
                    this.<dist>__2 = GameUtility.CalcDistance2D(this.<end_pos>__1, this.<stt_pos>__0);
                    this.<time>__3 = 0.1f + (0.1f * this.<dist>__2);
                    this.<passed_time>__4 = 0f;
                Label_007D:
                    this.<passed_time>__4 += Time.get_deltaTime();
                    this.<rate>__5 = Mathf.Clamp01(this.<passed_time>__4 / this.<time>__3);
                    this.<pos>__6 = Vector3.Lerp(this.<stt_pos>__0, this.<end_pos>__1, this.<rate>__5);
                    &this.<pos>__6.y += Mathf.Sin(this.<rate>__5 * 3.141593f) * 1.5f;
                    this.target.get_transform().set_position(this.<pos>__6);
                    if (this.<rate>__5 < 1f)
                    {
                        goto Label_0118;
                    }
                    goto Label_0130;
                Label_0118:
                    this.$current = null;
                    this.$PC = 1;
                    goto Label_014F;
                Label_012B:
                    goto Label_007D;
                Label_0130:
                    this.target.get_transform().set_position(this.target_pos);
                    this.$PC = -1;
                Label_014D:
                    return 0;
                Label_014F:
                    return 1;
                    return flag;
                }

                [DebuggerHidden]
                public void Reset()
                {
                    throw new NotSupportedException();
                }

                object IEnumerator<object>.Current
                {
                    [DebuggerHidden]
                    get
                    {
                        return this.$current;
                    }
                }

                object IEnumerator.Current
                {
                    [DebuggerHidden]
                    get
                    {
                        return this.$current;
                    }
                }
            }

            [CompilerGenerated]
            private sealed class <lerpTurn>c__Iterator5B : IEnumerator, IDisposable, IEnumerator<object>
            {
                internal TacticsUnitController target;
                internal Quaternion <stt_rot>__0;
                internal Vector3 target_pos;
                internal Vector3 <dir>__1;
                internal Quaternion <end_rot>__2;
                internal float <passed_time>__3;
                internal float <rate>__4;
                internal int $PC;
                internal object $current;
                internal TacticsUnitController <$>target;
                internal Vector3 <$>target_pos;

                public <lerpTurn>c__Iterator5B()
                {
                    base..ctor();
                    return;
                }

                [DebuggerHidden]
                public void Dispose()
                {
                    this.$PC = -1;
                    return;
                }

                public unsafe bool MoveNext()
                {
                    uint num;
                    bool flag;
                    num = this.$PC;
                    this.$PC = -1;
                    switch (num)
                    {
                        case 0:
                            goto Label_0021;

                        case 1:
                            goto Label_00FC;
                    }
                    goto Label_0108;
                Label_0021:
                    this.<stt_rot>__0 = this.target.get_transform().get_rotation();
                    this.<dir>__1 = this.target_pos - this.target.get_transform().get_position();
                    &this.<dir>__1.y = 0f;
                    this.<end_rot>__2 = Quaternion.LookRotation(this.<dir>__1);
                    this.<passed_time>__3 = 0f;
                Label_0084:
                    this.<passed_time>__3 += Time.get_deltaTime();
                    this.<rate>__4 = Mathf.Clamp01(this.<passed_time>__3 / 0.1f);
                    this.target.get_transform().set_rotation(Quaternion.Slerp(this.<stt_rot>__0, this.<end_rot>__2, this.<rate>__4));
                    if (this.<rate>__4 < 1f)
                    {
                        goto Label_00E9;
                    }
                    goto Label_0101;
                Label_00E9:
                    this.$current = null;
                    this.$PC = 1;
                    goto Label_010A;
                Label_00FC:
                    goto Label_0084;
                Label_0101:
                    this.$PC = -1;
                Label_0108:
                    return 0;
                Label_010A:
                    return 1;
                    return flag;
                }

                [DebuggerHidden]
                public void Reset()
                {
                    throw new NotSupportedException();
                }

                object IEnumerator<object>.Current
                {
                    [DebuggerHidden]
                    get
                    {
                        return this.$current;
                    }
                }

                object IEnumerator.Current
                {
                    [DebuggerHidden]
                    get
                    {
                        return this.$current;
                    }
                }
            }
        }

        private class State_WaitResources : TacticsUnitController.State
        {
            public State_WaitResources()
            {
                base..ctor();
                return;
            }

            public override void End(TacticsUnitController self)
            {
                if (self.mUnit == null)
                {
                    goto Label_001C;
                }
                if (self.mUnit.IsDead == null)
                {
                    goto Label_001C;
                }
                return;
            Label_001C:
                if (self.KeepUnitHidden != null)
                {
                    goto Label_002E;
                }
                self.SetVisible(1);
            Label_002E:
                return;
            }

            public override void Update(TacticsUnitController self)
            {
                if (self.IsLoading == null)
                {
                    goto Label_000C;
                }
                return;
            Label_000C:
                self.mLoadedPartially = 1;
                if (self.mCharacterData.Movable == null)
                {
                    goto Label_007D;
                }
                self.LoadUnitAnimationAsync("RUN", TacticsUnitController.ANIM_RUN_FIELD, 1, 0);
                self.LoadUnitAnimationAsync("STEP", TacticsUnitController.ANIM_STEP, 0, 0);
                self.LoadUnitAnimationAsync("FLLP", TacticsUnitController.ANIM_FALL_LOOP, 0, 0);
                self.LoadUnitAnimationAsync("FLEN", TacticsUnitController.ANIM_FALL_END, 0, 0);
                self.LoadUnitAnimationAsync("CLMB", TacticsUnitController.ANIM_CLIMBUP, 0, 0);
            Label_007D:
                self.PlayIdle(0f);
                return;
            }
        }
    }
}

