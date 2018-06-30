namespace SRPG
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using UnityEngine;
    using UnityEngine.Events;
    using UnityEngine.UI;

    public class JobIconScrollListController : MonoBehaviour
    {
        private float ITEM_DISTANCE;
        private float SINGLE_ICON_ZERO_MERGIN;
        private float SINGLE_ICON_ONE_MERGIN;
        private float SINGLE_ICON_TWO_MERGIN;
        private float SINGLE_ICON_THREE_MERGIN;
        [SerializeField]
        private GameObject mTemplateItem;
        [Range(0f, 30f), SerializeField]
        protected int m_ItemCnt;
        public OnItemPositionChange OnItemUpdate;
        public OnAfterStartUpEvent OnAfterStartup;
        public OnUpdateEvent OnUpdateItemEvent;
        public OnItemPositionAreaOverEvent OnItemPositionAreaOver;
        public Direction m_Direction;
        public Mode m_ScrollMode;
        private RectTransform m_RectTransform;
        private List<ItemData> mItems;
        private Rect mViewArea;
        private float mPreAnchoredPositionX;
        private bool IsInitialized;
        [SerializeField]
        private RectTransform mViewPort;
        [CompilerGenerated]
        private static Func<MonoBehaviour, bool> <>f__am$cache13;
        [CompilerGenerated]
        private static Func<MonoBehaviour, ScrollListSetUp> <>f__am$cache14;

        public JobIconScrollListController()
        {
            this.ITEM_DISTANCE = 10f;
            this.SINGLE_ICON_ZERO_MERGIN = 20f;
            this.SINGLE_ICON_ONE_MERGIN = 22f;
            this.SINGLE_ICON_TWO_MERGIN = 45f;
            this.SINGLE_ICON_THREE_MERGIN = 80f;
            this.m_ItemCnt = 8;
            this.OnItemUpdate = new OnItemPositionChange();
            this.OnAfterStartup = new OnAfterStartUpEvent();
            this.OnUpdateItemEvent = new OnUpdateEvent();
            this.OnItemPositionAreaOver = new OnItemPositionAreaOverEvent();
            base..ctor();
            return;
        }

        [CompilerGenerated]
        private static bool <CreateInstance>m__34F(MonoBehaviour item)
        {
            return ((item as ScrollListSetUp) > null);
        }

        [CompilerGenerated]
        private static ScrollListSetUp <CreateInstance>m__350(MonoBehaviour item)
        {
            return (item as ScrollListSetUp);
        }

        private unsafe bool CheckLeftAreaOut(ItemData item)
        {
            RectTransform transform;
            Vector2 vector;
            transform = base.get_transform() as RectTransform;
            if (&this.mViewArea.get_x() <= ((&transform.get_anchoredPosition().x + &item.position.x) + item.job_icon.HalfWidth))
            {
                goto Label_0044;
            }
            return 1;
        Label_0044:
            return 0;
        }

        private unsafe bool CheckRightAreaOut(ItemData item)
        {
            RectTransform transform;
            Vector2 vector;
            transform = base.get_transform() as RectTransform;
            if (&this.mViewArea.get_width() >= ((&transform.get_anchoredPosition().x + &item.position.x) + item.job_icon.HalfWidth))
            {
                goto Label_0044;
            }
            return 1;
        Label_0044:
            return 0;
        }

        public unsafe void CreateInstance()
        {
            int num;
            GameObject obj2;
            List<ScrollListSetUp> list;
            ScrollListSetUp up;
            List<ScrollListSetUp>.Enumerator enumerator;
            int num2;
            this.mItems = new List<ItemData>();
            this.mTemplateItem.SetActive(0);
            num = 0;
            goto Label_0058;
        Label_001E:
            obj2 = Object.Instantiate<GameObject>(this.mTemplateItem);
            obj2.get_transform().SetParent(base.get_transform(), 0);
            obj2.SetActive(1);
            this.mItems.Add(new ItemData(obj2));
            num += 1;
        Label_0058:
            if (num < this.m_ItemCnt)
            {
                goto Label_001E;
            }
            if (<>f__am$cache13 != null)
            {
                goto Label_0082;
            }
            <>f__am$cache13 = new Func<MonoBehaviour, bool>(JobIconScrollListController.<CreateInstance>m__34F);
        Label_0082:
            if (<>f__am$cache14 != null)
            {
                goto Label_00A4;
            }
            <>f__am$cache14 = new Func<MonoBehaviour, ScrollListSetUp>(JobIconScrollListController.<CreateInstance>m__350);
        Label_00A4:
            enumerator = Enumerable.ToList<ScrollListSetUp>(Enumerable.Select<MonoBehaviour, ScrollListSetUp>(Enumerable.Where<MonoBehaviour>(base.GetComponents<MonoBehaviour>(), <>f__am$cache13), <>f__am$cache14)).GetEnumerator();
        Label_00BC:
            try
            {
                goto Label_0104;
            Label_00C1:
                up = &enumerator.Current;
                up.OnSetUpItems();
                num2 = 0;
                goto Label_00F7;
            Label_00D7:
                up.OnUpdateItems(num2, this.mItems[num2].gameObject);
                num2 += 1;
            Label_00F7:
                if (num2 < this.m_ItemCnt)
                {
                    goto Label_00D7;
                }
            Label_0104:
                if (&enumerator.MoveNext() != null)
                {
                    goto Label_00C1;
                }
                goto Label_0122;
            }
            finally
            {
            Label_0115:
                ((List<ScrollListSetUp>.Enumerator) enumerator).Dispose();
            }
        Label_0122:
            return;
        }

        public void Init()
        {
            if (this.OnAfterStartup == null)
            {
                goto Label_0017;
            }
            this.OnAfterStartup.Invoke(1);
        Label_0017:
            return;
        }

        public unsafe void Repotision()
        {
            float num;
            float num2;
            float num3;
            int num4;
            UnitInventoryJobIcon icon;
            int num5;
            List<string> list;
            int num6;
            float num7;
            Vector2 vector;
            Vector2 vector2;
            this.GetRectTransForm.set_anchoredPosition(Vector2.get_zero());
            this.mPreAnchoredPositionX = this.AnchoredPosition;
            num = 0f;
            num2 = 0f;
            num3 = 0f;
            num4 = 0;
            goto Label_00DC;
        Label_0035:
            icon = this.mItems[num4].job_icon;
            if (num4 > 0)
            {
                goto Label_0066;
            }
            num3 += icon.HalfWidth;
            num = icon.HalfWidth;
            goto Label_007F;
        Label_0066:
            num3 += icon.HalfWidth + this.ITEM_DISTANCE;
            num2 = icon.HalfWidth;
        Label_007F:
            this.mItems[num4].rectTransform.set_anchoredPosition(new Vector2(num3 * this.ScrollDir, 0f));
            this.mItems[num4].position = this.mItems[num4].rectTransform.get_anchoredPosition();
            num3 += icon.HalfWidth;
            num4 += 1;
        Label_00DC:
            if (num4 < this.mItems.Count)
            {
                goto Label_0035;
            }
            this.mViewArea = new Rect(&this.mItems[0].rectTransform.get_anchoredPosition().x - num, 0f, &this.mItems[this.mItems.Count - 1].rectTransform.get_anchoredPosition().x + num2, 0f);
            num5 = 0;
            list = new List<string>();
            num6 = 0;
            goto Label_01D5;
        Label_0162:
            if (list.Contains(this.mItems[num6].job_icon.BaseJobIconButton.get_name()) != null)
            {
                goto Label_01CF;
            }
            list.Add(this.mItems[num6].job_icon.BaseJobIconButton.get_name());
            if (this.mItems[num6].job_icon.IsSingleIcon == null)
            {
                goto Label_01CF;
            }
            num5 += 1;
        Label_01CF:
            num6 += 1;
        Label_01D5:
            if (num6 < this.mItems.Count)
            {
                goto Label_0162;
            }
            num7 = this.SINGLE_ICON_ZERO_MERGIN;
            if (num5 != 1)
            {
                goto Label_01FF;
            }
            num7 = this.SINGLE_ICON_ONE_MERGIN;
        Label_01FF:
            if (num5 != 2)
            {
                goto Label_020F;
            }
            num7 = this.SINGLE_ICON_TWO_MERGIN;
        Label_020F:
            if (num5 < 3)
            {
                goto Label_021F;
            }
            num7 = this.SINGLE_ICON_THREE_MERGIN;
        Label_021F:
            this.mViewPort.set_offsetMin(new Vector2(num7, 0f));
            this.mViewPort.set_offsetMax(new Vector2(-num7, 0f));
            this.IsInitialized = 1;
            return;
        }

        private void Start()
        {
            ScrollRect rect;
            base.GetComponentInParent<ScrollRect>().set_content(this.GetRectTransForm);
            return;
        }

        public void Step()
        {
            this.Update();
            return;
        }

        private void Update()
        {
            Mode mode;
            if (this.IsInitialized != null)
            {
                goto Label_000C;
            }
            return;
        Label_000C:
            mode = this.m_ScrollMode;
            if (mode == null)
            {
                goto Label_0025;
            }
            if (mode == 1)
            {
                goto Label_0030;
            }
            goto Label_003B;
        Label_0025:
            this.UpdateModeNormal();
            goto Label_003B;
        Label_0030:
            this.UpdateModeReverse();
        Label_003B:
            return;
        }

        private unsafe void UpdateItemsPositionNormal(bool is_move_right, bool is_move_left)
        {
            RectTransform transform;
            int num;
            float num2;
            float num3;
            float num4;
            int num5;
            float num6;
            float num7;
            float num8;
            Vector2 vector;
            Vector3 vector2;
            Rect rect;
            Vector2 vector3;
            Vector3 vector4;
            Rect rect2;
            Vector2 vector5;
            Vector3 vector6;
            Vector2 vector7;
            Vector2 vector8;
            Vector2 vector9;
            Vector3 vector10;
            Vector2 vector11;
            Vector3 vector12;
            Vector2 vector13;
            Vector3 vector14;
            Vector2 vector15;
            Vector2 vector16;
            transform = base.get_transform() as RectTransform;
            if (is_move_right == null)
            {
                goto Label_018C;
            }
            num = this.mItems.Count - 1;
            goto Label_0185;
        Label_0025:
            if (this.mItems[num].gameObject.get_activeSelf() != null)
            {
                goto Label_0045;
            }
            goto Label_0181;
        Label_0045:
            num2 = (&this.mItems[num].rectTransform.get_sizeDelta().x * &this.mItems[num].rectTransform.get_localScale().x) * 0.5f;
            if (&this.mViewPort.get_rect().get_width() >= ((&transform.get_anchoredPosition().x + &this.mItems[num].gameObject.get_transform().get_localPosition().x) + num2))
            {
                goto Label_0181;
            }
            num3 = &this.mViewPort.get_rect().get_width() - ((&transform.get_anchoredPosition().x + &this.mItems[num].gameObject.get_transform().get_localPosition().x) + num2);
            num4 = &transform.get_anchoredPosition().x + num3;
            transform.set_anchoredPosition(new Vector2(num4, &transform.get_anchoredPosition().y));
            if (this.OnItemPositionAreaOver == null)
            {
                goto Label_018C;
            }
            this.OnItemPositionAreaOver.Invoke(this.mItems[num].gameObject);
            goto Label_018C;
        Label_0181:
            num -= 1;
        Label_0185:
            if (num >= 0)
            {
                goto Label_0025;
            }
        Label_018C:
            if (is_move_left == null)
            {
                goto Label_02FB;
            }
            num5 = 0;
            goto Label_02E9;
        Label_019A:
            if (this.mItems[num5].gameObject.get_activeSelf() != null)
            {
                goto Label_01BB;
            }
            goto Label_02E3;
        Label_01BB:
            num6 = (&this.mItems[num5].rectTransform.get_sizeDelta().x * &this.mItems[num5].rectTransform.get_localScale().x) * 0.5f;
            if (0f <= ((&transform.get_anchoredPosition().x + &this.mItems[num5].gameObject.get_transform().get_localPosition().x) - num6))
            {
                goto Label_02E3;
            }
            num7 = 0f - ((&transform.get_anchoredPosition().x + &this.mItems[num5].gameObject.get_transform().get_localPosition().x) - num6);
            num8 = &transform.get_anchoredPosition().x + num7;
            transform.set_anchoredPosition(new Vector2(num8, &transform.get_anchoredPosition().y));
            if (this.OnItemPositionAreaOver == null)
            {
                goto Label_02FB;
            }
            this.OnItemPositionAreaOver.Invoke(this.mItems[num5].gameObject);
            goto Label_02FB;
        Label_02E3:
            num5 += 1;
        Label_02E9:
            if (num5 < this.mItems.Count)
            {
                goto Label_019A;
            }
        Label_02FB:
            return;
        }

        private unsafe void UpdateItemsPositionReverse(bool is_move_right, bool is_move_left)
        {
            int num;
            UnitInventoryJobIcon icon;
            UnitInventoryJobIcon icon2;
            UnitInventoryJobIcon icon3;
            int num2;
            float num3;
            ItemData data;
            int num4;
            float num5;
            ItemData data2;
            num = 0;
            goto Label_022C;
        Label_0007:
            icon = this.mItems[num].job_icon;
            icon2 = this.mItems[0].job_icon;
            icon3 = this.mItems[this.mItems.Count - 1].job_icon;
            if (is_move_right == null)
            {
                goto Label_012D;
            }
            if (this.CheckRightAreaOut(this.mItems[num]) == null)
            {
                goto Label_012D;
            }
            num2 = int.Parse(this.mItems[0].gameObject.get_name());
            this.OnItemUpdate.Invoke(num2 - 1, this.mItems[num].gameObject);
            num3 = ((&this.mItems[0].position.x - icon2.HalfWidth) - this.ITEM_DISTANCE) - icon.HalfWidth;
            this.mItems[num].position = new Vector2(num3, &this.mItems[num].position.y);
            data = this.mItems[num];
            this.mItems.RemoveAt(num);
            this.mItems.Insert(0, data);
            num = -1;
            goto Label_0228;
        Label_012D:
            if (is_move_left == null)
            {
                goto Label_0228;
            }
            if (this.CheckLeftAreaOut(this.mItems[num]) == null)
            {
                goto Label_0228;
            }
            num4 = int.Parse(this.mItems[this.mItems.Count - 1].gameObject.get_name());
            this.OnItemUpdate.Invoke(num4 + 1, this.mItems[num].gameObject);
            num5 = ((&this.mItems[this.mItems.Count - 1].position.x + icon3.HalfWidth) + this.ITEM_DISTANCE) + icon.HalfWidth;
            this.mItems[num].position = new Vector2(num5, &this.mItems[num].position.y);
            data2 = this.mItems[num];
            this.mItems.RemoveAt(num);
            this.mItems.Add(data2);
            num = -1;
        Label_0228:
            num += 1;
        Label_022C:
            if (num < this.mItems.Count)
            {
                goto Label_0007;
            }
            return;
        }

        private void UpdateModeNormal()
        {
            bool flag;
            bool flag2;
            if (this.mPreAnchoredPositionX == this.AnchoredPosition)
            {
                goto Label_004F;
            }
            flag = (this.AnchoredPosition - this.mPreAnchoredPositionX) > 0f;
            flag2 = (this.AnchoredPosition - this.mPreAnchoredPositionX) < 0f;
            this.UpdateItemsPositionNormal(flag, flag2);
            this.mPreAnchoredPositionX = this.AnchoredPosition;
        Label_004F:
            if (this.OnUpdateItemEvent == null)
            {
                goto Label_006B;
            }
            this.OnUpdateItemEvent.Invoke(this.mItems);
        Label_006B:
            return;
        }

        private void UpdateModeReverse()
        {
            bool flag;
            bool flag2;
            if (this.mPreAnchoredPositionX == this.AnchoredPosition)
            {
                goto Label_004F;
            }
            flag = (this.AnchoredPosition - this.mPreAnchoredPositionX) > 0f;
            flag2 = (this.AnchoredPosition - this.mPreAnchoredPositionX) < 0f;
            this.mPreAnchoredPositionX = this.AnchoredPosition;
            this.UpdateItemsPositionReverse(flag, flag2);
        Label_004F:
            if (this.OnUpdateItemEvent == null)
            {
                goto Label_006B;
            }
            this.OnUpdateItemEvent.Invoke(this.mItems);
        Label_006B:
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

        public float ScrollDir
        {
            get
            {
                return ((this.m_ScrollMode != null) ? 1f : -1f);
            }
        }

        public List<ItemData> Items
        {
            get
            {
                return this.mItems;
            }
        }

        public enum Direction
        {
            Vertical,
            Horizontal
        }

        public class ItemData
        {
            public GameObject gameObject;
            public RectTransform rectTransform;
            public Vector2 position;
            public UnitInventoryJobIcon job_icon;

            public ItemData(GameObject obj)
            {
                base..ctor();
                this.gameObject = obj;
                this.rectTransform = obj.get_transform() as RectTransform;
                this.position = this.rectTransform.get_anchoredPosition();
                this.job_icon = obj.GetComponent<UnitInventoryJobIcon>();
                return;
            }
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
        public class OnItemPositionAreaOverEvent : UnityEvent<GameObject>
        {
            public OnItemPositionAreaOverEvent()
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
        public class OnUpdateEvent : UnityEvent<List<JobIconScrollListController.ItemData>>
        {
            public OnUpdateEvent()
            {
                base..ctor();
                return;
            }
        }
    }
}

