// Decompiled with JetBrains decompiler
// Type: SRPG.StatusParam
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 85BFDF7F-5712-4D45-9CD6-3465C703DFDF
// Assembly location: S:\Desktop\Assembly-CSharp.dll

using System;

namespace SRPG
{
  public class StatusParam
  {
    public static readonly int MAX_STATUS = Enum.GetNames(typeof (StatusTypes)).Length;
    public static readonly ParamTypes[] ConvertParamTypes = new ParamTypes[14]
    {
      ParamTypes.Hp,
      ParamTypes.Mp,
      ParamTypes.MpIni,
      ParamTypes.Atk,
      ParamTypes.Def,
      ParamTypes.Mag,
      ParamTypes.Mnd,
      ParamTypes.Rec,
      ParamTypes.Dex,
      ParamTypes.Spd,
      ParamTypes.Cri,
      ParamTypes.Luk,
      ParamTypes.Mov,
      ParamTypes.Jmp
    };
    public OInt values_hp = (OInt) 0;
    public OShort[] values = new OShort[StatusParam.MAX_STATUS - 1];

    public int Length
    {
      get
      {
        return StatusParam.MAX_STATUS;
      }
    }

    public OInt this[StatusTypes type]
    {
      get
      {
        if (type == StatusTypes.Hp)
          return this.hp;
        return (OInt) this.values[(int) (type - 1)];
      }
      set
      {
        if (type == StatusTypes.Hp)
          this.hp = value;
        else
          this.values[(int) (type - 1)] = (OShort) value;
      }
    }

    public OInt hp
    {
      get
      {
        return this.values_hp;
      }
      set
      {
        this.values_hp = value;
      }
    }

    public OShort mp
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

    public OShort imp
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

    public OShort atk
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

    public OShort def
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

    public OShort mag
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

    public OShort mnd
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

    public OShort rec
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

    public OShort dex
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

    public OShort spd
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

    public OShort cri
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

    public OShort luk
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

    public OShort mov
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

    public OShort jmp
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

    public int total
    {
      get
      {
        int num = 0;
        for (int index = 0; index < this.values.Length; ++index)
          num += (int) this.values[index];
        return num + (int) this.values_hp;
      }
    }

    public void Clear()
    {
      this.values_hp = (OInt) 0;
      Array.Clear((Array) this.values, 0, this.values.Length);
    }

    public void CopyTo(StatusParam dsc)
    {
      if (dsc == null)
        return;
      for (int index = 0; index < this.values.Length; ++index)
        dsc.values[index] = this.values[index];
      dsc.values_hp = this.values_hp;
    }

    public void Add(StatusParam src)
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
      StatusParam statusParam = this;
      statusParam.values_hp = (OInt) ((int) statusParam.values_hp + (int) src.values_hp);
    }

    public void Sub(StatusParam src)
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
      StatusParam statusParam = this;
      statusParam.values_hp = (OInt) ((int) statusParam.values_hp - (int) src.values_hp);
    }

    public void AddRate(StatusParam src)
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
      StatusParam statusParam = this;
      statusParam.values_hp = (OInt) ((int) statusParam.values_hp + (int) this.values_hp * (int) src.values_hp / 100);
    }

    public void ReplceHighest(StatusParam comp)
    {
      for (int index = 0; index < this.values.Length; ++index)
      {
        if ((int) this.values[index] < (int) comp.values[index])
          this.values[index] = comp.values[index];
      }
      if ((int) this.values_hp >= (int) comp.values_hp)
        return;
      this.values_hp = comp.values_hp;
    }

    public void ReplceLowest(StatusParam comp)
    {
      for (int index = 0; index < this.values.Length; ++index)
      {
        if ((int) this.values[index] > (int) comp.values[index])
          this.values[index] = comp.values[index];
      }
      if ((int) this.values_hp <= (int) comp.values_hp)
        return;
      this.values_hp = comp.values_hp;
    }

    public void ChoiceHighest(StatusParam scale, StatusParam base_status)
    {
      for (int index = 0; index < this.values.Length; ++index)
      {
        if ((int) this.values[index] < (int) scale.values[index] * (int) base_status.values[index] / 100)
          this.values[index] = (OShort) 0;
        else
          scale.values[index] = (OShort) 0;
      }
      if ((int) this.values_hp < (int) scale.values_hp * (int) base_status.values_hp / 100)
        this.values_hp = (OInt) 0;
      else
        scale.values_hp = (OInt) 0;
    }

    public void ChoiceLowest(StatusParam scale, StatusParam base_status)
    {
      for (int index = 0; index < this.values.Length; ++index)
      {
        if ((int) this.values[index] > (int) scale.values[index] * (int) base_status.values[index] / 100)
          this.values[index] = (OShort) 0;
        else
          scale.values[index] = (OShort) 0;
      }
      if ((int) this.values_hp > (int) scale.values_hp * (int) base_status.values_hp / 100)
        this.values_hp = (OInt) 0;
      else
        scale.values_hp = (OInt) 0;
    }

    public void ApplyMinVal()
    {
      for (int index = 0; index < this.values.Length; ++index)
      {
        switch (index)
        {
          case 1:
          case 6:
            continue;
          case 8:
            this.values[index] = (OShort) Math.Max((short) this.values[index], (short) 1);
            continue;
          default:
            this.values[index] = (OShort) Math.Max((short) this.values[index], (short) 0);
            continue;
        }
      }
      this.values_hp = (OInt) Math.Max((int) this.values_hp, 1);
    }

    public void AddConvRate(StatusParam scale, StatusParam base_status)
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
      StatusParam statusParam = this;
      statusParam.values_hp = (OInt) ((int) statusParam.values_hp + (int) scale.values_hp * (int) base_status.values_hp / 100);
    }

    public void SubConvRate(StatusParam scale, StatusParam base_status)
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
      StatusParam statusParam = this;
      statusParam.values_hp = (OInt) ((int) statusParam.values_hp - (int) scale.values_hp * (int) base_status.values_hp / 100);
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
      StatusParam statusParam = this;
      statusParam.values_hp = (OInt) ((int) statusParam.values_hp * mul_val);
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
      StatusParam statusParam = this;
      statusParam.values_hp = (OInt) ((int) statusParam.values_hp / div_val);
    }

    public void Swap(StatusParam src, bool is_rev)
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
      GameUtility.swap<OInt>(ref this.values_hp, ref src.values_hp);
      if (!is_rev)
        return;
      StatusParam statusParam1 = this;
      statusParam1.values_hp = (OInt) ((int) statusParam1.values_hp * -1);
      StatusParam statusParam2 = src;
      statusParam2.values_hp = (OInt) ((int) statusParam2.values_hp * -1);
    }

    public ParamTypes GetParamTypes(int index)
    {
      return StatusParam.ConvertParamTypes[index];
    }
  }
}
