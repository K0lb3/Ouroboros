// Decompiled with JetBrains decompiler
// Type: SRPG.EnchantParam
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 85BFDF7F-5712-4D45-9CD6-3465C703DFDF
// Assembly location: S:\Desktop\Assembly-CSharp.dll

using System;

namespace SRPG
{
  public class EnchantParam
  {
    public static readonly int MAX_ENCHANGT = Enum.GetNames(typeof (EnchantTypes)).Length;
    public static readonly ParamTypes[] ConvertAssistParamTypes = new ParamTypes[37]
    {
      ParamTypes.Assist_Poison,
      ParamTypes.Assist_Paralysed,
      ParamTypes.Assist_Stun,
      ParamTypes.Assist_Sleep,
      ParamTypes.Assist_Charm,
      ParamTypes.Assist_Stone,
      ParamTypes.Assist_Blind,
      ParamTypes.Assist_DisableSkill,
      ParamTypes.Assist_DisableMove,
      ParamTypes.Assist_DisableAttack,
      ParamTypes.Assist_Zombie,
      ParamTypes.Assist_DeathSentence,
      ParamTypes.Assist_Berserk,
      ParamTypes.Assist_Knockback,
      ParamTypes.Assist_ResistBuff,
      ParamTypes.Assist_ResistDebuff,
      ParamTypes.Assist_Stun,
      ParamTypes.Assist_Fast,
      ParamTypes.Assist_Slow,
      ParamTypes.Assist_AutoHeal,
      ParamTypes.Assist_Donsoku,
      ParamTypes.Assist_Rage,
      ParamTypes.Assist_GoodSleep,
      ParamTypes.Assist_AutoJewel,
      ParamTypes.Assist_DisableHeal,
      ParamTypes.Assist_SingleAttack,
      ParamTypes.Assist_AreaAttack,
      ParamTypes.Assist_DecCT,
      ParamTypes.Assist_IncCT,
      ParamTypes.Assist_ESA_Fire,
      ParamTypes.Assist_ESA_Water,
      ParamTypes.Assist_ESA_Wind,
      ParamTypes.Assist_ESA_Thunder,
      ParamTypes.Assist_ESA_Shine,
      ParamTypes.Assist_ESA_Dark,
      ParamTypes.Assist_MaxDamageHp,
      ParamTypes.Assist_MaxDamageMp
    };
    public static readonly ParamTypes[] ConvertResistParamTypes = new ParamTypes[37]
    {
      ParamTypes.Resist_Poison,
      ParamTypes.Resist_Paralysed,
      ParamTypes.Resist_Stun,
      ParamTypes.Resist_Sleep,
      ParamTypes.Resist_Charm,
      ParamTypes.Resist_Stone,
      ParamTypes.Resist_Blind,
      ParamTypes.Resist_DisableSkill,
      ParamTypes.Resist_DisableMove,
      ParamTypes.Resist_DisableAttack,
      ParamTypes.Resist_Zombie,
      ParamTypes.Resist_DeathSentence,
      ParamTypes.Resist_Berserk,
      ParamTypes.Resist_Knockback,
      ParamTypes.Resist_ResistBuff,
      ParamTypes.Resist_ResistDebuff,
      ParamTypes.Resist_Stun,
      ParamTypes.Resist_Fast,
      ParamTypes.Resist_Slow,
      ParamTypes.Resist_AutoHeal,
      ParamTypes.Resist_Donsoku,
      ParamTypes.Resist_Rage,
      ParamTypes.Resist_GoodSleep,
      ParamTypes.Resist_AutoJewel,
      ParamTypes.Resist_DisableHeal,
      ParamTypes.Resist_SingleAttack,
      ParamTypes.Resist_AreaAttack,
      ParamTypes.Resist_DecCT,
      ParamTypes.Resist_IncCT,
      ParamTypes.Resist_ESA_Fire,
      ParamTypes.Resist_ESA_Water,
      ParamTypes.Resist_ESA_Wind,
      ParamTypes.Resist_ESA_Thunder,
      ParamTypes.Resist_ESA_Shine,
      ParamTypes.Resist_ESA_Dark,
      ParamTypes.Resist_MaxDamageHp,
      ParamTypes.Resist_MaxDamageMp
    };
    public OShort[] values = new OShort[EnchantParam.MAX_ENCHANGT];

    public OShort this[EnchantTypes type]
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

    public OShort poison
    {
      get
      {
        return this.values[0];
      }
      set
      {
        this.values[0] = value;
      }
    }

    public OShort paralyse
    {
      get
      {
        return this.values[1];
      }
      set
      {
        this.values[1] = value;
      }
    }

    public OShort stun
    {
      get
      {
        return this.values[2];
      }
      set
      {
        this.values[2] = value;
      }
    }

    public OShort sleep
    {
      get
      {
        return this.values[3];
      }
      set
      {
        this.values[3] = value;
      }
    }

    public OShort charm
    {
      get
      {
        return this.values[4];
      }
      set
      {
        this.values[4] = value;
      }
    }

