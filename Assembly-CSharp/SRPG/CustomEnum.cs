// Decompiled with JetBrains decompiler
// Type: SRPG.CustomEnum
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

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
