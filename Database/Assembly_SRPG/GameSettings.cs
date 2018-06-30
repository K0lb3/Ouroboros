namespace SRPG
{
    using System;
    using System.Runtime.InteropServices;
    using UnityEngine;

    [ExecuteInEditMode]
    public class GameSettings : ScriptableObject
    {
        public const float ListRefreshFadeTime = 0.3f;
        private static GameSettings mInstance;
        public int Network_BGDLChunkSize;
        public string[] Tutorial_Steps;
        public string[] Tutorial_Flags;
        [Tooltip("カメラの上下の傾きの角度")]
        public float GameCamera_AngleX;
        [Tooltip("クエストでのカメラの最低回転角度")]
        public float GameCamera_YawMin;
        [Tooltip("クエストでのカメラの最大回転角度")]
        public float GameCamera_YawMax;
        [Tooltip("クエストカメラで限界を超えて回転できる角度")]
        public float GameCamera_YawSoftLimit;
        [Tooltip("カメラでユニットを注視する際の高さオフセット")]
        public float GameCamera_UnitHeightOffset;
        [Tooltip("カメラのデフォルトの距離")]
        public float GameCamera_DefaultDistance;
        [Tooltip("引いたカメラの距離")]
        public float GameCamera_MoreFarDistance;
        [Tooltip("イベントカメラのデフォルトの距離")]
        public float GameCamera_EventCameraDistance;
        [Tooltip("カメラのマップ確認時の距離")]
        public float GameCamera_MapDistance;
        [Tooltip("カメラの最大距離")]
        public float GameCamera_MaxDistance;
        [Range(1f, 80f)]
        public float GameCamera_TacticsSceneFOV;
        [Range(1f, 80f)]
        public float GameCamera_BattleSceneFOV;
        [Tooltip("マップ上でスキルを使用する際のカメラの距離")]
        public float GameCamera_SkillCameraDistance;
        [Tooltip("敵の行動前の待機時間　1/MoveWait秒")]
        public float AiUnit_MoveWait;
        [Tooltip("敵の行動前の待機時間　1/SkillWait秒")]
        public float AiUnit_SkillWait;
        [Tooltip("キャラクターの最大の発光強度")]
        public float Unit_MaxGlowStrength;
        [Tooltip("小ジャンプの閾値")]
        public float Unit_StepAnimationThreshold;
        [Tooltip("段差登りの閾値")]
        public float Unit_JumpAnimationThreshold;
        [Tooltip("落下の閾値")]
        public float Unit_FallAnimationThreshold;
        [Tooltip("段差登り時の前方方向へのカーブ")]
        public AnimationCurve Unit_JumpZCurve;
        [Tooltip("段差登り時の上方向へのカーブ")]
        public AnimationCurve Unit_JumpYCurve;
        [Tooltip("段差登り時の最小時間")]
        public float Unit_JumpMinTime;
        [Tooltip("段差登り時の高さ1毎の追加時間")]
        public float Unit_JumpTimePerHeight;
        [Tooltip("段差降り時の最小時間")]
        public float Unit_FallMinTime;
        [Tooltip("段差降り時の高さ1毎の追加時間")]
        public float Unit_FallTimePerHeight;
        [Tooltip("キャラクターを揺らす幅")]
        public float ShakeUnit_Offset;
        [Tooltip("キャラクターを揺らす回数")]
        public int ShakeUnit_MaxCount;
        [Tooltip("キャラクターの最大の発光強度")]
        public int Gem_DrainCount_FrontHit;
        public int Gem_DrainCount_SideHit;
        public int Gem_DrainCount_BackHit;
        public int Gem_DrainCount_Randomness;
        public Color32 Buff_TextTopColor;
        public Color32 Buff_TextBottomColor;
        public Color32 Debuff_TextTopColor;
        public Color32 Debuff_TextBottomColor;
        public Color32 FailCondition_TextTopColor;
        public Color32 FailCondition_TextBottomColor;
        [Tooltip("被ヒット時のポップアップの発生間隔")]
        public float HitPopup_PopDeley;
        [Tooltip("被ヒット時のポップアップの表示間隔")]
        public float HitPopup_YSpacing;
        [Tooltip("クリティカル演出で発生するフラッシュ効果の強さ")]
        public float CriticalHit_FlashStrength;
        [Tooltip("クリティカル演出で発生するフラッシュ効果の表示時間")]
        public float CriticalHit_FlashDuration;
        [Tooltip("クリティカル演出で発生するカメラシェイクの時間")]
        public float CriticalHit_ShakeDuration;
        [Tooltip("クリティカル演出で発生するカメラシェイクの横揺れ回数")]
        public float CriticalHit_ShakeFrequencyX;
        [Tooltip("クリティカル演出で発生するカメラシェイクの縦揺れ回数")]
        public float CriticalHit_ShakeFrequencyY;
        [Tooltip("クリティカル演出で発生するカメラシェイクの横揺れの強さ")]
        public float CriticalHit_ShakeAmplitudeX;
        [Tooltip("クリティカル演出で発生するカメラシェイクの縦揺れの強さ")]
        public float CriticalHit_ShakeAmplitudeY;
        [Tooltip("ブルームのぼかし強度")]
        public float PostEffect_BloomBlurStrength;
        [Tooltip("ブルームの最大強度")]
        public float PostEffect_BloomMaxStrength;
        public Color Character_DefaultDirectLitColor;
        public Color Character_DefaultIndirectLitColor;
        public Color32 Character_PlayerGlowColor;
        public Color32 Character_EnemyGlowColor;
        public Sprite[] Elements_IconSmall;
        public UnitSortIcon[] UnitSort_Modes;
        public Sprite[] UnitIcon_Frames;
        public Sprite[] UnitIcon_Rarity;
        public Sprite ArtifactIcon_Weapon;
        public Sprite ArtifactIcon_Armor;
        public Sprite ArtifactIcon_Misc;
        public Sprite[] ArtifactIcon_Frames;
        public Sprite[] ArtifactIcon_Rarity;
        public Sprite[] ArtifactIcon_RarityBG;
        public Sprite[] ConceptCardIcon_Frames;
        public Sprite[] ConceptCardIcon_Rarity;
        [StringIsResourcePath(typeof(GameObject))]
        public string ConceptCard_GetUnit;
        public Color32 Gauge_HP_Base;
        public Color32 Gauge_HP_Damage;
        public Color32 Gauge_HP_Heal;
        public Color32 Gauge_PlayerHP_Base;
        public Color32 Gauge_PlayerHP_Damage;
        public Color32 Gauge_PlayerHP_Heal;
        public Color32 Gauge_EnemyHP_Base;
        public Color32 Gauge_EnemyHP_Damage;
        public Color32 Gauge_EnemyHP_Heal;
        public GameObject Dialog_BuyCoin;
        [StringIsResourcePath(typeof(GameObject))]
        public string Dialog_AbilityDetail;
        public string CharacterQuest_Unlock;
        public Canvas Canvas2D;
        public Transform EnemyPosRig;
        public Transform CameraPosRig;
        public DialogSettings Dialogs;
        public CameraSettings Cameras;
        public ColorSettings Colors;
        public QuestSettings Quest;
        public ItemIconSettings ItemIcons;
        public Sprite[] ItemPriceIconFrames;
        public char[] ValidInputChars;
        public float QuestLoad_WaitSecond;
        public string QuestLoad_OkyakusamaCode;
        [SerializeField, Range(1f, 100f)]
        public int HoldMargin;
        public HoldCountSettings[] HoldCount;
        [StringIsResourcePath(typeof(GameObject))]
        public string UnitGet_EffectTemplate;
        public string CharacterQuestSection;
        public FlowNode_WebView.URL_Mode WebHelp_URLMode;
        [StringIsResourcePath(typeof(GameObject))]
        public string WebHelp_PrefabPath;
        [Tooltip("バトルリザルト背景のプレハブ名"), StringIsResourcePath(typeof(GameObject))]
        public string BattleResultBg_ResourcePath;
        [Tooltip("バトルリザルト背景に現在のバトルBGを設定する")]
        public bool BattleResultBg_UseBattleBG;
        [Tooltip("バトルリザルト背景の表示待ち時間")]
        public float BattleResultBg_WaitTime;

        public unsafe GameSettings()
        {
            Keyframe[] keyframeArray2;
            Keyframe[] keyframeArray1;
            DialogSettings settings;
            CameraSettings settings2;
            ColorSettings settings3;
            QuestSettings settings4;
            ItemIconSettings settings5;
            this.Network_BGDLChunkSize = 0x20000;
            this.Tutorial_Steps = new string[0];
            this.Tutorial_Flags = new string[0];
            this.GameCamera_AngleX = -45f;
            this.GameCamera_YawMin = 45f;
            this.GameCamera_YawMax = 145f;
            this.GameCamera_YawSoftLimit = 30f;
            this.GameCamera_UnitHeightOffset = 1f;
            this.GameCamera_DefaultDistance = 10f;
            this.GameCamera_MoreFarDistance = 24f;
            this.GameCamera_EventCameraDistance = 10f;
            this.GameCamera_MapDistance = 10f;
            this.GameCamera_MaxDistance = 30f;
            this.GameCamera_TacticsSceneFOV = 40f;
            this.GameCamera_BattleSceneFOV = 15f;
            this.GameCamera_SkillCameraDistance = 5f;
            this.AiUnit_MoveWait = 1f;
            this.AiUnit_SkillWait = 1f;
            this.Unit_MaxGlowStrength = 0.3f;
            this.Unit_StepAnimationThreshold = 0.2f;
            this.Unit_JumpAnimationThreshold = 0.5f;
            this.Unit_FallAnimationThreshold = -0.4f;
            keyframeArray1 = new Keyframe[2];
            *(&(keyframeArray1[0])) = new Keyframe(0f, 0f);
            *(&(keyframeArray1[1])) = new Keyframe(1f, 1f);
            this.Unit_JumpZCurve = new AnimationCurve(keyframeArray1);
            keyframeArray2 = new Keyframe[3];
            *(&(keyframeArray2[0])) = new Keyframe(0f, 0f);
            *(&(keyframeArray2[1])) = new Keyframe(0.5f, 1f);
            *(&(keyframeArray2[2])) = new Keyframe(1f, 0f);
            this.Unit_JumpYCurve = new AnimationCurve(keyframeArray2);
            this.Unit_JumpMinTime = 0.5f;
            this.Unit_JumpTimePerHeight = 0.2f;
            this.Unit_FallMinTime = 0.5f;
            this.Unit_FallTimePerHeight = 0.2f;
            this.ShakeUnit_Offset = 0.0125f;
            this.ShakeUnit_MaxCount = 8;
            this.Gem_DrainCount_FrontHit = 4;
            this.Gem_DrainCount_SideHit = 9;
            this.Gem_DrainCount_BackHit = 15;
            this.Gem_DrainCount_Randomness = 3;
            this.Buff_TextTopColor = new Color32(0, 0xff, 0, 0xff);
            this.Buff_TextBottomColor = new Color32(0, 0xff, 0, 0xff);
            this.Debuff_TextTopColor = new Color32(0xff, 0, 0, 0xff);
            this.Debuff_TextBottomColor = new Color32(0xff, 0, 0, 0xff);
            this.FailCondition_TextTopColor = new Color32(0, 0xff, 0, 0xff);
            this.FailCondition_TextBottomColor = new Color32(0, 0xff, 0, 0xff);
            this.HitPopup_PopDeley = 0.4f;
            this.CriticalHit_FlashStrength = 0.75f;
            this.CriticalHit_FlashDuration = 0.3f;
            this.CriticalHit_ShakeDuration = 0.3f;
            this.CriticalHit_ShakeFrequencyX = 10f;
            this.CriticalHit_ShakeFrequencyY = 10f;
            this.CriticalHit_ShakeAmplitudeX = 1f;
            this.CriticalHit_ShakeAmplitudeY = 1f;
            this.PostEffect_BloomBlurStrength = 2f;
            this.PostEffect_BloomMaxStrength = 16f;
            this.Character_DefaultDirectLitColor = new Color(1f, 1f, 1f, 1f);
            this.Character_DefaultIndirectLitColor = new Color(1f, 1f, 1f, 1f);
            this.Character_PlayerGlowColor = new Color32(0, 0x80, 0xff, 0);
            this.Character_EnemyGlowColor = new Color32(0xff, 0, 0, 0);
            this.Elements_IconSmall = new Sprite[0];
            this.UnitIcon_Frames = new Sprite[0];
            this.UnitIcon_Rarity = new Sprite[0];
            this.ArtifactIcon_Frames = new Sprite[0];
            this.ArtifactIcon_Rarity = new Sprite[0];
            this.ArtifactIcon_RarityBG = new Sprite[0];
            this.ConceptCardIcon_Frames = new Sprite[0];
            this.ConceptCardIcon_Rarity = new Sprite[0];
            this.ConceptCard_GetUnit = string.Empty;
            this.Gauge_HP_Base = new Color32(0, 0xff, 0xff, 0xff);
            this.Gauge_HP_Damage = new Color32(0xff, 0, 0, 0xff);
            this.Gauge_HP_Heal = new Color32(0, 0xff, 0, 0xff);
            this.Gauge_PlayerHP_Base = new Color32(0, 0, 0xff, 0xff);
            this.Gauge_PlayerHP_Damage = new Color32(0, 0xff, 0, 0xff);
            this.Gauge_PlayerHP_Heal = new Color32(0xff, 0xff, 0, 0xff);
            this.Gauge_EnemyHP_Base = new Color32(0xff, 0, 0, 0xff);
            this.Gauge_EnemyHP_Damage = new Color32(0xff, 0xff, 0, 0xff);
            this.Gauge_EnemyHP_Heal = new Color32(0, 0xff, 0, 0xff);
            this.Dialogs = new DialogSettings();
            this.Cameras = new CameraSettings();
            this.Colors = new ColorSettings();
            this.Quest = new QuestSettings();
            this.ItemIcons = new ItemIconSettings();
            this.ValidInputChars = new char[0];
            this.QuestLoad_OkyakusamaCode = string.Empty;
            this.HoldCount = new HoldCountSettings[0];
            this.UnitGet_EffectTemplate = string.Empty;
            this.CharacterQuestSection = "WD_CHARA";
            base..ctor();
            return;
        }

        public long CreateTutorialFlagMask(string flagName)
        {
            int num;
            num = 1;
            goto Label_0026;
        Label_0007:
            if ((this.Tutorial_Flags[num] == flagName) == null)
            {
                goto Label_0022;
            }
            return (1L << (num & 0x3f));
        Label_0022:
            num += 1;
        Label_0026:
            if (num < ((int) this.Tutorial_Flags.Length))
            {
                goto Label_0007;
            }
            return 0L;
        }

        public Sprite GetConceptCardFrame(ConceptCardParam param)
        {
            if (param != null)
            {
                goto Label_001D;
            }
            DebugUtility.LogError(string.Format("GameSettings.GetConceptCardFrame => param == null", new object[0]));
            return null;
        Label_001D:
            return this.GetConceptCardFrame(param.rare);
        }

        public Sprite GetConceptCardFrame(int rarity)
        {
            int num;
            if (this.ConceptCardIcon_Frames != null)
            {
                goto Label_0022;
            }
            DebugUtility.LogError(string.Format("GameSettings.GetConceptCardFrame => ConceptCardIcon_Frames == null", new object[0]));
            return null;
        Label_0022:
            num = Mathf.Clamp(rarity, 0, ((int) this.ConceptCardIcon_Frames.Length) - 1);
            return this.ConceptCardIcon_Frames[num];
        }

        public Sprite GetItemFrame(ItemParam itemParam)
        {
            return this.GetItemFrame(itemParam.type, itemParam.rare);
        }

        public unsafe Sprite GetItemFrame(EItemType type, int rare)
        {
            Sprite[] spriteArray;
            int num;
            spriteArray = null;
            if (type == 1)
            {
                goto Label_0011;
            }
            if (type != 11)
            {
                goto Label_0022;
            }
        Label_0011:
            spriteArray = &this.ItemIcons.KakeraFrames;
            goto Label_0047;
        Label_0022:
            if (type != 14)
            {
                goto Label_003B;
            }
            spriteArray = &this.ItemIcons.ArtifactKakeraFrames;
            goto Label_0047;
        Label_003B:
            spriteArray = &this.ItemIcons.NormalFrames;
        Label_0047:
            num = Mathf.Clamp(rare, 0, ((int) spriteArray.Length) - 1);
            if (0 > num)
            {
                goto Label_005F;
            }
            return spriteArray[num];
        Label_005F:
            return null;
        }

        public unsafe Sprite GetUnitSortModeIcon(GameUtility.UnitSortModes mode)
        {
            int num;
            num = 0;
            goto Label_0034;
        Label_0007:
            if (&(this.UnitSort_Modes[num]).Mode != mode)
            {
                goto Label_0030;
            }
            return &(this.UnitSort_Modes[num]).Icon;
        Label_0030:
            num += 1;
        Label_0034:
            if (num < ((int) this.UnitSort_Modes.Length))
            {
                goto Label_0007;
            }
            return null;
        }

        public static GameSettings Instance
        {
            get
            {
                if ((mInstance == null) == null)
                {
                    goto Label_001F;
                }
                mInstance = AssetManager.Load<GameSettings>("GameSettings");
            Label_001F:
                return mInstance;
            }
        }

        [Serializable, StructLayout(LayoutKind.Sequential)]
        public struct CameraSettings
        {
            public Camera OverlayCamera;
        }

        [Serializable, StructLayout(LayoutKind.Sequential)]
        public struct ColorSettings
        {
            public Color Enemy;
            public Color Player;
            public Color Helper;
            public Color DamageDigits;
            public Color HealDigits;
            public Color CriticalDigits;
            public Color WalkableArea;
            public Color StartGrid;
            public Color AttackArea;
            public Color AttackArea2;
            public Color ChargeAreaGrn;
            public Color ChargeAreaRed;
        }

        [Serializable, StructLayout(LayoutKind.Sequential)]
        public struct DialogSettings
        {
            public Win_Btn_DecideCancel_FL_Check_C YesNoDialogWithCheckBox;
            public Win_Btn_DecideCancel_FL_C YesNoDialog;
            public Win_Btn_Decide_Title_Flx YesDialogWithTitle;
            public Win_Btn_YN_Title_Flx YesNoDialogWithTitle;
            public Win_Btn_Decide_Flx YesDialog;
            public Win_SysMessage_Flx SysMsgDialog;
        }

        [Serializable, StructLayout(LayoutKind.Sequential)]
        public struct HoldCountSettings
        {
            public int Count;
            public float UseSpan;
        }

        [Serializable, StructLayout(LayoutKind.Sequential)]
        public struct ItemIconSettings
        {
            public Sprite[] NormalFrames;
            public Sprite[] KakeraFrames;
            public Sprite[] ArtifactKakeraFrames;
        }

        [Serializable, StructLayout(LayoutKind.Sequential)]
        public struct QuestSettings
        {
            public Transform TacticsCamera;
            public Transform MoveCamera;
            public Transform UnitCamera;
            public Transform BattleCamera;
            public Transform RunCamera;
            public AnimationCurve RunCameraInterpRate;
            public float BattleRunSpeed;
            public float MapRunSpeedMin;
            public float MapRunSpeedMax;
            public float MapCharacterScale;
            public float AnimateGridSnapRadius;
            public float GridSnapDelay;
            public float GridSnapSpeed;
            public float MapTransitionSpeed;
            public float DoorEnterTime;
            public float TreasureTime;
            public float ViewingUnitSnapSpeed;
            public float BattleTurnEndWait;
            public float RenkeiEndWait;
            public float WaitAfterUnitPickupGimmick;
            [Description("きりもみ状態での毎秒の回転角度")]
            public float KirimomiRotationRate;
            [Description("ユニット交代時のエフェクト待ち時間")]
            public float UnitChangeEffectWaitTime;
            [Description("オートプレイ時のイベントステップ待ち時間")]
            public float WaitTimeScriptEventForward;
            [Description("ユニット撤退時のエフェクト待ち時間")]
            public float WithdrawUnitEffectWaitTime;
            [Description("壊れるオブジェクトの設置最大許容数")]
            public int BreakObjAllowEntryMax;
            [Description("天候エフェクトの切り替え時間")]
            public float WeatherEffectChangeTime;
            [Description("特殊パネルの演出最大待ち時間")]
            public float TrickEffectWaitMaxTime;
        }

        [Serializable, StructLayout(LayoutKind.Sequential)]
        public struct UnitSortIcon
        {
            public GameUtility.UnitSortModes Mode;
            public Sprite Icon;
        }
    }
}

