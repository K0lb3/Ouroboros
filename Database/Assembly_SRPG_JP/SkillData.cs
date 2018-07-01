// Decompiled with JetBrains decompiler
// Type: SRPG.SkillData
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 85BFDF7F-5712-4D45-9CD6-3465C703DFDF
// Assembly location: S:\Desktop\Assembly-CSharp.dll

using GR;
using System;
using System.Collections.Generic;

namespace SRPG
{
  public class SkillData
  {
    private OInt mRank = (OInt) 1;
    private OInt mRankCap = (OInt) 1;
    private SkillParam mSkillParam;
    private OInt mCastSpeed;
    private OInt mEffectRate;
    private OInt mEffectValue;
    private OInt mEffectRange;
    private OInt mElementValue;
    private OInt mControlDamageRate;
    private OInt mControlDamageValue;
    private OInt mControlChargeTimeRate;
    private OInt mControlChargeTimeValue;
    private OInt mShieldTurn;
    private OInt mShieldValue;
    public BuffEffect mTargetBuffEffect;
    public CondEffect mTargetCondEffect;
    public BuffEffect mSelfBuffEffect;
    public CondEffect mSelfCondEffect;
    private OInt mUseRate;
    private SkillLockCondition mUseCondition;
    private bool mCheckCount;
    private OBool mIsCollabo;
    private string mReplaceSkillId;
    public string m_BaseSkillIname;
    private AbilityData m_OwnerAbility;
    private SkillDeriveParam m_SkillDeriveParam;

    public SkillParam SkillParam
    {
      get
      {
        return this.mSkillParam;
      }
    }

    public string SkillID
    {
      get
      {
        if (this.mSkillParam != null)
          return this.mSkillParam.iname;
        return (string) null;
      }
    }

    public int Rank
    {
      get
      {
        return (int) this.mRank;
      }
    }

    public string Name
    {
      get
      {
        return this.mSkillParam.name;
      }
    }

    public ESkillType SkillType
    {
      get
      {
        return this.mSkillParam.type;
      }
    }

    public ESkillTarget Target
    {
      get
      {
        return this.mSkillParam.target;
      }
    }

    public ESkillTiming Timing
    {
      get
      {
        return this.mSkillParam.timing;
      }
    }

    public ESkillCondition Condition
    {
      get
      {
        return this.mSkillParam.condition;
      }
    }

    public int Cost
    {
      get
      {
        return (int) this.SkillParam.cost;
      }
    }

    public ELineType LineType
    {
      get
      {
        return this.mSkillParam.line_type;
      }
    }

    public int EnableAttackGridHeight
    {
      get
      {
        return (int) this.mSkillParam.effect_height;
      }
    }

    public int RangeMin
    {
      get
      {
        return (int) this.mSkillParam.range_min;
      }
    }

    public int RangeMax
    {
      get
      {
        return (int) this.mSkillParam.range_max;
      }
    }

    public int Scope
    {
      get
      {
        return (int) this.mSkillParam.scope;
      }
    }

    public int HpCostRate
    {
      get
      {
        return (int) this.mSkillParam.hp_cost_rate;
      }
    }

    public ECastTypes CastType
    {
      get
      {
        return this.mSkillParam.cast_type;
      }
    }

    public OInt CastSpeed
    {
      get
      {
        return this.mCastSpeed;
      }
    }

    public SkillEffectTypes EffectType
    {
      get
      {
        return this.mSkillParam.effect_type;
      }
    }

    public OInt EffectRate
    {
      get
      {
        return this.mEffectRate;
      }
    }

    public OInt EffectValue
    {
      get
      {
        return this.mEffectValue;
      }
    }

    public OInt EffectRange
    {
      get
      {
        return this.mEffectRange;
      }
    }

    public OInt EffectHpMaxRate
    {
      get
      {
        return this.mSkillParam.effect_hprate;
      }
    }

    public OInt EffectGemsMaxRate
    {
      get
      {
        return this.mSkillParam.effect_mprate;
      }
    }

    public SkillParamCalcTypes EffectCalcType
    {
      get
      {
        return this.mSkillParam.effect_calc;
      }
    }

    public EElement ElementType
    {
      get
      {
        return this.mSkillParam.element_type;
      }
    }

