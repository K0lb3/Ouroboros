// Decompiled with JetBrains decompiler
// Type: SRPG.BuffBit
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 85BFDF7F-5712-4D45-9CD6-3465C703DFDF
// Assembly location: S:\Desktop\Assembly-CSharp.dll

using System;

namespace SRPG
{
  public class BuffBit
  {
    private static readonly int MaxBitArray = SkillParam.MAX_PARAMTYPES / 32 + 1;
    public int[] bits = new int[BuffBit.MaxBitArray];

    public void SetBit(ParamTypes type)
    {
      int num = (int) type;
      this.bits[num / 32] |= 1 << num % 32;
    }

    public void ResetBit(ParamTypes type)
    {
      int num = (int) type;
      this.bits[num / 32] &= ~(1 << num % 32);
    }

    public bool CheckBit(ParamTypes type)
    {
      int num = (int) type;
      return (this.bits[num / 32] & 1 << num % 32) != 0;
    }

    public void CopyTo(BuffBit dsc)
    {
      for (int index = 0; index < this.bits.Length; ++index)
        dsc.bits[index] = this.bits[index];
    }

    public void Clear()
    {
      Array.Clear((Array) this.bits, 0, this.bits.Length);
    }

    public bool CheckEffect()
    {
      for (int index = 0; index < this.bits.Length; ++index)
      {
        if (this.bits[index] != 0)
          return true;
      }
      return false;
    }
  }
}
