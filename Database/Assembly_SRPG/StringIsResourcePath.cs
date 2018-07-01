// Decompiled with JetBrains decompiler
// Type: SRPG.StringIsResourcePath
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

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
