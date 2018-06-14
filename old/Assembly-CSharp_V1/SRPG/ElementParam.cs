// Decompiled with JetBrains decompiler
// Type: SRPG.ElementParam
// Assembly: Assembly-CSharp, Version=1.2.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9BA76916-D0BD-4DB6-A90B-FE0BCC53E511
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using System;

namespace SRPG
{
  public class ElementParam
  {
    public static readonly OInt MAX_ELEMENT = (OInt) Enum.GetNames(typeof (EElement)).Length;
    public static readonly ParamTypes[] ConvertAssistParamTypes = new ParamTypes[7]{ ParamTypes.None, ParamTypes.Assist_Fire, ParamTypes.Assist_Water, ParamTypes.Assist_Wind, ParamTypes.Assist_Thunder, ParamTypes.Assist_Shine, ParamTypes.Assist_Dark };
    public static readonly ParamTypes[] ConvertResistParamTypes = new ParamTypes[7]{ ParamTypes.None, ParamTypes.Resist_Fire, ParamTypes.Resist_Water, ParamTypes.Resist_Wind, ParamTypes.Resist_Thunder, ParamTypes.Resist_Shine, ParamTypes.Resist_Dark };
    public OShort[] values = new OShort[(int) ElementParam.MAX_ELEMENT];

    public OShort this[EElement type]
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

    public OShort fire
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

    public OShort water
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

    public OShort wind
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

    public OShort thunder
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

    public OShort shine
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

    public OShort dark
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

    public void Clear()
    {
      Array.Clear((Array) this.values, 0, this.values.Length);
    }

    public void CopyTo(ElementParam dsc)
    {
      if (dsc == null)
        return;
      for (int index = 0; index < this.values.Length; ++index)
        dsc.values[index] = this.values[index];
    }

    public void Add(ElementParam src)
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

    public void Sub(ElementParam src)
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

    public void AddRate(ElementParam src)
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

    public void ReplceHighest(ElementParam comp)
    {
      for (int index = 0; index < this.values.Length; ++index)
      {
        if ((int) this.values[index] < (int) comp.values[index])
          this.values[index] = comp.values[index];
      }
    }

    public void ReplceLowest(ElementParam comp)
    {
      for (int index = 0; index < this.values.Length; ++index)
      {
        if ((int) this.values[index] > (int) comp.values[index])
          this.values[index] = comp.values[index];
      }
    }

    public ParamTypes GetAssistParamTypes(int index)
    {
      return ElementParam.ConvertAssistParamTypes[index];
    }

    public ParamTypes GetResistParamTypes(int index)
    {
      return ElementParam.ConvertResistParamTypes[index];
    }
  }
}
