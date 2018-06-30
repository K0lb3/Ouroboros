namespace SRPG
{
    using Gsc.Auth;
    using System;

    [Pin(10, "Success", 1, 10), NodeType("System/NewGameEmailDevice", 0x7fe5), Pin(11, "Failed", 1, 11), Pin(0, "Chain New Account", 0, 0)]
    public class FlowNode_NewGameEmailDevice : FlowNode
    {
        private const int PIN_INPUT = 0;
        private const int PIN_SUCCESS = 10;
        private const int PIN_FAILED = 11;

        public FlowNode_NewGameEmailDevice()
        {
            base..ctor();
            return;
        }

        private unsafe void AddUserResponse(AddDeviceWithEmailAddressAndPasswordResult res)
        {
            AddDeviceWithEmailAddressAndPasswordResultCode code;
            switch (&res.ResultCode)
            {
                case 0:
                    goto Label_0023;

                case 1:
                    goto Label_0036;

                case 2:
                    goto Label_0036;

                case 3:
                    goto Label_0036;
            }
            goto Label_0036;
        Label_0023:
            GameUtility.ClearPreferences();
            base.ActivateOutputLinks(10);
            goto Label_0044;
        Label_0036:
            base.ActivateOutputLinks(11);
        Label_0044:
            return;
        }

        public override void OnActivate(int pinID)
        {
            if (pinID != null)
            {
                goto Label_0027;
            }
            Session.DefaultSession.AddDeviceWithEmailAddressAndPassword(FlowNode_NewGameRegister.gEmail, FlowNode_NewGameRegister.gPassword, new Action<AddDeviceWithEmailAddressAndPasswordResult>(this.AddUserResponse));
        Label_0027:
            return;
        }
    }
}

