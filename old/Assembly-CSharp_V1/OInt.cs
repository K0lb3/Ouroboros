// Decompiled with JetBrains decompiler
// Type: OInt
// Assembly: Assembly-CSharp, Version=1.2.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9BA76916-D0BD-4DB6-A90B-FE0BCC53E511
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using CodeStage.AntiCheat.ObscuredTypes;

public struct OInt
{
  private ObscuredInt value;

  public OInt(int value)
  {
    this.value = (ObscuredInt) value;
  }

  public override string ToString()
  {
    return this.value.ToString();
  }

  public static implicit operator OInt(int value)
  {
    return new OInt(value);
  }

  public static implicit operator int(OInt value)
  {
    return (int) value.value;
  }

  public static implicit operator OInt(short value)
  {
    return new OInt((int) value);
  }

  public static implicit operator OInt(OShort value)
  {
    return new OInt((int) value);
  }

  public static implicit operator OInt(sbyte value)
  {
    return new OInt((int) value);
  }

  public static implicit operator OInt(OSbyte value)
  {
    return new OInt((int) (sbyte) value);
  }

  public static OInt operator ++(OInt value)
  {
    return new OInt((int) value + 1);
  }

  public static OInt operator --(OInt value)
  {
    return new OInt((int) value - 1);
  }
}
