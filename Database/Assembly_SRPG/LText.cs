// Decompiled with JetBrains decompiler
// Type: SRPG.LText
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using UnityEngine;
using UnityEngine.UI;

namespace SRPG
{
  public class LText : Text
  {
    private string mCurrentText;

    public LText()
    {
      base.\u002Ector();
    }

    private void LateUpdate()
    {
      if (!Application.get_isPlaying())
        return;
      if (string.IsNullOrEmpty(this.mCurrentText))
      {
        if (string.IsNullOrEmpty(this.get_text()))
          return;
      }
      else if (!string.IsNullOrEmpty(this.get_text()) && this.mCurrentText.Equals(this.get_text()))
        return;
      this.set_text(LocalizedText.Get(this.get_text()));
      this.mCurrentText = this.get_text();
    }
  }
}
