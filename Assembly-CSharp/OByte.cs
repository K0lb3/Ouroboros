// Decompiled with JetBrains decompiler
// Type: OByte
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
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
    // ISSUE: explicit reference operation
    // ISSUE: variable of a reference type
    OByte& local = @value;
    // ISSUE: explicit reference operation
    // ISSUE: explicit reference operation
    (^local).value = (ObscuredByte) ((byte) ((uint) (byte) (^local).value + 1U));
    return value;
  }

  public static OByte operator --(OByte value)
  {
    // ISSUE: explicit reference operation
    // ISSUE: variable of a reference type
    OByte& local = @value;
    // ISSUE: explicit reference operation
    // ISSUE: explicit reference operation
    (^local).value = (ObscuredByte) ((byte) ((uint) (byte) (^local).value - 1U));
    return value;
  }
}