    public OInt ElementValue
    {
      get
      {
        return this.mElementValue;
      }
    }

    public AttackTypes AttackType
    {
      get
      {
        return this.mSkillParam.attack_type;
      }
    }

    public AttackDetailTypes AttackDetailType
    {
      get
      {
        return this.mSkillParam.attack_detail;
      }
    }

    public int BackAttackDefenseDownRate
    {
      get
      {
        return (int) this.mSkillParam.back_defrate;
      }
    }

    public int SideAttackDefenseDownRate
    {
      get
      {
        return (int) this.mSkillParam.side_defrate;
      }
    }

    public DamageTypes ReactionDamageType
    {
      get
      {
        return this.mSkillParam.reaction_damage_type;
      }
    }

    public int DamageAbsorbRate
    {
      get
      {
        return (int) this.mSkillParam.absorb_damage_rate;
      }
    }

    public OInt ControlDamageRate
    {
      get
      {
        return this.mControlDamageRate;
      }
    }

    public OInt ControlDamageValue
    {
      get
      {
        return this.mControlDamageValue;
      }
    }

    public SkillParamCalcTypes ControlDamageCalcType
    {
      get
      {
        return this.mSkillParam.control_damage_calc;
      }
    }

    public OInt ControlChargeTimeRate
    {
      get
      {
        return this.mControlChargeTimeRate;
      }
    }

    public OInt ControlChargeTimeValue
    {
      get
      {
        return this.mControlChargeTimeValue;
      }
    }

    public SkillParamCalcTypes ControlChargeTimeCalcType
    {
      get
      {
        return this.mSkillParam.control_ct_calc;
      }
    }

    public ShieldTypes ShieldType
    {
      get
      {
        return this.mSkillParam.shield_type;
      }
    }

    public DamageTypes ShieldDamageType
    {
      get
      {
        return this.mSkillParam.shield_damage_type;
      }
    }

    public OInt ShieldTurn
    {
      get
      {
        return this.mShieldTurn;
      }
    }

    public OInt ShieldValue
    {
      get
      {
        return this.mShieldValue;
      }
    }

    public OInt UseRate
    {
      get
      {
        return this.mUseRate;
      }
      set
      {
        this.mUseRate = value;
      }
    }

    public SkillLockCondition UseCondition
    {
      get
      {
        return this.mUseCondition;
      }
      set
      {
        if (value == null)
          return;
        if (this.mUseCondition == null)
          this.mUseCondition = new SkillLockCondition();
        value.CopyTo(this.mUseCondition);
      }
    }

    public bool CheckCount
    {
      get
      {
        return this.mCheckCount;
      }
      set
      {
        this.mCheckCount = value;
      }
    }

    public int DuplicateCount
    {
      get
      {
        return (int) this.mSkillParam.DuplicateCount;
      }
    }

    public OBool IsCollabo
    {
      get
      {
        return this.mIsCollabo;
      }
      set
      {
        this.mIsCollabo = value;
      }
    }

    public string ReplaceSkillId
    {
      get
      {
        return this.mReplaceSkillId;
      }
      set
      {
        this.mReplaceSkillId = value;
      }
    }

    public eTeleportType TeleportType
    {
      get
      {
        return this.mSkillParam.TeleportType;
      }
    }

    public ESkillTarget TeleportTarget
    {
      get
      {
        return this.mSkillParam.TeleportTarget;
      }
    }

    public int TeleportHeight
    {
      get
      {
        return this.mSkillParam.TeleportHeight;
      }
    }

    public bool TeleportIsMove
    {
      get
      {
        return this.mSkillParam.TeleportIsMove;
      }
    }

    public OInt KnockBackRate
    {
      get
      {
        return this.mSkillParam.KnockBackRate;
      }
    }

    public OInt KnockBackVal
    {
      get
      {
        return this.mSkillParam.KnockBackVal;
      }
    }

    public eKnockBackDir KnockBackDir
    {
      get
      {
        return this.mSkillParam.KnockBackDir;
      }
    }

    public eKnockBackDs KnockBackDs
    {
      get
      {
        return this.mSkillParam.KnockBackDs;
      }
    }

    public int WeatherRate
    {
      get
      {
        return this.mSkillParam.WeatherRate;
      }
    }

