namespace SRPG
{
    using System;
    using UnityEngine;
    using UnityEngine.UI;

    public class SyncScroll : MonoBehaviour
    {
        [SerializeField]
        private ScrollRect m_ScrollRect;
        [SerializeField]
        private ScrollMode m_ScrollMode;
        [SerializeField]
        private RectTransform parent;
        private RectTransform rectTransform;

        public SyncScroll()
        {
            base..ctor();
            return;
        }

        private void Awake()
        {
            this.rectTransform = base.GetComponent<RectTransform>();
            this.parent = base.GetComponentInParent<RectTransform>();
            base.set_enabled(((this.m_ScrollRect != null) == null) ? 0 : (this.rectTransform != null));
            return;
        }

        private unsafe void LateUpdate()
        {
            Vector2 vector;
            Vector2 vector2;
            Vector2 vector3;
            Rect rect;
            Vector2 vector4;
            Vector2 vector5;
            Vector2 vector6;
            Rect rect2;
            Vector2 vector7;
            Vector2 vector8;
            Vector2 vector9;
            Rect rect3;
            Vector2 vector10;
            Vector2 vector11;
            Rect rect4;
            Vector2 vector12;
            Vector2 vector13;
            Rect rect5;
            Vector2 vector14;
            Vector2 vector15;
            if (this.m_ScrollRect.get_horizontal() == null)
            {
                goto Label_00DF;
            }
            vector = this.rectTransform.get_anchoredPosition();
            if (this.m_ScrollMode != null)
            {
                goto Label_007E;
            }
            &vector.x = (&this.rectTransform.get_sizeDelta().x + (&&this.parent.get_rect().get_size().x * 0.5f)) * &this.m_ScrollRect.get_normalizedPosition().x;
            goto Label_00D3;
        Label_007E:
            &vector.x = -(&this.rectTransform.get_sizeDelta().x - (&&this.parent.get_rect().get_size().x * 0.5f)) * &this.m_ScrollRect.get_normalizedPosition().x;
        Label_00D3:
            this.rectTransform.set_anchoredPosition(vector);
        Label_00DF:
            if (this.m_ScrollRect.get_vertical() == null)
            {
                goto Label_01E5;
            }
            vector2 = this.rectTransform.get_anchoredPosition();
            if (this.m_ScrollMode != null)
            {
                goto Label_0184;
            }
            &vector2.y = (-(&this.rectTransform.get_sizeDelta().y - (&&this.parent.get_rect().get_size().y * 0.5f)) * &this.m_ScrollRect.get_normalizedPosition().y) + (&&this.parent.get_rect().get_size().y * 0.5f);
            goto Label_01D9;
        Label_0184:
            &vector2.y = -(&this.rectTransform.get_sizeDelta().y - (&&this.parent.get_rect().get_size().y * 0.5f)) * &this.m_ScrollRect.get_normalizedPosition().y;
        Label_01D9:
            this.rectTransform.set_anchoredPosition(vector2);
        Label_01E5:
            return;
        }

        public bool isNormal
        {
            get
            {
                return (this.m_ScrollMode == 0);
            }
            set
            {
                this.m_ScrollMode = (value == null) ? 1 : 0;
                return;
            }
        }

        public bool isReverse
        {
            get
            {
                return (this.m_ScrollMode == 1);
            }
            set
            {
                this.m_ScrollMode = (value == null) ? 0 : 1;
                return;
            }
        }

        public ScrollMode scrollMode
        {
            get
            {
                return this.m_ScrollMode;
            }
            set
            {
                this.m_ScrollMode = value;
                return;
            }
        }

        public enum ScrollMode
        {
            Normal,
            Reverse
        }
    }
}

