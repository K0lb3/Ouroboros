// Decompiled with JetBrains decompiler
// Type: ListExtras
// Assembly: Assembly-CSharp, Version=1.2.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9BA76916-D0BD-4DB6-A90B-FE0BCC53E511
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof (ScrollRect))]
[DisallowMultipleComponent]
[AddComponentMenu("UI/ListExtras")]
public class ListExtras : MonoBehaviour
{
  public Selectable PageUpButton;
  public Selectable PageDownButton;
  public float PageScrollTime;
  private Vector2 mScrollAnimStart;
  private Vector2 mScrollAnimEnd;
  private float mScrollAnimTime;
  private ScrollRect mScrollRect;

  public ListExtras()
  {
    base.\u002Ector();
  }

  protected void Awake()
  {
    this.mScrollRect = (ScrollRect) ((Component) this).GetComponent<ScrollRect>();
    if (!Object.op_Equality((Object) this.mScrollRect, (Object) null))
      return;
    ((Behaviour) this).set_enabled(false);
  }

  protected void Update()
  {
    if ((double) this.mScrollAnimTime >= 0.0)
    {
      this.mScrollAnimTime += Time.get_deltaTime();
      float num = (double) this.PageScrollTime <= 0.0 ? 1f : Mathf.Sin((float) ((double) Mathf.Clamp01(this.mScrollAnimTime / this.PageScrollTime) * 3.14159274101257 * 0.5));
      this.mScrollRect.set_normalizedPosition(Vector2.Lerp(this.mScrollAnimStart, this.mScrollAnimEnd, num));
      if ((double) num >= 1.0)
        this.mScrollAnimTime = -1f;
    }
    float num1 = Mathf.Abs(Vector2.Dot(this.mScrollRect.get_normalizedPosition(), this.ScrollDir));
    RectTransform transform1 = ((Component) this.mScrollRect).get_transform() as RectTransform;
    RectTransform transform2 = ((Component) this.mScrollRect.get_content()).get_transform() as RectTransform;
    if (!Object.op_Inequality((Object) this.mScrollRect.get_content(), (Object) null))
      return;
    Rect rect1 = transform1.get_rect();
    // ISSUE: explicit reference operation
    float num2 = Mathf.Abs(Vector2.Dot(((Rect) @rect1).get_size(), this.ScrollDir));
    Rect rect2 = transform2.get_rect();
    // ISSUE: explicit reference operation
    float num3 = Mathf.Abs(Vector2.Dot(((Rect) @rect2).get_size(), this.ScrollDir));
    if (this.mScrollRect.get_horizontal())
      num1 = 1f - num1;
    if (Object.op_Inequality((Object) this.PageUpButton, (Object) null))
      this.PageUpButton.set_interactable((double) num1 < 0.999000012874603 && (double) num2 < (double) num3);
    if (!Object.op_Inequality((Object) this.PageDownButton, (Object) null))
      return;
    this.PageDownButton.set_interactable((double) num1 > 1.0 / 1000.0 && (double) num2 < (double) num3);
  }

  private Vector2 ScrollDir
  {
    get
    {
      if (this.mScrollRect.get_vertical())
        return Vector2.op_UnaryNegation(Vector2.get_up());
      return Vector2.get_right();
    }
  }

  public void PageUp(float delta)
  {
    this.Scroll(-delta);
  }

  public void PageDown(float delta)
  {
    this.Scroll(delta);
  }

  public void SetScrollPos(float position)
  {
    this.mScrollRect.set_normalizedPosition(Vector2.op_Multiply(new Vector2(Mathf.Abs((float) this.ScrollDir.x), Mathf.Abs((float) this.ScrollDir.y)), position));
    this.mScrollAnimTime = -1f;
  }

  public void ScrollTo(float normalizedPosition)
  {
    this.mScrollAnimStart = this.mScrollRect.get_normalizedPosition();
    this.mScrollAnimEnd = Vector2.op_Multiply(this.ScrollDir, normalizedPosition);
    this.mScrollAnimTime = 0.0f;
  }

  private void Scroll(float delta)
  {
    Vector2 scrollDir = this.ScrollDir;
    RectTransform content = this.mScrollRect.get_content();
    RectTransform transform = (RectTransform) ((Component) this.mScrollRect).get_transform();
    Vector2 vector2_1 = scrollDir;
    Rect rect1 = transform.get_rect();
    // ISSUE: explicit reference operation
    Vector2 size1 = ((Rect) @rect1).get_size();
    float num1 = Mathf.Abs(Vector2.Dot(vector2_1, size1));
    Vector2 vector2_2 = scrollDir;
    Rect rect2 = content.get_rect();
    // ISSUE: explicit reference operation
    Vector2 size2 = ((Rect) @rect2).get_size();
    float num2 = Mathf.Abs(Vector2.Dot(vector2_2, size2)) - num1;
    if ((double) num2 <= 0.0)
      return;
    float num3 = num1 * delta / num2;
    Vector2 vector2_3 = Vector2.op_Addition(this.mScrollRect.get_normalizedPosition(), Vector2.op_Multiply(scrollDir, num3));
    vector2_3.x = (__Null) (double) Mathf.Clamp01((float) vector2_3.x);
    vector2_3.y = (__Null) (double) Mathf.Clamp01((float) vector2_3.y);
    this.mScrollAnimStart = this.mScrollRect.get_normalizedPosition();
    this.mScrollAnimEnd = vector2_3;
    this.mScrollAnimTime = 0.0f;
  }
}
