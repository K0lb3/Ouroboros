// Decompiled with JetBrains decompiler
// Type: SRPG.BaseStatus
// Assembly: Assembly-CSharp, Version=1.2.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9BA76916-D0BD-4DB6-A90B-FE0BCC53E511
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using System;

namespace SRPG
{
  public class BaseStatus
  {
    public static readonly OInt MAX_BATTLE_BONUS = (OInt) Enum.GetNames(typeof (BattleBonus)).Length;
    public StatusParam param = new StatusParam();
    public ElementParam element_assist = new ElementParam();
    public ElementParam element_resist = new ElementParam();
    public EnchantParam enchant_assist = new EnchantParam();
    public EnchantParam enchant_resist = new EnchantParam();
    public BattleBonusParam bonus = new BattleBonusParam();

    public BaseStatus()
    {
    }

    public BaseStatus(BaseStatus src)
    {
      src.CopyTo(this);
    }

    public OInt this[StatusTypes type]
    {
      get
      {
        return this.param[type];
      }
      set
      {
        this.param[type] = value;
      }
    }

    public OInt this[EnchantCategory category, EElement element]
    {
      get
      {
        return (OInt) (category != EnchantCategory.Assist ? this.element_resist[element] : this.element_assist[element]);
      }
      set
      {
        if (category == EnchantCategory.Assist)
          this.element_assist[element] = (OShort) value;
        else
          this.element_resist[element] = (OShort) value;
      }
    }

    public OInt this[EnchantCategory category, EnchantTypes type]
    {
      get
      {
        if (category == EnchantCategory.Assist)
          return this.enchant_assist[type];
        return this.enchant_resist[type];
      }
      set
      {
        if (category == EnchantCategory.Assist)
          this.enchant_assist[type] = value;
        else
          this.enchant_resist[type] = value;
      }
    }

    public OInt this[BattleBonus type]
    {
      get
      {
        return this.bonus[type];
      }
      set
      {
        this.bonus[type] = value;
      }
    }

