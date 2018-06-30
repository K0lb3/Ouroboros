namespace SRPG
{
    using GR;
    using System;
    using System.Runtime.CompilerServices;
    using UnityEngine;
    using UnityEngine.UI;

    [Pin(0, "RequestAdd", 0, 0), Pin(1, "RequestRelease", 0, 1), Pin(2, "Success", 1, 2), NodeType("Network/fgg_device", 0x7fe5)]
    public class FlowNode_ReqFggDevice : FlowNode_Network
    {
        public string HikkoshiFgGMailAddress;
        public string HikkoshiFgGPassword;
        [CompilerGenerated]
        private API <m_Api>k__BackingField;

        public FlowNode_ReqFggDevice()
        {
            base..ctor();
            return;
        }

        private void Failure(int pinID)
        {
            base.set_enabled(0);
            base.ActivateOutputLinks(pinID);
            return;
        }

        public override void OnActivate(int pinID)
        {
            GameObject obj2;
            GameObject obj3;
            InputField field;
            InputField field2;
            if (pinID != null)
            {
                goto Label_00BA;
            }
            if (Network.Mode != 1)
            {
                goto Label_0018;
            }
            this.Success();
            return;
        Label_0018:
            obj2 = GameObjectID.FindGameObject(this.HikkoshiFgGMailAddress);
            obj3 = GameObjectID.FindGameObject(this.HikkoshiFgGPassword);
            if ((obj2 == null) != null)
            {
                goto Label_004F;
            }
            if (((field = obj2.GetComponent<InputField>()) == null) == null)
            {
                goto Label_005A;
            }
        Label_004F:
            DebugUtility.LogError("InputField doesn't exist.");
            return;
        Label_005A:
            if ((obj3 == null) != null)
            {
                goto Label_0079;
            }
            if (((field2 = obj3.GetComponent<InputField>()) == null) == null)
            {
                goto Label_0084;
            }
        Label_0079:
            DebugUtility.LogError("InputField doesn't exist.");
            return;
        Label_0084:
            base.ExecRequest(new ReqGAuthFgGDevice(SystemInfo.get_deviceUniqueIdentifier(), field.get_text(), field2.get_text(), new Network.ResponseCallback(this.ResponseCallback)));
            base.set_enabled(1);
            this.m_Api = 0;
        Label_00BA:
            return;
        }

        public override unsafe void OnSuccess(WWWResult www)
        {
            WebAPI.JSON_BodyResponse<ReqGAuthFgGDevice.Json_FggDevice> response;
            Network.EErrCode code;
            if (Network.IsError == null)
            {
                goto Label_0046;
            }
            code = Network.ErrCode;
            switch ((code - 0x2260))
            {
                case 0:
                    goto Label_0038;

                case 1:
                    goto Label_0028;

                case 2:
                    goto Label_0038;
            }
        Label_0028:
            if (code == 0x13ed)
            {
                goto Label_0038;
            }
            goto Label_003F;
        Label_0038:
            this.OnBack();
            return;
        Label_003F:
            this.OnRetry();
            return;
        Label_0046:
            if (this.m_Api != null)
            {
                goto Label_00B1;
            }
            response = JSONParser.parseJSONObject<WebAPI.JSON_BodyResponse<ReqGAuthFgGDevice.Json_FggDevice>>(&www.text);
            DebugUtility.Assert((response == null) == 0, "res == null");
            if (response.body != null)
            {
                goto Label_0081;
            }
            this.OnRetry();
            return;
        Label_0081:
            MonoSingleton<GameManager>.Instance.SaveAuthWithKey(response.body.device_id, response.body.secret_key);
            MonoSingleton<GameManager>.Instance.InitAuth();
            GameUtility.ClearPreferences();
        Label_00B1:
            Network.RemoveAPI();
            this.Success();
            return;
        }

        private void Success()
        {
            base.set_enabled(0);
            base.ActivateOutputLinks(2);
            return;
        }

        private API m_Api
        {
            [CompilerGenerated]
            get
            {
                return this.<m_Api>k__BackingField;
            }
            [CompilerGenerated]
            set
            {
                this.<m_Api>k__BackingField = value;
                return;
            }
        }

        private enum API
        {
            Add,
            Release
        }
    }
}

