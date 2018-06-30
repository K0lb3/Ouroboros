namespace SRPG
{
    using System;
    using UnityEngine;
    using UnityEngine.EventSystems;

    [ExecuteInEditMode]
    public class ClampSize : UIBehaviour
    {
        private RectTransform mTransform;
        public RectTransform Target;
        public bool ClampX;
        public float XSize;
        public float XMargin;
        public float XPadding;
        public bool ClampY;
        public float YSize;
        public float YMargin;
        public float YPadding;

        public ClampSize()
        {
            this.ClampX = 1;
            this.XSize = 64f;
            this.ClampY = 1;
            this.YSize = 64f;
            base..ctor();
            return;
        }

        protected override void Awake()
        {
            base.Awake();
            return;
        }

        protected override void OnRectTransformDimensionsChange()
        {
            base.OnRectTransformDimensionsChange();
            this.Resize();
            return;
        }

        public unsafe void Resize()
        {
            Rect rect;
            Vector2 vector;
            float num;
            float num2;
            if ((this.mTransform == null) == null)
            {
                goto Label_0022;
            }
            this.mTransform = (RectTransform) base.get_transform();
        Label_0022:
            if ((this.Target == null) == null)
            {
                goto Label_0034;
            }
            return;
        Label_0034:
            if (this.Target.IsChildOf(this.mTransform) != null)
            {
                goto Label_006B;
            }
            Debug.LogError(this.Target.get_name() + " is not child of " + base.get_name());
            return;
        Label_006B:
            vector = &this.mTransform.get_rect().get_size();
            if (this.ClampX == null)
            {
                goto Label_00C2;
            }
            num = (Mathf.Floor(((&vector.x - this.XMargin) - this.XPadding) / this.XSize) * this.XSize) + this.XMargin;
            &vector.x = num;
        Label_00C2:
            if (this.ClampY == null)
            {
                goto Label_0105;
            }
            num2 = (Mathf.Floor(((&vector.y - this.YMargin) - this.YPadding) / this.YSize) * this.YSize) + this.YMargin;
            &vector.y = num2;
        Label_0105:
            this.Target.set_sizeDelta(vector);
            return;
        }
    }
}

