// Decompiled with JetBrains decompiler
// Type: SRPG.StringIsResourcePathPopup
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 85BFDF7F-5712-4D45-9CD6-3465C703DFDF
// Assembly location: S:\Desktop\Assembly-CSharp.dll

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
