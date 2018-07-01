// Decompiled with JetBrains decompiler
// Type: SRPG.Pulldown
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using GR;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace SRPG
{
  public class Pulldown : Selectable, IBeginDragHandler, IDragHandler, IEndDragHandler, IEventSystemHandler
  {
    public Pulldown.SetupPulldownItemEvent OnSetupPulldownItem;
    public Pulldown.UpdateSelectionEvent OnUpdateSelection;
    public RectTransform PulldownMenu;
    public Text SelectionText;
    public GameObject PulldownItemTemplate;
    public Text PulldownText;
    public Graphic PulldownGraphic;
    public string OpenSE;
    public string CloseSE;
    public string SelectSE;
    public Pulldown.SelectItemEvent OnSelectionChangeDelegate;
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
      base.\u002Ector();
    }

    public int Selection
    {
      get
      {
        return this.mSelectionIndex;
      }
      set
      {
        if (this.mSelectionIndex == value || value < 0 || value >= this.mItems.Count)
          return;
        this.mSelectionIndex = value;
        for (int index = 0; index < this.mItems.Count; ++index)
        {
          if (Object.op_Inequality((Object) this.mItems[index].Overray, (Object) null))
            ((Component) this.mItems[index].Overray).get_gameObject().SetActive(index == this.mSelectionIndex);
        }
        if (Object.op_Inequality((Object) this.mItems[this.mSelectionIndex].Text, (Object) null))
          this.SelectionText.set_text(this.mItems[this.mSelectionIndex].Text.get_text());
        if (this.OnUpdateSelection != null)
          this.OnUpdateSelection();
        else
          this.UpdateSelection();
      }
    }

    protected virtual void Awake()
    {
      base.Awake();
      if (!Object.op_Equality((Object) this.PulldownItemTemplate, (Object) null) || !Object.op_Inequality((Object) this.PulldownText, (Object) null))
        return;
      this.PulldownItemTemplate = ((Component) this.PulldownText).get_gameObject();
    }

    protected virtual void Start()
    {
      ((UIBehaviour) this).Start();
      if (Object.op_Inequality((Object) this.PulldownItemTemplate, (Object) null))
        this.PulldownItemTemplate.get_gameObject().SetActive(false);
      if (Object.op_Inequality((Object) this.PulldownMenu, (Object) null))
        ((Component) this.PulldownMenu).get_gameObject().SetActive(false);
      EventTrigger.TriggerEvent triggerEvent = new EventTrigger.TriggerEvent();
      // ISSUE: method pointer
      ((UnityEvent<BaseEventData>) triggerEvent).AddListener(new UnityAction<BaseEventData>((object) this, __methodptr(OnPulldownMenuTouch)));
      EventTrigger.Entry entry = new EventTrigger.Entry();
      entry.eventID = (__Null) 2;
      entry.callback = (__Null) triggerEvent;
      EventTrigger eventTrigger = ((Component) this.PulldownMenu).get_gameObject().RequireComponent<EventTrigger>();
      eventTrigger.set_triggers(new List<EventTrigger.Entry>());
      eventTrigger.get_triggers().Add(entry);
    }

    protected virtual void OnDestroy()
    {
      ((UIBehaviour) this).OnDestroy();
      this.ClearItems();
    }

    private void OnPulldownMenuTouch(BaseEventData eventData)
    {
      this.SelectNearestItem(eventData as PointerEventData);
      this.ClosePulldown(false);
      this.TriggerItemChange();
    }

    protected virtual PulldownItem SetupPulldownItem(GameObject itemObject)
    {
      return (PulldownItem) itemObject.AddComponent<PulldownItem>();
    }

    public virtual PulldownItem AddItem(string label, int value)
    {
      if (Object.op_Equality((Object) this.PulldownItemTemplate, (Object) null))
        return (PulldownItem) null;
      if (!this.mPulldownItemInitialized)
      {
        this.mPulldownItemInitialized = true;
        PulldownItem pulldownItem = this.OnSetupPulldownItem == null ? this.SetupPulldownItem(this.PulldownItemTemplate) : this.OnSetupPulldownItem(this.PulldownItemTemplate);
        pulldownItem.Text = this.PulldownText;
        pulldownItem.Graphic = this.PulldownGraphic;
      }
      GameObject gameObject = (GameObject) Object.Instantiate<GameObject>((M0) this.PulldownItemTemplate);
      PulldownItem component = (PulldownItem) gameObject.GetComponent<PulldownItem>();
      if (Object.op_Inequality((Object) component.Text, (Object) null))
        component.Text.set_text(label);
      component.Value = value;
      this.mItems.Add(component);
      gameObject.get_transform().SetParent((Transform) this.PulldownMenu, false);
      gameObject.SetActive(true);
      return component;
    }

    private void TriggerItemChange()
    {
      if (!string.IsNullOrEmpty(this.SelectSE))
        MonoSingleton<MySound>.Instance.PlaySEOneShot(this.SelectSE, 0.0f);
      if (this.mPrevSelectionIndex == this.mSelectionIndex)
        return;
      this.mPrevSelectionIndex = this.mSelectionIndex;
      int num = this.mItems[this.mSelectionIndex].Value;
      if (this.OnSelectionChange != null)
        this.OnSelectionChange.Invoke(num);
      if (this.OnSelectionChangeDelegate == null)
        return;
      this.OnSelectionChangeDelegate(num);
    }

    public void ClearItems()
    {
      for (int index = 0; index < this.mItems.Count; ++index)
        Object.Destroy((Object) ((Component) this.mItems[index]).get_gameObject());
      this.mItems.Clear();
      if (Object.op_Inequality((Object) this.SelectionText, (Object) null) && !string.IsNullOrEmpty(this.SelectionText.get_text()))
        this.SelectionText.set_text(string.Empty);
      this.mSelectionIndex = -1;
    }

    private void SelectNearestItem(PointerEventData e)
    {
      Vector2 position = e.get_position();
      float num1 = float.MaxValue;
      int num2 = -1;
      for (int index = 0; index < this.mItems.Count; ++index)
      {
        RectTransform transform = ((Component) this.mItems[index]).get_transform() as RectTransform;
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
        return;
      if (num2 != this.Selection)
        this.mTrackTouchPosititon = true;
      this.Selection = num2;
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

    private void OpenPulldown()
    {
      if (this.mOpened || this.mItems.Count <= 1)
        return;
      ((Component) this.PulldownMenu).get_gameObject().SetActive(true);
      this.mAutoClose = false;
      this.mOpened = true;
      this.mPollMouseUp = false;
      this.mTrackTouchPosititon = false;
      if (string.IsNullOrEmpty(this.OpenSE))
        return;
      MonoSingleton<MySound>.Instance.PlaySEOneShot(this.OpenSE, 0.0f);
    }

    private void ClosePulldown(bool se = true)
    {
      if (!this.mOpened)
        return;
      ((Component) this.PulldownMenu).get_gameObject().SetActive(false);
      this.mAutoClose = false;
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

    public virtual void OnPointerUp(PointerEventData eventData)
    {
      base.OnPointerUp(eventData);
      this.mAutoClose = true;
    }

    private void Update()
    {
      if (!this.mAutoClose || !Input.GetMouseButtonUp(0))
        return;
      if (!this.mPollMouseUp)
      {
        this.mPollMouseUp = true;
      }
      else
      {
        this.mAutoClose = false;
        this.ClosePulldown(true);
      }
    }

    protected virtual void UpdateSelection()
    {
    }

    public PulldownItem GetItemAt(int index)
    {
      if (0 <= index && index < this.mItems.Count)
        return this.mItems[index];
      return (PulldownItem) null;
    }

    public int ItemCount
    {
      get
      {
        return this.mItems.Count;
      }
    }

    public PulldownItem GetCurrentSelection()
    {
      return this.GetItemAt(this.mSelectionIndex);
    }

    public delegate PulldownItem SetupPulldownItemEvent(GameObject go);

    public delegate void UpdateSelectionEvent();

    public delegate void SelectItemEvent(int value);
  }
}
