namespace SRPG
{
    using GR;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using UnityEngine;
    using UnityEngine.Events;
    using UnityEngine.UI;

    [Pin(0, "背景ロード完了", 1, 0)]
    public class TowerScrollListController : MonoBehaviour
    {
        [SerializeField]
        private RectTransform m_ItemBase;
        [SerializeField]
        internal ScrollMode m_ScrollMode;
        [Range(0f, 30f), SerializeField]
        protected int m_ItemCnt;
        [SerializeField]
        private float m_Margin;
        public List<RectTransform> m_ItemList;
        private float m_PrevPosition;
        private int m_CurrentItemID;
        public Direction m_Direction;
        private RectTransform m_RectTransform;
        public OnItemPositionChange OnItemUpdate;
        [SerializeField]
        private RectTransform Cursor;
        [SerializeField]
        private ScrollAutoFit m_ScrollAutoFit;
        [SerializeField]
        private SyncScroll m_ScrollBG;
        public Selectable PageUpButton;
        public Selectable PageDownButton;
        public ListItemFocusEvent OnListItemFocus;
        [SerializeField]
        private Button mChallengeButton;
        [SerializeField]
        private Animator FadeAnimator;
        [SerializeField]
        private RawImage Bg;
        private static string BGTexturePath;
        private static string FloorBGTexturePath;
        private static string LockFloorBGTexturePath;
        private float m_ItemScale;
        [CompilerGenerated]
        private static Func<MonoBehaviour, bool> <>f__am$cache17;
        [CompilerGenerated]
        private static Func<MonoBehaviour, ScrollListSetUp> <>f__am$cache18;
        [CompilerGenerated]
        private static Converter<RectTransform, TowerQuestListItem> <>f__am$cache19;
        [CompilerGenerated]
        private static Func<MonoBehaviour, bool> <>f__am$cache1A;
        [CompilerGenerated]
        private static Func<MonoBehaviour, ScrollListSetUp> <>f__am$cache1B;

        static TowerScrollListController()
        {
            BGTexturePath = "Tower/TowerBGs";
            FloorBGTexturePath = "Tower/TowerFloors";
            LockFloorBGTexturePath = "Tower/TowerLockFloors";
            return;
        }

        public TowerScrollListController()
        {
            this.m_ItemCnt = 8;
            this.m_Margin = 1.1f;
            this.OnItemUpdate = new OnItemPositionChange();
            this.OnListItemFocus = new ListItemFocusEvent();
            this.m_ItemScale = -1f;
            base..ctor();
            return;
        }

        private unsafe void _SetScrollTo(float pos)
        {
            float num;
            float num2;
            float num3;
            float num4;
            float num5;
            float num6;
            float num7;
            float num8;
            Rect rect;
            Vector2 vector;
            Rect rect2;
            Vector2 vector2;
            Vector2 vector3;
            num = &&this.m_ScrollAutoFit.get_viewport().get_rect().get_size().y;
            num3 = (&&this.m_ScrollAutoFit.rect.get_size().y * 0.5f) - (num * 0.5f);
            num4 = 0f;
            num5 = 0f;
            num6 = (float) ((this.m_ScrollMode != null) ? 1 : -1);
            num4 = num3;
            num5 = (num3 + num) - &this.m_RectTransform.get_sizeDelta().y;
            num4 *= num6;
            num5 *= num6;
            num7 = Mathf.Min(num4, num5);
            num8 = Mathf.Max(num4, num5);
            pos = Mathf.Clamp(pos, num7, num8);
            this.m_ScrollAutoFit.SetScrollTo(pos);
            return;
        }

        [CompilerGenerated]
        private static TowerQuestListItem <FocusUpdate>m__42D(RectTransform item)
        {
            return item.GetComponent<TowerQuestListItem>();
        }

        [CompilerGenerated]
        private static bool <Start>m__42B(MonoBehaviour item)
        {
            return ((item as ScrollListSetUp) > null);
        }

