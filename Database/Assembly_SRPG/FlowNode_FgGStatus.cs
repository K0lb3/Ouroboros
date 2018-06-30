namespace SRPG
{
    using GR;
    using System;

    [Pin(3, "未連携", 1, 2), Pin(4, "連携済み", 1, 3), Pin(2, "非表示", 1, 1), Pin(1, "Input", 0, 0), NodeType("FgGID/FgGStatus", 0x7fe5)]
    public class FlowNode_FgGStatus : FlowNode
    {
        private const int PIN_ID_INPUT = 1;
        private const int PIN_ID_DISABLE = 2;
        private const int PIN_ID_NOT_SYNCHRONIZED = 3;
        private const int PIN_ID_SYNCHRONIZED = 4;

        public FlowNode_FgGStatus()
        {
            base..ctor();
            return;
        }

        public override void OnActivate(int pinID)
        {
            ReqFgGAuth.eAuthStatus status;
            switch ((MonoSingleton<GameManager>.Instance.AuthStatus - 1))
            {
                case 0:
                    goto Label_0024;

                case 1:
                    goto Label_0031;

                case 2:
                    goto Label_003E;
            }
            goto Label_004B;
        Label_0024:
            base.ActivateOutputLinks(2);
            goto Label_0058;
        Label_0031:
            base.ActivateOutputLinks(3);
            goto Label_0058;
        Label_003E:
            base.ActivateOutputLinks(4);
            goto Label_0058;
        Label_004B:
            base.ActivateOutputLinks(2);
        Label_0058:
            return;
        }
    }
}

