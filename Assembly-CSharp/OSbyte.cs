// Decompiled with JetBrains decompiler
// Type: OSbyte
// Assembly: Assembly-CSharp, Version=1.2.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9BA76916-D0BD-4DB6-A90B-FE0BCC53E511
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using CodeStage.AntiCheat.ObscuredTypes;

public struct OSbyte
{
  private ObscuredSByte value;

  public OSbyte(sbyte value)
  {
    this.value = (ObscuredSByte) value;
  }

  public override string ToString()
  {
    return this.value.ToString();
  }

  public static implicit operator OSbyte(sbyte value)
  {
    return new OSbyte(value);
  }

  public static implicit operator sbyte(OSbyte value)
  {
    return (sbyte) value.value;
  }

  public static OSbyte operator ++(OSbyte value)
  {
    return new OSbyte((sbyte) ((int) (sbyte) value + 1));
  }

  public static OSbyte operator --(OSbyte value)
  {
    return new OSbyte((sbyte) ((int) (sbyte) value - 1));
  }
}
