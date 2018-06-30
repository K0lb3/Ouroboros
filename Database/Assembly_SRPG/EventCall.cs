namespace SRPG
{
    using System;
    using UnityEngine;
    using UnityEngine.Events;

    public class EventCall : MonoBehaviour
    {
        public CustomEvent m_Events;
        public static object currentValue;

        public EventCall()
        {
            base..ctor();
            return;
        }

        public void Invoke(string key, string value)
        {
            currentValue = null;
            this.m_Events.Invoke(key, value);
            return;
        }

        public void Invoke(string key, string value, object obj)
        {
            currentValue = obj;
            this.m_Events.Invoke(key, value);
            return;
        }

        [Serializable]
        public class CustomEvent : UnityEvent<string, string>
        {
            public CustomEvent()
            {
                base..ctor();
                return;
            }
        }
    }
}

