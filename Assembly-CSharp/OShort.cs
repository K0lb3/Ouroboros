// Decompiled with JetBrains decompiler
// Type: OShort
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
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
    // ISSUE: explicit reference operation
    // ISSUE: variable of a reference type
    OShort& local = @value;
    // ISSUE: explicit reference operation
    // ISSUE: explicit reference operation
    (^local).value = (ObscuredShort) ((short) ((int) (short) (^local).value + 1));
    return value;
  }

  public static OShort operator --(OShort value)
  {
    // ISSUE: explicit reference operation
    // ISSUE: variable of a reference type
    OShort& local = @value;
    // ISSUE: explicit reference operation
    // ISSUE: explicit reference operation
    (^local).value = (ObscuredShort) ((short) ((int) (short) (^local).value - 1));
    return value;
  }
}
