namespace SRPG
{
    using System;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.EventSystems;
    using UnityEngine.UI;

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
            this.DEFAULT_REST_ADD_TIME = 0.05f;
            this.DEFAULT_SCROLL_DECELERATION_RATE = 0.135f;
            this.WHEEL_SCROLL_DECELERATION_RATE = 0.0005f;
            this.SCROLL_FIXED_VALUE = 500f;
            this.SCROLL_VALUE_COEF = 5000f;
            this.IS_ENABLE_WHEEL_SCROLL_FOR_HORIZENTAL = 1;
            this.child_objects = new List<GameObject>();
            this.raycast_result_list = new List<RaycastResult>();
            base..ctor();
            return;
        }

        protected override void Awake()
        {
            base.Awake();
            if ((base.get_verticalScrollbar() != null) == null)
            {
                goto Label_0027;
            }
            base.get_verticalScrollbar().set_value(1f);
        Label_0027:
            this.defalt_movement_type = base.get_movementType();
            return;
        }

        private unsafe bool IsHitRayCast()
        {
            SRPG_ScrollRect[] rectArray;
            int num;
            Transform[] transformArray;
            int num2;
            RaycastResult result;
            rectArray = base.GetComponentsInChildren<SRPG_ScrollRect>();
            num = 0;
            goto Label_0034;
        Label_000E:
            if ((this == rectArray[num]) == null)
            {
                goto Label_0021;
            }
            goto Label_0030;
        Label_0021:
            if (rectArray[num].IsHitRayCast() == null)
            {
                goto Label_0030;
            }
            return 0;
        Label_0030:
            num += 1;
        Label_0034:
            if (num < ((int) rectArray.Length))
            {
                goto Label_000E;
            }
            this.raycast_result_list.Clear();
            this.pointer_event = new PointerEventData(EventSystem.get_current());
            this.pointer_event.set_position(Input.get_mousePosition());
            EventSystem.get_current().RaycastAll(this.pointer_event, this.raycast_result_list);
            if (this.raycast_result_list.Count > 0)
            {
                goto Label_0096;
            }
            return 0;
        Label_0096:
            this.child_objects.Clear();
            transformArray = base.GetComponentsInChildren<Transform>();
            num2 = 0;
            goto Label_00C6;
        Label_00AF:
            this.child_objects.Add(transformArray[num2].get_gameObject());
            num2 += 1;
        Label_00C6:
            if (num2 < ((int) transformArray.Length))
            {
                goto Label_00AF;
            }
            result = this.raycast_result_list[0];
            if (this.child_objects.Contains(&result.get_gameObject()) != null)
            {
                goto Label_00F6;
            }
            return 0;
        Label_00F6:
            return 1;
        }

        protected override void LateUpdate()
        {
            base.LateUpdate();
            this.UpdateDecelerationRate();
            this.UpdateWheelScrollFlag();
            this.UpdateScroll();
            return;
        }

        public override void OnBeginDrag(PointerEventData eventData)
        {
            base.OnBeginDrag(eventData);
            if (this.IsWheelScroll == null)
            {
                goto Label_001F;
            }
            this.IsWheelScroll = 0;
            this.StopMovement();
        Label_001F:
            return;
        }

        private unsafe void SetGlideValue(float _axis_value, bool _is_begin)
        {
            float num;
            Vector2 vector;
            Vector2 vector2;
            Vector2 vector3;
            Vector2 vector4;
            num = (_is_begin == null) ? 0f : this.SCROLL_FIXED_VALUE;
            if (base.get_vertical() == null)
            {
                goto Label_008A;
            }
            if (this.axis_val < 0f)
            {
                goto Label_0049;
            }
            num = -num + (-this.axis_val * this.SCROLL_VALUE_COEF);
            goto Label_005A;
        Label_0049:
            num += -this.axis_val * this.SCROLL_VALUE_COEF;
        Label_005A:
            base.set_velocity(new Vector2(&base.get_velocity().x, &base.get_velocity().y + num));
            this.is_scroll_now = 1;
        Label_008A:
            if (base.get_horizontal() == null)
            {
                goto Label_0107;
            }
            if (this.IS_ENABLE_WHEEL_SCROLL_FOR_HORIZENTAL == null)
            {
                goto Label_0107;
            }
            if (this.axis_val < 0f)
            {
                goto Label_00C5;
            }
            num += this.axis_val * this.SCROLL_VALUE_COEF;
            goto Label_00D6;
        Label_00C5:
            num = -num + (this.axis_val * this.SCROLL_VALUE_COEF);
        Label_00D6:
            base.set_velocity(new Vector2(&base.get_velocity().x + num, &base.get_velocity().y));
            this.is_scroll_now = 1;
        Label_0107:
            return;
        }

        private void UpdateDecelerationRate()
        {
            base.set_decelerationRate((this.IsWheelScroll == null) ? this.DEFAULT_SCROLL_DECELERATION_RATE : this.WHEEL_SCROLL_DECELERATION_RATE);
            return;
        }

        private void UpdateScroll()
        {
            this.axis_val = Input.GetAxis("Mouse ScrollWheel");
            if (this.axis_val != 0f)
            {
                goto Label_0028;
            }
            this.is_scroll_now = 0;
            return;
        Label_0028:
            if (this.IsHitRayCast() != null)
            {
                goto Label_0034;
            }
            return;
        Label_0034:
            this.rest_add_time = Mathf.Max(0f, this.rest_add_time - Time.get_deltaTime());
            if (this.rest_add_time > 0f)
            {
                goto Label_0081;
            }
            this.rest_add_time = this.DEFAULT_REST_ADD_TIME;
            this.SetGlideValue(this.axis_val, 1);
            this.IsWheelScroll = 1;
            return;
        Label_0081:
            this.SetGlideValue(this.axis_val, 0);
            this.IsWheelScroll = 1;
            return;
        }

        private unsafe void UpdateWheelScrollFlag()
        {
            Vector2 vector;
            Vector2 vector2;
            if (this.IsWheelScroll == null)
            {
                goto Label_0042;
            }
            if (&base.get_velocity().x != 0f)
            {
                goto Label_0042;
            }
            if (&base.get_velocity().y != 0f)
            {
                goto Label_0042;
            }
            this.IsWheelScroll = 0;
        Label_0042:
            return;
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
                if (base.get_movementType() == null)
                {
                    goto Label_002A;
                }
                base.set_movementType((value == null) ? this.defalt_movement_type : 2);
            Label_002A:
                this.OnEndDrag(new PointerEventData(EventSystem.get_current()));
                return;
            }
        }

        public bool IsWheelScrollNow
        {
            get
            {
                return this.IsWheelScroll;
            }
        }
    }
}