        [CompilerGenerated]
        private static ScrollListSetUp <Start>m__42C(MonoBehaviour item)
        {
            return (item as ScrollListSetUp);
        }

        [CompilerGenerated]
        private static bool <UpdateList>m__42E(MonoBehaviour item)
        {
            return ((item as ScrollListSetUp) > null);
        }

        [CompilerGenerated]
        private static ScrollListSetUp <UpdateList>m__42F(MonoBehaviour item)
        {
            return (item as ScrollListSetUp);
        }

        public void ChangeScrollMode(ScrollMode scrollMode)
        {
            this.m_ScrollMode = scrollMode;
            this.SetAnchor(scrollMode);
            this.m_ItemList.Reverse();
            if (Application.get_isPlaying() == null)
            {
                goto Label_0029;
            }
            this.UpdateList();
        Label_0029:
            return;
        }

        private unsafe void FocusUpdate()
        {
            float num;
            Rect rect;
            List<TowerQuestListItem> list;
            TowerQuestListItem item;
            List<TowerQuestListItem>.Enumerator enumerator;
            Vector2 vector;
            Vector2 vector2;
            num = (float) ((this.m_ScrollMode != null) ? 1 : -1);
            rect = this.Cursor.get_rect();
            &rect.set_center(new Vector2(0f, ((this.ItemScaleMargin * 3f) - (this.ItemScaleMargin * 0.5f)) * num));
            &rect.set_size(this.Cursor.get_sizeDelta());
            if (<>f__am$cache19 != null)
            {
                goto Label_007C;
            }
            <>f__am$cache19 = new Converter<RectTransform, TowerQuestListItem>(TowerScrollListController.<FocusUpdate>m__42D);
        Label_007C:
            enumerator = this.m_ItemList.ConvertAll<TowerQuestListItem>(<>f__am$cache19).GetEnumerator();
        Label_008F:
            try
            {
                goto Label_00EB;
            Label_0094:
                item = &enumerator.Current;
                if (item.get_gameObject().get_activeInHierarchy() == null)
                {
                    goto Label_00EB;
                }
                vector = item.rectTransform.get_anchoredPosition();
                &vector.y = &this.GetRectTransForm.get_anchoredPosition().y + &vector.y;
                item.OnFocus(&rect.Contains(vector));
            Label_00EB:
                if (&enumerator.MoveNext() != null)
                {
                    goto Label_0094;
                }
                goto Label_0109;
            }
            finally
            {
            Label_00FC:
                ((List<TowerQuestListItem>.Enumerator) enumerator).Dispose();
            }
        Label_0109:
            return;
        }

        private unsafe void LateUpdate()
        {
            float num;
            float num2;
            RectTransform transform;
            RectTransform transform2;
            float num3;
            float num4;
            Rect rect;
            Rect rect2;
            num = (float) ((this.m_ScrollMode != null) ? 1 : -1);
            this.MovePosition(num);
            num2 = Mathf.Abs(Vector2.Dot(this.m_ScrollAutoFit.get_normalizedPosition(), this.ScrollDir));
            transform = this.m_ScrollAutoFit.get_transform() as RectTransform;
            transform2 = this.m_ScrollAutoFit.get_content().get_transform() as RectTransform;
            if ((this.m_ScrollAutoFit.get_content() != null) == null)
            {
                goto Label_012E;
            }
            num3 = Mathf.Abs(Vector2.Dot(&transform.get_rect().get_size(), this.ScrollDir));
            num4 = Mathf.Abs(Vector2.Dot(&transform2.get_rect().get_size(), this.ScrollDir));
            if (this.m_ScrollAutoFit.get_horizontal() == null)
            {
                goto Label_00CE;
            }
            num2 = 1f - num2;
        Label_00CE:
            if ((this.PageUpButton != null) == null)
            {
                goto Label_00FE;
            }
            this.PageUpButton.set_interactable((num2 >= 0.999f) ? 0 : (num3 < num4));
        Label_00FE:
            if ((this.PageDownButton != null) == null)
            {
                goto Label_012E;
            }
            this.PageDownButton.set_interactable((num2 <= 0.001f) ? 0 : (num3 < num4));
        Label_012E:
            this.FocusUpdate();
            return;
        }

