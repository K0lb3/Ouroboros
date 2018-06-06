// Decompiled with JetBrains decompiler
// Type: Donuts
// Assembly: Assembly-CSharp, Version=1.2.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9BA76916-D0BD-4DB6-A90B-FE0BCC53E511
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using System;
using UnityEngine;
using UnityEngine.UI;

public class Donuts : Image
{
  public float MinAngle;
  public float MaxAngle;
  public float RadiusMin;
  public float RadiusMax;
  public float Step;
  public float Tiling;

  public Donuts()
  {
    base.\u002Ector();
  }

  public void SetRange(float angleMin, float angleMax)
  {
    this.MinAngle = angleMin;
    this.MaxAngle = angleMax;
    ((Graphic) this).SetVerticesDirty();
    ((Graphic) this).UpdateGeometry();
  }

  protected virtual void OnPopulateMesh(VertexHelper vh)
  {
    vh.Clear();
    if ((double) Mathf.Abs(this.MinAngle - this.MaxAngle) <= 1.0 || (double) this.Step <= 1.40129846432482E-45)
      return;
    UIVertex uiVertex = (UIVertex) null;
    CanvasRenderer canvasRenderer = ((Graphic) this).get_canvasRenderer();
    Sprite sprite = this.get_sprite();
    Vector2 zero1 = Vector2.get_zero();
    Vector2 zero2 = Vector2.get_zero();
    float num1;
    if (Object.op_Inequality((Object) sprite, (Object) null))
    {
      float num2 = 1f / (float) ((Texture) sprite.get_texture()).get_width();
      float num3 = 1f / (float) ((Texture) sprite.get_texture()).get_height();
      Rect rect = sprite.get_rect();
      // ISSUE: explicit reference operation
      zero1.x = (__Null) (((Rect) @rect).get_min().x * (double) num2);
      // ISSUE: explicit reference operation
      zero1.y = (__Null) (((Rect) @rect).get_min().y * (double) num3);
      // ISSUE: explicit reference operation
      zero2.x = (__Null) (((Rect) @rect).get_max().x * (double) num2);
      // ISSUE: explicit reference operation
      zero2.y = (__Null) (((Rect) @rect).get_max().y * (double) num3);
      // ISSUE: explicit reference operation
      num1 = ((Rect) @rect).get_width();
    }
    else
      num1 = float.MaxValue;
    float step = this.Step;
    float num4;
    float num5;
    if ((double) this.MinAngle < (double) this.MaxAngle)
    {
      num4 = this.MinAngle;
      num5 = this.MaxAngle;
    }
    else
    {
      num4 = this.MaxAngle;
      num5 = this.MinAngle;
    }
    int num6 = 0;
    float num7 = num4 * ((float) Math.PI / 180f);
    uiVertex.position = (__Null) Vector2.op_Implicit(new Vector2(Mathf.Cos(num7) * this.RadiusMax, Mathf.Sin(num7) * this.RadiusMax));
    uiVertex.color = (__Null) Color32.op_Implicit(canvasRenderer.GetColor());
    // ISSUE: explicit reference operation
    // ISSUE: cast to a reference type
    // ISSUE: explicit reference operation
    (^(Vector2&) @uiVertex.uv0).x = zero1.x;
    // ISSUE: explicit reference operation
    // ISSUE: cast to a reference type
    // ISSUE: explicit reference operation
    (^(Vector2&) @uiVertex.uv0).y = zero2.y;
    vh.AddVert(uiVertex);
    uiVertex.position = (__Null) Vector2.op_Implicit(new Vector2(Mathf.Cos(num7) * this.RadiusMin, Mathf.Sin(num7) * this.RadiusMin));
    uiVertex.color = (__Null) Color32.op_Implicit(canvasRenderer.GetColor());
    // ISSUE: explicit reference operation
    // ISSUE: cast to a reference type
    // ISSUE: explicit reference operation
    (^(Vector2&) @uiVertex.uv0).x = zero1.x;
    // ISSUE: explicit reference operation
    // ISSUE: cast to a reference type
    // ISSUE: explicit reference operation
    (^(Vector2&) @uiVertex.uv0).y = zero1.y;
    vh.AddVert(uiVertex);
    float num8 = 0.0f;
    float num9 = num4;
    float num10 = (float) (Math.PI / 180.0 * (double) this.RadiusMin * 2.0) * this.Tiling;
    while (true)
    {
      float num2 = num9 + step;
      float num3 = (num9 - num4) * num10;
      float num11 = (num2 - num4) * num10;
      num8 += num11 - num3;
      bool flag = false;
      if ((double) num8 >= (double) num1)
      {
        float num12 = (float) ((double) num8 / (double) num1 - 1.0);
        num2 = num9 + step * num12;
        num8 = 0.0f;
        flag = true;
      }
      num9 = num2;
      if ((double) num9 < (double) num5)
      {
        float num12 = num9 * ((float) Math.PI / 180f);
        float num13 = num8 / num1;
        uiVertex.position = (__Null) Vector2.op_Implicit(new Vector2(Mathf.Cos(num12) * this.RadiusMin, Mathf.Sin(num12) * this.RadiusMin));
        uiVertex.color = (__Null) Color32.op_Implicit(canvasRenderer.GetColor());
        if (flag)
        {
          // ISSUE: explicit reference operation
          // ISSUE: cast to a reference type
          // ISSUE: explicit reference operation
          (^(Vector2&) @uiVertex.uv0).x = zero2.x;
        }
        else
        {
          // ISSUE: explicit reference operation
          // ISSUE: cast to a reference type
          // ISSUE: explicit reference operation
          (^(Vector2&) @uiVertex.uv0).x = (__Null) ((zero2.x - zero1.x) * (double) num13 + zero1.x);
        }
        // ISSUE: explicit reference operation
        // ISSUE: cast to a reference type
        // ISSUE: explicit reference operation
        (^(Vector2&) @uiVertex.uv0).y = zero1.y;
        vh.AddVert(uiVertex);
        uiVertex.position = (__Null) Vector2.op_Implicit(new Vector2(Mathf.Cos(num12) * this.RadiusMax, Mathf.Sin(num12) * this.RadiusMax));
        uiVertex.color = (__Null) Color32.op_Implicit(canvasRenderer.GetColor());
        // ISSUE: explicit reference operation
        // ISSUE: cast to a reference type
        // ISSUE: explicit reference operation
        (^(Vector2&) @uiVertex.uv0).y = zero2.y;
        vh.AddVert(uiVertex);
        vh.AddTriangle(num6, num6 + 1, num6 + 2);
        vh.AddTriangle(num6 + 2, num6 + 3, num6);
        num6 += 4;
        if (flag)
        {
          // ISSUE: explicit reference operation
          // ISSUE: cast to a reference type
          // ISSUE: explicit reference operation
          (^(Vector2&) @uiVertex.uv0).x = zero1.x;
        }
        uiVertex.position = (__Null) Vector2.op_Implicit(new Vector2(Mathf.Cos(num12) * this.RadiusMax, Mathf.Sin(num12) * this.RadiusMax));
        uiVertex.color = (__Null) Color32.op_Implicit(canvasRenderer.GetColor());
        // ISSUE: explicit reference operation
        // ISSUE: cast to a reference type
        // ISSUE: explicit reference operation
        (^(Vector2&) @uiVertex.uv0).y = zero2.y;
        vh.AddVert(uiVertex);
        uiVertex.position = (__Null) Vector2.op_Implicit(new Vector2(Mathf.Cos(num12) * this.RadiusMin, Mathf.Sin(num12) * this.RadiusMin));
        uiVertex.color = (__Null) Color32.op_Implicit(canvasRenderer.GetColor());
        // ISSUE: explicit reference operation
        // ISSUE: cast to a reference type
        // ISSUE: explicit reference operation
        (^(Vector2&) @uiVertex.uv0).y = zero1.y;
        vh.AddVert(uiVertex);
      }
      else
        break;
    }
    float num14 = num5 * ((float) Math.PI / 180f);
    float num15 = (num8 + (num5 - num9) * num10) / num1;
    // ISSUE: explicit reference operation
    // ISSUE: cast to a reference type
    // ISSUE: explicit reference operation
    (^(Vector2&) @uiVertex.uv0).x = (__Null) ((zero2.x - zero1.x) * (double) num15 + zero1.x);
    uiVertex.position = (__Null) Vector2.op_Implicit(new Vector2(Mathf.Cos(num14) * this.RadiusMin, Mathf.Sin(num14) * this.RadiusMin));
    uiVertex.color = (__Null) Color32.op_Implicit(canvasRenderer.GetColor());
    // ISSUE: explicit reference operation
    // ISSUE: cast to a reference type
    // ISSUE: explicit reference operation
    (^(Vector2&) @uiVertex.uv0).y = zero1.y;
    vh.AddVert(uiVertex);
    uiVertex.position = (__Null) Vector2.op_Implicit(new Vector2(Mathf.Cos(num14) * this.RadiusMax, Mathf.Sin(num14) * this.RadiusMax));
    uiVertex.color = (__Null) Color32.op_Implicit(canvasRenderer.GetColor());
    // ISSUE: explicit reference operation
    // ISSUE: cast to a reference type
    // ISSUE: explicit reference operation
    (^(Vector2&) @uiVertex.uv0).y = zero2.y;
    vh.AddVert(uiVertex);
    vh.AddTriangle(num6, num6 + 1, num6 + 2);
    vh.AddTriangle(num6 + 2, num6 + 3, num6);
    int num16 = num6 + 4;
  }

  public virtual bool Raycast(Vector2 sp, Camera eventCamera)
  {
    return false;
  }
}
