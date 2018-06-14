// Decompiled with JetBrains decompiler
// Type: SyncSize
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using UnityEngine;

[DisallowMultipleComponent]
public class SyncSize : MonoBehaviour
{
  public RectTransform Source;
  public float ExtraW;
  public float ExtraH;
  private float mLastWidth;
  private float mLastHeight;
  private RectTransform mRect;

  public SyncSize()
  {
    base.\u002Ector();
  }

  private void Start()
  {
    this.mRect = ((Component) this).get_transform() as RectTransform;
    this.Sync();
  }

  private void LateUpdate()
  {
    this.Sync();
  }

  private void Sync()
  {
    if (Object.op_Equality((Object) this.Source, (Object) null) || Object.op_Equality((Object) this.mRect, (Object) null))
      return;
    Rect rect1 = this.Source.get_rect();
    // ISSUE: explicit reference operation
    float num1 = ((Rect) @rect1).get_width() + this.ExtraW;
    Rect rect2 = this.Source.get_rect();
    // ISSUE: explicit reference operation
    float num2 = ((Rect) @rect2).get_height() + this.ExtraH;
    if ((double) num1 == (double) this.mLastWidth && (double) num2 == (double) this.mLastHeight)
      return;
    this.mLastWidth = num1;
    this.mLastHeight = num2;
    Vector2 vector2 = (Vector2) null;
    vector2.x = (__Null) (double) this.mLastWidth;
    vector2.y = (__Null) (double) this.mLastHeight;
    this.mRect.set_sizeDelta(vector2);
  }
}
