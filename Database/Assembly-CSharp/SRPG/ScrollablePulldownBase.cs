// Decompiled with JetBrains decompiler
// Type: SRPG.ScrollablePulldownBase
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using GR;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace SRPG
{
  public abstract class ScrollablePulldownBase : Selectable, IBeginDragHandler, IDragHandler, IEndDragHandler, IEventSystemHandler
  {
    public ScrollablePulldownBase.SelectItemEvent OnSelectionChangeDelegate;
    [SerializeField]
    protected List<PulldownItem> Items;
    [SerializeField]
    protected RectTransform ItemHolder;
    [SerializeField]
    protected ScrollRect ScrollRect;
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
      base.\u002Ector();
    }

    protected virtual void Start()
    {
      ((UIBehaviour) this).Start();
      if (!Object.op_Inequality((Object) this.BackGround, (Object) null))
        return;
      this.BackGround.get_gameObject().SetActive(false);
    }

    protected virtual void OnDestroy()
    {
      ((UIBehaviour) this).OnDestroy();
      if (Object.op_Inequality((Object) this.SelectionText, (Object) null) && !string.IsNullOrEmpty(this.SelectionText.get_text()))
        this.SelectionText.set_text(string.Empty);
      this.mSelectionIndex = -1;
    }

    protected void ResetAllStatus()
    {
      this.mSelectionIndex = -1;
    }

    public int Selection
    {
      get
      {
        return this.mSelectionIndex;
      }
      set
      {
        if (this.mSelectionIndex == value || value < 0 || value >= this.Items.Count)
          return;
        this.mSelectionIndex = value;
        for (int index = 0; index < this.Items.Count; ++index)
          this.Items[index].OnStatusChanged(index == this.mSelectionIndex);
        if (!Object.op_Inequality((Object) this.Items[this.mSelectionIndex].Text, (Object) null))
          return;
        this.SelectionText.set_text(this.Items[this.mSelectionIndex].Text.get_text());
      }
    }

    public int PrevSelection
    {
      set
      {
        this.mPrevSelectionIndex = value;
      }
    }

    private bool SelectNearestItem(PointerEventData e)
    {
      Vector2 position = e.get_position();
      float num1 = float.MaxValue;
      int num2 = -1;
      for (int index = 0; index < this.Items.Count; ++index)
      {
        RectTransform transform = ((Component) this.Items[index]).get_transform() as RectTransform;
        Vector2 vector2;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(transform, position, (Camera) null, ref vector2);
        if (this.mTrackTouchPosititon)
        {
          // ISSUE: explicit reference operation
          float magnitude = ((Vector2) @vector2).get_magnitude();
          if ((double) magnitude < (double) num1)
          {
            num2 = index;
            num1 = magnitude;
          }
        }
        else
        {
          Rect rect = transform.get_rect();
          // ISSUE: explicit reference operation
          // ISSUE: explicit reference operation
          // ISSUE: explicit reference operation
          // ISSUE: explicit reference operation
          if ((double) ((Rect) @rect).get_xMin() <= vector2.x && vector2.x < (double) ((Rect) @rect).get_xMax() && ((double) ((Rect) @rect).get_yMin() <= vector2.y && vector2.y < (double) ((Rect) @rect).get_yMax()))
            num2 = index;
        }
      }
      if (num2 < 0)
        return false;
      if (num2 != this.Selection)
        this.mTrackTouchPosititon = true;
      this.Selection = num2;
      return true;
    }

    protected void TriggerItemChange()
    {
      if (!string.IsNullOrEmpty(this.SelectSE))
        MonoSingleton<MySound>.Instance.PlaySEOneShot(this.SelectSE, 0.0f);
      if (this.mPrevSelectionIndex == this.mSelectionIndex)
        return;
      this.mPrevSelectionIndex = this.mSelectionIndex;
      int num = this.Items[this.mSelectionIndex].Value;
      if (this.OnSelectionChangeDelegate == null)
        return;
      this.OnSelectionChangeDelegate(num);
    }

    private void OnPulldownMenuTouch(BaseEventData eventData)
    {
      if (this.SelectNearestItem(eventData as PointerEventData))
      {
        this.ClosePulldown(false);
        this.TriggerItemChange();
      }
      else
        this.ClosePulldown(true);
    }

    public virtual void OnPointerDown(PointerEventData eventData)
    {
      base.OnPointerDown(eventData);
      if (!this.IsInteractable())
        return;
      if (this.mOpened)
        this.ClosePulldown(true);
      else
        this.OpenPulldown();
    }

    public void OpenPulldown()
    {
      if (this.mOpened || this.Items.Count <= 1)
        return;
      this.BackGround.SetActive(true);
      this.mOpened = true;
      this.mTrackTouchPosititon = false;
      if (string.IsNullOrEmpty(this.OpenSE))
        return;
      MonoSingleton<MySound>.Instance.PlaySEOneShot(this.OpenSE, 0.0f);
    }

    public void ClosePulldown(bool se = true)
    {
      if (!this.mOpened)
        return;
      this.ScrollRect.set_verticalNormalizedPosition(1f);
      this.ScrollRect.set_horizontalNormalizedPosition(1f);
      this.BackGround.SetActive(false);
      this.mOpened = false;
      if (!se || string.IsNullOrEmpty(this.CloseSE))
        return;
      MonoSingleton<MySound>.Instance.PlaySEOneShot(this.CloseSE, 0.0f);
    }

    public void OnDrag(PointerEventData eventData)
    {
      if (!this.mOpened)
        return;
      this.SelectNearestItem(eventData);
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
    }

    public void OnEndDrag(PointerEventData eventData)
    {
      if (!this.mOpened)
        return;
      Vector2 vector2 = Vector2.op_Subtraction(eventData.get_pressPosition(), eventData.get_position());
      // ISSUE: explicit reference operation
      if ((double) ((Vector2) @vector2).get_magnitude() <= 5.0)
        return;
      this.SelectNearestItem(eventData);
      this.ClosePulldown(false);
      this.TriggerItemChange();
    }

    public PulldownItem GetItemAt(int index)
    {
      if (0 <= index && index < this.Items.Count)
        return this.Items[index];
      return (PulldownItem) null;
    }

    public int ItemCount
    {
      get
      {
        return this.Items.Count;
      }
    }

    public PulldownItem GetCurrentSelection()
    {
      return this.GetItemAt(this.mSelectionIndex);
    }

    public delegate void SelectItemEvent(int value);
  }
}
