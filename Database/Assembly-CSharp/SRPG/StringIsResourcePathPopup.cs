// Decompiled with JetBrains decompiler
// Type: SRPG.StringIsResourcePathPopup
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using UnityEngine;

namespace SRPG
{
  public class StringIsResourcePathPopup : PropertyAttribute
  {
    public System.Type ResourceType;
    public string ParentDirectory;

    public StringIsResourcePathPopup(System.Type type)
    {
      this.\u002Ector();
      this.ResourceType = type;
      this.ParentDirectory = (string) null;
    }

    public StringIsResourcePathPopup(System.Type type, string dir)
    {
      this.\u002Ector();
      this.ResourceType = type;
      this.ParentDirectory = dir;
    }
  }
}
