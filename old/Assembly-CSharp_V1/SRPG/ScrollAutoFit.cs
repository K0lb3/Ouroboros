// Decompiled with JetBrains decompiler
// Type: SRPG.ScrollAutoFit
// Assembly: Assembly-CSharp, Version=1.2.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9BA76916-D0BD-4DB6-A90B-FE0BCC53E511
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace SRPG
{
  [FlowNode.Pin(2, "有効化完了", FlowNode.PinTypes.Output, 2)]
  [FlowNode.Pin(3, "無効化完了", FlowNode.PinTypes.Output, 3)]
  [FlowNode.Pin(1, "無効化", FlowNode.PinTypes.Input, 1)]
  [FlowNode.Pin(0, "有効化", FlowNode.PinTypes.Input, 0)]
  public class ScrollAutoFit : ScrollRect, IFlowInterface
  {
    [SerializeField]
    [HideInInspector]
    public bool UseAutoFit;
    [HideInInspector]
    [SerializeField]
    public float FitTime;
    [HideInInspector]
    [SerializeField]
    public float ItemScale;
    [SerializeField]
    public bool IsHorizontal;
    private ScrollAutoFit.State mState;
    private float mStartPos;
    private float mEndPos;
    private float mScrollAnimTime;
    private bool isDragging;
    private RectTransform rectTransform;
    private bool mForceScroll;
    public ScrollAutoFit.ScrollStopEvent OnScrollStop;
    public ScrollAutoFit.ScrollBeginEvent OnScrollBegin;

    public ScrollAutoFit()
    {
      base.\u002Ector();
    }

    public Rect rect
    {
      get
      {
        return this.rectTransform.get_rect();
      }
    }

    protected virtual void Awake()
    {
      ((UIBehaviour) this).Awake();
      this.rectTransform = (RectTransform) ((Component) this).GetComponent<RectTransform>();
    }

    private void Update()
    {
      if (!this.UseAutoFit)
        return;
      switch (this.mState)
      {
        case ScrollAutoFit.State.DragEnd:
          if (!this.isDragging && (double) Mathf.Abs(!this.IsHorizontal ? (float) this.get_velocity().y : (float) this.get_velocity().x) < (double) this.ItemScale)
          {
            this.SetScrollTo((float) Mathf.RoundToInt((!this.IsHorizontal ? (float) this.get_content().get_anchoredPosition().y : (float) this.get_content().get_anchoredPosition().x) / this.ItemScale) * this.ItemScale);
            break;
          }
          break;
        case ScrollAutoFit.State.Scrolling:
          if ((double) this.mScrollAnimTime >= 0.0)
          {
            this.mScrollAnimTime += Time.get_deltaTime();
            float num = (double) this.FitTime <= 0.0 ? 1f : Mathf.Sin((float) ((double) Mathf.Clamp01(this.mScrollAnimTime / this.FitTime) * 3.14159274101257 * 0.5));
            Vector2 anchoredPosition = this.get_content().get_anchoredPosition();
            if (this.IsHorizontal)
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

    public virtual void OnBeginDrag(PointerEventData eventData)
    {
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
      this.mStartPos = !this.IsHorizontal ? (float) this.get_content().get_anchoredPosition().y : (float) this.get_content().get_anchoredPosition().x;
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

    private enum State
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