    public string WeatherId
    {
      get
      {
        return this.mSkillParam.WeatherId;
      }
    }

    public int ElementSpcAtkRate
    {
      get
      {
        return this.mSkillParam.ElementSpcAtkRate;
      }
    }

    public int MaxDamageValue
    {
      get
      {
        return this.mSkillParam.MaxDamageValue;
      }
    }

    public string CutInConceptCardId
    {
      get
      {
        return this.mSkillParam.CutInConceptCardId;
      }
    }

    public int JumpSpcAtkRate
    {
      get
      {
        return this.mSkillParam.JumpSpcAtkRate;
      }
    }

    public AbilityData OwnerAbiliy
    {
      get
      {
        return this.m_OwnerAbility;
      }
    }

    public bool IsDerivedSkill
    {
      get
      {
        return !string.IsNullOrEmpty(this.m_BaseSkillIname);
      }
    }

    public SkillDeriveParam DeriveParam
    {
      get
      {
        return this.m_SkillDeriveParam;
      }
    }

    public void Setup(string iname, int rank, int rankcap = 1, MasterParam master = null)
    {
      if (string.IsNullOrEmpty(iname))
      {
        this.Reset();
      }
      else
      {
        if (master == null)
          master = MonoSingleton<GameManager>.GetInstanceDirect().MasterParam;
        this.mSkillParam = master.GetSkillParam(iname);
        this.mRankCap = (int) this.mSkillParam.lvcap != 0 ? (OInt) Math.Max((int) this.mSkillParam.lvcap, 1) : (OInt) Math.Max(rankcap, 1);
        this.mRank = (OInt) Math.Min(rank, (int) this.mRankCap);
        this.mTargetBuffEffect = BuffEffect.CreateBuffEffect(master.GetBuffEffectParam(this.SkillParam.target_buff_iname), (int) this.mRank, (int) this.mRankCap);
        this.mSelfBuffEffect = BuffEffect.CreateBuffEffect(master.GetBuffEffectParam(this.SkillParam.self_buff_iname), (int) this.mRank, (int) this.mRankCap);
        this.mTargetCondEffect = CondEffect.CreateCondEffect(master.GetCondEffectParam(this.SkillParam.target_cond_iname), (int) this.mRank, (int) this.mRankCap);
        this.mSelfCondEffect = CondEffect.CreateCondEffect(master.GetCondEffectParam(this.SkillParam.self_cond_iname), (int) this.mRank, (int) this.mRankCap);
        this.UpdateParam();
      }
    }

    private void Reset()
    {
      this.mSkillParam = (SkillParam) null;
      this.mRank = (OInt) 1;
    }

    public bool IsValid()
    {
      return this.mSkillParam != null;
    }

    public int GetRankCap()
    {
      return (int) this.mRankCap;
    }

    public void UpdateParam()
    {
      if (this.SkillParam == null)
        return;
      int rank = this.Rank;
      int rankCap = this.GetRankCap();
      this.mCastSpeed = (OInt) this.SkillParam.CalcCurrentRankValue(rank, rankCap, this.SkillParam.cast_speed);
      this.mEffectRate = (OInt) this.SkillParam.CalcCurrentRankValue(rank, rankCap, this.SkillParam.effect_rate);
      this.mEffectValue = (OInt) this.SkillParam.CalcCurrentRankValue(rank, rankCap, this.SkillParam.effect_value);
      this.mEffectRange = (OInt) this.SkillParam.CalcCurrentRankValue(rank, rankCap, this.SkillParam.effect_range);
      this.mElementValue = (OInt) this.SkillParam.CalcCurrentRankValue(rank, rankCap, this.SkillParam.element_value);
      this.mControlDamageRate = (OInt) this.SkillParam.CalcCurrentRankValue(rank, rankCap, this.SkillParam.control_damage_rate);
      this.mControlDamageValue = (OInt) this.SkillParam.CalcCurrentRankValue(rank, rankCap, this.SkillParam.control_damage_value);
      this.mControlChargeTimeRate = (OInt) this.SkillParam.CalcCurrentRankValue(rank, rankCap, this.SkillParam.control_ct_rate);
      this.mControlChargeTimeValue = (OInt) this.SkillParam.CalcCurrentRankValue(rank, rankCap, this.SkillParam.control_ct_value);
      this.mShieldTurn = (OInt) this.SkillParam.CalcCurrentRankValue(rank, rankCap, this.SkillParam.shield_turn);
      this.mShieldValue = (OInt) this.SkillParam.CalcCurrentRankValue(rank, rankCap, this.SkillParam.shield_value);
      if (this.mTargetBuffEffect != null)
        this.mTargetBuffEffect.UpdateCurrentValues(rank, rankCap);
      if (this.mTargetCondEffect != null)
        this.mTargetCondEffect.UpdateCurrentValues(rank, rankCap);
      if (this.mSelfBuffEffect != null)
        this.mSelfBuffEffect.UpdateCurrentValues(rank, rankCap);
      if (this.mSelfCondEffect == null)
        return;
      this.mSelfCondEffect.UpdateCurrentValues(rank, rankCap);
    }

