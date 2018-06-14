// Decompiled with JetBrains decompiler
// Type: SRPG.PolyImage
// Assembly: Assembly-CSharp, Version=1.2.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9BA76916-D0BD-4DB6-A90B-FE0BCC53E511
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace SRPG
{
  public class PolyImage : Image
  {
    public Quad[] Quads;
    public bool Transparent;
    private RectTransform mRectTransform;

    public PolyImage()
    {
      base.\u002Ector();
    }

    protected virtual void Awake()
    {
      ((UIBehaviour) this).Awake();
      this.mRectTransform = (RectTransform) ((Component) this).GetComponent<RectTransform>();
    }

    protected virtual void OnPopulateMesh(VertexHelper vh)
    {
      vh.Clear();
      if (Object.op_Equality((Object) this.get_sprite(), (Object) null) && this.Transparent || ((Graphic) this).get_color().a <= 0.0)
        return;
      UIVertex uiVertex = (UIVertex) null;
      Color32 color32 = Color32.op_Implicit(((Graphic) this).get_color());
      Rect rect1 = this.mRectTransform.get_rect();
      int num1 = 0;
      Sprite sprite = this.get_sprite();
      Rect rect2;
      if (Object.op_Inequality((Object) sprite, (Object) null))
      {
        float num2 = 1f / (float) ((Texture) sprite.get_texture()).get_width();
        float num3 = 1f / (float) ((Texture) sprite.get_texture()).get_height();
        Rect rect3 = sprite.get_rect();
        // ISSUE: explicit reference operation
        // ISSUE: explicit reference operation
        // ISSUE: explicit reference operation
        // ISSUE: explicit reference operation
        // ISSUE: explicit reference operation
        ((Rect) @rect2).\u002Ector(((Rect) @rect3).get_x() * num2, ((Rect) @rect3).get_y() * num3, ((Rect) @rect3).get_width() * num2, ((Rect) @rect3).get_height() * num3);
      }
      else
      {
        // ISSUE: explicit reference operation
        ((Rect) @rect2).\u002Ector(0.0f, 0.0f, 1f, 1f);
      }
      if (color32.r == (int) byte.MaxValue && color32.g == (int) byte.MaxValue && (color32.b == (int) byte.MaxValue && color32.a == (int) byte.MaxValue))
      {
        for (int index = this.Quads.Length - 1; index >= 0; --index)
        {
          // ISSUE: explicit reference operation
          // ISSUE: cast to a reference type
          // ISSUE: explicit reference operation
          // ISSUE: explicit reference operation
          // ISSUE: explicit reference operation
          (^(Vector3&) @uiVertex.position).x = (__Null) (double) Mathf.Lerp(((Rect) @rect1).get_xMin(), ((Rect) @rect1).get_xMax(), (float) this.Quads[index].v0.x);
          // ISSUE: explicit reference operation
          // ISSUE: cast to a reference type
          // ISSUE: explicit reference operation
          // ISSUE: explicit reference operation
          // ISSUE: explicit reference operation
          (^(Vector3&) @uiVertex.position).y = (__Null) (double) Mathf.Lerp(((Rect) @rect1).get_yMin(), ((Rect) @rect1).get_yMax(), (float) this.Quads[index].v0.y);
          uiVertex.color = (__Null) this.Quads[index].c0;
          // ISSUE: explicit reference operation
          // ISSUE: cast to a reference type
          // ISSUE: explicit reference operation
          // ISSUE: explicit reference operation
          // ISSUE: explicit reference operation
          (^(Vector2&) @uiVertex.uv0).x = (__Null) (double) Mathf.Lerp(((Rect) @rect2).get_xMin(), ((Rect) @rect2).get_xMax(), (float) this.Quads[index].v0.x);
          // ISSUE: explicit reference operation
          // ISSUE: cast to a reference type
          // ISSUE: explicit reference operation
          // ISSUE: explicit reference operation
          // ISSUE: explicit reference operation
          (^(Vector2&) @uiVertex.uv0).y = (__Null) (double) Mathf.Lerp(((Rect) @rect2).get_yMin(), ((Rect) @rect2).get_yMax(), (float) this.Quads[index].v0.y);
          vh.AddVert(uiVertex);
          // ISSUE: explicit reference operation
          // ISSUE: cast to a reference type
          // ISSUE: explicit reference operation
          // ISSUE: explicit reference operation
          // ISSUE: explicit reference operation
          (^(Vector3&) @uiVertex.position).x = (__Null) (double) Mathf.Lerp(((Rect) @rect1).get_xMin(), ((Rect) @rect1).get_xMax(), (float) this.Quads[index].v1.x);
          // ISSUE: explicit reference operation
          // ISSUE: cast to a reference type
          // ISSUE: explicit reference operation
          // ISSUE: explicit reference operation
          // ISSUE: explicit reference operation
          (^(Vector3&) @uiVertex.position).y = (__Null) (double) Mathf.Lerp(((Rect) @rect1).get_yMin(), ((Rect) @rect1).get_yMax(), (float) this.Quads[index].v1.y);
          uiVertex.color = (__Null) this.Quads[index].c1;
          // ISSUE: explicit reference operation
          // ISSUE: cast to a reference type
          // ISSUE: explicit reference operation
          // ISSUE: explicit reference operation
          // ISSUE: explicit reference operation
          (^(Vector2&) @uiVertex.uv0).x = (__Null) (double) Mathf.Lerp(((Rect) @rect2).get_xMin(), ((Rect) @rect2).get_xMax(), (float) this.Quads[index].v1.x);
          // ISSUE: explicit reference operation
          // ISSUE: cast to a reference type
          // ISSUE: explicit reference operation
          // ISSUE: explicit reference operation
          // ISSUE: explicit reference operation
          (^(Vector2&) @uiVertex.uv0).y = (__Null) (double) Mathf.Lerp(((Rect) @rect2).get_yMin(), ((Rect) @rect2).get_yMax(), (float) this.Quads[index].v1.y);
          vh.AddVert(uiVertex);
          // ISSUE: explicit reference operation
          // ISSUE: cast to a reference type
          // ISSUE: explicit reference operation
          // ISSUE: explicit reference operation
          // ISSUE: explicit reference operation
          (^(Vector3&) @uiVertex.position).x = (__Null) (double) Mathf.Lerp(((Rect) @rect1).get_xMin(), ((Rect) @rect1).get_xMax(), (float) this.Quads[index].v2.x);
          // ISSUE: explicit reference operation
          // ISSUE: cast to a reference type
          // ISSUE: explicit reference operation
          // ISSUE: explicit reference operation
          // ISSUE: explicit reference operation
          (^(Vector3&) @uiVertex.position).y = (__Null) (double) Mathf.Lerp(((Rect) @rect1).get_yMin(), ((Rect) @rect1).get_yMax(), (float) this.Quads[index].v2.y);
          uiVertex.color = (__Null) this.Quads[index].c2;
          // ISSUE: explicit reference operation
          // ISSUE: cast to a reference type
          // ISSUE: explicit reference operation
          // ISSUE: explicit reference operation
          // ISSUE: explicit reference operation
          (^(Vector2&) @uiVertex.uv0).x = (__Null) (double) Mathf.Lerp(((Rect) @rect2).get_xMin(), ((Rect) @rect2).get_xMax(), (float) this.Quads[index].v2.x);
          // ISSUE: explicit reference operation
          // ISSUE: cast to a reference type
          // ISSUE: explicit reference operation
          // ISSUE: explicit reference operation
          // ISSUE: explicit reference operation
          (^(Vector2&) @uiVertex.uv0).y = (__Null) (double) Mathf.Lerp(((Rect) @rect2).get_yMin(), ((Rect) @rect2).get_yMax(), (float) this.Quads[index].v2.y);
          vh.AddVert(uiVertex);
          // ISSUE: explicit reference operation
          // ISSUE: cast to a reference type
          // ISSUE: explicit reference operation
          // ISSUE: explicit reference operation
          // ISSUE: explicit reference operation
          (^(Vector3&) @uiVertex.position).x = (__Null) (double) Mathf.Lerp(((Rect) @rect1).get_xMin(), ((Rect) @rect1).get_xMax(), (float) this.Quads[index].v3.x);
          // ISSUE: explicit reference operation
          // ISSUE: cast to a reference type
          // ISSUE: explicit reference operation
          // ISSUE: explicit reference operation
          // ISSUE: explicit reference operation
          (^(Vector3&) @uiVertex.position).y = (__Null) (double) Mathf.Lerp(((Rect) @rect1).get_yMin(), ((Rect) @rect1).get_yMax(), (float) this.Quads[index].v3.y);
          uiVertex.color = (__Null) this.Quads[index].c3;
          // ISSUE: explicit reference operation
          // ISSUE: cast to a reference type
          // ISSUE: explicit reference operation
          // ISSUE: explicit reference operation
          // ISSUE: explicit reference operation
          (^(Vector2&) @uiVertex.uv0).x = (__Null) (double) Mathf.Lerp(((Rect) @rect2).get_xMin(), ((Rect) @rect2).get_xMax(), (float) this.Quads[index].v3.x);
          // ISSUE: explicit reference operation
          // ISSUE: cast to a reference type
          // ISSUE: explicit reference operation
          // ISSUE: explicit reference operation
          // ISSUE: explicit reference operation
          (^(Vector2&) @uiVertex.uv0).y = (__Null) (double) Mathf.Lerp(((Rect) @rect2).get_yMin(), ((Rect) @rect2).get_yMax(), (float) this.Quads[index].v3.y);
          vh.AddVert(uiVertex);
          vh.AddTriangle(num1, num1 + 1, num1 + 2);
          vh.AddTriangle(num1 + 2, num1 + 3, num1);
          num1 += 4;
        }
      }
      else
      {
        for (int index = this.Quads.Length - 1; index >= 0; --index)
        {
          // ISSUE: explicit reference operation
          // ISSUE: cast to a reference type
          // ISSUE: explicit reference operation
          // ISSUE: explicit reference operation
          // ISSUE: explicit reference operation
          (^(Vector3&) @uiVertex.position).x = (__Null) (double) Mathf.Lerp(((Rect) @rect1).get_xMin(), ((Rect) @rect1).get_xMax(), (float) this.Quads[index].v0.x);
          // ISSUE: explicit reference operation
          // ISSUE: cast to a reference type
          // ISSUE: explicit reference operation
          // ISSUE: explicit reference operation
          // ISSUE: explicit reference operation
          (^(Vector3&) @uiVertex.position).y = (__Null) (double) Mathf.Lerp(((Rect) @rect1).get_yMin(), ((Rect) @rect1).get_yMax(), (float) this.Quads[index].v0.y);
          // ISSUE: explicit reference operation
          // ISSUE: cast to a reference type
          // ISSUE: explicit reference operation
          (^(Color32&) @uiVertex.color).r = (__Null) (int) (byte) (this.Quads[index].c0.r * color32.r / (int) byte.MaxValue);
          // ISSUE: explicit reference operation
          // ISSUE: cast to a reference type
          // ISSUE: explicit reference operation
          (^(Color32&) @uiVertex.color).g = (__Null) (int) (byte) (this.Quads[index].c0.g * color32.g / (int) byte.MaxValue);
          // ISSUE: explicit reference operation
          // ISSUE: cast to a reference type
          // ISSUE: explicit reference operation
          (^(Color32&) @uiVertex.color).b = (__Null) (int) (byte) (this.Quads[index].c0.b * color32.b / (int) byte.MaxValue);
          // ISSUE: explicit reference operation
          // ISSUE: cast to a reference type
          // ISSUE: explicit reference operation
          (^(Color32&) @uiVertex.color).a = (__Null) (int) (byte) (this.Quads[index].c0.a * color32.a / (int) byte.MaxValue);
          // ISSUE: explicit reference operation
          // ISSUE: cast to a reference type
          // ISSUE: explicit reference operation
          // ISSUE: explicit reference operation
          // ISSUE: explicit reference operation
          (^(Vector2&) @uiVertex.uv0).x = (__Null) (double) Mathf.Lerp(((Rect) @rect2).get_xMin(), ((Rect) @rect2).get_xMax(), (float) this.Quads[index].v0.x);
          // ISSUE: explicit reference operation
          // ISSUE: cast to a reference type
          // ISSUE: explicit reference operation
          // ISSUE: explicit reference operation
          // ISSUE: explicit reference operation
          (^(Vector2&) @uiVertex.uv0).y = (__Null) (double) Mathf.Lerp(((Rect) @rect2).get_yMin(), ((Rect) @rect2).get_yMax(), (float) this.Quads[index].v0.y);
          vh.AddVert(uiVertex);
          // ISSUE: explicit reference operation
          // ISSUE: cast to a reference type
          // ISSUE: explicit reference operation
          // ISSUE: explicit reference operation
          // ISSUE: explicit reference operation
          (^(Vector3&) @uiVertex.position).x = (__Null) (double) Mathf.Lerp(((Rect) @rect1).get_xMin(), ((Rect) @rect1).get_xMax(), (float) this.Quads[index].v1.x);
          // ISSUE: explicit reference operation
          // ISSUE: cast to a reference type
          // ISSUE: explicit reference operation
          // ISSUE: explicit reference operation
          // ISSUE: explicit reference operation
          (^(Vector3&) @uiVertex.position).y = (__Null) (double) Mathf.Lerp(((Rect) @rect1).get_yMin(), ((Rect) @rect1).get_yMax(), (float) this.Quads[index].v1.y);
          // ISSUE: explicit reference operation
          // ISSUE: cast to a reference type
          // ISSUE: explicit reference operation
          (^(Color32&) @uiVertex.color).r = (__Null) (int) (byte) (this.Quads[index].c1.r * color32.r / (int) byte.MaxValue);
          // ISSUE: explicit reference operation
          // ISSUE: cast to a reference type
          // ISSUE: explicit reference operation
          (^(Color32&) @uiVertex.color).g = (__Null) (int) (byte) (this.Quads[index].c1.g * color32.g / (int) byte.MaxValue);
          // ISSUE: explicit reference operation
          // ISSUE: cast to a reference type
          // ISSUE: explicit reference operation
          (^(Color32&) @uiVertex.color).b = (__Null) (int) (byte) (this.Quads[index].c1.b * color32.b / (int) byte.MaxValue);
          // ISSUE: explicit reference operation
          // ISSUE: cast to a reference type
          // ISSUE: explicit reference operation
          (^(Color32&) @uiVertex.color).a = (__Null) (int) (byte) (this.Quads[index].c1.a * color32.a / (int) byte.MaxValue);
          // ISSUE: explicit reference operation
          // ISSUE: cast to a reference type
          // ISSUE: explicit reference operation
          // ISSUE: explicit reference operation
          // ISSUE: explicit reference operation
          (^(Vector2&) @uiVertex.uv0).x = (__Null) (double) Mathf.Lerp(((Rect) @rect2).get_xMin(), ((Rect) @rect2).get_xMax(), (float) this.Quads[index].v1.x);
          // ISSUE: explicit reference operation
          // ISSUE: cast to a reference type
          // ISSUE: explicit reference operation
          // ISSUE: explicit reference operation
          // ISSUE: explicit reference operation
          (^(Vector2&) @uiVertex.uv0).y = (__Null) (double) Mathf.Lerp(((Rect) @rect2).get_yMin(), ((Rect) @rect2).get_yMax(), (float) this.Quads[index].v1.y);
          vh.AddVert(uiVertex);
          // ISSUE: explicit reference operation
          // ISSUE: cast to a reference type
          // ISSUE: explicit reference operation
          // ISSUE: explicit reference operation
          // ISSUE: explicit reference operation
          (^(Vector3&) @uiVertex.position).x = (__Null) (double) Mathf.Lerp(((Rect) @rect1).get_xMin(), ((Rect) @rect1).get_xMax(), (float) this.Quads[index].v2.x);
          // ISSUE: explicit reference operation
          // ISSUE: cast to a reference type
          // ISSUE: explicit reference operation
          // ISSUE: explicit reference operation
          // ISSUE: explicit reference operation
          (^(Vector3&) @uiVertex.position).y = (__Null) (double) Mathf.Lerp(((Rect) @rect1).get_yMin(), ((Rect) @rect1).get_yMax(), (float) this.Quads[index].v2.y);
          // ISSUE: explicit reference operation
          // ISSUE: cast to a reference type
          // ISSUE: explicit reference operation
          (^(Color32&) @uiVertex.color).r = (__Null) (int) (byte) (this.Quads[index].c2.r * color32.r / (int) byte.MaxValue);
          // ISSUE: explicit reference operation
          // ISSUE: cast to a reference type
          // ISSUE: explicit reference operation
          (^(Color32&) @uiVertex.color).g = (__Null) (int) (byte) (this.Quads[index].c2.g * color32.g / (int) byte.MaxValue);
          // ISSUE: explicit reference operation
          // ISSUE: cast to a reference type
          // ISSUE: explicit reference operation
          (^(Color32&) @uiVertex.color).b = (__Null) (int) (byte) (this.Quads[index].c2.b * color32.b / (int) byte.MaxValue);
          // ISSUE: explicit reference operation
          // ISSUE: cast to a reference type
          // ISSUE: explicit reference operation
          (^(Color32&) @uiVertex.color).a = (__Null) (int) (byte) (this.Quads[index].c2.a * color32.a / (int) byte.MaxValue);
          // ISSUE: explicit reference operation
          // ISSUE: cast to a reference type
          // ISSUE: explicit reference operation
          // ISSUE: explicit reference operation
          // ISSUE: explicit reference operation
          (^(Vector2&) @uiVertex.uv0).x = (__Null) (double) Mathf.Lerp(((Rect) @rect2).get_xMin(), ((Rect) @rect2).get_xMax(), (float) this.Quads[index].v2.x);
          // ISSUE: explicit reference operation
          // ISSUE: cast to a reference type
          // ISSUE: explicit reference operation
          // ISSUE: explicit reference operation
          // ISSUE: explicit reference operation
          (^(Vector2&) @uiVertex.uv0).y = (__Null) (double) Mathf.Lerp(((Rect) @rect2).get_yMin(), ((Rect) @rect2).get_yMax(), (float) this.Quads[index].v2.y);
          vh.AddVert(uiVertex);
          // ISSUE: explicit reference operation
          // ISSUE: cast to a reference type
          // ISSUE: explicit reference operation
          // ISSUE: explicit reference operation
          // ISSUE: explicit reference operation
          (^(Vector3&) @uiVertex.position).x = (__Null) (double) Mathf.Lerp(((Rect) @rect1).get_xMin(), ((Rect) @rect1).get_xMax(), (float) this.Quads[index].v3.x);
          // ISSUE: explicit reference operation
          // ISSUE: cast to a reference type
          // ISSUE: explicit reference operation
          // ISSUE: explicit reference operation
          // ISSUE: explicit reference operation
          (^(Vector3&) @uiVertex.position).y = (__Null) (double) Mathf.Lerp(((Rect) @rect1).get_yMin(), ((Rect) @rect1).get_yMax(), (float) this.Quads[index].v3.y);
          // ISSUE: explicit reference operation
          // ISSUE: cast to a reference type
          // ISSUE: explicit reference operation
          (^(Color32&) @uiVertex.color).r = (__Null) (int) (byte) (this.Quads[index].c3.r * color32.r / (int) byte.MaxValue);
          // ISSUE: explicit reference operation
          // ISSUE: cast to a reference type
          // ISSUE: explicit reference operation
          (^(Color32&) @uiVertex.color).g = (__Null) (int) (byte) (this.Quads[index].c3.g * color32.g / (int) byte.MaxValue);
          // ISSUE: explicit reference operation
          // ISSUE: cast to a reference type
          // ISSUE: explicit reference operation
          (^(Color32&) @uiVertex.color).b = (__Null) (int) (byte) (this.Quads[index].c3.b * color32.b / (int) byte.MaxValue);
          // ISSUE: explicit reference operation
          // ISSUE: cast to a reference type
          // ISSUE: explicit reference operation
          (^(Color32&) @uiVertex.color).a = (__Null) (int) (byte) (this.Quads[index].c3.a * color32.a / (int) byte.MaxValue);
          // ISSUE: explicit reference operation
          // ISSUE: cast to a reference type
          // ISSUE: explicit reference operation
          // ISSUE: explicit reference operation
          // ISSUE: explicit reference operation
          (^(Vector2&) @uiVertex.uv0).x = (__Null) (double) Mathf.Lerp(((Rect) @rect2).get_xMin(), ((Rect) @rect2).get_xMax(), (float) this.Quads[index].v3.x);
          // ISSUE: explicit reference operation
          // ISSUE: cast to a reference type
          // ISSUE: explicit reference operation
          // ISSUE: explicit reference operation
          // ISSUE: explicit reference operation
          (^(Vector2&) @uiVertex.uv0).y = (__Null) (double) Mathf.Lerp(((Rect) @rect2).get_yMin(), ((Rect) @rect2).get_yMax(), (float) this.Quads[index].v3.y);
          vh.AddVert(uiVertex);
          vh.AddTriangle(num1, num1 + 1, num1 + 2);
          vh.AddTriangle(num1 + 2, num1 + 3, num1);
          num1 += 4;
        }
      }
    }
  }
}
