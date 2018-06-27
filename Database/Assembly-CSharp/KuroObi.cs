// Decompiled with JetBrains decompiler
// Type: KuroObi
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using UnityEngine;
using UnityEngine.UI;

[ExecuteInEditMode]
public class KuroObi : Graphic
{
  public KuroObi()
  {
    base.\u002Ector();
  }

  public static float CalcObiSize()
  {
    if (!SRPG_CanvasScaler.UseKuroObi)
      return 0.0f;
    float num1 = (float) Screen.get_width() / (float) Screen.get_height();
    float num2 = 1.6f;
    if ((double) num1 >= (double) num2)
      return 0.0f;
    return (float) ((750.0 / (double) (num1 / num2) - 750.0) * 0.5);
  }

  public virtual bool Raycast(Vector2 sp, Camera eventCamera)
  {
    RectTransform transform = ((Component) this).get_transform() as RectTransform;
    Rect rect = transform.get_rect();
    float num = KuroObi.CalcObiSize();
    Vector2 vector2;
    RectTransformUtility.ScreenPointToLocalPointInRectangle(transform, sp, (Camera) null, ref vector2);
    // ISSUE: explicit reference operation
    // ISSUE: explicit reference operation
    return vector2.y < (double) ((Rect) @rect).get_yMin() + (double) num || (double) ((Rect) @rect).get_yMax() - (double) num < vector2.y;
  }

  protected virtual void OnPopulateMesh(VertexHelper vh)
  {
    vh.Clear();
    Color32 color32 = Color32.op_Implicit(this.get_color());
    Rect rect = (((Component) this).get_transform() as RectTransform).get_rect();
    UIVertex uiVertex = (UIVertex) null;
    float num1 = KuroObi.CalcObiSize();
    // ISSUE: explicit reference operation
    // ISSUE: explicit reference operation
    uiVertex.position = (__Null) Vector2.op_Implicit(new Vector2(((Rect) @rect).get_xMin(), ((Rect) @rect).get_yMax()));
    uiVertex.color = (__Null) color32;
    vh.AddVert(uiVertex);
    // ISSUE: explicit reference operation
    // ISSUE: explicit reference operation
    uiVertex.position = (__Null) Vector2.op_Implicit(new Vector2(((Rect) @rect).get_xMax(), ((Rect) @rect).get_yMax()));
    uiVertex.color = (__Null) color32;
    vh.AddVert(uiVertex);
    // ISSUE: explicit reference operation
    float num2 = ((Rect) @rect).get_yMax() - num1;
    // ISSUE: explicit reference operation
    uiVertex.position = (__Null) Vector2.op_Implicit(new Vector2(((Rect) @rect).get_xMax(), num2));
    uiVertex.color = (__Null) color32;
    vh.AddVert(uiVertex);
    // ISSUE: explicit reference operation
    uiVertex.position = (__Null) Vector2.op_Implicit(new Vector2(((Rect) @rect).get_xMin(), num2));
    uiVertex.color = (__Null) color32;
    vh.AddVert(uiVertex);
    // ISSUE: explicit reference operation
    float num3 = ((Rect) @rect).get_yMin() + num1;
    // ISSUE: explicit reference operation
    uiVertex.position = (__Null) Vector2.op_Implicit(new Vector2(((Rect) @rect).get_xMin(), num3));
    uiVertex.color = (__Null) color32;
    vh.AddVert(uiVertex);
    // ISSUE: explicit reference operation
    uiVertex.position = (__Null) Vector2.op_Implicit(new Vector2(((Rect) @rect).get_xMax(), num3));
    uiVertex.color = (__Null) color32;
    vh.AddVert(uiVertex);
    // ISSUE: explicit reference operation
    // ISSUE: explicit reference operation
    uiVertex.position = (__Null) Vector2.op_Implicit(new Vector2(((Rect) @rect).get_xMax(), ((Rect) @rect).get_yMin()));
    uiVertex.color = (__Null) color32;
    vh.AddVert(uiVertex);
    // ISSUE: explicit reference operation
    // ISSUE: explicit reference operation
    uiVertex.position = (__Null) Vector2.op_Implicit(new Vector2(((Rect) @rect).get_xMin(), ((Rect) @rect).get_yMin()));
    uiVertex.color = (__Null) color32;
    vh.AddVert(uiVertex);
  }
}
