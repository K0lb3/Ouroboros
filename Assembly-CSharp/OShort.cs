// Decompiled with JetBrains decompiler
// Type: OShort
// Assembly: Assembly-CSharp, Version=1.2.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9BA76916-D0BD-4DB6-A90B-FE0BCC53E511
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using CodeStage.AntiCheat.ObscuredTypes;

public struct OShort
{
  private ObscuredShort value;

  public OShort(short value)
  {
    this.value = (ObscuredShort) value;
  }

  public OShort(int value)
  {
    this.value = (ObscuredShort) ((short) value);
  }

  public override string ToString()
  {
    return this.value.ToString();
  }

  public static implicit operator OShort(short value)
  {
    return new OShort(value);
  }

  public static implicit operator short(OShort value)
  {
    return (short) value.value;
  }

  public static implicit operator int(OShort value)
  {
    return (int) (short) value.value;
  }

  public static implicit operator OShort(OInt value)
  {
    return new OShort((int) value);
  }

  public static implicit operator OShort(int value)
  {
    return new OShort(value);
  }

  public static OShort operator ++(OShort value)
  {
    return new OShort((short) ((int) value + 1));
  }

  public static OShort operator --(OShort value)
  {
    return new OShort((short) ((int) value - 1));
  }
}
