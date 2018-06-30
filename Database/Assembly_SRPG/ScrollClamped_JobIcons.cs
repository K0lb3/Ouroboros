namespace SRPG
{
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;
    using UnityEngine;
    using UnityEngine.Events;
    using UnityEngine.UI;

    public class ScrollClamped_JobIcons : MonoBehaviour, ScrollListSetUp
    {
        private float FRAME_OUT_POSITION_VALUE;
        private float JOB_ICON_DISP_EFFECT_TIME;
        private float MIN_SCALE_DOUBLE;
        private float MIN_SCALE_SINGLE;
        private float VELOCITY_MAX;
        private float AUTOFIT_BEGIN_VELOCITY;
        public float Space;
        public int Max;
        public RectTransform ViewObj;
        public ScrollAutoFit AutoFit;
        public Button back;
        private JobIconScrollListController mController;
        private float mOffset;
        private float mStartPos;
        private float mCenter;
        private int mSelectIdx;
        private bool mIsSelected;
        private bool mIsImmediateFocusNow;
        private float mDefaultAutoFitTime;
        private WindowController mWindowController;
        private bool mIsNeedAutoFit;
        private bool mIsPreDragging;
        private bool mIsFocusNow;
        private Vector2 mDragStartLocalPosition;
        private Vector3 mDefaultViewportLocalPosition;
        private float mJobIconDispEffectTime;
        public FrameOutItem OnFrameOutItem;

        public ScrollClamped_JobIcons()
        {
            this.FRAME_OUT_POSITION_VALUE = 100000f;
            this.JOB_ICON_DISP_EFFECT_TIME = 0.1f;
            this.MIN_SCALE_DOUBLE = 0.65f;
            this.MIN_SCALE_SINGLE = 0.75f;
            this.VELOCITY_MAX = 800f;
            this.AUTOFIT_BEGIN_VELOCITY = 200f;
            this.Space = 1f;
            base..ctor();
            return;
        }

        private unsafe void CheckNeedExecAutoFocus()
        {
            float num;
            List<GameObject> list;
            UnitInventoryJobIcon[] iconArray;
            int num2;
            GameObject obj2;
            UnitInventoryJobIcon icon;
            Vector2 vector;
            Vector2 vector2;
            num = Mathf.Clamp(&this.AutoFit.get_velocity().x, -this.VELOCITY_MAX, this.VELOCITY_MAX);
            this.AutoFit.set_velocity(new Vector2(num, 0f));
            if (Mathf.Abs(&this.AutoFit.get_velocity().x) > this.AUTOFIT_BEGIN_VELOCITY)
            {
                goto Label_00EC;
            }
            list = new List<GameObject>();
            iconArray = base.GetComponentsInChildren<UnitInventoryJobIcon>();
            num2 = 0;
            goto Label_0087;
        Label_0075:
            list.Add(iconArray[num2].get_gameObject());
            num2 += 1;
        Label_0087:
            if (num2 < ((int) iconArray.Length))
            {
                goto Label_0075;
            }
            obj2 = this.Focus(list, 0, 0, 0.1f);
            if ((obj2 != null) == null)
            {
                goto Label_00E5;
            }
            icon = obj2.GetComponent<UnitInventoryJobIcon>();
            if ((icon != null) == null)
            {
                goto Label_00E5;
            }
            if (icon.IsDisabledBaseJobIcon() != null)
            {
                goto Label_00E5;
            }
            UnitEnhanceV3.Instance.OnJobSlotClick(icon.BaseJobIconButton.get_gameObject());
        Label_00E5:
            this.IsNeedAutoFit = 0;
        Label_00EC:
            return;
        }

        public unsafe GameObject Focus(List<GameObject> objects, bool is_immediate, bool is_hide, float focus_time)
        {
            RectTransform transform;
            float num;
            float num2;
            GameObject obj2;
            int num3;
            RectTransform transform2;
            float num4;
            Rect rect;
            Vector2 vector;
            Vector2 vector2;
            if (objects.Count > 0)
            {
                goto Label_000E;
            }
            return null;
        Label_000E:
            transform = base.GetComponent<RectTransform>();
            num = &this.ViewObj.get_rect().get_width() * 0.5f;
            this.mCenter = num - &transform.get_anchoredPosition().x;
            num2 = 3.402823E+38f;
            obj2 = objects[0];
            num3 = 0;
            goto Label_00A9;
        Label_005D:
            transform2 = objects[num3].get_transform() as RectTransform;
            num4 = Mathf.Abs(this.mCenter - &transform2.get_anchoredPosition().x);
            if (num2 < num4)
            {
                goto Label_00A3;
            }
            num2 = num4;
            obj2 = objects[num3];
        Label_00A3:
            num3 += 1;
        Label_00A9:
            if (num3 < objects.Count)
            {
                goto Label_005D;
            }
            this.Focus(obj2, is_immediate, is_hide, focus_time);
            return obj2;
        }

        public unsafe void Focus(GameObject obj, bool is_immediate, bool is_hide, float focus_time)
        {
            float num;
            RectTransform transform;
            int num2;
            float num3;
            <Focus>c__AnonStorey39D storeyd;
            Vector2 vector;
            storeyd = new <Focus>c__AnonStorey39D();
            storeyd.obj = obj;
            if ((this.AutoFit == null) == null)
            {
                goto Label_0021;
            }
            return;
        Label_0021:
            num = this.mDefaultAutoFitTime;
            if (focus_time <= 0f)
            {
                goto Label_0037;
            }
            num = focus_time;
        Label_0037:
            if (is_immediate == null)
            {
                goto Label_00AC;
            }
            num = 0f;
            if (is_hide == null)
            {
                goto Label_00AC;
            }
            this.mIsImmediateFocusNow = 1;
            if ((this.mDefaultViewportLocalPosition == Vector3.get_zero()) == null)
            {
                goto Label_007B;
            }
            this.mDefaultViewportLocalPosition = base.get_transform().get_parent().get_localPosition();
        Label_007B:
            base.get_transform().get_parent().set_localPosition(new Vector3(0f, this.FRAME_OUT_POSITION_VALUE, 0f));
            this.mJobIconDispEffectTime = this.JOB_ICON_DISP_EFFECT_TIME;
        Label_00AC:
            transform = base.get_gameObject().GetComponent<RectTransform>();
            num2 = this.mController.Items.FindIndex(new Predicate<JobIconScrollListController.ItemData>(storeyd.<>m__3F8));
            if (num2 == -1)
            {
                goto Label_015F;
            }
            this.AutoFit.FitTime = num;
            num3 = &transform.get_anchoredPosition().x - (&this.mController.Items[num2].position.x - this.mCenter);
            this.AutoFit.SetScrollToHorizontal(num3);
            if (is_immediate == null)
            {
                goto Label_013B;
            }
            this.AutoFit.Step();
        Label_013B:
            if ((this.back != null) == null)
            {
                goto Label_0158;
            }
            this.back.set_interactable(0);
        Label_0158:
            this.mIsFocusNow = 1;
        Label_015F:
            return;
        }

        private void ImmediateFocus()
        {
            if (this.AutoFit.CurrentState == null)
            {
                goto Label_0011;
            }
            return;
        Label_0011:
            this.mJobIconDispEffectTime -= Time.get_deltaTime();
            if (this.mJobIconDispEffectTime > 0f)
            {
                goto Label_0057;
            }
            this.mIsImmediateFocusNow = 0;
            base.get_transform().get_parent().set_localPosition(this.mDefaultViewportLocalPosition);
            this.IsNeedAutoFit = 0;
        Label_0057:
            return;
        }

        public void OnClick(GameObject obj)
        {
            this.Focus(obj, 0, 0, 0f);
            return;
        }

        public void OnItemPositionAreaOver(GameObject obj)
        {
            this.AutoFit.set_velocity(Vector2.get_zero());
            return;
        }

        private void OnScrollStop()
        {
            if ((this.WinController != null) == null)
            {
                goto Label_001D;
            }
            this.WinController.SetCollision(1);
        Label_001D:
            return;
        }

        public void OnSetUpItems()
        {
            this.mController = base.GetComponent<JobIconScrollListController>();
            this.mController.OnItemUpdate.AddListener(new UnityAction<int, GameObject>(this, this.OnUpdateItems));
            this.mController.OnUpdateItemEvent.AddListener(new UnityAction<List<JobIconScrollListController.ItemData>>(this, this.OnUpdateScale));
            return;
        }

        public unsafe void OnUpdateItems(int idx, GameObject obj)
        {
            obj.set_name(&idx.ToString());
            if (this.OnFrameOutItem == null)
            {
                goto Label_0025;
            }
            this.OnFrameOutItem(obj, idx);
        Label_0025:
            return;
        }

        public unsafe void OnUpdateScale(List<JobIconScrollListController.ItemData> items)
        {
            RectTransform transform;
            float num;
            int num2;
            float num3;
            float num4;
            float num5;
            float num6;
            float num7;
            float num8;
            Rect rect;
            Vector2 vector;
            Vector2 vector2;
            transform = base.GetComponent<RectTransform>();
            num = &this.ViewObj.get_rect().get_width() * 0.5f;
            this.mCenter = num - &transform.get_anchoredPosition().x;
            num2 = 0;
            goto Label_018E;
        Label_0040:
            num3 = (items[num2].job_icon.IsSingleIcon == null) ? this.MIN_SCALE_DOUBLE : this.MIN_SCALE_SINGLE;
            num4 = (&items[num2].position.x + &transform.get_anchoredPosition().x) - num;
            num5 = Mathf.Abs(num4);
            num6 = Mathf.Clamp(1f - (num5 / num), 0f, 1f);
            num7 = num3 + ((1f - num3) * num6);
            items[num2].gameObject.get_transform().set_localScale(new Vector3(num7, num7, num7));
            num8 = items[num2].job_icon.HalfWidth * (1f - num7);
            if (num4 <= 0f)
            {
                goto Label_0143;
            }
            items[num2].rectTransform.set_anchoredPosition(new Vector2(&items[num2].position.x - num8, &items[num2].position.y));
        Label_0143:
            if (num4 >= 0f)
            {
                goto Label_018A;
            }
            items[num2].rectTransform.set_anchoredPosition(new Vector2(&items[num2].position.x + num8, &items[num2].position.y));
        Label_018A:
            num2 += 1;
        Label_018E:
            if (num2 < items.Count)
            {
                goto Label_0040;
            }
            return;
        }

        public unsafe void Start()
        {
            RectTransform transform;
            float num;
            Rect rect;
            Vector2 vector;
            this.mDefaultAutoFitTime = this.AutoFit.FitTime;
            transform = base.GetComponent<RectTransform>();
            num = &this.ViewObj.get_rect().get_width() * 0.5f;
            this.mCenter = num - &transform.get_anchoredPosition().x;
            this.AutoFit.OnScrollStop.AddListener(new UnityAction(this, this.OnScrollStop));
            this.mController.OnItemPositionAreaOver.AddListener(new UnityAction<GameObject>(this, this.OnItemPositionAreaOver));
            return;
        }

        private void Update()
        {
            this.UpdateIsNeedAutoFitFlagByDrag();
            this.UpdateIsNeedAutoFitFlagByWheel();
            if (this.AutoFit.CurrentState != null)
            {
                goto Label_0023;
            }
            this.mIsFocusNow = 0;
        Label_0023:
            if (this.mIsImmediateFocusNow == null)
            {
                goto Label_0035;
            }
            this.ImmediateFocus();
            return;
        Label_0035:
            if (this.IsNeedAutoFit == null)
            {
                goto Label_0047;
            }
            this.CheckNeedExecAutoFocus();
            return;
        Label_0047:
            return;
        }

        private unsafe void UpdateIsNeedAutoFitFlagByDrag()
        {
            float num;
            Vector3 vector;
            if (this.mIsPreDragging != null)
            {
                goto Label_0043;
            }
            if (this.AutoFit.IsDrag == null)
            {
                goto Label_0043;
            }
            this.mDragStartLocalPosition = base.get_transform().get_localPosition();
            this.mIsPreDragging = this.AutoFit.IsDrag;
            return;
        Label_0043:
            if (this.mIsPreDragging == null)
            {
                goto Label_00B1;
            }
            if (this.AutoFit.IsDrag != null)
            {
                goto Label_00B1;
            }
            if (this.mIsFocusNow != null)
            {
                goto Label_00A0;
            }
            if (Mathf.Abs(&this.mDragStartLocalPosition.x - &base.get_transform().get_localPosition().x) < 1f)
            {
                goto Label_00A0;
            }
            this.IsNeedAutoFit = 1;
        Label_00A0:
            this.mIsPreDragging = this.AutoFit.IsDrag;
        Label_00B1:
            return;
        }

        private void UpdateIsNeedAutoFitFlagByWheel()
        {
            if (this.AutoFit.IsWheelScrollNow != null)
            {
                goto Label_0011;
            }
            return;
        Label_0011:
            this.IsNeedAutoFit = 1;
            return;
        }

        private WindowController WinController
        {
            get
            {
                if ((this.mWindowController == null) == null)
                {
                    goto Label_001D;
                }
                this.mWindowController = base.GetComponentInParent<WindowController>();
            Label_001D:
                return this.mWindowController;
            }
        }

        private bool IsNeedAutoFit
        {
            get
            {
                return this.mIsNeedAutoFit;
            }
            set
            {
                this.mIsNeedAutoFit = value;
                if (this.mIsNeedAutoFit == null)
                {
                    goto Label_002F;
                }
                if ((this.WinController != null) == null)
                {
                    goto Label_002F;
                }
                this.WinController.SetCollision(0);
            Label_002F:
                return;
            }
        }

        [CompilerGenerated]
        private sealed class <Focus>c__AnonStorey39D
        {
            internal GameObject obj;

            public <Focus>c__AnonStorey39D()
            {
                base..ctor();
                return;
            }

            internal bool <>m__3F8(JobIconScrollListController.ItemData data)
            {
                return (data.gameObject.GetInstanceID() == this.obj.GetInstanceID());
            }
        }

        public delegate void FrameOutItem(GameObject target, int index);
    }
}