    public bool IsNormalAttack()
    {
      if (this.SkillParam != null)
        return this.SkillParam.type == ESkillType.Attack;
      return false;
    }

    public bool IsPassiveSkill()
    {
      if (this.SkillParam == null)
        return false;
      if (this.SkillType == ESkillType.Passive)
        return true;
      if (this.SkillType == ESkillType.Item)
        return this.EffectType == SkillEffectTypes.Equipment;
      return false;
    }

    public bool IsItem()
    {
      if (this.SkillParam != null)
        return this.SkillParam.type == ESkillType.Item;
      return false;
    }

    public bool IsReactionSkill()
    {
      if (this.SkillParam != null)
        return this.SkillParam.IsReactionSkill();
      return false;
    }

    public bool IsEnableChangeRange()
    {
      if (this.SkillParam != null)
        return this.SkillParam.IsEnableChangeRange();
      return false;
    }

    public bool IsEnableHeightRangeBonus()
    {
      if (this.SkillParam != null)
        return this.SkillParam.IsEnableHeightRangeBonus();
      return false;
    }

    public bool IsEnableHeightParamAdjust()
    {
      if (this.SkillParam != null)
        return this.SkillParam.IsEnableHeightParamAdjust();
      return false;
    }

    public bool IsEnableUnitLockTarget()
    {
      if (this.SkillParam != null)
        return this.SkillParam.IsEnableUnitLockTarget();
      return false;
    }

    public bool IsDamagedSkill()
    {
      if (this.SkillParam != null)
        return this.SkillParam.IsDamagedSkill();
      return false;
    }

    public bool IsHealSkill()
    {
      if (this.SkillParam != null)
        return this.SkillParam.IsHealSkill();
      return false;
    }

    public bool IsPierce()
    {
      if (this.SkillParam != null)
        return this.SkillParam.IsPierce();
      return false;
    }

    public bool IsJewelAttack()
    {
      if (this.SkillParam != null)
        return this.SkillParam.IsJewelAttack();
      return false;
    }

    public bool IsForceHit()
    {
      if (this.SkillParam != null)
        return this.SkillParam.IsForceHit();
      return false;
    }

    public bool IsSuicide()
    {
      if (this.SkillParam != null)
        return this.SkillParam.IsSuicide();
      return false;
    }

    public bool IsSubActuate()
    {
      if (this.SkillParam != null)
        return this.SkillParam.IsSubActuate();
      return false;
    }

    public bool IsFixedDamage()
    {
      if (this.SkillParam != null)
        return this.SkillParam.IsFixedDamage();
      return false;
    }

    public bool IsForceUnitLock()
    {
      if (this.SkillParam != null)
        return this.SkillParam.IsForceUnitLock();
      return false;
    }

    public bool IsAllDamageReaction()
    {
      if (this.SkillParam != null)
        return this.SkillParam.IsAllDamageReaction();
      return false;
    }

    public bool IsIgnoreElement()
    {
      if (this.SkillParam != null)
        return this.SkillParam.IsIgnoreElement();
      return false;
    }

    public bool IsPrevApply()
    {
      if (this.SkillParam != null)
        return this.SkillParam.IsPrevApply();
      return false;
    }

    public bool IsMhmDamage()
    {
      if (this.SkillParam != null)
        return this.SkillParam.IsMhmDamage();
      return false;
    }

    public bool IsCastBreak()
    {
      if (this.SkillParam != null)
        return this.SkillParam.IsCastBreak();
      return false;
    }

