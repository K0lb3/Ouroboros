// Decompiled with JetBrains decompiler
// Type: SRPG.SRPG_ScrollRect
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 85BFDF7F-5712-4D45-9CD6-3465C703DFDF
// Assembly location: S:\Desktop\Assembly-CSharp.dll

using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace SRPG
{
  [AddComponentMenu("UI/ScrollRect (SRPG)")]
  public class SRPG_ScrollRect : ScrollRect
  {
    private float DEFAULT_REST_ADD_TIME;
    private float DEFAULT_SCROLL_DECELERATION_RATE;
    private float WHEEL_SCROLL_DECELERATION_RATE;
    private float SCROLL_FIXED_VALUE;
    private float SCROLL_VALUE_COEF;
    private bool IS_ENABLE_WHEEL_SCROLL_FOR_HORIZENTAL;
    private PointerEventData pointer_event;
    private List<GameObject> child_objects;
    private List<RaycastResult> raycast_result_list;
    private ScrollRect.MovementType defalt_movement_type;
    private float axis_val;
    private float rest_add_time;
    private bool is_scroll_now;
    private bool is_wheel_scroll;

    public SRPG_ScrollRect()
    {
      base.\u002Ector();
    }

    protected virtual void Awake()
    {
      ((UIBehaviour) this).Awake();
      if (Object.op_Inequality((Object) this.get_verticalScrollbar(), (Object) null))
        this.get_verticalScrollbar().set_value(1f);
      this.defalt_movement_type = this.get_movementType();
    }

    public bool IsScrollNow
    {
      get
      {
        return this.is_scroll_now;
      }
    }

    private bool IsWheelScroll
    {
      get
      {
        return this.is_wheel_scroll;
      }
      set
      {
        this.is_wheel_scroll = value;
        if (this.get_movementType() != null)
          this.set_movementType(!value ? this.defalt_movement_type : (ScrollRect.MovementType) 2);
        this.OnEndDrag(new PointerEventData(EventSystem.get_current()));
      }
    }

    public bool IsWheelScrollNow
    {
      get
      {
        return this.IsWheelScroll;
      }
    }

    protected virtual void LateUpdate()
    {
      base.LateUpdate();
      this.UpdateDecelerationRate();
      this.UpdateWheelScrollFlag();
      this.UpdateScroll();
    }

    private void UpdateDecelerationRate()
    {
      this.set_decelerationRate(!this.IsWheelScroll ? this.DEFAULT_SCROLL_DECELERATION_RATE : this.WHEEL_SCROLL_DECELERATION_RATE);
    }

    private void UpdateWheelScrollFlag()
    {
      if (!this.IsWheelScroll || this.get_velocity().x != 0.0 || this.get_velocity().y != 0.0)
        return;
      this.IsWheelScroll = false;
    }

    private void UpdateScroll()
    {
      this.axis_val = Input.GetAxis("Mouse ScrollWheel");
      if ((double) this.axis_val == 0.0)
      {
        this.is_scroll_now = false;
      }
      else
      {
        if (!this.IsHitRayCast())
          return;
        this.rest_add_time = Mathf.Max(0.0f, this.rest_add_time - Time.get_deltaTime());
        if ((double) this.rest_add_time <= 0.0)
        {
          this.rest_add_time = this.DEFAULT_REST_ADD_TIME;
          this.SetGlideValue(this.axis_val, true);
          this.IsWheelScroll = true;
        }
        else
        {
          this.SetGlideValue(this.axis_val, false);
          this.IsWheelScroll = true;
        }
      }
    }

    private void SetGlideValue(float _axis_value, bool _is_begin)
    {
      float num = !_is_begin ? 0.0f : this.SCROLL_FIXED_VALUE;
      if (this.get_vertical())
      {
        if ((double) this.axis_val >= 0.0)
          num = (float) (-(double) num + -(double) this.axis_val * (double) this.SCROLL_VALUE_COEF);
        else
          num += -this.axis_val * this.SCROLL_VALUE_COEF;
        this.set_velocity(new Vector2((float) this.get_velocity().x, (float) this.get_velocity().y + num));
        this.is_scroll_now = true;
      }
      if (!this.get_horizontal() || !this.IS_ENABLE_WHEEL_SCROLL_FOR_HORIZENTAL)
        return;
      this.set_velocity(new Vector2((float) this.get_velocity().x + ((double) this.axis_val < 0.0 ? (float) (-(double) num + (double) this.axis_val * (double) this.SCROLL_VALUE_COEF) : num + this.axis_val * this.SCROLL_VALUE_COEF), (float) this.get_velocity().y));
      this.is_scroll_now = true;
    }

    private bool IsHitRayCast()
    {
      SRPG_ScrollRect[] componentsInChildren = (SRPG_ScrollRect[]) ((Component) this).GetComponentsInChildren<SRPG_ScrollRect>();
      for (int index = 0; index < componentsInChildren.Length; ++index)
      {
        if (!Object.op_Equality((Object) this, (Object) componentsInChildren[index]) && componentsInChildren[index].IsHitRayCast())
          return false;
      }
      this.raycast_result_list.Clear();
      this.pointer_event = new PointerEventData(EventSystem.get_current());
      this.pointer_event.set_position(Vector2.op_Implicit(Input.get_mousePosition()));
      EventSystem.get_current().RaycastAll(this.pointer_event, this.raycast_result_list);
      if (this.raycast_result_list.Count <= 0)
        return false;
      this.child_objects.Clear();
      foreach (Component componentsInChild in (Transform[]) ((Component) this).GetComponentsInChildren<Transform>())
        this.child_objects.Add(componentsInChild.get_gameObject());
      List<GameObject> childObjects = this.child_objects;
      RaycastResult raycastResult = this.raycast_result_list[0];
      // ISSUE: explicit reference operation
      GameObject gameObject = ((RaycastResult) @raycastResult).get_gameObject();
      return childObjects.Contains(gameObject);
    }

    public virtual void OnBeginDrag(PointerEventData eventData)
    {
      base.OnBeginDrag(eventData);
      if (!this.IsWheelScroll)
        return;
      this.IsWheelScroll = false;
      this.StopMovement();
    }
  }
}
