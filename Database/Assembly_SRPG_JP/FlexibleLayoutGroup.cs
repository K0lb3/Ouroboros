// Decompiled with JetBrains decompiler
// Type: SRPG.FlexibleLayoutGroup
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 85BFDF7F-5712-4D45-9CD6-3465C703DFDF
// Assembly location: S:\Desktop\Assembly-CSharp.dll

using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace SRPG
{
  [DisallowMultipleComponent]
  [RequireComponent(typeof (RectTransform))]
  [ExecuteInEditMode]
  public class FlexibleLayoutGroup : UIBehaviour, ILayoutGroup, ILayoutElement, ILayoutController
  {
    [SerializeField]
    private RectOffset m_Padding;
    [SerializeField]
    private Vector2 m_Spacing;
    [SerializeField]
    private FlexibleLayoutGroup.Axis m_StartAxis;
    [SerializeField]
    [HideInInspector]
    private float m_LineSpace;
    [SerializeField]
    [HideInInspector]
    private float m_ColumnSpace;
    private List<RectTransform> m_RectChildren;
    private Vector2 m_TotalMinSize;
    private Vector2 m_TotalPreferredSize;
    private Vector2 m_TotalFlexibleSize;
    private RectTransform m_RectTransform;

    protected FlexibleLayoutGroup()
    {
      base.\u002Ector();
      if (this.m_Padding != null)
        return;
      this.m_Padding = new RectOffset();
    }

    private List<RectTransform> rectChildren
    {
      get
      {
        if (this.m_RectChildren == null)
          this.m_RectChildren = new List<RectTransform>();
        return this.m_RectChildren;
      }
    }

    private RectTransform rectTransform
    {
      get
      {
        if (Object.op_Equality((Object) this.m_RectTransform, (Object) null))
          this.m_RectTransform = (RectTransform) ((Component) this).GetComponent<RectTransform>();
        return this.m_RectTransform;
      }
    }

    private bool isRootLayoutGroup
    {
      get
      {
        if (Object.op_Equality((Object) ((Component) this).get_transform().get_parent(), (Object) null))
          return true;
        return Object.op_Equality((Object) ((Component) ((Component) this).get_transform().get_parent()).GetComponent(typeof (ILayoutGroup)), (Object) null);
      }
    }

    public virtual float minWidth
    {
      get
      {
        return this.GetTotalMinSize(0);
      }
    }

    public virtual float preferredWidth
    {
      get
      {
        return this.GetTotalPreferredSize(0);
      }
    }

    public virtual float flexibleWidth
    {
      get
      {
        return this.GetTotalFlexibleSize(0);
      }
    }

    public virtual float minHeight
    {
      get
      {
        return this.GetTotalMinSize(1);
      }
    }

    public virtual float preferredHeight
    {
      get
      {
        return this.GetTotalPreferredSize(1);
      }
    }

    public virtual float flexibleHeight
    {
      get
      {
        return this.GetTotalFlexibleSize(1);
      }
    }

    public virtual int layoutPriority
    {
      get
      {
        return 0;
      }
    }

    protected float GetTotalMinSize(int axis)
    {
      // ISSUE: explicit reference operation
      return ((Vector2) @this.m_TotalMinSize).get_Item(axis);
    }

    protected float GetTotalPreferredSize(int axis)
    {
      // ISSUE: explicit reference operation
      return ((Vector2) @this.m_TotalPreferredSize).get_Item(axis);
    }

    protected float GetTotalFlexibleSize(int axis)
    {
      // ISSUE: explicit reference operation
      return ((Vector2) @this.m_TotalFlexibleSize).get_Item(axis);
    }

    public virtual void CalculateLayoutInputHorizontal()
    {
      this.rectChildren.Clear();
      for (int index = 0; index < ((Transform) this.rectTransform).get_childCount(); ++index)
      {
        RectTransform child = ((Transform) this.rectTransform).GetChild(index) as RectTransform;
        if (((Component) child).get_gameObject().get_activeInHierarchy())
        {
          ILayoutIgnorer component = (ILayoutIgnorer) ((Component) child).get_gameObject().GetComponent<ILayoutIgnorer>();
          if (component == null || !component.get_ignoreLayout())
            this.rectChildren.Add(child);
        }
      }
    }

    public virtual void CalculateLayoutInputVertical()
    {
    }

    public virtual void SetLayoutHorizontal()
    {
      this.UpdateLayout();
    }

    public virtual void SetLayoutVertical()
    {
      this.UpdateLayout();
    }

    private void SetChildAlongAxis(RectTransform rect, int axis, float pos, float size)
    {
      if (Object.op_Equality((Object) rect, (Object) null))
        return;
      rect.SetInsetAndSizeFromParentEdge(axis != 0 ? (RectTransform.Edge) 2 : (RectTransform.Edge) 0, pos, size);
    }

    private void UpdateLayout()
    {
      Bounds rectTransformBounds1 = RectTransformUtility.CalculateRelativeRectTransformBounds((Transform) this.rectTransform);
      // ISSUE: explicit reference operation
      // ISSUE: variable of a reference type
      Bounds& local1 = @rectTransformBounds1;
      ((Bounds) local1).set_min(Vector3.op_Addition(((Bounds) local1).get_min(), new Vector3((float) this.m_Padding.get_left(), (float) this.m_Padding.get_bottom())));
      // ISSUE: explicit reference operation
      // ISSUE: variable of a reference type
      Bounds& local2 = @rectTransformBounds1;
      ((Bounds) local2).set_max(Vector3.op_Subtraction(((Bounds) local2).get_max(), new Vector3((float) this.m_Padding.get_right(), (float) this.m_Padding.get_top())));
      Vector2 sizeDelta1 = this.rectTransform.get_sizeDelta();
      Vector2 vector2_1;
      // ISSUE: explicit reference operation
      ((Vector2) @vector2_1).\u002Ector((float) this.m_Padding.get_left(), (float) this.m_Padding.get_top());
      Vector2 vector2_2 = vector2_1;
      for (int index = 0; index < this.rectChildren.Count; ++index)
      {
        Vector2 sizeDelta2 = this.rectChildren[index].get_sizeDelta();
        // ISSUE: explicit reference operation
        // ISSUE: explicit reference operation
        this.SetChildAlongAxis(this.rectChildren[index], 0, ((Vector2) @vector2_2).get_Item(0), ((Vector2) @sizeDelta2).get_Item(0));
        // ISSUE: explicit reference operation
        // ISSUE: explicit reference operation
        this.SetChildAlongAxis(this.rectChildren[index], 1, ((Vector2) @vector2_2).get_Item(1), ((Vector2) @sizeDelta2).get_Item(1));
        Bounds rectTransformBounds2 = RectTransformUtility.CalculateRelativeRectTransformBounds((Transform) this.rectTransform, (Transform) this.rectChildren[index]);
        if (this.m_StartAxis == FlexibleLayoutGroup.Axis.Horizontal)
        {
          // ISSUE: explicit reference operation
          // ISSUE: explicit reference operation
          if (((Bounds) @rectTransformBounds1).get_max().x <= ((Bounds) @rectTransformBounds2).get_max().x)
          {
            // ISSUE: explicit reference operation
            // ISSUE: explicit reference operation
            ((Vector2) @vector2_2).set_Item(0, ((Vector2) @vector2_1).get_Item(0));
            // ISSUE: variable of a reference type
            Vector2& local3;
            int num;
            // ISSUE: explicit reference operation
            // ISSUE: explicit reference operation
            ((Vector2) (local3 = @vector2_2)).set_Item(num = 1, ((Vector2) local3).get_Item(num) + (this.m_LineSpace + ((Vector2) @this.m_Spacing).get_Item(1)));
            // ISSUE: explicit reference operation
            // ISSUE: explicit reference operation
            this.SetChildAlongAxis(this.rectChildren[index], 0, ((Vector2) @vector2_2).get_Item(0), ((Vector2) @sizeDelta2).get_Item(0));
            // ISSUE: explicit reference operation
            // ISSUE: explicit reference operation
            this.SetChildAlongAxis(this.rectChildren[index], 1, ((Vector2) @vector2_2).get_Item(1), ((Vector2) @sizeDelta2).get_Item(1));
          }
          // ISSUE: variable of a reference type
          Vector2& local4;
          int num1;
          // ISSUE: explicit reference operation
          // ISSUE: explicit reference operation
          // ISSUE: explicit reference operation
          ((Vector2) (local4 = @vector2_2)).set_Item(num1 = 0, ((Vector2) local4).get_Item(num1) + (((Vector2) @sizeDelta2).get_Item(0) + ((Vector2) @this.m_Spacing).get_Item(0)));
        }
        else
        {
          // ISSUE: explicit reference operation
          // ISSUE: explicit reference operation
          if (((Bounds) @rectTransformBounds1).get_min().y >= ((Bounds) @rectTransformBounds2).get_min().y)
          {
            // ISSUE: variable of a reference type
            Vector2& local3;
            int num;
            // ISSUE: explicit reference operation
            // ISSUE: explicit reference operation
            ((Vector2) (local3 = @vector2_2)).set_Item(num = 0, ((Vector2) local3).get_Item(num) + (this.m_ColumnSpace + ((Vector2) @this.m_Spacing).get_Item(0)));
            // ISSUE: explicit reference operation
            // ISSUE: explicit reference operation
            ((Vector2) @vector2_2).set_Item(1, ((Vector2) @vector2_1).get_Item(1));
            // ISSUE: explicit reference operation
            // ISSUE: explicit reference operation
            this.SetChildAlongAxis(this.rectChildren[index], 0, ((Vector2) @vector2_2).get_Item(0), ((Vector2) @sizeDelta2).get_Item(0));
            // ISSUE: explicit reference operation
            // ISSUE: explicit reference operation
            this.SetChildAlongAxis(this.rectChildren[index], 1, ((Vector2) @vector2_2).get_Item(1), ((Vector2) @sizeDelta2).get_Item(1));
          }
          // ISSUE: variable of a reference type
          Vector2& local4;
          int num1;
          // ISSUE: explicit reference operation
          // ISSUE: explicit reference operation
          // ISSUE: explicit reference operation
          ((Vector2) (local4 = @vector2_2)).set_Item(num1 = 1, ((Vector2) local4).get_Item(num1) + (((Vector2) @sizeDelta2).get_Item(1) + ((Vector2) @this.m_Spacing).get_Item(1)));
        }
      }
      if (this.m_StartAxis == FlexibleLayoutGroup.Axis.Horizontal)
      {
        // ISSUE: explicit reference operation
        // ISSUE: explicit reference operation
        ((Vector2) @this.m_TotalMinSize).set_Item(0, ((Vector2) @sizeDelta1).get_Item(0));
        // ISSUE: explicit reference operation
        // ISSUE: explicit reference operation
        ((Vector2) @this.m_TotalMinSize).set_Item(1, ((Vector2) @vector2_2).get_Item(1) + (float) this.m_Padding.get_bottom() + this.m_LineSpace);
        // ISSUE: explicit reference operation
        // ISSUE: explicit reference operation
        ((Vector2) @this.m_TotalPreferredSize).set_Item(0, ((Vector2) @sizeDelta1).get_Item(0));
        // ISSUE: explicit reference operation
        // ISSUE: explicit reference operation
        ((Vector2) @this.m_TotalPreferredSize).set_Item(1, ((Vector2) @vector2_2).get_Item(1) + (float) this.m_Padding.get_bottom() + this.m_LineSpace);
        // ISSUE: explicit reference operation
        ((Vector2) @this.m_TotalFlexibleSize).set_Item(0, 0.0f);
        // ISSUE: explicit reference operation
        ((Vector2) @this.m_TotalFlexibleSize).set_Item(1, 0.0f);
      }
      else
      {
        // ISSUE: explicit reference operation
        // ISSUE: explicit reference operation
        ((Vector2) @this.m_TotalMinSize).set_Item(0, ((Vector2) @vector2_2).get_Item(0) + (float) this.m_Padding.get_right() + this.m_ColumnSpace);
        // ISSUE: explicit reference operation
        // ISSUE: explicit reference operation
        ((Vector2) @this.m_TotalMinSize).set_Item(1, ((Vector2) @sizeDelta1).get_Item(1));
        // ISSUE: explicit reference operation
        // ISSUE: explicit reference operation
        ((Vector2) @this.m_TotalPreferredSize).set_Item(0, ((Vector2) @vector2_2).get_Item(0) + (float) this.m_Padding.get_right() + this.m_ColumnSpace);
        // ISSUE: explicit reference operation
        // ISSUE: explicit reference operation
        ((Vector2) @this.m_TotalPreferredSize).set_Item(1, ((Vector2) @sizeDelta1).get_Item(1));
        // ISSUE: explicit reference operation
        ((Vector2) @this.m_TotalFlexibleSize).set_Item(0, 0.0f);
        // ISSUE: explicit reference operation
        ((Vector2) @this.m_TotalFlexibleSize).set_Item(1, 0.0f);
      }
    }

    protected virtual void OnEnable()
    {
      base.OnEnable();
      this.SetDirty();
    }

    protected virtual void OnDisable()
    {
      LayoutRebuilder.MarkLayoutForRebuild(this.rectTransform);
      base.OnDisable();
    }

    protected virtual void OnDidApplyAnimationProperties()
    {
      this.SetDirty();
    }

    protected virtual void OnRectTransformDimensionsChange()
    {
      base.OnRectTransformDimensionsChange();
      if (!this.isRootLayoutGroup)
        return;
      this.SetDirty();
    }

    protected virtual void OnTransformChildrenChanged()
    {
      this.SetDirty();
    }

    protected void SetDirty()
    {
      if (!this.IsActive())
        return;
      LayoutRebuilder.MarkLayoutForRebuild(this.rectTransform);
    }

    public enum Constraint
    {
      Flexible,
      FixedColumnCount,
      FixedRowCount,
    }

    public enum Corner
    {
      UpperLeft,
      UpperRight,
      LowerLeft,
      LowerRight,
    }

    public enum Axis
    {
      Horizontal,
      Vertical,
    }
  }
}
