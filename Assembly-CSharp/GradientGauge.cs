// Decompiled with JetBrains decompiler
// Type: GradientGauge
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof (CanvasRenderer))]
public class GradientGauge : Image
{
  public Color32[] Colors;
  [Range(0.0f, 1f)]
  public float Value;
  private float mStartValue;
  private float mEndValue;
  private float mAnimateTime;
  private float mCurrentTime;

  public GradientGauge()
  {
    base.\u002Ector();
  }

  public void UpdateColors(Color32[] colors)
  {
    this.Colors = colors;
    ((Graphic) this).UpdateGeometry();
  }

  public bool IsAnimating
  {
    get
    {
      return (double) this.mCurrentTime < (double) this.mAnimateTime;
    }
  }

  public void AnimateRangedValue(int current, int maxValue, float time)
  {
    this.AnimateValue(Mathf.Clamp01((float) current / (float) maxValue), time);
  }

  public void AnimateValue(float newValue, float time)
  {
    this.mStartValue = this.Value;
    this.mEndValue = newValue;
    this.mAnimateTime = time;
    this.mCurrentTime = 0.0f;
    if ((double) time <= 0.0)
      this.Value = this.mEndValue;
    ((Graphic) this).UpdateGeometry();
  }

  private void Update()
  {
    if ((double) this.mCurrentTime >= (double) this.mAnimateTime)
      return;
    this.mCurrentTime += Time.get_deltaTime();
    this.Value = Mathf.Lerp(this.mStartValue, this.mEndValue, Mathf.Clamp01(this.mCurrentTime / this.mAnimateTime));
    ((Graphic) this).UpdateGeometry();
  }

  private Color32 ApplyBaseColor(Color32 c)
  {
    Color color = ((Graphic) this).get_color();
    c.r = (__Null) (int) (byte) ((double) (float) c.r * color.r);
    c.g = (__Null) (int) (byte) ((double) (float) c.g * color.g);
    c.b = (__Null) (int) (byte) ((double) (float) c.b * color.b);
    c.a = (__Null) (int) (byte) (color.a * (double) byte.MaxValue);
    return c;
  }

