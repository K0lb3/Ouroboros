namespace SRPG
{
    using System;
    using UnityEngine;
    using UnityEngine.UI;

    [Pin(3, "Finished", 1, 100), Pin(1, "Enable", 0, 0), NodeType("FgGID/FgGWebView", 0x7fe5), Pin(2, "Disable", 0, 1)]
    public class FlowNode_FgGWebView : FlowNode
    {
        private const int PIN_ID_ENABLE = 1;
        private const int PIN_ID_DISABLE = 2;
        private const int PIN_ID_FINISHED = 3;
        [DropTarget(typeof(GameObject), true), ShowInInfo]
        public GameObject Target;
        public string URL;
        public RawImage mClientArea;

        public FlowNode_FgGWebView()
        {
            base..ctor();
            return;
        }

        public override void OnActivate(int pinID)
        {
            int num;
            num = pinID;
            if (num == 1)
            {
                goto Label_0015;
            }
            if (num == 2)
            {
                goto Label_003D;
            }
            goto Label_005F;
        Label_0015:
            if ((this.Target != null) == null)
            {
                goto Label_005F;
            }
            this.mClientArea.set_enabled(1);
            this.OpenURL();
            goto Label_005F;
        Label_003D:
            if ((this.Target != null) == null)
            {
                goto Label_005F;
            }
            this.mClientArea.set_enabled(0);
        Label_005F:
            return;
        }

        private void OpenURL()
        {
        }
    }
}