    public bool IsCastSkill()
    {
      if (this.SkillParam != null)
        return this.SkillParam.IsCastSkill();
      return false;
    }

    public bool IsCutin()
    {
      if (this.SkillParam != null)
        return this.SkillParam.IsCutin();
      return false;
    }

    public bool IsMapSkill()
    {
      if (this.SkillParam != null)
        return this.SkillParam.IsMapSkill();
      return true;
    }

    public bool IsBattleSkill()
    {
      if (this.SkillParam != null)
        return this.SkillParam.IsBattleSkill();
      return false;
    }

    public bool IsAreaSkill()
    {
      if (this.SkillParam != null)
        return this.SkillParam.IsAreaSkill();
      return false;
    }

    public bool IsAllEffect()
    {
      if (this.SkillParam != null)
        return this.SkillParam.IsAllEffect();
      return false;
    }

    public bool IsLongRangeSkill()
    {
      if (this.SkillParam != null)
        return this.SkillParam.IsLongRangeSkill();
      return false;
    }

    public bool IsSupportSkill()
    {
      if (this.SkillParam != null)
        return this.SkillParam.IsSupportSkill();
      return false;
    }

    public bool IsTrickSkill()
    {
      if (this.SkillParam != null)
        return this.SkillParam.IsTrickSkill();
      return false;
    }

    public bool IsTransformSkill()
    {
      if (this.SkillParam != null)
        return this.SkillParam.IsTransformSkill();
      return false;
    }

    public bool IsSetBreakObjSkill()
    {
      if (this.SkillParam != null)
        return this.SkillParam.IsSetBreakObjSkill();
      return false;
    }

    public bool IsChangeWeatherSkill()
    {
      if (this.SkillParam != null)
        return this.SkillParam.IsChangeWeatherSkill();
      return false;
    }

    public bool IsJudgeHp(Unit unit)
    {
      if (this.SkillParam != null)
        return this.SkillParam.IsJudgeHp(unit);
      return false;
    }

    public bool IsConditionSkill()
    {
      if (this.SkillParam != null)
        return this.SkillParam.IsConditionSkill();
      return false;
    }

    public bool IsPhysicalAttack()
    {
      return this.SkillParam != null && this.SkillParam.attack_type == AttackTypes.PhyAttack;
    }

    public bool IsMagicalAttack()
    {
      return this.SkillParam != null && this.SkillParam.attack_type == AttackTypes.MagAttack;
    }

    public bool IsSelfTargetSelect()
    {
      return this.SkillParam.IsSelfTargetSelect();
    }

    public bool IsAdvantage()
    {
      if (this.SkillParam != null)
        return this.SkillParam.IsAdvantage();
      return false;
    }

    public bool CheckGridLineSkill()
    {
      SkillParam skillParam = this.SkillParam;
      return skillParam != null && skillParam.line_type != ELineType.None && this.RangeMax > 1;
    }

    public bool CheckUnitSkillTarget()
    {
      switch (this.Target)
      {
        case ESkillTarget.Self:
        case ESkillTarget.SelfSide:
        case ESkillTarget.EnemySide:
        case ESkillTarget.UnitAll:
        case ESkillTarget.NotSelf:
          return true;
        default:
          return false;
      }
    }

    public int GetHpCost(Unit self)
    {
      int hpCost = (int) this.SkillParam.hp_cost;
      int num = hpCost + hpCost * (int) self.CurrentStatus[BattleBonus.HpCostRate] * 100 / 10000;
      return (int) self.MaximumStatus.param.hp * num * 100 / 10000;
    }

    public BuffEffect GetBuffEffect(SkillEffectTargets target = SkillEffectTargets.Target)
    {
      switch (target)
      {
        case SkillEffectTargets.Target:
          return this.mTargetBuffEffect;
        case SkillEffectTargets.Self:
          return this.mSelfBuffEffect;
        default:
          return (BuffEffect) null;
      }
    }

    public CondEffect GetCondEffect(SkillEffectTargets target = SkillEffectTargets.Target)
    {
      switch (target)
      {
        case SkillEffectTargets.Target:
          return this.mTargetCondEffect;
        case SkillEffectTargets.Self:
          return this.mSelfCondEffect;
        default:
          return (CondEffect) null;
      }
    }

