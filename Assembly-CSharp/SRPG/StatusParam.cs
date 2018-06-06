// Decompiled with JetBrains decompiler
// Type: SRPG.StatusParam
// Assembly: Assembly-CSharp, Version=1.2.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9BA76916-D0BD-4DB6-A90B-FE0BCC53E511
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using System;

namespace SRPG
{
  public class StatusParam
  {
    public static readonly OInt MAX_STATUS = (OInt) Enum.GetNames(typeof (StatusTypes)).Length;
    public static readonly ParamTypes[] ConvertParamTypes = new ParamTypes[14]{ ParamTypes.Hp, ParamTypes.Mp, ParamTypes.MpIni, ParamTypes.Atk, ParamTypes.Def, ParamTypes.Mag, ParamTypes.Mnd, ParamTypes.Rec, ParamTypes.Dex, ParamTypes.Spd, ParamTypes.Cri, ParamTypes.Luk, ParamTypes.Mov, ParamTypes.Jmp };
    public OInt values_hp = (OInt) 0;
    public OShort[] values = new OShort[(int) StatusParam.MAX_STATUS - 1];

    public int Length
    {
      get
      {
        return (int) StatusParam.MAX_STATUS;
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

    public ParamTypes GetParamTypes(int index)
    {
      return StatusParam.ConvertParamTypes[index];
    }
  }
}
