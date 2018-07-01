// Decompiled with JetBrains decompiler
// Type: SRPG.RawPolyImage
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 85BFDF7F-5712-4D45-9CD6-3465C703DFDF
// Assembly location: S:\Desktop\Assembly-CSharp.dll

using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace SRPG
{
  public class RawPolyImage : RawImage
  {
    public Quad[] Quads;
    public bool Transparent;
    public string Preview;
    private RectTransform mRectTransform;

    public RawPolyImage()
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
      if (Object.op_Equality((Object) this.get_texture(), (Object) null) && this.Transparent || ((Graphic) this).get_color().a <= 0.0)
        return;
      UIVertex uiVertex = (UIVertex) null;
      Rect rect = this.mRectTransform.get_rect();
      Rect uvRect = this.get_uvRect();
      Color32 color32 = Color32.op_Implicit(((Graphic) this).get_color());
      int num = 0;
      if (color32.r == (int) byte.MaxValue && color32.g == (int) byte.MaxValue && (color32.b == (int) byte.MaxValue && color32.a == (int) byte.MaxValue))
      {
        for (int index = this.Quads.Length - 1; index >= 0; --index)
        {
          // ISSUE: explicit reference operation
          // ISSUE: cast to a reference type
          // ISSUE: explicit reference operation
          // ISSUE: explicit reference operation
          // ISSUE: explicit reference operation
          (^(Vector3&) @uiVertex.position).x = (__Null) (double) Mathf.Lerp(((Rect) @rect).get_xMin(), ((Rect) @rect).get_xMax(), (float) this.Quads[index].v0.x);
          // ISSUE: explicit reference operation
          // ISSUE: cast to a reference type
          // ISSUE: explicit reference operation
          // ISSUE: explicit reference operation
          // ISSUE: explicit reference operation
          (^(Vector3&) @uiVertex.position).y = (__Null) (double) Mathf.Lerp(((Rect) @rect).get_yMin(), ((Rect) @rect).get_yMax(), (float) this.Quads[index].v0.y);
          uiVertex.color = (__Null) this.Quads[index].c0;
          // ISSUE: explicit reference operation
          // ISSUE: cast to a reference type
          // ISSUE: explicit reference operation
          // ISSUE: explicit reference operation
          // ISSUE: explicit reference operation
          (^(Vector2&) @uiVertex.uv0).x = (__Null) (double) Mathf.Lerp(((Rect) @uvRect).get_xMin(), ((Rect) @uvRect).get_xMax(), (float) this.Quads[index].v0.x);
          // ISSUE: explicit reference operation
          // ISSUE: cast to a reference type
          // ISSUE: explicit reference operation
          // ISSUE: explicit reference operation
          // ISSUE: explicit reference operation
          (^(Vector2&) @uiVertex.uv0).y = (__Null) (double) Mathf.Lerp(((Rect) @uvRect).get_yMin(), ((Rect) @uvRect).get_yMax(), (float) this.Quads[index].v0.y);
          vh.AddVert(uiVertex);
          // ISSUE: explicit reference operation
          // ISSUE: cast to a reference type
          // ISSUE: explicit reference operation
          // ISSUE: explicit reference operation
          // ISSUE: explicit reference operation
          (^(Vector3&) @uiVertex.position).x = (__Null) (double) Mathf.Lerp(((Rect) @rect).get_xMin(), ((Rect) @rect).get_xMax(), (float) this.Quads[index].v1.x);
          // ISSUE: explicit reference operation
          // ISSUE: cast to a reference type
          // ISSUE: explicit reference operation
          // ISSUE: explicit reference operation
          // ISSUE: explicit reference operation
          (^(Vector3&) @uiVertex.position).y = (__Null) (double) Mathf.Lerp(((Rect) @rect).get_yMin(), ((Rect) @rect).get_yMax(), (float) this.Quads[index].v1.y);
          uiVertex.color = (__Null) this.Quads[index].c1;
          // ISSUE: explicit reference operation
          // ISSUE: cast to a reference type
          // ISSUE: explicit reference operation
          // ISSUE: explicit reference operation
          // ISSUE: explicit reference operation
          (^(Vector2&) @uiVertex.uv0).x = (__Null) (double) Mathf.Lerp(((Rect) @uvRect).get_xMin(), ((Rect) @uvRect).get_xMax(), (float) this.Quads[index].v1.x);
          // ISSUE: explicit reference operation
          // ISSUE: cast to a reference type
          // ISSUE: explicit reference operation
          // ISSUE: explicit reference operation
          // ISSUE: explicit reference operation
          (^(Vector2&) @uiVertex.uv0).y = (__Null) (double) Mathf.Lerp(((Rect) @uvRect).get_yMin(), ((Rect) @uvRect).get_yMax(), (float) this.Quads[index].v1.y);
          vh.AddVert(uiVertex);
          // ISSUE: explicit reference operation
          // ISSUE: cast to a reference type
          // ISSUE: explicit reference operation
          // ISSUE: explicit reference operation
          // ISSUE: explicit reference operation
          (^(Vector3&) @uiVertex.position).x = (__Null) (double) Mathf.Lerp(((Rect) @rect).get_xMin(), ((Rect) @rect).get_xMax(), (float) this.Quads[index].v2.x);
          // ISSUE: explicit reference operation
          // ISSUE: cast to a reference type
          // ISSUE: explicit reference operation
          // ISSUE: explicit reference operation
          // ISSUE: explicit reference operation
          (^(Vector3&) @uiVertex.position).y = (__Null) (double) Mathf.Lerp(((Rect) @rect).get_yMin(), ((Rect) @rect).get_yMax(), (float) this.Quads[index].v2.y);
          uiVertex.color = (__Null) this.Quads[index].c2;
          // ISSUE: explicit reference operation
          // ISSUE: cast to a reference type
          // ISSUE: explicit reference operation
          // ISSUE: explicit reference operation
          // ISSUE: explicit reference operation
          (^(Vector2&) @uiVertex.uv0).x = (__Null) (double) Mathf.Lerp(((Rect) @uvRect).get_xMin(), ((Rect) @uvRect).get_xMax(), (float) this.Quads[index].v2.x);
          // ISSUE: explicit reference operation
          // ISSUE: cast to a reference type
          // ISSUE: explicit reference operation
          // ISSUE: explicit reference operation
          // ISSUE: explicit reference operation
          (^(Vector2&) @uiVertex.uv0).y = (__Null) (double) Mathf.Lerp(((Rect) @uvRect).get_yMin(), ((Rect) @uvRect).get_yMax(), (float) this.Quads[index].v2.y);
          vh.AddVert(uiVertex);
          // ISSUE: explicit reference operation
          // ISSUE: cast to a reference type
          // ISSUE: explicit reference operation
          // ISSUE: explicit reference operation
          // ISSUE: explicit reference operation
          (^(Vector3&) @uiVertex.position).x = (__Null) (double) Mathf.Lerp(((Rect) @rect).get_xMin(), ((Rect) @rect).get_xMax(), (float) this.Quads[index].v3.x);
          // ISSUE: explicit reference operation
          // ISSUE: cast to a reference type
          // ISSUE: explicit reference operation
          // ISSUE: explicit reference operation
          // ISSUE: explicit reference operation
          (^(Vector3&) @uiVertex.position).y = (__Null) (double) Mathf.Lerp(((Rect) @rect).get_yMin(), ((Rect) @rect).get_yMax(), (float) this.Quads[index].v3.y);
          uiVertex.color = (__Null) this.Quads[index].c3;
          // ISSUE: explicit reference operation
          // ISSUE: cast to a reference type
          // ISSUE: explicit reference operation
          // ISSUE: explicit reference operation
          // ISSUE: explicit reference operation
          (^(Vector2&) @uiVertex.uv0).x = (__Null) (double) Mathf.Lerp(((Rect) @uvRect).get_xMin(), ((Rect) @uvRect).get_xMax(), (float) this.Quads[index].v3.x);
          // ISSUE: explicit reference operation
          // ISSUE: cast to a reference type
          // ISSUE: explicit reference operation
          // ISSUE: explicit reference operation
          // ISSUE: explicit reference operation
          (^(Vector2&) @uiVertex.uv0).y = (__Null) (double) Mathf.Lerp(((Rect) @uvRect).get_yMin(), ((Rect) @uvRect).get_yMax(), (float) this.Quads[index].v3.y);
          vh.AddVert(uiVertex);
          vh.AddTriangle(num, num + 1, num + 2);
          vh.AddTriangle(num + 2, num + 3, num);
          num += 4;
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
          (^(Vector3&) @uiVertex.position).x = (__Null) (double) Mathf.Lerp(((Rect) @rect).get_xMin(), ((Rect) @rect).get_xMax(), (float) this.Quads[index].v0.x);
          // ISSUE: explicit reference operation
          // ISSUE: cast to a reference type
          // ISSUE: explicit reference operation
          // ISSUE: explicit reference operation
          // ISSUE: explicit reference operation
          (^(Vector3&) @uiVertex.position).y = (__Null) (double) Mathf.Lerp(((Rect) @rect).get_yMin(), ((Rect) @rect).get_yMax(), (float) this.Quads[index].v0.y);
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
          (^(Vector2&) @uiVertex.uv0).x = (__Null) (double) Mathf.Lerp(((Rect) @uvRect).get_xMin(), ((Rect) @uvRect).get_xMax(), (float) this.Quads[index].v0.x);
          // ISSUE: explicit reference operation
          // ISSUE: cast to a reference type
          // ISSUE: explicit reference operation
          // ISSUE: explicit reference operation
          // ISSUE: explicit reference operation
          (^(Vector2&) @uiVertex.uv0).y = (__Null) (double) Mathf.Lerp(((Rect) @uvRect).get_yMin(), ((Rect) @uvRect).get_yMax(), (float) this.Quads[index].v0.y);
          vh.AddVert(uiVertex);
          // ISSUE: explicit reference operation
          // ISSUE: cast to a reference type
          // ISSUE: explicit reference operation
          // ISSUE: explicit reference operation
          // ISSUE: explicit reference operation
          (^(Vector3&) @uiVertex.position).x = (__Null) (double) Mathf.Lerp(((Rect) @rect).get_xMin(), ((Rect) @rect).get_xMax(), (float) this.Quads[index].v1.x);
          // ISSUE: explicit reference operation
          // ISSUE: cast to a reference type
          // ISSUE: explicit reference operation
          // ISSUE: explicit reference operation
          // ISSUE: explicit reference operation
          (^(Vector3&) @uiVertex.position).y = (__Null) (double) Mathf.Lerp(((Rect) @rect).get_yMin(), ((Rect) @rect).get_yMax(), (float) this.Quads[index].v1.y);
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
          (^(Vector2&) @uiVertex.uv0).x = (__Null) (double) Mathf.Lerp(((Rect) @uvRect).get_xMin(), ((Rect) @uvRect).get_xMax(), (float) this.Quads[index].v1.x);
          // ISSUE: explicit reference operation
          // ISSUE: cast to a reference type
          // ISSUE: explicit reference operation
          // ISSUE: explicit reference operation
          // ISSUE: explicit reference operation
          (^(Vector2&) @uiVertex.uv0).y = (__Null) (double) Mathf.Lerp(((Rect) @uvRect).get_yMin(), ((Rect) @uvRect).get_yMax(), (float) this.Quads[index].v1.y);
          vh.AddVert(uiVertex);
          // ISSUE: explicit reference operation
          // ISSUE: cast to a reference type
          // ISSUE: explicit reference operation
          // ISSUE: explicit reference operation
          // ISSUE: explicit reference operation
          (^(Vector3&) @uiVertex.position).x = (__Null) (double) Mathf.Lerp(((Rect) @rect).get_xMin(), ((Rect) @rect).get_xMax(), (float) this.Quads[index].v2.x);
          // ISSUE: explicit reference operation
          // ISSUE: cast to a reference type
          // ISSUE: explicit reference operation
          // ISSUE: explicit reference operation
          // ISSUE: explicit reference operation
          (^(Vector3&) @uiVertex.position).y = (__Null) (double) Mathf.Lerp(((Rect) @rect).get_yMin(), ((Rect) @rect).get_yMax(), (float) this.Quads[index].v2.y);
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
          (^(Vector2&) @uiVertex.uv0).x = (__Null) (double) Mathf.Lerp(((Rect) @uvRect).get_xMin(), ((Rect) @uvRect).get_xMax(), (float) this.Quads[index].v2.x);
          // ISSUE: explicit reference operation
          // ISSUE: cast to a reference type
          // ISSUE: explicit reference operation
          // ISSUE: explicit reference operation
          // ISSUE: explicit reference operation
          (^(Vector2&) @uiVertex.uv0).y = (__Null) (double) Mathf.Lerp(((Rect) @uvRect).get_yMin(), ((Rect) @uvRect).get_yMax(), (float) this.Quads[index].v2.y);
          vh.AddVert(uiVertex);
          // ISSUE: explicit reference operation
          // ISSUE: cast to a reference type
          // ISSUE: explicit reference operation
          // ISSUE: explicit reference operation
          // ISSUE: explicit reference operation
          (^(Vector3&) @uiVertex.position).x = (__Null) (double) Mathf.Lerp(((Rect) @rect).get_xMin(), ((Rect) @rect).get_xMax(), (float) this.Quads[index].v3.x);
          // ISSUE: explicit reference operation
          // ISSUE: cast to a reference type
          // ISSUE: explicit reference operation
          // ISSUE: explicit reference operation
          // ISSUE: explicit reference operation
          (^(Vector3&) @uiVertex.position).y = (__Null) (double) Mathf.Lerp(((Rect) @rect).get_yMin(), ((Rect) @rect).get_yMax(), (float) this.Quads[index].v3.y);
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
          (^(Vector2&) @uiVertex.uv0).x = (__Null) (double) Mathf.Lerp(((Rect) @uvRect).get_xMin(), ((Rect) @uvRect).get_xMax(), (float) this.Quads[index].v3.x);
          // ISSUE: explicit reference operation
          // ISSUE: cast to a reference type
          // ISSUE: explicit reference operation
          // ISSUE: explicit reference operation
          // ISSUE: explicit reference operation
          (^(Vector2&) @uiVertex.uv0).y = (__Null) (double) Mathf.Lerp(((Rect) @uvRect).get_yMin(), ((Rect) @uvRect).get_yMax(), (float) this.Quads[index].v3.y);
          vh.AddVert(uiVertex);
          vh.AddTriangle(num, num + 1, num + 2);
          vh.AddTriangle(num + 2, num + 3, num);
          num += 4;
        }
      }
    }
  }
}
