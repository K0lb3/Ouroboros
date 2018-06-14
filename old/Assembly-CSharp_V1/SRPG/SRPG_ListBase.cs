// Decompiled with JetBrains decompiler
// Type: SRPG.SRPG_ListBase
// Assembly: Assembly-CSharp, Version=1.2.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9BA76916-D0BD-4DB6-A90B-FE0BCC53E511
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace SRPG
{
  public class SRPG_ListBase : MonoBehaviour
  {
    private Transform mItemBodyPool;
    private List<ListItemEvents> mItems;
    private ScrollRect mScrollRect;
    private RectTransform mTransform;
    private RectTransform mScrollRectTransform;

    public SRPG_ListBase()
    {
      base.\u002Ector();
    }

    public void AddItem(ListItemEvents item)
    {
      this.mItems.Add(item);
      this.InitPool();
      item.DetachBody(this.mItemBodyPool);
    }

    public void ClearItems()
    {
      for (int index = 0; index < this.mItems.Count; ++index)
      {
        if (Object.op_Inequality((Object) this.mItems[index].Body, (Object) null))
        {
          Object.Destroy((Object) ((Component) this.mItems[index].Body).get_gameObject());
          this.mItems[index].Body = (Transform) null;
        }
      }
      GameUtility.DestroyGameObjects<ListItemEvents>(this.mItems);
    }

    protected bool IsEmpty
    {
      get
      {
        return this.mItems.Count == 0;
      }
    }

    protected ListItemEvents[] Items
    {
      get
      {
        return this.mItems.ToArray();
      }
    }

    private void InitPool()
    {
      if (!Object.op_Equality((Object) this.mItemBodyPool, (Object) null))
        return;
      this.mItemBodyPool = (Transform) UIUtility.Pool;
    }

    protected virtual ScrollRect GetScrollRect()
    {
      return (ScrollRect) ((Component) this).GetComponentInParent<ScrollRect>();
    }

    protected virtual RectTransform GetRectTransform()
    {
      return ((Component) this).get_transform() as RectTransform;
    }

    protected virtual void Start()
    {
      this.mScrollRect = this.GetScrollRect();
      this.mTransform = this.GetRectTransform();
      if (Object.op_Inequality((Object) this.mScrollRect, (Object) null))
        this.mScrollRectTransform = ((Component) this.mScrollRect).get_transform() as RectTransform;
      this.InitPool();
    }

    protected virtual void OnDestroy()
    {
      for (int index = 0; index < this.mItems.Count; ++index)
      {
        if (Object.op_Inequality((Object) this.mItems[index], (Object) null) && Object.op_Inequality((Object) this.mItems[index].Body, (Object) null))
        {
          Object.Destroy((Object) ((Component) this.mItems[index].Body).get_gameObject());
          this.mItems[index].Body = (Transform) null;
        }
      }
    }

    protected virtual void LateUpdate()
    {
      if (!Object.op_Inequality((Object) this.mScrollRect, (Object) null))
        return;
      Rect rect1 = this.mScrollRectTransform.get_rect();
      Vector2 vector2_1 = (Vector2) null;
      Vector2 vector2_2 = (Vector2) null;
      // ISSUE: explicit reference operation
      vector2_1.x = (__Null) (double) Mathf.Lerp(0.0f, ((Rect) @rect1).get_width(), (float) this.mTransform.get_anchorMin().x);
      // ISSUE: explicit reference operation
      vector2_1.y = (__Null) (double) Mathf.Lerp(0.0f, ((Rect) @rect1).get_height(), (float) this.mTransform.get_anchorMin().y);
      // ISSUE: explicit reference operation
      vector2_2.x = (__Null) (double) Mathf.Lerp(0.0f, ((Rect) @rect1).get_width(), (float) this.mTransform.get_anchorMax().x);
      // ISSUE: explicit reference operation
      vector2_2.y = (__Null) (double) Mathf.Lerp(0.0f, ((Rect) @rect1).get_height(), (float) this.mTransform.get_anchorMax().y);
      Vector2 vector2_3 = (Vector2) null;
      vector2_3.x = (__Null) (double) Mathf.Lerp((float) vector2_1.x, (float) vector2_2.x, (float) this.mTransform.get_pivot().x);
      vector2_3.y = (__Null) (double) Mathf.Lerp((float) vector2_1.y, (float) vector2_2.y, (float) this.mTransform.get_pivot().y);
      Vector2 anchoredPosition = this.mTransform.get_anchoredPosition();
      Rect rect2 = this.mTransform.get_rect();
      // ISSUE: explicit reference operation
      Vector2 position = ((Rect) @rect2).get_position();
      Vector2 vector2_4 = Vector2.op_Addition(Vector2.op_Addition(anchoredPosition, position), vector2_3);
      Rect rect3 = this.mTransform.get_rect();
      // ISSUE: explicit reference operation
      float height = ((Rect) @rect3).get_height();
      // ISSUE: explicit reference operation
      ((Rect) @rect1).set_position(Vector2.op_UnaryNegation(vector2_4));
      for (int index = this.mItems.Count - 1; index >= 0; --index)
      {
        if (!Object.op_Equality((Object) this.mItems[index], (Object) null) && ((Component) this.mItems[index]).get_gameObject().get_activeInHierarchy())
        {
          RectTransform rectTransform = this.mItems[index].GetRectTransform();
          Rect rect4 = rectTransform.get_rect();
          // ISSUE: explicit reference operation
          // ISSUE: variable of a reference type
          Rect& local1 = @rect4;
          ((Rect) local1).set_x(((Rect) local1).get_x() + (float) rectTransform.get_anchoredPosition().x);
          // ISSUE: explicit reference operation
          // ISSUE: variable of a reference type
          Rect& local2 = @rect4;
          ((Rect) local2).set_y(((Rect) local2).get_y() + (height + (float) rectTransform.get_anchoredPosition().y));
          // ISSUE: explicit reference operation
          if (((Rect) @rect4).Overlaps(rect1))
            this.mItems[index].AttachBody();
          else
            this.mItems[index].DetachBody(this.mItemBodyPool);
        }
      }
    }
  }
}
