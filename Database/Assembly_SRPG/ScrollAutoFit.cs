namespace SRPG
{
    using System;
    using UnityEngine;
    using UnityEngine.Events;
    using UnityEngine.EventSystems;

    [Pin(1, "無効化", 0, 1), Pin(2, "有効化完了", 1, 2), Pin(0, "有効化", 0, 0), Pin(3, "無効化完了", 1, 3)]
    public class ScrollAutoFit : SRPG_ScrollRect, IFlowInterface
    {
        [SerializeField, HideInInspector]
        public bool UseAutoFit;
        [SerializeField, HideInInspector]
        public float FitTime;
        [SerializeField, HideInInspector]
        public float ItemScale;
        [SerializeField, HideInInspector]
        public bool HorizontalMode;
        [SerializeField]
        public float Offset;
        [SerializeField]
        public bool UseMoveRange;
        private State mState;
        private float mStartPos;
        private float mEndPos;
        private float mScrollAnimTime;
        private bool isDragging;
        private RectTransform rectTransform;
        private int mStartIdx;
        private Vector2 mStartDragPos;
        private bool mForceScroll;
        public ScrollStopEvent OnScrollStop;
        public ScrollBeginEvent OnScrollBegin;

        public ScrollAutoFit()
        {
            this.FitTime = 0.2f;
            this.OnScrollStop = new ScrollStopEvent();
            this.OnScrollBegin = new ScrollBeginEvent();
            base..ctor();
            return;
        }

        public void Activated(int pinID)
        {
            if (pinID != null)
            {
                goto Label_0014;
            }
            base.set_vertical(1);
            FlowNode_GameObject.ActivateOutputLinks(this, 2);
        Label_0014:
            if (pinID != 1)
            {
                goto Label_0029;
            }
            base.set_vertical(0);
            FlowNode_GameObject.ActivateOutputLinks(this, 3);
        Label_0029:
            return;
        }

        protected override void Awake()
        {
            base.Awake();
            this.rectTransform = base.GetComponent<RectTransform>();
            return;
        }

        private unsafe bool CheckSetScrollPos()
        {
            ScrollContentsInfo info;
            Vector2 vector;
            if (this.UseMoveRange == null)
            {
                goto Label_001C;
            }
            if ((base.get_content() == null) == null)
            {
                goto Label_001E;
            }
        Label_001C:
            return 0;
        Label_001E:
            info = base.get_content().GetComponent<ScrollContentsInfo>();
            if ((info == null) == null)
            {
                goto Label_0038;
            }
            return 0;
        Label_0038:
            return info.CheckRangePos(&base.get_content().get_anchoredPosition().x);
        }

        public unsafe int GetCurrent()
        {
            Vector2 vector;
            Vector2 vector2;
            if (this.HorizontalMode == null)
            {
                goto Label_0032;
            }
            return Mathf.RoundToInt((&base.get_content().get_anchoredPosition().x - this.Offset) / this.ItemScale);
        Label_0032:
            return Mathf.RoundToInt(&base.get_content().get_anchoredPosition().y / this.ItemScale);
        }

        private unsafe float GetNearIconPos()
        {
            float num;
            int num2;
            ScrollContentsInfo info;
            Vector2 vector;
            num = 0f;
            num = (((float) this.GetCurrent()) * this.ItemScale) + this.Offset;
            if (this.UseMoveRange == null)
            {
                goto Label_003A;
            }
            if ((base.get_content() == null) == null)
            {
                goto Label_003C;
            }
        Label_003A:
            return num;
        Label_003C:
            info = base.get_content().GetComponent<ScrollContentsInfo>();
            if ((info == null) == null)
            {
                goto Label_0056;
            }
            return num;
        Label_0056:
            return info.GetNearIconPos(&base.get_content().get_anchoredPosition().x);
        }

        private void MoveContentRange()
        {
            ScrollContentsInfo info;
            if (this.UseMoveRange == null)
            {
                goto Label_001C;
            }
            if ((base.get_content() == null) == null)
            {
                goto Label_001D;
            }
        Label_001C:
            return;
        Label_001D:
            info = base.get_content().GetComponent<ScrollContentsInfo>();
            if ((info == null) == null)
            {
                goto Label_0036;
            }
            return;
        Label_0036:
            base.get_content().set_anchoredPosition(info.SetRangePos(base.get_content().get_anchoredPosition()));
            return;
        }

        public override void OnBeginDrag(PointerEventData eventData)
        {
            this.mStartIdx = this.GetCurrent();
            base.OnBeginDrag(eventData);
            this.isDragging = 1;
            this.mState = 1;
            this.OnScrollBegin.Invoke();
            return;
        }

        public override void OnEndDrag(PointerEventData eventData)
        {
            base.OnEndDrag(eventData);
            this.isDragging = 0;
            if (this.mForceScroll == null)
            {
                goto Label_001A;
            }
            return;
        Label_001A:
            if (this.ItemScale == 0f)
            {
                goto Label_003C;
            }
            if (this.UseAutoFit == null)
            {
                goto Label_003C;
            }
            this.mState = 2;
        Label_003C:
            return;
        }

        public unsafe void SetScrollTo(float pos)
        {
            Vector2 vector;
            this.mForceScroll = 1;
            this.mStartPos = &base.get_content().get_anchoredPosition().y;
            this.mEndPos = pos;
            this.mScrollAnimTime = 0f;
            this.mState = 3;
            return;
        }

        public unsafe void SetScrollToHorizontal(float pos)
        {
            Vector2 vector;
            this.mForceScroll = 1;
            this.mStartPos = &base.get_content().get_anchoredPosition().x;
            this.mEndPos = pos;
            this.mScrollAnimTime = 0f;
            this.mState = 3;
            return;
        }

        public void Step()
        {
            this.Update();
            return;
        }

        private unsafe void Update()
        {
            float num;
            int num2;
            float num3;
            int num4;
            float num5;
            Vector2 vector;
            State state;
            Vector2 vector2;
            Vector2 vector3;
            Vector2 vector4;
            if (this.UseAutoFit != null)
            {
                goto Label_000C;
            }
            return;
        Label_000C:
            switch (this.mState)
            {
                case 0:
                    goto Label_0030;

                case 1:
                    goto Label_003B;

                case 2:
                    goto Label_0046;

                case 3:
                    goto Label_0163;
            }
            goto Label_025C;
        Label_0030:
            this.UpdateWait();
            goto Label_025C;
        Label_003B:
            this.MoveContentRange();
            goto Label_025C;
        Label_0046:
            if (this.isDragging != null)
            {
                goto Label_025C;
            }
            if (this.HorizontalMode == null)
            {
                goto Label_0106;
            }
            if (Mathf.Abs(&base.get_velocity().x) >= this.ItemScale)
            {
                goto Label_00BB;
            }
            if (this.UseMoveRange == null)
            {
                goto Label_0099;
            }
            this.SetScrollToHorizontal(this.GetNearIconPos());
            goto Label_00B6;
        Label_0099:
            num2 = this.GetCurrent();
            this.SetScrollToHorizontal((((float) num2) * this.ItemScale) + this.Offset);
        Label_00B6:
            goto Label_0101;
        Label_00BB:
            if (this.UseMoveRange == null)
            {
                goto Label_025C;
            }
            if (this.CheckSetScrollPos() == null)
            {
                goto Label_025C;
            }
            this.MoveContentRange();
            this.mScrollAnimTime = -1f;
            this.mState = 0;
            this.OnScrollStop.Invoke();
            this.StopMovement();
            this.GetNearIconPos();
        Label_0101:
            goto Label_015E;
        Label_0106:
            if (Mathf.Abs(&base.get_velocity().y) >= this.ItemScale)
            {
                goto Label_025C;
            }
            num4 = Mathf.RoundToInt(&base.get_content().get_anchoredPosition().y / this.ItemScale);
            this.SetScrollTo((((float) num4) * this.ItemScale) + this.Offset);
        Label_015E:
            goto Label_025C;
        Label_0163:
            if (this.mScrollAnimTime < 0f)
            {
                goto Label_025C;
            }
            this.mScrollAnimTime += Time.get_deltaTime();
            if (this.FitTime <= 0f)
            {
                goto Label_01C3;
            }
            num5 = Mathf.Sin((Mathf.Clamp01(this.mScrollAnimTime / this.FitTime) * 3.141593f) * 0.5f);
            goto Label_01CA;
        Label_01C3:
            num5 = 1f;
        Label_01CA:
            vector = base.get_content().get_anchoredPosition();
            if (this.HorizontalMode == null)
            {
                goto Label_0201;
            }
            &vector.x = Mathf.Lerp(this.mStartPos, this.mEndPos, num5);
            goto Label_021B;
        Label_0201:
            &vector.y = Mathf.Lerp(this.mStartPos, this.mEndPos, num5);
        Label_021B:
            base.get_content().set_anchoredPosition(vector);
            if (num5 < 1f)
            {
                goto Label_025C;
            }
            this.mScrollAnimTime = -1f;
            this.mState = 0;
            this.OnScrollStop.Invoke();
            this.StopMovement();
        Label_025C:
            this.mForceScroll = 0;
            return;
        }

        private void UpdateWait()
        {
            if (base.IsScrollNow == null)
            {
                goto Label_004E;
            }
            if (this.isDragging != null)
            {
                goto Label_004E;
            }
            if (this.mForceScroll != null)
            {
                goto Label_004E;
            }
            this.OnScrollBegin.Invoke();
            if (this.ItemScale == 0f)
            {
                goto Label_004E;
            }
            if (this.UseAutoFit == null)
            {
                goto Label_004E;
            }
            this.mState = 2;
        Label_004E:
            return;
        }

        public State CurrentState
        {
            get
            {
                return this.mState;
            }
        }

        public Rect rect
        {
            get
            {
                return this.rectTransform.get_rect();
            }
        }

        public int DragStartIdx
        {
            get
            {
                return this.mStartIdx;
            }
            set
            {
                this.mStartIdx = value;
                return;
            }
        }

        public Vector2 DragStartPos
        {
            get
            {
                return this.mStartDragPos;
            }
            set
            {
                this.mStartDragPos = value;
                return;
            }
        }

        public bool IsDrag
        {
            get
            {
                return this.isDragging;
            }
        }

        public bool IsMove
        {
            get
            {
                Vector2 vector;
                return (((this.isDragging != null) || (this.mState != null)) ? 1 : (&base.get_velocity().get_magnitude() > 0.1f));
            }
        }

        [SerializeField]
        public class ScrollBeginEvent : UnityEvent
        {
            public ScrollBeginEvent()
            {
                base..ctor();
                return;
            }
        }

        [SerializeField]
        public class ScrollStopEvent : UnityEvent
        {
            public ScrollStopEvent()
            {
                base..ctor();
                return;
            }
        }

        public enum State
        {
            Wait,
            Dragging,
            DragEnd,
            Scrolling
        }
    }
}

