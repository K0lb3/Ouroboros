namespace SRPG
{
    using System;
    using UnityEngine;

    [NodeType("System/App Version", 0x7fe5), Pin(0, "In", 0, 0), Pin(1, "Default", 1, 1)]
    public class FlowNode_AppVer : FlowNode
    {
        [FlexibleArray]
        public string[] Versions;

        public FlowNode_AppVer()
        {
            this.Versions = new string[0];
            base..ctor();
            return;
        }

        public override FlowNode.Pin[] GetDynamicPins()
        {
            FlowNode.Pin[] pinArray;
            int num;
            pinArray = new FlowNode.Pin[(int) this.Versions.Length];
            num = 0;
            goto Label_0030;
        Label_0015:
            pinArray[num] = new FlowNode.Pin(2 + num, this.Versions[num], 1, 2 + num);
            num += 1;
        Label_0030:
            if (num < ((int) this.Versions.Length))
            {
                goto Label_0015;
            }
            return pinArray;
        }

        public override void OnActivate(int pinID)
        {
            int num;
            if (pinID != null)
            {
                goto Label_0049;
            }
            num = 0;
            goto Label_0033;
        Label_000D:
            if ((Application.get_version() == this.Versions[num]) == null)
            {
                goto Label_002F;
            }
            base.ActivateOutputLinks(2 + num);
            return;
        Label_002F:
            num += 1;
        Label_0033:
            if (num < ((int) this.Versions.Length))
            {
                goto Label_000D;
            }
            base.ActivateOutputLinks(1);
        Label_0049:
            return;
        }
    }
}

