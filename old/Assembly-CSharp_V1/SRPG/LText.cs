// Decompiled with JetBrains decompiler
// Type: SRPG.LText
// Assembly: Assembly-CSharp, Version=1.2.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9BA76916-D0BD-4DB6-A90B-FE0BCC53E511
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using UnityEngine;
using UnityEngine.UI;

namespace SRPG
{
  public class LText : Text
  {
    private string mCurrentText;
    private string mCurrentDict;

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
      if (this.get_text().Contains("."))
        this.set_text(LocalizedText.Get(this.get_text()));
      string str = this.get_text().Split(new char[1]{ '\t' }, 2)[0];
      if (this.mCurrentDict != null && str != this.mCurrentDict)
        return;
      this.mCurrentDict = str;
      this.set_text(LocalizedText.Get(this.get_text()));
      this.mCurrentText = this.get_text();
    }
  }
}