        [DebuggerHidden]
        public IEnumerator LoadTowerBG(TowerQuestListItem[] tower_quest_list)
        {
            <LoadTowerBG>c__Iterator145 iterator;
            iterator = new <LoadTowerBG>c__Iterator145();
            iterator.tower_quest_list = tower_quest_list;
            iterator.<$>tower_quest_list = tower_quest_list;
            iterator.<>f__this = this;
            return iterator;
        }

        private unsafe void MovePosition(float scrollDir)
        {
            RectTransform transform;
            RectTransform transform2;
            Vector2 vector;
            float num;
            int num2;
            RectTransform transform3;
            RectTransform transform4;
            float num3;
            Vector2 vector2;
            goto Label_009E;
        Label_0005:
            this.m_PrevPosition -= this.ItemScaleMargin;
            transform = this.m_ItemList[0];
            transform2 = Enumerable.Last<RectTransform>(this.m_ItemList);
            this.m_ItemList.RemoveAt(0);
            this.m_ItemList.Add(transform);
            num = &transform2.get_anchoredPosition().y + (this.ItemScaleMargin * scrollDir);
            transform.set_anchoredPosition(new Vector2(0f, num));
            this.OnItemUpdate.Invoke(this.m_CurrentItemID + this.m_ItemCnt, transform.get_gameObject());
            this.m_CurrentItemID += 1;
        Label_009E:
            if ((this.AnchoredPosition - this.m_PrevPosition) < -(this.ItemScaleMargin + (this.ItemScale * 0.5f)))
            {
                goto Label_0005;
            }
            goto Label_0172;
        Label_00C9:
            this.m_PrevPosition += this.ItemScaleMargin;
            num2 = this.m_ItemCnt - 1;
            transform3 = this.m_ItemList[num2];
            transform4 = this.m_ItemList[0];
            this.m_ItemList.RemoveAt(num2);
            this.m_ItemList.Insert(0, transform3);
            this.m_CurrentItemID -= 1;
            num3 = &transform4.get_anchoredPosition().y - (this.ItemScaleMargin * scrollDir);
            transform3.set_anchoredPosition(new Vector2(0f, num3));
            this.OnItemUpdate.Invoke(this.m_CurrentItemID, transform3.get_gameObject());
        Label_0172:
            if ((this.AnchoredPosition - this.m_PrevPosition) > (-this.ItemScale * 0.5f))
            {
                goto Label_00C9;
            }
            return;
        }

        private unsafe void OnScrollStop()
        {
            float num;
            Rect rect;
            RectTransform transform;
            List<RectTransform>.Enumerator enumerator;
            Vector2 vector;
            Vector2 vector2;
            num = (float) ((this.m_ScrollMode != null) ? 1 : -1);
            this.MovePosition(num);
            rect = this.Cursor.get_rect();
            &rect.set_center(new Vector2(0f, ((this.ItemScaleMargin * 3f) - (this.ItemScaleMargin * 0.5f)) * num));
            &rect.set_size(this.Cursor.get_sizeDelta());
            enumerator = this.m_ItemList.GetEnumerator();
        Label_0071:
            try
            {
                goto Label_00D8;
            Label_0076:
                transform = &enumerator.Current;
                if (transform.get_gameObject().get_activeInHierarchy() == null)
                {
                    goto Label_00D8;
                }
                vector = transform.get_anchoredPosition();
                &vector.y = &this.GetRectTransForm.get_anchoredPosition().y + &vector.y;
                if (&rect.Contains(vector) == null)
                {
                    goto Label_00D8;
                }
                this.OnListItemFocus.Invoke(transform.get_gameObject());
            Label_00D8:
                if (&enumerator.MoveNext() != null)
                {
                    goto Label_0076;
                }
                goto Label_00F5;
            }
            finally
            {
            Label_00E9:
                ((List<RectTransform>.Enumerator) enumerator).Dispose();
            }
        Label_00F5:
            return;
        }