    public int CalcBuffEffectValue(ESkillTiming timing, ParamTypes type, int src, SkillEffectTargets target = SkillEffectTargets.Target)
    {
      if (timing != this.Timing)
        return src;
      return this.CalcBuffEffectValue(type, src, target);
    }

    public int CalcBuffEffectValue(ParamTypes type, int src, SkillEffectTargets target = SkillEffectTargets.Target)
    {
      BuffEffect buffEffect = this.GetBuffEffect(target);
      if (buffEffect == null)
        return src;
      BuffEffect.BuffTarget buffTarget = buffEffect[type];
      if (buffTarget == null)
        return src;
      return SkillParam.CalcSkillEffectValue(buffTarget.calcType, (int) buffTarget.value, src);
    }

    public int GetBuffEffectValue(ParamTypes type, SkillEffectTargets target = SkillEffectTargets.Target)
    {
      BuffEffect buffEffect = this.GetBuffEffect(target);
      if (buffEffect == null)
        return 0;
      BuffEffect.BuffTarget buffTarget = buffEffect[type];
      if (buffTarget == null)
        return 0;
      return (int) buffTarget.value;
    }

    public bool BuffSkill(ESkillTiming timing, EElement element, BaseStatus buff, BaseStatus buff_negative, BaseStatus buff_scale, BaseStatus debuff, BaseStatus debuff_negative, BaseStatus debuff_scale, RandXorshift rand = null, SkillEffectTargets buff_target = SkillEffectTargets.Target, bool is_resume = false, List<BuffEffect.BuffValues> list = null)
    {
      if (this.Timing != timing)
        return false;
      BuffEffect buffEffect = this.GetBuffEffect(buff_target);
      if (buffEffect == null)
        return false;
      if (!is_resume)
      {
        int rate = (int) buffEffect.param.rate;
        if (rate > 0 && rate < 100)
        {
          DebugUtility.Assert(rand != null, "発動確率が設定されているスキルを正規タイミングで発動させたにも関わらず乱数生成器の設定がされていない");
          if ((int) (rand.Get() % 100U) > rate)
            return false;
        }
      }
      if (list == null)
      {
        if (buff != null)
          this.InternalBuffSkill(buffEffect, element, BuffTypes.Buff, true, false, SkillParamCalcTypes.Add, buff, (List<BuffEffect.BuffValues>) null);
        if (buff_negative != null)
          this.InternalBuffSkill(buffEffect, element, BuffTypes.Buff, true, true, SkillParamCalcTypes.Add, buff_negative, (List<BuffEffect.BuffValues>) null);
        if (buff_scale != null)
          this.InternalBuffSkill(buffEffect, element, BuffTypes.Buff, false, false, SkillParamCalcTypes.Scale, buff_scale, (List<BuffEffect.BuffValues>) null);
        if (debuff != null)
          this.InternalBuffSkill(buffEffect, element, BuffTypes.Debuff, true, false, SkillParamCalcTypes.Add, debuff, (List<BuffEffect.BuffValues>) null);
        if (debuff_negative != null)
          this.InternalBuffSkill(buffEffect, element, BuffTypes.Debuff, true, true, SkillParamCalcTypes.Add, debuff_negative, (List<BuffEffect.BuffValues>) null);
        if (debuff_scale != null)
          this.InternalBuffSkill(buffEffect, element, BuffTypes.Debuff, false, false, SkillParamCalcTypes.Scale, debuff_scale, (List<BuffEffect.BuffValues>) null);
      }
      else
      {
        this.InternalBuffSkill(buffEffect, element, BuffTypes.Buff, false, false, SkillParamCalcTypes.Add, (BaseStatus) null, list);
        this.InternalBuffSkill(buffEffect, element, BuffTypes.Buff, false, false, SkillParamCalcTypes.Scale, (BaseStatus) null, list);
        this.InternalBuffSkill(buffEffect, element, BuffTypes.Debuff, false, false, SkillParamCalcTypes.Add, (BaseStatus) null, list);
        this.InternalBuffSkill(buffEffect, element, BuffTypes.Debuff, false, false, SkillParamCalcTypes.Scale, (BaseStatus) null, list);
      }
      return true;
    }

