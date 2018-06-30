namespace SRPG
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using UnityEngine;
    using UnityEngine.EventSystems;
    using UnityEngine.Serialization;
    using UnityEngine.UI;

    public class ButtonEvent : EventTrigger
    {
        private const float HODL_TIME = 0.5f;
        public const string SYSTEM_LOCK = "system";
        public GameObject syncEvent;
        [FormerlySerializedAs("autoLock")]
        public int flag;
        public SelectableType selectableType;
        public List<Event> events;
        public bool flickEventEnable;
        public Event flickEvent;
        private Selectable m_Selectable;
        private Vector2 m_Pos;
        private bool m_DragMove;
        private float m_BeginEnterTime;
        private bool m_Enter;
        private bool m_HoldCheck;
        private bool m_HoldOn;
        private static Dictionary<string, int> m_Lock;
        private static List<Listener> m_Listener;

        static ButtonEvent()
        {
            m_Lock = new Dictionary<string, int>();
            m_Listener = new List<Listener>();
            return;
        }

        public ButtonEvent()
        {
            this.events = new List<Event>();
            this.flickEvent = new Event();
            this.m_Pos = Vector2.get_zero();
            base..ctor();
            return;
        }

        public static Listener AddListener(string eventName, Action<bool, object> callback)
        {
            Listener listener;
            listener = new Listener();
            listener.eventName = eventName;
            listener.callback = callback;
            m_Listener.Add(listener);
            return listener;
        }

        private void Awake()
        {
            SelectableType type;
            type = 0;
            this.m_Selectable = base.GetComponent<Button>();
            if ((this.m_Selectable != null) == null)
            {
                goto Label_0026;
            }
            type = 1;
            goto Label_0045;
        Label_0026:
            this.m_Selectable = base.GetComponent<Toggle>();
            if ((this.m_Selectable != null) == null)
            {
                goto Label_0045;
            }
            type = 2;
        Label_0045:
            if (this.selectableType != null)
            {
                goto Label_0057;
            }
            this.selectableType = type;
        Label_0057:
            return;
        }

        public static void ForceInvoke(string eventName, object value)
        {
            int num;
            Listener listener;
            num = 0;
            goto Label_0035;
        Label_0007:
            listener = m_Listener[num];
            if ((listener.eventName == eventName) == null)
            {
                goto Label_0031;
            }
            listener.callback(1, value);
        Label_0031:
            num += 1;
        Label_0035:
            if (num < m_Listener.Count)
            {
                goto Label_0007;
            }
            return;
        }

        public Event GetEvent(string eventName)
        {
            <GetEvent>c__AnonStorey308 storey;
            storey = new <GetEvent>c__AnonStorey308();
            storey.eventName = eventName;
            return this.events.Find(new Predicate<Event>(storey.<>m__2A2));
        }

        public Event[] GetEvents(string eventName)
        {
            <GetEvents>c__AnonStorey309 storey;
            storey = new <GetEvents>c__AnonStorey309();
            storey.eventName = eventName;
            return Enumerable.ToArray<Event>(Enumerable.Where<Event>(this.events, new Func<Event, bool>(storey.<>m__2A3)));
        }

        public Event[] GetEvents(EventTriggerType trigger)
        {
            <GetEvents>c__AnonStorey306 storey;
            storey = new <GetEvents>c__AnonStorey306();
            storey.trigger = trigger;
            return Enumerable.ToArray<Event>(Enumerable.Where<Event>(this.events, new Func<Event, bool>(storey.<>m__2A0)));
        }

        public Event[] GetHoldEvents(EventTriggerType trigger)
        {
            <GetHoldEvents>c__AnonStorey307 storey;
            storey = new <GetHoldEvents>c__AnonStorey307();
            storey.trigger = trigger;
            return Enumerable.ToArray<Event>(Enumerable.Where<Event>(this.events, new Func<Event, bool>(storey.<>m__2A1)));
        }

        public static int GetLockCount()
        {
            return m_Lock.Count;
        }

        public bool HasEvent(EventTriggerType trigger)
        {
            <HasEvent>c__AnonStorey304 storey;
            storey = new <HasEvent>c__AnonStorey304();
            storey.trigger = trigger;
            return ((this.events.FindIndex(new Predicate<Event>(storey.<>m__29E)) == -1) == 0);
        }

        public bool HasHoldEvent(EventTriggerType trigger)
        {
            <HasHoldEvent>c__AnonStorey305 storey;
            storey = new <HasHoldEvent>c__AnonStorey305();
            storey.trigger = trigger;
            return ((this.events.FindIndex(new Predicate<Event>(storey.<>m__29F)) == -1) == 0);
        }

        public static void Invoke(string eventName, object value)
        {
            int num;
            Listener listener;
            if (IsLock() == null)
            {
                goto Label_000B;
            }
            return;
        Label_000B:
            num = 0;
            goto Label_0040;
        Label_0012:
            listener = m_Listener[num];
            if ((listener.eventName == eventName) == null)
            {
                goto Label_003C;
            }
            listener.callback(0, value);
        Label_003C:
            num += 1;
        Label_0040:
            if (num < m_Listener.Count)
            {
                goto Label_0012;
            }
            return;
        }

        public bool IsFlag(Flag value)
        {
            return (((this.flag & value) == 0) == 0);
        }

        public static bool IsLock()
        {
            return (m_Lock.Count > 0);
        }

        private void LateUpdate()
        {
            this.RefreshButton();
            return;
        }

        public static void Lock()
        {
            Lock("system");
            return;
        }

        public static void Lock(string key)
        {
            Dictionary<string, int> dictionary;
            string str;
            int num;
            if (string.IsNullOrEmpty(key) == null)
            {
                goto Label_0012;
            }
            key = "system";
        Label_0012:
            if (m_Lock.ContainsKey(key) != null)
            {
                goto Label_0033;
            }
            m_Lock.Add(key, 1);
            goto Label_004D;
        Label_0033:
            num = dictionary[str];
            (dictionary = m_Lock)[str = key] = num + 1;
        Label_004D:
            return;
        }

        public override void OnBeginDrag(PointerEventData data)
        {
            IBeginDragHandler handler;
            this.m_DragMove = 0;
            this.m_Pos = data.get_position();
            if (this.HasEvent(13) == null)
            {
                goto Label_002F;
            }
            this.Send(13, data.get_position());
        Label_002F:
            if ((this.syncEvent != null) == null)
            {
                goto Label_0059;
            }
            handler = this.syncEvent.GetComponent<IBeginDragHandler>();
            if (handler == null)
            {
                goto Label_0059;
            }
            handler.OnBeginDrag(data);
        Label_0059:
            return;
        }

        public override void OnCancel(BaseEventData data)
        {
            if (this.HasEvent(0x10) == null)
            {
                goto Label_001C;
            }
            this.Send(0x10, data.get_selectedObject());
        Label_001C:
            return;
        }

        public override void OnDeselect(BaseEventData data)
        {
            if (this.HasEvent(10) == null)
            {
                goto Label_001C;
            }
            this.Send(10, data.get_selectedObject());
        Label_001C:
            return;
        }

        public override unsafe void OnDrag(PointerEventData data)
        {
            Vector2 vector;
            IDragHandler handler;
            if (this.HasEvent(5) == null)
            {
                goto Label_001A;
            }
            this.Send(5, data.get_position());
        Label_001A:
            if (this.m_DragMove != null)
            {
                goto Label_004F;
            }
            vector = data.get_position() - this.m_Pos;
            if (&vector.get_magnitude() <= 10f)
            {
                goto Label_004F;
            }
            this.m_DragMove = 1;
        Label_004F:
            if ((this.syncEvent != null) == null)
            {
                goto Label_0079;
            }
            handler = this.syncEvent.GetComponent<IDragHandler>();
            if (handler == null)
            {
                goto Label_0079;
            }
            handler.OnDrag(data);
        Label_0079:
            return;
        }

        public override void OnDrop(PointerEventData data)
        {
            if (this.HasEvent(6) == null)
            {
                goto Label_001A;
            }
            this.Send(6, data.get_position());
        Label_001A:
            return;
        }

        public override void OnEndDrag(PointerEventData data)
        {
            IEndDragHandler handler;
            this.m_DragMove = 0;
            if (this.HasEvent(14) == null)
            {
                goto Label_0023;
            }
            this.Send(14, data.get_position());
        Label_0023:
            if ((this.syncEvent != null) == null)
            {
                goto Label_004D;
            }
            handler = this.syncEvent.GetComponent<IEndDragHandler>();
            if (handler == null)
            {
                goto Label_004D;
            }
            handler.OnEndDrag(data);
        Label_004D:
            return;
        }

        private void OnHold(EventTriggerType trigger)
        {
            float num;
            if (this.m_HoldCheck == null)
            {
                goto Label_0013;
            }
            this.m_HoldOn = 0;
            return;
        Label_0013:
            if (this.m_Enter == null)
            {
                goto Label_0078;
            }
            if (this.IsFlag(2) == null)
            {
                goto Label_0035;
            }
            if (this.m_DragMove != null)
            {
                goto Label_007F;
            }
        Label_0035:
            num = Time.get_unscaledTime() - this.m_BeginEnterTime;
            if (num <= 0.5f)
            {
                goto Label_007F;
            }
            this.m_HoldCheck = 1;
            if (this.HasHoldEvent(trigger) == null)
            {
                goto Label_007F;
            }
            this.m_HoldOn = this.SendHold(trigger, this.m_Pos);
            goto Label_007F;
        Label_0078:
            this.m_HoldCheck = 1;
        Label_007F:
            return;
        }

        public override void OnInitializePotentialDrag(PointerEventData data)
        {
            IInitializePotentialDragHandler handler;
            if (this.HasEvent(12) == null)
            {
                goto Label_001C;
            }
            this.Send(12, data.get_position());
        Label_001C:
            if ((this.syncEvent != null) == null)
            {
                goto Label_0046;
            }
            handler = this.syncEvent.GetComponent<IInitializePotentialDragHandler>();
            if (handler == null)
            {
                goto Label_0046;
            }
            handler.OnInitializePotentialDrag(data);
        Label_0046:
            return;
        }

        public override void OnMove(AxisEventData data)
        {
            if (this.HasEvent(11) == null)
            {
                goto Label_001C;
            }
            this.Send(11, data.get_moveVector());
        Label_001C:
            return;
        }

        public override void OnPointerClick(PointerEventData data)
        {
            if (this.m_HoldOn == null)
            {
                goto Label_000C;
            }
            return;
        Label_000C:
            if (this.HasEvent(4) == null)
            {
                goto Label_0050;
            }
            this.OnHold(3);
            if (this.m_HoldOn == null)
            {
                goto Label_002B;
            }
            return;
        Label_002B:
            if (this.IsFlag(2) == null)
            {
                goto Label_0042;
            }
            if (this.m_DragMove != null)
            {
                goto Label_0050;
            }
        Label_0042:
            this.Send(4, data.get_position());
        Label_0050:
            return;
        }

        public override void OnPointerDown(PointerEventData data)
        {
            this.m_HoldCheck = 0;
            if (this.HasEvent(2) == null)
            {
                goto Label_0021;
            }
            this.Send(2, data.get_position());
        Label_0021:
            return;
        }

        public override void OnPointerEnter(PointerEventData data)
        {
            this.m_Enter = 1;
            this.m_BeginEnterTime = Time.get_unscaledTime();
            if (this.HasEvent(0) == null)
            {
                goto Label_002C;
            }
            this.Send(0, data.get_position());
        Label_002C:
            return;
        }

        public override void OnPointerExit(PointerEventData data)
        {
            this.m_Enter = 0;
            if (this.HasEvent(1) == null)
            {
                goto Label_0021;
            }
            this.Send(1, data.get_position());
        Label_0021:
            return;
        }

        public override void OnPointerUp(PointerEventData data)
        {
            if (this.HasEvent(3) == null)
            {
                goto Label_001A;
            }
            this.Send(3, data.get_position());
        Label_001A:
            return;
        }

        public override void OnScroll(PointerEventData data)
        {
            IScrollHandler handler;
            if (this.HasEvent(7) == null)
            {
                goto Label_001A;
            }
            this.Send(7, data.get_position());
        Label_001A:
            if ((this.syncEvent != null) == null)
            {
                goto Label_0044;
            }
            handler = this.syncEvent.GetComponent<IScrollHandler>();
            if (handler == null)
            {
                goto Label_0044;
            }
            handler.OnScroll(data);
        Label_0044:
            return;
        }

        public override void OnSelect(BaseEventData data)
        {
            if (this.HasEvent(9) == null)
            {
                goto Label_001C;
            }
            this.Send(9, data.get_selectedObject());
        Label_001C:
            return;
        }

        public override void OnSubmit(BaseEventData data)
        {
            if (this.HasEvent(15) == null)
            {
                goto Label_001C;
            }
            this.Send(15, data.get_selectedObject());
        Label_001C:
            return;
        }

        public override void OnUpdateSelected(BaseEventData data)
        {
            if (this.HasEvent(8) == null)
            {
                goto Label_001A;
            }
            this.Send(8, data.get_selectedObject());
        Label_001A:
            return;
        }

        public void PlaySe(Event ev)
        {
            if (ev.se < 0)
            {
                goto Label_0017;
            }
            SystemSound.Play(ev.se);
        Label_0017:
            return;
        }

        private void RefreshButton()
        {
            Toggle toggle;
            bool flag;
            if (this.IsFlag(1) == null)
            {
                goto Label_007F;
            }
            if ((this.m_Selectable != null) == null)
            {
                goto Label_007F;
            }
            if (this.selectableType != 3)
            {
                goto Label_0059;
            }
            toggle = this.m_Selectable as Toggle;
            if ((toggle != null) == null)
            {
                goto Label_0059;
            }
            if (toggle.get_isOn() == null)
            {
                goto Label_0059;
            }
            this.m_Selectable.set_interactable(0);
            return;
        Label_0059:
            flag = IsLock() == 0;
            if (this.m_Selectable.get_interactable() == flag)
            {
                goto Label_007F;
            }
            this.m_Selectable.set_interactable(flag);
        Label_007F:
            return;
        }

        public static void RemoveListener(Listener listener)
        {
            m_Listener.Remove(listener);
            return;
        }

        public static void Reset()
        {
            m_Lock.Clear();
            m_Listener.Clear();
            return;
        }

        public void ResetFlag(Flag value)
        {
            this.flag &= ~value;
            return;
        }

        public static void ResetLock(string key)
        {
            if (string.IsNullOrEmpty(key) != null)
            {
                goto Label_0017;
            }
            m_Lock.Remove(key);
        Label_0017:
            return;
        }

        private bool Send(Event[] evs, GameObject select)
        {
            bool flag;
            int num;
            Event event2;
            SerializeValueList list;
            flag = 0;
            if (evs == null)
            {
                goto Label_009D;
            }
            num = 0;
            goto Label_0094;
        Label_000F:
            event2 = evs[num];
            if (event2 == null)
            {
                goto Label_0090;
            }
            if (string.IsNullOrEmpty(event2.eventName) != null)
            {
                goto Label_0090;
            }
            if (this.isInteractable == null)
            {
                goto Label_0090;
            }
            this.PlaySe(event2);
            list = new SerializeValueList(event2.valueList);
            list.AddField("_self", base.get_gameObject());
            list.AddField("_select", select);
            if (event2.ignoreLock == null)
            {
                goto Label_0082;
            }
            ForceInvoke(event2.eventName, list);
            goto Label_008E;
        Label_0082:
            Invoke(event2.eventName, list);
        Label_008E:
            flag = 1;
        Label_0090:
            num += 1;
        Label_0094:
            if (num < ((int) evs.Length))
            {
                goto Label_000F;
            }
        Label_009D:
            return flag;
        }

        private bool Send(EventTriggerType trigger, GameObject select)
        {
            return this.Send(this.GetEvents(trigger), select);
        }

        private bool Send(Event[] evs, Vector2 pos)
        {
            bool flag;
            int num;
            Event event2;
            SerializeValueList list;
            flag = 0;
            if (evs == null)
            {
                goto Label_009D;
            }
            num = 0;
            goto Label_0094;
        Label_000F:
            event2 = evs[num];
            if (event2 == null)
            {
                goto Label_0090;
            }
            if (string.IsNullOrEmpty(event2.eventName) != null)
            {
                goto Label_0090;
            }
            if (this.isInteractable == null)
            {
                goto Label_0090;
            }
            this.PlaySe(event2);
            list = new SerializeValueList(event2.valueList);
            list.AddField("_self", base.get_gameObject());
            list.AddField("_pos", pos);
            if (event2.ignoreLock == null)
            {
                goto Label_0082;
            }
            ForceInvoke(event2.eventName, list);
            goto Label_008E;
        Label_0082:
            Invoke(event2.eventName, list);
        Label_008E:
            flag = 1;
        Label_0090:
            num += 1;
        Label_0094:
            if (num < ((int) evs.Length))
            {
                goto Label_000F;
            }
        Label_009D:
            return flag;
        }

        private bool Send(EventTriggerType trigger, Vector2 pos)
        {
            return this.Send(this.GetEvents(trigger), pos);
        }

        private bool Send(Event[] evs, Vector2 pos, Vector2 vct)
        {
            bool flag;
            int num;
            Event event2;
            SerializeValueList list;
            flag = 0;
            if (evs == null)
            {
                goto Label_00A4;
            }
            num = 0;
            goto Label_009B;
        Label_000F:
            event2 = evs[num];
            if (string.IsNullOrEmpty(event2.eventName) != null)
            {
                goto Label_0097;
            }
            if (this.isInteractable == null)
            {
                goto Label_0097;
            }
            this.PlaySe(event2);
            list = new SerializeValueList(event2.valueList);
            list.AddField("_self", base.get_gameObject());
            list.AddField("_pos", pos);
            list.AddField("_vct", vct);
            if (event2.ignoreLock == null)
            {
                goto Label_0089;
            }
            ForceInvoke(event2.eventName, list);
            goto Label_0095;
        Label_0089:
            Invoke(event2.eventName, list);
        Label_0095:
            flag = 1;
        Label_0097:
            num += 1;
        Label_009B:
            if (num < ((int) evs.Length))
            {
                goto Label_000F;
            }
        Label_00A4:
            return flag;
        }

        private bool Send(EventTriggerType trigger, Vector2 pos, Vector2 vct)
        {
            return this.Send(this.GetEvents(trigger), pos, vct);
        }

        private bool SendHold(EventTriggerType trigger, Vector2 pos)
        {
            bool flag;
            Event[] eventArray;
            int num;
            Event event2;
            SerializeValueList list;
            flag = 0;
            eventArray = this.GetHoldEvents(trigger);
            if (eventArray == null)
            {
                goto Label_00AA;
            }
            num = 0;
            goto Label_00A1;
        Label_0017:
            event2 = eventArray[num];
            if (event2 == null)
            {
                goto Label_009D;
            }
            if (string.IsNullOrEmpty(event2.eventName) != null)
            {
                goto Label_009D;
            }
            if (this.isInteractable == null)
            {
                goto Label_009D;
            }
            this.PlaySe(event2);
            list = new SerializeValueList(event2.valueList);
            list.AddField("_self", base.get_gameObject());
            list.AddField("_pos", pos);
            if (event2.ignoreLock == null)
            {
                goto Label_008E;
            }
            ForceInvoke(event2.eventName, list);
            goto Label_009B;
        Label_008E:
            Invoke(event2.eventName, list);
        Label_009B:
            flag = 1;
        Label_009D:
            num += 1;
        Label_00A1:
            if (num < ((int) eventArray.Length))
            {
                goto Label_0017;
            }
        Label_00AA:
            return flag;
        }

        public void SetFlag(Flag value)
        {
            this.flag |= value;
            return;
        }

        protected virtual void Start()
        {
        }

        public static void UnLock()
        {
            UnLock("system");
            return;
        }

        public static unsafe void UnLock(string key)
        {
            int num;
            if (string.IsNullOrEmpty(key) == null)
            {
                goto Label_0012;
            }
            key = "system";
        Label_0012:
            num = 0;
            if (m_Lock.TryGetValue(key, &num) == null)
            {
                goto Label_004E;
            }
            num -= 1;
            if (num <= 0)
            {
                goto Label_0042;
            }
            m_Lock[key] = num;
            goto Label_004E;
        Label_0042:
            m_Lock.Remove(key);
        Label_004E:
            return;
        }

        private void Update()
        {
            this.OnHold(0);
            return;
        }

        private bool isInteractable
        {
            get
            {
                if ((this.m_Selectable != null) == null)
                {
                    goto Label_001D;
                }
                return this.m_Selectable.IsInteractable();
            Label_001D:
                return 1;
            }
        }

        [CompilerGenerated]
        private sealed class <GetEvent>c__AnonStorey308
        {
            internal string eventName;

            public <GetEvent>c__AnonStorey308()
            {
                base..ctor();
                return;
            }

            internal bool <>m__2A2(ButtonEvent.Event prop)
            {
                return (prop.eventName == this.eventName);
            }
        }

        [CompilerGenerated]
        private sealed class <GetEvents>c__AnonStorey306
        {
            internal EventTriggerType trigger;

            public <GetEvents>c__AnonStorey306()
            {
                base..ctor();
                return;
            }

            internal bool <>m__2A0(ButtonEvent.Event prop)
            {
                return ((prop.hold != null) ? 0 : (prop.trigger == this.trigger));
            }
        }

        [CompilerGenerated]
        private sealed class <GetEvents>c__AnonStorey309
        {
            internal string eventName;

            public <GetEvents>c__AnonStorey309()
            {
                base..ctor();
                return;
            }

            internal bool <>m__2A3(ButtonEvent.Event prop)
            {
                return (prop.eventName == this.eventName);
            }
        }

        [CompilerGenerated]
        private sealed class <GetHoldEvents>c__AnonStorey307
        {
            internal EventTriggerType trigger;

            public <GetHoldEvents>c__AnonStorey307()
            {
                base..ctor();
                return;
            }

            internal bool <>m__2A1(ButtonEvent.Event prop)
            {
                return ((prop.hold == null) ? 0 : (prop.trigger == this.trigger));
            }
        }

        [CompilerGenerated]
        private sealed class <HasEvent>c__AnonStorey304
        {
            internal EventTriggerType trigger;

            public <HasEvent>c__AnonStorey304()
            {
                base..ctor();
                return;
            }

            internal bool <>m__29E(ButtonEvent.Event prop)
            {
                return ((prop.hold != null) ? 0 : (prop.trigger == this.trigger));
            }
        }

        [CompilerGenerated]
        private sealed class <HasHoldEvent>c__AnonStorey305
        {
            internal EventTriggerType trigger;

            public <HasHoldEvent>c__AnonStorey305()
            {
                base..ctor();
                return;
            }

            internal bool <>m__29F(ButtonEvent.Event prop)
            {
                return ((prop.hold == null) ? 0 : (prop.trigger == this.trigger));
            }
        }

        [Serializable]
        public class Event
        {
            public EventTriggerType trigger;
            public string eventName;
            public int se;
            [FormerlySerializedAs("ignoreLock")]
            public int flag;
            public SerializeValueList valueList;

            public Event()
            {
                this.trigger = 4;
                this.eventName = string.Empty;
                this.se = -1;
                this.valueList = new SerializeValueList();
                base..ctor();
                return;
            }

            private bool IsFlag(Flag value)
            {
                return (((this.flag & value) == 0) == 0);
            }

            private void SetFlag(Flag value, bool sw)
            {
                if (sw == null)
                {
                    goto Label_0019;
                }
                this.flag |= value;
                goto Label_0028;
            Label_0019:
                this.flag &= ~value;
            Label_0028:
                return;
            }

            public bool ignoreLock
            {
                get
                {
                    return this.IsFlag(1);
                }
                set
                {
                    this.SetFlag(1, value);
                    return;
                }
            }

            public bool hold
            {
                get
                {
                    return this.IsFlag(2);
                }
                set
                {
                    this.SetFlag(2, value);
                    return;
                }
            }

            private enum Flag
            {
                IGNORELOCK = 1,
                HOLD = 2
            }
        }

        public enum Flag
        {
            AUTOLOCK = 1,
            DRAGMOVELOCK = 2
        }

        public class Listener
        {
            public string eventName;
            public Action<bool, object> callback;

            public Listener()
            {
                base..ctor();
                return;
            }
        }

        public enum SelectableType
        {
            AUTO,
            BUTTON,
            TOGGLE,
            TAB
        }
    }
}

