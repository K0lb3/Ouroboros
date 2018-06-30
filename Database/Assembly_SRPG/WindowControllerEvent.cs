namespace SRPG
{
    using System;
    using UnityEngine;

    public class WindowControllerEvent : StateMachineBehaviour
    {
        public EventTypes Type;

        public WindowControllerEvent()
        {
            base..ctor();
            return;
        }

        public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            WindowController controller;
            controller = animator.GetComponent<WindowController>();
            if ((controller != null) == null)
            {
                goto Label_002F;
            }
            if (this.Type != null)
            {
                goto Label_0029;
            }
            controller.OnOpen();
            goto Label_002F;
        Label_0029:
            controller.OnClose();
        Label_002F:
            return;
        }

        public enum EventTypes
        {
            Opened,
            Closed
        }
    }
}

