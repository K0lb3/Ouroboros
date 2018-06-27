// Decompiled with JetBrains decompiler
// Type: OInt
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
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
    // ISSUE: explicit reference operation
    // ISSUE: variable of a reference type
    OInt& local = @value;
    // ISSUE: explicit reference operation
    // ISSUE: explicit reference operation
    (^local).value = (ObscuredInt) ((int) (^local).value + 1);
    return value;
  }

  public static OInt operator --(OInt value)
  {
    // ISSUE: explicit reference operation
    // ISSUE: variable of a reference type
    OInt& local = @value;
    // ISSUE: explicit reference operation
    // ISSUE: explicit reference operation
    (^local).value = (ObscuredInt) ((int) (^local).value - 1);
    return value;
  }
}