    public OInt this[ParamTypes type]
    {
      get
      {
        switch (type)
        {
          case ParamTypes.Hp:
            return this[StatusTypes.Hp];
          case ParamTypes.HpMax:
            return this[StatusTypes.Hp];
          case ParamTypes.Mp:
            return this[StatusTypes.Mp];
          case ParamTypes.MpIni:
            return this[StatusTypes.MpIni];
          case ParamTypes.Atk:
            return this[StatusTypes.Atk];
          case ParamTypes.Def:
            return this[StatusTypes.Def];
          case ParamTypes.Mag:
            return this[StatusTypes.Mag];
          case ParamTypes.Mnd:
            return this[StatusTypes.Mnd];
          case ParamTypes.Rec:
            return this[StatusTypes.Rec];
          case ParamTypes.Dex:
            return this[StatusTypes.Dex];
          case ParamTypes.Spd:
            return this[StatusTypes.Spd];
          case ParamTypes.Cri:
            return this[StatusTypes.Cri];
          case ParamTypes.Luk:
            return this[StatusTypes.Luk];
          case ParamTypes.Mov:
            return this[StatusTypes.Mov];
          case ParamTypes.Jmp:
            return this[StatusTypes.Jmp];
          case ParamTypes.EffectRange:
            return this[BattleBonus.EffectRange];
          case ParamTypes.EffectScope:
            return this[BattleBonus.EffectScope];
          case ParamTypes.EffectHeight:
            return this[BattleBonus.EffectHeight];
          case ParamTypes.Assist_Fire:
            return this[EnchantCategory.Assist, EElement.Fire];
          case ParamTypes.Assist_Water:
            return this[EnchantCategory.Assist, EElement.Water];
          case ParamTypes.Assist_Wind:
            return this[EnchantCategory.Assist, EElement.Wind];
          case ParamTypes.Assist_Thunder:
            return this[EnchantCategory.Assist, EElement.Thunder];
          case ParamTypes.Assist_Shine:
            return this[EnchantCategory.Assist, EElement.Shine];
          case ParamTypes.Assist_Dark:
            return this[EnchantCategory.Assist, EElement.Dark];
          case ParamTypes.Assist_Poison:
            return this[EnchantCategory.Assist, EnchantTypes.Poison];
          case ParamTypes.Assist_Paralysed:
            return this[EnchantCategory.Assist, EnchantTypes.Paralysed];
          case ParamTypes.Assist_Stun:
            return this[EnchantCategory.Assist, EnchantTypes.Stun];
          case ParamTypes.Assist_Sleep:
            return this[EnchantCategory.Assist, EnchantTypes.Sleep];
          case ParamTypes.Assist_Charm:
            return this[EnchantCategory.Assist, EnchantTypes.Charm];
          case ParamTypes.Assist_Stone:
            return this[EnchantCategory.Assist, EnchantTypes.Stone];
          case ParamTypes.Assist_Blind:
            return this[EnchantCategory.Assist, EnchantTypes.Blind];
          case ParamTypes.Assist_DisableSkill:
            return this[EnchantCategory.Assist, EnchantTypes.DisableSkill];
          case ParamTypes.Assist_DisableMove:
            return this[EnchantCategory.Assist, EnchantTypes.DisableMove];
          case ParamTypes.Assist_DisableAttack:
            return this[EnchantCategory.Assist, EnchantTypes.DisableAttack];
          case ParamTypes.Assist_Zombie:
            return this[EnchantCategory.Assist, EnchantTypes.Zombie];
          case ParamTypes.Assist_DeathSentence:
            return this[EnchantCategory.Assist, EnchantTypes.DeathSentence];
          case ParamTypes.Assist_Berserk:
            return this[EnchantCategory.Assist, EnchantTypes.Berserk];
          case ParamTypes.Assist_Knockback:
            return this[EnchantCategory.Assist, EnchantTypes.Knockback];
          case ParamTypes.Assist_ResistBuff:
            return this[EnchantCategory.Assist, EnchantTypes.ResistBuff];
          case ParamTypes.Assist_ResistDebuff:
            return this[EnchantCategory.Assist, EnchantTypes.ResistDebuff];
          case ParamTypes.Assist_Stop:
            return this[EnchantCategory.Assist, EnchantTypes.Stop];
          case ParamTypes.Assist_Fast:
            return this[EnchantCategory.Assist, EnchantTypes.Fast];
          case ParamTypes.Assist_Slow:
            return this[EnchantCategory.Assist, EnchantTypes.Slow];
          case ParamTypes.Assist_AutoHeal:
            return this[EnchantCategory.Assist, EnchantTypes.AutoHeal];
          case ParamTypes.Assist_Donsoku:
            return this[EnchantCategory.Assist, EnchantTypes.Donsoku];
          case ParamTypes.Assist_Rage:
            return this[EnchantCategory.Assist, EnchantTypes.Rage];
          case ParamTypes.Assist_GoodSleep:
            return this[EnchantCategory.Assist, EnchantTypes.GoodSleep];
          case ParamTypes.Resist_Fire:
            return this[EnchantCategory.Resist, EElement.Fire];
          case ParamTypes.Resist_Water:
            return this[EnchantCategory.Resist, EElement.Water];
          case ParamTypes.Resist_Wind:
            return this[EnchantCategory.Resist, EElement.Wind];
          case ParamTypes.Resist_Thunder:
            return this[EnchantCategory.Resist, EElement.Thunder];
          case ParamTypes.Resist_Shine:
            return this[EnchantCategory.Resist, EElement.Shine];
          case ParamTypes.Resist_Dark:
            return this[EnchantCategory.Resist, EElement.Dark];
          case ParamTypes.Resist_Poison:
            return this[EnchantCategory.Resist, EnchantTypes.Poison];
          case ParamTypes.Resist_Paralysed:
            return this[EnchantCategory.Resist, EnchantTypes.Paralysed];
          case ParamTypes.Resist_Stun:
            return this[EnchantCategory.Resist, EnchantTypes.Stun];
          case ParamTypes.Resist_Sleep:
            return this[EnchantCategory.Resist, EnchantTypes.Sleep];
          case ParamTypes.Resist_Charm:
            return this[EnchantCategory.Resist, EnchantTypes.Charm];
          case ParamTypes.Resist_Stone:
            return this[EnchantCategory.Resist, EnchantTypes.Stone];
          case ParamTypes.Resist_Blind:
            return this[EnchantCategory.Resist, EnchantTypes.Blind];
          case ParamTypes.Resist_DisableSkill:
            return this[EnchantCategory.Resist, EnchantTypes.DisableSkill];
          case ParamTypes.Resist_DisableMove:
            return this[EnchantCategory.Resist, EnchantTypes.DisableMove];
          case ParamTypes.Resist_DisableAttack:
            return this[EnchantCategory.Resist, EnchantTypes.DisableAttack];
          case ParamTypes.Resist_Zombie:
            return this[EnchantCategory.Resist, EnchantTypes.Zombie];
          case ParamTypes.Resist_DeathSentence:
            return this[EnchantCategory.Resist, EnchantTypes.DeathSentence];
          case ParamTypes.Resist_Berserk:
            return this[EnchantCategory.Resist, EnchantTypes.Berserk];
          case ParamTypes.Resist_Knockback:
            return this[EnchantCategory.Resist, EnchantTypes.Knockback];
          case ParamTypes.Resist_ResistBuff:
            return this[EnchantCategory.Resist, EnchantTypes.ResistBuff];
          case ParamTypes.Resist_ResistDebuff:
            return this[EnchantCategory.Resist, EnchantTypes.ResistDebuff];
          case ParamTypes.Resist_Stop:
            return this[EnchantCategory.Resist, EnchantTypes.Stop];
          case ParamTypes.Resist_Fast:
            return this[EnchantCategory.Resist, EnchantTypes.Fast];
          case ParamTypes.Resist_Slow:
            return this[EnchantCategory.Resist, EnchantTypes.Slow];
          case ParamTypes.Resist_AutoHeal:
            return this[EnchantCategory.Resist, EnchantTypes.AutoHeal];
          case ParamTypes.Resist_Donsoku:
            return this[EnchantCategory.Resist, EnchantTypes.Donsoku];
          case ParamTypes.Resist_Rage:
            return this[EnchantCategory.Resist, EnchantTypes.Rage];
          case ParamTypes.Resist_GoodSleep:
            return this[EnchantCategory.Resist, EnchantTypes.GoodSleep];
          case ParamTypes.HitRate:
            return this[BattleBonus.HitRate];
          case ParamTypes.AvoidRate:
            return this[BattleBonus.AvoidRate];
          case ParamTypes.CriticalRate:
            return this[BattleBonus.CriticalRate];
          case ParamTypes.GainJewel:
            return this[BattleBonus.GainJewel];
          case ParamTypes.UsedJewelRate:
            return this[BattleBonus.UsedJewelRate];
          case ParamTypes.ActionCount:
            return this[BattleBonus.ActionCount];
          case ParamTypes.SlashAttack:
            return this[BattleBonus.SlashAttack];
          case ParamTypes.PierceAttack:
            return this[BattleBonus.PierceAttack];
          case ParamTypes.BlowAttack:
            return this[BattleBonus.BlowAttack];
          case ParamTypes.ShotAttack:
            return this[BattleBonus.ShotAttack];
          case ParamTypes.MagicAttack:
            return this[BattleBonus.MagicAttack];
          case ParamTypes.ReactionAttack:
            return this[BattleBonus.ReactionAttack];
          case ParamTypes.JumpAttack:
            return this[BattleBonus.JumpAttack];
          case ParamTypes.GutsRate:
            return this[BattleBonus.GutsRate];
          case ParamTypes.AutoJewel:
            return this[BattleBonus.AutoJewel];
          case ParamTypes.ChargeTimeRate:
            return this[BattleBonus.ChargeTimeRate];
          case ParamTypes.CastTimeRate:
            return this[BattleBonus.CastTimeRate];
          case ParamTypes.BuffTurn:
            return this[BattleBonus.BuffTurn];
          case ParamTypes.DebuffTurn:
            return this[BattleBonus.DebuffTurn];
          case ParamTypes.CombinationRange:
            return this[BattleBonus.CombinationRange];
          case ParamTypes.HpCostRate:
            return this[BattleBonus.HpCostRate];
          case ParamTypes.SkillUseCount:
            return this[BattleBonus.SkillUseCount];
          case ParamTypes.PoisonDamage:
            return this[BattleBonus.PoisonDamage];
          case ParamTypes.PoisonTurn:
            return this[BattleBonus.PoisonTurn];
          case ParamTypes.Assist_AutoJewel:
            return this[EnchantCategory.Assist, EnchantTypes.AutoJewel];
          case ParamTypes.Resist_AutoJewel:
            return this[EnchantCategory.Resist, EnchantTypes.AutoJewel];
          case ParamTypes.Assist_DisableHeal:
            return this[EnchantCategory.Assist, EnchantTypes.DisableHeal];
          case ParamTypes.Resist_DisableHeal:
            return this[EnchantCategory.Resist, EnchantTypes.DisableHeal];
          case ParamTypes.Resist_Slash:
            return this[BattleBonus.Resist_Slash];
          case ParamTypes.Resist_Pierce:
            return this[BattleBonus.Resist_Pierce];
          case ParamTypes.Resist_Blow:
            return this[BattleBonus.Resist_Blow];
          case ParamTypes.Resist_Shot:
            return this[BattleBonus.Resist_Shot];
          case ParamTypes.Resist_Magic:
            return this[BattleBonus.Resist_Magic];
          case ParamTypes.Resist_Reaction:
            return this[BattleBonus.Resist_Reaction];
          case ParamTypes.Resist_Jump:
            return this[BattleBonus.Resist_Jump];
          case ParamTypes.Avoid_Slash:
            return this[BattleBonus.Avoid_Slash];
          case ParamTypes.Avoid_Pierce:
            return this[BattleBonus.Avoid_Pierce];
          case ParamTypes.Avoid_Blow:
            return this[BattleBonus.Avoid_Blow];
          case ParamTypes.Avoid_Shot:
            return this[BattleBonus.Avoid_Shot];
          case ParamTypes.Avoid_Magic:
            return this[BattleBonus.Avoid_Magic];
          case ParamTypes.Avoid_Reaction:
            return this[BattleBonus.Avoid_Reaction];
          case ParamTypes.Avoid_Jump:
            return this[BattleBonus.Avoid_Jump];
          default:
            return (OInt) 0;
        }
      }
    }

