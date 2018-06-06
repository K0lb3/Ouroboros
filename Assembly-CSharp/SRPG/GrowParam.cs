// Decompiled with JetBrains decompiler
// Type: SRPG.GrowParam
// Assembly: Assembly-CSharp, Version=1.2.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9BA76916-D0BD-4DB6-A90B-FE0BCC53E511
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using System.Collections.Generic;

namespace SRPG
{
  public class GrowParam
  {
    public string type;
    public List<GrowSample> curve;

    public bool Deserialize(JSON_GrowParam json)
    {
      if (json == null)
        return false;
      this.type = json.type;
      if (json.curve != null)
      {
        int length = json.curve.Length;
        this.curve = new List<GrowSample>(length);
        for (int index = 0; index < length; ++index)
          this.curve.Add(new GrowSample()
          {
            lv = (OInt) json.curve[index].lv,
            scale = (OInt) json.curve[index].val,
            status = {
              param = {
                hp = (OInt) json.curve[index].hp,
                mp = (OShort) json.curve[index].mp,
                atk = (OShort) json.curve[index].atk,
                def = (OShort) json.curve[index].def,
                mag = (OShort) json.curve[index].mag,
                mnd = (OShort) json.curve[index].mnd,
                dex = (OShort) json.curve[index].dex,
                spd = (OShort) json.curve[index].spd,
                cri = (OShort) json.curve[index].cri,
                luk = (OShort) json.curve[index].luk
              },
              element_assist = {
                fire = (OShort) json.curve[index].afi,
                water = (OShort) json.curve[index].awa,
                wind = (OShort) json.curve[index].awi,
                thunder = (OShort) json.curve[index].ath,
                shine = (OShort) json.curve[index].ash,
                dark = (OShort) json.curve[index].ada
              },
              element_resist = {
                fire = (OShort) json.curve[index].rfi,
                water = (OShort) json.curve[index].rwa,
                wind = (OShort) json.curve[index].rwi,
                thunder = (OShort) json.curve[index].rth,
                shine = (OShort) json.curve[index].rsh,
                dark = (OShort) json.curve[index].rda
              },
              enchant_assist = {
                poison = (OInt) json.curve[index].apo,
                paralyse = (OInt) json.curve[index].apa,
                stun = (OInt) json.curve[index].ast,
                sleep = (OInt) json.curve[index].asl,
                charm = (OInt) json.curve[index].ach,
                stone = (OInt) json.curve[index].asn,
                blind = (OInt) json.curve[index].abl,
                notskl = (OInt) json.curve[index].ans,
                notmov = (OInt) json.curve[index].anm,
                notatk = (OInt) json.curve[index].ana,
                zombie = (OInt) json.curve[index].azo,
                death = (OInt) json.curve[index].ade,
                knockback = (OInt) json.curve[index].akn,
                berserk = (OInt) json.curve[index].abe,
                resist_buff = (OInt) json.curve[index].abf,
                resist_debuff = (OInt) json.curve[index].adf
              },
              enchant_resist = {
                poison = (OInt) json.curve[index].rpo,
                paralyse = (OInt) json.curve[index].rpa,
                stun = (OInt) json.curve[index].rst,
                sleep = (OInt) json.curve[index].rsl,
                charm = (OInt) json.curve[index].rch,
                stone = (OInt) json.curve[index].rsn,
                blind = (OInt) json.curve[index].rbl,
                notskl = (OInt) json.curve[index].rns,
                notmov = (OInt) json.curve[index].rnm,
                notatk = (OInt) json.curve[index].rna,
                zombie = (OInt) json.curve[index].rzo,
                death = (OInt) json.curve[index].rde,
                knockback = (OInt) json.curve[index].rkn,
                berserk = (OInt) json.curve[index].rbe,
                resist_buff = (OInt) json.curve[index].rbf,
                resist_debuff = (OInt) json.curve[index].rdf
              }
            }
          });
      }
      return true;
    }

    public int GetLevelCap()
    {
      if (this.curve != null && this.curve.Count > 0)
        return (int) this.curve[this.curve.Count - 1].lv;
      return 0;
    }

