// Decompiled with JetBrains decompiler
// Type: FlowComment
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using System;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class FlowComment : MonoBehaviour
{
  public List<FlowComment.Comment> Comments;

  public FlowComment()
  {
    base.\u002Ector();
  }

  private void Awake()
  {
  }

  [Serializable]
  public struct Comment
  {
    public Vector2 Position;
    public string Text;
    public Color Color;
    public Color Background;
    public int FontSize;

    public Comment(Vector2 pos, string text)
    {
      this.Position = pos;
      this.Text = text;
      this.FontSize = 20;
      this.Background = Color.get_gray();
      this.Color = Color.get_black();
    }
  }
}
