// Decompiled with JetBrains decompiler
// Type: SRPG.SyncScroll
// Assembly: Assembly-CSharp, Version=1.2.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9BA76916-D0BD-4DB6-A90B-FE0BCC53E511
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using UnityEngine;
using UnityEngine.UI;

namespace SRPG
{
  public class SyncScroll : MonoBehaviour
  {
    [SerializeField]
    private ScrollRect m_ScrollRect;
    [SerializeField]
    private SyncScroll.ScrollMode m_ScrollMode;
    [SerializeField]
    private RectTransform parent;
    private RectTransform rectTransform;

    public SyncScroll()
    {
      base.\u002Ector();
    }

    public bool isNormal
    {
      get
      {
        return this.m_ScrollMode == SyncScroll.ScrollMode.Normal;
      }
      set
      {
        this.m_ScrollMode = !value ? SyncScroll.ScrollMode.Reverse : SyncScroll.ScrollMode.Normal;
      }
    }

    public bool isReverse
    {
      get
      {
        return this.m_ScrollMode == SyncScroll.ScrollMode.Reverse;
      }
      set
      {
        this.m_ScrollMode = !value ? SyncScroll.ScrollMode.Normal : SyncScroll.ScrollMode.Reverse;
      }
    }

    public SyncScroll.ScrollMode scrollMode
    {
      get
      {
        return this.m_ScrollMode;
      }
      set
      {
        this.m_ScrollMode = value;
      }
    }

    private void Awake()
    {
      this.rectTransform = (RectTransform) ((Component) this).GetComponent<RectTransform>();
      this.parent = (RectTransform) ((Component) this).GetComponentInParent<RectTransform>();
      ((Behaviour) this).set_enabled(Object.op_Inequality((Object) this.m_ScrollRect, (Object) null) && Object.op_Inequality((Object) this.rectTransform, (Object) null));
    }

    private void LateUpdate()
    {
      if (this.m_ScrollRect.get_horizontal())
      {
        Vector2 anchoredPosition = this.rectTransform.get_anchoredPosition();
        if (this.m_ScrollMode == SyncScroll.ScrollMode.Normal)
        {
          // ISSUE: explicit reference operation
          // ISSUE: variable of a reference type
          Vector2& local = @anchoredPosition;
          // ISSUE: variable of the null type
          __Null x = this.rectTransform.get_sizeDelta().x;
          Rect rect = this.parent.get_rect();
          // ISSUE: explicit reference operation
          double num1 = ((Rect) @rect).get_size().x * 0.5;
          double num2 = (x + num1) * this.m_ScrollRect.get_normalizedPosition().x;
          // ISSUE: explicit reference operation
          (^local).x = (__Null) num2;
        }
        else
        {
          // ISSUE: explicit reference operation
          // ISSUE: variable of a reference type
          Vector2& local = @anchoredPosition;
          // ISSUE: variable of the null type
          __Null x = this.rectTransform.get_sizeDelta().x;
          Rect rect = this.parent.get_rect();
          // ISSUE: explicit reference operation
          double num1 = ((Rect) @rect).get_size().x * 0.5;
          double num2 = -(x - num1) * this.m_ScrollRect.get_normalizedPosition().x;
          // ISSUE: explicit reference operation
          (^local).x = (__Null) num2;
        }
        this.rectTransform.set_anchoredPosition(anchoredPosition);
      }
      if (!this.m_ScrollRect.get_vertical())
        return;
      Vector2 anchoredPosition1 = this.rectTransform.get_anchoredPosition();
      if (this.m_ScrollMode == SyncScroll.ScrollMode.Normal)
      {
        // ISSUE: explicit reference operation
        // ISSUE: variable of a reference type
        Vector2& local = @anchoredPosition1;
        // ISSUE: variable of the null type
        __Null y = this.rectTransform.get_sizeDelta().y;
        Rect rect1 = this.parent.get_rect();
        // ISSUE: explicit reference operation
        double num1 = ((Rect) @rect1).get_size().y * 0.5;
        double num2 = -(y - num1) * this.m_ScrollRect.get_normalizedPosition().y;
        Rect rect2 = this.parent.get_rect();
        // ISSUE: explicit reference operation
        double num3 = ((Rect) @rect2).get_size().y * 0.5;
        double num4 = num2 + num3;
        // ISSUE: explicit reference operation
        (^local).y = (__Null) num4;
      }
      else
      {
        // ISSUE: explicit reference operation
        // ISSUE: variable of a reference type
        Vector2& local = @anchoredPosition1;
        // ISSUE: variable of the null type
        __Null y = this.rectTransform.get_sizeDelta().y;
        Rect rect = this.parent.get_rect();
        // ISSUE: explicit reference operation
        double num1 = ((Rect) @rect).get_size().y * 0.5;
        double num2 = -(y - num1) * this.m_ScrollRect.get_normalizedPosition().y;
        // ISSUE: explicit reference operation
        (^local).y = (__Null) num2;
      }
      this.rectTransform.set_anchoredPosition(anchoredPosition1);
    }

    public enum ScrollMode
    {
      Normal,
      Reverse,
    }
  }
}
