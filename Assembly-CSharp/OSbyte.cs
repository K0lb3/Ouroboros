// Decompiled with JetBrains decompiler
// Type: OSbyte
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
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
    // ISSUE: explicit reference operation
    // ISSUE: variable of a reference type
    OSbyte& local = @value;
    // ISSUE: explicit reference operation
    // ISSUE: explicit reference operation
    (^local).value = (ObscuredSByte) ((sbyte) ((int) (sbyte) (^local).value + 1));
    return value;
  }

  public static OSbyte operator --(OSbyte value)
  {
    // ISSUE: explicit reference operation
    // ISSUE: variable of a reference type
    OSbyte& local = @value;
    // ISSUE: explicit reference operation
    // ISSUE: explicit reference operation
    (^local).value = (ObscuredSByte) ((sbyte) ((int) (sbyte) (^local).value - 1));
    return value;
  }
}