    public void CalcLevelCurveStatus(int rank, ref BaseStatus result, BaseStatus ini_status, BaseStatus max_status)
    {
      int num1 = this.GetLevelCap() - 1;
      int num2 = rank - 1;
      ini_status.CopyTo(result);
      if (num2 < 1 || num1 < 1)
        return;
      if (num2 >= num1)
      {
        max_status.CopyTo(result);
      }
      else
      {
        BaseStatus baseStatus1 = new BaseStatus();
        BaseStatus baseStatus2 = new BaseStatus();
        for (int index1 = 0; index1 < this.curve.Count; ++index1)
        {
          long num3 = (long) ((int) this.curve[index1].lv - 1);
          for (int index2 = index1; index2 > 0; --index2)
            num3 -= (long) (int) this.curve[index2 - 1].lv;
          long num4 = (long) num2 >= num3 ? num3 : (long) num2;
          StatusParam statusParam1 = ini_status.param;
          StatusParam statusParam2 = max_status.param;
          StatusParam src = this.curve[index1].status.param;
          src.CopyTo(baseStatus1.param);
          baseStatus1.param.Sub(baseStatus2.param);
          baseStatus2.param.Add(src);
          for (int index2 = 0; index2 < baseStatus1.param.Length; ++index2)
          {
            long num5 = (long) (((int) statusParam2[(StatusTypes) index2] - (int) statusParam1[(StatusTypes) index2]) * (int) baseStatus1.param[(StatusTypes) index2] / 100);
            if (num5 != 0L)
            {
              StatusParam statusParam3;
              StatusTypes index3;
              (statusParam3 = result.param)[index3 = (StatusTypes) index2] = (OInt) ((int) statusParam3[index3] + (int) (100000L * num5 / num3 * num4 / 100000L));
            }
          }
          ElementParam elementAssist1 = ini_status.element_assist;
          ElementParam elementAssist2 = max_status.element_assist;
          ElementParam elementAssist3 = this.curve[index1].status.element_assist;
          elementAssist3.CopyTo(baseStatus1.element_assist);
          baseStatus1.element_assist.Sub(baseStatus2.element_assist);
          baseStatus2.element_assist.Add(elementAssist3);
          for (int index2 = 0; index2 < baseStatus1.element_assist.values.Length; ++index2)
          {
            long num5 = (long) (((int) elementAssist2.values[index2] - (int) elementAssist1.values[index2]) * (int) baseStatus1.element_assist.values[index2] / 100);
            if (num5 != 0L)
            {
              // ISSUE: explicit reference operation
              // ISSUE: variable of a reference type
              OShort& local = @result.element_assist.values[index2];
              // ISSUE: explicit reference operation
              // ISSUE: explicit reference operation
              ^local = (OShort) ((int) (^local) + (int) (100000L * num5 / num3 * num4 / 100000L));
            }
          }
          ElementParam elementResist1 = ini_status.element_resist;
          ElementParam elementResist2 = max_status.element_resist;
          ElementParam elementResist3 = this.curve[index1].status.element_resist;
          elementResist3.CopyTo(baseStatus1.element_resist);
          baseStatus1.element_resist.Sub(baseStatus2.element_resist);
          baseStatus2.element_resist.Add(elementResist3);
          for (int index2 = 0; index2 < baseStatus1.element_resist.values.Length; ++index2)
          {
            long num5 = (long) (((int) elementResist2.values[index2] - (int) elementResist1.values[index2]) * (int) baseStatus1.element_resist.values[index2] / 100);
            if (num5 != 0L)
            {
              // ISSUE: explicit reference operation
              // ISSUE: variable of a reference type
              OShort& local = @result.element_resist.values[index2];
              // ISSUE: explicit reference operation
              // ISSUE: explicit reference operation
              ^local = (OShort) ((int) (^local) + (int) (100000L * num5 / num3 * num4 / 100000L));
            }
          }
          EnchantParam enchantAssist1 = ini_status.enchant_assist;
          EnchantParam enchantAssist2 = max_status.enchant_assist;
          EnchantParam enchantAssist3 = this.curve[index1].status.enchant_assist;
          enchantAssist3.CopyTo(baseStatus1.enchant_assist);
          baseStatus1.enchant_assist.Sub(baseStatus2.enchant_assist);
          baseStatus2.enchant_assist.Add(enchantAssist3);
          for (int index2 = 0; index2 < baseStatus1.enchant_assist.values.Length; ++index2)
          {
            long num5 = (long) (((int) enchantAssist2.values[index2] - (int) enchantAssist1.values[index2]) * (int) baseStatus1.enchant_assist.values[index2] / 100);
            if (num5 != 0L)
            {
              // ISSUE: explicit reference operation
              // ISSUE: variable of a reference type
              OInt& local = @result.enchant_assist.values[index2];
              // ISSUE: explicit reference operation
              // ISSUE: explicit reference operation
              ^local = (OInt) ((int) (^local) + (int) (100000L * num5 / num3 * num4 / 100000L));
            }
          }
          EnchantParam enchantResist1 = ini_status.enchant_resist;
          EnchantParam enchantResist2 = max_status.enchant_resist;
          EnchantParam enchantResist3 = this.curve[index1].status.enchant_resist;
          enchantResist3.CopyTo(baseStatus1.enchant_resist);
          baseStatus1.enchant_resist.Sub(baseStatus2.enchant_resist);
          baseStatus2.enchant_resist.Add(enchantResist3);
          for (int index2 = 0; index2 < baseStatus1.enchant_resist.values.Length; ++index2)
          {
            long num5 = (long) (((int) enchantResist2.values[index2] - (int) enchantResist1.values[index2]) * (int) baseStatus1.enchant_resist.values[index2] / 100);
            if (num5 != 0L)
            {
              // ISSUE: explicit reference operation
              // ISSUE: variable of a reference type
              OInt& local = @result.enchant_resist.values[index2];
              // ISSUE: explicit reference operation
              // ISSUE: explicit reference operation
              ^local = (OInt) ((int) (^local) + (int) (100000L * num5 / num3 * num4 / 100000L));
            }
          }
          if (rank <= (int) this.curve[index1].lv)
            break;
          num2 = rank - (int) this.curve[index1].lv - 1;
        }
      }
    }

    public int CalcLevelCurveValue(int rank, int ini, int max)
    {
      int num1 = this.GetLevelCap() - 1;
      int num2 = rank - 1;
      if (ini == max || num2 < 1 || num1 < 1)
        return ini;
      if (num2 >= num1)
        return max;
      int num3 = 0;
      int num4 = ini;
      for (int index1 = 0; index1 < this.curve.Count; ++index1)
      {
        long num5 = (long) ((int) this.curve[index1].lv - 1);
        for (int index2 = index1; index2 > 0; --index2)
          num5 -= (long) (int) this.curve[index2 - 1].lv;
        long num6 = (long) num2 >= num5 ? num5 : (long) num2;
        int num7 = (int) this.curve[index1].scale - num3;
        num3 += num7;
        long num8 = (long) ((max - ini) * num7 / 100);
        if (num8 != 0L)
          num4 += (int) (100000L * num8 / num5 * num6 / 100000L);
        if (rank > (int) this.curve[index1].lv)
          num2 = rank - (int) this.curve[index1].lv - 1;
        else
          break;
      }
      return num4;
    }
  }
}