        public unsafe void PageDown(int value)
        {
            int num;
            Vector2 vector;
            num = Mathf.RoundToInt(&this.GetRectTransForm.get_anchoredPosition().y / this.ItemScale);
            this.mChallengeButton.set_interactable(0);
            this._SetScrollTo((((float) num) * this.ItemScale) + (((float) value) * this.ItemScale));
            return;
        }

        public unsafe void PageUp(int value)
        {
            int num;
            Vector2 vector;
            num = Mathf.RoundToInt(&this.GetRectTransForm.get_anchoredPosition().y / this.ItemScale);
            this.mChallengeButton.set_interactable(0);
            this._SetScrollTo((((float) num) * this.ItemScale) - (((float) value) * this.ItemScale));
            return;
        }

        internal unsafe void SetAnchor(ScrollMode scrollMode)
        {
            RectTransform transform;
            List<RectTransform>.Enumerator enumerator;
            if (Application.get_isPlaying() != null)
            {
                goto Label_0058;
            }
            SetAnchor(this.GetRectTransForm, scrollMode);
            if ((this.m_ScrollBG != null) == null)
            {
                goto Label_0047;
            }
            SetAnchor(this.m_ScrollBG.GetComponent<RectTransform>(), scrollMode);
            this.m_ScrollBG.isNormal = scrollMode == 0;
        Label_0047:
            SetItemAnchor(this.m_ItemBase, scrollMode);
            goto Label_00D2;
        Label_0058:
            SetAnchor(this.GetRectTransForm, scrollMode);
            if ((this.m_ScrollBG != null) == null)
            {
                goto Label_0095;
            }
            SetAnchor(this.m_ScrollBG.GetComponent<RectTransform>(), scrollMode);
            this.m_ScrollBG.isNormal = scrollMode == 0;
        Label_0095:
            enumerator = this.m_ItemList.GetEnumerator();
        Label_00A1:
            try
            {
                goto Label_00B5;
            Label_00A6:
                transform = &enumerator.Current;
                SetItemAnchor(transform, scrollMode);
            Label_00B5:
                if (&enumerator.MoveNext() != null)
                {
                    goto Label_00A6;
                }
                goto Label_00D2;
            }
            finally
            {
            Label_00C6:
                ((List<RectTransform>.Enumerator) enumerator).Dispose();
            }
        Label_00D2:
            return;
        }

        internal static unsafe void SetAnchor(RectTransform rt, ScrollMode scrollMode)
        {
            Vector2 vector;
            float num;
            float num2;
            Vector2 vector2;
            float num3;
            float num4;
            Vector2 vector3;
            Vector2 vector4;
            Vector2 vector5;
            Vector2 vector6;
            if ((rt == null) == null)
            {
                goto Label_000D;
            }
            return;
        Label_000D:
            if (scrollMode != null)
            {
                goto Label_007D;
            }
            vector = rt.get_anchoredPosition();
            num = &rt.get_anchorMin().x;
            num2 = &rt.get_anchorMax().x;
            rt.set_anchorMin(new Vector2(num, 1f));
            rt.set_anchorMax(new Vector2(num2, 1f));
            rt.set_pivot(new Vector2(0f, 1f));
            rt.set_anchoredPosition(vector);
            goto Label_00E6;
        Label_007D:
            vector2 = rt.get_anchoredPosition();
            num3 = &rt.get_anchorMin().x;
            num4 = &rt.get_anchorMax().x;
            rt.set_anchorMin(new Vector2(num3, 0f));
            rt.set_anchorMax(new Vector2(num4, 0f));
            rt.set_pivot(new Vector2(0f, 0f));
            rt.set_anchoredPosition(vector2);
        Label_00E6:
            return;
        }

