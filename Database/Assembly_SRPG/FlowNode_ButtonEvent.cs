namespace SRPG
{
    using System;
    using UnityEngine;

    [NodeType("Event/ButtonEvent", 0xef7fe5), Pin(1, "Triggered", 1, 0), AddComponentMenu("")]
    public class FlowNode_ButtonEvent : FlowNode
    {
        public static object currentValue;
        [StringIsButtonEventID]
        public string EventName;
        [BitMask]
        public CriticalSections CSMask;
        public bool DoLock;
        public string LockKey;
        private ButtonEvent.Listener m_Listener;

        static FlowNode_ButtonEvent()
        {
        }

        public FlowNode_ButtonEvent()
        {
            this.CSMask = -1;
            base..ctor();
            return;
        }

        protected override void Awake()
        {
            base.Awake();
            if (string.IsNullOrEmpty(this.EventName) != null)
            {
                goto Label_0033;
            }
            this.m_Listener = ButtonEvent.AddListener(this.EventName, new Action<bool, object>(this.OnButtonEvent));
        Label_0033:
            return;
        }

        public override string[] GetInfoLines()
        {
            string[] textArray1;
            if (string.IsNullOrEmpty(this.EventName) != null)
            {
                goto Label_0042;
            }
            textArray1 = new string[] { "Event is " + this.EventName, "CsMask is " + ((CriticalSections) this.CSMask) };
            return textArray1;
        Label_0042:
            return base.GetInfoLines();
        }

        private void OnButtonEvent(bool isForce, object obj)
        {
            CriticalSections sections;
            if (isForce != null)
            {
                goto Label_004A;
            }
            if ((CriticalSection.GetActive() & this.CSMask) != null)
            {
                goto Label_0049;
            }
            if ((HomeWindow.Current != null) == null)
            {
                goto Label_0039;
            }
            if (HomeWindow.Current.IsSceneChanging == null)
            {
                goto Label_0039;
            }
            return;
        Label_0039:
            if (SRPG_InputField.IsFocus == null)
            {
                goto Label_004A;
            }
            return;
            goto Label_004A;
        Label_0049:
            return;
        Label_004A:
            if (this.DoLock == null)
            {
                goto Label_0060;
            }
            ButtonEvent.Lock(this.LockKey);
        Label_0060:
            currentValue = obj;
            base.ActivateOutputLinks(1);
            return;
        }

        protected override void OnDestroy()
        {
            if (this.m_Listener == null)
            {
                goto Label_001D;
            }
            ButtonEvent.RemoveListener(this.m_Listener);
            this.m_Listener = null;
        Label_001D:
            return;
        }
    }
}

