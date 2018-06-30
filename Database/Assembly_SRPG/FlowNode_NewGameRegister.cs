namespace SRPG
{
    using GR;
    using System;
    using System.Runtime.CompilerServices;

    [Pin(0, "Create New Account", 0, 0), NodeType("System/NewGameRegister", 0x7fe5), Pin(10, "Success", 1, 10)]
    public class FlowNode_NewGameRegister : FlowNode_Network
    {
        public static string gPassword;
        [CompilerGenerated]
        private static string <gDeviceID>k__BackingField;
        [CompilerGenerated]
        private static string <gEmail>k__BackingField;

        static FlowNode_NewGameRegister()
        {
            gPassword = "DmmPassword";
            return;
        }

        public FlowNode_NewGameRegister()
        {
            base..ctor();
            return;
        }

        public override void OnActivate(int pinID)
        {
            string str;
            string str2;
            if (pinID != null)
            {
                goto Label_0047;
            }
            MonoSingleton<GameManager>.Instance.InitAuth();
            str = MonoSingleton<GameManager>.Instance.SecretKey;
            str2 = MonoSingleton<GameManager>.Instance.UdId;
            base.ExecRequest(new ReqGetDeviceID(str, str2, new Network.ResponseCallback(this.ResponseCallback)));
            base.set_enabled(1);
        Label_0047:
            return;
        }

        public override unsafe void OnSuccess(WWWResult www)
        {
            WebAPI.JSON_BodyResponse<JSON_DeviceID> response;
            Network.EErrCode code;
            if (Network.IsError == null)
            {
                goto Label_002E;
            }
            if (Network.ErrCode == 0x4b0)
            {
                goto Label_0020;
            }
            goto Label_0027;
        Label_0020:
            this.OnFailed();
            return;
        Label_0027:
            this.OnFailed();
            return;
        Label_002E:
            response = JSONParser.parseJSONObject<WebAPI.JSON_BodyResponse<JSON_DeviceID>>(&www.text);
            DebugUtility.Assert((response == null) == 0, "res == null");
            gDeviceID = response.body.device_id;
            Network.RemoveAPI();
            this.Success();
            return;
        }

        private void Success()
        {
            base.set_enabled(0);
            base.ActivateOutputLinks(10);
            return;
        }

        public static string gDeviceID
        {
            [CompilerGenerated]
            get
            {
                return <gDeviceID>k__BackingField;
            }
            [CompilerGenerated]
            set
            {
                <gDeviceID>k__BackingField = value;
                return;
            }
        }

        public static string gEmail
        {
            [CompilerGenerated]
            get
            {
                return <gEmail>k__BackingField;
            }
            [CompilerGenerated]
            set
            {
                <gEmail>k__BackingField = value;
                return;
            }
        }

        private class JSON_DeviceID
        {
            public string device_id;

            public JSON_DeviceID()
            {
                base..ctor();
                return;
            }
        }
    }
}

