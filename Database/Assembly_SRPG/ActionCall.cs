namespace SRPG
{
    using System;
    using UnityEngine;
    using UnityEngine.Events;

    public class ActionCall : MonoBehaviour
    {
        public string m_EventName;
        public CustomEvent m_Events;
        public SerializeValueList m_Value;

        public ActionCall()
        {
            base..ctor();
            return;
        }

        private void Awake()
        {
            this.m_Value.AddField("self", base.get_gameObject());
            this.m_Events.Invoke(this.m_EventName, 0, this.m_Value);
            return;
        }

        public void Invoke(EventType eventType, SerializeValueList value)
        {
            SerializeValueList list;
            list = new SerializeValueList(this.m_Value);
            list.Write(value);
            this.m_Events.Invoke(this.m_EventName, eventType, list);
            return;
        }

        private void OnDisable()
        {
            this.m_Events.Invoke(this.m_EventName, 4, this.m_Value);
            return;
        }

        private void OnEnable()
        {
            this.m_Events.Invoke(this.m_EventName, 3, this.m_Value);
            return;
        }

        private void Start()
        {
            this.m_Events.Invoke(this.m_EventName, 1, this.m_Value);
            return;
        }

        private void Update()
        {
            this.m_Events.Invoke(this.m_EventName, 2, this.m_Value);
            return;
        }

        [Serializable]
        public class CustomEvent : UnityEvent<string, ActionCall.EventType, SerializeValueList>
        {
            public CustomEvent()
            {
                base..ctor();
                return;
            }
        }

        public enum EventType
        {
            AWAKE,
            START,
            UPDATE,
            ONENABLE,
            ONDISABLE,
            OPEN,
            OPENED,
            EVERY,
            CLOSE,
            CLOSED,
            CUSTOM1,
            CUSTOM2,
            CUSTOM3,
            CUSTOM4,
            CUSTOM5,
            CUSTOM6,
            CUSTOM7,
            CUSTOM8
        }
    }
}

