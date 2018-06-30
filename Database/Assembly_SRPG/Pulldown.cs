namespace SRPG
{
    using GR;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;
    using UnityEngine;
    using UnityEngine.Events;
    using UnityEngine.EventSystems;
    using UnityEngine.UI;

    public class Pulldown : Selectable, IBeginDragHandler, IDragHandler, IEndDragHandler, IEventSystemHandler
    {
        public SetupPulldownItemEvent OnSetupPulldownItem;
        public UpdateSelectionEvent OnUpdateSelection;
        public RectTransform PulldownMenu;
        public Text SelectionText;
        public GameObject PulldownItemTemplate;
        public Text PulldownText;
        public Graphic PulldownGraphic;
        public string OpenSE;
        public string CloseSE;
        public string SelectSE;
        public SelectItemEvent OnSelectionChangeDelegate;
        public UnityAction<int> OnSelectionChange;
        private int mPrevSelectionIndex;
        private int mSelectionIndex;
        private bool mOpened;
        private bool mAutoClose;
        private bool mTrackTouchPosititon;
        private List<PulldownItem> mItems;
        private bool mPulldownItemInitialized;
        private bool mPollMouseUp;

        public Pulldown()
        {
            this.mPrevSelectionIndex = -1;
            this.mSelectionIndex = -1;
            this.mItems = new List<PulldownItem>();
            base..ctor();
            return;
        }

        public virtual PulldownItem AddItem(string label, int value)
        {
            PulldownItem item;
            GameObject obj2;
            PulldownItem item2;
            if ((this.PulldownItemTemplate == null) == null)
            {
                goto Label_0013;
            }
            return null;
        Label_0013:
            if (this.mPulldownItemInitialized != null)
            {
                goto Label_006C;
            }
            this.mPulldownItemInitialized = 1;
            if (this.OnSetupPulldownItem == null)
            {
                goto Label_0047;
            }
            item = this.OnSetupPulldownItem(this.PulldownItemTemplate);
            goto Label_0054;
        Label_0047:
            item = this.SetupPulldownItem(this.PulldownItemTemplate);
        Label_0054:
            item.Text = this.PulldownText;
            item.Graphic = this.PulldownGraphic;
        Label_006C:
            obj2 = Object.Instantiate<GameObject>(this.PulldownItemTemplate);
            item2 = obj2.GetComponent<PulldownItem>();
            if ((item2.Text != null) == null)
            {
                goto Label_009C;
            }
            item2.Text.set_text(label);
        Label_009C:
            item2.Value = value;
            this.mItems.Add(item2);
            obj2.get_transform().SetParent(this.PulldownMenu, 0);
            obj2.SetActive(1);
            return item2;
        }

        protected override void Awake()
        {
            base.Awake();
            if ((this.PulldownItemTemplate == null) == null)
            {
                goto Label_0039;
            }
            if ((this.PulldownText != null) == null)
            {
                goto Label_0039;
            }
            this.PulldownItemTemplate = this.PulldownText.get_gameObject();
        Label_0039:
            return;
        }

        public void ClearItems()
        {
            int num;
            num = 0;
            goto Label_0021;
        Label_0007:
            Object.Destroy(this.mItems[num].get_gameObject());
            num += 1;
        Label_0021:
            if (num < this.mItems.Count)
            {
                goto Label_0007;
            }
            this.mItems.Clear();
            if ((this.SelectionText != null) == null)
            {
                goto Label_0073;
            }
            if (string.IsNullOrEmpty(this.SelectionText.get_text()) != null)
            {
                goto Label_0073;
            }
            this.SelectionText.set_text(string.Empty);
        Label_0073:
            this.mSelectionIndex = -1;
            return;
        }

        private void ClosePulldown(bool se)
        {
            if (this.mOpened != null)
            {
                goto Label_000C;
            }
            return;
        Label_000C:
            this.PulldownMenu.get_gameObject().SetActive(0);
            this.mAutoClose = 0;
            this.mOpened = 0;
            if (se == null)
            {
                goto Label_0056;
            }
            if (string.IsNullOrEmpty(this.CloseSE) != null)
            {
                goto Label_0056;
            }
            MonoSingleton<MySound>.Instance.PlaySEOneShot(this.CloseSE, 0f);
        Label_0056:
            return;
        }

        public PulldownItem GetCurrentSelection()
        {
            return this.GetItemAt(this.mSelectionIndex);
        }

        public PulldownItem GetItemAt(int index)
        {
            return (((0 > index) || (index >= this.mItems.Count)) ? null : this.mItems[index]);
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();
            this.ClearItems();
            return;
        }

        public void OnDrag(PointerEventData eventData)
        {
            if (this.mOpened == null)
            {
                goto Label_0012;
            }
            this.SelectNearestItem(eventData);
        Label_0012:
            return;
        }

        public unsafe void OnEndDrag(PointerEventData eventData)
        {
            Vector2 vector;
            if (this.mOpened == null)
            {
                goto Label_0042;
            }
            vector = eventData.get_pressPosition() - eventData.get_position();
            if (&vector.get_magnitude() <= 5f)
            {
                goto Label_0042;
            }
            this.SelectNearestItem(eventData);
            this.ClosePulldown(0);
            this.TriggerItemChange();
        Label_0042:
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

        public override void OnPointerUp(PointerEventData eventData)
        {
            base.OnPointerUp(eventData);
            this.mAutoClose = 1;
            return;
        }

        private void OnPulldownMenuTouch(BaseEventData eventData)
        {
            PointerEventData data;
            data = eventData as PointerEventData;
            this.SelectNearestItem(data);
            this.ClosePulldown(0);
            this.TriggerItemChange();
            return;
        }

        private void OpenPulldown()
        {
            if (this.mOpened != null)
            {
                goto Label_001C;
            }
            if (this.mItems.Count > 1)
            {
                goto Label_001D;
            }
        Label_001C:
            return;
        Label_001D:
            this.PulldownMenu.get_gameObject().SetActive(1);
            this.mAutoClose = 0;
            this.mOpened = 1;
            this.mPollMouseUp = 0;
            this.mTrackTouchPosititon = 0;
            if (string.IsNullOrEmpty(this.OpenSE) != null)
            {
                goto Label_006F;
            }
            MonoSingleton<MySound>.Instance.PlaySEOneShot(this.OpenSE, 0f);
        Label_006F:
            return;
        }

        private unsafe void SelectNearestItem(PointerEventData e)
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
            transform = this.mItems[num3].get_transform() as RectTransform;
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
            if (num3 < this.mItems.Count)
            {
                goto Label_0017;
            }
            if (num2 < 0)
            {
                goto Label_00F4;
            }
            if (num2 == this.Selection)
            {
                goto Label_00ED;
            }
            this.mTrackTouchPosititon = 1;
        Label_00ED:
            this.Selection = num2;
        Label_00F4:
            return;
        }

        protected virtual PulldownItem SetupPulldownItem(GameObject itemObject)
        {
            return itemObject.AddComponent<PulldownItem>();
        }

        protected override void Start()
        {
            EventTrigger.TriggerEvent event2;
            EventTrigger.Entry entry;
            EventTrigger trigger;
            base.Start();
            if ((this.PulldownItemTemplate != null) == null)
            {
                goto Label_0028;
            }
            this.PulldownItemTemplate.get_gameObject().SetActive(0);
        Label_0028:
            if ((this.PulldownMenu != null) == null)
            {
                goto Label_004A;
            }
            this.PulldownMenu.get_gameObject().SetActive(0);
        Label_004A:
            event2 = new EventTrigger.TriggerEvent();
            event2.AddListener(new UnityAction<BaseEventData>(this, this.OnPulldownMenuTouch));
            entry = new EventTrigger.Entry();
            entry.eventID = 2;
            entry.callback = event2;
            trigger = SRPG_Extensions.RequireComponent<EventTrigger>(this.PulldownMenu.get_gameObject());
            trigger.set_triggers(new List<EventTrigger.Entry>());
            trigger.get_triggers().Add(entry);
            return;
        }

        private void TriggerItemChange()
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
            num = this.mItems[this.mSelectionIndex].Value;
            if (this.OnSelectionChange == null)
            {
                goto Label_0071;
            }
            this.OnSelectionChange.Invoke(num);
        Label_0071:
            if (this.OnSelectionChangeDelegate == null)
            {
                goto Label_0088;
            }
            this.OnSelectionChangeDelegate(num);
        Label_0088:
            return;
        }

        private void Update()
        {
            if (this.mAutoClose == null)
            {
                goto Label_003B;
            }
            if (Input.GetMouseButtonUp(0) == null)
            {
                goto Label_003B;
            }
            if (this.mPollMouseUp != null)
            {
                goto Label_002D;
            }
            this.mPollMouseUp = 1;
            goto Label_003B;
        Label_002D:
            this.mAutoClose = 0;
            this.ClosePulldown(1);
        Label_003B:
            return;
        }

        protected virtual void UpdateSelection()
        {
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
                if (value < this.mItems.Count)
                {
                    goto Label_0025;
                }
            Label_0024:
                return;
            Label_0025:
                this.mSelectionIndex = value;
                num = 0;
                goto Label_0077;
            Label_0033:
                if ((this.mItems[num].Overray != null) == null)
                {
                    goto Label_0073;
                }
                this.mItems[num].Overray.get_gameObject().SetActive(num == this.mSelectionIndex);
            Label_0073:
                num += 1;
            Label_0077:
                if (num < this.mItems.Count)
                {
                    goto Label_0033;
                }
                if ((this.mItems[this.mSelectionIndex].Text != null) == null)
                {
                    goto Label_00CF;
                }
                this.SelectionText.set_text(this.mItems[this.mSelectionIndex].Text.get_text());
            Label_00CF:
                if (this.OnUpdateSelection == null)
                {
                    goto Label_00EA;
                }
                this.OnUpdateSelection();
                goto Label_00F0;
            Label_00EA:
                this.UpdateSelection();
            Label_00F0:
                return;
            }
        }

        public int ItemCount
        {
            get
            {
                return this.mItems.Count;
            }
        }

        public delegate void SelectItemEvent(int value);

        public delegate PulldownItem SetupPulldownItemEvent(GameObject go);

        public delegate void UpdateSelectionEvent();
    }
}

