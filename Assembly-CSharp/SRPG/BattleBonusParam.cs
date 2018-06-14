// Decompiled with JetBrains decompiler
// Type: SRPG.BattleBonusParam
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using System;

namespace SRPG
{
  public class BattleBonusParam
  {
    public static readonly OInt MAX_BATTLE_BONUS = (OInt) Enum.GetNames(typeof (BattleBonus)).Length;
    public static readonly ParamTypes[] ConvertParamTypes = new ParamTypes[43]{ ParamTypes.EffectRange, ParamTypes.EffectScope, ParamTypes.EffectHeight, ParamTypes.HitRate, ParamTypes.AvoidRate, ParamTypes.CriticalRate, ParamTypes.SlashAttack, ParamTypes.PierceAttack, ParamTypes.BlowAttack, ParamTypes.ShotAttack, ParamTypes.MagicAttack, ParamTypes.ReactionAttack, ParamTypes.JumpAttack, ParamTypes.GainJewel, ParamTypes.UsedJewelRate, ParamTypes.ActionCount, ParamTypes.GutsRate, ParamTypes.AutoJewel, ParamTypes.ChargeTimeRate, ParamTypes.CastTimeRate, ParamTypes.BuffTurn, ParamTypes.DebuffTurn, ParamTypes.CombinationRange, ParamTypes.HpCostRate, ParamTypes.SkillUseCount, ParamTypes.PoisonDamage, ParamTypes.PoisonTurn, ParamTypes.Resist_Slash, ParamTypes.Resist_Pierce, ParamTypes.Resist_Blow, ParamTypes.Resist_Shot, ParamTypes.Resist_Magic, ParamTypes.Resist_Reaction, ParamTypes.Resist_Jump, ParamTypes.Avoid_Slash, ParamTypes.Avoid_Pierce, ParamTypes.Avoid_Blow, ParamTypes.Avoid_Shot, ParamTypes.Avoid_Magic, ParamTypes.Avoid_Reaction, ParamTypes.Avoid_Jump, ParamTypes.GainJewelRate, ParamTypes.UsedJewel };
    public OInt[] values = new OInt[(int) BattleBonusParam.MAX_BATTLE_BONUS];

    public OInt this[BattleBonus type]
    {
      get
      {
        return this.values[(int) type];
      }
      set
      {
        this.values[(int) type] = value;
      }
    }

    public void Clear()
    {
      Array.Clear((Array) this.values, 0, this.values.Length);
    }

    public void CopyTo(BattleBonusParam dsc)
    {
      if (dsc == null)
        return;
      for (int index = 0; index < this.values.Length; ++index)
        dsc.values[index] = this.values[index];
    }

    public void Add(BattleBonusParam src)
    {
      if (src == null)
        return;
      for (int index = 0; index < this.values.Length; ++index)
      {
        // ISSUE: explicit reference operation
        // ISSUE: variable of a reference type
        OInt& local = @this.values[index];
        // ISSUE: explicit reference operation
        // ISSUE: explicit reference operation
        ^local = (OInt) ((int) (^local) + (int) src.values[index]);
      }
    }

    public void Sub(BattleBonusParam src)
    {
      if (src == null)
        return;
      for (int index = 0; index < this.values.Length; ++index)
      {
        // ISSUE: explicit reference operation
        // ISSUE: variable of a reference type
        OInt& local = @this.values[index];
        // ISSUE: explicit reference operation
        // ISSUE: explicit reference operation
        ^local = (OInt) ((int) (^local) - (int) src.values[index]);
      }
    }

    public void AddRate(BattleBonusParam src)
    {
      for (int index = 0; index < this.values.Length; ++index)
      {
        // ISSUE: explicit reference operation
        // ISSUE: variable of a reference type
        OInt& local = @this.values[index];
        // ISSUE: explicit reference operation
        // ISSUE: explicit reference operation
        ^local = (OInt) ((int) (^local) + (int) this.values[index] * (int) src.values[index] / 100);
      }
    }

    public void ReplceHighest(BattleBonusParam comp)
    {
      for (int index = 0; index < this.values.Length; ++index)
      {
        if ((int) this.values[index] < (int) comp.values[index])
          this.values[index] = comp.values[index];
      }
    }

    public void ReplceLowest(BattleBonusParam comp)
    {
      for (int index = 0; index < this.values.Length; ++index)
      {
        if ((int) this.values[index] > (int) comp.values[index])
          this.values[index] = comp.values[index];
      }
    }

    public ParamTypes GetParamTypes(int index)
    {
      return BattleBonusParam.ConvertParamTypes[index];
    }
  }
}
