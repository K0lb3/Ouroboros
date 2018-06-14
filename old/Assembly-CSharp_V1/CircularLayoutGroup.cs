// Decompiled with JetBrains decompiler
// Type: CircularLayoutGroup
// Assembly: Assembly-CSharp, Version=1.2.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9BA76916-D0BD-4DB6-A90B-FE0BCC53E511
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using System;
using UnityEngine;
using UnityEngine.UI;

[AddComponentMenu("Layout/Circular Layout Group")]
public class CircularLayoutGroup : LayoutGroup
{
  [SerializeField]
  protected float m_AngleMin;
  [SerializeField]
  protected float m_AngleMax;
  [SerializeField]
  protected float m_Radius;
  [Range(0.0f, 1f)]
  [SerializeField]
  protected float m_Fraction;
  [SerializeField]
  protected bool m_UseFullRange;
  [SerializeField]
  protected float m_FixedStride;

  public CircularLayoutGroup()
  {
    base.\u002Ector();
  }

  public float AngleMin
  {
    get
    {
      return this.m_AngleMin;
    }
    set
    {
      this.SetProperty<float>((M0&) @this.m_AngleMin, (M0) (double) value);
    }
  }

  public float AngleMax
  {
    get
    {
      return this.m_AngleMax;
    }
    set
    {
      this.SetProperty<float>((M0&) @this.m_AngleMax, (M0) (double) value);
    }
  }

  public float Radius
  {
    get
    {
      return this.m_Radius;
    }
    set
    {
      this.SetProperty<float>((M0&) @this.m_Radius, (M0) (double) value);
    }
  }

  public float Fraction
  {
    get
    {
      return this.m_Fraction;
    }
    set
    {
      this.SetProperty<float>((M0&) @this.m_Fraction, (M0) (double) value);
    }
  }

  public bool UseFullRange
  {
    get
    {
      return this.m_UseFullRange;
    }
    set
    {
      this.SetProperty<bool>((M0&) @this.m_UseFullRange, (M0) (value ? 1 : 0));
    }
  }

  public float FixedStride
  {
    get
    {
      return this.m_FixedStride;
    }
    set
    {
      this.SetProperty<float>((M0&) @this.m_FixedStride, (M0) (double) value);
    }
  }

  public virtual void CalculateLayoutInputHorizontal()
  {
    base.CalculateLayoutInputHorizontal();
  }

  public virtual void CalculateLayoutInputVertical()
  {
  }

  public virtual void SetLayoutHorizontal()
  {
  }

  public virtual void SetLayoutVertical()
  {
    RectTransform transform = ((Component) this).get_transform() as RectTransform;
    int childCount = ((Transform) transform).get_childCount();
    RectTransform[] rectTransformArray = new RectTransform[childCount];
    int num1 = 0;
    for (int index = 0; index < childCount; ++index)
    {
      RectTransform child = ((Transform) transform).GetChild(index) as RectTransform;
      if (((Component) child).get_gameObject().get_activeInHierarchy())
        rectTransformArray[num1++] = child;
    }
    float num2 = 0.0f;
    if (num1 > 0)
      num2 = (double) this.m_FixedStride <= 0.0 ? (!this.m_UseFullRange || num1 < 2 ? (this.AngleMax - this.AngleMin) / (float) num1 : (this.AngleMax - this.AngleMin) / (float) (num1 - 1)) : ((double) this.AngleMax >= (double) this.AngleMin ? this.m_FixedStride : -this.m_FixedStride);
    for (int index = 0; index < num1; ++index)
    {
      RectTransform rectTransform = rectTransformArray[index];
      float num3 = (float) (((double) this.AngleMin + (double) num2 * ((double) index + (double) this.m_Fraction)) * (Math.PI / 180.0));
      Vector2 vector2 = (Vector2) null;
      vector2.x = (__Null) ((double) Mathf.Cos(num3) * (double) this.m_Radius);
      vector2.y = (__Null) ((double) Mathf.Sin(num3) * (double) this.m_Radius);
      rectTransform.set_anchoredPosition(vector2);
    }
  }
}
