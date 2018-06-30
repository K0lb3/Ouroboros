namespace SRPG
{
    using GR;
    using System;
    using UnityEngine;

    [Pin(10, "output", 1, 10), Pin(1, "Start", 0, 0), NodeType("System/Notify", 0x7fe5)]
    public class FlowNode_Notify : FlowNode
    {
        public GameObject NotifyListTemplate;

        public FlowNode_Notify()
        {
            base..ctor();
            return;
        }

        public override void OnActivate(int pinID)
        {
            if (pinID != 1)
            {
                goto Label_0017;
            }
            MonoSingleton<GameManager>.Instance.InitNotifyList(this.NotifyListTemplate);
        Label_0017:
            base.ActivateOutputLinks(10);
            return;
        }
    }
}

