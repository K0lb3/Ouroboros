namespace SRPG
{
    using GR;
    using System;

    [Pin(0, "Create New Account", 0, 0), NodeType("System/NewEmailGameRegister", 0x7fe5), Pin(10, "Success", 1, 10), Pin(11, "Failed", 1, 11)]
    public class FlowNode_NewGameEmailRegister : FlowNode
    {
        private const int PIN_INPUT = 0;
        private const int PIN_SUCCESS = 10;
        private const int PIN_FAILED = 11;

        public FlowNode_NewGameEmailRegister()
        {
            base..ctor();
            return;
        }

        private unsafe void ImmediateResponseCallback(WWWResult www)
        {
            Network.RemoveAPI();
            base.set_enabled(0);
            if (0 > &www.text.IndexOf("\"is_succeeded\":true"))
            {
                goto Label_0031;
            }
            base.ActivateOutputLinks(10);
            goto Label_003A;
        Label_0031:
            base.ActivateOutputLinks(11);
        Label_003A:
            return;
        }

        public override void OnActivate(int pinID)
        {
            string str;
            ReqAuthEmailRegister register;
            if (pinID != null)
            {
                goto Label_0071;
            }
            str = FlowNode_NewGameRegister.gDeviceID.ToString().Replace("-", string.Empty).Substring(0, 0x10);
            FlowNode_NewGameRegister.gEmail = str;
            register = new ReqAuthEmailRegister(str, FlowNode_NewGameRegister.gPassword, FlowNode_NewGameRegister.gDeviceID, MonoSingleton<GameManager>.Instance.SecretKey, MonoSingleton<GameManager>.Instance.UdId, new Network.ResponseCallback(this.ImmediateResponseCallback));
            Network.RequestAPIImmediate(register, 1);
            base.set_enabled(1);
        Label_0071:
            return;
        }
    }
}