    public OShort stone
    {
      get
      {
        return this.values[5];
      }
      set
      {
        this.values[5] = value;
      }
    }

    public OShort blind
    {
      get
      {
        return this.values[6];
      }
      set
      {
        this.values[6] = value;
      }
    }

    public OShort notskl
    {
      get
      {
        return this.values[7];
      }
      set
      {
        this.values[7] = value;
      }
    }

    public OShort notmov
    {
      get
      {
        return this.values[8];
      }
      set
      {
        this.values[8] = value;
      }
    }

    public OShort notatk
    {
      get
      {
        return this.values[9];
      }
      set
      {
        this.values[9] = value;
      }
    }

    public OShort zombie
    {
      get
      {
        return this.values[10];
      }
      set
      {
        this.values[10] = value;
      }
    }

    public OShort death
    {
      get
      {
        return this.values[11];
      }
      set
      {
        this.values[11] = value;
      }
    }

    public OShort berserk
    {
      get
      {
        return this.values[12];
      }
      set
      {
        this.values[12] = value;
      }
    }

    public OShort knockback
    {
      get
      {
        return this.values[13];
      }
      set
      {
        this.values[13] = value;
      }
    }

    public OShort resist_buff
    {
      get
      {
        return this.values[14];
      }
      set
      {
        this.values[14] = value;
      }
    }

    public OShort resist_debuff
    {
      get
      {
        return this.values[15];
      }
      set
      {
        this.values[15] = value;
      }
    }

    public OShort stop
    {
      get
      {
        return this.values[16];
      }
      set
      {
        this.values[16] = value;
      }
    }

    public OShort fast
    {
      get
      {
        return this.values[17];
      }
      set
      {
        this.values[17] = value;
      }
    }

    public OShort slow
    {
      get
      {
        return this.values[18];
      }
      set
      {
        this.values[18] = value;
      }
    }

    public OShort auto_heal
    {
      get
      {
        return this.values[19];
      }
      set
      {
        this.values[19] = value;
      }
    }

    public OShort donsoku
    {
      get
      {
        return this.values[20];
      }
      set
      {
        this.values[20] = value;
      }
    }

    public OShort rage
    {
      get
      {
        return this.values[21];
      }
      set
      {
        this.values[21] = value;
      }
    }

    public OShort good_sleep
    {
      get
      {
        return this.values[22];
      }
      set
      {
        this.values[22] = value;
      }
    }

    public OShort auto_jewel
    {
      get
      {
        return this.values[23];
      }
      set
      {
        this.values[23] = value;
      }
    }

    public OShort notheal
    {
      get
      {
        return this.values[24];
      }
      set
      {
        this.values[24] = value;
      }
    }

    public OShort single_attack
    {
      get
      {
        return this.values[25];
      }
      set
      {
        this.values[25] = value;
      }
    }

    public OShort area_attack
    {
      get
      {
        return this.values[26];
      }
      set
      {
        this.values[26] = value;
      }
    }

    public OShort dec_ct
    {
      get
      {
        return this.values[27];
      }
      set
      {
        this.values[27] = value;
      }
    }

    public OShort inc_ct
    {
      get
      {
        return this.values[28];
      }
      set
      {
        this.values[28] = value;
      }
    }

    public OShort esa_fire
    {
      get
      {
        return this.values[29];
      }
      set
      {
        this.values[29] = value;
      }
    }

    public OShort esa_water
    {
      get
      {
        return this.values[30];
      }
      set
      {
        this.values[30] = value;
      }
    }

    public OShort esa_wind
    {
      get
      {
        return this.values[31];
      }
      set
      {
        this.values[31] = value;
      }
    }

    public OShort esa_thunder
    {
      get
      {
        return this.values[32];
      }
      set
      {
        this.values[32] = value;
      }
    }

    public OShort esa_shine
    {
      get
      {
        return this.values[33];
      }
      set
      {
        this.values[33] = value;
      }
    }

    public OShort esa_dark
    {
      get
      {
        return this.values[34];
      }
      set
      {
        this.values[34] = value;
      }
    }

    public OShort max_damage_hp
    {
      get
      {
        return this.values[35];
      }
      set
      {
        this.values[35] = value;
      }
    }

    public OShort max_damage_mp
    {
      get
      {
        return this.values[36];
      }
      set
      {
        this.values[36] = value;
      }
    }

