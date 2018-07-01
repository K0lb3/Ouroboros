// Decompiled with JetBrains decompiler
// Type: SRPG.StringIsTextID
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 85BFDF7F-5712-4D45-9CD6-3465C703DFDF
// Assembly location: S:\Desktop\Assembly-CSharp.dll

using UnityEngine;

namespace SRPG
{
  public class StringIsTextID : PropertyAttribute
  {
    public bool ContainsVoiceID;

    public StringIsTextID(bool containsVoiceID = false)
    {
      this.\u002Ector();
      this.ContainsVoiceID = containsVoiceID;
    }
  }
}
