// Decompiled with JetBrains decompiler
// Type: SRPG.HeaderBar
// Assembly: Assembly-CSharp, Version=1.2.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9BA76916-D0BD-4DB6-A90B-FE0BCC53E511
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using UnityEngine;

namespace SRPG
{
  public class HeaderBar : PropertyAttribute
  {
    public string Text;
    public Color BGColor;
    public Color FGColor;

    public HeaderBar(string text)
    {
      this.\u002Ector();
      this.Text = text;
      this.BGColor = new Color(0.0f, 0.2f, 0.5f);
      this.FGColor = Color.get_white();
    }

    public HeaderBar(string text, Color bg)
    {
      this.\u002Ector();
      this.Text = text;
      this.BGColor = bg;
      this.FGColor = Color.get_white();
    }

    public HeaderBar(string text, Color bg, Color fg)
    {
      this.\u002Ector();
      this.Text = text;
      this.BGColor = bg;
      this.FGColor = fg;
    }
  }
}