  protected virtual void OnPopulateMesh(VertexHelper vh)
  {
    vh.Clear();
    if (Object.op_Equality((Object) this.get_sprite(), (Object) null) || this.Colors.Length <= 0 || (double) this.Value <= 0.0)
      return;
    Sprite sprite = this.get_sprite();
    Vector4 border = sprite.get_border();
    Rect rect1 = sprite.get_rect();
    Rect rect2 = (((Component) this).get_transform() as RectTransform).get_rect();
    float num1 = 1f / (float) ((Texture) sprite.get_texture()).get_width();
    float num2 = 1f / (float) ((Texture) sprite.get_texture()).get_height();
    UIVertex simpleVert = (UIVertex) UIVertex.simpleVert;
    int num3 = 0;
    // ISSUE: explicit reference operation
    // ISSUE: cast to a reference type
    // ISSUE: explicit reference operation
    // ISSUE: explicit reference operation
    (^(Vector3&) @simpleVert.position).x = (__Null) (double) ((Rect) @rect2).get_xMin();
    // ISSUE: explicit reference operation
    // ISSUE: cast to a reference type
    // ISSUE: explicit reference operation
    // ISSUE: explicit reference operation
    (^(Vector3&) @simpleVert.position).y = (__Null) (double) ((Rect) @rect2).get_yMin();
    simpleVert.color = (__Null) this.ApplyBaseColor(this.Colors[0]);
    // ISSUE: explicit reference operation
    // ISSUE: cast to a reference type
    // ISSUE: explicit reference operation
    // ISSUE: explicit reference operation
    (^(Vector2&) @simpleVert.uv0).x = (__Null) ((double) ((Rect) @rect1).get_xMin() * (double) num1);
    // ISSUE: explicit reference operation
    // ISSUE: cast to a reference type
    // ISSUE: explicit reference operation
    // ISSUE: explicit reference operation
    (^(Vector2&) @simpleVert.uv0).y = (__Null) ((double) ((Rect) @rect1).get_yMin() * (double) num2);
    vh.AddVert(simpleVert);
    // ISSUE: explicit reference operation
    // ISSUE: cast to a reference type
    // ISSUE: explicit reference operation
    // ISSUE: explicit reference operation
    (^(Vector3&) @simpleVert.position).y = (__Null) (double) ((Rect) @rect2).get_yMax();
    // ISSUE: explicit reference operation
    // ISSUE: cast to a reference type
    // ISSUE: explicit reference operation
    // ISSUE: explicit reference operation
    (^(Vector2&) @simpleVert.uv0).y = (__Null) ((double) ((Rect) @rect1).get_yMax() * (double) num2);
    vh.AddVert(simpleVert);
    // ISSUE: explicit reference operation
    // ISSUE: cast to a reference type
    // ISSUE: explicit reference operation
    // ISSUE: explicit reference operation
    (^(Vector3&) @simpleVert.position).x = (__Null) ((double) ((Rect) @rect2).get_xMin() + border.x);
    // ISSUE: explicit reference operation
    // ISSUE: cast to a reference type
    // ISSUE: explicit reference operation
    // ISSUE: explicit reference operation
    (^(Vector2&) @simpleVert.uv0).x = (__Null) (((double) ((Rect) @rect1).get_xMin() + border.x) * (double) num1);
    vh.AddVert(simpleVert);
    // ISSUE: explicit reference operation
    // ISSUE: cast to a reference type
    // ISSUE: explicit reference operation
    // ISSUE: explicit reference operation
    (^(Vector3&) @simpleVert.position).y = (__Null) (double) ((Rect) @rect2).get_yMin();
    // ISSUE: explicit reference operation
    // ISSUE: cast to a reference type
    // ISSUE: explicit reference operation
    // ISSUE: explicit reference operation
    (^(Vector2&) @simpleVert.uv0).y = (__Null) ((double) ((Rect) @rect1).get_yMin() * (double) num2);
    vh.AddVert(simpleVert);
    vh.AddTriangle(num3, num3 + 1, num3 + 2);
    vh.AddTriangle(num3 + 2, num3 + 3, num3);
    int num4 = num3 + 4;
    // ISSUE: explicit reference operation
    float num5 = (float) ((double) ((Rect) @rect2).get_width() - border.x - border.z) * this.Value;
    // ISSUE: explicit reference operation
    float num6 = (((Rect) @rect1).get_xMin() + (float) border.x) * num1;
    // ISSUE: explicit reference operation
    float num7 = (((Rect) @rect1).get_xMax() - (float) border.z) * num1;
    int num8 = 0;
    for (int index = 0; index < this.Colors.Length; ++index)
      num8 += (int) this.Colors[index].a;
    int num9 = 0;
    for (int index = 0; index < this.Colors.Length; ++index)
    {
      if (this.Colors[index].a > 0)
      {
        float num10 = (float) num9 / (float) num8;
        float num11 = (float) (num9 + this.Colors[index].a) / (float) num8;
        // ISSUE: explicit reference operation
        float num12 = (float) ((double) ((Rect) @rect2).get_xMin() + border.x + (double) num5 * (double) num10);
        // ISSUE: explicit reference operation
        float num13 = (float) ((double) ((Rect) @rect2).get_xMin() + border.x + (double) num5 * (double) num11);
        // ISSUE: explicit reference operation
        // ISSUE: cast to a reference type
        // ISSUE: explicit reference operation
        (^(Vector3&) @simpleVert.position).x = (__Null) (double) num12;
        // ISSUE: explicit reference operation
        // ISSUE: cast to a reference type
        // ISSUE: explicit reference operation
        // ISSUE: explicit reference operation
        (^(Vector3&) @simpleVert.position).y = (__Null) (double) ((Rect) @rect2).get_yMin();
        simpleVert.color = (__Null) this.ApplyBaseColor(this.Colors[index]);
        // ISSUE: explicit reference operation
        // ISSUE: cast to a reference type
        // ISSUE: explicit reference operation
        (^(Vector2&) @simpleVert.uv0).x = (__Null) (double) Mathf.Lerp(num6, num7, num10);
        // ISSUE: explicit reference operation
        // ISSUE: cast to a reference type
        // ISSUE: explicit reference operation
        // ISSUE: explicit reference operation
        (^(Vector2&) @simpleVert.uv0).y = (__Null) ((double) ((Rect) @rect1).get_yMin() * (double) num2);
        vh.AddVert(simpleVert);
        // ISSUE: explicit reference operation
        // ISSUE: cast to a reference type
        // ISSUE: explicit reference operation
        // ISSUE: explicit reference operation
        (^(Vector3&) @simpleVert.position).y = (__Null) (double) ((Rect) @rect2).get_yMax();
        // ISSUE: explicit reference operation
        // ISSUE: cast to a reference type
        // ISSUE: explicit reference operation
        // ISSUE: explicit reference operation
        (^(Vector2&) @simpleVert.uv0).y = (__Null) ((double) ((Rect) @rect1).get_yMax() * (double) num2);
        vh.AddVert(simpleVert);
        // ISSUE: explicit reference operation
        // ISSUE: cast to a reference type
        // ISSUE: explicit reference operation
        (^(Vector3&) @simpleVert.position).x = (__Null) (double) num13;
        // ISSUE: explicit reference operation
        // ISSUE: cast to a reference type
        // ISSUE: explicit reference operation
        (^(Vector2&) @simpleVert.uv0).x = (__Null) (double) Mathf.Lerp(num6, num7, num11);
        vh.AddVert(simpleVert);
        // ISSUE: explicit reference operation
        // ISSUE: cast to a reference type
        // ISSUE: explicit reference operation
        // ISSUE: explicit reference operation
        (^(Vector3&) @simpleVert.position).y = (__Null) (double) ((Rect) @rect2).get_yMin();
        // ISSUE: explicit reference operation
        // ISSUE: cast to a reference type
        // ISSUE: explicit reference operation
        // ISSUE: explicit reference operation
        (^(Vector2&) @simpleVert.uv0).y = (__Null) ((double) ((Rect) @rect1).get_yMin() * (double) num2);
        vh.AddVert(simpleVert);
        vh.AddTriangle(num4, num4 + 1, num4 + 2);
        vh.AddTriangle(num4 + 2, num4 + 3, num4);
        num4 += 4;
        num9 += (int) this.Colors[index].a;
      }
    }
    // ISSUE: explicit reference operation
    float num14 = ((Rect) @rect2).get_xMin() + (float) border.x + num5;
    // ISSUE: explicit reference operation
    // ISSUE: cast to a reference type
    // ISSUE: explicit reference operation
    (^(Vector3&) @simpleVert.position).x = (__Null) (double) num14;
    // ISSUE: explicit reference operation
    // ISSUE: cast to a reference type
    // ISSUE: explicit reference operation
    // ISSUE: explicit reference operation
    (^(Vector3&) @simpleVert.position).y = (__Null) (double) ((Rect) @rect2).get_yMin();
    // ISSUE: explicit reference operation
    // ISSUE: cast to a reference type
    // ISSUE: explicit reference operation
    // ISSUE: explicit reference operation
    (^(Vector2&) @simpleVert.uv0).x = (__Null) (((double) ((Rect) @rect1).get_xMax() - border.x) * (double) num1);
    // ISSUE: explicit reference operation
    // ISSUE: cast to a reference type
    // ISSUE: explicit reference operation
    // ISSUE: explicit reference operation
    (^(Vector2&) @simpleVert.uv0).y = (__Null) ((double) ((Rect) @rect1).get_yMin() * (double) num2);
    vh.AddVert(simpleVert);
    // ISSUE: explicit reference operation
    // ISSUE: cast to a reference type
    // ISSUE: explicit reference operation
    // ISSUE: explicit reference operation
    (^(Vector3&) @simpleVert.position).y = (__Null) (double) ((Rect) @rect2).get_yMax();
    // ISSUE: explicit reference operation
    // ISSUE: cast to a reference type
    // ISSUE: explicit reference operation
    // ISSUE: explicit reference operation
    (^(Vector2&) @simpleVert.uv0).y = (__Null) ((double) ((Rect) @rect1).get_yMax() * (double) num2);
    vh.AddVert(simpleVert);
    // ISSUE: explicit reference operation
    // ISSUE: cast to a reference type
    // ISSUE: explicit reference operation
    (^(Vector3&) @simpleVert.position).x = (__Null) ((double) num14 + border.z);
    // ISSUE: explicit reference operation
    // ISSUE: cast to a reference type
    // ISSUE: explicit reference operation
    // ISSUE: explicit reference operation
    (^(Vector2&) @simpleVert.uv0).x = (__Null) ((double) ((Rect) @rect1).get_xMax() * (double) num1);
    vh.AddVert(simpleVert);
    // ISSUE: explicit reference operation
    // ISSUE: cast to a reference type
    // ISSUE: explicit reference operation
    // ISSUE: explicit reference operation
    (^(Vector3&) @simpleVert.position).y = (__Null) (double) ((Rect) @rect2).get_yMin();
    // ISSUE: explicit reference operation
    // ISSUE: cast to a reference type
    // ISSUE: explicit reference operation
    // ISSUE: explicit reference operation
    (^(Vector2&) @simpleVert.uv0).y = (__Null) ((double) ((Rect) @rect1).get_yMin() * (double) num2);
    vh.AddVert(simpleVert);
    vh.AddTriangle(num4, num4 + 1, num4 + 2);
    vh.AddTriangle(num4 + 2, num4 + 3, num4);
    int num15 = num4 + 4;
  }
}
