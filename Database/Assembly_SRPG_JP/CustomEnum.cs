// Decompiled with JetBrains decompiler
// Type: SRPG.CustomEnum
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 85BFDF7F-5712-4D45-9CD6-3465C703DFDF
// Assembly location: S:\Desktop\Assembly-CSharp.dll

using UnityEngine;

namespace SRPG
{
  public class CustomEnum : PropertyAttribute
  {
    public System.Type EnumType;
    public int DefaultValue;

    public CustomEnum(System.Type enumType, int defaultValue)
    {
      this.\u002Ector();
      this.EnumType = enumType;
      this.DefaultValue = defaultValue;
    }
  }
}
