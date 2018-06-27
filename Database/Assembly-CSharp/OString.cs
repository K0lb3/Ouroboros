// Decompiled with JetBrains decompiler
// Type: OString
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using CodeStage.AntiCheat.ObscuredTypes;

public struct OString
{
  private ObscuredString value;

  public OString(string value)
  {
    this.value = (ObscuredString) value;
  }

  public override string ToString()
  {
    return this.value.ToString();
  }

  public static implicit operator OString(string value)
  {
    return new OString(value);
  }

  public static implicit operator string(OString value)
  {
    return (string) value.value;
  }
}
