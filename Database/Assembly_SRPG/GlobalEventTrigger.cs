namespace SRPG
{
    using System;
    using UnityEngine;

    [DisallowMultipleComponent, AddComponentMenu("Event/Global Event Trigger")]
    public class GlobalEventTrigger : MonoBehaviour
    {
        public GlobalEventTrigger()
        {
            base..ctor();
            return;
        }

        public void Trigger(string eventName)
        {
            GlobalEvent.Invoke(eventName, base.get_gameObject());
            return;
        }
    }
}

