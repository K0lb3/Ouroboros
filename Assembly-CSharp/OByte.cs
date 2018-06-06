// Decompiled with JetBrains decompiler
// Type: OByte
// Assembly: Assembly-CSharp, Version=1.2.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9BA76916-D0BD-4DB6-A90B-FE0BCC53E511
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using CodeStage.AntiCheat.ObscuredTypes;

public struct OByte
{
  private ObscuredByte value;

  public OByte(byte value)
  {
    this.value = (ObscuredByte) value;
  }

  public override string ToString()
  {
    return this.value.ToString();
  }

  public static implicit operator OByte(byte value)
  {
    return new OByte(value);
  }

  public static implicit operator byte(OByte value)
  {
    return (byte) value.value;
  }

  public static OByte operator ++(OByte value)
  {
    return new OByte((byte) ((uint) (byte) value + 1U));
  }

  public static OByte operator --(OByte value)
  {
    return new OByte((byte) ((uint) (byte) value - 1U));
  }
}
