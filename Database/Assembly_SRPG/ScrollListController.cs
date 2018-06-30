namespace SRPG
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using UnityEngine;
    using UnityEngine.Events;
    using UnityEngine.UI;

    public class ScrollListController : MonoBehaviour
    {
        [SerializeField]
        private RectTransform m_ItemBase;
        [Range(0f, 30f), SerializeField]
        protected int m_ItemCnt;
        public OnItemPositionChange OnItemUpdate;
        public OnAfterStartUpEvent OnAfterStartup;
        public OnUpdateEvent OnUpdateItemEvent;
        public List<RectTransform> m_ItemList;
        private List<Vector2> m_ItemPos;
        private float m_PrevPosition;
        private int m_CurrentItemID;
        public Direction m_Direction;
        public Mode m_ScrollMode;
        public float Space;
        private RectTransform m_RectTransform;
        private float m_ItemScale;
        [CompilerGenerated]
        private static Func<MonoBehaviour, bool> <>f__am$cacheE;
        [CompilerGenerated]
        private static Func<MonoBehaviour, ScrollListSetUp> <>f__am$cacheF;
        [CompilerGenerated]
        private static Func<MonoBehaviour, bool> <>f__am$cache10;
        [CompilerGenerated]
        private static Func<MonoBehaviour, ScrollListSetUp> <>f__am$cache11;
        [CompilerGenerated]
        private static Func<MonoBehaviour, bool> <>f__am$cache12;
        [CompilerGenerated]
        private static Func<MonoBehaviour, ScrollListSetUp> <>f__am$cache13;

        public ScrollListController()
        {
            this.m_ItemCnt = 8;
            this.OnItemUpdate = new OnItemPositionChange();
            this.OnAfterStartup = new OnAfterStartUpEvent();
            this.OnUpdateItemEvent = new OnUpdateEvent();
            this.m_ItemPos = new List<Vector2>();
            this.Space = 1.2f;
            this.m_ItemScale = -1f;
            base..ctor();
            return;
        }

        [CompilerGenerated]
        private static bool <Refresh>m__400(MonoBehaviour item)
        {
            return ((item as ScrollListSetUp) > null);
        }

        [CompilerGenerated]
        private static ScrollListSetUp <Refresh>m__401(MonoBehaviour item)
        {
            return (item as ScrollListSetUp);
        }

        [CompilerGenerated]
        private static bool <Start>m__3FC(MonoBehaviour item)
        {
            return ((item as ScrollListSetUp) > null);
        }

        [CompilerGenerated]
        private static ScrollListSetUp <Start>m__3FD(MonoBehaviour item)
        {
            return (item as ScrollListSetUp);
        }

        [CompilerGenerated]
        private static bool <UpdateList>m__3FE(MonoBehaviour item)
        {
            return ((item as ScrollListSetUp) > null);
        }

        [CompilerGenerated]
        private static ScrollListSetUp <UpdateList>m__3FF(MonoBehaviour item)
        {
            return (item as ScrollListSetUp);
        }

        public void ClearItem()
        {
            int num;
            if (this.m_ItemList == null)
            {
                goto Label_005A;
            }
            num = 0;
            goto Label_003E;
        Label_0012:
            if ((this.m_ItemList[num] != null) == null)
            {
                goto Label_003A;
            }
            Object.Destroy(this.m_ItemList[num]);
        Label_003A:
            num += 1;
        Label_003E:
            if (num < this.m_ItemList.Count)
            {
                goto Label_0012;
            }
            this.m_ItemList.Clear();
        Label_005A:
            return;
        }

        public unsafe bool MovePos(float goal, float move)
        {
            bool flag;
            Vector2 vector;
            bool flag2;
            float num;
            bool flag3;
            flag = 0;
            vector = this.GetRectTransForm.get_anchoredPosition();
            if (this.m_ScrollMode != null)
            {
                goto Label_002C;
            }
            &vector.y *= -1f;
        Label_002C:
            flag2 = &vector.y < goal;
            num = (flag2 == null) ? -move : move;
            &vector.y += num;
            if (((flag2 == null) ? (&vector.y < goal) : (&vector.y > goal)) == null)
            {
                goto Label_0087;
            }
            &vector.y = goal;
            flag = 1;
        Label_0087:
            this.GetRectTransForm.set_anchoredPosition(vector);
            return flag;
        }

        public unsafe void Refresh()
        {
            List<ScrollListSetUp> list;
            ScrollListSetUp up;
            List<ScrollListSetUp>.Enumerator enumerator;
            int num;
            RectTransform transform;
            if (<>f__am$cache12 != null)
            {
                goto Label_001E;
            }
            <>f__am$cache12 = new Func<MonoBehaviour, bool>(ScrollListController.<Refresh>m__400);
        Label_001E:
            if (<>f__am$cache13 != null)
            {
                goto Label_0040;
            }
            <>f__am$cache13 = new Func<MonoBehaviour, ScrollListSetUp>(ScrollListController.<Refresh>m__401);
        Label_0040:
            enumerator = Enumerable.ToList<ScrollListSetUp>(Enumerable.Select<MonoBehaviour, ScrollListSetUp>(Enumerable.Where<MonoBehaviour>(base.GetComponents<MonoBehaviour>(), <>f__am$cache12), <>f__am$cache13)).GetEnumerator();
        Label_0057:
            try
            {
                goto Label_012A;
            Label_005C:
                up = &enumerator.Current;
                num = 0;
                goto Label_0119;
            Label_006B:
                transform = this.m_ItemList[num];
                if (this.m_Direction != 1)
                {
                    goto Label_00BF;
                }
                transform.set_anchoredPosition(new Vector2((((this.ItemScale * this.Space) * ((float) num)) + (this.ItemScale * 0.5f)) * this.ScrollDir, 0f));
                goto Label_00F4;
            Label_00BF:
                transform.set_anchoredPosition(new Vector2(0f, (((this.ItemScale * this.Space) * ((float) num)) + (this.ItemScale * 0.5f)) * this.ScrollDir));
            Label_00F4:
                this.m_ItemPos[num] = transform.get_anchoredPosition();
                up.OnUpdateItems(num, transform.get_gameObject());
                num += 1;
            Label_0119:
                if (num < this.m_ItemList.Count)
                {
                    goto Label_006B;
                }
            Label_012A:
                if (&enumerator.MoveNext() != null)
                {
                    goto Label_005C;
                }
                goto Label_0147;
            }
            finally
            {
            Label_013B:
                ((List<ScrollListSetUp>.Enumerator) enumerator).Dispose();
            }
        Label_0147:
            this.m_PrevPosition = 0f;
            this.m_CurrentItemID = 0;
            return;
        }

        protected virtual unsafe void Start()
        {
            List<ScrollListSetUp> list;
            ScrollRect rect;
            int num;
            RectTransform transform;
            ScrollListSetUp up;
            List<ScrollListSetUp>.Enumerator enumerator;
            int num2;
            if (<>f__am$cacheE != null)
            {
                goto Label_001E;
            }
            <>f__am$cacheE = new Func<MonoBehaviour, bool>(ScrollListController.<Start>m__3FC);
        Label_001E:
            if (<>f__am$cacheF != null)
            {
                goto Label_0040;
            }
            <>f__am$cacheF = new Func<MonoBehaviour, ScrollListSetUp>(ScrollListController.<Start>m__3FD);
        Label_0040:
            list = Enumerable.ToList<ScrollListSetUp>(Enumerable.Select<MonoBehaviour, ScrollListSetUp>(Enumerable.Where<MonoBehaviour>(base.GetComponents<MonoBehaviour>(), <>f__am$cacheE), <>f__am$cacheF));
            base.GetComponentInParent<ScrollRect>().set_content(this.GetRectTransForm);
            this.m_ItemBase.get_gameObject().SetActive(0);
            num = 0;
            goto Label_013A;
        Label_007B:
            transform = Object.Instantiate<RectTransform>(this.m_ItemBase);
            transform.SetParent(base.get_transform(), 0);
            if (this.m_Direction != 1)
            {
                goto Label_00D9;
            }
            transform.set_anchoredPosition(new Vector2((((this.ItemScale * this.Space) * ((float) num)) + (this.ItemScale * 0.5f)) * this.ScrollDir, 0f));
            goto Label_010D;
        Label_00D9:
            transform.set_anchoredPosition(new Vector2(0f, (((this.ItemScale * this.Space) * ((float) num)) + (this.ItemScale * 0.5f)) * this.ScrollDir));
        Label_010D:
            this.m_ItemList.Add(transform);
            this.m_ItemPos.Add(transform.get_anchoredPosition());
            transform.get_gameObject().SetActive(1);
            num += 1;
        Label_013A:
            if (num < this.m_ItemCnt)
            {
                goto Label_007B;
            }
            enumerator = list.GetEnumerator();
        Label_014E:
            try
            {
                goto Label_0199;
            Label_0153:
                up = &enumerator.Current;
                up.OnSetUpItems();
                num2 = 0;
                goto Label_018C;
            Label_016B:
                up.OnUpdateItems(num2, this.m_ItemList[num2].get_gameObject());
                num2 += 1;
            Label_018C:
                if (num2 < this.m_ItemCnt)
                {
                    goto Label_016B;
                }
            Label_0199:
                if (&enumerator.MoveNext() != null)
                {
                    goto Label_0153;
                }
                goto Label_01B7;
            }
            finally
            {
            Label_01AA:
                ((List<ScrollListSetUp>.Enumerator) enumerator).Dispose();
            }
        Label_01B7:
            if (this.OnAfterStartup == null)
            {
                goto Label_01CE;
            }
            this.OnAfterStartup.Invoke(1);
        Label_01CE:
            return;
        }

        private unsafe void Update()
        {
            RectTransform transform;
            Vector2 vector;
            float num;
            float num2;
            int num3;
            RectTransform transform2;
            Vector2 vector2;
            float num4;
            float num5;
            goto Label_0106;
        Label_0005:
            this.m_PrevPosition -= this.ItemScale * this.Space;
            transform = this.m_ItemList[0];
            this.m_ItemList.RemoveAt(0);
            this.m_ItemList.Add(transform);
            vector = Enumerable.Last<Vector2>(this.m_ItemPos);
            if (this.m_Direction != 1)
            {
                goto Label_008F;
            }
            num = &vector.x + ((this.ItemScale * this.Space) * this.ScrollDir);
            transform.set_anchoredPosition(new Vector2(num, 0f));
            goto Label_00BD;
        Label_008F:
            num2 = &vector.y + ((this.ItemScale * this.Space) * this.ScrollDir);
            transform.set_anchoredPosition(new Vector2(0f, num2));
        Label_00BD:
            this.m_ItemPos.RemoveAt(0);
            this.m_ItemPos.Add(transform.get_anchoredPosition());
            this.OnItemUpdate.Invoke(this.m_CurrentItemID + this.m_ItemCnt, transform.get_gameObject());
            this.m_CurrentItemID += 1;
        Label_0106:
            if ((this.AnchoredPosition - this.m_PrevPosition) < -((this.ItemScale * this.Space) + (this.ItemScale * 0.5f)))
            {
                goto Label_0005;
            }
            goto Label_024D;
        Label_0138:
            this.m_PrevPosition += this.ItemScale * this.Space;
            num3 = this.m_ItemCnt - 1;
            transform2 = this.m_ItemList[num3];
            this.m_ItemList.RemoveAt(num3);
            this.m_ItemList.Insert(0, transform2);
            this.m_CurrentItemID -= 1;
            vector2 = this.m_ItemPos[0];
            if (this.m_Direction != 1)
            {
                goto Label_01E4;
            }
            num4 = &vector2.x - ((this.ItemScale * this.Space) * this.ScrollDir);
            transform2.set_anchoredPosition(new Vector2(num4, 0f));
            goto Label_0215;
        Label_01E4:
            num5 = &vector2.y - ((this.ItemScale * this.Space) * this.ScrollDir);
            transform2.set_anchoredPosition(new Vector2(0f, num5));
        Label_0215:
            this.m_ItemPos.RemoveAt(num3);
            this.m_ItemPos.Insert(0, transform2.get_anchoredPosition());
            this.OnItemUpdate.Invoke(this.m_CurrentItemID, transform2.get_gameObject());
        Label_024D:
            if ((this.AnchoredPosition - this.m_PrevPosition) > (-this.ItemScale * 0.5f))
            {
                goto Label_0138;
            }
            if (this.OnUpdateItemEvent == null)
            {
                goto Label_0288;
            }
            this.OnUpdateItemEvent.Invoke(this.m_ItemList);
        Label_0288:
            return;
        }

        public unsafe void UpdateList()
        {
            List<ScrollListSetUp> list;
            ScrollRect rect;
            int num;
            RectTransform transform;
            ScrollListSetUp up;
            List<ScrollListSetUp>.Enumerator enumerator;
            int num2;
            RectTransform transform2;
            Vector2 vector;
            if (<>f__am$cache10 != null)
            {
                goto Label_001E;
            }
            <>f__am$cache10 = new Func<MonoBehaviour, bool>(ScrollListController.<UpdateList>m__3FE);
        Label_001E:
            if (<>f__am$cache11 != null)
            {
                goto Label_0040;
            }
            <>f__am$cache11 = new Func<MonoBehaviour, ScrollListSetUp>(ScrollListController.<UpdateList>m__3FF);
        Label_0040:
            list = Enumerable.ToList<ScrollListSetUp>(Enumerable.Select<MonoBehaviour, ScrollListSetUp>(Enumerable.Where<MonoBehaviour>(base.GetComponents<MonoBehaviour>(), <>f__am$cache10), <>f__am$cache11));
            base.GetComponentInParent<ScrollRect>().set_content(this.GetRectTransForm);
            this.m_ItemBase.get_gameObject().SetActive(0);
            num = 0;
            goto Label_0112;
        Label_007B:
            transform = this.m_ItemList[num];
            transform.SetParent(base.get_transform(), 0);
            if (this.m_Direction != 1)
            {
                goto Label_00D4;
            }
            transform.set_anchoredPosition(new Vector2(((-this.ItemScale * this.Space) * ((float) num)) - (this.ItemScale * 0.5f), 0f));
            goto Label_0102;
        Label_00D4:
            transform.set_anchoredPosition(new Vector2(0f, ((-this.ItemScale * this.Space) * ((float) num)) - (this.ItemScale * 0.5f)));
        Label_0102:
            transform.get_gameObject().SetActive(1);
            num += 1;
        Label_0112:
            if (num < this.m_ItemCnt)
            {
                goto Label_007B;
            }
            enumerator = list.GetEnumerator();
        Label_0126:
            try
            {
                goto Label_0171;
            Label_012B:
                up = &enumerator.Current;
                up.OnSetUpItems();
                num2 = 0;
                goto Label_0164;
            Label_0143:
                up.OnUpdateItems(num2, this.m_ItemList[num2].get_gameObject());
                num2 += 1;
            Label_0164:
                if (num2 < this.m_ItemCnt)
                {
                    goto Label_0143;
                }
            Label_0171:
                if (&enumerator.MoveNext() != null)
                {
                    goto Label_012B;
                }
                goto Label_018F;
            }
            finally
            {
            Label_0182:
                ((List<ScrollListSetUp>.Enumerator) enumerator).Dispose();
            }
        Label_018F:
            this.m_PrevPosition = 0f;
            this.m_CurrentItemID = 0;
            transform2 = base.get_transform().GetComponent<RectTransform>();
            vector = transform2.get_anchoredPosition();
            &vector.y = 0f;
            transform2.set_anchoredPosition(vector);
            return;
        }

        protected RectTransform GetRectTransForm
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

        public float AnchoredPosition
        {
            get
            {
                Vector2 vector;
                Vector2 vector2;
                Vector2 vector3;
                Vector2 vector4;
                if (this.m_ScrollMode != null)
                {
                    goto Label_0043;
                }
                return ((this.m_Direction != null) ? &this.GetRectTransForm.get_anchoredPosition().x : -&this.GetRectTransForm.get_anchoredPosition().y);
            Label_0043:
                return ((this.m_Direction != null) ? &this.GetRectTransForm.get_anchoredPosition().x : &this.GetRectTransForm.get_anchoredPosition().y);
            }
        }

        public float ItemScale
        {
            get
            {
                Vector2 vector;
                Vector2 vector2;
                if (((this.m_ItemBase != null) == null) || (this.m_ItemScale != -1f))
                {
                    goto Label_005D;
                }
                this.m_ItemScale = (this.m_Direction != null) ? &this.m_ItemBase.get_sizeDelta().x : &this.m_ItemBase.get_sizeDelta().y;
            Label_005D:
                return this.m_ItemScale;
            }
        }

        public float ScrollDir
        {
            get
            {
                return ((this.m_ScrollMode != null) ? 1f : -1f);
            }
        }

        public List<RectTransform> ItemList
        {
            get
            {
                return this.m_ItemList;
            }
        }

        public List<Vector2> ItemPosList
        {
            get
            {
                return this.m_ItemPos;
            }
        }

        public enum Direction
        {
            Vertical,
            Horizontal
        }

        public enum Mode
        {
            Normal,
            Reverse
        }

        [Serializable]
        public class OnAfterStartUpEvent : UnityEvent<bool>
        {
            public OnAfterStartUpEvent()
            {
                base..ctor();
                return;
            }
        }

        [Serializable]
        public class OnItemPositionChange : UnityEvent<int, GameObject>
        {
            public OnItemPositionChange()
            {
                base..ctor();
                return;
            }
        }

        [Serializable]
        public class OnUpdateEvent : UnityEvent<List<RectTransform>>
        {
            public OnUpdateEvent()
            {
                base..ctor();
                return;
            }
        }
    }
}

