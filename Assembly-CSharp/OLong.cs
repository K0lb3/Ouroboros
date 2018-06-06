// Decompiled with JetBrains decompiler
// Type: OLong
// Assembly: Assembly-CSharp, Version=1.2.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9BA76916-D0BD-4DB6-A90B-FE0BCC53E511
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using CodeStage.AntiCheat.ObscuredTypes;

public struct OLong
{
  private ObscuredLong value;

  public OLong(long value)
  {
    this.value = (ObscuredLong) value;
  }

  public override string ToString()
  {
    return this.value.ToString();
  }

  public static implicit operator OLong(long value)
  {
    return new OLong(value);
  }

  public static implicit operator long(OLong value)
  {
    return (long) value.value;
  }

  public static OLong operator ++(OLong value)
  {
    return new OLong((long) value + 1L);
  }

  public static OLong operator --(OLong value)
  {
    return new OLong((long) value - 1L);
  }
}
