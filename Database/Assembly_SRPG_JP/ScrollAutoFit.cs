// Decompiled with JetBrains decompiler
// Type: SRPG.ScrollAutoFit
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 85BFDF7F-5712-4D45-9CD6-3465C703DFDF
// Assembly location: S:\Desktop\Assembly-CSharp.dll

using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace SRPG
{
  [FlowNode.Pin(1, "無効化", FlowNode.PinTypes.Input, 1)]
  [FlowNode.Pin(2, "有効化完了", FlowNode.PinTypes.Output, 2)]
  [FlowNode.Pin(0, "有効化", FlowNode.PinTypes.Input, 0)]
  [FlowNode.Pin(3, "無効化完了", FlowNode.PinTypes.Output, 3)]
  public class ScrollAutoFit : SRPG_ScrollRect, IFlowInterface
  {
    [SerializeField]
    [HideInInspector]
    public float FitTime = 0.2f;
    public ScrollAutoFit.ScrollStopEvent OnScrollStop = new ScrollAutoFit.ScrollStopEvent();
    public ScrollAutoFit.ScrollBeginEvent OnScrollBegin = new ScrollAutoFit.ScrollBeginEvent();
    [SerializeField]
    [HideInInspector]
    public bool UseAutoFit;
    [SerializeField]
    [HideInInspector]
    public float ItemScale;
    [SerializeField]
    [HideInInspector]
    public bool HorizontalMode;
    [SerializeField]
    public float Offset;
    [SerializeField]
    public bool UseMoveRange;
    private ScrollAutoFit.State mState;
    private float mStartPos;
    private float mEndPos;
    private float mScrollAnimTime;
    private bool isDragging;
    private RectTransform rectTransform;
    private int mStartIdx;
    private Vector2 mStartDragPos;
    private bool mForceScroll;

    public ScrollAutoFit.State CurrentState
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

    protected override void Awake()
    {
      base.Awake();
      this.rectTransform = (RectTransform) ((Component) this).GetComponent<RectTransform>();
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
        if (this.isDragging || this.mState != ScrollAutoFit.State.Wait)
          return true;
        Vector2 velocity = this.get_velocity();
        return (double) ((Vector2) @velocity).get_magnitude() > 0.100000001490116;
      }
    }

    private void Update()
    {
      if (!this.UseAutoFit)
        return;
      switch (this.mState)
      {
        case ScrollAutoFit.State.Wait:
          this.UpdateWait();
          break;
        case ScrollAutoFit.State.Dragging:
          this.MoveContentRange();
          break;
        case ScrollAutoFit.State.DragEnd:
          if (!this.isDragging)
          {
            if (this.HorizontalMode)
            {
              if ((double) Mathf.Abs((float) this.get_velocity().x) < (double) this.ItemScale)
              {
                if (this.UseMoveRange)
                {
                  this.SetScrollToHorizontal(this.GetNearIconPos());
                  break;
                }
                this.SetScrollToHorizontal((float) this.GetCurrent() * this.ItemScale + this.Offset);
                break;
              }
              if (this.UseMoveRange && this.CheckSetScrollPos())
              {
                this.MoveContentRange();
                this.mScrollAnimTime = -1f;
                this.mState = ScrollAutoFit.State.Wait;
                this.OnScrollStop.Invoke();
                this.StopMovement();
                double nearIconPos = (double) this.GetNearIconPos();
                break;
              }
              break;
            }
            if ((double) Mathf.Abs((float) this.get_velocity().y) < (double) this.ItemScale)
            {
              this.SetScrollTo((float) Mathf.RoundToInt((float) this.get_content().get_anchoredPosition().y / this.ItemScale) * this.ItemScale + this.Offset);
              break;
            }
            break;
          }
          break;
        case ScrollAutoFit.State.Scrolling:
          if ((double) this.mScrollAnimTime >= 0.0)
          {
            this.mScrollAnimTime += Time.get_deltaTime();
            float num = (double) this.FitTime <= 0.0 ? 1f : Mathf.Sin((float) ((double) Mathf.Clamp01(this.mScrollAnimTime / this.FitTime) * 3.14159274101257 * 0.5));
            Vector2 anchoredPosition = this.get_content().get_anchoredPosition();
            if (this.HorizontalMode)
              anchoredPosition.x = (__Null) (double) Mathf.Lerp(this.mStartPos, this.mEndPos, num);
            else
              anchoredPosition.y = (__Null) (double) Mathf.Lerp(this.mStartPos, this.mEndPos, num);
            this.get_content().set_anchoredPosition(anchoredPosition);
            if ((double) num >= 1.0)
            {
              this.mScrollAnimTime = -1f;
              this.mState = ScrollAutoFit.State.Wait;
              this.OnScrollStop.Invoke();
              this.StopMovement();
              break;
            }
            break;
          }
          break;
      }
      this.mForceScroll = false;
    }

    private void UpdateWait()
    {
      if (!this.IsScrollNow || this.isDragging || this.mForceScroll)
        return;
      this.OnScrollBegin.Invoke();
      if ((double) this.ItemScale == 0.0 || !this.UseAutoFit)
        return;
      this.mState = ScrollAutoFit.State.DragEnd;
    }

    public override void OnBeginDrag(PointerEventData eventData)
    {
      this.mStartIdx = this.GetCurrent();
      base.OnBeginDrag(eventData);
      this.isDragging = true;
      this.mState = ScrollAutoFit.State.Dragging;
      this.OnScrollBegin.Invoke();
    }

    public virtual void OnEndDrag(PointerEventData eventData)
    {
      base.OnEndDrag(eventData);
      this.isDragging = false;
      if (this.mForceScroll || (double) this.ItemScale == 0.0 || !this.UseAutoFit)
        return;
      this.mState = ScrollAutoFit.State.DragEnd;
    }

    public void SetScrollTo(float pos)
    {
      this.mForceScroll = true;
      this.mStartPos = (float) this.get_content().get_anchoredPosition().y;
      this.mEndPos = pos;
      this.mScrollAnimTime = 0.0f;
      this.mState = ScrollAutoFit.State.Scrolling;
    }

    public void SetScrollToHorizontal(float pos)
    {
      this.mForceScroll = true;
      this.mStartPos = (float) this.get_content().get_anchoredPosition().x;
      this.mEndPos = pos;
      this.mScrollAnimTime = 0.0f;
      this.mState = ScrollAutoFit.State.Scrolling;
    }

    public void Activated(int pinID)
    {
      if (pinID == 0)
      {
        this.set_vertical(true);
        FlowNode_GameObject.ActivateOutputLinks((Component) this, 2);
      }
      if (pinID != 1)
        return;
      this.set_vertical(false);
      FlowNode_GameObject.ActivateOutputLinks((Component) this, 3);
    }

    public int GetCurrent()
    {
      if (this.HorizontalMode)
        return Mathf.RoundToInt(((float) this.get_content().get_anchoredPosition().x - this.Offset) / this.ItemScale);
      return Mathf.RoundToInt((float) this.get_content().get_anchoredPosition().y / this.ItemScale);
    }

    public void Step()
    {
      this.Update();
    }

    private void MoveContentRange()
    {
      if (!this.UseMoveRange || Object.op_Equality((Object) this.get_content(), (Object) null))
        return;
      ScrollContentsInfo component = (ScrollContentsInfo) ((Component) this.get_content()).GetComponent<ScrollContentsInfo>();
      if (Object.op_Equality((Object) component, (Object) null))
        return;
      this.get_content().set_anchoredPosition(component.SetRangePos(this.get_content().get_anchoredPosition()));
    }

    private bool CheckSetScrollPos()
    {
      if (!this.UseMoveRange || Object.op_Equality((Object) this.get_content(), (Object) null))
        return false;
      ScrollContentsInfo component = (ScrollContentsInfo) ((Component) this.get_content()).GetComponent<ScrollContentsInfo>();
      if (Object.op_Equality((Object) component, (Object) null))
        return false;
      return component.CheckRangePos((float) this.get_content().get_anchoredPosition().x);
    }

    private float GetNearIconPos()
    {
      float num = (float) this.GetCurrent() * this.ItemScale + this.Offset;
      if (!this.UseMoveRange || Object.op_Equality((Object) this.get_content(), (Object) null))
        return num;
      ScrollContentsInfo component = (ScrollContentsInfo) ((Component) this.get_content()).GetComponent<ScrollContentsInfo>();
      if (Object.op_Equality((Object) component, (Object) null))
        return num;
      return component.GetNearIconPos((float) this.get_content().get_anchoredPosition().x);
    }

    public enum State
    {
      Wait,
      Dragging,
      DragEnd,
      Scrolling,
    }

    [SerializeField]
    public class ScrollStopEvent : UnityEvent
    {
      public ScrollStopEvent()
      {
        base.\u002Ector();
      }
    }

    [SerializeField]
    public class ScrollBeginEvent : UnityEvent
    {
      public ScrollBeginEvent()
      {
        base.\u002Ector();
      }
    }
  }
}
