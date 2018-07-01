// Decompiled with JetBrains decompiler
// Type: SRPG.QuestAssets
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 85BFDF7F-5712-4D45-9CD6-3465C703DFDF
// Assembly location: S:\Desktop\Assembly-CSharp.dll

using UnityEngine;

namespace SRPG
{
  public class QuestAssets : ScriptableObject
  {
    public GameObject UI;
    public DropItemEffect DropItemEffect;
    public DropGoldEffect DropGoldEffect;
    public GameObject GemEffect;
    public TreasureBox TreasureBox;
    public BattleSceneSettings BattleScene;
    public GemParticle GemDrainEffect_Front;
    public GemParticle GemDrainEffect_Side;
    public GemParticle GemDrainEffect_Back;
    public GameObject GemDrainHitEffect;
    public DamagePopup DamagePopup;
    public DamageDsgPopup DamageDsgPopup;
    public DamagePopup HealPopup;
    public DamagePopup MpHealPopup;
    public GameObject AutoHealEffect;
    public GameObject DrainHpEffectTemplate;
    public GameObject DrainMpEffectTemplate;
    public GameObject GuardPopup;
    public GameObject AbsorbPopup;
    public GameObject CriticalPopup;
    public GameObject BackstabPopup;
    public GameObject MissPopup;
    public GameObject PerfectAvoidPopup;
    public GameObject WeakPopup;
    public GameObject ResistPopup;
    public GameObject GutsPopup;
    public GameObject ParamChangeEffect;
    public ParamChangeEffects ParamChangeEffects;
    public GameObject ConditionChangeEffect;
    public SkillNamePlate SkillNamePlate;
    public GameObject GridBlocked;
    public DirectionArrow DirectionArrow;
    public GameObject TargetMarker;
    public UnitGauge PlayerHPGauge;
    public UnitGauge EnemyHPGauge;
    public UnitCursor UnitCursor;
    public GameObject RenkeiAura;
    public GameObject RenkeiAssist;
    public GameObject RenkeiCharge;
    public GameObject RenkeiHit;
    public GameObject SummonEffect;
    public GameObject UnitChangeEffect;
    public GameObject WithdrawUnitEffect;
    public GameObject ChargeGrnTargetUnit;
    public GameObject ChargeRedTargetUnit;
    public GameObject JumpSpotEffect;
    public SkillTargetWindow Prefab_SkillTargetWindow;
    public GameObject CurseEffect;
    public string CurseEffectAttachTarget;
    public GameObject KnockBackEffect;
    public GameObject TrickMarker;
    public string[] TrickMarkerIds;
    public GameObject[] TrickMarkerGos;
    public GameObject ContinueWindow;
    public GameObject JumpFallEffect;

    public QuestAssets()
    {
      base.\u002Ector();
    }
  }
}