    private void InternalBuffSkill(BuffEffect effect, EElement element, BuffTypes buffType, bool is_check_negative_value, bool is_negative_value_is_buff, SkillParamCalcTypes calcType, BaseStatus status, List<BuffEffect.BuffValues> list = null)
    {
      for (int index1 = 0; index1 < effect.targets.Count; ++index1)
      {
        BuffEffect.BuffTarget target = effect.targets[index1];
        if (target != null && target.buffType == buffType && (!is_check_negative_value || BuffEffectParam.IsNegativeValueIsBuff(target.paramType) == is_negative_value_is_buff) && target.calcType == calcType)
        {
          if (element != EElement.None)
          {
            bool flag = false;
            switch (target.paramType)
            {
              case ParamTypes.Assist_Fire:
              case ParamTypes.UnitDefenseFire:
                flag = element != EElement.Fire;
                break;
              case ParamTypes.Assist_Water:
              case ParamTypes.UnitDefenseWater:
                flag = element != EElement.Water;
                break;
              case ParamTypes.Assist_Wind:
              case ParamTypes.UnitDefenseWind:
                flag = element != EElement.Wind;
                break;
              case ParamTypes.Assist_Thunder:
              case ParamTypes.UnitDefenseThunder:
                flag = element != EElement.Thunder;
                break;
              case ParamTypes.Assist_Shine:
              case ParamTypes.UnitDefenseShine:
                flag = element != EElement.Shine;
                break;
              case ParamTypes.Assist_Dark:
              case ParamTypes.UnitDefenseDark:
                flag = element != EElement.Dark;
                break;
            }
            if (flag)
              continue;
          }
          BuffMethodTypes buffMethodType = this.GetBuffMethodType(target.buffType, calcType);
          ParamTypes paramType = target.paramType;
          int num = (int) target.value;
          if ((bool) effect.param.mIsUpBuff)
            num = 0;
          if (list == null)
          {
            BuffEffect.SetBuffValues(paramType, buffMethodType, ref status, num);
          }
          else
          {
            bool flag = true;
            for (int index2 = 0; index2 < list.Count; ++index2)
            {
              BuffEffect.BuffValues buffValues = list[index2];
              if (buffValues.param_type == paramType && buffValues.method_type == buffMethodType)
              {
                buffValues.value += num;
                list[index2] = buffValues;
                flag = false;
                break;
              }
            }
            if (flag)
              list.Add(new BuffEffect.BuffValues()
              {
                param_type = paramType,
                method_type = buffMethodType,
                value = num
              });
          }
        }
      }
    }

    private BuffMethodTypes GetBuffMethodType(BuffTypes buff, SkillParamCalcTypes calc)
    {
      if (this.Timing == ESkillTiming.Passive || calc != SkillParamCalcTypes.Scale)
        return BuffMethodTypes.Add;
      return buff == BuffTypes.Buff ? BuffMethodTypes.Highest : BuffMethodTypes.Lowest;
    }

    public bool IsReactionDet(AttackDetailTypes atk_detail_type)
    {
      if (this.mSkillParam == null)
        return false;
      return this.mSkillParam.IsReactionDet(atk_detail_type);
    }

    public static void GetHomePassiveBuffStatus(SkillData skill, EElement element, ref BaseStatus status, ref BaseStatus scale_status)
    {
      SkillData.GetHomePassiveBuffStatus(skill, element, ref status, ref status, ref status, ref status, ref scale_status);
    }

