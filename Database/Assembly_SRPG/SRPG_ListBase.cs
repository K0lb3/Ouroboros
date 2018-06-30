namespace SRPG
{
    using System;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.UI;

    public class SRPG_ListBase : MonoBehaviour
    {
        private Transform mItemBodyPool;
        private List<ListItemEvents> mItems;
        private ScrollRect mScrollRect;
        private RectTransform mTransform;
        private RectTransform mScrollRectTransform;

        public SRPG_ListBase()
        {
            this.mItems = new List<ListItemEvents>(0x20);
            base..ctor();
            return;
        }

        public void AddItem(ListItemEvents item)
        {
            this.mItems.Add(item);
            this.InitPool();
            item.DetachBody(this.mItemBodyPool);
            return;
        }

        public void ClearItems()
        {
            int num;
            num = 0;
            goto Label_0054;
        Label_0007:
            if ((this.mItems[num].Body != null) == null)
            {
                goto Label_0050;
            }
            Object.Destroy(this.mItems[num].Body.get_gameObject());
            this.mItems[num].Body = null;
        Label_0050:
            num += 1;
        Label_0054:
            if (num < this.mItems.Count)
            {
                goto Label_0007;
            }
            GameUtility.DestroyGameObjects<ListItemEvents>(this.mItems);
            this.mItems.Clear();
            return;
        }

        protected virtual RectTransform GetRectTransform()
        {
            return (base.get_transform() as RectTransform);
        }

        protected virtual ScrollRect GetScrollRect()
        {
            return base.GetComponentInParent<ScrollRect>();
        }

        private void InitPool()
        {
            if ((this.mItemBodyPool == null) == null)
            {
                goto Label_001C;
            }
            this.mItemBodyPool = UIUtility.Pool;
        Label_001C:
            return;
        }

        protected virtual unsafe void LateUpdate()
        {
            Rect rect;
            Vector2 vector;
            Vector2 vector2;
            Vector2 vector3;
            Vector2 vector4;
            float num;
            int num2;
            RectTransform transform;
            Rect rect2;
            Vector2 vector5;
            Vector2 vector6;
            Vector2 vector7;
            Vector2 vector8;
            Vector2 vector9;
            Vector2 vector10;
            Rect rect3;
            Rect rect4;
            Vector2 vector11;
            Vector2 vector12;
            if ((this.mScrollRect != null) == null)
            {
                goto Label_0336;
            }
            rect = this.mScrollRectTransform.get_rect();
            vector = new Vector2();
            vector2 = new Vector2();
            &vector.x = Mathf.Lerp(0f, &rect.get_width(), &this.mTransform.get_anchorMin().x);
            &vector.y = Mathf.Lerp(0f, &rect.get_height(), &this.mTransform.get_anchorMin().y);
            &vector2.x = Mathf.Lerp(0f, &rect.get_width(), &this.mTransform.get_anchorMax().x);
            &vector2.y = Mathf.Lerp(0f, &rect.get_height(), &this.mTransform.get_anchorMax().y);
            vector3 = new Vector2();
            &vector3.x = Mathf.Lerp(&vector.x, &vector2.x, &this.mTransform.get_pivot().x);
            &vector3.y = Mathf.Lerp(&vector.y, &vector2.y, &this.mTransform.get_pivot().y);
            vector4 = (this.mTransform.get_anchoredPosition() + &this.mTransform.get_rect().get_position()) + vector3;
            num = &this.mTransform.get_rect().get_height();
            &rect.set_position(-vector4);
            num2 = this.mItems.Count - 1;
            goto Label_032E;
        Label_01A5:
            if ((this.mItems[num2] == null) == null)
            {
                goto Label_01C2;
            }
            goto Label_0328;
        Label_01C2:
            if (this.mItems[num2].get_gameObject().get_activeInHierarchy() != null)
            {
                goto Label_01E3;
            }
            goto Label_0328;
        Label_01E3:
            transform = this.mItems[num2].GetRectTransform();
            rect2 = transform.get_rect();
            &rect2.set_x(&rect2.get_x() + (&transform.get_anchoredPosition().x - &this.mItems[num2].DisplayRectMergin.x));
            &rect2.set_y(&rect2.get_y() + ((num + &transform.get_anchoredPosition().y) - &this.mItems[num2].DisplayRectMergin.y));
            if (&this.mItems[num2].ParentScale.y >= 1f)
            {
                goto Label_02EC;
            }
            float introduced19 = &rect2.get_y();
            &rect2.set_y(introduced19 + ((&rect2.get_height() * (1f - &this.mItems[num2].ParentScale.y)) * ((float) num2)));
            &rect2.set_height(&rect2.get_height() * &this.mItems[num2].ParentScale.y);
        Label_02EC:
            if (&rect2.Overlaps(rect) == null)
            {
                goto Label_0310;
            }
            this.mItems[num2].AttachBody();
            goto Label_0328;
        Label_0310:
            this.mItems[num2].DetachBody(this.mItemBodyPool);
        Label_0328:
            num2 -= 1;
        Label_032E:
            if (num2 >= 0)
            {
                goto Label_01A5;
            }
        Label_0336:
            return;
        }

        protected virtual void OnDestroy()
        {
            int num;
            num = 0;
            goto Label_006B;
        Label_0007:
            if ((this.mItems[num] != null) == null)
            {
                goto Label_0067;
            }
            if ((this.mItems[num].Body != null) == null)
            {
                goto Label_0067;
            }
            Object.Destroy(this.mItems[num].Body.get_gameObject());
            this.mItems[num].Body = null;
        Label_0067:
            num += 1;
        Label_006B:
            if (num < this.mItems.Count)
            {
                goto Label_0007;
            }
            return;
        }

        protected virtual void Start()
        {
            this.mScrollRect = this.GetScrollRect();
            this.mTransform = this.GetRectTransform();
            if ((this.mScrollRect != null) == null)
            {
                goto Label_003F;
            }
            this.mScrollRectTransform = this.mScrollRect.get_transform() as RectTransform;
        Label_003F:
            this.InitPool();
            return;
        }

        protected bool IsEmpty
        {
            get
            {
                return (this.mItems.Count == 0);
            }
        }

        protected ListItemEvents[] Items
        {
            get
            {
                return this.mItems.ToArray();
            }
        }
    }
}