        public unsafe void SetAnchoredPosition(float position)
        {
            Vector2 vector;
            Vector2 vector2;
            if (this.m_Direction != null)
            {
                goto Label_0049;
            }
            vector = this.GetRectTransForm.get_anchoredPosition();
            if (this.m_ScrollMode != null)
            {
                goto Label_002F;
            }
            &vector.y = position;
            goto Label_0038;
        Label_002F:
            &vector.y = -position;
        Label_0038:
            this.GetRectTransForm.set_anchoredPosition(vector);
            goto Label_0069;
        Label_0049:
            vector2 = this.GetRectTransForm.get_anchoredPosition();
            &vector2.x = position;
            this.GetRectTransForm.set_anchoredPosition(vector2);
        Label_0069:
            return;
        }

        internal static unsafe void SetItemAnchor(RectTransform rt, ScrollMode scrollMode)
        {
            Vector2 vector;
            float num;
            float num2;
            Vector2 vector2;
            float num3;
            float num4;
            Vector2 vector3;
            Vector2 vector4;
            Vector2 vector5;
            Vector2 vector6;
            if ((rt == null) == null)
            {
                goto Label_000D;
            }
            return;
        Label_000D:
            if (scrollMode != null)
            {
                goto Label_0077;
            }
            vector = rt.get_anchoredPosition();
            &vector.y = -&vector.y;
            num = &rt.get_anchorMin().x;
            num2 = &rt.get_anchorMax().x;
            rt.set_anchorMin(new Vector2(num, 1f));
            rt.set_anchorMax(new Vector2(num2, 1f));
            rt.set_anchoredPosition(vector);
            goto Label_00DA;
        Label_0077:
            vector2 = rt.get_anchoredPosition();
            &vector2.y = -&vector2.y;
            num3 = &rt.get_anchorMin().x;
            num4 = &rt.get_anchorMax().x;
            rt.set_anchorMin(new Vector2(num3, 0f));
            rt.set_anchorMax(new Vector2(num4, 0f));
            rt.set_anchoredPosition(vector2);
        Label_00DA:
            return;
        }

        public void SetScrollTo(float pos)
        {
            if (this.m_Direction != null)
            {
                goto Label_002A;
            }
            if (this.m_ScrollMode != null)
            {
                goto Label_0022;
            }
            this._SetScrollTo(pos);
            goto Label_002A;
        Label_0022:
            this._SetScrollTo(-pos);
        Label_002A:
            return;
        }

        public void SetTowerImage(LoadRequest floor_req, int index, string image_name, TowerQuestListItem[] tower_quest_list)
        {
            GachaTabSprites sprites;
            int num;
            int num2;
            sprites = floor_req.asset as GachaTabSprites;
            if ((sprites == null) == null)
            {
                goto Label_0019;
            }
            return;
        Label_0019:
            num = 0;
            goto Label_008A;
        Label_0020:
            if ((sprites.Sprites[num].get_name() != image_name) == null)
            {
                goto Label_003D;
            }
            goto Label_0086;
        Label_003D:
            num2 = 0;
            goto Label_007C;
        Label_0044:
            tower_quest_list[num2].Banner[0].Images[index] = sprites.Sprites[num];
            tower_quest_list[num2].Banner[1].Images[index] = sprites.Sprites[num];
            num2 += 1;
        Label_007C:
            if (num2 < ((int) tower_quest_list.Length))
            {
                goto Label_0044;
            }
        Label_0086:
            num += 1;
        Label_008A:
            if (num < ((int) sprites.Sprites.Length))
            {
                goto Label_0020;
            }
            return;
        }

