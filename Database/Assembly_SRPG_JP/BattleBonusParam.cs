// Decompiled with JetBrains decompiler
// Type: SRPG.BattleBonusParam
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 85BFDF7F-5712-4D45-9CD6-3465C703DFDF
// Assembly location: S:\Desktop\Assembly-CSharp.dll

using System;

namespace SRPG
{
  public class BattleBonusParam
  {
    public static readonly int MAX_BATTLE_BONUS = Enum.GetNames(typeof (BattleBonus)).Length;
    public static readonly ParamTypes[] ConvertParamTypes = new ParamTypes[49]
    {
      ParamTypes.EffectRange,
      ParamTypes.EffectScope,
      ParamTypes.EffectHeight,
      ParamTypes.HitRate,
      ParamTypes.AvoidRate,
      ParamTypes.CriticalRate,
      ParamTypes.SlashAttack,
      ParamTypes.PierceAttack,
      ParamTypes.BlowAttack,
      ParamTypes.ShotAttack,
      ParamTypes.MagicAttack,
      ParamTypes.ReactionAttack,
      ParamTypes.JumpAttack,
      ParamTypes.GainJewel,
      ParamTypes.UsedJewelRate,
      ParamTypes.ActionCount,
      ParamTypes.GutsRate,
      ParamTypes.AutoJewel,
      ParamTypes.ChargeTimeRate,
      ParamTypes.CastTimeRate,
      ParamTypes.BuffTurn,
      ParamTypes.DebuffTurn,
      ParamTypes.CombinationRange,
      ParamTypes.HpCostRate,
      ParamTypes.SkillUseCount,
      ParamTypes.PoisonDamage,
      ParamTypes.PoisonTurn,
      ParamTypes.Resist_Slash,
      ParamTypes.Resist_Pierce,
      ParamTypes.Resist_Blow,
      ParamTypes.Resist_Shot,
      ParamTypes.Resist_Magic,
      ParamTypes.Resist_Reaction,
      ParamTypes.Resist_Jump,
      ParamTypes.Avoid_Slash,
      ParamTypes.Avoid_Pierce,
      ParamTypes.Avoid_Blow,
      ParamTypes.Avoid_Shot,
      ParamTypes.Avoid_Magic,
      ParamTypes.Avoid_Reaction,
      ParamTypes.Avoid_Jump,
      ParamTypes.GainJewelRate,
      ParamTypes.UsedJewel,
      ParamTypes.UnitDefenseFire,
      ParamTypes.UnitDefenseWater,
      ParamTypes.UnitDefenseWind,
      ParamTypes.UnitDefenseThunder,
      ParamTypes.UnitDefenseShine,
      ParamTypes.UnitDefenseDark
    };
    public OShort[] values = new OShort[BattleBonusParam.MAX_BATTLE_BONUS];

    public OShort this[BattleBonus type]
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
        OShort& local = @this.values[index];
        // ISSUE: explicit reference operation
        // ISSUE: explicit reference operation
        ^local = (OShort) ((int) (^local) + (int) src.values[index]);
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
        OShort& local = @this.values[index];
        // ISSUE: explicit reference operation
        // ISSUE: explicit reference operation
        ^local = (OShort) ((int) (^local) - (int) src.values[index]);
      }
    }

    public void AddRate(BattleBonusParam src)
    {
      for (int index = 0; index < this.values.Length; ++index)
      {
        // ISSUE: explicit reference operation
        // ISSUE: variable of a reference type
        OShort& local = @this.values[index];
        // ISSUE: explicit reference operation
        // ISSUE: explicit reference operation
        ^local = (OShort) ((int) (^local) + (int) this.values[index] * (int) src.values[index] / 100);
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

    public void ChoiceHighest(BattleBonusParam scale, BattleBonusParam base_status)
    {
      for (int index = 0; index < this.values.Length; ++index)
      {
        if ((int) this.values[index] < (int) scale.values[index] * (int) base_status.values[index] / 100)
          this.values[index] = (OShort) 0;
        else
          scale.values[index] = (OShort) 0;
      }
    }

    public void ChoiceLowest(BattleBonusParam scale, BattleBonusParam base_status)
    {
      for (int index = 0; index < this.values.Length; ++index)
      {
        if ((int) this.values[index] > (int) scale.values[index] * (int) base_status.values[index] / 100)
          this.values[index] = (OShort) 0;
        else
          scale.values[index] = (OShort) 0;
      }
    }

    public void AddConvRate(BattleBonusParam scale, BattleBonusParam base_status)
    {
      for (int index = 0; index < this.values.Length; ++index)
      {
        // ISSUE: explicit reference operation
        // ISSUE: variable of a reference type
        OShort& local = @this.values[index];
        // ISSUE: explicit reference operation
        // ISSUE: explicit reference operation
        ^local = (OShort) ((int) (^local) + (int) scale.values[index] * (int) base_status.values[index] / 100);
      }
    }

    public void SubConvRate(BattleBonusParam scale, BattleBonusParam base_status)
    {
      for (int index = 0; index < this.values.Length; ++index)
      {
        // ISSUE: explicit reference operation
        // ISSUE: variable of a reference type
        OShort& local = @this.values[index];
        // ISSUE: explicit reference operation
        // ISSUE: explicit reference operation
        ^local = (OShort) ((int) (^local) - (int) scale.values[index] * (int) base_status.values[index] / 100);
      }
    }

    public void Mul(int mul_val)
    {
      if (mul_val == 0)
        return;
      for (int index = 0; index < this.values.Length; ++index)
      {
        // ISSUE: explicit reference operation
        // ISSUE: variable of a reference type
        OShort& local = @this.values[index];
        // ISSUE: explicit reference operation
        // ISSUE: explicit reference operation
        ^local = (OShort) ((int) (^local) * mul_val);
      }
    }

    public void Div(int div_val)
    {
      if (div_val == 0)
        return;
      for (int index = 0; index < this.values.Length; ++index)
      {
        // ISSUE: explicit reference operation
        // ISSUE: variable of a reference type
        OShort& local = @this.values[index];
        // ISSUE: explicit reference operation
        // ISSUE: explicit reference operation
        ^local = (OShort) ((int) (^local) / div_val);
      }
    }

    public void Swap(BattleBonusParam src, bool is_rev)
    {
      for (int index = 0; index < this.values.Length; ++index)
      {
        GameUtility.swap<OShort>(ref this.values[index], ref src.values[index]);
        if (is_rev)
        {
          // ISSUE: explicit reference operation
          // ISSUE: variable of a reference type
          OShort& local1 = @this.values[index];
          // ISSUE: explicit reference operation
          // ISSUE: explicit reference operation
          ^local1 = (OShort) ((int) (^local1) * -1);
          // ISSUE: explicit reference operation
          // ISSUE: variable of a reference type
          OShort& local2 = @src.values[index];
          // ISSUE: explicit reference operation
          // ISSUE: explicit reference operation
          ^local2 = (OShort) ((int) (^local2) * -1);
        }
      }
    }

    public ParamTypes GetParamTypes(int index)
    {
      return BattleBonusParam.ConvertParamTypes[index];
    }
  }
}
