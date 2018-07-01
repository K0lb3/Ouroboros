// Decompiled with JetBrains decompiler
// Type: SRPG.StringIsResourcePath
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 85BFDF7F-5712-4D45-9CD6-3465C703DFDF
// Assembly location: S:\Desktop\Assembly-CSharp.dll

using UnityEngine;

namespace SRPG
{
  public class StringIsResourcePath : PropertyAttribute
  {
    public System.Type ResourceType;
    public string ParentDirectory;

    public StringIsResourcePath(System.Type type)
    {
      this.\u002Ector();
      this.ResourceType = type;
      this.ParentDirectory = (string) null;
    }

    public StringIsResourcePath(System.Type type, string dir)
    {
      this.\u002Ector();
      this.ResourceType = type;
      this.ParentDirectory = dir;
    }
  }
}
