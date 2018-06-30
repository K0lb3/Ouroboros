namespace SRPG
{
    using System;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.EventSystems;

    public class BGSlider : MonoBehaviour, IDragHandler, IEventSystemHandler
    {
        public float ScrollSpeed;
        public float BGWidth;
        public List<GiziScroll> SyncScrollWith;
        public string SyncScrollWithID;
        private float mScrollPos;
        private float mDesiredScrollPos;
        private Vector2 mDefaultPosition;
        private bool mResetScrollPos;
        public float DefaultScrollRatio;
        private float WHEEL_SCROLL_COEF;
        private float axis_val;
        private List<RaycastResult> raycast_result_list;
        private List<GameObject> child_objects;
        private PointerEventData pointer_event;

        public BGSlider()
        {
            this.ScrollSpeed = 10f;
            this.BGWidth = 1500f;
            this.SyncScrollWith = new List<GiziScroll>();
            this.WHEEL_SCROLL_COEF = 300f;
            this.raycast_result_list = new List<RaycastResult>();
            this.child_objects = new List<GameObject>();
            base..ctor();
            return;
        }

        private void ClampScrollPos(float min, float max)
        {
            this.mScrollPos = Mathf.Clamp(this.mScrollPos, min, max);
            this.mDesiredScrollPos = Mathf.Clamp(this.mDesiredScrollPos, min, max);
            return;
        }

        private unsafe bool IsHitRayCast()
        {
            Transform[] transformArray;
            int num;
            RaycastResult result;
            this.raycast_result_list.Clear();
            this.pointer_event = new PointerEventData(EventSystem.get_current());
            this.pointer_event.set_position(Input.get_mousePosition());
            EventSystem.get_current().RaycastAll(this.pointer_event, this.raycast_result_list);
            if (this.raycast_result_list.Count > 0)
            {
                goto Label_0059;
            }
            return 0;
        Label_0059:
            this.child_objects.Clear();
            transformArray = base.GetComponentsInChildren<Transform>();
            num = 0;
            goto Label_0089;
        Label_0072:
            this.child_objects.Add(transformArray[num].get_gameObject());
            num += 1;
        Label_0089:
            if (num < ((int) transformArray.Length))
            {
                goto Label_0072;
            }
            result = this.raycast_result_list[0];
            if (this.child_objects.Contains(&result.get_gameObject()) != null)
            {
                goto Label_00B8;
            }
            return 0;
        Label_00B8:
            return 1;
        }

        public unsafe void OnDrag(PointerEventData eventData)
        {
            Vector2 vector;
            if ((eventData.get_pointerDrag() != base.get_gameObject()) == null)
            {
                goto Label_0017;
            }
            return;
        Label_0017:
            this.mDesiredScrollPos -= &eventData.get_delta().x;
            eventData.Use();
            return;
        }

        private void Start()
        {
            GameObject[] objArray;
            GameObject obj2;
            GameObject[] objArray2;
            int num;
            this.mResetScrollPos = 1;
            if (this.SyncScrollWith.Count != null)
            {
                goto Label_0064;
            }
            if (string.IsNullOrEmpty(this.SyncScrollWithID) != null)
            {
                goto Label_0064;
            }
            objArray = GameObjectID.FindGameObjects(this.SyncScrollWithID);
            if (objArray == null)
            {
                goto Label_0064;
            }
            objArray2 = objArray;
            num = 0;
            goto Label_005B;
        Label_0042:
            obj2 = objArray2[num];
            this.SyncScrollWith.Add(obj2.GetComponent<GiziScroll>());
            num += 1;
        Label_005B:
            if (num < ((int) objArray2.Length))
            {
                goto Label_0042;
            }
        Label_0064:
            return;
        }

        private unsafe void Update()
        {
            float num;
            float num2;
            float num3;
            GiziScroll scroll;
            List<GiziScroll>.Enumerator enumerator;
            Rect rect;
            float num4;
            this.UpdateWheelScroll();
            this.mScrollPos = Mathf.Lerp(this.mScrollPos, this.mDesiredScrollPos, Time.get_deltaTime() * this.ScrollSpeed);
            num = &(base.get_transform() as RectTransform).get_rect().get_width();
            num3 = Mathf.Max(this.BGWidth - num, 0f);
            if (this.mResetScrollPos == null)
            {
                goto Label_0083;
            }
            this.mScrollPos = this.mDesiredScrollPos = num3 * this.DefaultScrollRatio;
            this.mResetScrollPos = 0;
        Label_0083:
            this.ClampScrollPos(0f, num3);
            enumerator = this.SyncScrollWith.GetEnumerator();
        Label_009C:
            try
            {
                goto Label_00CE;
            Label_00A1:
                scroll = &enumerator.Current;
                if ((scroll != null) == null)
                {
                    goto Label_00CE;
                }
                if (num3 <= 0f)
                {
                    goto Label_00CE;
                }
                scroll.ScrollPos = this.mScrollPos / num3;
            Label_00CE:
                if (&enumerator.MoveNext() != null)
                {
                    goto Label_00A1;
                }
                goto Label_00EC;
            }
            finally
            {
            Label_00DF:
                ((List<GiziScroll>.Enumerator) enumerator).Dispose();
            }
        Label_00EC:
            return;
        }

        private void UpdateWheelScroll()
        {
            this.axis_val = Input.GetAxis("Mouse ScrollWheel");
            if (this.axis_val != 0f)
            {
                goto Label_0021;
            }
            return;
        Label_0021:
            if (this.IsHitRayCast() != null)
            {
                goto Label_002D;
            }
            return;
        Label_002D:
            this.mDesiredScrollPos -= this.axis_val * this.WHEEL_SCROLL_COEF;
            return;
        }
    }
}

