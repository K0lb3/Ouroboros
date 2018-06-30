namespace SRPG
{
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;
    using System.Text;
    using UnityEngine;
    using UnityEngine.EventSystems;

    [AddComponentMenu("Event/Touch Input Module (SRPG)")]
    public class SRPG_TouchInputModule : PointerInputModule
    {
        private static int mLockCount;
        public GameObject TouchEffect;
        private GameObject[] mTouchEffectPool;
        private int mNumActiveTouchEffects;
        private bool mTouchEffectPoolInitialized;
        public OnDoubleTapDelegate OnDoubleTap;
        private float mDoubleTap1stReleasedTime;
        private readonly int BUTTON_INDEX_MAX;
        private int pressing_button_index;
        public static bool IsMultiTouching;
        private Vector2 m_LastMousePosition;
        private Vector2 m_MousePosition;
        [SerializeField]
        private bool m_AllowActivationOnStandalone;
        private int mPrimaryFingerID;
        [CompilerGenerated]
        private bool <IsHandling>k__BackingField;
        [CompilerGenerated]
        private static OnDoubleTapDelegate <>f__am$cacheF;

        public SRPG_TouchInputModule()
        {
            this.mTouchEffectPool = new GameObject[8];
            this.mDoubleTap1stReleasedTime = -1f;
            this.BUTTON_INDEX_MAX = 3;
            this.pressing_button_index = -1;
            this.m_AllowActivationOnStandalone = 1;
            this.mPrimaryFingerID = -1;
            base..ctor();
            return;
        }

        [CompilerGenerated]
        private static void <Start>m__3F7(Vector2 position)
        {
        }

        public override void DeactivateModule()
        {
            base.DeactivateModule();
            base.ClearSelection();
            return;
        }

        private unsafe void FakeTouches()
        {
            bool flag;
            bool flag2;
            bool flag3;
            int num;
            int num2;
            PointerEventData data;
            PointerEventData data2;
            RaycastResult result;
            RaycastResult result2;
            RaycastResult result3;
            flag = 0;
            flag2 = 0;
            flag3 = 0;
            if (this.pressing_button_index > -1)
            {
                goto Label_0042;
            }
            num = 0;
            goto Label_0036;
        Label_0019:
            if (Input.GetMouseButtonDown(num) == null)
            {
                goto Label_0032;
            }
            this.pressing_button_index = num;
            flag2 = 1;
            goto Label_0042;
        Label_0032:
            num += 1;
        Label_0036:
            if (num < this.BUTTON_INDEX_MAX)
            {
                goto Label_0019;
            }
        Label_0042:
            num2 = 0;
            goto Label_0077;
        Label_004A:
            if ((Input.GetMouseButtonUp(num2) == null) || (this.pressing_button_index != num2))
            {
                goto Label_0071;
            }
            this.pressing_button_index = -1;
            flag3 = 1;
            goto Label_0084;
        Label_0071:
            num2 += 1;
        Label_0077:
            if (num2 < this.BUTTON_INDEX_MAX)
            {
                goto Label_004A;
            }
        Label_0084:
            this.IsHandling = (flag2 != null) ? 1 : flag3;
            data = this.GetMousePointerEventData().GetButtonState(0).get_eventData().buttonData;
            this.ProcessTouchPress(data, flag2, flag3);
            if (Input.GetMouseButton(0) != null)
            {
                goto Label_00D7;
            }
            if (Input.GetMouseButton(1) != null)
            {
                goto Label_00D7;
            }
            if (Input.GetMouseButton(2) == null)
            {
                goto Label_0165;
            }
        Label_00D7:
            this.IsHandling = 1;
            this.ProcessMove(data);
            this.ProcessDrag(data);
            if (&data.get_pointerPressRaycast().get_isValid() == null)
            {
                goto Label_0165;
            }
            data2 = this.GetMousePointerEventData().GetButtonState(1).get_eventData().buttonData;
            if (Input.GetMouseButtonDown(1) == null)
            {
                goto Label_0134;
            }
            data2.set_pointerPressRaycast(data2.get_pointerCurrentRaycast());
        Label_0134:
            if (Input.GetMouseButton(1) == null)
            {
                goto Label_0165;
            }
            flag = &data.get_pointerPressRaycast().get_gameObject() == &data2.get_pointerPressRaycast().get_gameObject();
        Label_0165:
            IsMultiTouching = flag;
            return;
        }

        private unsafe PointerEventData GetMousePointerEvent(int index)
        {
            int[] numArray1;
            int num;
            bool flag;
            PointerEventData data;
            bool flag2;
            Vector2 vector;
            RaycastResult result;
            numArray1 = new int[] { -1, -2, -3 };
            num = numArray1[index];
            flag = Input.GetMouseButtonDown(num);
            flag2 = base.GetPointerData(num, &data, 1);
            data.Reset();
            if (flag2 == null)
            {
                goto Label_0045;
            }
            data.set_position(Input.get_mousePosition());
        Label_0045:
            vector = Input.get_mousePosition();
            data.set_delta(vector - data.get_position());
            data.set_position(vector);
            data.set_scrollDelta(Input.get_mouseScrollDelta());
            base.get_eventSystem().RaycastAll(data, base.m_RaycastResultCache);
            result = BaseInputModule.FindFirstRaycast(base.m_RaycastResultCache);
            data.set_pointerCurrentRaycast(result);
            if (flag == null)
            {
                goto Label_00AF;
            }
            data.set_delta(Vector2.get_zero());
        Label_00AF:
            return data;
        }

        private void InitTouchEffects()
        {
            int num;
            if (this.mTouchEffectPoolInitialized == null)
            {
                goto Label_000C;
            }
            return;
        Label_000C:
            if ((this.TouchEffect != null) == null)
            {
                goto Label_0074;
            }
            num = 0;
            goto Label_0066;
        Label_0024:
            this.mTouchEffectPool[num] = Object.Instantiate<GameObject>(this.TouchEffect);
            this.mTouchEffectPool[num].SetActive(0);
            this.mTouchEffectPool[num].get_transform().SetParent(UIUtility.ParticleCanvas.get_transform(), 0);
            num += 1;
        Label_0066:
            if (num < ((int) this.mTouchEffectPool.Length))
            {
                goto Label_0024;
            }
        Label_0074:
            this.mTouchEffectPoolInitialized = 1;
            return;
        }

        public override bool IsModuleSupported()
        {
            return ((this.m_AllowActivationOnStandalone != null) ? 1 : Application.get_isMobilePlatform());
        }

        public static void LockInput()
        {
            mLockCount += 1;
            EventSystem.get_current().get_currentInputModule().set_enabled(mLockCount == 0);
            return;
        }

        public override void Process()
        {
            this.IsHandling = 0;
            if (Application.get_platform() == 7)
            {
                goto Label_0032;
            }
            if (Application.get_platform() == 2)
            {
                goto Label_0032;
            }
            if (Application.get_platform() == null)
            {
                goto Label_0032;
            }
            if (Application.get_platform() != 1)
            {
                goto Label_0039;
            }
        Label_0032:
            this.SendUpdateEventToSelectedObject();
        Label_0039:
            if (base.get_enabled() != null)
            {
                goto Label_0045;
            }
            return;
        Label_0045:
            if (this.UseFakeInput() == null)
            {
                goto Label_005B;
            }
            this.FakeTouches();
            goto Label_0061;
        Label_005B:
            this.ProcessTouchEvents();
        Label_0061:
            if (this.IsHandling != null)
            {
                goto Label_0082;
            }
            if (Input.GetKeyDown(0x1b) == null)
            {
                goto Label_0082;
            }
            BackHandler.Invoke();
            Input.ResetInputAxes();
        Label_0082:
            return;
        }

        private unsafe void ProcessTouchEvents()
        {
            List<GameObject> list;
            int num;
            Touch touch;
            bool flag;
            bool flag2;
            PointerEventData data;
            bool flag3;
            int num2;
            int num3;
            RaycastResult result;
            RaycastResult result2;
            list = new List<GameObject>();
            num = 0;
            goto Label_00EC;
        Label_000D:
            touch = Input.GetTouch(num);
            data = base.GetTouchPointerEventData(touch, &flag2, &flag);
            flag3 = 0;
            if (this.mPrimaryFingerID != -1)
            {
                goto Label_0044;
            }
            if (flag2 == null)
            {
                goto Label_0044;
            }
            this.mPrimaryFingerID = &touch.get_fingerId();
        Label_0044:
            if ((this.mPrimaryFingerID == &touch.get_fingerId()) == null)
            {
                goto Label_00A4;
            }
            this.ProcessTouchPress(data, flag2, flag);
            if (flag != null)
            {
                goto Label_0098;
            }
            list.Add(&data.get_pointerPressRaycast().get_gameObject());
            this.ProcessMove(data);
            this.ProcessDrag(data);
            goto Label_009F;
        Label_0098:
            this.mPrimaryFingerID = -1;
        Label_009F:
            goto Label_00DA;
        Label_00A4:
            if (flag2 == null)
            {
                goto Label_00BE;
            }
            data.set_pointerPressRaycast(data.get_pointerCurrentRaycast());
            goto Label_00DA;
        Label_00BE:
            if (flag != null)
            {
                goto Label_00DA;
            }
            list.Add(&data.get_pointerPressRaycast().get_gameObject());
        Label_00DA:
            if (flag == null)
            {
                goto Label_00E8;
            }
            base.RemovePointerData(data);
        Label_00E8:
            num += 1;
        Label_00EC:
            if (num < Input.get_touchCount())
            {
                goto Label_000D;
            }
            IsMultiTouching = 0;
            if (this.mPrimaryFingerID == -1)
            {
                goto Label_0183;
            }
            if (list.Count < 2)
            {
                goto Label_0183;
            }
            num2 = 0;
            goto Label_0170;
        Label_011D:
            if ((list[num2] == null) == null)
            {
                goto Label_0131;
            }
            return;
        Label_0131:
            num3 = num2 + 1;
            goto Label_015D;
        Label_013C:
            if ((list[num2] != list[num3]) == null)
            {
                goto Label_0157;
            }
            return;
        Label_0157:
            num3 += 1;
        Label_015D:
            if (num3 < list.Count)
            {
                goto Label_013C;
            }
            num2 += 1;
        Label_0170:
            if (num2 < list.Count)
            {
                goto Label_011D;
            }
            IsMultiTouching = 1;
        Label_0183:
            return;
        }

        private unsafe void ProcessTouchPress(PointerEventData pointerEvent, bool pressed, bool released)
        {
            GameObject obj2;
            GameObject obj3;
            GameObject obj4;
            float num;
            GameObject obj5;
            float num2;
            RaycastResult result;
            obj2 = &pointerEvent.get_pointerCurrentRaycast().get_gameObject();
            if (pressed == null)
            {
                goto Label_00E9;
            }
            pointerEvent.set_eligibleForClick(1);
            pointerEvent.set_delta(Vector2.get_zero());
            pointerEvent.set_pressPosition(pointerEvent.get_position());
            pointerEvent.set_pointerPressRaycast(pointerEvent.get_pointerCurrentRaycast());
            if ((pointerEvent.get_pointerEnter() != obj2) == null)
            {
                goto Label_0060;
            }
            base.HandlePointerExitAndEnter(pointerEvent, obj2);
            pointerEvent.set_pointerEnter(obj2);
        Label_0060:
            obj3 = ExecuteEvents.ExecuteHierarchy<IPointerDownHandler>(obj2, pointerEvent, ExecuteEvents.get_pointerDownHandler());
            if ((obj3 == null) == null)
            {
                goto Label_0080;
            }
            obj3 = ExecuteEvents.GetEventHandler<IPointerClickHandler>(obj2);
        Label_0080:
            if ((obj3 != pointerEvent.get_pointerPress()) == null)
            {
                goto Label_00A6;
            }
            pointerEvent.set_pointerPress(obj3);
            pointerEvent.set_rawPointerPress(obj2);
            pointerEvent.set_clickCount(0);
        Label_00A6:
            pointerEvent.set_pointerDrag(ExecuteEvents.GetEventHandler<IDragHandler>(obj2));
            if ((pointerEvent.get_pointerDrag() != null) == null)
            {
                goto Label_00D5;
            }
            ExecuteEvents.Execute<IBeginDragHandler>(pointerEvent.get_pointerDrag(), pointerEvent, ExecuteEvents.get_beginDragHandler());
        Label_00D5:
            obj4 = ExecuteEvents.GetEventHandler<ISelectHandler>(obj2);
            base.get_eventSystem().SetSelectedGameObject(obj4, pointerEvent);
        Label_00E9:
            if (released == null)
            {
                goto Label_0257;
            }
            num = Time.get_unscaledTime();
            if (this.mDoubleTap1stReleasedTime >= 0f)
            {
                goto Label_0111;
            }
            this.mDoubleTap1stReleasedTime = num;
            goto Label_014B;
        Label_0111:
            if ((num - this.mDoubleTap1stReleasedTime) < 0.3f)
            {
                goto Label_012F;
            }
            this.mDoubleTap1stReleasedTime = num;
            goto Label_014B;
        Label_012F:
            this.OnDoubleTap(pointerEvent.get_position());
            this.mDoubleTap1stReleasedTime = -1f;
        Label_014B:
            ExecuteEvents.Execute<IPointerUpHandler>(pointerEvent.get_pointerPress(), pointerEvent, ExecuteEvents.get_pointerUpHandler());
            obj5 = ExecuteEvents.GetEventHandler<IPointerClickHandler>(obj2);
            if ((pointerEvent.get_pointerPress() == obj5) == null)
            {
                goto Label_01E1;
            }
            if (pointerEvent.get_eligibleForClick() == null)
            {
                goto Label_01E1;
            }
            num2 = Time.get_unscaledTime();
            if ((num2 - pointerEvent.get_clickTime()) >= 0.3f)
            {
                goto Label_01AF;
            }
            pointerEvent.set_clickCount(pointerEvent.get_clickCount() + 1);
            goto Label_01B6;
        Label_01AF:
            pointerEvent.set_clickCount(1);
        Label_01B6:
            pointerEvent.set_clickTime(num2);
            ExecuteEvents.Execute<IPointerClickHandler>(pointerEvent.get_pointerPress(), pointerEvent, ExecuteEvents.get_pointerClickHandler());
            this.SpawnTouchEffect(pointerEvent.get_position());
            goto Label_01FF;
        Label_01E1:
            if ((pointerEvent.get_pointerDrag() != null) == null)
            {
                goto Label_01FF;
            }
            ExecuteEvents.ExecuteHierarchy<IDropHandler>(obj2, pointerEvent, ExecuteEvents.get_dropHandler());
        Label_01FF:
            pointerEvent.set_eligibleForClick(0);
            pointerEvent.set_pointerPress(null);
            pointerEvent.set_rawPointerPress(null);
            if ((pointerEvent.get_pointerDrag() != null) == null)
            {
                goto Label_0237;
            }
            ExecuteEvents.Execute<IEndDragHandler>(pointerEvent.get_pointerDrag(), pointerEvent, ExecuteEvents.get_endDragHandler());
        Label_0237:
            pointerEvent.set_pointerDrag(null);
            ExecuteEvents.ExecuteHierarchy<IPointerExitHandler>(pointerEvent.get_pointerEnter(), pointerEvent, ExecuteEvents.get_pointerExitHandler());
            pointerEvent.set_pointerEnter(null);
        Label_0257:
            return;
        }

        private bool SendUpdateEventToSelectedObject()
        {
            BaseEventData data;
            if ((base.get_eventSystem().get_currentSelectedGameObject() == null) == null)
            {
                goto Label_0018;
            }
            return 0;
        Label_0018:
            data = this.GetBaseEventData();
            ExecuteEvents.Execute<IUpdateSelectedHandler>(base.get_eventSystem().get_currentSelectedGameObject(), data, ExecuteEvents.get_updateSelectedHandler());
            return data.get_used();
        }

        public override unsafe bool ShouldActivateModule()
        {
            bool flag;
            int num;
            Touch touch;
            Vector2 vector;
            if (base.ShouldActivateModule() != null)
            {
                goto Label_000D;
            }
            return 0;
        Label_000D:
            if (this.UseFakeInput() == null)
            {
                goto Label_005D;
            }
            flag = ((Input.GetMouseButtonDown(0) != null) || (Input.GetMouseButtonDown(1) != null)) ? 1 : Input.GetMouseButtonDown(2);
            vector = this.m_MousePosition - this.m_LastMousePosition;
            flag |= &vector.get_sqrMagnitude() > 0f;
            return flag;
        Label_005D:
            num = 0;
            goto Label_0097;
        Label_0064:
            touch = Input.GetTouch(num);
            if (&touch.get_phase() == null)
            {
                goto Label_0091;
            }
            if (&touch.get_phase() == 1)
            {
                goto Label_0091;
            }
            if (&touch.get_phase() != 2)
            {
                goto Label_0093;
            }
        Label_0091:
            return 1;
        Label_0093:
            num += 1;
        Label_0097:
            if (num < Input.get_touchCount())
            {
                goto Label_0064;
            }
            return 0;
        }

        private unsafe void SpawnTouchEffect(Vector2 position)
        {
            int num;
            GameObject obj2;
            RectTransform transform;
            Vector2 vector;
            if (this.mTouchEffectPoolInitialized != null)
            {
                goto Label_0011;
            }
            this.InitTouchEffects();
        Label_0011:
            num = 0;
            goto Label_0088;
        Label_0018:
            if ((this.mTouchEffectPool[num] != null) == null)
            {
                goto Label_0084;
            }
            if (this.mTouchEffectPool[num].get_activeSelf() != null)
            {
                goto Label_0084;
            }
            obj2 = this.mTouchEffectPool[num];
            transform = obj2.get_transform() as RectTransform;
            RectTransformUtility.ScreenPointToLocalPointInRectangle(transform.get_parent() as RectTransform, position, null, &vector);
            transform.set_anchoredPosition(vector);
            obj2.SetActive(1);
            this.mNumActiveTouchEffects += 1;
            return;
        Label_0084:
            num += 1;
        Label_0088:
            if (num < ((int) this.mTouchEffectPool.Length))
            {
                goto Label_0018;
            }
            return;
        }

        protected override void Start()
        {
            base.Start();
            UIUtility.InitParticleCanvas();
            this.InitTouchEffects();
            if (<>f__am$cacheF != null)
            {
                goto Label_0030;
            }
            <>f__am$cacheF = new OnDoubleTapDelegate(SRPG_TouchInputModule.<Start>m__3F7);
        Label_0030:
            this.OnDoubleTap = (OnDoubleTapDelegate) Delegate.Combine(this.OnDoubleTap, <>f__am$cacheF);
            return;
        }

        public override unsafe string ToString()
        {
            StringBuilder builder;
            PointerEventData data;
            KeyValuePair<int, PointerEventData> pair;
            Dictionary<int, PointerEventData>.Enumerator enumerator;
            builder = new StringBuilder();
            builder.AppendLine((this.UseFakeInput() == null) ? "Input: Touch" : "Input: Faked");
            if (this.UseFakeInput() == null)
            {
                goto Label_008A;
            }
            data = base.GetLastPointerEventData(-1);
            if (data == null)
            {
                goto Label_004D;
            }
            builder.AppendLine(data.ToString());
        Label_004D:
            data = base.GetLastPointerEventData(-2);
            if (data == null)
            {
                goto Label_0069;
            }
            builder.AppendLine(data.ToString());
        Label_0069:
            data = base.GetLastPointerEventData(-3);
            if (data == null)
            {
                goto Label_00CE;
            }
            builder.AppendLine(data.ToString());
            goto Label_00CE;
        Label_008A:
            enumerator = base.m_PointerData.GetEnumerator();
        Label_0096:
            try
            {
                goto Label_00B1;
            Label_009B:
                pair = &enumerator.Current;
                builder.AppendLine(&pair.ToString());
            Label_00B1:
                if (&enumerator.MoveNext() != null)
                {
                    goto Label_009B;
                }
                goto Label_00CE;
            }
            finally
            {
            Label_00C2:
                ((Dictionary<int, PointerEventData>.Enumerator) enumerator).Dispose();
            }
        Label_00CE:
            return builder.ToString();
        }

        public static void UnlockInput(bool forceReset)
        {
            if (forceReset == null)
            {
                goto Label_0011;
            }
            mLockCount = 0;
            goto Label_001D;
        Label_0011:
            mLockCount -= 1;
        Label_001D:
            EventSystem.get_current().get_currentInputModule().set_enabled(mLockCount == 0);
            return;
        }

        private void Update()
        {
            if (this.mNumActiveTouchEffects <= 0)
            {
                goto Label_0012;
            }
            this.UpdateTouchEffects();
        Label_0012:
            return;
        }

        public override void UpdateModule()
        {
            this.m_LastMousePosition = this.m_MousePosition;
            this.m_MousePosition = Input.get_mousePosition();
            return;
        }

        private void UpdateTouchEffects()
        {
            int num;
            bool flag;
            UIParticleSystem[] systemArray;
            int num2;
            int num3;
            num = 0;
            goto Label_009C;
        Label_0007:
            if (this.mTouchEffectPool[num].get_activeSelf() == null)
            {
                goto Label_0098;
            }
            flag = 0;
            systemArray = this.mTouchEffectPool[num].GetComponentsInChildren<UIParticleSystem>();
            num2 = ((int) systemArray.Length) - 1;
            goto Label_004C;
        Label_0034:
            if (systemArray[num2].IsAlive() == null)
            {
                goto Label_0048;
            }
            flag = 1;
            goto Label_0053;
        Label_0048:
            num2 -= 1;
        Label_004C:
            if (num2 >= 0)
            {
                goto Label_0034;
            }
        Label_0053:
            if (flag != null)
            {
                goto Label_0098;
            }
            num3 = ((int) systemArray.Length) - 1;
            goto Label_0074;
        Label_0065:
            systemArray[num3].ResetParticleSystem();
            num3 -= 1;
        Label_0074:
            if (num3 >= 0)
            {
                goto Label_0065;
            }
            this.mTouchEffectPool[num].SetActive(0);
            this.mNumActiveTouchEffects -= 1;
        Label_0098:
            num += 1;
        Label_009C:
            if (num < ((int) this.mTouchEffectPool.Length))
            {
                goto Label_0007;
            }
            return;
        }

        private bool UseFakeInput()
        {
            return (Application.get_isMobilePlatform() == 0);
        }

        private bool IsHandling
        {
            [CompilerGenerated]
            get
            {
                return this.<IsHandling>k__BackingField;
            }
            [CompilerGenerated]
            set
            {
                this.<IsHandling>k__BackingField = value;
                return;
            }
        }

        public bool allowActivationOnStandalone
        {
            get
            {
                return this.m_AllowActivationOnStandalone;
            }
            set
            {
                this.m_AllowActivationOnStandalone = value;
                return;
            }
        }

        public delegate void OnDoubleTapDelegate(Vector2 position);
    }
}

