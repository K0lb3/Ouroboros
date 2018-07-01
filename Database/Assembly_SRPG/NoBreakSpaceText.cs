// Decompiled with JetBrains decompiler
// Type: SRPG.NoBreakSpaceText
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using System;
using UnityEngine.UI;

namespace SRPG
{
  public class NoBreakSpaceText : Text
  {
    public NoBreakSpaceText()
    {
      base.\u002Ector();
    }

    public virtual string text
    {
      get
      {
        return base.get_text();
      }
      set
      {
        base.set_text(value);
        this.Refresh();
      }
    }

    public void Refresh()
    {
      this.m_Text = (__Null) ((string) this.m_Text).Replace(" ", Convert.ToChar(Convert.ToInt32("00A0", 16)).ToString());
    }
  }
}
