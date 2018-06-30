namespace SRPG
{
    using GR;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;
    using UnityEngine;
    using UnityEngine.EventSystems;
    using UnityEngine.UI;

    public abstract class ScrollablePulldownBase : Selectable, IBeginDragHandler, IDragHandler, IEndDragHandler, IEventSystemHandler
    {
        public SelectItemEvent OnSelectionChangeDelegate;
        [SerializeField]
        protected List<PulldownItem> Items;
        [SerializeField]
        protected RectTransform ItemHolder;
        [SerializeField]
        protected UnityEngine.UI.ScrollRect ScrollRect;
        [SerializeField]
        private Text SelectionText;
        [SerializeField]
        private GameObject BackGround;
        [SerializeField]
        private string OpenSE;
        [SerializeField]
        private string CloseSE;
        [SerializeField]
        private string SelectSE;
        private int mPrevSelectionIndex;
        private int mSelectionIndex;
        private bool mOpened;
        private bool mTrackTouchPosititon;

        protected ScrollablePulldownBase()
        {
            this.Items = new List<PulldownItem>();
            this.mPrevSelectionIndex = -1;
            this.mSelectionIndex = -1;
            base..ctor();
            return;
        }

        public void ClosePulldown(bool se)
        {
            if (this.mOpened != null)
            {
                goto Label_000C;
            }
            return;
        Label_000C:
            this.ScrollRect.set_verticalNormalizedPosition(1f);
            this.ScrollRect.set_horizontalNormalizedPosition(1f);
            this.BackGround.SetActive(0);
            this.mOpened = 0;
            if (se == null)
            {
                goto Label_006A;
            }
            if (string.IsNullOrEmpty(this.CloseSE) != null)
            {
                goto Label_006A;
            }
            MonoSingleton<MySound>.Instance.PlaySEOneShot(this.CloseSE, 0f);
        Label_006A:
            return;
        }

        public PulldownItem GetCurrentSelection()
        {
            return this.GetItemAt(this.mSelectionIndex);
        }

        public PulldownItem GetItemAt(int index)
        {
            return (((0 > index) || (index >= this.Items.Count)) ? null : this.Items[index]);
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();
            if ((this.SelectionText != null) == null)
            {
                goto Label_003C;
            }
            if (string.IsNullOrEmpty(this.SelectionText.get_text()) != null)
            {
                goto Label_003C;
            }
            this.SelectionText.set_text(string.Empty);
        Label_003C:
            this.mSelectionIndex = -1;
            return;
        }

        public void OnDrag(PointerEventData eventData)
        {
            if (this.mOpened == null)
            {
                goto Label_0013;
            }
            this.SelectNearestItem(eventData);
        Label_0013:
            return;
        }

        public unsafe void OnEndDrag(PointerEventData eventData)
        {
            Vector2 vector;
            if (this.mOpened == null)
            {
                goto Label_0043;
            }
            vector = eventData.get_pressPosition() - eventData.get_position();
            if (&vector.get_magnitude() <= 5f)
            {
                goto Label_0043;
            }
            this.SelectNearestItem(eventData);
            this.ClosePulldown(0);
            this.TriggerItemChange();
        Label_0043:
            return;
        }

        public override void OnPointerDown(PointerEventData eventData)
        {
            base.OnPointerDown(eventData);
            if (this.IsInteractable() != null)
            {
                goto Label_0013;
            }
            return;
        Label_0013:
            if (this.mOpened == null)
            {
                goto Label_0026;
            }
            this.ClosePulldown(1);
            return;
        Label_0026:
            this.OpenPulldown();
            return;
        }

        private void OnPulldownMenuTouch(BaseEventData eventData)
        {
            PointerEventData data;
            data = eventData as PointerEventData;
            if (this.SelectNearestItem(data) == null)
            {
                goto Label_0025;
            }
            this.ClosePulldown(0);
            this.TriggerItemChange();
            goto Label_002C;
        Label_0025:
            this.ClosePulldown(1);
        Label_002C:
            return;
        }

        public void OpenPulldown()
        {
            if (this.mOpened != null)
            {
                goto Label_001C;
            }
            if (this.Items.Count > 1)
            {
                goto Label_001D;
            }
        Label_001C:
            return;
        Label_001D:
            this.BackGround.SetActive(1);
            this.mOpened = 1;
            this.mTrackTouchPosititon = 0;
            if (string.IsNullOrEmpty(this.OpenSE) != null)
            {
                goto Label_005C;
            }
            MonoSingleton<MySound>.Instance.PlaySEOneShot(this.OpenSE, 0f);
        Label_005C:
            return;
        }

        protected void ResetAllStatus()
        {
            this.mSelectionIndex = -1;
            return;
        }

        private unsafe bool SelectNearestItem(PointerEventData e)
        {
            Vector2 vector;
            Vector2 vector2;
            float num;
            int num2;
            int num3;
            RectTransform transform;
            float num4;
            Rect rect;
            vector = e.get_position();
            num = 3.402823E+38f;
            num2 = -1;
            num3 = 0;
            goto Label_00C1;
        Label_0017:
            transform = this.Items[num3].get_transform() as RectTransform;
            RectTransformUtility.ScreenPointToLocalPointInRectangle(transform, vector, null, &vector2);
            if (this.mTrackTouchPosititon == null)
            {
                goto Label_0063;
            }
            num4 = &vector2.get_magnitude();
            if (num4 >= num)
            {
                goto Label_00BB;
            }
            num2 = num3;
            num = num4;
            goto Label_00BB;
        Label_0063:
            rect = transform.get_rect();
            if (&rect.get_xMin() > &vector2.x)
            {
                goto Label_00BB;
            }
            if (&vector2.x >= &rect.get_xMax())
            {
                goto Label_00BB;
            }
            if (&rect.get_yMin() > &vector2.y)
            {
                goto Label_00BB;
            }
            if (&vector2.y >= &rect.get_yMax())
            {
                goto Label_00BB;
            }
            num2 = num3;
        Label_00BB:
            num3 += 1;
        Label_00C1:
            if (num3 < this.Items.Count)
            {
                goto Label_0017;
            }
            if (num2 < 0)
            {
                goto Label_00F6;
            }
            if (num2 == this.Selection)
            {
                goto Label_00ED;
            }
            this.mTrackTouchPosititon = 1;
        Label_00ED:
            this.Selection = num2;
            return 1;
        Label_00F6:
            return 0;
        }

        protected override void Start()
        {
            base.Start();
            if ((this.BackGround != null) == null)
            {
                goto Label_0028;
            }
            this.BackGround.get_gameObject().SetActive(0);
        Label_0028:
            return;
        }

        protected void TriggerItemChange()
        {
            int num;
            if (string.IsNullOrEmpty(this.SelectSE) != null)
            {
                goto Label_0025;
            }
            MonoSingleton<MySound>.Instance.PlaySEOneShot(this.SelectSE, 0f);
        Label_0025:
            if (this.mPrevSelectionIndex != this.mSelectionIndex)
            {
                goto Label_0037;
            }
            return;
        Label_0037:
            this.mPrevSelectionIndex = this.mSelectionIndex;
            num = this.Items[this.mSelectionIndex].Value;
            if (this.OnSelectionChangeDelegate == null)
            {
                goto Label_0071;
            }
            this.OnSelectionChangeDelegate(num);
        Label_0071:
            return;
        }

        public int Selection
        {
            get
            {
                return this.mSelectionIndex;
            }
            set
            {
                int num;
                if (this.mSelectionIndex == value)
                {
                    goto Label_0024;
                }
                if (value < 0)
                {
                    goto Label_0024;
                }
                if (value < this.Items.Count)
                {
                    goto Label_0025;
                }
            Label_0024:
                return;
            Label_0025:
                this.mSelectionIndex = value;
                num = 0;
                goto Label_0051;
            Label_0033:
                this.Items[num].OnStatusChanged(num == this.mSelectionIndex);
                num += 1;
            Label_0051:
                if (num < this.Items.Count)
                {
                    goto Label_0033;
                }
                if ((this.Items[this.mSelectionIndex].Text != null) == null)
                {
                    goto Label_00A9;
                }
                this.SelectionText.set_text(this.Items[this.mSelectionIndex].Text.get_text());
            Label_00A9:
                return;
            }
        }

        public int PrevSelection
        {
            set
            {
                this.mPrevSelectionIndex = value;
                return;
            }
        }

        public int ItemCount
        {
            get
            {
                return this.Items.Count;
            }
        }

        public delegate void SelectItemEvent(int value);
    }
}

