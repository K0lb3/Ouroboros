// Decompiled with JetBrains decompiler
// Type: OBool
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using CodeStage.AntiCheat.ObscuredTypes;

public struct OBool
{
  private ObscuredBool value;

  public OBool(bool value)
  {
    this.value = (ObscuredBool) value;
  }

  public override string ToString()
  {
    return this.value.ToString();
  }

  public static implicit operator OBool(bool value)
  {
    return new OBool(value);
  }

  public static implicit operator bool(OBool value)
  {
    return (bool) value.value;
  }
}