    public void Clear()
    {
      this.param.Clear();
      this.element_assist.Clear();
      this.element_resist.Clear();
      this.enchant_assist.Clear();
      this.enchant_resist.Clear();
      this.bonus.Clear();
    }

    public void CopyTo(BaseStatus dsc)
    {
      this.param.CopyTo(dsc.param);
      this.element_assist.CopyTo(dsc.element_assist);
      this.element_resist.CopyTo(dsc.element_resist);
      this.enchant_assist.CopyTo(dsc.enchant_assist);
      this.enchant_resist.CopyTo(dsc.enchant_resist);
      this.bonus.CopyTo(dsc.bonus);
    }

    public void Add(BaseStatus src)
    {
      this.param.Add(src.param);
      this.element_assist.Add(src.element_assist);
      this.element_resist.Add(src.element_resist);
      this.enchant_assist.Add(src.enchant_assist);
      this.enchant_resist.Add(src.enchant_resist);
      this.bonus.Add(src.bonus);
    }

    public void Sub(BaseStatus src)
    {
      this.param.Sub(src.param);
      this.element_assist.Sub(src.element_assist);
      this.element_resist.Sub(src.element_resist);
      this.enchant_assist.Sub(src.enchant_assist);
      this.enchant_resist.Sub(src.enchant_resist);
      this.bonus.Sub(src.bonus);
    }

