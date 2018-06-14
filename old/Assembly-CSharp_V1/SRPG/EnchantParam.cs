// Decompiled with JetBrains decompiler
// Type: SRPG.EnchantParam
// Assembly: Assembly-CSharp, Version=1.2.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9BA76916-D0BD-4DB6-A90B-FE0BCC53E511
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using System;

namespace SRPG
{
  public class EnchantParam
  {
    public static readonly OInt MAX_ENCHANGT = (OInt) Enum.GetNames(typeof (EnchantTypes)).Length;
    public static readonly ParamTypes[] ConvertAssistParamTypes = new ParamTypes[25]{ ParamTypes.Assist_Poison, ParamTypes.Assist_Paralysed, ParamTypes.Assist_Stun, ParamTypes.Assist_Sleep, ParamTypes.Assist_Charm, ParamTypes.Assist_Stone, ParamTypes.Assist_Blind, ParamTypes.Assist_DisableSkill, ParamTypes.Assist_DisableMove, ParamTypes.Assist_DisableAttack, ParamTypes.Assist_Zombie, ParamTypes.Assist_DeathSentence, ParamTypes.Assist_Berserk, ParamTypes.Assist_Knockback, ParamTypes.Assist_ResistBuff, ParamTypes.Assist_ResistDebuff, ParamTypes.Assist_Stun, ParamTypes.Assist_Fast, ParamTypes.Assist_Slow, ParamTypes.Assist_AutoHeal, ParamTypes.Assist_Donsoku, ParamTypes.Assist_Rage, ParamTypes.Assist_GoodSleep, ParamTypes.Assist_AutoJewel, ParamTypes.Assist_DisableHeal };
    public static readonly ParamTypes[] ConvertResistParamTypes = new ParamTypes[25]{ ParamTypes.Resist_Poison, ParamTypes.Resist_Paralysed, ParamTypes.Resist_Stun, ParamTypes.Resist_Sleep, ParamTypes.Resist_Charm, ParamTypes.Resist_Stone, ParamTypes.Resist_Blind, ParamTypes.Resist_DisableSkill, ParamTypes.Resist_DisableMove, ParamTypes.Resist_DisableAttack, ParamTypes.Resist_Zombie, ParamTypes.Resist_DeathSentence, ParamTypes.Resist_Berserk, ParamTypes.Resist_Knockback, ParamTypes.Resist_ResistBuff, ParamTypes.Resist_ResistDebuff, ParamTypes.Resist_Stun, ParamTypes.Resist_Fast, ParamTypes.Resist_Slow, ParamTypes.Resist_AutoHeal, ParamTypes.Resist_Donsoku, ParamTypes.Resist_Rage, ParamTypes.Resist_GoodSleep, ParamTypes.Resist_AutoJewel, ParamTypes.Resist_DisableHeal };
    public OInt[] values = new OInt[(int) EnchantParam.MAX_ENCHANGT];

    public OInt this[EnchantTypes type]
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

    public OInt poison
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

    public OInt paralyse
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

    public OInt stun
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

    public OInt sleep
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

    public OInt charm
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

    public OInt stone
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

    public OInt blind
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

    public OInt notskl
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

    public OInt notmov
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

    public OInt notatk
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

    public OInt zombie
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

    public OInt death
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

    public OInt berserk
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

    public OInt knockback
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

    public OInt resist_buff
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

    public OInt resist_debuff
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

    public OInt stop
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

    public OInt fast
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

    public OInt slow
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

    public OInt auto_heal
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

    public OInt donsoku
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

    public OInt rage
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

    public OInt good_sleep
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

    public OInt auto_jewel
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

    public OInt notheal
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

