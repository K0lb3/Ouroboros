namespace SRPG
{
    using System;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.EventSystems;
    using UnityEngine.UI;

    [DisallowMultipleComponent, RequireComponent(typeof(RectTransform)), ExecuteInEditMode]
    public class FlexibleLayoutGroup : UIBehaviour, ILayoutGroup, ILayoutElement, ILayoutController
    {
        [SerializeField]
        private RectOffset m_Padding;
        [SerializeField]
        private Vector2 m_Spacing;
        [SerializeField]
        private Axis m_StartAxis;
        [SerializeField, HideInInspector]
        private float m_LineSpace;
        [SerializeField, HideInInspector]
        private float m_ColumnSpace;
        private List<RectTransform> m_RectChildren;
        private Vector2 m_TotalMinSize;
        private Vector2 m_TotalPreferredSize;
        private Vector2 m_TotalFlexibleSize;
        private RectTransform m_RectTransform;

        protected FlexibleLayoutGroup()
        {
            this.m_RectChildren = new List<RectTransform>();
            this.m_TotalMinSize = Vector2.get_zero();
            this.m_TotalPreferredSize = Vector2.get_zero();
            this.m_TotalFlexibleSize = Vector2.get_zero();
            base..ctor();
            if (this.m_Padding != null)
            {
                goto Label_0048;
            }
            this.m_Padding = new RectOffset();
        Label_0048:
            return;
        }

        public virtual void CalculateLayoutInputHorizontal()
        {
            int num;
            RectTransform transform;
            ILayoutIgnorer ignorer;
            this.rectChildren.Clear();
            num = 0;
            goto Label_006B;
        Label_0012:
            transform = this.rectTransform.GetChild(num) as RectTransform;
            if (transform.get_gameObject().get_activeInHierarchy() != null)
            {
                goto Label_0039;
            }
            goto Label_0067;
        Label_0039:
            ignorer = transform.get_gameObject().GetComponent<ILayoutIgnorer>();
            if (ignorer == null)
            {
                goto Label_005B;
            }
            if (ignorer.get_ignoreLayout() == null)
            {
                goto Label_005B;
            }
            goto Label_0067;
        Label_005B:
            this.rectChildren.Add(transform);
        Label_0067:
            num += 1;
        Label_006B:
            if (num < this.rectTransform.get_childCount())
            {
                goto Label_0012;
            }
            return;
        }

        public virtual void CalculateLayoutInputVertical()
        {
        }

        protected unsafe float GetTotalFlexibleSize(int axis)
        {
            return &this.m_TotalFlexibleSize.get_Item(axis);
        }

        protected unsafe float GetTotalMinSize(int axis)
        {
            return &this.m_TotalMinSize.get_Item(axis);
        }

        protected unsafe float GetTotalPreferredSize(int axis)
        {
            return &this.m_TotalPreferredSize.get_Item(axis);
        }

        protected override void OnDidApplyAnimationProperties()
        {
            this.SetDirty();
            return;
        }

        protected override void OnDisable()
        {
            LayoutRebuilder.MarkLayoutForRebuild(this.rectTransform);
            base.OnDisable();
            return;
        }

        protected override void OnEnable()
        {
            base.OnEnable();
            this.SetDirty();
            return;
        }

        protected override void OnRectTransformDimensionsChange()
        {
            base.OnRectTransformDimensionsChange();
            if (this.isRootLayoutGroup == null)
            {
                goto Label_0017;
            }
            this.SetDirty();
        Label_0017:
            return;
        }

        protected virtual void OnTransformChildrenChanged()
        {
            this.SetDirty();
            return;
        }

        private void SetChildAlongAxis(RectTransform rect, int axis, float pos, float size)
        {
            if ((rect == null) == null)
            {
                goto Label_000D;
            }
            return;
        Label_000D:
            rect.SetInsetAndSizeFromParentEdge((axis != null) ? 2 : 0, pos, size);
            return;
        }

        protected void SetDirty()
        {
            if (this.IsActive() != null)
            {
                goto Label_000C;
            }
            return;
        Label_000C:
            LayoutRebuilder.MarkLayoutForRebuild(this.rectTransform);
            return;
        }

        public virtual void SetLayoutHorizontal()
        {
            this.UpdateLayout();
            return;
        }

        public virtual void SetLayoutVertical()
        {
            this.UpdateLayout();
            return;
        }

        private unsafe void UpdateLayout()
        {
            Bounds bounds;
            Vector2 vector;
            Vector2 vector2;
            Vector2 vector3;
            int num;
            Vector2 vector4;
            Bounds bounds2;
            Vector3 vector5;
            Vector3 vector6;
            ref Vector2 vectorRef;
            int num2;
            float num3;
            ref Vector2 vectorRef2;
            Vector3 vector7;
            Vector3 vector8;
            ref Vector2 vectorRef3;
            ref Vector2 vectorRef4;
            bounds = RectTransformUtility.CalculateRelativeRectTransformBounds(this.rectTransform);
            &bounds.set_min(&bounds.get_min() + new Vector3((float) this.m_Padding.get_left(), (float) this.m_Padding.get_bottom()));
            &bounds.set_max(&bounds.get_max() - new Vector3((float) this.m_Padding.get_right(), (float) this.m_Padding.get_top()));
            vector = this.rectTransform.get_sizeDelta();
            &vector2..ctor((float) this.m_Padding.get_left(), (float) this.m_Padding.get_top());
            vector3 = vector2;
            num = 0;
            goto Label_02E5;
        Label_009F:
            vector4 = this.rectChildren[num].get_sizeDelta();
            this.SetChildAlongAxis(this.rectChildren[num], 0, &vector3.get_Item(0), &vector4.get_Item(0));
            this.SetChildAlongAxis(this.rectChildren[num], 1, &vector3.get_Item(1), &vector4.get_Item(1));
            bounds2 = RectTransformUtility.CalculateRelativeRectTransformBounds(this.rectTransform, this.rectChildren[num]);
            if (this.m_StartAxis != null)
            {
                goto Label_0202;
            }
            if (&&bounds.get_max().x > &&bounds2.get_max().x)
            {
                goto Label_01CC;
            }
            &vector3.set_Item(0, &vector2.get_Item(0));
            (vectorRef = &vector3).set_Item(num2 = 1, vectorRef.get_Item(num2) + (this.m_LineSpace + &this.m_Spacing.get_Item(1)));
            this.SetChildAlongAxis(this.rectChildren[num], 0, &vector3.get_Item(0), &vector4.get_Item(0));
            this.SetChildAlongAxis(this.rectChildren[num], 1, &vector3.get_Item(1), &vector4.get_Item(1));
        Label_01CC:
            (vectorRef2 = &vector3).set_Item(num2 = 0, vectorRef2.get_Item(num2) + (&vector4.get_Item(0) + &this.m_Spacing.get_Item(0)));
            goto Label_02DF;
        Label_0202:
            if (&&bounds.get_min().y < &&bounds2.get_min().y)
            {
                goto Label_02AE;
            }
            (vectorRef3 = &vector3).set_Item(num2 = 0, vectorRef3.get_Item(num2) + (this.m_ColumnSpace + &this.m_Spacing.get_Item(0)));
            &vector3.set_Item(1, &vector2.get_Item(1));
            this.SetChildAlongAxis(this.rectChildren[num], 0, &vector3.get_Item(0), &vector4.get_Item(0));
            this.SetChildAlongAxis(this.rectChildren[num], 1, &vector3.get_Item(1), &vector4.get_Item(1));
        Label_02AE:
            (vectorRef4 = &vector3).set_Item(num2 = 1, vectorRef4.get_Item(num2) + (&vector4.get_Item(1) + &this.m_Spacing.get_Item(1)));
        Label_02DF:
            num += 1;
        Label_02E5:
            if (num < this.rectChildren.Count)
            {
                goto Label_009F;
            }
            if (this.m_StartAxis != null)
            {
                goto Label_03A1;
            }
            &this.m_TotalMinSize.set_Item(0, &vector.get_Item(0));
            &this.m_TotalMinSize.set_Item(1, (&vector3.get_Item(1) + ((float) this.m_Padding.get_bottom())) + this.m_LineSpace);
            &this.m_TotalPreferredSize.set_Item(0, &vector.get_Item(0));
            &this.m_TotalPreferredSize.set_Item(1, (&vector3.get_Item(1) + ((float) this.m_Padding.get_bottom())) + this.m_LineSpace);
            &this.m_TotalFlexibleSize.set_Item(0, 0f);
            &this.m_TotalFlexibleSize.set_Item(1, 0f);
            goto Label_043B;
        Label_03A1:
            &this.m_TotalMinSize.set_Item(0, (&vector3.get_Item(0) + ((float) this.m_Padding.get_right())) + this.m_ColumnSpace);
            &this.m_TotalMinSize.set_Item(1, &vector.get_Item(1));
            &this.m_TotalPreferredSize.set_Item(0, (&vector3.get_Item(0) + ((float) this.m_Padding.get_right())) + this.m_ColumnSpace);
            &this.m_TotalPreferredSize.set_Item(1, &vector.get_Item(1));
            &this.m_TotalFlexibleSize.set_Item(0, 0f);
            &this.m_TotalFlexibleSize.set_Item(1, 0f);
        Label_043B:
            return;
        }

        private List<RectTransform> rectChildren
        {
            get
            {
                if (this.m_RectChildren != null)
                {
                    goto Label_0016;
                }
                this.m_RectChildren = new List<RectTransform>();
            Label_0016:
                return this.m_RectChildren;
            }
        }

        private RectTransform rectTransform
        {
            get
            {
                if ((this.m_RectTransform == null) == null)
                {
                    goto Label_001D;
                }
                this.m_RectTransform = base.GetComponent<RectTransform>();
            Label_001D:
                return this.m_RectTransform;
            }
        }

        private bool isRootLayoutGroup
        {
            get
            {
                Transform transform;
                if ((base.get_transform().get_parent() == null) == null)
                {
                    goto Label_001A;
                }
                return 1;
            Label_001A:
                return (base.get_transform().get_parent().GetComponent(typeof(ILayoutGroup)) == null);
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

        public enum Axis
        {
            Horizontal,
            Vertical
        }

        public enum Constraint
        {
            Flexible,
            FixedColumnCount,
            FixedRowCount
        }

        public enum Corner
        {
            UpperLeft,
            UpperRight,
            LowerLeft,
            LowerRight
        }
    }
}

