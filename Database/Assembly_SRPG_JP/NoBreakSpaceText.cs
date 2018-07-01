// Decompiled with JetBrains decompiler
// Type: SRPG.NoBreakSpaceText
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 85BFDF7F-5712-4D45-9CD6-3465C703DFDF
// Assembly location: S:\Desktop\Assembly-CSharp.dll

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
