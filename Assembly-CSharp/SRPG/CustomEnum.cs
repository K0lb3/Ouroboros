// Decompiled with JetBrains decompiler
// Type: SRPG.CustomEnum
// Assembly: Assembly-CSharp, Version=1.2.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9BA76916-D0BD-4DB6-A90B-FE0BCC53E511
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
