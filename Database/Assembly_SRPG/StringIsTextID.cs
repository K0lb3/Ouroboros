// Decompiled with JetBrains decompiler
// Type: SRPG.StringIsTextID
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

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