        protected virtual unsafe void Start()
        {
            List<ScrollListSetUp> list;
            float num;
            List<TowerQuestListItem> list2;
            int num2;
            RectTransform transform;
            TowerQuestListItem item;
            ScrollListSetUp up;
            List<ScrollListSetUp>.Enumerator enumerator;
            int num3;
            bool flag;
            if (<>f__am$cache17 != null)
            {
                goto Label_001E;
            }
            <>f__am$cache17 = new Func<MonoBehaviour, bool>(TowerScrollListController.<Start>m__42B);
        Label_001E:
            if (<>f__am$cache18 != null)
            {
                goto Label_0040;
            }
            <>f__am$cache18 = new Func<MonoBehaviour, ScrollListSetUp>(TowerScrollListController.<Start>m__42C);
        Label_0040:
            list = Enumerable.ToList<ScrollListSetUp>(Enumerable.Select<MonoBehaviour, ScrollListSetUp>(Enumerable.Where<MonoBehaviour>(base.GetComponents<MonoBehaviour>(), <>f__am$cache17), <>f__am$cache18));
            if ((this.m_ScrollAutoFit != null) == null)
            {
                goto Label_009F;
            }
            this.m_ScrollAutoFit.set_content(this.GetRectTransForm);
            this.m_ScrollAutoFit.ItemScale = this.ItemScaleMargin;
            this.m_ScrollAutoFit.OnScrollStop.AddListener(new UnityAction(this, this.OnScrollStop));
        Label_009F:
            this.m_ItemBase.get_gameObject().SetActive(0);
            num = (float) ((this.m_ScrollMode != null) ? 1 : -1);
            list2 = new List<TowerQuestListItem>();
            num2 = 0;
            goto Label_0158;
        Label_00D1:
            transform = Object.Instantiate<RectTransform>(this.m_ItemBase);
            transform.SetParent(base.get_transform(), 0);
            transform.set_anchoredPosition(new Vector2(0f, (((this.ItemScale * this.Margin) * ((float) num2)) + (this.ItemScale * 0.5f)) * num));
            this.m_ItemList.Add(transform);
            item = transform.GetComponent<TowerQuestListItem>();
            if ((item != null) == null)
            {
                goto Label_0147;
            }
            list2.Add(item);
        Label_0147:
            transform.get_gameObject().SetActive(1);
            num2 += 1;
        Label_0158:
            if (num2 < this.m_ItemCnt)
            {
                goto Label_00D1;
            }
            enumerator = list.GetEnumerator();
        Label_016C:
            try
            {
                goto Label_01B7;
            Label_0171:
                up = &enumerator.Current;
                up.OnSetUpItems();
                num3 = 0;
                goto Label_01AA;
            Label_0189:
                up.OnUpdateItems(num3, this.m_ItemList[num3].get_gameObject());
                num3 += 1;
            Label_01AA:
                if (num3 < this.m_ItemCnt)
                {
                    goto Label_0189;
                }
            Label_01B7:
                if (&enumerator.MoveNext() != null)
                {
                    goto Label_0171;
                }
                goto Label_01D5;
            }
            finally
            {
            Label_01C8:
                ((List<ScrollListSetUp>.Enumerator) enumerator).Dispose();
            }
        Label_01D5:
            flag = MonoSingleton<GameManager>.Instance.FindTower(GlobalVars.SelectedTowerID).is_down;
            this.m_ScrollMode = (flag == null) ? 1 : 0;
            this.ChangeScrollMode(this.m_ScrollMode);
            base.StartCoroutine(this.LoadTowerBG(list2.ToArray()));
            return;
        }

