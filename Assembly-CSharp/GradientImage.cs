// Decompiled with JetBrains decompiler
// Type: GradientImage
// Assembly: Assembly-CSharp, Version=1.2.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9BA76916-D0BD-4DB6-A90B-FE0BCC53E511
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using UnityEngine;
using UnityEngine.UI;

[AddComponentMenu("UI/Gradient Image")]
public class GradientImage : Image
{
  public Color32 TopLeft;
  public Color32 TopRight;
  public Color32 BottomLeft;
  public Color32 BottomRight;

  public GradientImage()
  {
    base.\u002Ector();
  }

  protected virtual void OnPopulateMesh(VertexHelper vh)
  {
    vh.Clear();
    if (this.get_type() == null)
    {
      Color32 a = Color32.op_Implicit(((Graphic) this).get_color());
      if (Object.op_Inequality((Object) this.get_sprite(), (Object) null))
      {
        Vector2[] vertices = this.get_sprite().get_vertices();
        Vector2[] uv = this.get_sprite().get_uv();
        vh.AddVert(Vector2.op_Implicit(vertices[0]), GradientImage.MultiplyColor(a, this.BottomLeft), uv[0]);
        vh.AddVert(Vector2.op_Implicit(vertices[1]), GradientImage.MultiplyColor(a, this.TopLeft), uv[1]);
        vh.AddVert(Vector2.op_Implicit(vertices[2]), GradientImage.MultiplyColor(a, this.TopRight), uv[2]);
        vh.AddVert(Vector2.op_Implicit(vertices[3]), GradientImage.MultiplyColor(a, this.BottomRight), uv[3]);
      }
      else
      {
        Rect rect = ((RectTransform) ((Component) this).GetComponent<RectTransform>()).get_rect();
        // ISSUE: explicit reference operation
        vh.AddVert(Vector2.op_Implicit(((Rect) @rect).get_min()), GradientImage.MultiplyColor(a, this.BottomLeft), new Vector2(0.0f, 0.0f));
        // ISSUE: explicit reference operation
        // ISSUE: explicit reference operation
        vh.AddVert(Vector2.op_Implicit(new Vector2(((Rect) @rect).get_x(), ((Rect) @rect).get_yMax())), GradientImage.MultiplyColor(a, this.TopLeft), new Vector2(0.0f, 1f));
        // ISSUE: explicit reference operation
        vh.AddVert(Vector2.op_Implicit(((Rect) @rect).get_max()), GradientImage.MultiplyColor(a, this.TopRight), new Vector2(1f, 1f));
        // ISSUE: explicit reference operation
        // ISSUE: explicit reference operation
        vh.AddVert(Vector2.op_Implicit(new Vector2(((Rect) @rect).get_xMax(), ((Rect) @rect).get_y())), GradientImage.MultiplyColor(a, this.BottomRight), new Vector2(1f, 0.0f));
      }
      vh.AddTriangle(0, 1, 2);
      vh.AddTriangle(2, 3, 0);
    }
    else
      base.OnPopulateMesh(vh);
  }

  private static Color32 MultiplyColor(Color32 a, Color32 b)
  {
    a.r = (__Null) (int) (byte) (a.r * b.r / (int) byte.MaxValue);
    a.g = (__Null) (int) (byte) (a.g * b.g / (int) byte.MaxValue);
    a.b = (__Null) (int) (byte) (a.b * b.b / (int) byte.MaxValue);
    a.a = (__Null) (int) (byte) (a.a * b.a / (int) byte.MaxValue);
    return a;
  }
}
