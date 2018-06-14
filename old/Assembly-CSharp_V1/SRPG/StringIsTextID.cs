// Decompiled with JetBrains decompiler
// Type: SRPG.StringIsTextID
// Assembly: Assembly-CSharp, Version=1.2.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9BA76916-D0BD-4DB6-A90B-FE0BCC53E511
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