        public unsafe void UpdateList()
        {
            List<ScrollListSetUp> list;
            ScrollRect rect;
            float num;
            int num2;
            RectTransform transform;
            ScrollListSetUp up;
            List<ScrollListSetUp>.Enumerator enumerator;
            int num3;
            RectTransform transform2;
            Vector2 vector;
            if (<>f__am$cache1A != null)
            {
                goto Label_001E;
            }
            <>f__am$cache1A = new Func<MonoBehaviour, bool>(TowerScrollListController.<UpdateList>m__42E);
        Label_001E:
            if (<>f__am$cache1B != null)
            {
                goto Label_0040;
            }
            <>f__am$cache1B = new Func<MonoBehaviour, ScrollListSetUp>(TowerScrollListController.<UpdateList>m__42F);
        Label_0040:
            list = Enumerable.ToList<ScrollListSetUp>(Enumerable.Select<MonoBehaviour, ScrollListSetUp>(Enumerable.Where<MonoBehaviour>(base.GetComponents<MonoBehaviour>(), <>f__am$cache1A), <>f__am$cache1B));
            base.GetComponentInParent<ScrollRect>().set_content(this.GetRectTransForm);
            this.m_ItemBase.get_gameObject().SetActive(0);
            num = (float) ((this.m_ScrollMode != null) ? 1 : -1);
            num2 = 0;
            goto Label_00EC;
        Label_008F:
            transform = this.m_ItemList[num2];
            transform.SetParent(base.get_transform(), 0);
            transform.set_anchoredPosition(new Vector2(0f, (((this.ItemScale * this.Margin) * ((float) num2)) + (this.ItemScale * 0.5f)) * num));
            transform.get_gameObject().SetActive(1);
            num2 += 1;
        Label_00EC:
            if (num2 < this.m_ItemCnt)
            {
                goto Label_008F;
            }
            enumerator = list.GetEnumerator();
        Label_0100:
            try
            {
                goto Label_014B;
            Label_0105:
                up = &enumerator.Current;
                up.OnSetUpItems();
                num3 = 0;
                goto Label_013E;
            Label_011D:
                up.OnUpdateItems(num3, this.m_ItemList[num3].get_gameObject());
                num3 += 1;
            Label_013E:
                if (num3 < this.m_ItemCnt)
                {
                    goto Label_011D;
                }
            Label_014B:
                if (&enumerator.MoveNext() != null)
                {
                    goto Label_0105;
                }
                goto Label_0169;
            }
            finally
            {
            Label_015C:
                ((List<ScrollListSetUp>.Enumerator) enumerator).Dispose();
            }
        Label_0169:
            this.m_PrevPosition = 0f;
            this.m_CurrentItemID = 0;
            transform2 = base.get_transform().GetComponent<RectTransform>();
            vector = transform2.get_anchoredPosition();
            &vector.y = 0f;
            transform2.set_anchoredPosition(vector);
            return;
        }