    public OShort this[EUnitCondition condition]
    {
      get
      {
        EUnitCondition eunitCondition = condition;
        if (eunitCondition >= EUnitCondition.Poison && eunitCondition <= EUnitCondition.Sleep)
        {
          switch (eunitCondition)
          {
            case EUnitCondition.Poison:
              return this.poison;
            case EUnitCondition.Paralysed:
              return this.paralyse;
            case EUnitCondition.Stun:
              return this.stun;
            case EUnitCondition.Sleep:
              return this.sleep;
          }
        }
        switch (eunitCondition)
        {
          case EUnitCondition.Charm:
            return this.charm;
          case EUnitCondition.Stone:
            return this.stone;
          case EUnitCondition.Blindness:
            return this.blind;
          case EUnitCondition.DisableSkill:
            return this.notskl;
          case EUnitCondition.DisableMove:
            return this.notmov;
          case EUnitCondition.DisableAttack:
            return this.notatk;
          case EUnitCondition.Zombie:
            return this.zombie;
          case EUnitCondition.DeathSentence:
            return this.death;
          case EUnitCondition.Berserk:
            return this.berserk;
          case EUnitCondition.Stop:
            return this.stop;
          case EUnitCondition.Fast:
            return this.fast;
          case EUnitCondition.Slow:
            return this.slow;
          case EUnitCondition.AutoHeal:
            return this.auto_heal;
          case EUnitCondition.Donsoku:
            return this.donsoku;
          case EUnitCondition.Rage:
            return this.rage;
          case EUnitCondition.GoodSleep:
            return this.good_sleep;
          case EUnitCondition.AutoJewel:
            return this.auto_jewel;
          case EUnitCondition.DisableHeal:
            return this.notheal;
          default:
            return (OShort) 0;
        }
      }
      set
      {
        EUnitCondition eunitCondition = condition;
        if (eunitCondition >= EUnitCondition.Poison && eunitCondition <= EUnitCondition.Sleep)
        {
          switch (eunitCondition)
          {
            case EUnitCondition.Poison:
              this.poison = value;
              return;
            case EUnitCondition.Paralysed:
              this.paralyse = value;
              return;
            case EUnitCondition.Stun:
              this.stun = value;
              return;
            case EUnitCondition.Sleep:
              this.sleep = value;
              return;
          }
        }
        switch (eunitCondition)
        {
          case EUnitCondition.Charm:
            this.charm = value;
            break;
          case EUnitCondition.Stone:
            this.stone = value;
            break;
          case EUnitCondition.Blindness:
            this.blind = value;
            break;
          case EUnitCondition.DisableSkill:
            this.notskl = value;
            break;
          case EUnitCondition.DisableMove:
            this.notmov = value;
            break;
          case EUnitCondition.DisableAttack:
            this.notatk = value;
            break;
          case EUnitCondition.Zombie:
            this.zombie = value;
            break;
          case EUnitCondition.DeathSentence:
            this.death = value;
            break;
          case EUnitCondition.Berserk:
            this.berserk = value;
            break;
          case EUnitCondition.Stop:
            this.stop = value;
            break;
          case EUnitCondition.Fast:
            this.fast = value;
            break;
          case EUnitCondition.Slow:
            this.slow = value;
            break;
          case EUnitCondition.AutoHeal:
            this.auto_heal = value;
            break;
          case EUnitCondition.Donsoku:
            this.donsoku = value;
            break;
          case EUnitCondition.Rage:
            this.rage = value;
            break;
          case EUnitCondition.GoodSleep:
            this.good_sleep = value;
            break;
          case EUnitCondition.AutoJewel:
            this.auto_jewel = value;
            break;
          case EUnitCondition.DisableHeal:
            this.notheal = value;
            break;
        }
      }
    }

    public void Clear()
    {
      Array.Clear((Array) this.values, 0, this.values.Length);
    }

    public void CopyTo(EnchantParam dsc)
    {
      if (dsc == null)
        return;
      for (int index = 0; index < this.values.Length; ++index)
        dsc.values[index] = this.values[index];
    }

    public void Add(EnchantParam src)
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

    public void Sub(EnchantParam src)
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

    public void AddRate(EnchantParam src)
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

    public void ReplceHighest(EnchantParam comp)
    {
      for (int index = 0; index < this.values.Length; ++index)
      {
        if ((int) this.values[index] < (int) comp.values[index])
          this.values[index] = comp.values[index];
      }
    }

    public void ReplceLowest(EnchantParam comp)
    {
      for (int index = 0; index < this.values.Length; ++index)
      {
        if ((int) this.values[index] > (int) comp.values[index])
          this.values[index] = comp.values[index];
      }
    }

    public void ChoiceHighest(EnchantParam scale, EnchantParam base_status)
    {
      for (int index = 0; index < this.values.Length; ++index)
      {
        if ((int) this.values[index] < (int) scale.values[index] * (int) base_status.values[index] / 100)
          this.values[index] = (OShort) 0;
        else
          scale.values[index] = (OShort) 0;
      }
    }

    public void ChoiceLowest(EnchantParam scale, EnchantParam base_status)
    {
      for (int index = 0; index < this.values.Length; ++index)
      {
        if ((int) this.values[index] > (int) scale.values[index] * (int) base_status.values[index] / 100)
          this.values[index] = (OShort) 0;
        else
          scale.values[index] = (OShort) 0;
      }
    }

    public void AddConvRate(EnchantParam scale, EnchantParam base_status)
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

    public void SubConvRate(EnchantParam scale, EnchantParam base_status)
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

    public void Swap(EnchantParam src, bool is_rev)
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

    public ParamTypes GetAssistParamTypes(int index)
    {
      return EnchantParam.ConvertAssistParamTypes[index];
    }

    public ParamTypes GetResistParamTypes(int index)
    {
      return EnchantParam.ConvertResistParamTypes[index];
    }
  }
}
