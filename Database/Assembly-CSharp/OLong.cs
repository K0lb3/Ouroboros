// Decompiled with JetBrains decompiler
// Type: OLong
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
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
    // ISSUE: explicit reference operation
    // ISSUE: variable of a reference type
    OLong& local = @value;
    // ISSUE: explicit reference operation
    // ISSUE: explicit reference operation
    (^local).value = (ObscuredLong) ((long) (^local).value + 1L);
    return value;
  }

  public static OLong operator --(OLong value)
  {
    // ISSUE: explicit reference operation
    // ISSUE: variable of a reference type
    OLong& local = @value;
    // ISSUE: explicit reference operation
    // ISSUE: explicit reference operation
    (^local).value = (ObscuredLong) ((long) (^local).value - 1L);
    return value;
  }
}