        public float Margin
        {
            get
            {
                return this.m_Margin;
            }
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

        public float ItemScaleMargin
        {
            get
            {
                return (this.ItemScale * this.Margin);
            }
        }

        private float AnchoredPosition
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

        private Vector2 ScrollDir
        {
            get
            {
                if (this.m_ScrollAutoFit.get_vertical() == null)
                {
                    goto Label_001B;
                }
                return -Vector2.get_up();
            Label_001B:
                return Vector2.get_right();
            }
        }

        [CompilerGenerated]
        private sealed class <LoadTowerBG>c__Iterator145 : IEnumerator, IDisposable, IEnumerator<object>
        {
            internal TowerParam <tower_param>__0;
            internal LoadRequest <req>__1;
            internal GachaBGTextures <bg_texture>__2;
            internal int <i>__3;
            internal LoadRequest <floor_req>__4;
            internal TowerQuestListItem[] tower_quest_list;
            internal LoadRequest <floor_req>__5;
            internal int <i>__6;
            internal int $PC;
            internal object $current;
            internal TowerQuestListItem[] <$>tower_quest_list;
            internal TowerScrollListController <>f__this;

            public <LoadTowerBG>c__Iterator145()
            {
                base..ctor();
                return;
            }

            [DebuggerHidden]
            public void Dispose()
            {
                this.$PC = -1;
                return;
            }

            public bool MoveNext()
            {
                uint num;
                bool flag;
                num = this.$PC;
                this.$PC = -1;
                switch (num)
                {
                    case 0:
                        goto Label_0029;

                    case 1:
                        goto Label_0090;

                    case 2:
                        goto Label_0164;

                    case 3:
                        goto Label_01B4;
                }
                goto Label_023A;
            Label_0029:
                this.<tower_param>__0 = MonoSingleton<GameManager>.Instance.FindTower(GlobalVars.SelectedTowerID);
                if ((this.<>f__this.FadeAnimator != null) == null)
                {
                    goto Label_0233;
                }
                if (string.IsNullOrEmpty(TowerScrollListController.BGTexturePath) != null)
                {
                    goto Label_0233;
                }
                this.<req>__1 = AssetManager.LoadAsync<GachaBGTextures>(TowerScrollListController.BGTexturePath);
                this.$current = this.<req>__1.StartCoroutine();
                this.$PC = 1;
                goto Label_023C;
            Label_0090:
                this.<bg_texture>__2 = this.<req>__1.asset as GachaBGTextures;
                if ((this.<bg_texture>__2 != null) == null)
                {
                    goto Label_0137;
                }
                this.<i>__3 = 0;
                goto Label_011F;
            Label_00C3:
                if ((this.<bg_texture>__2.Textures[this.<i>__3].get_name() == this.<tower_param>__0.bg) == null)
                {
                    goto Label_0111;
                }
                this.<>f__this.Bg.set_texture(this.<bg_texture>__2.Textures[this.<i>__3]);
            Label_0111:
                this.<i>__3 += 1;
            Label_011F:
                if (this.<i>__3 < ((int) this.<bg_texture>__2.Textures.Length))
                {
                    goto Label_00C3;
                }
            Label_0137:
                this.<floor_req>__4 = AssetManager.LoadAsync<GachaTabSprites>(TowerScrollListController.FloorBGTexturePath);
                this.$current = this.<floor_req>__4.StartCoroutine();
                this.$PC = 2;
                goto Label_023C;
            Label_0164:
                this.<>f__this.SetTowerImage(this.<floor_req>__4, 0, this.<tower_param>__0.floor_bg_open, this.tower_quest_list);
                this.<floor_req>__5 = AssetManager.LoadAsync<GachaTabSprites>(TowerScrollListController.LockFloorBGTexturePath);
                this.$current = this.<floor_req>__5.StartCoroutine();
                this.$PC = 3;
                goto Label_023C;
            Label_01B4:
                this.<>f__this.SetTowerImage(this.<floor_req>__5, 1, this.<tower_param>__0.floor_bg_close, this.tower_quest_list);
                this.<i>__6 = 0;
                goto Label_0203;
            Label_01E3:
                this.tower_quest_list[this.<i>__6].SetNowImage();
                this.<i>__6 += 1;
            Label_0203:
                if (this.<i>__6 < ((int) this.tower_quest_list.Length))
                {
                    goto Label_01E3;
                }
                this.<>f__this.FadeAnimator.set_enabled(1);
                FlowNode_GameObject.ActivateOutputLinks(this.<>f__this, 0);
            Label_0233:
                this.$PC = -1;
            Label_023A:
                return 0;
            Label_023C:
                return 1;
                return flag;
            }

            [DebuggerHidden]
            public void Reset()
            {
                throw new NotSupportedException();
            }

            object IEnumerator<object>.Current
            {
                [DebuggerHidden]
                get
                {
                    return this.$current;
                }
            }

            object IEnumerator.Current
            {
                [DebuggerHidden]
                get
                {
                    return this.$current;
                }
            }
        }

        public enum Direction
        {
            Vertical,
            Horizontal
        }

        [SerializeField]
        public class ListItemFocusEvent : UnityEvent<GameObject>
        {
            public ListItemFocusEvent()
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

        public enum ScrollMode
        {
            Normal,
            Reverse
        }
    }
}

