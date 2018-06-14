// Decompiled with JetBrains decompiler
// Type: SRPG.GameSettings
// Assembly: Assembly-CSharp, Version=1.2.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9BA76916-D0BD-4DB6-A90B-FE0BCC53E511
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using System;
using UnityEngine;

namespace SRPG
{
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
    [Tooltip("カメラのデフォルトの距離")]
    public float GameCamera_EventCameraDistance;
    [Tooltip("カメラのマップ確認時の距離")]
    public float GameCamera_MapDistance;
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
    public GameSettings.UnitSortIcon[] UnitSort_Modes;
    public Sprite[] UnitIcon_Frames;
    public Sprite[] UnitIcon_Rarity;
    public Sprite ArtifactIcon_Weapon;
    public Sprite ArtifactIcon_Armor;
    public Sprite ArtifactIcon_Misc;
    public Sprite[] ArtifactIcon_Frames;
    public Sprite[] ArtifactIcon_Rarity;
    public Sprite[] ArtifactIcon_RarityBG;
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
    [StringIsResourcePath(typeof (GameObject))]
    public string Dialog_AbilityDetail;
    public string CharacterQuest_Unlock;
    public Canvas Canvas2D;
    public Transform EnemyPosRig;
    public Transform CameraPosRig;
    public GameSettings.DialogSettings Dialogs;
    public GameSettings.CameraSettings Cameras;
    public GameSettings.ColorSettings Colors;
    public GameSettings.QuestSettings Quest;
    public GameSettings.ItemIconSettings ItemIcons;
    public Sprite[] ItemPriceIconFrames;
    public char[] ValidInputChars;
    public float QuestLoad_WaitSecond;
    public string QuestLoad_OkyakusamaCode;
    [Range(1f, 100f)]
    [SerializeField]
    public int HoldMargin;
    public GameSettings.HoldCountSettings[] HoldCount;
    [StringIsResourcePath(typeof (GameObject))]
    public string UnitGet_EffectTemplate;
    public string CharacterQuestSection;
    public FlowNode_WebView.URL_Mode WebHelp_URLMode;
    [StringIsResourcePath(typeof (GameObject))]
    public string WebHelp_PrefabPath;

    public GameSettings()
    {
      base.\u002Ector();
    }

    public static GameSettings Instance
    {
      get
      {
        if (Object.op_Equality((Object) GameSettings.mInstance, (Object) null))
          GameSettings.mInstance = AssetManager.Load<GameSettings>(nameof (GameSettings));
        return GameSettings.mInstance;
      }
    }

    public Sprite GetUnitSortModeIcon(GameUtility.UnitSortModes mode)
    {
      for (int index = 0; index < this.UnitSort_Modes.Length; ++index)
      {
        if (this.UnitSort_Modes[index].Mode == mode)
          return this.UnitSort_Modes[index].Icon;
      }
      return (Sprite) null;
    }

    public Sprite GetItemFrame(ItemParam itemParam)
    {
      Sprite[] spriteArray = itemParam.type == EItemType.UnitPiece || itemParam.type == EItemType.ItemPiecePiece ? this.ItemIcons.KakeraFrames : (itemParam.type != EItemType.ArtifactPiece ? this.ItemIcons.NormalFrames : this.ItemIcons.ArtifactKakeraFrames);
      int index = Mathf.Clamp((int) itemParam.rare, 0, spriteArray.Length - 1);
      if (0 <= index)
        return spriteArray[index];
      return (Sprite) null;
    }

    public long CreateTutorialFlagMask(string flagName)
    {
      for (int index = 1; index < this.Tutorial_Flags.Length; ++index)
      {
        if (this.Tutorial_Flags[index] == flagName)
          return 1L << index;
      }
      return 0;
    }

    [Serializable]
    public struct UnitSortIcon
    {
      public GameUtility.UnitSortModes Mode;
      public Sprite Icon;
    }

    [Serializable]
    public struct DialogSettings
    {
      public Win_Btn_DecideCancel_FL_Check_C YesNoDialogWithCheckBox;
      public Win_Btn_DecideCancel_FL_C YesNoDialog;
      public Win_Btn_Decide_Title_Flx YesDialogWithTitle;
      public Win_Btn_YN_Title_Flx YesNoDialogWithTitle;
    }

    [Serializable]
    public struct CameraSettings
    {
      public Camera OverlayCamera;
    }

    [Serializable]
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

    [Serializable]
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
    }

    [Serializable]
    public struct ItemIconSettings
    {
      public Sprite[] NormalFrames;
      public Sprite[] KakeraFrames;
      public Sprite[] ArtifactKakeraFrames;
    }

    [Serializable]
    public struct HoldCountSettings
    {
      public int Count;
      public float UseSpan;
    }
  }
}
