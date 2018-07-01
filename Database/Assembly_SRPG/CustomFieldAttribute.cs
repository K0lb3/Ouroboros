// Decompiled with JetBrains decompiler
// Type: SRPG.CustomFieldAttribute
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using System;

namespace SRPG
{
  public class CustomFieldAttribute : Attribute
  {
    public CustomFieldAttribute(string _text, CustomFieldAttribute.Type _type)
    {
    }

    public enum Type
    {
      MonoBehaviour,
      GameObject,
      UIText,
      UIRawImage,
      UIImage,
      UISprite,
    }
  }
}