    public OInt this[EUnitCondition condition]
    {
      get
      {
        EUnitCondition eunitCondition = condition;
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
          default:
            if (eunitCondition == EUnitCondition.Charm)
              return this.charm;
            if (eunitCondition == EUnitCondition.Stone)
              return this.stone;
            if (eunitCondition == EUnitCondition.Blindness)
              return this.blind;
            if (eunitCondition == EUnitCondition.DisableSkill)
              return this.notskl;
            if (eunitCondition == EUnitCondition.DisableMove)
              return this.notmov;
            if (eunitCondition == EUnitCondition.DisableAttack)
              return this.notatk;
            if (eunitCondition == EUnitCondition.Zombie)
              return this.zombie;
            if (eunitCondition == EUnitCondition.DeathSentence)
              return this.death;
            if (eunitCondition == EUnitCondition.Berserk)
              return this.berserk;
            if (eunitCondition == EUnitCondition.Stop)
              return this.stop;
            if (eunitCondition == EUnitCondition.Fast)
              return this.fast;
            if (eunitCondition == EUnitCondition.Slow)
              return this.slow;
            if (eunitCondition == EUnitCondition.AutoHeal)
              return this.auto_heal;
            if (eunitCondition == EUnitCondition.Donsoku)
              return this.donsoku;
            if (eunitCondition == EUnitCondition.Rage)
              return this.rage;
            if (eunitCondition == EUnitCondition.GoodSleep)
              return this.good_sleep;
            if (eunitCondition == EUnitCondition.AutoJewel)
              return this.auto_jewel;
            if (eunitCondition == EUnitCondition.DisableHeal)
              return this.notheal;
            return (OInt) 0;
        }
      }
      set
      {
        EUnitCondition eunitCondition = condition;
        switch (eunitCondition)
        {
          case EUnitCondition.Poison:
            this.poison = value;
            break;
          case EUnitCondition.Paralysed:
            this.paralyse = value;
            break;
          case EUnitCondition.Stun:
            this.stun = value;
            break;
          case EUnitCondition.Sleep:
            this.sleep = value;
            break;
          default:
            if (eunitCondition != EUnitCondition.Charm)
            {
              if (eunitCondition != EUnitCondition.Stone)
              {
                if (eunitCondition != EUnitCondition.Blindness)
                {
                  if (eunitCondition != EUnitCondition.DisableSkill)
                  {
                    if (eunitCondition != EUnitCondition.DisableMove)
                    {
                      if (eunitCondition != EUnitCondition.DisableAttack)
                      {
                        if (eunitCondition != EUnitCondition.Zombie)
                        {
                          if (eunitCondition != EUnitCondition.DeathSentence)
                          {
                            if (eunitCondition != EUnitCondition.Berserk)
                            {
                              if (eunitCondition != EUnitCondition.Stop)
                              {
                                if (eunitCondition != EUnitCondition.Fast)
                                {
                                  if (eunitCondition != EUnitCondition.Slow)
                                  {
                                    if (eunitCondition != EUnitCondition.AutoHeal)
                                    {
                                      if (eunitCondition != EUnitCondition.Donsoku)
                                      {
                                        if (eunitCondition != EUnitCondition.Rage)
                                        {
                                          if (eunitCondition != EUnitCondition.GoodSleep)
                                          {
                                            if (eunitCondition != EUnitCondition.AutoJewel)
                                            {
                                              if (eunitCondition != EUnitCondition.DisableHeal)
                                                break;
                                              this.notheal = value;
                                              break;
                                            }
                                            this.auto_jewel = value;
                                            break;
                                          }
                                          this.good_sleep = value;
                                          break;
                                        }
                                        this.rage = value;
                                        break;
                                      }
                                      this.donsoku = value;
                                      break;
                                    }
                                    this.auto_heal = value;
                                    break;
                                  }
                                  this.slow = value;
                                  break;
                                }
                                this.fast = value;
                                break;
                              }
                              this.stop = value;
                              break;
                            }
                            this.berserk = value;
                            break;
                          }
                          this.death = value;
                          break;
                        }
                        this.zombie = value;
                        break;
                      }
                      this.notatk = value;
                      break;
                    }
                    this.notmov = value;
                    break;
                  }
                  this.notskl = value;
                  break;
                }
                this.blind = value;
                break;
              }
              this.stone = value;
              break;
            }
            this.charm = value;
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
        OInt& local = @this.values[index];
        // ISSUE: explicit reference operation
        // ISSUE: explicit reference operation
        ^local = (OInt) ((int) (^local) + (int) src.values[index]);
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
        OInt& local = @this.values[index];
        // ISSUE: explicit reference operation
        // ISSUE: explicit reference operation
        ^local = (OInt) ((int) (^local) - (int) src.values[index]);
      }
    }

    public void AddRate(EnchantParam src)
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
