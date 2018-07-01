// Decompiled with JetBrains decompiler
// Type: SRPG.CustomFieldAttribute
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 85BFDF7F-5712-4D45-9CD6-3465C703DFDF
// Assembly location: S:\Desktop\Assembly-CSharp.dll

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
