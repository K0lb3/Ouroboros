// Decompiled with JetBrains decompiler
// Type: OBool
// Assembly: Assembly-CSharp, Version=1.2.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9BA76916-D0BD-4DB6-A90B-FE0BCC53E511
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