    public static void GetHomePassiveBuffStatus(SkillData skill, EElement element, ref BaseStatus status, ref BaseStatus negative_status, ref BaseStatus debuff_status, ref BaseStatus negative_debuff_status, ref BaseStatus scale_status)
    {
      if (skill == null || !skill.IsPassiveSkill() || (skill.Target != ESkillTarget.Self || skill.Condition != ESkillCondition.None) || !string.IsNullOrEmpty(skill.SkillParam.tokkou))
        return;
      BuffEffect buffEffect1 = skill.GetBuffEffect(SkillEffectTargets.Target);
      if (buffEffect1 != null && buffEffect1.param != null && (buffEffect1.param.cond == ESkillCondition.None && buffEffect1.param.mAppType == EAppType.Standard) && (buffEffect1.param.mEffRange == EEffRange.Self && !(bool) buffEffect1.param.mIsUpBuff))
        skill.BuffSkill(ESkillTiming.Passive, element, status, negative_status, scale_status, debuff_status, negative_debuff_status, scale_status, (RandXorshift) null, SkillEffectTargets.Target, false, (List<BuffEffect.BuffValues>) null);
      BuffEffect buffEffect2 = skill.GetBuffEffect(SkillEffectTargets.Self);
      if (buffEffect2 == null || buffEffect2.param == null || (buffEffect2.param.cond != ESkillCondition.None || buffEffect2.param.mAppType != EAppType.Standard) || (buffEffect2.param.mEffRange != EEffRange.Self || (bool) buffEffect2.param.mIsUpBuff))
        return;
      skill.BuffSkill(ESkillTiming.Passive, element, status, negative_status, scale_status, debuff_status, negative_debuff_status, scale_status, (RandXorshift) null, SkillEffectTargets.Self, false, (List<BuffEffect.BuffValues>) null);
    }

    public static void GetPassiveBuffStatus(SkillData skill, BuffEffect[] add_buff_effects, EElement element, ref BaseStatus status, ref BaseStatus scale_status)
    {
      SkillData.GetPassiveBuffStatus(skill, add_buff_effects, element, ref status, ref status, ref status, ref status, ref scale_status);
    }

    public static void GetPassiveBuffStatus(SkillData skill, BuffEffect[] add_buff_effects, EElement element, ref BaseStatus status, ref BaseStatus negative_status, ref BaseStatus debuff_status, ref BaseStatus negative_debuff_status, ref BaseStatus scale_status)
    {
      if (skill == null || !skill.IsPassiveSkill())
        return;
      BuffEffect buffEffect1 = skill.GetBuffEffect(SkillEffectTargets.Target);
      if (buffEffect1 != null && buffEffect1.param != null && (buffEffect1.param.mAppType == EAppType.Standard && buffEffect1.param.mEffRange == EEffRange.Self) && !(bool) buffEffect1.param.mIsUpBuff)
        skill.BuffSkill(ESkillTiming.Passive, element, status, negative_status, scale_status, debuff_status, negative_debuff_status, scale_status, (RandXorshift) null, SkillEffectTargets.Target, false, (List<BuffEffect.BuffValues>) null);
      BuffEffect buffEffect2 = skill.GetBuffEffect(SkillEffectTargets.Self);
      if (buffEffect2 == null || buffEffect2.param == null || (buffEffect2.param.mAppType != EAppType.Standard || buffEffect2.param.mEffRange != EEffRange.Self) || (bool) buffEffect2.param.mIsUpBuff)
        return;
      skill.BuffSkill(ESkillTiming.Passive, element, status, negative_status, scale_status, debuff_status, negative_debuff_status, scale_status, (RandXorshift) null, SkillEffectTargets.Self, false, (List<BuffEffect.BuffValues>) null);
    }

    public bool IsTargetGridNoUnit
    {
      get
      {
        if (this.SkillParam != null)
          return this.SkillParam.IsTargetGridNoUnit;
        return false;
      }
    }

    public bool IsTargetValidGrid
    {
      get
      {
        if (this.SkillParam != null)
          return this.SkillParam.IsTargetValidGrid;
        return false;
      }
    }

    public bool IsSkillCountNoLimit
    {
      get
      {
        if (this.SkillParam != null)
          return this.SkillParam.IsSkillCountNoLimit;
        return false;
      }
    }

    public bool IsTargetTeleport
    {
      get
      {
        if (this.SkillParam != null)
          return this.SkillParam.IsTargetTeleport;
        return false;
      }
    }

    public void SetOwnerAbility(AbilityData owner)
    {
      this.m_OwnerAbility = owner;
    }

    public SkillData CreateDeriveSkill(SkillDeriveParam skillDeriveParam)
    {
      SkillData skillData = new SkillData();
      skillData.Setup(skillDeriveParam.DeriveSkillIname, this.Rank, 1, (MasterParam) null);
      skillData.m_OwnerAbility = this.m_OwnerAbility;
      skillData.m_BaseSkillIname = this.SkillID;
      skillData.m_SkillDeriveParam = skillDeriveParam;
      return skillData;
    }
  }
}
