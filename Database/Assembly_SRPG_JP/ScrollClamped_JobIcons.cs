// Decompiled with JetBrains decompiler
// Type: SRPG.ScrollClamped_JobIcons
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 85BFDF7F-5712-4D45-9CD6-3465C703DFDF
// Assembly location: S:\Desktop\Assembly-CSharp.dll

using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace SRPG
{
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
    public ScrollClamped_JobIcons.FrameOutItem OnFrameOutItem;

    public ScrollClamped_JobIcons()
    {
      base.\u002Ector();
    }

    private WindowController WinController
    {
      get
      {
        if (UnityEngine.Object.op_Equality((UnityEngine.Object) this.mWindowController, (UnityEngine.Object) null))
          this.mWindowController = (WindowController) ((Component) this).GetComponentInParent<WindowController>();
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
        if (!this.mIsNeedAutoFit || !UnityEngine.Object.op_Inequality((UnityEngine.Object) this.WinController, (UnityEngine.Object) null))
          return;
        this.WinController.SetCollision(false);
      }
    }

    public void Start()
    {
      this.mDefaultAutoFitTime = this.AutoFit.FitTime;
      RectTransform component = (RectTransform) ((Component) this).GetComponent<RectTransform>();
      Rect rect = this.ViewObj.get_rect();
      // ISSUE: explicit reference operation
      this.mCenter = ((Rect) @rect).get_width() * 0.5f - (float) component.get_anchoredPosition().x;
      // ISSUE: method pointer
      this.AutoFit.OnScrollStop.AddListener(new UnityAction((object) this, __methodptr(OnScrollStop)));
      // ISSUE: method pointer
      this.mController.OnItemPositionAreaOver.AddListener(new UnityAction<GameObject>((object) this, __methodptr(OnItemPositionAreaOver)));
    }

    private void Update()
    {
      this.UpdateIsNeedAutoFitFlagByDrag();
      this.UpdateIsNeedAutoFitFlagByWheel();
      if (this.AutoFit.CurrentState == ScrollAutoFit.State.Wait)
        this.mIsFocusNow = false;
      if (this.mIsImmediateFocusNow)
      {
        this.ImmediateFocus();
      }
      else
      {
        if (!this.IsNeedAutoFit)
          return;
        this.CheckNeedExecAutoFocus();
      }
    }

    private void CheckNeedExecAutoFocus()
    {
      this.AutoFit.set_velocity(new Vector2(Mathf.Clamp((float) this.AutoFit.get_velocity().x, -this.VELOCITY_MAX, this.VELOCITY_MAX), 0.0f));
      if ((double) Mathf.Abs((float) this.AutoFit.get_velocity().x) > (double) this.AUTOFIT_BEGIN_VELOCITY)
        return;
      List<GameObject> objects = new List<GameObject>();
      foreach (Component componentsInChild in (UnitInventoryJobIcon[]) ((Component) this).GetComponentsInChildren<UnitInventoryJobIcon>())
        objects.Add(componentsInChild.get_gameObject());
      GameObject gameObject = this.Focus(objects, false, false, 0.1f);
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) gameObject, (UnityEngine.Object) null))
      {
        UnitInventoryJobIcon component = (UnitInventoryJobIcon) gameObject.GetComponent<UnitInventoryJobIcon>();
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) component, (UnityEngine.Object) null) && !component.IsDisabledBaseJobIcon())
          UnitEnhanceV3.Instance.OnJobSlotClick(((Component) component.BaseJobIconButton).get_gameObject());
      }
      this.IsNeedAutoFit = false;
    }

    private void ImmediateFocus()
    {
      if (this.AutoFit.CurrentState != ScrollAutoFit.State.Wait)
        return;
      this.mJobIconDispEffectTime -= Time.get_deltaTime();
      if ((double) this.mJobIconDispEffectTime > 0.0)
        return;
      this.mIsImmediateFocusNow = false;
      ((Component) this).get_transform().get_parent().set_localPosition(this.mDefaultViewportLocalPosition);
      this.IsNeedAutoFit = false;
    }

    private void UpdateIsNeedAutoFitFlagByWheel()
    {
      if (!this.AutoFit.IsWheelScrollNow)
        return;
      this.IsNeedAutoFit = true;
    }

    private void UpdateIsNeedAutoFitFlagByDrag()
    {
      if (!this.mIsPreDragging && this.AutoFit.IsDrag)
      {
        this.mDragStartLocalPosition = Vector2.op_Implicit(((Component) this).get_transform().get_localPosition());
        this.mIsPreDragging = this.AutoFit.IsDrag;
      }
      else
      {
        if (!this.mIsPreDragging || this.AutoFit.IsDrag)
          return;
        if (!this.mIsFocusNow && (double) Mathf.Abs((float) (this.mDragStartLocalPosition.x - ((Component) this).get_transform().get_localPosition().x)) >= 1.0)
          this.IsNeedAutoFit = true;
        this.mIsPreDragging = this.AutoFit.IsDrag;
      }
    }

    private void OnScrollStop()
    {
      if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) this.WinController, (UnityEngine.Object) null))
        return;
      this.WinController.SetCollision(true);
    }

    public void OnSetUpItems()
    {
      this.mController = (JobIconScrollListController) ((Component) this).GetComponent<JobIconScrollListController>();
      JobIconScrollListController.OnItemPositionChange onItemUpdate = this.mController.OnItemUpdate;
      ScrollClamped_JobIcons scrollClampedJobIcons = this;
      // ISSUE: virtual method pointer
      UnityAction<int, GameObject> unityAction = new UnityAction<int, GameObject>((object) scrollClampedJobIcons, __vmethodptr(scrollClampedJobIcons, OnUpdateItems));
      onItemUpdate.AddListener(unityAction);
      // ISSUE: method pointer
      this.mController.OnUpdateItemEvent.AddListener(new UnityAction<List<JobIconScrollListController.ItemData>>((object) this, __methodptr(OnUpdateScale)));
    }

    public void OnUpdateItems(int idx, GameObject obj)
    {
      ((UnityEngine.Object) obj).set_name(idx.ToString());
      if (this.OnFrameOutItem == null)
        return;
      this.OnFrameOutItem(obj, idx);
    }

    public void OnItemPositionAreaOver(GameObject obj)
    {
      this.AutoFit.set_velocity(Vector2.get_zero());
    }

    public void OnUpdateScale(List<JobIconScrollListController.ItemData> items)
    {
      RectTransform component = (RectTransform) ((Component) this).GetComponent<RectTransform>();
      Rect rect = this.ViewObj.get_rect();
      // ISSUE: explicit reference operation
      float num1 = ((Rect) @rect).get_width() * 0.5f;
      this.mCenter = num1 - (float) component.get_anchoredPosition().x;
      for (int index = 0; index < items.Count; ++index)
      {
        float num2 = !items[index].job_icon.IsSingleIcon ? this.MIN_SCALE_DOUBLE : this.MIN_SCALE_SINGLE;
        float num3 = (float) (items[index].position.x + component.get_anchoredPosition().x) - num1;
        float num4 = Mathf.Clamp((float) (1.0 - (double) Mathf.Abs(num3) / (double) num1), 0.0f, 1f);
        float num5 = num2 + (1f - num2) * num4;
        items[index].gameObject.get_transform().set_localScale(new Vector3(num5, num5, num5));
        float num6 = items[index].job_icon.HalfWidth * (1f - num5);
        if ((double) num3 > 0.0)
          items[index].rectTransform.set_anchoredPosition(new Vector2((float) items[index].position.x - num6, (float) items[index].position.y));
        if ((double) num3 < 0.0)
          items[index].rectTransform.set_anchoredPosition(new Vector2((float) items[index].position.x + num6, (float) items[index].position.y));
      }
    }

    public void OnClick(GameObject obj)
    {
      this.Focus(obj, false, false, 0.0f);
    }

    public GameObject Focus(List<GameObject> objects, bool is_immediate = false, bool is_hide = false, float focus_time = 0.0f)
    {
      if (objects.Count <= 0)
        return (GameObject) null;
      RectTransform component = (RectTransform) ((Component) this).GetComponent<RectTransform>();
      Rect rect = this.ViewObj.get_rect();
      // ISSUE: explicit reference operation
      this.mCenter = ((Rect) @rect).get_width() * 0.5f - (float) component.get_anchoredPosition().x;
      float num1 = float.MaxValue;
      GameObject gameObject = objects[0];
      for (int index = 0; index < objects.Count; ++index)
      {
        float num2 = Mathf.Abs(this.mCenter - (float) (objects[index].get_transform() as RectTransform).get_anchoredPosition().x);
        if ((double) num1 >= (double) num2)
        {
          num1 = num2;
          gameObject = objects[index];
        }
      }
      this.Focus(gameObject, is_immediate, is_hide, focus_time);
      return gameObject;
    }

    public void Focus(GameObject obj, bool is_immediate = false, bool is_hide = false, float focus_time = 0.0f)
    {
      if (UnityEngine.Object.op_Equality((UnityEngine.Object) this.AutoFit, (UnityEngine.Object) null))
        return;
      float num = this.mDefaultAutoFitTime;
      if ((double) focus_time > 0.0)
        num = focus_time;
      if (is_immediate)
      {
        num = 0.0f;
        if (is_hide)
        {
          this.mIsImmediateFocusNow = true;
          if (Vector3.op_Equality(this.mDefaultViewportLocalPosition, Vector3.get_zero()))
            this.mDefaultViewportLocalPosition = ((Component) this).get_transform().get_parent().get_localPosition();
          ((Component) this).get_transform().get_parent().set_localPosition(new Vector3(0.0f, this.FRAME_OUT_POSITION_VALUE, 0.0f));
          this.mJobIconDispEffectTime = this.JOB_ICON_DISP_EFFECT_TIME;
        }
      }
      RectTransform component = (RectTransform) ((Component) this).get_gameObject().GetComponent<RectTransform>();
      int index = this.mController.Items.FindIndex((Predicate<JobIconScrollListController.ItemData>) (data => ((UnityEngine.Object) data.gameObject).GetInstanceID() == ((UnityEngine.Object) obj).GetInstanceID()));
      if (index == -1)
        return;
      this.AutoFit.FitTime = num;
      this.AutoFit.SetScrollToHorizontal((float) component.get_anchoredPosition().x - ((float) this.mController.Items[index].position.x - this.mCenter));
      if (is_immediate)
        this.AutoFit.Step();
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.back, (UnityEngine.Object) null))
        ((Selectable) this.back).set_interactable(false);
      this.mIsFocusNow = true;
    }

    public delegate void FrameOutItem(GameObject target, int index);
  }
}
