namespace SRPG
{
    using System;
    using UnityEngine;

    public class QuestAssets : ScriptableObject
    {
        public GameObject UI;
        public SRPG.DropItemEffect DropItemEffect;
        public SRPG.DropGoldEffect DropGoldEffect;
        public GameObject GemEffect;
        public SRPG.TreasureBox TreasureBox;
        public BattleSceneSettings BattleScene;
        public GemParticle GemDrainEffect_Front;
        public GemParticle GemDrainEffect_Side;
        public GemParticle GemDrainEffect_Back;
        public GameObject GemDrainHitEffect;
        public SRPG.DamagePopup DamagePopup;
        public SRPG.DamageDsgPopup DamageDsgPopup;
        public SRPG.DamagePopup HealPopup;
        public SRPG.DamagePopup MpHealPopup;
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
        public SRPG.ParamChangeEffects ParamChangeEffects;
        public GameObject ConditionChangeEffect;
        public SkillNamePlate SkillNamePlate;
        public GameObject GridBlocked;
        public SRPG.DirectionArrow DirectionArrow;
        public GameObject TargetMarker;
        public UnitGauge PlayerHPGauge;
        public UnitGauge EnemyHPGauge;
        public SRPG.UnitCursor UnitCursor;
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
            base..ctor();
            return;
        }
    }
}