    public void AddRate(BaseStatus src)
    {
      this.param.AddRate(src.param);
      this.element_assist.AddRate(src.element_assist);
      this.element_resist.AddRate(src.element_resist);
      this.enchant_assist.AddRate(src.enchant_assist);
      this.enchant_resist.AddRate(src.enchant_resist);
      this.bonus.AddRate(src.bonus);
    }

    public void AddRate(StatusParam src)
    {
      this.param.AddRate(src);
    }

    public void ReplaceHighest(BaseStatus comp)
    {
      this.param.ReplceHighest(comp.param);
      this.element_assist.ReplceHighest(comp.element_assist);
      this.element_resist.ReplceHighest(comp.element_resist);
      this.enchant_assist.ReplceHighest(comp.enchant_assist);
      this.enchant_resist.ReplceHighest(comp.enchant_resist);
      this.bonus.ReplceHighest(comp.bonus);
    }

    public void ReplaceLowest(BaseStatus comp)
    {
      this.param.ReplceLowest(comp.param);
      this.element_assist.ReplceLowest(comp.element_assist);
      this.element_resist.ReplceLowest(comp.element_resist);
      this.enchant_assist.ReplceLowest(comp.enchant_assist);
      this.enchant_resist.ReplceLowest(comp.enchant_resist);
      this.bonus.ReplceLowest(comp.bonus);
    }
  }
}
