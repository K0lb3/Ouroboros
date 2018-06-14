// Decompiled with JetBrains decompiler
// Type: GR.RandXorshift
// Assembly: Assembly-CSharp, Version=1.2.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9BA76916-D0BD-4DB6-A90B-FE0BCC53E511
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using System;

namespace GR
{
  public class RandXorshift
  {
    private uint[] mSeed = new uint[4];
    public string mName = string.Empty;

    public RandXorshift(string name = "unknown")
    {
      this.mName = name;
      this.Reset();
    }

    public RandXorshift Clone()
    {
      RandXorshift randXorshift = new RandXorshift(this.mName);
      for (int index = 0; index < randXorshift.mSeed.Length; ++index)
        randXorshift.mSeed[index] = this.mSeed[index];
      return randXorshift;
    }

    public void Reset()
    {
      this.mSeed[0] = 123456789U;
      this.mSeed[1] = 362436069U;
      this.mSeed[2] = 521288629U;
      this.mSeed[3] = 88675123U;
    }

    public void Seed(uint seed)
    {
      uint num = seed;
      for (uint index = 0; index < 4U; ++index)
        this.mSeed[(IntPtr) index] = num = (uint) (1812433253 * ((int) num ^ (int) (num >> 30))) + index;
    }

    public uint Get()
    {
      uint num = this.mSeed[0] ^ this.mSeed[0] << 11;
      this.mSeed[0] = this.mSeed[1];
      this.mSeed[1] = this.mSeed[2];
      this.mSeed[2] = this.mSeed[3];
      this.mSeed[3] = (uint) ((int) this.mSeed[3] ^ (int) (this.mSeed[3] >> 19) ^ ((int) num ^ (int) (num >> 8)));
      return this.mSeed[3];
    }

    public uint[] GetSeed()
    {
      return this.mSeed;
    }

    public void SetSeed(int index, uint seed)
    {
      switch (index)
      {
        case 0:
        case 1:
        case 2:
        case 3:
          this.mSeed[index] = seed;
          break;
      }
    }
  }
}
